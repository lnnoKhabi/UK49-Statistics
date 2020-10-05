using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Globalization;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Brain
{
	public partial class ReadAloud : Form
	{
		private Dictionary<string, List<string>> date_AND_combination = new Dictionary<string, List<string>>();
		SpeechSynthesizer ss = new SpeechSynthesizer();

		public ReadAloud()
		{
			InitializeComponent();
			ss.SelectVoiceByHints(VoiceGender.Female);

		}

		private void button2_Import_Click( object sender, EventArgs e )
		{
			//Task t = new Task(GetNumbers);
			//t.Start();
			//await t;
			GetNumbers();
		}
		private void GetNumbers()
		{
			
			date_AND_combination = new Dictionary<string, List<string>>(Form1.date_AND_combination
			.Where(a => DateTime.Parse(a.Key) >= dateTimePicker1_Start.Value)
			.Where(b => DateTime.Parse(b.Key) <= dateTimePicker1_End.Value)
			.ToDictionary(c => c.Key, d => d.Value));
			
			
			if ( listView1_Numbers.Items.Count > 0 )
			{
				listView1_Numbers.Items.Clear();
			}
			
			if ( date_AND_combination.Count > 0 )
			{
				foreach ( string key in date_AND_combination.Keys )
				{
					List<int> nums = Array.ConvertAll(date_AND_combination[ key ].ToArray(), a => int.Parse(a)).ToList();
					int bonus = nums[ nums.Count - 1 ];
					nums.RemoveAt(nums.Count - 1);
					nums.Sort();
					DateTime dt = DateTime.Parse(key);
					//Invoke(new Action(() => { 
						ListViewItem item = listView1_Numbers.Items.Add(dt.ToShortDateString());
						item.SubItems.Add($"{nums[ 0 ]} - {nums[ 1 ]} - {nums[ 2 ]} - {nums[ 3 ]} - {nums[ 4 ]} - {nums[ 5 ]} - [{bonus}]");
						item.Tag = $"{nums[ 0 ]} {nums[ 1 ]} {nums[ 2 ]} {nums[ 3 ]} {nums[ 4 ]} {nums[ 5 ]} {bonus}";
					//}));
				}
			}
			
		}

		private void button1_Read_Click( object sender, EventArgs e )
		{
			if ( date_AND_combination.Count > 0 )
			{
				int count = 0;
				button1_Read.Enabled = false;
				button2_Import.Enabled = false;
				button1.Enabled = true;
				trackBar1_SpeechSpeed.Enabled = true;

				Speak(count);
				ss.SpeakCompleted += ( a, b ) =>
				{
					//listView1_Numbers.Items[ count ].Checked = false;
					count++;
					Speak(count);
				};
			}
		}
		private void Speak(int index)
		{
			if ( index <= listView1_Numbers.Items.Count - 1 )
			{
				listView1_Numbers.Focus();
				try {
					ListViewItem item = listView1_Numbers.Items[ index ];
					//item.Checked = true;
					//item.Focused = true;
					item.Selected = true;
					item.EnsureVisible();
					ss.SpeakAsync(item.Tag.ToString());
					
				}
				catch
				{
					ss = new SpeechSynthesizer();
					ss.SelectVoiceByHints(VoiceGender.Female);
					Console.WriteLine(ss.Rate);

					ListViewItem item = listView1_Numbers.Items[ index ];
					//item.Checked = true;
					//item.Focused = true;

					item.Selected = true;
					item.EnsureVisible();
					ss.SpeakAsync(item.Tag.ToString());
				}
			}
			else
			{
				ss.Dispose();
				button1_Read.Enabled = true;
				button2_Import.Enabled = true;
				button1.Enabled = false;
				trackBar1_SpeechSpeed.Enabled = false;

				//MessageBox.Show("Done reading all the numbers!", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
		private void Pause()
		{
			try
			{
				if ( ss.State == SynthesizerState.Speaking )
				{
					ss.Pause();
					button1.Text = "PLAY";
				}
				else if ( ss.State == SynthesizerState.Paused )
				{
					ss.Resume();
					button1.Text = "PAUSE";
				}
			}
			catch
			{

			}
		}

		private void button1_Click( object sender, EventArgs e )
		{
			Pause();
		}

		private void ReadAloud_FormClosing( object sender, FormClosingEventArgs e )
		{
			ss.Volume = 0;
			ss.Dispose();
		}

		private void trackBar1_SpeechSpeed_Scroll(object sender, EventArgs e)
		{
			try
			{
				ss.Rate = trackBar1_SpeechSpeed.Value;
			}
			catch{
				ss = new SpeechSynthesizer();
				ss.SelectVoiceByHints(VoiceGender.Female);
				ss.Rate = trackBar1_SpeechSpeed.Value;
			}
		}

		private void ReadAloud_Shown( object sender, EventArgs e )
		{
			if ( Form1.date_AND_combination.Count > 1 )
			{
				dateTimePicker1_Start.Value = DateTime.Parse(Form1.date_AND_combination.Keys.ElementAt(0));
				dateTimePicker1_End.Value = DateTime.Parse(Form1.date_AND_combination.Keys.ElementAt(Form1.date_AND_combination.Count - 1));
			}
		}
	}
}
