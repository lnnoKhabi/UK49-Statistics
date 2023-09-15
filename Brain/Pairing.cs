using System;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;

namespace Brain
{
	public partial class Pairing : Form
	{
		public Pairing()
		{
			InitializeComponent();
			toolStripStatusLabel1_text.Text = "Enter numbers separated by commas.";

		}
		
		private void button1_Pair_Click( object sender, EventArgs e )
		{
			Pair();
		}

		private void Pair()
		{
			//check if nums were entered
			try
			{
				listView1_Pairings.Items.Clear();
				int[] nums = String.IsNullOrEmpty(richTextBox1_TypeNums.Text) ? new int[ 0 ] : Array.ConvertAll(richTextBox1_TypeNums.Text.Split(','), a => int.Parse(a));
				if ( nums.Length == 0 )
				{
					toolStripStatusLabel1_text.Text = "Please enter numbers separated by commas.";
				}
				else if ( nums.Length < 3 )
				{
					toolStripStatusLabel1_text.Text = "Please enter atleast 3 numbers separated by commas.";
				}

				else
				{
					//check if pair in is greater than antered numbers
					if ( comboBox1_PairIn.SelectedIndex != -1 )
					{
						if ( comboBox1_PairIn.SelectedIndex + 2 < nums.Length )
						{
							nums = nums.OrderBy(a => a).ToArray();
							BeginPair(nums, comboBox1_PairIn.SelectedIndex + 2);
						}
						else
						{
							toolStripStatusLabel1_text.Text = "Please enter more numbers.";
						}
					}
					else
					{
						toolStripStatusLabel1_text.Text = "Please choose how you want to pair numbers.";
					}
				}
			}
			catch(Exception e)
			{
				toolStripStatusLabel1_text.Text = e.Message;

			}
		}

		private void BeginPair(int[] nums, int pair_in)
		{
			if ( pair_in == 5 )
			{
				int count = 1;
				for ( int a = 0; a < nums.Length - 4; a++ )
				{
					for ( int b = a + 1; b < nums.Length - 3; b++ )
					{
						for ( int c = b + 1; c < nums.Length - 2; c++ )
						{
							for ( int d = c + 1; d < nums.Length - 1; d++ )
							{
								for ( int e = d + 1; e < nums.Length; e++ )
								{
									listView1_Pairings.Items.Add(count.ToString()).SubItems.Add($"{nums[ a ]} - {nums[ b ]} - {nums[ c ]} - {nums[ d ]} - {nums[ e ] }");
									count++;
								}
							}
						}
					}
				}
			}

			if ( pair_in == 4 )
			{
				int count = 1;
				for ( int b = 0; b < nums.Length - 3; b++ )
				{
					for ( int c = b + 1; c < nums.Length - 2; c++ )
					{
						for ( int d = c + 1; d < nums.Length - 1; d++ )
						{
							for ( int e = d + 1; e < nums.Length; e++ )
							{
								listView1_Pairings.Items.Add(count.ToString()).SubItems.Add($"{nums[ b ]} - {nums[ c ]} - {nums[ d ]} - {nums[ e ] }");
								count++;
							}
						}
					}
				}
			}

			if ( pair_in == 3 )
			{
				int count = 1;

				for ( int c = 0; c < nums.Length - 2; c++ )
				{
					for ( int d = c + 1; d < nums.Length - 1; d++ )
					{
						for ( int e = d + 1; e < nums.Length; e++ )
						{
							listView1_Pairings.Items.Add(count.ToString()).SubItems.Add($"{nums[ c ]} - {nums[ d ]} - {nums[ e ] }");
							count++;
						}
					}
				}
			}

			if ( pair_in == 2 )
			{
				int count = 1;

				for ( int d = 0; d < nums.Length - 1; d++ )
				{
					for ( int e = d + 1; e < nums.Length; e++ )
					{
						listView1_Pairings.Items.Add(count.ToString()).SubItems.Add($"{nums[ d ]} - {nums[ e ] }");
						count++;
					}
				}
			}
			toolStripStatusLabel1_text.Text = "Done.";
		}

		private void button1_clear_Click(object sender, EventArgs e)
		{
			if(!string.IsNullOrEmpty(textBox1_n_objects.Text))
			{
				textBox1_n_objects.Text = string.Empty;
			}
			if (!string.IsNullOrEmpty(textBox1_r_sample.Text))
			{
				textBox1_r_sample.Text = string.Empty;
			}
		}

		private void button1_calculate_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(textBox1_n_objects.Text))
			{
				if (!string.IsNullOrEmpty(textBox1_r_sample.Text))
				{
					//
					int n = int.Parse(textBox1_n_objects.Text);
					int r = int.Parse(textBox1_r_sample.Text);
					BigInteger ans = fact(n) / (fact(r)  * fact(n-r));
					label4_Answer.Text = ans.ToString();
				}
			}
			
		}

		public static BigInteger fact(BigInteger n)
		{
			if(n == 1)
			{
				return 1;
			}
			return n * fact(n - 1);
		} 
	}
}
