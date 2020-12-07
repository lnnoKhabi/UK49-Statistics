using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brain
{
	public partial class Statistics : Form
	{
		private Dictionary<string, List<string>> date_AND_combination;
		bool imported = false;


		public Statistics()
		{
			InitializeComponent();
		}

		private void button1_generate_Click( object sender, EventArgs e )
		{
			try
			{
				Generate_Info();
			}
			catch ( Exception ex )
			{
				toolStripStatusLabel1_info_label.Text = ex.Message;
			}
		}

		private void ComboBox_Cycle_Date_SelectionChangeCommitted( object sender, EventArgs e )
		{
			try
			{
				if ( imported )
				{
					if ( ComboBox_Cycle_Date.SelectedIndex == 1 )
					{
						DateTime date = GetLastCycleDate();
						dateTimePicker1_from.Value = date;
						toolStripStatusLabel1_info_label.Text = $"'From' date set to {date.ToShortDateString()}.";
					}
					else
					{
						dateTimePicker1_from.Value = DateTime.Parse(date_AND_combination.Keys.ElementAt(0));
						toolStripStatusLabel1_info_label.Text = $"'From' date set to {dateTimePicker1_from.Value.ToShortDateString()}.";

					}
				}
				else
				{
					toolStripStatusLabel1_info_label.Text = "You need to import some results.";
				}
			}
			catch ( Exception exc )
			{
				toolStripStatusLabel1_info_label.Text = exc.Message;

			}
		}

		private void listView1_Table_ItemSelectionChanged( object sender, ListViewItemSelectionChangedEventArgs e )
		{
			toolStripStatusLabel1_info_label.Text = $"{listView1_Table.SelectedItems.Count} item(s) selected.";

		}

		private void Statistics_Shown( object sender, EventArgs e )
		{
			try
			{
				if ( Form1.date_AND_combination.Count > 1 )
				{
					date_AND_combination = new Dictionary<string, List<string>>(Form1.date_AND_combination);//populate dict
					imported = true;
					//set from and to date
					dateTimePicker1_from.Value = DateTime.Parse(date_AND_combination.Keys.ElementAt(0));
					dateTimePicker2_to.Value = DateTime.Parse(date_AND_combination.Keys.ElementAt(date_AND_combination.Count - 1));
				}
				//toolStripStatusLabel1_info_label.Text = isLinear(new int[] {3,9,20,40}).ToString();
			}
			catch ( Exception ex )
			{
				toolStripStatusLabel1_info_label.Text = ex.Message;

			}
		}

		private void DrawFrequencyGraph()
		{
			Dictionary<string, List<string>> Numbers = new Dictionary<string, List<string>>(date_AND_combination
						.Where(a => DateTime.Parse(a.Key) >= dateTimePicker1_from.Value)
						.Where(b => DateTime.Parse(b.Key) <= dateTimePicker2_to.Value)
						.ToDictionary(c => c.Key, d => d.Value)
						);

			chart2_all_freq.Series[ 0 ].Points.Clear();
			chart2_all_freq.Series[ 1 ].Points.Clear();

			Dictionary<int, int> num_playcount_all = new Dictionary<int, int>();
			Dictionary<int, int> num_playcount_all_bonus = new Dictionary<int, int>();
			for ( int i = 1; i < 50; i++ )//populate dicts with default 0 values
			{
				//main set info
				num_playcount_all.Add(i, Numbers
					.Where(a => Array.ConvertAll(a.Value.ToArray(), b => int.Parse(b)).ToList().Contains(i))
					.Count());

				//bonus info
				num_playcount_all_bonus.Add(i, Numbers
					.Where(a => Array.ConvertAll(a.Value.ToArray(), b => int.Parse(b)).ToList()[ 6 ] == i)
					.Count());

			}

			//draw main numbers for all numbers
			chart2_all_freq.ChartAreas[ 0 ].AxisY.Maximum = num_playcount_all.Values.Max() + 10 - ( num_playcount_all.Values.Max() % 10 );
			foreach ( int num in num_playcount_all.Keys )
			{
				//draw chart info for main numbers
				chart2_all_freq.Series[ 0 ].Points.AddXY(num, num_playcount_all[ num ] - num_playcount_all_bonus[ num ]);
			}

			//draw bonus numbers for all numbers
			foreach ( int num in num_playcount_all_bonus.Keys )
			{
				//draw chart info for bonus
				chart2_all_freq.Series[ 1 ].Points.AddXY(num, num_playcount_all_bonus[ num ]);
			}
		}

		private void Generate_Info()
		{
			try
			{
				if (imported)
				{
					DrawFrequencyGraph();
					Cycle();
					NumbersStats();
					listView1_ActualPred.Items.Add($"{Prediction()}");

					toolStripStatusLabel1_info_label.Text = "Complete.";

				}
				else
				{
					toolStripStatusLabel1_info_label.Text = "You need to import some results.";
				}
			}
			catch ( Exception e )
			{

				toolStripStatusLabel1_info_label.Text = e.Message;

			}
		}

		private void NumbersStats()
		{
			listView1_Table.Items.Clear();
			listView1_Table.Groups.Add(new ListViewGroup("Top Numbers"));
			listView1_Table.Groups.Add(new ListViewGroup("Bottom Numbers"));

			Dictionary<string, List<string>> Numbers = new Dictionary<string, List<string>>(date_AND_combination
						.Where(a => DateTime.Parse(a.Key) >= dateTimePicker1_from.Value)
						.Where(b => DateTime.Parse(b.Key) <= dateTimePicker2_to.Value)
						.ToDictionary(c => c.Key, d => d.Value)
						);

			//get number of time nums where played
			Dictionary<int, int> PlayTime = new Dictionary<int, int>();
			for ( int i = 1; i < 50; i++ )
			{
				PlayTime.Add(i, Numbers.Values.Where(list => Array.ConvertAll(list.ToArray(), num => int.Parse(num)).Contains(i)).Count());
			}
			PlayTime = PlayTime.OrderByDescending(order => order.Value).ToDictionary(k => k.Key, v => v.Value);

			Dictionary<int, List<string>> PickedWith = new Dictionary<int, List<string>>();

			//get last pick (days)
			Dictionary<int, int> LastPicked = new Dictionary<int, int>();
			DateTime today = DateTime.Parse( Numbers.Keys.ElementAt(Numbers.Keys.Count() - 1)).AddDays(1);


			for ( int i = 1; i < 50; i++ )
			{
				string last_play_date = null;
				try
				{
					last_play_date = Numbers.Keys.Where(list => Array.ConvertAll(Numbers[ list ].ToArray(), num => int.Parse(num)).Contains(i)).Last();
				}
				catch
				{
					last_play_date = today.ToShortDateString();
				}
				List<string> with = last_play_date == today.ToShortDateString() ? new List<string>():Numbers[last_play_date];

				LastPicked.Add(i, ( today - DateTime.Parse(last_play_date)).Days);

				PickedWith.Add(i, with);
			}

			Dictionary<int, int[]> MostAndLastPlayOrdering = new Dictionary<int, int[]>();
			for ( int m = 1; m < 50; m++ )
			{
				if ( PlayTime.ContainsKey(m) )
				{
					MostAndLastPlayOrdering.Add(m, new int[ 2 ] { PlayTime[ m ], LastPicked[ m ] });
				}
			}
			//order the dict by the number of time a number was played then by when last the number was played
			MostAndLastPlayOrdering = MostAndLastPlayOrdering.OrderByDescending(pt => pt.Value[ 0 ]).ThenBy(lp => lp.Value[ 1 ]).ToDictionary(k => k.Key, v => v.Value);
			PlayTime.Clear();
			foreach ( int num in MostAndLastPlayOrdering.Keys )
			{
				PlayTime.Add(num, MostAndLastPlayOrdering[ num ][ 0 ]);
			}

			for ( int i = 0; i < PlayTime.Count(); i++ )
			{

				int number = PlayTime.Keys.ElementAt(i);
				Dictionary<string, List<string>> d = new Dictionary<string, List<string>>(Numbers.Where(a => a.Value.Contains($"{number}")).ToDictionary(b=>b.Key,c=>c.Value));
				//get most paired with number
				int paired_with = 0;
				int paired_times = 0;
				for ( int k = 1; k < 50; k++ )
				{
					int count = k == number?0:d.Values.Where(a => a.Contains($"{k}")).Count();
					paired_with = count > paired_times ? k : paired_with;
					paired_times = count > paired_times ? count : paired_times;
				}

				//get average pick
				string average_pick = LastPicked[ number ] == 0? "Never drawn." : $"{(double)Numbers.Count / (double)PlayTime[ number ]} days.";
				string last_pick = LastPicked[ number ] == 0? "Never drawn.":$"{LastPicked[ number ]} day(s) ago. ({today.Subtract(new TimeSpan(LastPicked[number],0,0,0)).ToShortDateString()})";

				//get picked with numbers
				List<string> LastP = new List<string>();
				string last_picked_with = null;
				if ( PickedWith[ number ].Count() > 0 )
				{
					string bonus = PickedWith[ number ][ 6 ];
					for ( int j = 0; j < 6; j++ )
					{
						LastP.Add($"{PickedWith[ number ][ j ]}");
					}
					LastP = LastP.OrderBy(o => int.Parse(o)).ToList();
					last_picked_with = $"{LastP[ 0 ]} - {LastP[ 1 ]} - {LastP[ 2 ]} - {LastP[ 3 ]} - {LastP[ 4 ]} - {LastP[ 5 ]} - [{bonus}]";
				}
				else last_picked_with = "Never drawn.";
				//get item group
				ListViewGroup group = i < 24 ? listView1_Table.Groups[ 0 ]: listView1_Table.Groups[ 1] ;


				//get times picked
				string times_picked = PlayTime[ number ] == 0 ? "Never drawn." : $"{PlayTime[ number ]} time(s).";

				string pair = paired_with == 0 ? "Never drawn." : $"{paired_with} ({paired_times} times.)";

				//add to listview
				ListViewItem item = listView1_Table.Items.Add($"{number}");
				item.SubItems.Add($"{times_picked}");
				item.SubItems.Add($"{last_pick}");
				item.SubItems.Add($"{average_pick}");
				item.SubItems.Add($"{last_picked_with}");
				item.SubItems.Add(pair);
				item.Group = group;
			}
		}

		private void Cycle()
		{
			listView1_Cycle.Items.Clear();
			List<string> one_to_49 = new List<string>();

			int count = 1;

			//Dictionary<string, List<string>> Numbers = new Dictionary<string, List<string>>(date_AND_combination
			//			.Where(a => DateTime.Parse(a.Key) >= dateTimePicker1_from.Value)
			//			.Where(b => DateTime.Parse(b.Key) <= dateTimePicker2_to.Value)
			//			.ToDictionary(c => c.Key, d => d.Value)
			//			);
			Dictionary<string, List<string>> Numbers = new Dictionary<string, List<string>>(date_AND_combination);
			int LastNumFound = 0;

			
			foreach ( string date_key in Numbers.Keys )
			{
				string _date = DateTime.Parse(date_key).ToShortDateString();
				string State1 = date_key.Contains(" ") ? "T" : "L";

				LastNumFound = 0;


				foreach ( string number in Numbers[ date_key ] )
				{
					if ( one_to_49.Count == 0 ) //populate numbers 1 - 49 if its empty
					{
						List<int> DontIncludeInCycle = new List<int>();
						if(DateTime.Parse(date_key) >= dateTimePicker1_from.Value && DateTime.Parse(date_key) <= dateTimePicker2_to.Value )
						{

							listView1_Cycle.Items.Add($"Cyle {count} start date: {_date} {State1}");
						}
						string start_comb = "";
						foreach ( string n in Numbers[ date_key ] )
						{
							int p_n = int.Parse(n);
							if ( LastNumFound != 0 || LastNumFound == p_n )
							{
								DontIncludeInCycle.Add(p_n);
								LastNumFound = LastNumFound == p_n?0:LastNumFound;
								continue;
							}
							
							start_comb += Numbers[ date_key ].IndexOf( n) == 6?$"[{n}]": n + " - ";
						}
						if ( DateTime.Parse(date_key) >= dateTimePicker1_from.Value && DateTime.Parse(date_key) <= dateTimePicker2_to.Value )
						{
							listView1_Cycle.Items.Add($"Starting Results: {start_comb}");
						}
						//listView1_Cycle.Items.Add($"Starting Results: {Numbers[ date_key ][ 0 ]} - {Numbers[ date_key ][ 1 ]} - {Numbers[ date_key ][ 2 ]} - {Numbers[ date_key ][ 3 ]} - {Numbers[ date_key ][ 4 ]} - {Numbers[ date_key ][ 5 ]} - [{Numbers[ date_key ][ 6 ]}]");
						if ( DateTime.Parse(date_key) >= dateTimePicker1_from.Value && DateTime.Parse(date_key) <= dateTimePicker2_to.Value )
						{
							count++;
						}
						for ( int i = 1; i <= 49; i++ )
						{
							if ( DontIncludeInCycle.Contains(i) )
							{
								continue;
							}
							one_to_49.Add(i.ToString());
						}
					}
					if ( one_to_49.Contains(number) )
					{
						one_to_49.Remove(number);
						switch ( one_to_49.Count )
						{
							case 7:
								if ( DateTime.Parse(date_key) >= dateTimePicker1_from.Value && DateTime.Parse(date_key) <= dateTimePicker2_to.Value )
								{
									listView1_Cycle.Items.Add($"Last 7 Numbers: {one_to_49[ 0 ]} - {one_to_49[ 1 ]} - {one_to_49[ 2 ]} - {one_to_49[ 3 ]} - {one_to_49[ 4 ]} - {one_to_49[ 5 ]} - {one_to_49[ 6 ]}");
								}
								break;

							case 6:
								if ( DateTime.Parse(date_key) >= dateTimePicker1_from.Value && DateTime.Parse(date_key) <= dateTimePicker2_to.Value )
								{
									listView1_Cycle.Items.Add($"{number} Drawn with {Numbers[ date_key ][ 0 ]} - {Numbers[ date_key ][ 1 ]} - {Numbers[ date_key ][ 2 ]} - {Numbers[ date_key ][ 3 ]} - {Numbers[ date_key ][ 4 ]} - {Numbers[ date_key ][ 5 ]} - [{Numbers[ date_key ][ 6 ]}] on {_date} {State1}");
								}
								break;

							case 5:
								if ( DateTime.Parse(date_key) >= dateTimePicker1_from.Value && DateTime.Parse(date_key) <= dateTimePicker2_to.Value )
								{
									listView1_Cycle.Items.Add($"{number} Drawn with {Numbers[ date_key ][ 0 ]} - {Numbers[ date_key ][ 1 ]} - {Numbers[ date_key ][ 2 ]} - {Numbers[ date_key ][ 3 ]} - {Numbers[ date_key ][ 4 ]} - {Numbers[ date_key ][ 5 ]} - [{Numbers[ date_key ][ 6 ]}] on {_date} {State1}");
								}
								break;

							case 4:
								if ( DateTime.Parse(date_key) >= dateTimePicker1_from.Value && DateTime.Parse(date_key) <= dateTimePicker2_to.Value )
								{
									listView1_Cycle.Items.Add($"{number} Drawn with {Numbers[ date_key ][ 0 ]} - {Numbers[ date_key ][ 1 ]} - {Numbers[ date_key ][ 2 ]} - {Numbers[ date_key ][ 3 ]} - {Numbers[ date_key ][ 4 ]} - {Numbers[ date_key ][ 5 ]} - [{Numbers[ date_key ][ 6 ]}] on {_date} {State1}");
								}
								break;

							case 3:
								if ( DateTime.Parse(date_key) >= dateTimePicker1_from.Value && DateTime.Parse(date_key) <= dateTimePicker2_to.Value )
								{
									listView1_Cycle.Items.Add($"{number} Drawn with {Numbers[ date_key ][ 0 ]} - {Numbers[ date_key ][ 1 ]} - {Numbers[ date_key ][ 2 ]} - {Numbers[ date_key ][ 3 ]} - {Numbers[ date_key ][ 4 ]} - {Numbers[ date_key ][ 5 ]} - [{Numbers[ date_key ][ 6 ]}] on {_date} {State1}");
								}
								break;

							case 2:
								if ( DateTime.Parse(date_key) >= dateTimePicker1_from.Value && DateTime.Parse(date_key) <= dateTimePicker2_to.Value )
								{
									listView1_Cycle.Items.Add($"{number} Drawn with {Numbers[ date_key ][ 0 ]} - {Numbers[ date_key ][ 1 ]} - {Numbers[ date_key ][ 2 ]} - {Numbers[ date_key ][ 3 ]} - {Numbers[ date_key ][ 4 ]} - {Numbers[ date_key ][ 5 ]} - [{Numbers[ date_key ][ 6 ]}] on {_date} {State1}");
									listView1_Cycle.Items.Add($"Last 2 Numbers: {one_to_49[ 0 ]} - {one_to_49[ 1 ]}");
								}
								break;

							case 1:
								if ( DateTime.Parse(date_key) >= dateTimePicker1_from.Value && DateTime.Parse(date_key) <= dateTimePicker2_to.Value )
								{
									listView1_Cycle.Items.Add($"{number} Drawn with {Numbers[ date_key ][ 0 ]} - {Numbers[ date_key ][ 1 ]} - {Numbers[ date_key ][ 2 ]} - {Numbers[ date_key ][ 3 ]} - {Numbers[ date_key ][ 4 ]} - {Numbers[ date_key ][ 5 ]} - [{Numbers[ date_key ][ 6 ]}] on {_date} {State1}");
								}
								break;

							case 0:
								LastNumFound = int.Parse(number);
								if ( DateTime.Parse(date_key) >= dateTimePicker1_from.Value && DateTime.Parse(date_key) <= dateTimePicker2_to.Value )
								{
									listView1_Cycle.Items.Add($"{number} Drawn with {Numbers[ date_key ][ 0 ]} - {Numbers[ date_key ][ 1 ]} - {Numbers[ date_key ][ 2 ]} - {Numbers[ date_key ][ 3 ]} - {Numbers[ date_key ][ 4 ]} - {Numbers[ date_key ][ 5 ]} - [{Numbers[ date_key ][ 6 ]}] on {_date} {State1}");
									listView1_Cycle.Items.Add("");
								}
								break;
						}
					}

				}
			}
		}

		private DateTime GetLastCycleDate()
		{
			List<int> num1_49 = new List<int>();//all numbers
			Dictionary<string, List<string>> Numbers = new Dictionary<string, List<string>>(date_AND_combination);
			DateTime LastCycleDate = dateTimePicker1_from.Value;
			foreach ( string key in Numbers.Keys )
			{
				List<int> listOfNums = Array.ConvertAll(Numbers[ key ].ToArray(), a => int.Parse(a)).ToList();
				foreach ( int num in listOfNums )
				{
					try 
					{ 
						num1_49.Remove(num);
						if ( num1_49.Count == 0 ) 
						{
							LastCycleDate = DateTime.Parse(key);
							for ( int i = 1; i < 50; i++ )
							{
								num1_49.Add(i);
							}
						}
					}
					catch { }
				}
			}
			return LastCycleDate;
		}

		private int Prediction()
		{
			int count = 0;
			int max = 50;
			for ( int i = 1; i < max-3; i++ )
			{
				for ( int j = i + 1; j < max- 2; j++ )
				{
					for ( int l = j + 1; l < max - 1; l++ )
					{
						for ( int m = l + 1; m < max; m++ )
						{
							int[] nums = { i, j, l, m };
							string[] cou = date_AND_combination.Values.ElementAt(date_AND_combination.Count() - 1).ToArray();
							for ( int k = 0; k < nums.Length; k++ )
							{
								if(cou.Contains($"{nums[ k ]}" ))
								{
									if( isApproved(nums))
									{
										count++;
										listView1_ActualPred.Items.Add($"{i}-{j}-{l}-{m}");
										break;
									}
								}
							}
							//count += isLinear(new int[] { i, j, l, m }) ? 0 : 1;
						}
					}
				}
			}
			return count;
		}

		private bool isApproved(int[] numbers )
		{

			HashSet<int> differences = new HashSet<int>();
			HashSet<int> quad_differences = new HashSet<int>();
			
			for ( int i = numbers.Length - 1; i > 0 ; i-- )
			{
				//checking differences in case on linear sequance
				differences.Add(numbers[ i ] - numbers[ i - 1 ]);
				quad_differences.Add(numbers[ i ] / numbers[ i - 1 ]);
				
			}

			if ( differences.Count != numbers.Length -1 || quad_differences.Count != numbers.Length - 1 )
			{
				return false;
			}

			return true;
		}
	}
}
