using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Brain
{
	public partial class Search : Form
	{
		private Dictionary<string, List<string>> date_AND_combination ;
		bool imported = false;
		public Search()
		{
			InitializeComponent();
		}
		private void richTextBox1_TypeNums_KeyPress( object sender, KeyPressEventArgs e )
		{
			try
			{
				if ( e.KeyChar == ( char ) Keys.Enter )
				{
					SearchNumbers();
				}
			}
			catch ( Exception ex )
			{
				toolStripStatusLabel1_info_label.Text = ex.Message;
			}
		}
		

		private void button1_Search_Click( object sender, EventArgs e )
		{
			try
			{
				SearchNumbers();
			}
			catch(Exception ex) 
			{
				toolStripStatusLabel1_info_label.Text = ex.Message;
			}
		}

		private void SearchNumbers()
		{
			try
			{
				if ( !string.IsNullOrEmpty(richTextBox1_TypeNums.Text) && imported )
				{
					if ( date_AND_combination.Count > 1 )
					{
						if ( richTextBox1_TypeNums.Text.Length <= 2 || (richTextBox1_TypeNums.Text.Length > 2 && richTextBox1_TypeNums.Text.Contains("-")))
						{
							List<string> nums_to_search = richTextBox1_TypeNums.Text.Split('-').ToList();
							Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>(date_AND_combination
								.Where(a => DateTime.Parse(a.Key) >= dateTimePicker1_from.Value)
								.Where(b => DateTime.Parse(b.Key) <= dateTimePicker2_to.Value)
								.ToDictionary(c => c.Key, d => d.Value)
								);

							listView1_Numbers.Items.Clear();

							chart1.Series[0].Points.Clear();
							chart1.Series[1].Points.Clear();
							chart1.Series[2].Points.Clear();

							Dictionary<int, int> num_playcount = new Dictionary<int, int>();
							Dictionary<int, int> num_playcount_bonus = new Dictionary<int, int>();

							for ( int i = 1; i < 50; i++ )//populate dicts with default 0 values
							{
								num_playcount.Add(i, 0);//main info
								num_playcount_bonus.Add(i, 0);//bonus info
							}
							foreach ( string key in dict.Keys )
							{
								bool ContainsAll = true;
								foreach ( string num in nums_to_search )
								{
									if ( !dict[ key ].Contains(num) )
									{
										ContainsAll = false;
										break;
									}
								}
								if ( ContainsAll )//add if contains all typed nums
								{
									num_playcount_bonus[ int.Parse(dict[ key ][6]) ]++;
									foreach ( string it in dict[key] )//calculate number frequency
									{
										num_playcount[int.Parse(it) ] += 1;
									}
									DateTime dt = DateTime.Parse(key);
									List<int> nums = Array.ConvertAll(dict[ key ].ToArray(), a => int.Parse(a)).ToList();
									int bonus = nums[ nums.Count - 1 ];
									nums.RemoveAt(nums.Count - 1);
									nums.Sort();
									string state = key.Length < 12 ? "L" : "T";
									ListViewItem item = listView1_Numbers.Items.Add(dt.ToShortDateString());
									item.SubItems.Add($"{nums[ 0 ]} - {nums[ 1 ]} - {nums[ 2 ]} - {nums[ 3 ]} - {nums[ 4 ]} - {nums[ 5 ]} - [{bonus}]");
									item.SubItems.Add(state);

								}
							}

							//draw main numbers for found numbers
							chart1.ChartAreas[ 0 ].AxisY.Maximum = num_playcount.Values.Max() + 10 - ( num_playcount.Values.Max() % 10);
							foreach ( int num in num_playcount.Keys )
							{
								//draw chart info for main numbers
								chart1.Series[ 0 ].Points.AddXY(num, num_playcount[ num ] - num_playcount_bonus[ num ]);
								chart1.Series[2].Points.AddXY(num, num_playcount[num]);

							}
							foreach ( int num in num_playcount_bonus.Keys )
							{
								//draw chart info for bonus
								chart1.Series[ 1 ].Points.AddXY(num, num_playcount_bonus[ num ]);
							}

							toolStripStatusLabel1_info_label.Text = $"{listView1_Numbers.Items.Count} Result(s) found.";
						}
						else//if nums dont contain -
						{
							toolStripStatusLabel1_info_label.Text = "Please separate the numbers by dashes (-).";//tell user
						}
					}
				}else
				{
					if ( imported )//if search box is empty
					{
						toolStripStatusLabel1_info_label.Text = "Search box can't be empty.";//tell user
					}
					else//if search box isn't empty but no results were imported
					{
						toolStripStatusLabel1_info_label.Text = "You need to import some results.";

					}

				}
			}
			catch(Exception e)
			{

				toolStripStatusLabel1_info_label.Text = e.Message;
				
			}
		}


		private void Search_Shown( object sender, EventArgs e )
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
					//DrawChart();
				}
			}
			catch ( Exception ex )
			{
				toolStripStatusLabel1_info_label.Text = ex.Message;
			}
		}

	}
}
