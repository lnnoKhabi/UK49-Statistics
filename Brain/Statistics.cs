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
		List<ListViewItem> allitems = new List<ListViewItem>(49);


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

		private void clear_()
		{
			chart2_all_freq.Series[ 0 ].Points.Clear();
			chart2_all_freq.Series[ 1 ].Points.Clear();
			chart2_all_freq.Series[ 2 ].Points.Clear();

			listView1_Cycle.Items.Clear();

			listView1_Table.Items.Clear();

			listView1_PredChosen.Items.Clear();
			listView1_ActualPred.Items.Clear();

			listView_PlusMinus_Pred.Items.Clear();

		}

		private void DrawFrequencyGraph()
		{
			Dictionary<string, List<string>> Numbers = new Dictionary<string, List<string>>(date_AND_combination
						.Where(a => DateTime.Parse(a.Key) >= dateTimePicker1_from.Value)
						.Where(b => DateTime.Parse(b.Key) <= dateTimePicker2_to.Value)
						.ToDictionary(c => c.Key, d => d.Value)
						);

			

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

				//draw overall frequency
				chart2_all_freq.Series[2].Points.AddXY(num, num_playcount_all[num]);
			}

			//draw bonus numbers for all numbers
			foreach ( int num in num_playcount_all_bonus.Keys )
			{
				//draw chart info for bonus
				chart2_all_freq.Series[ 1 ].Points.AddXY(num, num_playcount_all_bonus[ num ]);
			}
		}

		private void ActualPred()
		{
			Dictionary<string, List<string>> NumsPlusMinus = new Dictionary<string, List<string>>(date_AND_combination
						.Where(a => DateTime.Parse(a.Key) >= dateTimePicker1_from.Value)
						.Where(b => DateTime.Parse(b.Key) <= dateTimePicker2_to.Value)
						.ToDictionary(c => c.Key, d => d.Value)
						);
			//zero_count = index 0 in dict  values array
			//plus1_count = index 1 in dict values array
			//minus1_count = index 2 in dict values array
			//plus10_count = index 3 in dict values array
			//minus10_count = index 4 in dict values array
			Dictionary<int, int[]> res = new Dictionary<int, int[]>() {
				{ 1, new int[5] },
				{ 2, new int[5] },
				{ 3, new int[5] },
				{ 4, new int[5] },
				{ 5, new int[5] },
				{ 6, new int[5] },
				{ 7, new int[5] }
			};

			//show stats for plus/minus in past results
			for (int i = 0; i < NumsPlusMinus.Count() - 1; i++)
			{
				int count = 1;
				//loop a result
				foreach (string num in NumsPlusMinus.ElementAt(i).Value)
				{
					int ball = int.Parse(num);
					//+-0
					if (NumsPlusMinus.ElementAt(i + 1).Value.Contains(num))//compare current ball with next day result
					{
						//ball was repeated
						res[count][0]++;
					}
					//+1
					if (NumsPlusMinus.ElementAt(i + 1).Value.Contains((ball + 1).ToString()))//compare current ball with next day result
					{
						//ball plus 1 was played
						res[count][1]++;
					}
					//+10
					if (NumsPlusMinus.ElementAt(i + 1).Value.Contains((ball + 10).ToString()))//compare current ball with next day result
					{
						//ball plus 10 was played
						res[count][2]++;
					}
					//-1
					if (NumsPlusMinus.ElementAt(i + 1).Value.Contains((ball - 1).ToString()))//compare current ball with next day result
					{
						//ball minus 1 was played
						res[count][3]++;
					}
					//-10
					if (NumsPlusMinus.ElementAt(i + 1).Value.Contains((ball - 10).ToString()))//compare current ball with next day result
					{
						//ball minus 1 was played
						res[count][4]++;
					}
					count++;
				}

			}

			//print to list
			listView_PlusMinus_Pred.Items.Add("Repeated");
			listView_PlusMinus_Pred.Items.Add("+ One");
			listView_PlusMinus_Pred.Items.Add("+Ten");
			listView_PlusMinus_Pred.Items.Add("- One");
			listView_PlusMinus_Pred.Items.Add("-Ten");

			foreach (int[] n in res.Values)
			{
				//int max = n.Max();
				int count = 0;
				foreach (ListViewItem item in listView_PlusMinus_Pred.Items)
				{
					item.SubItems.Add(n[count].ToString());
					count++;
				}
			}
			//highlight highest values
		


			//populate all actual plus/minus predictions
			Dictionary<string, List<string>> Numbers = new Dictionary<string, List<string>>(date_AND_combination);
			HashSet<int> duplicate = new HashSet<int>();

			int date = Numbers.Count - (DateTime.Parse(Numbers.Keys.ElementAt(Numbers.Count-1)).Subtract(dateTimePicker2_to.Value.Date).Days + 1);
	
			foreach (string num in Numbers.Values.ElementAt(date))
			{
				int n = int.Parse(num);
				HashSet<int> possible5 = new HashSet<int>(5);
				if (duplicate.Add(n))
					possible5.Add(n);
				if (duplicate.Add(n-1))
					possible5.Add(n-1);
				if (duplicate.Add(n+1))
					possible5.Add(n+1);
				if (duplicate.Add(n-10))
					possible5.Add(n-10);
				if (duplicate.Add(n+10))
					possible5.Add(n+10);

				possible5 = possible5.Where(a => a > 0).Where(b => b < 50).ToHashSet();//numbers are  1-49


				listView1_ActualPred.Items.Add(string.Join(", " ,possible5.ToArray()));
			}

			listView1_ActualPred.Columns[0].Text = "Predictions " + $"({duplicate.Where(a => a > 0).Where(b => b < 50).Count()})";
		}

		private void Generate_Info()
		{
			//try
			//{
				if (imported)
				{
					clear_();
					DrawFrequencyGraph();
					Cycle();
					NumbersStats();
					//listView1_ActualPred.Items.Add($"{Prediction()}");
					LottoNumbersDotComPrediction();
					ActualPred();
					toolStripStatusLabel1_info_label.Text = "Complete.";

				}
				else
				{
					toolStripStatusLabel1_info_label.Text = "You need to import some results.";
				}
			//}
			//catch ( Exception e )
			//{

			//	toolStripStatusLabel1_info_label.Text = e.Message;

			//}
		}

		private void NumbersStats()
		{
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
				string average_pick = LastPicked[ number ] == 0? "Never drawn." : $"{Math.Round((double)Numbers.Count / (double)PlayTime[ number ], 1)} days.";
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

		private void LottoNumbersDotComPrediction()
		{
			Dictionary<string, List<string>> Numbers = new Dictionary<string, List<string>>(date_AND_combination
						.Where(a => DateTime.Parse(a.Key) >= dateTimePicker1_from.Value)
						.Where(b => DateTime.Parse(b.Key) <= dateTimePicker2_to.Value)
						.ToDictionary(c => c.Key, d => d.Value)
						);

			List<string> RecentNumbers = Numbers.Values.ElementAt(Numbers.Count - 1);
			ListView.ColumnHeaderCollection lchc = listView1_PredChosen.Columns;
			for ( int i = 1; i < 8; i++ )
			{
				lchc[ i ].Text = RecentNumbers[ i - 1 ];
			}

			Dictionary<string, Dictionary<string, int>> NextDayFrequency = new Dictionary<string, Dictionary<string, int>>(7) {
				{ RecentNumbers[0], new Dictionary<string, int>(49){ {"1", 0 }, {"2", 0 }, {"3", 0 }, {"4", 0 }, {"5", 0 }, {"6", 0 }, {"7", 0 }, {"8", 0 }, {"9", 0 }, { "10", 0 }, { "11", 0 }, { "12", 0 }, { "13", 0 }, { "14", 0 }, { "15", 0 }, { "16", 0 }, { "17", 0 }, { "18", 0 }, { "19", 0 }, { "20", 0 }, { "21", 0 }, { "22", 0 }, { "23", 0 }, { "24", 0 }, { "25", 0 }, { "26", 0 }, { "27", 0 }, { "28", 0 }, { "29", 0 }, { "30", 0 }, { "31", 0 }, { "32", 0 }, { "33", 0 }, { "34", 0 }, { "35", 0 }, { "36", 0 }, { "37", 0 }, { "38", 0 }, { "39", 0 }, { "40", 0 }, { "41", 0 }, { "42", 0 }, { "43", 0 }, { "44", 0 }, { "45", 0 }, { "46", 0 }, { "47", 0 }, { "48", 0 }, { "49", 0 } } },
				{ RecentNumbers[1], new Dictionary<string, int>(49){ {"1", 0 }, {"2", 0 }, {"3", 0 }, {"4", 0 }, {"5", 0 }, {"6", 0 }, {"7", 0 }, {"8", 0 }, {"9", 0 }, { "10", 0 }, { "11", 0 }, { "12", 0 }, { "13", 0 }, { "14", 0 }, { "15", 0 }, { "16", 0 }, { "17", 0 }, { "18", 0 }, { "19", 0 }, { "20", 0 }, { "21", 0 }, { "22", 0 }, { "23", 0 }, { "24", 0 }, { "25", 0 }, { "26", 0 }, { "27", 0 }, { "28", 0 }, { "29", 0 }, { "30", 0 }, { "31", 0 }, { "32", 0 }, { "33", 0 }, { "34", 0 }, { "35", 0 }, { "36", 0 }, { "37", 0 }, { "38", 0 }, { "39", 0 }, { "40", 0 }, { "41", 0 }, { "42", 0 }, { "43", 0 }, { "44", 0 }, { "45", 0 }, { "46", 0 }, { "47", 0 }, { "48", 0 }, { "49", 0 } }},
				{RecentNumbers[2],new Dictionary<string,int>(49){{"1",0},{"2",0},{"3",0},{"4",0},{"5",0},{"6",0},{"7",0},{"8",0},{"9",0},{"10",0},{"11",0},{"12",0},{"13",0},{"14",0},{"15",0},{"16",0},{"17",0},{"18",0},{"19",0},{"20",0},{"21",0},{"22",0},{"23",0},{"24",0},{"25",0},{"26",0},{"27",0},{"28",0},{"29",0},{"30",0},{"31",0},{"32",0},{"33",0},{"34",0},{"35",0},{"36",0},{"37",0},{"38",0},{"39",0},{"40",0},{"41",0},{"42",0},{"43",0},{"44",0},{"45",0},{"46",0},{"47",0},{"48",0},{"49",0}}},
				{RecentNumbers[3],new Dictionary<string,int>(49){{"1",0},{"2",0},{"3",0},{"4",0},{"5",0},{"6",0},{"7",0},{"8",0},{"9",0},{"10",0},{"11",0},{"12",0},{"13",0},{"14",0},{"15",0},{"16",0},{"17",0},{"18",0},{"19",0},{"20",0},{"21",0},{"22",0},{"23",0},{"24",0},{"25",0},{"26",0},{"27",0},{"28",0},{"29",0},{"30",0},{"31",0},{"32",0},{"33",0},{"34",0},{"35",0},{"36",0},{"37",0},{"38",0},{"39",0},{"40",0},{"41",0},{"42",0},{"43",0},{"44",0},{"45",0},{"46",0},{"47",0},{"48",0},{"49",0}}},
				{RecentNumbers[4],new Dictionary<string,int>(49){{"1",0},{"2",0},{"3",0},{"4",0},{"5",0},{"6",0},{"7",0},{"8",0},{"9",0},{"10",0},{"11",0},{"12",0},{"13",0},{"14",0},{"15",0},{"16",0},{"17",0},{"18",0},{"19",0},{"20",0},{"21",0},{"22",0},{"23",0},{"24",0},{"25",0},{"26",0},{"27",0},{"28",0},{"29",0},{"30",0},{"31",0},{"32",0},{"33",0},{"34",0},{"35",0},{"36",0},{"37",0},{"38",0},{"39",0},{"40",0},{"41",0},{"42",0},{"43",0},{"44",0},{"45",0},{"46",0},{"47",0},{"48",0},{"49",0}}},
				{RecentNumbers[5],new Dictionary<string,int>(49){{"1",0},{"2",0},{"3",0},{"4",0},{"5",0},{"6",0},{"7",0},{"8",0},{"9",0},{"10",0},{"11",0},{"12",0},{"13",0},{"14",0},{"15",0},{"16",0},{"17",0},{"18",0},{"19",0},{"20",0},{"21",0},{"22",0},{"23",0},{"24",0},{"25",0},{"26",0},{"27",0},{"28",0},{"29",0},{"30",0},{"31",0},{"32",0},{"33",0},{"34",0},{"35",0},{"36",0},{"37",0},{"38",0},{"39",0},{"40",0},{"41",0},{"42",0},{"43",0},{"44",0},{"45",0},{"46",0},{"47",0},{"48",0},{"49",0}}},
				{RecentNumbers[6],new Dictionary<string,int>(49){{"1",0},{"2",0},{"3",0},{"4",0},{"5",0},{"6",0},{"7",0},{"8",0},{"9",0},{"10",0},{"11",0},{"12",0},{"13",0},{"14",0},{"15",0},{"16",0},{"17",0},{"18",0},{"19",0},{"20",0},{"21",0},{"22",0},{"23",0},{"24",0},{"25",0},{"26",0},{"27",0},{"28",0},{"29",0},{"30",0},{"31",0},{"32",0},{"33",0},{"34",0},{"35",0},{"36",0},{"37",0},{"38",0},{"39",0},{"40",0},{"41",0},{"42",0},{"43",0},{"44",0},{"45",0},{"46",0},{"47",0},{"48",0},{"49",0}}}
			};




			for ( int i = 0; i <= Numbers.Count - 2; i++ )
			{

				List<string> res = Numbers.Values.ElementAt(i);
				foreach ( string num in res )
				{
					if ( RecentNumbers.Contains(num) )
					{
						List<string> next_res = Numbers.Values.ElementAt(i + 1);
						foreach ( string nxt_num in next_res )
						{
							NextDayFrequency[ num ][ nxt_num ]++;
						}
					}
				}
			}

			int best_sum = 0;
			int index = -1;
			int indx = -1;
			int overall = 0;

			allitems = new List<ListViewItem>(49);

			for ( int i = 1; i < 50; i++ )
			{
				int sum = 0;
				ListViewItem lvi = listView1_PredChosen.Items.Add(i.ToString());
				
				lvi.UseItemStyleForSubItems = false;
				List<int> l = new List<int>();
				for ( int j = 0; j < 7; j++ )
				{
					string key = NextDayFrequency.Keys.ElementAt(j);
					Dictionary<string, int> value = NextDayFrequency[ key ];

					int max = NextDayFrequency.ElementAt(j).Value.Max(a => a.Value);
					int min = NextDayFrequency.ElementAt(j).Value.Min(a => a.Value);

					lvi.SubItems.Add(value[ i.ToString() ].ToString());
					lvi.SubItems[ j + 1 ].BackColor = max == value[ i.ToString() ] ? Color.Aqua : lvi.SubItems[ j + 1 ].BackColor;
					lvi.SubItems[ j + 1 ].BackColor = min == value[ i.ToString() ] ? Color.PaleVioletRed : lvi.SubItems[ j + 1 ].BackColor;
					sum += value[ i.ToString() ];
					l.Add(value[ i.ToString() ]);
				}
				//lvi.SubItems[0].BackColor = Color.Aquamarine;
				index = sum > best_sum ? i - 1:index;
				best_sum = sum > best_sum ? sum : best_sum;
				lvi.SubItems.Add(sum.ToString());//total
				lvi.ToolTipText = sum.ToString();

				int ov = Numbers.Values.Where(a => a.Contains(i.ToString())).Count();
				indx = ov > overall ? i-1 : indx;
				overall = ov > overall ? ov : overall;
				lvi.SubItems.Add(ov.ToString());//overall frequency
				double avg = ( double ) ( sum / 7 );
				lvi.SubItems.Add(Math.Floor(avg ).ToString());//average
				lvi.SubItems.Add(l.Where(a => a < avg).Count().ToString());//less than average

				allitems.Add(lvi);
			}
			listView1_PredChosen.Items[ index ].SubItems[ 8 ].BackColor = Color.LimeGreen;
			listView1_PredChosen.Items[ indx ].SubItems[ 9 ].BackColor = Color.Lime;

			
		}

		private void listView1_PredChosen_ItemSelectionChanged( object sender, ListViewItemSelectionChangedEventArgs e )
		{
			toolStripStatusLabel1_info_label.Text = $"{listView1_PredChosen.SelectedItems.Count} item(s) selected.";

		}


		private void listView1_PredChosen_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if(e.Column == 8)//total column
			{
				listView1_PredChosen.Items.Clear();
				//sort all items
				allitems = allitems.OrderByDescending(a => int.Parse(a.ToolTipText)).ToList();
				//add
				listView1_PredChosen.Items.AddRange(allitems.ToArray());
				toolStripStatusLabel1_info_label.Text = "Sorted by total";
			}
			if(e.Column == 0)
			{
				listView1_PredChosen.Items.Clear();
				//sort all items
				allitems = allitems.OrderBy(a => int.Parse(a.Text)).ToList();
				//add
				listView1_PredChosen.Items.AddRange(allitems.ToArray());
				toolStripStatusLabel1_info_label.Text = "Sorted by number";

			}
		}
	}
}
