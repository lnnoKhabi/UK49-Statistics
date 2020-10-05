using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brain
{
	public partial class DreamGuide : Form
	{
		Dictionary<int,List<string>> DATA ;

		public DreamGuide()
		{
			InitializeComponent();
		}

		private void SearchDream()
		{
			ListView_Results.Items.Clear();
			if ( !string.IsNullOrEmpty(RichTextBox_Searchbox.Text) )
			{
				string words2Search = RichTextBox_Searchbox.Text.ToLower();
				int num_to_search;
				if ( int.TryParse(words2Search, out num_to_search) )
				{
					if(num_to_search >= 1 & num_to_search <= 49 )
					{
						List<string> list = DATA[ num_to_search ];
						string w = "";
						foreach ( string words in list )
						{
							w += words + "; ";
						}
						w = w.Remove(w.Length - 2, 2);
						ListView_Results.Items.Add(w).SubItems.Add(num_to_search.ToString());

					}
					else
					{
						toolStripStatusLabel_Info.Text = "search numbers from 1 - 49 (incl) only.";
					}
				}
				else
				{
					Dictionary<int, List<string>> found = DATA.Where(list => list.Value.Where(s => s.ToLower().Contains(words2Search)).Count() > 0).ToDictionary(k => k.Key, v => v.Value);
					if ( found.Count > 0 )
					{
						foreach ( int word_num in found.Keys )
						{
							List<string> lst = found[ word_num ];
							lst = lst.Where(l => l.ToLower().Contains(words2Search)).ToList();
							foreach ( string s in lst )
							{
								ListView_Results.Items.Add(s).SubItems.Add($"{word_num}");
							}
						}
						toolStripStatusLabel_Info.Text = $"found {found.Count} result(s).";
					}
					else
					{
						toolStripStatusLabel_Info.Text = "Nothing found.";

					}
				}
			}
			else DisplayInfo();
		} 

		private void InitDatabase()
		{
			DATA = new Dictionary<int, List<string>>() {

				{ 1, new List<string>{"King","Human blood","White man","Left eye"}},
				{ 2, new List<string>{"Monkey","Native","Spirit","Chief","Copper","Money","Jockey"}},
				{ 3, new List<string>{"Sea Water","Accident","Frog","Sailor","Sex"}},
				{ 4, new List<string>{"Dead man","Turkey","Small fortune","Bed"}},
				{ 5, new List<string>{"Tiger","Fight","Strong man"}},
				{ 6, new List<string>{"Ox Blood","Gentleman","Milk"}},
				{ 7, new List<string>{"Lion","Thief","Big stick","Chickens"}},
				{ 8, new List<string>{"Pig","Drunken man","Loafer","Fat man","Chinese king"}},
				{ 9, new List<string>{"Moon","Baby","Hole","Owl","Devil","Pumpkin","Anything round"}},
				{ 10, new List<string>{"Eggs","Train","Boat","Grave","Anything oval"}},
				{ 11, new List<string>{"Carriage","Wood","Tree","Furniture","Bicycle","Flowers"}},
				{ 12, new List<string>{"Dead woman","Ducks","Small fire","Chinese Queen"}},
				{ 13, new List<string>{"Big fish","Ghosts","Spirits"}},
				{ 14, new List<string>{"Old woman","Fox","Detective","Nurse","Native woman"}},
				{ 15, new List<string>{"Bad woman","Prostitute","Canary","White horse","Small knife"}},
				{ 16, new List<string>{"Small house","Coffin","Pigeon","Young woman","Paper money","Letter"}},
				{ 17, new List<string>{"Diamond woman","Queen","Pearls","Diamond","Stars","White woman"}},
				{ 18, new List<string>{"Silver money","Servant girl","Right eye","Butterfly","Hook","Rain"}},
				{ 19, new List<string>{"Little girl","Smoke","Bread","Big bird","Left hand"}},
				{ 20, new List<string>{"Cat","Sky","Handkerchief","Body","Music","Minister","Naked woman"}},
				{ 21, new List<string>{"Old man","Stranger","Fisherman","Elephant","Knife","Nose","Teeth"}},
				{ 22, new List<string>{"Rats","Motor car","Big ship","Left foot","Shoes"}},
				{ 23, new List<string>{"Horse","Doctor","Head","Hair","Crown"}},
				{ 24, new List<string>{"Mouth","Wild cat","Vixen","Lioness","Hole","Purse"}},
				{ 25, new List<string>{"Big house","Church","Boxer","Hospital"}},
				{ 26, new List<string>{"Bees","Crown","Bad man","Bush","General","Funeral","Madman"}},
				{ 27, new List<string>{"Dog","Policeman","Newborn baby","Medicine","Sad news"}},
				{ 28, new List<string>{"Sardines","Small fish","Thief","Right foot","Surprise","Small child"}},
				{ 29, new List<string>{"Small water","Coffin","Rain","Tears","Big knife","Right hand"}},
				{ 30, new List<string>{"Fowl","Graveyard","Sun","Throat","Indian","Forest"}},
				{ 31, new List<string>{"Big fire","Bishop","Big spirit","Feathers","Fight","Woman"}},
				{ 32, new List<string>{"Gold money","Dirty woman","Snake"}},
				{ 33, new List<string>{"Little boy","Spider"}},
				{ 34, new List<string>{"Meat","Human dung","Anything dirty","Cripple","Tramp"}},
				{ 35, new List<string>{"Clothes","Sheep","Big hole","Big grave"}},
				{ 36, new List<string>{"Shrimp","Stick","Admiral","Cigars","Gum"}},
				{ 37, new List<string>{"Arrow","Lawyer","Treasure","Cooking","Stream"}},
				{ 38, new List<string>{"Crocodile","Balloons","Sjhambok","Fireworks","Stadium"}},
				{ 39, new List<string>{"Sangoma","Soccer team","Tattoos","Bloodshed","Teacher"}},
				{ 40, new List<string>{"Birth","Clock","Snail","Dwarf","River","Traditional healer"}},
				{ 41, new List<string>{"Cattle","Planets","Cave","Desert","Monster"}},
				{ 42, new List<string>{"Tornado","Spear","Umbrella","Camel","Door"}},
				{ 43, new List<string>{"Army","Thunder","Astronaut","Rabbit","Turtle"}},
				{ 44, new List<string>{"Shark","Stud farm","Body builder","Injury","Mud"}},
				{ 45, new List<string>{"Football","Computers","Jewellery","Wrestler","Storm"}},
				{ 46, new List<string>{"Ambulance","Beard","Sea sand","Scissors","Key"}},
				{ 47, new List<string>{"Stallion","Kite","TV","Lightning","Carnival","Hut"}},
				{ 48, new List<string>{"Clown","Rainbow","Nightmare","Whale","Wealth"}},
				{ 49, new List<string>{"Shebeen","Circus","Chocolate","Space ship"}}
			};
		}

		private void DisplayInfo()
		{
			foreach ( int number in DATA.Keys )
			{
				List<string> list = DATA[ number ];
				string w = "";
				foreach ( string words in list )
				{
					w += words + "; ";
				}
				w = w.Remove(w.Length - 2, 2);
				ListView_Results.Items.Add(w).SubItems.Add(number.ToString());
			}
		}

		private void DreamGuide_Shown( object sender, EventArgs e )
		{
			try
			{
				InitDatabase();
				DisplayInfo();
			}
			catch(Exception ex)
			{
				toolStripStatusLabel_Info.Text = ex.Message;

			}
		}

		private void RichTextBox_Searchbox_TextChanged( object sender, EventArgs e )
		{
			try
			{
				toolStripStatusLabel_Info.Text = "";
				SearchDream();
			}
			catch ( Exception ex )
			{
				toolStripStatusLabel_Info.Text = ex.Message;

			}
		}
	}
}
