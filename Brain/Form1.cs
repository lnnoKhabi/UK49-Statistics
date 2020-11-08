using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using HtmlAgilityPack;
using System.Net.Http;

namespace Brain
{
    public partial class Form1 : Form
    {
        private int WhereIsSquaredNumber = 0;

        private Dictionary<int, string> NumberAndDate = new Dictionary<int, string>();
        public static Dictionary<string, List<string>> date_AND_combination = new Dictionary<string, List<string>>();

        public Dictionary<string, int> Indexes_85m = new Dictionary<string, int>();//save indexes of all combs
        public List<string> CombTypedAsStrings = new List<string>();//save all combinations saved as strings e.g 1-2-3-4-5-6-7

        int[] values = new int[ 43 ];//indexes of known combs e.g 1-2-3-4-5-6-7  ,  2-3-4-5-6-7-8

        private string BonusFrequencyDirectory = null;//for saving data 

        private int one_thirty = 0;//for 1-30 count in addNumbers

        private CultureInfo CULTURE = new CultureInfo("en-GB");

        private string state = null;


        static DateTime MostRecentDate = DateTime.MinValue;
        private string ImportedFile = null; //file used when importing

        private DialogResult ShouldExit = DialogResult.None;

        public delegate void InvokeDelegate();
        string quit = null;
        Form f;

        string success = null;

        public Form1()
        {
            InitializeComponent();
            CULTURE.DateTimeFormat.LongTimePattern = "hh:mm:ss tt";
            CULTURE.DateTimeFormat.ShortTimePattern = "hh:mm tt";
            CULTURE.DateTimeFormat.LongDatePattern = "dd MMMM yyyy";
            CULTURE.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";

            Application.CurrentCulture = CULTURE;

        }   

        
        /* All event handling methods*********************************************************************************************/
        private void Form1_Shown( object sender, EventArgs e )//form is shown event
        {
            //AskForSavePath();

            //get indexes of known 43 combs
            int count = 1;

            for ( int i = 1; i < 44; i++ )
            {
                values[ i - 1 ] = count;
                for ( int j = i + 1; j < 45; j++ )
                {
                    for ( int k = j + 1; k < 46; k++ )
                    {
                        for ( int l = k + 1; l < 47; l++ )
                        {
                            for ( int m = l + 1; m < 48; m++ )
                            {
                                for ( int n = m + 1; n < 49; n++ )
                                {
                                    for ( int o = n + 1; o < 50; o++ )
                                    {
                                        count++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //when quitting the application
        private void Form1_FormClosing( object sender, FormClosingEventArgs e )
        {

            if ( quit == "no" )
            {
                e.Cancel = true;
            }
            else if ( quit == null )
            {
                e.Cancel = !HandleExiting();
            }
        }
        //when menu >> exit is pressed
        private void exitToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if ( HandleExiting() )
            {
                quit = "yes";
                Application.Exit();

            }
            else quit = "no";
        }
        private bool HandleExiting()
        {

            ShouldExit = MessageBox.Show("Are you sure you want to close the app?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if ( ShouldExit == DialogResult.Yes )//save & exit
            {
                return true;
            }
            return false;
        }

        private void textBox1_TextChanged( object sender, EventArgs e )
        {
            button1_Add.Enabled = AreNumbersCorrect();
        }

        private void textBox2_TextChanged( object sender, EventArgs e )
        {

            button1_Add.Enabled = AreNumbersCorrect();

        }

        private void textBox3_TextChanged( object sender, EventArgs e )
        {
            button1_Add.Enabled = AreNumbersCorrect();
        }

        private void textBox4_TextChanged( object sender, EventArgs e )
        {

            button1_Add.Enabled = AreNumbersCorrect();

        }

        private void textBox5_TextChanged( object sender, EventArgs e )
        {

            button1_Add.Enabled = AreNumbersCorrect();

        }

        private void textBox6_TextChanged( object sender, EventArgs e )
        {

            button1_Add.Enabled = AreNumbersCorrect();

        }

        private void textBox7_TextChanged( object sender, EventArgs e )
        {
            button1_Add.Enabled = AreNumbersCorrect();

        }

        private void button1_Add_Click( object sender, EventArgs e )
        {
            AddNumbers();
            dateTimePicker1_Date.Value = dateTimePicker1_Date.Value.AddDays(1);

            listView1_Date_and_Numbers.Items[ listView1_Date_and_Numbers.Items.Count - 1 ].EnsureVisible();

            if ( !string.IsNullOrEmpty(ImportedFile) )
            {
                using ( StreamWriter sw = new StreamWriter(ImportedFile,true) )
                {
                    sw.WriteLine();
                    KeyValuePair<string, List<string>> res = date_AND_combination.ElementAt(date_AND_combination.Count - 1);
                    string[] data = res.Value.ToArray();
                    DateTime dt = DateTime.Parse(res.Key);
                    sw.Write($"{dt.ToLongDateString()},{data[ 0 ]},{data[ 1 ]},{data[ 2 ]},{data[ 3 ]},{data[ 4 ]},{data[ 5 ]},{data[ 6 ]}");
                }
            }
        }

        private void listView1_Date_and_Numbers_ColumnClick( object sender, ColumnClickEventArgs e )
        {
            //SortNumbers(e.Column, true);
            //switch ( e.Column )
            //{
            //    case 0:
            //        AreDatesSorted = true;
            //        break;
            //    case 1:
            //        AreNumbersSorted = true;
            //        break;
            //}
        }

        private void deleteToolStripMenuItem_Click( object sender, EventArgs e )
        {
            DeleteEntry();
        }

        private void editNumberToolStripMenuItem_Click( object sender, EventArgs e )
        {
            NewNumberEdit();
        }

        private void editDateToolStripMenuItem_Click( object sender, EventArgs e )
        {
            EditDate();
        }

        private void toolStripMenuItem2_Change_Click( object sender, EventArgs e )
        {
            NewNumberEdit();
        }

        private void sortNumbersToolStripMenuItem_Click( object sender, EventArgs e )//sort unsorted listview
        {
            try
            {
                //sort items of unsorted listview
                ListViewItem[] SortedItems = listView1_Unsorted.Items.Cast<ListViewItem>().OrderBy(x => x.Tag).ToArray();

                listView1_Unsorted.Items.Clear();
                listView1_Unsorted.Items.AddRange(SortedItems);

                refreshToolStripMenuItem.Enabled = true;
                sortNumbersToolStripMenuItem.Enabled = false;

            }
            catch ( Exception f )
            {
                MessageBox.Show(f.Message, "Error sorting", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void searchToolStripMenuItem_Click( object sender, EventArgs e )//when self search is pressed on header
        {
            try
            {
                if ( listView1_Date_and_Numbers.Groups.Count >= 1 )
                {
                    ListViewItem start = listView1_Date_and_Numbers.Groups[ 0 ].Items[ 0 ];
                    ListViewItem end = listView1_Date_and_Numbers.Groups[ listView1_Date_and_Numbers.Groups.Count - 1 ].Items[ listView1_Date_and_Numbers.Groups[ listView1_Date_and_Numbers.Groups.Count - 1 ].Items.Count - 1 ];
                    toolStripTextBox1_Date_From.Text = start.Text;
                    toolStripTextBox2_Date_To.Text = end.Text;
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void refreshToolStripMenuItem_Click( object sender, EventArgs e )
        {
            Refresh_Unsorted_Listview();
        }

        private async void toolStripMenuItem2_Click_1( object sender, EventArgs e )//save output event
        {
            try
            {
				//ask locate to save file in
				FolderBrowserDialog selectfolderdialog = new FolderBrowserDialog
				{
					Description = "Choose a folder to save results.",
					ShowNewFolderButton = true,
					RootFolder = Environment.SpecialFolder.MyComputer
				};
				selectfolderdialog.ShowDialog();
                string folder = selectfolderdialog.SelectedPath;

                if (!string.IsNullOrEmpty(folder))
                {

                    Notify("Task started...");
                    //Action<string> act = SaveGeneratedResultsToTXT;
                    //Task task = new Task(() => act(folder));
                    Task<bool> task = new Task<bool>(arg => { return SaveGeneratedResultsToTXT((string)arg); }, folder);

                    task.Start();
                    await task;
                    if ( task.Result )
                    {
                        Notify("Task complete.");
                    }
                    else Notify("Task did not completed.");

                }
                else
                {
                    Notify("Please enter a valid file path.");
                }

            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool SaveGeneratedResultsToTXT( string folderpath )//save result files into *state/file of 1 - 43 
        {
            try
            {


                string state_ = null;
                int num_days = 0;
                if ( date_AND_combination.Count >= 10000 )
                {
                    state_ = "COMBINED";
                    num_days = 28;
                }
                else
                {
                    state_ = state.ToUpper();
                    num_days = 14;
                }

                Directory.CreateDirectory($"{folderpath}/Brain's Files/{state_}");
                if ( state.ToUpper() == "COMBINED" )
                {
                    Directory.CreateDirectory($"{folderpath}/Brain's Files/{state_}/4.All Order & Disorder");

                }
                else Directory.CreateDirectory($"{folderpath}/Brain's Files/{state_}/3.All Order & Disorder");

                //save tying in order and disoder******************************************
                string all_order_and_disorder = $"{folderpath}/Brain's Files/{state_}/3.All Order & Disorder/All Order & Disorder.txt";
                SaveTypingsAsIs(all_order_and_disorder);
                for ( int i = 1; i < 44; i++ )
                {
                    Directory.CreateDirectory($"{folderpath}/Brain's Files/{state_}/File of {i}");
                    Directory.CreateDirectory($"{folderpath}/Brain's Files/{state_}/File of {i}/1.As typed");
                    Directory.CreateDirectory($"{folderpath}/Brain's Files/{state_}/File of {i}/2.In grouping");
                    Directory.CreateDirectory($"{folderpath}/Brain's Files/{state_}/File of {i}/3.Adjacent dates");
                    Directory.CreateDirectory($"{folderpath}/Brain's Files/{state_}/File of {i}/4.Cycle");

                    string as_typed_PATH = $"{folderpath}/Brain's Files/{state_}/File of {i}/1.As typed/{i} as typed.txt";
                    string in_grouping_PATH = $"{folderpath}/Brain's Files/{state_}/File of {i}/2.In grouping/{i} in grouping.txt";
                    string adjacent_dates_PATH = $"{folderpath}/Brain's Files/{state_}/File of {i}/3.Adjacent dates/{i} adjacent dates.txt";
                    string cycle_PATH = $"{folderpath}/Brain's Files/{state_}/File of {i}/4.Cycle/{i} cycle.txt";

                    //string save_directory = $"{SaveGeneratedResultsToTXTDirectory}/Brain's Files/{i} results.txt";




                    //save as typed*******************************************************************************************************************
                    using ( StreamWriter sw = new StreamWriter(as_typed_PATH) )
                    {

                        foreach ( string a in TypeRangeofWhat(i) )
                        {
                            sw.WriteLine(a);

                        }
                    }

                    //save in grouping*******************************************************************************************************************
                    using ( StreamWriter sw = new StreamWriter(in_grouping_PATH) )
                    {

                        foreach ( string a in GroupingOrder(i) )
                        {
                            sw.WriteLine(a);

                        }
                    }

                    //save adjacent dates*******************************************************************************************************************
                    using ( StreamWriter sw = new StreamWriter(adjacent_dates_PATH) )
                    {

                        foreach ( string a in AdjacentDates(i) )
                        {
                            sw.WriteLine(a);

                        }
                    }

                    //**************still need to add code for saving cycle of 1 (ASK KELVIN)*************
                    //added but ask kev
                    //save cycle*******************************************************************************************************************
                    using ( StreamWriter sw = new StreamWriter(cycle_PATH) )
                    {
                        int count = 1;
                        List<List<string>> list_of_list = CycleOfNumWithNumbering(i);
                        for ( int j = 0; j < list_of_list.Count; j++ )
                        {
                            try
                            {
                                //if forward count is same
                                if ( list_of_list[ j ].Count == list_of_list[ j + 1 ].Count )
                                {
                                    if ( list_of_list[ j ].Count != list_of_list[ j - 1 ].Count )
                                    {
                                        count = 1;
                                    }
                                    for ( int k = 0; k < list_of_list[ j ].Count; k++ )
                                    {
                                        list_of_list[ j ][ k ] = $"{count}\t" + list_of_list[ j ][ k ];
                                        count++;
                                    }
                                }
                                //if backward count is same

                                else if ( list_of_list[ j ].Count == list_of_list[ j - 1 ].Count )
                                {
                                    for ( int k = 0; k < list_of_list[ j ].Count; k++ )
                                    {
                                        list_of_list[ j ][ k ] = $"{count}\t" + list_of_list[ j ][ k ];
                                        count++;
                                    }
                                }
                                //
                                else
                                {
                                    count = 1;
                                    for ( int k = 0; k < list_of_list[ j ].Count; k++ )
                                    {
                                        list_of_list[ j ][ k ] = $"{count}\t" + list_of_list[ j ][ k ];
                                        count++;
                                    }

                                }
                            }
                            // nothing in front (we are done )
                            catch
                            {
                                for ( int k = 0; k < list_of_list[ j ].Count; k++ )
                                {
                                    list_of_list[ j ][ k ] = $"{count}\t" + list_of_list[ j ][ k ];
                                    count++;
                                }
                            }
                        }

                        foreach ( List<string> list in list_of_list )
                        {
                            foreach ( string item in list )
                            {
                                sw.WriteLine(item);
                            }
                            sw.WriteLine("\n");
                        }
                    }


                }
                //save bonuses over 14 days***************************************************************************************************
                Dictionary<int, List<string>> bon = BonusesOver14Days(14);
                for ( int i = 0; i < 15; i++ )
                {
                    Directory.CreateDirectory($"{folderpath}/Brain's Files/{state_}/2.Bonuses over 14 days/{i}. day");
                    string dir = $"{folderpath}/Brain's Files/{state_}/2.Bonuses over 14 days/{i}. day/{i} day.txt";
                    using ( StreamWriter sw = new StreamWriter(dir) )
                    {
                        foreach ( string item in bon[ i ] )
                        {
                            sw.WriteLine(item);
                        }
                    }
                }

                //save bonuses over 28 days***************************************************************************************************
                if ( num_days == 28 )
                {
                    Dictionary<int, List<string>> bon28 = BonusesOver14Days(num_days);
                    for ( int i = 0; i < num_days + 1; i++ )
                    {
                        Directory.CreateDirectory($"{folderpath}/Brain's Files/{state_}/3.Bonuses over {num_days} days/{i}. day");
                        string dir = $"{folderpath}/Brain's Files/{state_}/3.Bonuses over {num_days} days/{i}. day/{i} day.txt";
                        using ( StreamWriter sw = new StreamWriter(dir) )
                        {
                            foreach ( string item in bon28[ i ] )
                            {
                                sw.WriteLine(item);
                            }
                        }
                    }
                }

                //save repetitions*******************************************************************************************************************
                List<string>[] Repetition = Repetitions();

                Directory.CreateDirectory($"{folderpath}/Brain's Files/{state_}/1.Everyday Repetition/Repetitions");
                Directory.CreateDirectory($"{folderpath}/Brain's Files/{state_}/1.Everyday Repetition/Skipped before Repetitions");

                string repetitions_PATH = $"{folderpath}/Brain's Files/{state_}/1.Everyday Repetition/Repetitions/repetitions.txt";
                string skipped_repetitions_PATH = $"{folderpath}/Brain's Files/{state_}/1.Everyday Repetition/Skipped before Repetitions/skipped before repetitions.txt";
                string[] paths = { repetitions_PATH, skipped_repetitions_PATH };

                for ( int i = 0; i < 2; i++ )
                {
                    using ( StreamWriter sw = new StreamWriter(paths[ i ]) )
                    {
                        foreach ( string item in Repetition[ i ] )
                        {
                            sw.WriteLine(item);
                        }
                    }

                }

                /*if ( state_.ToUpper() == "COMBINED" )
                {
                    //save combs as typed***************************************************************************************************
                    Directory.CreateDirectory($"{SaveGeneratedResultsToTXTDirectory}/Brain's Files/{state_}/4.All As Typed with Numbering");
                    string As_Typed_PATH = $"{SaveGeneratedResultsToTXTDirectory}/Brain's Files/{state_}/4.All As Typed with Numbering/All As Typed.txt";
                    List<string> ls = AsTyped();

                    using ( StreamWriter sw = new StreamWriter(As_Typed_PATH) )
                    {

                        foreach ( string item in ls )
                        {
                            sw.WriteLine(item);
                        }
                    }

                    //save combs in order***************************************************************************************************
                    Directory.CreateDirectory($"{SaveGeneratedResultsToTXTDirectory}/Brain's Files/{state_}/5.All In Order with Numbering");
                    string In_Order_PATH = $"{SaveGeneratedResultsToTXTDirectory}/Brain's Files/{state_}/5.All In Order with Numbering/All In Order.txt";
                    List<string> ls_order = InOrder();
                    using ( StreamWriter sw = new StreamWriter(In_Order_PATH) )
                    {

                        foreach ( string item in ls_order )
                        {
                            sw.WriteLine(item);
                        }
                    }

                }*/
            }
            catch ( Exception e )
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private async void correspondingNumbersToolStripMenuItem_Click( object sender, EventArgs e )//corresponding numbers event
        {
            try
            {
                //ask locate to save file in
                SaveFileDialog savefiledialog = new SaveFileDialog();
                savefiledialog.DefaultExt = "txt";
                savefiledialog.AddExtension = true;

                savefiledialog.Title = "Save file as...";
                savefiledialog.ShowDialog();

                string filename = savefiledialog.FileName;
                savefiledialog.Dispose();

                if ( !string.IsNullOrEmpty(filename))
                {
                    Notify("Task started...");

                    //search asynchronously
                    //Task task = new Task(Corresponding_Date_Number);
                    Task<bool> task = new Task<bool>(arg => { return Corresponding_Date_Number(( string ) arg); }, filename);
                    task.Start();
                    await task;
                    if ( task.Result )
                    {
                        Notify("Task completed.");
                    }
                    else Notify("Task did not complete.");
                }
                else
                {
                    Notify("Please enter a valid file path.");
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool Corresponding_Date_Number( string filepath )//finds all the corresponding numbers in combs <num == date>
        {
            try
            {
                if ( !string.IsNullOrEmpty(filepath) )
                {
                    Dictionary<string, int> dict = new Dictionary<string, int>();
                    foreach ( string key in date_AND_combination.Keys )
                    {
                        //string date = key.Substring(0, key.IndexOf(" "));

                        DateTime wrong_date = DateTime.Parse(key);
                        string date = $"{wrong_date.Day}/{wrong_date.Month}/{wrong_date.Year}";

                        if ( date_AND_combination[ key ].Contains(wrong_date.Day.ToString()) )//if day == any number in the played number
                        {
                            string State1 = key.Contains(" ") ? "T" : "L";

                            //store in a dict to sort by found number 
                            dict.Add($"{date}\t\t{date_AND_combination[ key ][ 0 ]}-{date_AND_combination[ key ][ 1 ]}-{date_AND_combination[ key ][ 2 ]}-{date_AND_combination[ key ][ 3 ]}-{date_AND_combination[ key ][ 4 ]}-{date_AND_combination[ key ][ 5 ]}-[{date_AND_combination[ key ][ 6 ]}]      ==>  {wrong_date.Day}\t{State1}", Convert.ToInt32(wrong_date.Day));
                        }
                    }
                    using ( StreamWriter sw = new StreamWriter(filepath) )
                    {
                        sw.WriteLine(state);
                        foreach ( string item in dict.OrderBy(x => x.Value).ToDictionary(x => x.Key, y => y.Value).Keys )//sort the dict
                        {
                            sw.WriteLine(item);
                        }
                    }
                }
            }
            catch ( Exception e )
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private async void bonusFrequencyToolStripMenuItem_Click( object sender, EventArgs e )//bonus frequency event
        {
            try
            {
				//ask locate to save file in
				SaveFileDialog savefiledialog = new SaveFileDialog
				{
					DefaultExt = "txt",
					AddExtension = true,

					Title = "Save file as..."
				};
				savefiledialog.ShowDialog();

                string filename = savefiledialog.FileName;
                savefiledialog.Dispose();

                if ( !string.IsNullOrEmpty(filename) )
                {
                    Notify("Task started...");

                    //search asynchronously
                    Task<bool> task = new Task<bool>(arg => { return FindBonusFrequency((string)arg); }, filename) ;
                    task.Start();
                    await task;

                    if ( task.Result )
                    {
                        Notify("Task completed.");
                    }
                    else Notify("Task did not complete.");
                }
                else
                {
                    Notify("Please enter a valid file path.");
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool FindBonusFrequency(string filepath)//finds play diff and repetitions of bonuses
        {
            try
            {
                if ( string.IsNullOrEmpty(filepath) && date_AND_combination.Count >= 2 )
                {
                    //Console.WriteLine($"{DateTime.Now.ToLongTimeString()} Counting indexes...");
                    if ( Indexes_85m.Count != CombTypedAsStrings.Count )
                    {
                        FindIndexesin85m();
                    }
                    //Console.WriteLine($"{DateTime.Now.ToLongTimeString()} Done counting indexes...");

                    Dictionary<string, int> Dates_and_Bonuses = new Dictionary<string, int>();
                    List<int> found = new List<int>();

                    foreach ( string key in date_AND_combination.Keys )
                    {
                        Dates_and_Bonuses.Add(key, Convert.ToInt32(date_AND_combination[ key ][ 6 ]));
                    }
                    Dates_and_Bonuses = Dates_and_Bonuses.Reverse().ToDictionary(x => x.Key, x => x.Value);


                    using ( StreamWriter sw = new StreamWriter(BonusFrequencyDirectory) )
                    {
                        for ( int i = 1; i <= 49; i++ )
                        {
                            Dictionary<string, int> New_Dates_and_Bonuses = new Dictionary<string, int>(Dates_and_Bonuses.Where(x => x.Value == i).ToDictionary(a => a.Key, b => b.Value));

                            sw.WriteLine($"Bonus: {i}");
                            found.Clear();
                            for ( int d = 0; d < New_Dates_and_Bonuses.Count; d++ )
                            {
                                string date = New_Dates_and_Bonuses.Keys.ElementAt(d);
                                if ( New_Dates_and_Bonuses[ date ] == i )
                                {
                                    for ( int ad = d + 1; ad < New_Dates_and_Bonuses.Count; ad++ )
                                    {
                                        try
                                        {
                                            string another_date = New_Dates_and_Bonuses.Keys.ElementAt(ad);

                                            TimeSpan days_between = DateTime.Parse(date) - DateTime.Parse(another_date);
                                            TimeSpanConverter tmc = new TimeSpanConverter();
                                            string days = ( string ) tmc.ConvertTo(days_between, typeof(string));
                                            try
                                            {
                                                days = days.Substring(0, days.IndexOf('.'));

                                            }
                                            catch
                                            {
                                                days = days.Substring(0, days.IndexOf(':'));

                                            }
                                            int actual_days_between = Math.Abs(Convert.ToInt32(days)) - 1;

                                            if ( New_Dates_and_Bonuses[ date ] == New_Dates_and_Bonuses[ another_date ] )
                                            {
                                                if ( actual_days_between <= 14 && !found.Contains(actual_days_between) )
                                                {
                                                    found.Add(actual_days_between);
                                                    //string new_date = date.Substring(0, date.IndexOf(' '));
                                                    //string new_another_date = another_date.Substring(0, another_date.IndexOf(' '));

                                                    DateTime array_date = DateTime.Parse(date);
                                                    DateTime array_another_date = DateTime.Parse(another_date);

                                                    string new_date = $"{array_date.Day}/{array_date.Month}/{array_date.Year}";
                                                    string new_another_date = $"{array_another_date.Day}/{array_another_date.Month}/{array_another_date.Year}";

                                                    List<string> a = new List<string>(date_AND_combination[ date ].OrderBy(x => Convert.ToInt32(x)).ToList());
                                                    List<string> b = new List<string>(date_AND_combination[ another_date ].OrderBy(x => Convert.ToInt32(x)).ToList());

                                                    sw.WriteLine($"\t\t{actual_days_between} day(s) between.");
                                                    sw.WriteLine($"\t\t{actual_days_between + 2} day(s) total.");

                                                    string State1 = date.Contains(" ") ? "T" : "L";
                                                    string State2 = another_date.Contains(" ") ? "T" : "L";

                                                    sw.WriteLine($"\t\tfound on {new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} {State2} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}");
                                                    sw.WriteLine($"\t\tthen on {new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} {State1} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\n");

                                                }
                                            }
                                        }
                                        catch { }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch ( Exception e )
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private async void removeTypedFrom85mToolStripMenuItem_Click( object sender, EventArgs e )//remove typed event
        {
            try
            {
                //ask locate to save file in
                SaveFileDialog savefiledialog = new SaveFileDialog();
                savefiledialog.DefaultExt = "txt";
                savefiledialog.AddExtension = true;

                savefiledialog.Title = "Save file as...";
                savefiledialog.ShowDialog();

                string filename = savefiledialog.FileName;
                savefiledialog.Dispose();

                if ( !string.IsNullOrEmpty(filename))
                {
                    Notify("Task started...");

                    //search asynchronously
                    Task<bool> task = new Task<bool>(arg => { return RemoveTyped((string)arg); }, filename);
                    task.Start();
                    await task;
                    if ( task.Result )
                    {
                        Notify("Task completed.");
                    }
                    else Notify("Task did not complete.");
                }
                else
                {
                    Notify("Please enter a valid file path.");
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool RemoveTyped( string filename )//removes all the typed combinations from the 85 million
        {
            try
            {
                if ( !string.IsNullOrEmpty(filename) )
                {
                    HashSet<string> lst = new HashSet<string>(CombTypedAsStrings);
                    using ( StreamWriter sw = new StreamWriter(filename) )
                    {
                        for ( int a = 1; a < 44; a++ )
                        {
                            for ( int b = a + 1; b < 45; b++ )
                            {
                                for ( int c = b + 1; c < 46; c++ )
                                {
                                    for ( int d = c + 1; d < 47; d++ )
                                    {
                                        for ( int e = d + 1; e < 48; e++ )
                                        {
                                            for ( int f = e + 1; f < 49; f++ )
                                            {
                                                for ( int g = f + 1; g < 50; g++ )
                                                {
                                                    string str = lst.Contains($"{a}-{b}-{c}-{d}-{e}-{f}-{g}") ? null : $"{a}-{b}-{c}-{d}-{e}-{f}-{g}";
                                                    sw.Write(str == null ? str : str + "\n");
                                                }
                                            }

                                        }

                                    }

                                }
                            }
                        }
                    }

                }
            }
            catch ( Exception e )
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

		private async void includeBonusToolStripMenuItem1_Click( object sender, EventArgs e )
		{
            try
            {
                //ask locate to save file in
                SaveFileDialog savefiledialog = new SaveFileDialog();
                savefiledialog.DefaultExt = "txt";
                savefiledialog.AddExtension = true;
                savefiledialog.Title = "Save file as...";
                savefiledialog.ShowDialog();
                string filename = savefiledialog.FileName;
                savefiledialog.Dispose();

                if ( !string.IsNullOrEmpty(filename) )
                {
                    Notify("Task started");

                    //search asynchronously
                    Task<bool> task = new Task<bool>(arg => { return CalculateCycle_WithBonus(( string ) arg); }, filename);
                    task.Start();
                    await task;
                    if ( task.Result )
                    {
                        Notify("Task completed.");
                    }
                    else Notify("Task did not completed.");
                }
                else
                {
                    Notify("Please enter a valid file path.");
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }// compute cycle event with bonus
        private bool CalculateCycle_WithBonus(string filename)//
        {
            try
            {
                List<string> one_to_49 = new List<string>();

                int count = 1;
                bool number_of_items_changed = false;
                if (!string.IsNullOrEmpty(filename) )
                {

                    using ( StreamWriter sw = new StreamWriter(filename) )
                    {
                        foreach ( string date_key in date_AND_combination.Keys )
                        {
                            //string correct_date = date_key.Substring(0, date_key.IndexOf(' '));
                            DateTime wrong_date_array = DateTime.Parse(date_key);
                            string correct_date = $"{wrong_date_array.Day}/{wrong_date_array.Month}/{wrong_date_array.Year}";


                            if ( one_to_49.Count == 0 ) //populate numbers 1 - 49 if its empty
                            {
                                string State1 = date_key.Contains(" ") ? "T" : "L";

                                sw.WriteLine($"\n{State1}");
                                sw.WriteLine($"Cyle {count} begining date: {correct_date}");

                                sw.WriteLine($"First numbers of the cycle: {date_AND_combination[ date_key ][ 0 ]}-{date_AND_combination[ date_key ][ 1 ]}-{date_AND_combination[ date_key ][ 2 ]}-{date_AND_combination[ date_key ][ 3 ]}-{date_AND_combination[ date_key ][ 4 ]}-{date_AND_combination[ date_key ][ 5 ]}-[{date_AND_combination[ date_key ][ 6 ]}]");
                                count++;
                                for ( int i = 1; i <= 49; i++ )
                                {
                                    one_to_49.Add(i.ToString());
                                }
                            }

                            foreach ( string number in date_AND_combination[ date_key ] )
                            {
                                switch ( one_to_49.Count )
                                {

                                    case 7:
                                        if ( number_of_items_changed )
                                        {
                                            sw.WriteLine($"7 last numbers: {one_to_49[ 0 ]}-{one_to_49[ 1 ]}-{one_to_49[ 2 ]}-{one_to_49[ 3 ]}-{one_to_49[ 4 ]}-{one_to_49[ 5 ]}-{one_to_49[ 6 ]}");
                                        }
                                        break;

                                    case 6:
                                        if ( number_of_items_changed )
                                        {
                                            sw.WriteLine($"6 last numbers: {one_to_49[ 0 ]}-{one_to_49[ 1 ]}-{one_to_49[ 2 ]}-{one_to_49[ 3 ]}-{one_to_49[ 4 ]}-{one_to_49[ 5 ]}");
                                        }
                                        break;

                                    case 5:
                                        if ( number_of_items_changed )
                                        {
                                            sw.WriteLine($"5 last numbers: {one_to_49[ 0 ]}-{one_to_49[ 1 ]}-{one_to_49[ 2 ]}-{one_to_49[ 3 ]}-{one_to_49[ 4 ]}");
                                        }
                                        break;

                                    case 4:
                                        if ( number_of_items_changed )
                                        {
                                            sw.WriteLine($"4 last numbers: {one_to_49[ 0 ]}-{one_to_49[ 1 ]}-{one_to_49[ 2 ]}-{one_to_49[ 3 ]}");
                                        }
                                        break;

                                    case 3:
                                        if ( number_of_items_changed )
                                        {
                                            sw.WriteLine($"3 last numbers: {one_to_49[ 0 ]}-{one_to_49[ 1 ]}-{one_to_49[ 2 ]}");
                                        }
                                        break;

                                    case 2:
                                        if ( number_of_items_changed )
                                        {
                                            sw.WriteLine($"2 last numbers: {one_to_49[ 0 ]}-{one_to_49[ 1 ]}");
                                        }
                                        if ( one_to_49.Contains(number) )
                                        {
                                            sw.WriteLine($"{number} came out with {date_AND_combination[ date_key ][ 0 ]}-{date_AND_combination[ date_key ][ 1 ]}-{date_AND_combination[ date_key ][ 2 ]}-{date_AND_combination[ date_key ][ 3 ]}-{date_AND_combination[ date_key ][ 4 ]}-{date_AND_combination[ date_key ][ 5 ]}-[{date_AND_combination[ date_key ][ 6 ]}] on {correct_date}");
                                        }
                                        break;
                                    case 1:

                                        if ( one_to_49.Contains(number) )
                                        {
                                            sw.WriteLine($"{number} came out with {date_AND_combination[ date_key ][ 0 ]}-{date_AND_combination[ date_key ][ 1 ]}-{date_AND_combination[ date_key ][ 2 ]}-{date_AND_combination[ date_key ][ 3 ]}-{date_AND_combination[ date_key ][ 4 ]}-{date_AND_combination[ date_key ][ 5 ]}-[{date_AND_combination[ date_key ][ 6 ]}] on {correct_date}");
                                        }
                                        break;
                                }

                                if ( one_to_49.Contains(number) )
                                {
                                    one_to_49.Remove(number);
                                    number_of_items_changed = true;
                                }
                                else
                                {
                                    number_of_items_changed = false;

                                }
                            }
                        }
                    }

                }

            }
            catch ( Exception e )
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

		private async void excludeBonusesToolStripMenuItem_Click( object sender, EventArgs e )
		{
            try
            {
                //ask locate to save file in
                SaveFileDialog savefiledialog = new SaveFileDialog();
                savefiledialog.DefaultExt = "txt";
                savefiledialog.AddExtension = true;

                savefiledialog.Title = "Save file as...";
                savefiledialog.ShowDialog();

                string filename = savefiledialog.FileName;
                savefiledialog.Dispose();

                if ( !string.IsNullOrEmpty(filename) )
                {
                    Notify("Task started...");

                    //search asynchronously
                    Task<bool> task = new Task<bool>(arg => { return CalculateCycle_WithoutBonus(( string ) arg); }, filename);
                    task.Start();
                    await task;

                    if ( task.Result )
                    {
                        Notify("Task completed.");
                    }
                    else Notify("Task did not completed.");

                }
                else
                {
                    Notify("Please enter a valid file name.");
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }// compute cycle event without bonus
        private bool CalculateCycle_WithoutBonus(string filename)//
        {
            try
            {
                List<string> one_to_49 = new List<string>();
                int count = 1;
                bool number_of_items_changed = false;

                Dictionary<string, List<string>> dict_with_data = new Dictionary<string, List<string>>();
                foreach ( string key in date_AND_combination.Keys )
                {
                    dict_with_data.Add(key, new List<string>(date_AND_combination[ key ]));
                    dict_with_data[ key ].RemoveAt(6);
                }

                if (!string.IsNullOrEmpty(filename))
                {

                    using ( StreamWriter sw = new StreamWriter(filename) )
                    {
                        sw.WriteLine(state);

                        foreach ( string date_key in dict_with_data.Keys )
                        {

                            //string correct_date = date_key.Substring(0, date_key.IndexOf(' '));
                            DateTime wrong_date_array = DateTime.Parse(date_key);
                            string correct_date = $"{wrong_date_array.Day}/{wrong_date_array.Month}/{wrong_date_array.Year}";

                            if ( one_to_49.Count == 0 ) //populate numbers 1 - 49 if its empty
                            {
                                string State1 = date_key.Contains(" ") ? "T" : "L";

                                sw.WriteLine($"\n{State1}");
                                sw.WriteLine($"Cyle {count} begining date: {correct_date}");

                                sw.WriteLine($"First numbers of the cycle: {dict_with_data[ date_key ][ 0 ]}-{dict_with_data[ date_key ][ 1 ]}-{dict_with_data[ date_key ][ 2 ]}-{dict_with_data[ date_key ][ 3 ]}-{dict_with_data[ date_key ][ 4 ]}-{dict_with_data[ date_key ][ 5 ]}");
                                count++;
                                for ( int i = 1; i <= 49; i++ )
                                {
                                    one_to_49.Add(i.ToString());
                                }
                            }

                            foreach ( string number in dict_with_data[ date_key ] )
                            {
                                switch ( one_to_49.Count )
                                {

                                    case 7:
                                        if ( number_of_items_changed )
                                        {
                                            sw.WriteLine($"7 last numbers: {one_to_49[ 0 ]}-{one_to_49[ 1 ]}-{one_to_49[ 2 ]}-{one_to_49[ 3 ]}-{one_to_49[ 4 ]}-{one_to_49[ 5 ]}-{one_to_49[ 6 ]}");
                                        }
                                        break;

                                    case 6:
                                        if ( number_of_items_changed )
                                        {
                                            sw.WriteLine($"6 last numbers: {one_to_49[ 0 ]}-{one_to_49[ 1 ]}-{one_to_49[ 2 ]}-{one_to_49[ 3 ]}-{one_to_49[ 4 ]}-{one_to_49[ 5 ]}");
                                        }
                                        break;

                                    case 5:
                                        if ( number_of_items_changed )
                                        {
                                            sw.WriteLine($"5 last numbers: {one_to_49[ 0 ]}-{one_to_49[ 1 ]}-{one_to_49[ 2 ]}-{one_to_49[ 3 ]}-{one_to_49[ 4 ]}");
                                        }
                                        break;

                                    case 4:
                                        if ( number_of_items_changed )
                                        {
                                            sw.WriteLine($"4 last numbers: {one_to_49[ 0 ]}-{one_to_49[ 1 ]}-{one_to_49[ 2 ]}-{one_to_49[ 3 ]}");
                                        }
                                        break;

                                    case 3:
                                        if ( number_of_items_changed )
                                        {
                                            sw.WriteLine($"3 last numbers: {one_to_49[ 0 ]}-{one_to_49[ 1 ]}-{one_to_49[ 2 ]}");
                                        }
                                        break;

                                    case 2:
                                        if ( number_of_items_changed )
                                        {
                                            sw.WriteLine($"2 last numbers: {one_to_49[ 0 ]}-{one_to_49[ 1 ]}");
                                        }
                                        if ( one_to_49.Contains(number) )
                                        {
                                            sw.WriteLine($"{number} came out with {dict_with_data[ date_key ][ 0 ]}-{dict_with_data[ date_key ][ 1 ]}-{dict_with_data[ date_key ][ 2 ]}-{dict_with_data[ date_key ][ 3 ]}-{dict_with_data[ date_key ][ 4 ]}-{dict_with_data[ date_key ][ 5 ]} on {correct_date}");
                                        }
                                        break;
                                    case 1:

                                        if ( one_to_49.Contains(number) )
                                        {
                                            sw.WriteLine($"{number} came out with {dict_with_data[ date_key ][ 0 ]}-{dict_with_data[ date_key ][ 1 ]}-{dict_with_data[ date_key ][ 2 ]}-{dict_with_data[ date_key ][ 3 ]}-{dict_with_data[ date_key ][ 4 ]}-{dict_with_data[ date_key ][ 5 ]} on {correct_date}");
                                        }
                                        break;
                                }

                                if ( one_to_49.Contains(number) )
                                {
                                    one_to_49.Remove(number);
                                    number_of_items_changed = true;
                                }
                                else
                                {
                                    number_of_items_changed = false;

                                }
                            }
                        }
                    }

                }
            }
            catch ( Exception e )
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;

            }
				return true;
        }

        private void toolStripMenuItem3_Click( object sender, EventArgs e )//clear everything on form event
        {
            ClearFormData( true);
        }

        private void lunchtimeToolStripMenuItem_Click( object sender, EventArgs e )//Import Lunchtime event
        {
            ImportRequired("L");
        }
        private void teatimeToolStripMenuItem_Click( object sender, EventArgs e )//Import Teatime event
        {
            ImportRequired("T");
        }
        private void Import_Both_ToolStripMenuItem1_Click( object sender, EventArgs e )//import both event
        {
            ImportRequired("L");
            ImportRequired("T");

            menuStrip1_Toolbar.BackColor = Color.LightSeaGreen;

            //sort date_and_comb
            date_AND_combination = date_AND_combination.OrderBy(x => DateTime.Parse(x.Key)).ToDictionary(k => k.Key, v => v.Value);

            //sort the items in listview
            SortNumbers(0);

            //order unsorted_listview by date
            //AreUnsortedNumbersSorted = true;
            Refresh_Unsorted_Listview();
        }
        private void ImportRequired( string required_state )//import T, L or B
        {
            if ( required_state == "L" )
            {
                state = "L";
                if ( Import())//if import was successful
                {
                    menuStrip1_Toolbar.BackColor = Color.SkyBlue;
                    //disable all buttons
                    EnableButtons(false);
                }
                else
                {
                    state = null;
                }
            }
            else if ( required_state == "T" )
            {
                state = "T";
                if ( Import() )//if import was successful
                {
                    menuStrip1_Toolbar.BackColor = Color.Tomato;
                    //disable all buttons
                    EnableButtons(false);
                }
                else
                {
                    state = null;
                }
            }
        }
        private bool Import()//import method
        {
            try
            {
                string st = state == "L" ? "Lunchtime" : "Teatime";

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = $"Select {st} file.";
                ofd.Multiselect = false;
                ofd.Filter = "Text files (*.txt)|*.txt";
                ofd.ShowDialog();
                string filename = ofd.FileName;
                ImportedFile = filename;
                ofd.Dispose();
                if ( !string.IsNullOrEmpty(filename) )
                {
                    Task task = new Task(arg => { ShowProgress(( bool ) arg); }, true);
                    task.Start();

                    Cursor = Cursors.No;

                    TextBox[] boxes = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7 };
                    using ( StreamReader sr = new StreamReader(filename) )
                    {
                        
                        string str = "";
                        while ( ( str = sr.ReadLine() ) != null )
                        {
                            string[] data = str.Split(',');

                            dateTimePicker1_Date.Value = DateTime.Parse(data[ 0 ]);
                            boxes[ 0 ].Text = data[ 1 ];

                            boxes[ 1 ].Text = data[ 2 ];
                            boxes[ 2 ].Text = data[ 3 ];
                            boxes[ 3 ].Text = data[ 4 ];
                            boxes[ 4 ].Text = data[ 5 ];
                            boxes[ 5 ].Text = data[ 6 ];
                            boxes[ 6 ].Text = data[ 7 ];
                            if ( button1_Add.Enabled )
                                AddNumbers();
                            else Notify($"Error on {data[ 0 ]}. Numbers not added.");
                        }
                    }
                    dateTimePicker1_Date.Value = dateTimePicker1_Date.Value.AddDays(1);

                    listView1_Date_and_Numbers.Items[listView1_Date_and_Numbers.Items.Count-1].EnsureVisible();

                }
                else
				{
                    Cursor = Cursors.Default;
                    return false;
                }
            }
            catch ( Exception error )
            {
                ShowProgress(false);
                Cursor = Cursors.Default;

                MessageBox.Show(error.Message, "Error Importing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            ShowProgress(false);
            Cursor = Cursors.Default;

            return true;
        }
        private delegate void Myd();
        private void ShowProgress( bool add_or_remove )
        {
            //create status strip
            if ( add_or_remove )
            {
                string st = state == "L" ? "Lunchtime" : "Teatime";
                //create form for progress bar and label
                f = new Form();
                f.Text = $"Progress";
                f.Size = new Size(300, 100);
                f.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                f.StartPosition = FormStartPosition.CenterParent;
                f.ControlBox = false;
                //f.TopMost = true;
                f.MaximizeBox = false;
                f.MinimizeBox = false;

				// create label for holding text
				Label label = new Label();
				label.Text = $"Importing {st} results...";
				label.Dock = DockStyle.Top;
				//label.TextAlign = ContentAlignment.MiddleCenter;
				label.Parent = f;

                //create progress bar
                ProgressBar progressbar = new ProgressBar();
                progressbar.Style = ProgressBarStyle.Marquee;
                progressbar.Padding = new Padding(0,0,0,10);
                progressbar.Dock = DockStyle.Bottom;
                progressbar.Parent = f;
                progressbar.MarqueeAnimationSpeed = 45;//the higher the slower 

				f.ShowDialog();
            }
            else
            {
                f.BeginInvoke(( Myd ) delegate { f.Dispose(); });
            }
        }

        private void EnableButtons( bool yes_no )
        {
            if ( yes_no )
            {
                lunchtimeToolStripMenuItem.Enabled = true;//import lunch button
                teatimeToolStripMenuItem.Enabled = true;//import tea button
                Import_Both_ToolStripMenuItem1.Enabled = true;//import both button
                lunchtimeToolStripMenuItem1.Enabled = true;//get lunch button
                teatimeToolStripMenuItem1.Enabled = true;//get tea button
            }
            else
            {
                lunchtimeToolStripMenuItem.Enabled = false;//import lunch button
                teatimeToolStripMenuItem.Enabled = false;//import tea button
                Import_Both_ToolStripMenuItem1.Enabled = false;//import both button
                lunchtimeToolStripMenuItem1.Enabled = false;//get lunch button
                teatimeToolStripMenuItem1.Enabled = false;//get tea button
            }
        }

        private async void sixToolStripMenuItem_Click( object sender, EventArgs e )//event raised when self search all(6) is presssed
        {
            try
            {
                //ask location to save file as
                SaveFileDialog savefiledialog = new SaveFileDialog();
                savefiledialog.DefaultExt = ".txt";
                savefiledialog.AddExtension = true;

                savefiledialog.Title = "Save file";
                savefiledialog.ShowDialog();

                string filename = savefiledialog.FileName;
                savefiledialog.Dispose();

                if ( !string.IsNullOrEmpty(filename))
                {
                    Notify("Task started...");

                    //search the numbers asynchronously
                    Task<List<string>> task = new Task<List<string>>(arg => { return SelfSearch(( string ) arg); },filename);
                    task.Start();
                    await task;

                    if ( task.Result[ 0 ] == "Search successful!" )
                    {
                        Notify("Task completed.");
                    }
                    else
                    {
                        foreach ( string result in task.Result )
                        {
                            Notify(result);
                        }
                    }
                }
                else
                {
                    Notify("Please enter a valid file name.");
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private List<string> SelfSearch( string filename )//for searching for numbers between specified dates (all 85m numbers)
        {
            GetNumbersBetweenSpecifiedDates();
            List<string> result = new List<string>();
            try
            {
                if ( !string.IsNullOrEmpty(filename) )
                {
                    if ( NumberAndDate.Count == 49 )
                    {
                        using ( StreamWriter streamwriter = new StreamWriter(filename) )
                        {
                            streamwriter.WriteLine($"Dates for the numbers found.\n");

                            for ( int i = 1; i <= NumberAndDate.Count; i++ )
                            {
                                if ( NumberAndDate.ContainsKey(i) )
                                {
                                    streamwriter.WriteLine($"{i} ==> {NumberAndDate[ i ]}");

                                }
                            }
                            streamwriter.WriteLine($"\nNumbers found.\n");

                            DateTime[] dates = new DateTime[ 6 ];


                            int max = 50;
                            for ( int a = 2; a < max - 6; a++ )
                            {
                                for ( int b = a + 1; b < max - 5; b++ )
                                {
                                    for ( int c = b + 1; c < max - 4; c++ )
                                    {
                                        for ( int d = c + 1; d < max - 3; d++ )
                                        {
                                            for ( int e = d + 1; e < max - 2; e++ )
                                            {
                                                for ( int f = e + 1; f < max - 1; f++ )
                                                {
                                                    string folders = "";
                                                    for ( int g = 1; g < a; g++ )
                                                    {
                                                        folders += $" f{g}";
                                                    }

                                                    dates[ 0 ] = DateTime.Parse(NumberAndDate[ a ]);
                                                    dates[ 1 ] = DateTime.Parse(NumberAndDate[ b ]);
                                                    dates[ 2 ] = DateTime.Parse(NumberAndDate[ c ]);
                                                    dates[ 3 ] = DateTime.Parse(NumberAndDate[ d ]);
                                                    dates[ 4 ] = DateTime.Parse(NumberAndDate[ e ]);
                                                    dates[ 5 ] = DateTime.Parse(NumberAndDate[ f ]);
                                                    dates = dates.OrderBy(p => p).ToArray();
                                                    streamwriter.WriteLine($"{a}-{b}-{c}-{d}-{e}-{f} in {folders}, ==> {dates[ 0 ].ToShortDateString()}, {dates[ 1 ].ToShortDateString()}, {dates[ 2 ].ToShortDateString()}, {dates[ 3 ].ToShortDateString()}, {dates[ 4 ].ToShortDateString()}, {dates[ 5 ].ToShortDateString()}.");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            result.Add("Search successful!");
                        }
                    }

                    else if ( NumberAndDate.Count < 49 )
                    {
                        //get the numbers that were not found
                        int[] keys = NumberAndDate.Keys.ToArray();
                        int[] all_keys = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49 };

                        int[] diff_keys = all_keys.Except<int>(keys).ToArray();
                        using ( StreamWriter streamwriter = new StreamWriter(filename) )
                        {
                            streamwriter.WriteLine($"Dates for the numbers found.\n");

                            for ( int i = 1; i <= NumberAndDate.Count; i++ )
                            {
                                if ( NumberAndDate.ContainsKey(i) )
                                {
                                    streamwriter.WriteLine($"{i} ==> {NumberAndDate[ i ]}");

                                }
                            }
                            streamwriter.WriteLine($"\nNumbers found.\n");
                            DateTime[] dates = new DateTime[ 6 ];

                            int max = 50;
                            for ( int a = 2; a < max - 6; a++ )
                            {
                                for ( int b = a + 1; b < max - 5; b++ )
                                {
                                    for ( int c = b + 1; c < max - 4; c++ )
                                    {
                                        for ( int d = c + 1; d < max - 3; d++ )
                                        {
                                            for ( int e = d + 1; e < max - 2; e++ )
                                            {
                                                for ( int f = e + 1; f < max - 1; f++ )
                                                {
                                                    if ( diff_keys.Contains(a) || diff_keys.Contains(b) || diff_keys.Contains(c) || diff_keys.Contains(d) || diff_keys.Contains(e) || diff_keys.Contains(f) )
                                                    {
                                                        continue;
                                                    }
                                                    string folders = "";
                                                    for ( int g = 1; g < a; g++ )
                                                    {
                                                        folders += $" f{g}";
                                                    }


                                                    dates[ 0 ] = DateTime.Parse(NumberAndDate[ a ]);
                                                    dates[ 1 ] = DateTime.Parse(NumberAndDate[ b ]);
                                                    dates[ 2 ] = DateTime.Parse(NumberAndDate[ c ]);
                                                    dates[ 3 ] = DateTime.Parse(NumberAndDate[ d ]);
                                                    dates[ 4 ] = DateTime.Parse(NumberAndDate[ e ]);
                                                    dates[ 5 ] = DateTime.Parse(NumberAndDate[ f ]);
                                                    dates = dates.OrderBy(p => p).ToArray();
                                                    streamwriter.WriteLine($"{a}-{b}-{c}-{d}-{e}-{f} in {folders}, ==> {dates[ 0 ].ToShortDateString()}, {dates[ 1 ].ToShortDateString()}, {dates[ 2 ].ToShortDateString()}, {dates[ 3 ].ToShortDateString()}, {dates[ 4 ].ToShortDateString()}, {dates[ 5 ].ToShortDateString()}.");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            result.Add("Search successful!");

                        }
                    }
                    else if ( NumberAndDate.Count > 49 )
                    {
                        for ( int i = 50; i < NumberAndDate.Count; i++ )
                        {
                            result.Add($"Error, there might be a mistake on {NumberAndDate.Values.ElementAt(i)}");
                        }
                    }
                }
            }
            catch ( Exception e )
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result.Count != 0 ? result : new List<string> { "Unknown error" };
        }

        private async void sevenToolStripMenuItem_Click( object sender, EventArgs e )//event raised when self search all(7) is presssed
        {
            try
            {
                //ask location to save file as
                SaveFileDialog savefiledialog = new SaveFileDialog();
                savefiledialog.DefaultExt = ".txt";
                savefiledialog.AddExtension = true;

                savefiledialog.Title = "Save file";
                savefiledialog.ShowDialog();

                string filename = savefiledialog.FileName;
                savefiledialog.Dispose();

                if ( !string.IsNullOrEmpty(filename))
                {
                    Notify("Task started...");

                    //search the numbers asynchronously
                    Task<List<string>> task = new Task<List<string>>(arg => { return SelfSearch_7(( string ) arg); }, filename);
                    task.Start();
                    await task;

                    if ( task.Result[ 0 ] == "Search successful!" )
                    {
                        Notify("Task completed.");

                    }
                    else
                    {
                        foreach ( string result in task.Result )
                        {
                            Notify(result);
                        }
                    }
                }
                else
                {
                    Notify("Please enter a valid file name.");
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private List<string> SelfSearch_7( string filename )//for searching for numbers between specified dates
        {
            GetNumbersBetweenSpecifiedDates();
            List<string> result = new List<string>();
            try
            {
                if ( !string.IsNullOrEmpty(filename) )
                {
                    if ( NumberAndDate.Count == 49 )
                    {
                        using ( StreamWriter streamwriter = new StreamWriter(filename) )
                        {
                            streamwriter.WriteLine($"Dates for the numbers found.\n");

                            for ( int i = 1; i <= NumberAndDate.Count; i++ )
                            {
                                if ( NumberAndDate.ContainsKey(i) )
                                {
                                    streamwriter.WriteLine($"{i} ==> {NumberAndDate[ i ]}");

                                }
                            }
                            streamwriter.WriteLine($"\nNumbers found.\n");
                            DateTime[] dates = new DateTime[ 7 ];

                            int max = 50;

                            for ( int a = 2; a < max - 7; a++ )
                            {
                                for ( int b = a + 1; b < max - 6; b++ )
                                {
                                    for ( int c = b + 1; c < max - 5; c++ )
                                    {
                                        for ( int d = c + 1; d < max - 4; d++ )
                                        {
                                            for ( int e = d + 1; e < max - 3; e++ )
                                            {
                                                for ( int f = e + 1; f < max - 2; f++ )
                                                {
                                                    for ( int g = f + 1; g < max - 1; g++ )
                                                    {
                                                        string folders = "";
                                                        for ( int h = 1; h < a; h++ )
                                                        {
                                                            folders += $" f{h}";
                                                        }
                                                        dates[ 0 ] = DateTime.Parse(NumberAndDate[ a ]);
                                                        dates[ 1 ] = DateTime.Parse(NumberAndDate[ b ]);
                                                        dates[ 2 ] = DateTime.Parse(NumberAndDate[ c ]);
                                                        dates[ 3 ] = DateTime.Parse(NumberAndDate[ d ]);
                                                        dates[ 4 ] = DateTime.Parse(NumberAndDate[ e ]);
                                                        dates[ 5 ] = DateTime.Parse(NumberAndDate[ f ]);
                                                        dates[ 6 ] = DateTime.Parse(NumberAndDate[ g ]);
                                                        dates = dates.OrderBy(p => p).ToArray();
                                                        streamwriter.WriteLine($"{a}-{b}-{c}-{d}-{e}-{f}-{g} in {folders}, ==> {dates[ 0 ].ToShortDateString()}, {dates[ 1 ].ToShortDateString()}, {dates[ 2 ].ToShortDateString()}, {dates[ 3 ].ToShortDateString()}, {dates[ 4 ].ToShortDateString()}, {dates[ 5 ].ToShortDateString()}, {dates[ 6 ].ToShortDateString()}.");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            result.Add("Search successful!");
                        }
                    }

                    else if ( NumberAndDate.Count < 49 )
                    {
                        //get the numbers that were not found
                        int[] keys = NumberAndDate.Keys.ToArray();
                        int[] all_keys = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49 };

                        int[] diff_keys = all_keys.Except<int>(keys).ToArray();
                        using ( StreamWriter streamwriter = new StreamWriter(filename) )
                        {
                            streamwriter.WriteLine($"Dates for the numbers found.\n");

                            for ( int i = 1; i <= NumberAndDate.Count; i++ )
                            {
                                if ( NumberAndDate.ContainsKey(i) )
                                {
                                    streamwriter.WriteLine($"{i} ==> {NumberAndDate[ i ]}");

                                }
                            }
                            streamwriter.WriteLine($"\nNumbers found.\n");
                            DateTime[] dates = new DateTime[ 7 ];

                            int max = 50;
                            for ( int a = 2; a < max - 6; a++ )
                            {
                                for ( int b = a + 1; b < max - 5; b++ )
                                {
                                    for ( int c = b + 1; c < max - 4; c++ )
                                    {
                                        for ( int d = c + 1; d < max - 3; d++ )
                                        {
                                            for ( int e = d + 1; e < max - 2; e++ )
                                            {
                                                for ( int f = e + 1; f < max - 1; f++ )
                                                {
                                                    for ( int g = f + 1; g < max - 1; g++ )
                                                    {
                                                        if ( diff_keys.Contains(a) || diff_keys.Contains(b) || diff_keys.Contains(c) || diff_keys.Contains(d) || diff_keys.Contains(e) || diff_keys.Contains(f) || diff_keys.Contains(g) )
                                                        {
                                                            continue;
                                                        }
                                                        string folders = "";
                                                        for ( int h = 1; h < a; h++ )
                                                        {
                                                            folders += $" f{h}";
                                                        }
                                                        dates[ 0 ] = DateTime.Parse(NumberAndDate[ a ]);
                                                        dates[ 1 ] = DateTime.Parse(NumberAndDate[ b ]);
                                                        dates[ 2 ] = DateTime.Parse(NumberAndDate[ c ]);
                                                        dates[ 3 ] = DateTime.Parse(NumberAndDate[ d ]);
                                                        dates[ 4 ] = DateTime.Parse(NumberAndDate[ e ]);
                                                        dates[ 5 ] = DateTime.Parse(NumberAndDate[ f ]);
                                                        dates[ 6 ] = DateTime.Parse(NumberAndDate[ g ]);
                                                        dates = dates.OrderBy(p => p).ToArray();
                                                        streamwriter.WriteLine($"{a}-{b}-{c}-{d}-{e}-{f}-{g} in {folders}, ==> {dates[ 0 ].ToShortDateString()}, {dates[ 1 ].ToShortDateString()}, {dates[ 2 ].ToShortDateString()}, {dates[ 3 ].ToShortDateString()}, {dates[ 4 ].ToShortDateString()}, {dates[ 5 ].ToShortDateString()}, {dates[ 6 ].ToShortDateString()}.");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            result.Add("Search successful!");

                        }
                    }
                    else if ( NumberAndDate.Count > 49 )
                    {
                        for ( int i = 50; i < NumberAndDate.Count; i++ )
                        {
                            result.Add($"Error, there might be a  mistake on {NumberAndDate.Values.ElementAt(i)}");
                        }
                    }
                }
            }
            catch ( Exception e )
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result.Count != 0 ? result : new List<string> { "Unknown error" };
        }

        private async void sixToolStripMenuItem1_Click( object sender, EventArgs e )//event raised when self search played(6) is presssed
        {
            try
            {
                //ask location to save file as
                SaveFileDialog savefiledialog = new SaveFileDialog();
                savefiledialog.DefaultExt = ".txt";
                savefiledialog.AddExtension = true;

                savefiledialog.Title = "Save file";
                savefiledialog.ShowDialog();

                string filename = savefiledialog.FileName;
                savefiledialog.Dispose();

                if ( !string.IsNullOrEmpty(filename))
                {
                    Notify("Task started...");

                    //search the numbers asynchronously
                    Task<List<string>> task = new Task<List<string>>(arg => { return SelfSearch_PlayedNumbersOnly(( string ) arg); }, filename);
                    task.Start();
                    await task;
                    if ( task.Result[ 0 ] == "Search successful!" )
                    {
                        Notify("Task completed.");
                    }
                    else
                    {
                        foreach ( string result in task.Result )
                        {
                            Notify(result);
                        }
                    }
                }
                else
                {
                    Notify("Please enter a valid file name.");
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private List<string> SelfSearch_PlayedNumbersOnly(string filename)//another self search (only numbers played) [6 numbers]
        {
            GetNumbersBetweenSpecifiedDates();
            List<string> result = new List<string>();
            try
            {
                if ( !string.IsNullOrEmpty(filename) )
                {
                    if ( NumberAndDate.Count == 49 )
                    {
                        using ( StreamWriter streamwriter = new StreamWriter(filename) )
                        {
                            streamwriter.WriteLine($"\nCombinations found.\n");
                            Dictionary<string, List<int>> dc = new Dictionary<string, List<int>>(date_AND_combination
                                .ToDictionary(x => x.Key, y => Array.ConvertAll(y.Value.ToArray(), arr => int.Parse(arr)).OrderBy(z => z).OrderBy(z => z).ToList())
                                .OrderBy(a => a.Value[ 0 ])
                                .ThenBy(b => b.Value[ 1 ])
                                .ThenBy(c => c.Value[ 2 ])
                                .ThenBy(d => d.Value[ 3 ])
                                .ThenBy(e => e.Value[ 4 ])
                                .ThenBy(f => f.Value[ 5 ])
                                .ThenBy(g => g.Value[ 6 ])
                                .ToDictionary(v => v.Key, w => w.Value.ToList())
                                );
                            foreach ( string date in dc.Keys )
                            {
                                List<int> comb = new List<int>(dc[ date ].ToArray());

                                DateTime dt = DateTime.Parse(date);

                                streamwriter.WriteLine($"{comb[ 1 ]}-{comb[ 2 ]}-{comb[ 3 ]}-{comb[ 4 ]}-{comb[ 5 ]}-{comb[ 6 ]}, (last played on {dt.Day} {ConvertToMonth(dt.Month)} {dt.Year}) ==> {NumberAndDate[ comb[ 1 ] ]}, {NumberAndDate[ comb[ 2 ] ]}, {NumberAndDate[ comb[ 3 ] ]}, {NumberAndDate[ comb[ 4 ] ]}, {NumberAndDate[ comb[ 5 ] ]}, {NumberAndDate[ comb[ 6 ] ]}.");
                            }



                            streamwriter.WriteLine();
                            streamwriter.WriteLine("Sorted");

                            foreach ( string date in dc.Keys )
                            {
                                List<int> comb = new List<int>(dc[ date ].ToArray());

                                DateTime dt = DateTime.Parse(date);
                                DateTime[] dates = { DateTime.Parse(NumberAndDate[ comb[ 1 ] ]), DateTime.Parse(NumberAndDate[ comb[ 2 ] ]), DateTime.Parse(NumberAndDate[ comb[ 3 ] ]), DateTime.Parse(NumberAndDate[ comb[ 4 ] ]), DateTime.Parse(NumberAndDate[ comb[ 5 ] ]), DateTime.Parse(NumberAndDate[ comb[ 6 ] ]) };
                                dates = dates.OrderBy(a => a).ToArray();

                                streamwriter.WriteLine($"{comb[ 1 ]}-{comb[ 2 ]}-{comb[ 3 ]}-{comb[ 4 ]}-{comb[ 5 ]}-{comb[ 6 ]}, (last played on {dt.Day} {ConvertToMonth(dt.Month)} {dt.Year}) ==> {dates[ 0 ].ToShortDateString()}, {dates[ 1 ].ToShortDateString()}, {dates[ 2 ].ToShortDateString()}, {dates[ 3 ].ToShortDateString()}, {dates[ 4 ].ToShortDateString()}, {dates[ 5 ].ToShortDateString()}.");

                            }

                            result.Add("Search successful!");
                        }
                    }

                    else if ( NumberAndDate.Count < 49 )
                    {
                        //get the numbers that were not found
                        int[] keys = NumberAndDate.Keys.ToArray();
                        int[] all_keys = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49 };

                        int[] diff_keys = all_keys.Except<int>(keys).ToArray();
                        using ( StreamWriter streamwriter = new StreamWriter(filename) )
                        {

                            streamwriter.WriteLine($"\nCombinations found.\n");

                            Dictionary<string, List<int>> dc = new Dictionary<string, List<int>>(date_AND_combination
                                .ToDictionary(x => x.Key, y => Array.ConvertAll(y.Value.ToArray(), arr => int.Parse(arr)).OrderBy(z => z).OrderBy(z => z).ToList())
                                .OrderBy(a => a.Value[ 0 ])
                                .ThenBy(b => b.Value[ 1 ])
                                .ThenBy(c => c.Value[ 2 ])
                                .ThenBy(d => d.Value[ 3 ])
                                .ThenBy(e => e.Value[ 4 ])
                                .ThenBy(f => f.Value[ 5 ])
                                .ThenBy(g => g.Value[ 6 ])
                                .ToDictionary(v => v.Key, w => w.Value.ToList())
                                );
                            foreach ( string date in dc.Keys )
                            {

                                List<int> comb = new List<int>(dc[ date ].ToArray());

                                if ( !diff_keys.Contains(comb[ 0 ]) || !diff_keys.Contains(comb[ 1 ]) || diff_keys.Contains(comb[ 2 ]) || diff_keys.Contains(comb[ 3 ]) || diff_keys.Contains(comb[ 4 ]) || diff_keys.Contains(comb[ 5 ]) || diff_keys.Contains(comb[ 6 ]) )
                                {
                                    DateTime dt = DateTime.Parse(date);

                                    streamwriter.WriteLine($"{comb[ 1 ]}-{comb[ 2 ]}-{comb[ 3 ]}-{comb[ 4 ]}-{comb[ 5 ]}-{comb[ 6 ]}, (last played on {dt.Day} {ConvertToMonth(dt.Month)} {dt.Year}) ==> {NumberAndDate[ comb[ 1 ] ]}, {NumberAndDate[ comb[ 2 ] ]}, {NumberAndDate[ comb[ 3 ] ]}, {NumberAndDate[ comb[ 4 ] ]}, {NumberAndDate[ comb[ 5 ] ]}, {NumberAndDate[ comb[ 6 ] ]}.");
                                }


                            }


                            streamwriter.WriteLine();
                            streamwriter.WriteLine("Sorted");
                            foreach ( string date in dc.Keys )
                            {
                                List<int> comb = new List<int>(dc[ date ].ToArray());
                                comb.Sort();

                                if ( !diff_keys.Contains(comb[ 0 ]) || !diff_keys.Contains(comb[ 1 ]) || diff_keys.Contains(comb[ 2 ]) || diff_keys.Contains(comb[ 3 ]) || diff_keys.Contains(comb[ 4 ]) || diff_keys.Contains(comb[ 5 ]) || diff_keys.Contains(comb[ 6 ]) )
                                {
                                    DateTime dt = DateTime.Parse(date);

                                    DateTime[] dates = { DateTime.Parse(NumberAndDate[ comb[ 1 ] ]), DateTime.Parse(NumberAndDate[ comb[ 2 ] ]), DateTime.Parse(NumberAndDate[ comb[ 3 ] ]), DateTime.Parse(NumberAndDate[ comb[ 4 ] ]), DateTime.Parse(NumberAndDate[ comb[ 5 ] ]), DateTime.Parse(NumberAndDate[ comb[ 6 ] ]) };
                                    dates = dates.OrderBy(a => a).ToArray();

                                    streamwriter.WriteLine($"{comb[ 1 ]}-{comb[ 2 ]}-{comb[ 3 ]}-{comb[ 4 ]}-{comb[ 5 ]}-{comb[ 6 ]}, (last played on {dt.Day} {ConvertToMonth(dt.Month)} {dt.Year}) ==> {dates[ 0 ].ToShortDateString()}, {dates[ 1 ].ToShortDateString()}, {dates[ 2 ].ToShortDateString()}, {dates[ 3 ].ToShortDateString()}, {dates[ 4 ].ToShortDateString()}, {dates[ 5 ].ToShortDateString()}.");
                                }
                            }

                            result.Add("Search successful!");

                        }
                    }
                    else if ( NumberAndDate.Count > 49 )
                    {
                        for ( int i = 50; i < NumberAndDate.Count; i++ )
                        {
                            result.Add($"Error, there might be a  mistake on {NumberAndDate.Values.ElementAt(i)}");
                        }
                    }
                }
            }
            catch ( Exception e )
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result.Count != 0 ? result : new List<string> { "Unknown error" };
        }

        private async void sevenToolStripMenuItem1_Click( object sender, EventArgs e )//event raised when self search played(7) is presssed
        {
            try
            {
                //ask location to save file as
                SaveFileDialog savefiledialog = new SaveFileDialog();
                savefiledialog.DefaultExt = ".txt";
                savefiledialog.AddExtension = true;

                savefiledialog.Title = "Save file";
                savefiledialog.ShowDialog();

                string filename = savefiledialog.FileName;
                savefiledialog.Dispose();

                if ( !string.IsNullOrEmpty(filename))
                {
                    Notify("Task started...");

                    //search the numbers asynchronously
                    Task<List<string>> task = new Task<List<string>>(arg => { return SelfSearch_PlayedNumbersOnly_7(( string ) arg); }, filename);
                    task.Start();
                    await task;
                    if ( task.Result[ 0 ] == "Search successful!" )
                    {
                        Notify("Task completed.");
                    }
                    else
                    {
                        foreach ( string result in task.Result )
                        {
                            Notify(result);
                        }
                    }
                }
                else
                {
                    Notify("Please enter a valid file name.");
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private List<string> SelfSearch_PlayedNumbersOnly_7(string filename)//another self search( only numbers played) [7 numbers]
        {
            GetNumbersBetweenSpecifiedDates();
            List<string> result = new List<string>();
            try
            {
                if ( !string.IsNullOrEmpty(filename) )
                {
                    if ( NumberAndDate.Count == 49 )//**********************************************************************************************************************
                    {
                        using ( StreamWriter streamwriter = new StreamWriter(filename) )
                        {
                            streamwriter.WriteLine($"\nCombinations found.\n");
                            Dictionary<string, List<int>> dc = new Dictionary<string, List<int>>(date_AND_combination
                                .ToDictionary(x => x.Key, y => Array.ConvertAll(y.Value.ToArray(), arr => int.Parse(arr)).OrderBy(z => z).OrderBy(z => z).ToList())
                                .OrderBy(a => a.Value[ 0 ])
                                .ThenBy(b => b.Value[ 1 ])
                                .ThenBy(c => c.Value[ 2 ])
                                .ThenBy(d => d.Value[ 3 ])
                                .ThenBy(e => e.Value[ 4 ])
                                .ThenBy(f => f.Value[ 5 ])
                                .ThenBy(g => g.Value[ 6 ])
                                .ToDictionary(v => v.Key, w => w.Value.ToList())
                                );
                            foreach ( string date in dc.Keys )
                            {
                                List<int> comb = new List<int>(dc[ date ].ToArray());

                                DateTime dt = DateTime.Parse(date);

                                streamwriter.WriteLine($"{CombinationFormatting(comb[ 0 ].ToString(), comb[ 1 ].ToString(), comb[ 2 ].ToString(), comb[ 3 ].ToString(), comb[ 4 ].ToString(), comb[ 5 ].ToString(), comb[ 6 ].ToString(), dc[ date ].IndexOf(int.Parse(date_AND_combination[ date ][ 6 ]))).Replace(" ", "")},(last played on {dt.Day} {ConvertToMonth(dt.Month)} {dt.Year}) ==> {NumberAndDate[ comb[ 0 ] ]}, {NumberAndDate[ comb[ 1 ] ]}, {NumberAndDate[ comb[ 2 ] ]}, {NumberAndDate[ comb[ 3 ] ]}, {NumberAndDate[ comb[ 4 ] ]}, {NumberAndDate[ comb[ 5 ] ]}, {NumberAndDate[ comb[ 6 ] ]}.");
                            }

                            streamwriter.WriteLine();
                            streamwriter.WriteLine("Sorted");

                            foreach ( string date in dc.Keys )
                            {
                                List<int> comb = new List<int>(dc[ date ].ToArray());

                                DateTime dt = DateTime.Parse(date);
                                DateTime[] dates = { DateTime.Parse(NumberAndDate[ comb[ 0 ] ]), DateTime.Parse(NumberAndDate[ comb[ 1 ] ]), DateTime.Parse(NumberAndDate[ comb[ 2 ] ]), DateTime.Parse(NumberAndDate[ comb[ 3 ] ]), DateTime.Parse(NumberAndDate[ comb[ 4 ] ]), DateTime.Parse(NumberAndDate[ comb[ 5 ] ]), DateTime.Parse(NumberAndDate[ comb[ 6 ] ]) };
                                dates = dates.OrderBy(a => a).ToArray();

                                streamwriter.WriteLine($"{CombinationFormatting(comb[ 0 ].ToString(), comb[ 1 ].ToString(), comb[ 2 ].ToString(), comb[ 3 ].ToString(), comb[ 4 ].ToString(), comb[ 5 ].ToString(), comb[ 6 ].ToString(), dc[ date ].IndexOf(int.Parse(date_AND_combination[ date ][ 6 ]))).Replace(" ", "")},(last played on {dt.Day} {ConvertToMonth(dt.Month)} {dt.Year}) ==> {dates[ 0 ].ToShortDateString()}, {dates[ 1 ].ToShortDateString()}, {dates[ 2 ].ToShortDateString()}, {dates[ 3 ].ToShortDateString()}, {dates[ 4 ].ToShortDateString()}, {dates[ 5 ].ToShortDateString()}, {dates[ 6 ].ToShortDateString()}.");

                            }

                            result.Add("Search successful!");
                        }
                    }

                    else if ( NumberAndDate.Count < 49 )//**********************************************************************************************************************
                    {
                        //get the numbers that were not found
                        int[] keys = NumberAndDate.Keys.ToArray();
                        int[] all_keys = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49 };

                        int[] diff_keys = all_keys.Except<int>(keys).ToArray();
                        using ( StreamWriter streamwriter = new StreamWriter(filename) )
                        {

                            streamwriter.WriteLine($"\nCombinations found.\n");
                            Dictionary<string, List<int>> dc = new Dictionary<string, List<int>>(date_AND_combination
                                .ToDictionary(x => x.Key, y => Array.ConvertAll(y.Value.ToArray(), arr => int.Parse(arr)).OrderBy(z => z).OrderBy(z => z).ToList())
                                .OrderBy(a => a.Value[ 0 ])
                                .ThenBy(b => b.Value[ 1 ])
                                .ThenBy(c => c.Value[ 2 ])
                                .ThenBy(d => d.Value[ 3 ])
                                .ThenBy(e => e.Value[ 4 ])
                                .ThenBy(f => f.Value[ 5 ])
                                .ThenBy(g => g.Value[ 6 ])
                                .ToDictionary(v => v.Key, w => w.Value.ToList())
                                );
                            foreach ( string date in dc.Keys )
                            {

                                List<int> comb = new List<int>(dc[ date ].ToArray());

                                if ( !diff_keys.Contains(comb[ 0 ]) || !diff_keys.Contains(comb[ 1 ]) || diff_keys.Contains(comb[ 2 ]) || diff_keys.Contains(comb[ 3 ]) || diff_keys.Contains(comb[ 4 ]) || diff_keys.Contains(comb[ 5 ]) || diff_keys.Contains(comb[ 6 ]) )
                                {
                                    DateTime dt = DateTime.Parse(date);

                                    streamwriter.WriteLine($"{CombinationFormatting(comb[ 0 ].ToString(), comb[ 1 ].ToString(), comb[ 2 ].ToString(), comb[ 3 ].ToString(), comb[ 4 ].ToString(), comb[ 5 ].ToString(), comb[ 6 ].ToString(), dc[ date ].IndexOf(int.Parse(date_AND_combination[ date ][ 6 ]))).Replace(" ", "")},(last played on {dt.Day} {ConvertToMonth(dt.Month)} {dt.Year}) ==> {NumberAndDate[ comb[ 0 ] ]}, {NumberAndDate[ comb[ 1 ] ]}, {NumberAndDate[ comb[ 2 ] ]}, {NumberAndDate[ comb[ 3 ] ]}, {NumberAndDate[ comb[ 4 ] ]}, {NumberAndDate[ comb[ 5 ] ]}, {NumberAndDate[ comb[ 6 ] ]}.");
                                }


                            }


                            streamwriter.WriteLine();
                            streamwriter.WriteLine("Sorted");
                            foreach ( string date in dc.Keys )
                            {
                                List<int> comb = new List<int>(dc[ date ].ToArray());
                                comb.Sort();

                                if ( !diff_keys.Contains(comb[ 0 ]) || !diff_keys.Contains(comb[ 1 ]) || diff_keys.Contains(comb[ 2 ]) || diff_keys.Contains(comb[ 3 ]) || diff_keys.Contains(comb[ 4 ]) || diff_keys.Contains(comb[ 5 ]) || diff_keys.Contains(comb[ 6 ]) )
                                {
                                    DateTime dt = DateTime.Parse(date);

                                    DateTime[] dates = { DateTime.Parse(NumberAndDate[ comb[ 0 ] ]), DateTime.Parse(NumberAndDate[ comb[ 1 ] ]), DateTime.Parse(NumberAndDate[ comb[ 2 ] ]), DateTime.Parse(NumberAndDate[ comb[ 3 ] ]), DateTime.Parse(NumberAndDate[ comb[ 4 ] ]), DateTime.Parse(NumberAndDate[ comb[ 5 ] ]), DateTime.Parse(NumberAndDate[ comb[ 6 ] ]) };
                                    dates = dates.OrderBy(a => a).ToArray();

                                    streamwriter.WriteLine($"{CombinationFormatting(comb[ 0 ].ToString(), comb[ 1 ].ToString(), comb[ 2 ].ToString(), comb[ 3 ].ToString(), comb[ 4 ].ToString(), comb[ 5 ].ToString(), comb[ 6 ].ToString(), dc[ date ].IndexOf(int.Parse(date_AND_combination[ date ][ 6 ]))).Replace(" ", "")},(last played on {dt.Day} {ConvertToMonth(dt.Month)} {dt.Year}) ==> {dates[ 0 ].ToShortDateString()}, {dates[ 1 ].ToShortDateString()}, {dates[ 2 ].ToShortDateString()}, {dates[ 3 ].ToShortDateString()}, {dates[ 4 ].ToShortDateString()}, {dates[ 5 ].ToShortDateString()}, {dates[ 6 ].ToShortDateString()}.");
                                }
                            }
                            result.Add("Search successful!");
                        }
                    }

                    else if ( NumberAndDate.Count > 49 )//**********************************************************************************************************************
                    {
                        for ( int i = 50; i < NumberAndDate.Count; i++ )
                        {
                            result.Add($"Error, there might be a  mistake on {NumberAndDate.Values.ElementAt(i)}");
                        }
                    }
                }
            }
            catch ( Exception e )
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result.Count != 0 ? result : new List<string> { "Unknown error" };
        }

        private void GetNumbersBetweenSpecifiedDates()//alters the NumberAndDate dict
        {
            try
            {
                //get from and to dates
                string from_date = toolStripTextBox1_Date_From.Text != "" ? toolStripTextBox1_Date_From.Text : null;
                string to_date = toolStripTextBox2_Date_To.Text != "" ? toolStripTextBox2_Date_To.Text : null;

                if ( from_date != null && to_date != null )
                {

                    DateTime from_datetime = DateTime.Parse(from_date);
                    DateTime to_datetime = DateTime.Parse(to_date);

                    if ( to_datetime > from_datetime )//only if to is bigger than from
                    {
                        Dictionary<string, List<string>> new_d_and_c = new Dictionary<string, List<string>>();
                        foreach ( string date in date_AND_combination.Keys )
                        {
                            if ( DateTime.Parse(date) >= from_datetime && DateTime.Parse(date) <= to_datetime )//only if date is between to and from dates
                            {
                                new_d_and_c.Add(date, date_AND_combination[ date ]);
                            }
                        }

                        //clear number and date dictionary (dictionary for self searching with possibly 1-49 numbers)
                        NumberAndDate.Clear();

                        foreach ( string key in new_d_and_c.Keys )//for each date
                        {
                            DateTime date_format = DateTime.Parse(key);

                            foreach ( string number in new_d_and_c[ key ] )//for each list<string> (which is the combination)
                            {
                                //add numbers and dates to dictionary for self searching
                                int num = Convert.ToInt32(number);

                                //if the number hasn't been added
                                if ( !NumberAndDate.ContainsKey(num) )
                                {
                                    NumberAndDate.Add(num, date_format.ToShortDateString());
                                }
                                else
                                {
                                    DateTime old_date = DateTime.Parse(NumberAndDate[ num ]);
                                    if ( old_date < date_format )
                                    {
                                        NumberAndDate[ num ] = date_format.ToShortDateString();//change to most current date
                                    }
                                }
                            }
                        }
                    }
                }
            }

            catch ( Exception e )
            {
                MessageBox.Show(e.Message, "Something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void lunchtimeToolStripMenuItem1_Click( object sender, EventArgs e )
        {
            state = "L";
            await GetAllResults("lunchtime");
            if ( success =="true" )
			{
                success = null;
                menuStrip1_Toolbar.BackColor = Color.SkyBlue;
                //disable all buttons
                EnableButtons(false);
            }

        }
        private async void teatimeToolStripMenuItem1_Click( object sender, EventArgs e )
        {
            state = "T";
            await GetAllResults("teatime");
            if ( success == "true" )
			{
                success = null;
                menuStrip1_Toolbar.BackColor = Color.Tomato;
                //disable all buttons
                EnableButtons(false);
            }

        }
        private void toolStripMenuItem5_UpdateNums_Click( object sender, EventArgs e )
        {
            GetLatestResults();
        }

        //tools events
		private void readerToolStripMenuItem_Click( object sender, EventArgs e )
		{
            Form read_aloud = new ReadAloud();
            read_aloud.ShowDialog();//show as dialog so user cannot interact with main form
        }
		private void searchToolStripMenuItem1_Click( object sender, EventArgs e )
		{
            Form search_form = new Search();
            search_form.Show();//show as dialog so user cannot interact with main form
        }
		private void sToolStripMenuItem_Click( object sender, EventArgs e )
		{
            Form statistics_form = new Statistics();
            statistics_form.Show();
        }
		private void dreamGuideToolStripMenuItem_Click( object sender, EventArgs e )
		{
            Form dreamguide_form = new DreamGuide();
            dreamguide_form.Show();
        }
        //*************************************************************************************************************************************************************************

        private  void AddNumbers()
        {
            //array with all the textboxes
            TextBox[] AllTextBoxes = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7 };

            try
            {
                if ( state == null )
                {
                    state = "N/A";
                }

                List<int> CurrentNumbers = new List<int>();

                bool CanAdd = true;

                foreach ( TextBox text_box in AllTextBoxes )
                {
                    CurrentNumbers.Add(int.Parse(text_box.Text));
                }
                CurrentNumbers.Sort();
                string NumbersString = $"{CurrentNumbers[ 0 ]}-{CurrentNumbers[ 1 ]}-{CurrentNumbers[ 2 ]}-{CurrentNumbers[ 3 ]}-{CurrentNumbers[ 4 ]}-{CurrentNumbers[ 5 ]}-{CurrentNumbers[ 6 ]}";

                //check if numbers already exist
                if ( !CombTypedAsStrings.Contains(NumbersString) )
                {
                    CombTypedAsStrings.Add(NumbersString);
                }
                else
                {
                    DialogResult dr = MessageBox.Show($"Numbers on [{dateTimePicker1_Date.Value.Day} {ConvertToMonth( dateTimePicker1_Date.Value.Month)} {dateTimePicker1_Date.Value.Year}] were previously added. Do you want to add them again?", "Duplicate Numbers", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if ( dr == DialogResult.No )
                    {
                        CanAdd = false;
                    }
                }

                if ( CanAdd )
                {
                    
                    //handle squared number
                    WhereIsSquaredNumber = CurrentNumbers.IndexOf(Convert.ToInt32(textBox7.Text));

                    //add date to items
                    ListViewItem AddedItem = listView1_Date_and_Numbers.Items.Add(dateTimePicker1_Date.Value.ToShortDateString());
                    MostRecentDate = dateTimePicker1_Date.Value > MostRecentDate ? dateTimePicker1_Date.Value : MostRecentDate;
                    //add item to group
                    string NewGroupHeader = $"{ ConvertToMonth(dateTimePicker1_Date.Value.Month)}, {dateTimePicker1_Date.Value.Year}";

                    AddedItem.Group = listView1_Date_and_Numbers.Groups[NewGroupHeader] == null? listView1_Date_and_Numbers.Groups.Add(NewGroupHeader, NewGroupHeader): listView1_Date_and_Numbers.Groups[ NewGroupHeader ];
                    
                    AddedItem.Group.Tag = dateTimePicker1_Date.Value.Month;

                    AddedItem.Name = NewGroupHeader;

                    //keep track of the added numbers bonus
                    AddedItem.ToolTipText = Convert.ToInt32(textBox7.Text) < 10 ? "0" + textBox7.Text : textBox7.Text;

                    //add combination to subitems
                    AddedItem.SubItems.Add(CombinationFormatting($"{CurrentNumbers[ 0 ]}", $"{CurrentNumbers[ 1 ]}", $"{CurrentNumbers[ 2 ]}", $"{CurrentNumbers[ 3 ]}", $"{CurrentNumbers[ 4 ]}", $"{CurrentNumbers[ 5 ]}", $"{CurrentNumbers[ 6 ]}", WhereIsSquaredNumber));

                    //add state to subitems
                    AddedItem.SubItems.Add(state.ToUpper());

					//add index in 85m
					//List<int> l = Array.ConvertAll(CurrentNumbers.ToArray(), a => int.Parse(a)).ToList();
					//l.Sort();
					//Task<int> task = new Task<int>(arg => { return Index_For_One_Comb(( string ) arg); }, $"{CurrentNumbers[ 0 ]}-{CurrentNumbers[ 1 ]}-{CurrentNumbers[ 2 ]}-{CurrentNumbers[ 3 ]}-{CurrentNumbers[ 4 ]}-{CurrentNumbers[ 5 ]}-{CurrentNumbers[ 6 ]}");
					//task.Start();
					//await task;
					//int ind = task.Result;
					int ind = Index_For_One_Comb($"{CurrentNumbers[ 0 ]}-{CurrentNumbers[ 1 ]}-{CurrentNumbers[ 2 ]}-{CurrentNumbers[ 3 ]}-{CurrentNumbers[ 4 ]}-{CurrentNumbers[ 5 ]}-{CurrentNumbers[ 6 ]}");

					//add tag
					AddedItem.Tag = ind;
                    
                    try
					{
                        Indexes_85m.Add($"{CurrentNumbers[ 0 ]}-{CurrentNumbers[ 1 ]}-{CurrentNumbers[ 2 ]}-{CurrentNumbers[ 3 ]}-{CurrentNumbers[ 4 ]}-{CurrentNumbers[ 5 ]}-{CurrentNumbers[ 6 ]}", ind);
					}
					catch { }

                    //add items to unsorted list
                    string p_30 = ind % 30 == 0 ? "30" : ( ind % 30 ).ToString();
                    ListViewItem UnsortedItem = listView1_Unsorted.Items.Add($"{listView1_Unsorted.Items.Count + 1}");//count
                    one_thirty++;
                    one_thirty = one_thirty <= 30 ? one_thirty: 1;
                    UnsortedItem.SubItems.Add(one_thirty.ToString());//1-30 count
                    UnsortedItem.SubItems.Add(AddedItem.Text);//date
                    UnsortedItem.SubItems.Add(AddedItem.SubItems[ 1 ].Text);//combination
                    UnsortedItem.SubItems.Add(p_30);//1-30 in 85m
                    UnsortedItem.SubItems.Add($"{ind}");//index
                    UnsortedItem.SubItems.Add(state.ToUpper());//state
                    UnsortedItem.Tag = AddedItem.Tag;
                    UnsortedItem.ToolTipText = dateTimePicker1_Date.Value.ToShortDateString();
                    //AreUnsortedNumbersSorted = false;

                    //add date and combination for saving data to json RED
                    date_AND_combination.Add(ReturnCorrectComb(AddedItem.SubItems[ 2 ].Text, dateTimePicker1_Date.Value.ToShortDateString()), new List<string> { AllTextBoxes[ 0 ].Text, AllTextBoxes[ 1 ].Text, AllTextBoxes[ 2 ].Text, AllTextBoxes[ 3 ].Text, AllTextBoxes[ 4 ].Text, AllTextBoxes[ 5 ].Text, AllTextBoxes[ 6 ].Text, });

                    //dateTimePicker1_Date.Value = dateTimePicker1_Date.Value.AddDays(1);
                }
            }

            catch ( Exception e )
            {
                MessageBox.Show(e.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
 
            foreach ( TextBox box in AllTextBoxes )
            {
                box.Clear();
            }
            textBox1.Select();
            button1_Add.Enabled = false;
        }

        private void NewNumberEdit()
        {
            try
            {
                //edit the combination
                if ( !string.IsNullOrEmpty( toolStripTextBox1_From.Text) && !string.IsNullOrEmpty(toolStripTextBox2_To.Text) && listView1_Date_and_Numbers.SelectedItems.Count > 0 )
                {

                    string required_date = ReturnCorrectComb(listView1_Date_and_Numbers.SelectedItems[ 0 ].SubItems[ 2 ].Text, listView1_Date_and_Numbers.SelectedItems[ 0 ].Text);

                    List<string> nums = date_AND_combination[ required_date ];

                    List<int> listofoldnums = new List<int> { Convert.ToInt32(nums[ 0 ]), Convert.ToInt32(nums[ 1 ]), Convert.ToInt32(nums[ 2 ]), Convert.ToInt32(nums[ 3 ]), Convert.ToInt32(nums[ 4 ]), Convert.ToInt32(nums[ 5 ]), Convert.ToInt32(nums[ 6 ]) };
                    listofoldnums.Sort();
                    string old_string_combination = $"{listofoldnums[ 0 ]}-{listofoldnums[ 1 ]}-{listofoldnums[ 2 ]}-{listofoldnums[ 3 ]}-{listofoldnums[ 4 ]}-{listofoldnums[ 5 ]}-{listofoldnums[ 6 ]}";

                    int index = nums.IndexOf(toolStripTextBox1_From.Text); //get index of number to change

                    date_AND_combination[ required_date ][ index ] = toolStripTextBox2_To.Text; // change number

                    List<int> listofnums = new List<int> { Convert.ToInt32(nums[ 0 ]), Convert.ToInt32(nums[ 1 ]), Convert.ToInt32(nums[ 2 ]), Convert.ToInt32(nums[ 3 ]), Convert.ToInt32(nums[ 4 ]), Convert.ToInt32(nums[ 5 ]), Convert.ToInt32(nums[ 6 ]) };
                    listofnums.Sort();


                    Indexes_85m.Remove(old_string_combination);
                    CombTypedAsStrings.Remove(old_string_combination);

                    string new_string_combination = $"{listofnums[ 0 ]}-{listofnums[ 1 ]}-{listofnums[ 2 ]}-{listofnums[ 3 ]}-{listofnums[ 4 ]}-{listofnums[ 5 ]}-{listofnums[ 6 ]}";

                    CombTypedAsStrings.Add(new_string_combination);

					List<string> new_nums = new List<string>
					{
						listofnums[ 0 ].ToString(),
						listofnums[ 1 ].ToString(),
						listofnums[ 2 ].ToString(),
						listofnums[ 3 ].ToString(),
						listofnums[ 4 ].ToString(),
						listofnums[ 5 ].ToString(),
						listofnums[ 6 ].ToString()
					};
					int bonus_index = new_nums.IndexOf(date_AND_combination[ required_date ][ 6 ]);

                    listView1_Date_and_Numbers.SelectedItems[ 0 ].SubItems[ 1 ].Text = CombinationFormatting(Convert.ToInt32(new_nums[ 0 ]) >= 10 ? new_nums[ 0 ] : "0" + new_nums[ 0 ], Convert.ToInt32(new_nums[ 1 ]) >= 10 ? new_nums[ 1 ] : "0" + new_nums[ 1 ], Convert.ToInt32(new_nums[ 2 ]) >= 10 ? new_nums[ 2 ] : "0" + new_nums[ 2 ], Convert.ToInt32(new_nums[ 3 ]) >= 10 ? new_nums[ 3 ] : "0" + new_nums[ 3 ], Convert.ToInt32(new_nums[ 4 ]) >= 10 ? new_nums[ 4 ] : "0" + new_nums[ 4 ], Convert.ToInt32(new_nums[ 5 ]) >= 10 ? new_nums[ 5 ] : "0" + new_nums[ 5 ], Convert.ToInt32(new_nums[ 6 ]) >= 10 ? new_nums[ 6 ] : "0" + new_nums[ 6 ], bonus_index);

                    for ( int i = 0; i < new_nums.Count; i++ )
                    {
                        if ( Convert.ToInt32(new_nums[ i ]) < 10 )
                        {
                            new_nums[ i ] = "0" + new_nums[ i ];
                        }

                    }
                    listView1_Date_and_Numbers.SelectedItems[ 0 ].Tag = $"{new_nums[ 0 ]}{new_nums[ 1 ]}{new_nums[ 2 ]}{new_nums[ 3 ]}{new_nums[ 4 ]}{new_nums[ 5 ]}{new_nums[ 6 ]}";
                    //AreNumbersSorted = false;

                    if ( Convert.ToInt32(toolStripTextBox1_From.Text) == 0 || Convert.ToInt32(toolStripTextBox1_From.Text) >= 50 )
                    {
                        NumberAndDate.Remove(Convert.ToInt32(toolStripTextBox1_From.Text));
                    }

                    toolStripTextBox2_To.Text = null;
                    toolStripTextBox1_From.Text = null;

                }
                else
                {
                    MessageBox.Show("Make sure a row is selected and you entered a number in both boxes", "Could'nt edit number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch ( Exception e )
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EditDate()
        {
            try
            {


                //edit the date
                if ( listView1_Date_and_Numbers.SelectedItems.Count > 0 )
                {
                    if ( listView1_Date_and_Numbers.SelectedItems[ 0 ].Text != string.Empty )
                    {
                        //ask permission to edit
                        DialogResult msg = MessageBox.Show("Are you sure you want to overwrite the date?", "Alert!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if ( msg == DialogResult.Yes )
                        {

                            //edit the save file
                            string required_date = ReturnCorrectComb(listView1_Date_and_Numbers.SelectedItems[ 0 ].SubItems[ 2 ].Text, listView1_Date_and_Numbers.SelectedItems[ 0 ].Text);
                            if ( date_AND_combination.ContainsKey(required_date) && !date_AND_combination.ContainsKey(ReturnCorrectComb(listView1_Date_and_Numbers.SelectedItems[ 0 ].SubItems[ 2 ].Text, dateTimePicker1_Date.Value.ToShortDateString())) )
                            {
                                List<string> nums = date_AND_combination[ required_date ]; //store nums for the date we are changing
                                date_AND_combination.Remove(required_date); //remove date and combination from dictionary

                                date_AND_combination.Add(ReturnCorrectComb(listView1_Date_and_Numbers.SelectedItems[ 0 ].SubItems[ 2 ].Text, dateTimePicker1_Date.Value.ToShortDateString()), nums); //add new date with combination

                                listView1_Date_and_Numbers.SelectedItems[ 0 ].Text =  dateTimePicker1_Date.Value.ToShortDateString(); //change date in listview

                                foreach ( string number in nums )
                                {
                                    int int_num = Convert.ToInt32(number);
                                    // add numbers and dates to dictionary for self searching

                                    if ( NumberAndDate.ContainsKey(int_num) )
                                    {
                                        string[] date = NumberAndDate[ int_num ].Split('/');
                                        DateTime old_date = DateTime.Parse($"{date[ 1 ]}/{date[ 0 ]}/{date[ 2 ]}");
                                        DateTime new_date = dateTimePicker1_Date.Value;

                                        if ( old_date < new_date )
                                        {
                                            NumberAndDate[ int_num ] = listView1_Date_and_Numbers.SelectedItems[ 0 ].Text;
                                        }
                                    }
                                }

                                string NewGroupHeader = $"{ ConvertToMonth(dateTimePicker1_Date.Value.Month)}, {dateTimePicker1_Date.Value.Year}";
                                listView1_Date_and_Numbers.SelectedItems[ 0 ].Name = NewGroupHeader;

                                SortNumbers(0);
                                
                                //Save(true);
                                //Deserialize();
                            }
                        }
                    }
                }
            }
            catch ( Exception e )
            {
                MessageBox.Show(e.Message, "Edit Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DeleteEntry()
        {
            try
            {
                if ( listView1_Date_and_Numbers.SelectedItems.Count > 0 )
                {

                    //remove in dictionary
                    string required_date = ReturnCorrectComb(listView1_Date_and_Numbers.SelectedItems[ 0 ].SubItems[ 2 ].Text, listView1_Date_and_Numbers.SelectedItems[ 0 ].Text);

                    List<string> Numbers = date_AND_combination[ required_date ];
                    Numbers.Sort();
                    string NumberAsString = $"{Numbers[ 0 ]}-{Numbers[ 1 ]}-{Numbers[ 2 ]}-{Numbers[ 3 ]}-{Numbers[ 4 ]}-{Numbers[ 5 ]}-{Numbers[ 6 ]}";
                    CombTypedAsStrings.Remove(NumberAsString);
                    if ( Indexes_85m.Count > 0 )
                    {
                        Indexes_85m.Remove(NumberAsString);

                    }

                    date_AND_combination.Remove(required_date);
                    listView1_Date_and_Numbers.SelectedItems[ 0 ].Remove();
                }
            }
            catch ( Exception e )
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SortNumbers( int ColunmIndex)
        {

            //sort by dates
            if ( ColunmIndex == 0 )
            {
                try
                {

                    if ( listView1_Date_and_Numbers.Items.Count >= 2)
                    {
                        //sort items
                        ListViewItem[] SortedItems = listView1_Date_and_Numbers.Items.Cast<ListViewItem>().OrderBy(x => x.Text.Replace("/", "")).ToArray();

                        listView1_Date_and_Numbers.Items.Clear();
                        listView1_Date_and_Numbers.Items.AddRange(SortedItems);
                        listView1_Date_and_Numbers.Groups.Clear();

                        //assign item to new groups
                        foreach ( ListViewItem item in listView1_Date_and_Numbers.Items )
                        {
                            string NewGroupHeader = item.Name;
                            bool GroupFound = false;

                            foreach ( ListViewGroup group in listView1_Date_and_Numbers.Groups )
                            {
                                //if group exists
                                if ( group.Header == NewGroupHeader )
                                {
                                    item.Group = group;
                                    GroupFound = true;
                                    break;
                                }
                            }

                            //if group doesnt exist create new group
                            if ( !GroupFound )
                            {
                                item.Group = listView1_Date_and_Numbers.Groups.Add(NewGroupHeader, NewGroupHeader);
                                item.Group.Tag = ConvertToMonth(item.Group.Name.Substring(0, item.Group.Name.IndexOf(',')));
                            }
                        }

                        //sort the groups
                        ListViewGroup[] lvg = listView1_Date_and_Numbers.Groups.Cast<ListViewGroup>().OrderBy(x => x.Name.Substring(x.Name.IndexOf(',') + 1)).ThenBy(x => x.Tag).ToArray();
                        listView1_Date_and_Numbers.Groups.Clear();
                        listView1_Date_and_Numbers.Groups.AddRange(lvg);

                        //sort the dictionary for saving data
                        date_AND_combination = date_AND_combination.OrderBy(x => DateTime.Parse(x.Key)).ToDictionary(y => y.Key, z => z.Value);
                        
                    }
                    else
                    {
                        MessageBox.Show("There is nothing to sort.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch ( Exception b )
                {
                    MessageBox.Show(b.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            //sort by combination
            else if ( ColunmIndex == 1 )
            {
                try
                {
                    if ( listView1_Date_and_Numbers.Items.Count >= 2 )
                    {

                        //sort items
                        ListViewItem[] SortedItems = listView1_Date_and_Numbers.Items.Cast<ListViewItem>().OrderBy(x => x.Tag).ToArray();

                        listView1_Date_and_Numbers.Items.Clear();
                        listView1_Date_and_Numbers.Items.AddRange(SortedItems);
                        listView1_Date_and_Numbers.Groups.Clear();

                        //assign item to new groups
                        foreach ( ListViewItem item in listView1_Date_and_Numbers.Items )
                        {
                            string NewGroupHeader = item.Name;
                            bool GroupFound = false;

                            foreach ( ListViewGroup group in listView1_Date_and_Numbers.Groups )
                            {
                                //if group exists
                                if ( group.Header == NewGroupHeader )
                                {
                                    item.Group = group;
                                    GroupFound = true;
                                    break;
                                }
                            }

                            //if group doesnt exist create new group
                            if ( !GroupFound )
                            {
                                item.Group = listView1_Date_and_Numbers.Groups.Add(NewGroupHeader, NewGroupHeader);
                                item.Group.Tag = ConvertToMonth(item.Group.Name.Substring(0, item.Group.Name.IndexOf(',')));
                            }
                        }

                        //sort the groups
                        ListViewGroup[] lvg = listView1_Date_and_Numbers.Groups.Cast<ListViewGroup>().OrderBy(x => x.Name.Substring(x.Name.IndexOf(',') + 1)).ThenBy(x => x.Tag).ToArray();
                        listView1_Date_and_Numbers.Groups.Clear();
                        listView1_Date_and_Numbers.Groups.AddRange(lvg);

                       
                    }
                    else
                    {
                        MessageBox.Show("There is nothing to sort.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch ( Exception b )
                {
                    MessageBox.Show(b.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Refresh_Unsorted_Listview()
		{
            try
            {
                //if ( AreUnsortedNumbersSorted )
                //{
                    //unsort items of unsorted listview
                    ListViewItem[] SortedItems = listView1_Unsorted.Items.Cast<ListViewItem>().OrderBy(x => DateTime.Parse(x.ToolTipText)).ThenBy(y => ( int ) char.Parse(y.SubItems[ 6 ].Text)).ToArray();
                    int count = 0;
                    SortedItems.ToList().ForEach(x => x.Text = ( ++count ).ToString());
                    listView1_Unsorted.Items.Clear();
                    listView1_Unsorted.Items.AddRange(SortedItems);
                refreshToolStripMenuItem.Enabled = false;
                sortNumbersToolStripMenuItem.Enabled = true;
                //}

                //AreUnsortedNumbersSorted = false;
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //save to file of 1-43
        private List<string> TypeRangeofWhat( int which_number )
        {
            if ( Indexes_85m.Count != CombTypedAsStrings.Count )
            {
                FindIndexesin85m();
            }
            List<string> result = new List<string>();

            int count = 0;
            int One_30 = 0;
            string[] dict_ones = date_AND_combination.Keys.Where(a => date_AND_combination[ a ].Contains(which_number.ToString())).ToArray();
            foreach ( string key in dict_ones )
            {
				//get combinations as string and store in int[]

				List<int> ThoseNumbers = new List<int>();
				foreach ( string item in date_AND_combination[ key ] )
				{
					ThoseNumbers.Add(Convert.ToInt32(item));
				}

				ThoseNumbers.Sort();
				//if ( ThoseNumbers[ 0 ] != which_number )
				//{
				//	continue;
				//}

				count++;
                One_30++;
                One_30 = One_30 <= 30 ? One_30 : 1;

                string count_string = count.ToString();
                int string_len = count_string.Length;
				for ( int i = 0; i < dict_ones.Length.ToString().Length - string_len; i++ )
				{
                    count_string = count_string + " ";
				}

                string thoseNumbersString = CombinationFormatting(ThoseNumbers[ 0 ].ToString(), ThoseNumbers[ 1 ].ToString(), ThoseNumbers[ 2 ].ToString(), ThoseNumbers[ 3 ].ToString(), ThoseNumbers[ 4 ].ToString(), ThoseNumbers[ 5 ].ToString(), ThoseNumbers[ 6 ].ToString(), ThoseNumbers.IndexOf(Convert.ToInt32(date_AND_combination[ key ][ 6 ]))).Replace(" ", "");
                int thoseNumbersString_length = thoseNumbersString.Length;

                for ( int i = 0; i < 22 - thoseNumbersString_length; i++)
				{
                    thoseNumbersString = thoseNumbersString + " ";
				}

                string comb = $"{ThoseNumbers[ 0 ]}-{ThoseNumbers[ 1 ]}-{ThoseNumbers[ 2 ]}-{ThoseNumbers[ 3 ]}-{ThoseNumbers[ 4 ]}-{ThoseNumbers[ 5 ]}-{ThoseNumbers[ 6 ]}";

                //reformat the date
                DateTime dates = DateTime.Parse(key);
                string date = $"{dates.Day}/{dates.Month}/{dates.Year}";
                int date_length = date.Length;
				for ( int j = 0; j < 10 - date_length; j++ )
				{
                    date = date + " ";
				}

                string State1 = key.Contains(" ") ? "T" : "L";
                int c_in_85m = Indexes_85m[ comb ] % 30 == 0 ? 30 : Indexes_85m[ comb ] % 30;
                string index_in85m = Indexes_85m[ comb ].ToString();
                int length = index_in85m.Length;

                for ( int k = 0; k < 8 - length; k++ )
				{
                    index_in85m = index_in85m + " ";
				}

                result.Add($"{count_string}\t{One_30}\t{date}\t{thoseNumbersString}\t{c_in_85m}\t{index_in85m}\t{State1}");
                if(One_30 == 30 )
				{
                    result.Add("");
				}
                
            }
            return result;
        }

        //save in order by combination
        private List<string> GroupingOrder( int which_number )
        {
            List<string> result = new List<string>();

            //dict with sorted comb of (selected number eg 1)
            Dictionary<string, List<int>> Sorted_Nums = new Dictionary<string, List<int>>(date_AND_combination.Where(a => a.Value.Contains(which_number.ToString())).ToDictionary(a => a.Key, b => Array.ConvertAll(b.Value.ToArray(), c => int.Parse(c)).OrderBy(d => d).ToList()));//.Where(x => x.Value[ 0 ] == which_number).ToDictionary(e=>e.Key,f=>f.Value);
            
            //sort the dictionary
            //Sorted_Nums = Sorted_Nums.OrderBy(x => x.Value[ 0 ]).ThenBy(x => x.Value[ 1 ]).ThenBy(x => x.Value[ 2 ]).ThenBy(x => x.Value[ 3 ]).ThenBy(x => x.Value[ 4 ]).ThenBy(x => x.Value[ 5 ]).ThenBy(x => x.Value[ 6 ]).ToDictionary(x => x.Key, y => y.Value);
            //Sorted_Nums = Sorted_Nums.Where(x => x.Value[ 0 ] == which_number).ToDictionary(y => y.Key, z => z.Value);

            int count = 0;
            int c_count = 0;

            //calculate results
            foreach ( string key in Sorted_Nums.Keys )
            {

                //get combinations as string
                List<int> ThoseNumbers = new List<int>(Sorted_Nums[key]);
				//foreach ( int item in Sorted_Nums[ key ] )
				//{
				//	ThoseNumbers.Add(item);
				//}

				//ThoseNumbers.Sort();
                //if ( ThoseNumbers[ 0 ] != which_number )
                //{
                //    continue;
                //}
                count++;
                c_count++;
                c_count = c_count <= 30 ? c_count : 1;

                string count_string = count.ToString();
                int string_len = count_string.Length;
                for ( int i = 0; i < Sorted_Nums.Keys.Count.ToString().Length - string_len; i++ )
                {
                    count_string = count_string+ " ";
                }

                string thoseNumbersString = CombinationFormatting(ThoseNumbers[ 0 ].ToString(), ThoseNumbers[ 1 ].ToString(), ThoseNumbers[ 2 ].ToString(), ThoseNumbers[ 3 ].ToString(), ThoseNumbers[ 4 ].ToString(), ThoseNumbers[ 5 ].ToString(), ThoseNumbers[ 6 ].ToString(), ThoseNumbers.IndexOf(Convert.ToInt32(date_AND_combination[ key ][ 6 ]))).Replace(" ","");
                int thoseNumbersString_length = thoseNumbersString.Length;
                for ( int i = 0; i < 22-thoseNumbersString_length; i++ )
				{
                    thoseNumbersString = thoseNumbersString + " ";
				}

                //reformat the date
                DateTime dates = DateTime.Parse(key);
                string date = $"{dates.Day}/{dates.Month}/{dates.Year}";
                int date_length = date.Length;
				for ( int j = 0; j < 10 - date_length; j++ )
				{
                    date = date + " ";
				}

                string State1 = key.Contains(" ") ? "T" : "L";
                string comb = $"{ThoseNumbers[ 0 ]}-{ThoseNumbers[ 1 ]}-{ThoseNumbers[ 2 ]}-{ThoseNumbers[ 3 ]}-{ThoseNumbers[ 4 ]}-{ThoseNumbers[ 5 ]}-{ThoseNumbers[ 6 ]}";

                int c_30 = Indexes_85m[ comb ] % 30 == 0?30: Indexes_85m[ comb ]%30;
                string index_in85m = Indexes_85m[ comb ].ToString();
                int index_in85m_length = index_in85m.Length;
                for ( int k = 0; k < 8 - index_in85m_length; k++ )
                {
                    index_in85m = index_in85m + " ";
                }

                result.Add($"{count_string}\t{c_count}\t{date}\t{thoseNumbersString}\t{c_30}\t{index_in85m}\t{State1}");
                if(c_count == 30 )
				{
                    result.Add("");
				}
            }

            return result;
        }

        private List<string> AdjacentDates( int which_number )
        {
            List<string> result = new List<string>();

			if ( Indexes_85m.Count != CombTypedAsStrings.Count )
			{
				FindIndexesin85m();
			}

            //dict with sorted comb of (selected number eg 1)
            Dictionary<string, List<int>> AllOnes_Sorted = new Dictionary<string, List<int>>(date_AND_combination.Where(a => a.Value.Contains(which_number.ToString())).ToDictionary(a => a.Key, b => Array.ConvertAll(b.Value.ToArray(), c => int.Parse(c)).OrderBy(d => d).ToList()));//.Where(e=>e.Value[0] == which_number).ToDictionary(e => e.Key, f => f.Value);


            //remove all combs of 1 that are not adjacent
            for ( int i = 0; i < AllOnes_Sorted.Keys.Count; i++ )
            {
                string now_date = AllOnes_Sorted.Keys.ElementAt(i);
                string tomorrow_date = null;
                string yesterday_date = null;

                string days = null;
                int days_between_n_and_t = 0;
                int days_between_n_and_y = 0;

                try
                {
                    tomorrow_date = AllOnes_Sorted.Keys.ElementAt(i + 1);
                }
                catch
                {
                    tomorrow_date = now_date;
                }
                try
                {
                    yesterday_date = AllOnes_Sorted.Keys.ElementAt(i - 1);
                }
                catch
                {
                    yesterday_date = now_date;
                }

                TimeSpan days_between_today_and_tomorrow = DateTime.Parse(now_date) - DateTime.Parse(tomorrow_date);

                TimeSpanConverter tmc = new TimeSpanConverter();
                days = ( string ) tmc.ConvertTo(days_between_today_and_tomorrow, typeof(string));
                try
                {
                    days = days.Substring(0, days.IndexOf('.'));

                }
                catch
                {
                    days = days.Substring(0, days.IndexOf(':'));

                }
                days_between_n_and_t = Math.Abs(Convert.ToInt32(days));

                //*****************************************************************
                TimeSpan days_between_today_and_yesterday = DateTime.Parse(now_date) - DateTime.Parse(yesterday_date);
                string days_yester = ( string ) tmc.ConvertTo(days_between_today_and_yesterday, typeof(string));
                try
                {
                    days_yester = days_yester.Substring(0, days_yester.IndexOf('.'));

                }
                catch
                {
                    days_yester = days_yester.Substring(0, days_yester.IndexOf(':'));

                }
                days_between_n_and_y = Math.Abs(Convert.ToInt32(days_yester));

                if ( days_between_n_and_t != 1 && days_between_n_and_y != 1 )
                {
                    AllOnes_Sorted.Remove(now_date);
                    i -= 1;
                }
            }

            int count = 0;
            int count_1_30 = 0;
            for ( int i = 0; i < AllOnes_Sorted.Keys.Count; i++ )
            {

                int numbering_as_ones_indisorder = 0;
                string current_date = AllOnes_Sorted.Keys.ElementAt(i);
                string next_date = null;
                string previous_date = null;

                try
                {
                    next_date = AllOnes_Sorted.Keys.ElementAt(i + 1);
                }
                catch
                {
                    next_date = AllOnes_Sorted.Keys.ElementAt(i);
                }
                try
                {
                    previous_date = AllOnes_Sorted.Keys.ElementAt(i - 1);
                }
                catch
                {
                    previous_date = AllOnes_Sorted.Keys.ElementAt(i);
                }
                string days = null;
                int actual_days_between = 0;

                TimeSpan days_between = DateTime.Parse(current_date) - DateTime.Parse(next_date);

                TimeSpanConverter tmc = new TimeSpanConverter();
                days = ( string ) tmc.ConvertTo(days_between, typeof(string));
                try
                {
                    days = days.Substring(0, days.IndexOf('.'));

                }
                catch
                {
                    days = days.Substring(0, days.IndexOf(':'));

                }
                actual_days_between = Math.Abs(Convert.ToInt32(days));

                //*********************************************************
                string daysb = null;
                int actual_days_between_back = 0;
                TimeSpan days_between_back = DateTime.Parse(current_date) - DateTime.Parse(previous_date);

                TimeSpanConverter tmcb = new TimeSpanConverter();
                daysb = ( string ) tmcb.ConvertTo(days_between_back, typeof(string));
                try
                {
                    daysb = daysb.Substring(0, daysb.IndexOf('.'));

                }
                catch
                {
                    daysb = daysb.Substring(0, daysb.IndexOf(':'));
                }
                actual_days_between_back = Math.Abs(Convert.ToInt32(daysb));

                if ( actual_days_between == 1 || actual_days_between_back == 1 )
                {
                    //FIGURE OUT NUMBERING
                    //as typed
                    foreach ( string k in date_AND_combination.Keys )
                    {
                        numbering_as_ones_indisorder++;
                        if ( date_AND_combination[ k ].Contains(which_number.ToString()) )
                        {
                            if ( k == current_date )
                            {
                                break;
                            }
                        }
                    }

                    //reformat the date
                    //string date = current_date.Substring(0, current_date.IndexOf(' '));
                    DateTime dates = DateTime.Parse(current_date);
                    string date = $"{dates.Day}/{dates.Month}/{dates.Year}";

                    List<int> comb = new List<int> { Convert.ToInt32(AllOnes_Sorted[ current_date ][ 0 ]), Convert.ToInt32(AllOnes_Sorted[ current_date ][ 1 ]), Convert.ToInt32(AllOnes_Sorted[ current_date ][ 2 ]), Convert.ToInt32(AllOnes_Sorted[ current_date ][ 3 ]), Convert.ToInt32(AllOnes_Sorted[ current_date ][ 4 ]), Convert.ToInt32(AllOnes_Sorted[ current_date ][ 5 ]), Convert.ToInt32(AllOnes_Sorted[ current_date ][ 6 ]) };

                    comb.Sort();

                    //get combination
                    string thoseNumbersString = CombinationFormatting(comb[ 0 ].ToString(), comb[ 1 ].ToString(), comb[ 2 ].ToString(), comb[ 3 ].ToString(), comb[ 4 ].ToString(), comb[ 5 ].ToString(), comb[ 6 ].ToString(), comb.IndexOf(Convert.ToInt32(date_AND_combination[ current_date ][ 6 ]))).Replace(" ","");
                    int comb_len = thoseNumbersString.Length;
					for ( int j = 0; j < 22 - comb_len; j++ )
					{
                        thoseNumbersString = thoseNumbersString + " ";
					}

                    int index_difference = 0;
                    string Current_day_comb = thoseNumbersString.Replace("[","").Replace("]","").Trim();

                    if ( result.Count >= 1 )
                    {
                        //next_date day combination
                        List<int> comb_next_day = new List<int> { Convert.ToInt32(AllOnes_Sorted[ previous_date ][ 0 ]), Convert.ToInt32(AllOnes_Sorted[ previous_date ][ 1 ]), Convert.ToInt32(AllOnes_Sorted[ previous_date ][ 2 ]), Convert.ToInt32(AllOnes_Sorted[ previous_date ][ 3 ]), Convert.ToInt32(AllOnes_Sorted[ previous_date ][ 4 ]), Convert.ToInt32(AllOnes_Sorted[ previous_date ][ 5 ]), Convert.ToInt32(AllOnes_Sorted[ previous_date ][ 6 ]) };

                        comb_next_day.Sort();
                        Current_day_comb = $"{comb[ 0 ]}-{comb[ 1 ]}-{comb[ 2 ]}-{comb[ 3 ]}-{comb[ 4 ]}-{comb[ 5 ]}-{comb[ 6 ]}";
                        string Next_day_comb = $"{comb_next_day[ 0 ]}-{comb_next_day[ 1 ]}-{comb_next_day[ 2 ]}-{comb_next_day[ 3 ]}-{comb_next_day[ 4 ]}-{comb_next_day[ 5 ]}-{comb_next_day[ 6 ]}";
                        index_difference = Indexes_85m[ Next_day_comb ] - Indexes_85m[ Current_day_comb ];

                    }
                    string State1 = current_date.Contains(" ") ? "T" : "L";

                    int index_in_85m = Indexes_85m[ Current_day_comb ];

                    string index_85m_string = index_in_85m.ToString();
                    int index_len = index_85m_string.Length;
					for ( int k = 0; k < 8 - index_len; k++ )
					{
                        index_85m_string = index_85m_string + " ";
					}

                    int index_1_30_85m = index_in_85m % 30 == 0 ? 30 : index_in_85m % 30;
                    count++;
                    count_1_30++;
                    count_1_30 = count_1_30 <= 30 ? count_1_30 : 1;
                    result.Add($"{count}\t{count_1_30}\t{date}\t{thoseNumbersString}\t{index_1_30_85m}\t{index_85m_string}\t{State1}\t[{Math.Abs( index_difference  )}]");
                    if ( actual_days_between_back == 1 && actual_days_between != 1 && actual_days_between != 0
                        )
                    {
                        result.Add($"\n\t{actual_days_between} days to next adjacent dates.\n");
                    }
                }
            }

            return result;
        }

        private void SaveTypingsAsIs(string path)
        {
            FindIndexesin85m();

            using ( StreamWriter sw = new StreamWriter(path) )
            {
                sw.WriteLine($"[SORTED BY DATE]");
                int count_unsorted_increment = 0; //for counting 1 to n
                int count_unsorted_1_30_increment = 0; //for counting 1 to 30
                    
                Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>(); //dict for required nums;

                foreach (string date in date_AND_combination.Keys)
                {
                    count_unsorted_increment++;
                    string count = count_unsorted_increment.ToString();
                    int new_length = count.Length;
                    for ( int ni = 0; ni < date_AND_combination.Count.ToString().Length - new_length; ni++ )
                    {
                        count = count + " ";
                    }

                    count_unsorted_1_30_increment++;
                    count_unsorted_1_30_increment = count_unsorted_1_30_increment <= 30 ? count_unsorted_1_30_increment : 1;

                    DateTime current_date = DateTime.Parse(date); //current date

                    List<int> list_of_combination = Array.ConvertAll(date_AND_combination[ date].ToArray(), a => int.Parse(a)).ToList(); //has 7 integer nums
                    int bonus = list_of_combination[6];
                    list_of_combination.Sort();

                    int bonus_index = list_of_combination.IndexOf(bonus);

                    string combination = CombinationFormatting(list_of_combination[0].ToString(),list_of_combination[1].ToString(), list_of_combination[2].ToString(), list_of_combination[3].ToString(), list_of_combination[4].ToString(), list_of_combination[5].ToString(), list_of_combination[6].ToString(),bonus_index).Replace(" ", "");//comb for outputing
                    //add extra spaces if comb is shorter
                    int spaces = 22 - combination.Length;
                    for ( int c = 0; c < spaces; c++ )
					{
                        combination = combination + " ";
					}

                    string comb_for_index = $"{list_of_combination[0]}-{list_of_combination[1]}-{list_of_combination[2]}-{list_of_combination[3]}-{list_of_combination[4]}-{list_of_combination[5]}-{list_of_combination[6]}"; //comb string used to find index in 85m


                    dict.Add(date, new List<string>(Array.ConvertAll( list_of_combination.ToArray(),z => z.ToString()).ToList()));//add current comb to dict
                    dict[date].Add(count_unsorted_increment.ToString()); //add value of count_unsorted_increment
                    dict[date].Add(bonus_index.ToString()); // add index of bonus

                    string day = current_date.Day < 10 ? "0" + current_date.Day.ToString() : current_date.Day.ToString();
                    string month = current_date.Month < 10 ? "0" + current_date.Month.ToString() : current_date.Month.ToString();

                    //outputing index in 85m
                    int index_in_85m = Indexes_85m[comb_for_index];
                    string index_in_85m_string = index_in_85m.ToString();
                    for ( int o = 0; o < 8 - index_in_85m.ToString().Length; o++ )
                    {
                        index_in_85m_string = index_in_85m_string + " ";
                    }

                    string index_in_85m_1_30 = index_in_85m%30 == 0?"30":( index_in_85m % 30 ).ToString(); //for outputting value in 85m (1-30)

                    string State = date.Contains(" ") ? "T" : "L"; //for outputing the state

                    sw.WriteLine($"{count}\t{count_unsorted_1_30_increment}\t{day}/{month}/{current_date.Year}\t{combination}\t{index_in_85m_1_30}\t{index_in_85m_string}\t{State}");
                    
                    //if value is 30 skip a line
                    if( count_unsorted_1_30_increment == 30 )
                    {
                        sw.WriteLine();
                    }
                }

                sw.WriteLine($"\n[SORTED BY COMBINATION]");
                dict = dict.OrderBy(a => int.Parse(a.Value[0])).ThenBy(b => int.Parse(b.Value[1])).ThenBy(c => int.Parse(c.Value[2])).ThenBy(d => int.Parse(d.Value[3])).ThenBy(e => int.Parse(e.Value[4])).ThenBy(f => int.Parse(f.Value[5])).ThenBy(g => int.Parse(g.Value[6])).ToDictionary(x => x.Key, y => y.Value); //sort the dict 

                int count_sorted_increment = 0;
                int count_sorted_1_30_increment = 0;

                foreach ( string date in dict.Keys)
                {
                    count_sorted_increment++;

                    count_sorted_1_30_increment++;
                    count_sorted_1_30_increment = count_sorted_1_30_increment <= 30 ? count_sorted_1_30_increment : 1; //if value == 30 set to 1

                    DateTime current_date = DateTime.Parse(date);

                    List<int> list_of_combination = Array.ConvertAll(dict[date].ToArray(), a => int.Parse(a)).ToList();

                    //outputed combination string
                    int bonus_index = int.Parse(dict[date][8]);
                    string combination = CombinationFormatting(list_of_combination[0].ToString(), list_of_combination[1].ToString(), list_of_combination[2].ToString(), list_of_combination[3].ToString(), list_of_combination[4].ToString(), list_of_combination[5].ToString(), list_of_combination[6].ToString(), bonus_index).Replace(" ","");
                    int spaces = 22 - combination.Length;
                    for ( int i = 0; i < spaces; i++ )
                    {
                        combination =  combination + " ";
                    }

                    string comb_for_index = $"{list_of_combination[0]}-{list_of_combination[1]}-{list_of_combination[2]}-{list_of_combination[3]}-{list_of_combination[4]}-{list_of_combination[5]}-{list_of_combination[6]}";

                    //for index in comb computed in above for loop
                    string index_in_unsorted = int.Parse(dict[ date ][ 7 ]).ToString();
                    int index_length = index_in_unsorted.Length;
                    for ( int i = 0; i < date_AND_combination.Count.ToString().Length - index_length; i++ )
                    {
                        index_in_unsorted = index_in_unsorted + " ";
                    }

                    //for actual index in 85
                    int index_in_85m = Indexes_85m[ comb_for_index ];
                    string index_in_85m_string = index_in_85m.ToString();
					for ( int f = 0; f < 8 - index_in_85m.ToString().Length; f++ )
					{
                        index_in_85m_string = index_in_85m_string + " ";
					}

                    string day = current_date.Day < 10 ? "0" + current_date.Day.ToString() : current_date.Day.ToString();
                    string month = current_date.Month < 10 ? "0" + current_date.Month.ToString() : current_date.Month.ToString();

                    string index_in_85m_1_30 = index_in_85m % 30 == 0?"30":( index_in_85m % 30 ).ToString(); //<30 index in 85m

                    string State = date.Contains(" ") ? "T" : "L";//state

                    sw.WriteLine($"{index_in_unsorted}\t{count_sorted_1_30_increment}\t{day}/{month}/{current_date.Year}\t{combination}\t{index_in_85m_1_30}\t{index_in_85m_string}\t{State}");

                    //if value is 30 skip a line
                    if (count_sorted_1_30_increment == 30)
                    {
                        sw.WriteLine();
                    }
                }

            }
        }

        private List<List<string>> CycleOfNumWithNumbering( int which_number )
        {
            if ( Indexes_85m.Count != CombTypedAsStrings.Count )
            {
                FindIndexesin85m();
            }
            List<string> result = new List<string>();
            List<List<string>> NewREsult = new List<List<string>>();
            //dict with sorted comb of (selected number eg 1)
            Dictionary<string, List<int>> AllOnes = new Dictionary<string, List<int>>(date_AND_combination.Where(a => a.Value.Contains(which_number.ToString())).ToDictionary(a => a.Key, b => Array.ConvertAll(b.Value.ToArray(), c => int.Parse(c)).OrderBy(d => d).ToList()));//.Where(e=>e.Value[0] == which_number).ToDictionary(e => e.Key, f => f.Value)

            //int count = 0;
            //remove all dates that are not adjacent
            for ( int i = 0; i < AllOnes.Keys.Count; i++ )
            {
                string now_date = AllOnes.Keys.ElementAt(i);
                string tomorrow_date = null;
                string yesterday_date = null;

                string days = null;
                int days_between_n_and_t = 0;
                int days_between_n_and_y = 0;

                try
                {
                    tomorrow_date = AllOnes.Keys.ElementAt(i + 1);
                }
                catch
                {
                    tomorrow_date = now_date;
                }
                try
                {
                    yesterday_date = AllOnes.Keys.ElementAt(i - 1);
                }
                catch
                {
                    yesterday_date = now_date;
                }

                TimeSpan days_between_today_and_tomorrow = DateTime.Parse(now_date) - DateTime.Parse(tomorrow_date);

                TimeSpanConverter tmc = new TimeSpanConverter();
                days = ( string ) tmc.ConvertTo(days_between_today_and_tomorrow, typeof(string));
                try
                {
                    days = days.Substring(0, days.IndexOf('.'));

                }
                catch
                {
                    days = days.Substring(0, days.IndexOf(':'));

                }
                days_between_n_and_t = Math.Abs(Convert.ToInt32(days));

                //*****************************************************************
                TimeSpan days_between_today_and_yesterday = DateTime.Parse(now_date) - DateTime.Parse(yesterday_date);
                string days_yester = ( string ) tmc.ConvertTo(days_between_today_and_yesterday, typeof(string));
                try
                {
                    days_yester = days_yester.Substring(0, days_yester.IndexOf('.'));

                }
                catch
                {
                    days_yester = days_yester.Substring(0, days_yester.IndexOf(':'));

                }
                days_between_n_and_y = Math.Abs(Convert.ToInt32(days_yester));

                if ( days_between_n_and_t != 1 && days_between_n_and_y != 1 )
                {
                    AllOnes.Remove(now_date);
                    i -= 1;
                }
            }



            int count = 0;
            int count_1_30 = 0;
            //now allOnes dict only contains adjacent dates
            for ( int i = 0; i < AllOnes.Keys.Count; i++ )
            {

                //int numbering_as_ones_indisorder = 0;
                string current_date = AllOnes.Keys.ElementAt(i);
                string next_date = null;
                string previous_date = null;

                try
                {
                    next_date = AllOnes.Keys.ElementAt(i + 1);
                }
                catch
                {
                    next_date = AllOnes.Keys.ElementAt(i);
                }
                try
                {
                    previous_date = AllOnes.Keys.ElementAt(i - 1);
                }
                catch
                {
                    previous_date = AllOnes.Keys.ElementAt(i);
                }
                string days = null;
                int actual_days_between = 0;

                TimeSpan days_between = DateTime.Parse(current_date) - DateTime.Parse(next_date);

                TimeSpanConverter tmc = new TimeSpanConverter();
                days = ( string ) tmc.ConvertTo(days_between, typeof(string));
                try
                {
                    days = days.Substring(0, days.IndexOf('.'));

                }
                catch
                {
                    days = days.Substring(0, days.IndexOf(':'));

                }
                actual_days_between = Math.Abs(Convert.ToInt32(days));

                //*********************************************************
                string daysb = null;
                int actual_days_between_back = 0;
                TimeSpan days_between_back = DateTime.Parse(current_date) - DateTime.Parse(previous_date);

                TimeSpanConverter tmcb = new TimeSpanConverter();
                daysb = ( string ) tmcb.ConvertTo(days_between_back, typeof(string));
                try
                {
                    daysb = daysb.Substring(0, daysb.IndexOf('.'));

                }
                catch
                {
                    daysb = daysb.Substring(0, daysb.IndexOf(':'));
                }
                actual_days_between_back = Math.Abs(Convert.ToInt32(daysb));

                if ( actual_days_between == 1 || actual_days_between_back == 1 )
                {
                    //reformat the date
                    //string date = current_date.Substring(0, current_date.IndexOf(' '));
                    DateTime dates = DateTime.Parse(current_date);
                    string date = $"{dates.Day}/{dates.Month}/{dates.Year}";
                    int date_len = date.Length;
                    for ( int j = 0; j < 10 - date_len; j++ )
                    {
                        date = date + " ";
                    }

                    List<int> comb = new List<int> { Convert.ToInt32(AllOnes[ current_date ][ 0 ]), Convert.ToInt32(AllOnes[ current_date ][ 1 ]), Convert.ToInt32(AllOnes[ current_date ][ 2 ]), Convert.ToInt32(AllOnes[ current_date ][ 3 ]), Convert.ToInt32(AllOnes[ current_date ][ 4 ]), Convert.ToInt32(AllOnes[ current_date ][ 5 ]), Convert.ToInt32(AllOnes[ current_date ][ 6 ]) };

                    comb.Sort();

                    //get combination
                    string thoseNumbersString = CombinationFormatting(comb[ 0 ].ToString(), comb[ 1 ].ToString(), comb[ 2 ].ToString(), comb[ 3 ].ToString(), comb[ 4 ].ToString(), comb[ 5 ].ToString(), comb[ 6 ].ToString(), comb.IndexOf(Convert.ToInt32(date_AND_combination[ current_date ][ 6 ]))).Replace(" ", "");
                    int comb_len = thoseNumbersString.Length;
                    for ( int j = 0; j < 22 - comb_len; j++ )
                    {
                        thoseNumbersString = thoseNumbersString + " ";
                    }
                    //int index_difference = 0;
                    if ( result.Count >= 1 )
                    {


                        //next_date day combination
                        List<int> comb_next_day = new List<int> { Convert.ToInt32(AllOnes[ previous_date ][ 0 ]), Convert.ToInt32(AllOnes[ previous_date ][ 1 ]), Convert.ToInt32(AllOnes[ previous_date ][ 2 ]), Convert.ToInt32(AllOnes[ previous_date ][ 3 ]), Convert.ToInt32(AllOnes[ previous_date ][ 4 ]), Convert.ToInt32(AllOnes[ previous_date ][ 5 ]), Convert.ToInt32(AllOnes[ previous_date ][ 6 ]) };

                        comb_next_day.Sort();
                    }
                    string State1 = current_date.Contains(" ") ? "T" : "L";
                    int index_in85 = Indexes_85m[ $"{comb[ 0 ]}-{comb[ 1 ]}-{comb[ 2 ]}-{comb[ 3 ]}-{comb[ 4 ]}-{comb[ 5 ]}-{comb[ 6 ]}" ];
                    string index_string = index_in85.ToString();
                    int index_len = index_string.Length;
                    for ( int k = 0; k < 8 - index_len; k++ )
                    {
                        index_string += " ";
                    }
                    int index_1_30_85m = index_in85 % 30 == 0 ? 30 : index_in85 % 30;
                    count++;
                    string count_string = count.ToString();
                    int count_len = count_string.Length;
                    for ( int l = 0; l < AllOnes.Keys.Count.ToString().Length - count_len; l++ )
                    {
                        count_string = count_string + " ";
                    }

                    count_1_30++;
                    count_1_30 = count_1_30 <= 30 ? count_1_30 : 1;
                    result.Add($"{count}\t{count_1_30}\t{date}\t{thoseNumbersString}\t{index_1_30_85m}\t{index_string}\t{State1}");
                    if ( actual_days_between_back == 1 && actual_days_between != 1 )
                    {
                        NewREsult.Add(new List<string>(result));
                        result.Clear();

                    }
                }
            }

            return NewREsult;
        }

        private Dictionary<int, List<string>> BonusesOver14Days( int number_of_days )
        {

            Dictionary<int, List<string>> result = new Dictionary<int, List<string>>();

            if ( date_AND_combination.Count >= 2 )
            {
                for ( int i = 0; i < number_of_days + 1; i++ )
                {
                    result.Add(i, new List<string>());
                }
                if ( Indexes_85m.Count != CombTypedAsStrings.Count )
                {
                    FindIndexesin85m();
                }

                Dictionary<string, int> Dates_and_Bonuses = new Dictionary<string, int>();
                //List<int> found = new List<int>();

                foreach ( string key in date_AND_combination.Keys )
                {
                    Dates_and_Bonuses.Add(key, Convert.ToInt32(date_AND_combination[ key ][ 6 ]));
                }

                //reverse dictionary so it starts with most recent
                Dates_and_Bonuses = Dates_and_Bonuses.Reverse().ToDictionary(x => x.Key, x => x.Value);

                //for ( int i = 1; i <= 49; i++ )
                //{
                //sw.WriteLine($"Bonus: {i}");
                //found.Clear();
                for ( int i = 0; i < Dates_and_Bonuses.Count; i++ )
                {
                    //if(i >= 21 )
                    //{
                    //    Console.WriteLine();
                    //}
                    string date = Dates_and_Bonuses.ElementAt(i).Key;
                    for ( int j = i + 1; j < i + number_of_days + 1; j++ )
                    {
                        string another_date = null;
                        try
                        {
                            another_date = Dates_and_Bonuses.ElementAt(j).Key;
                        }
                        catch
                        {
                            break;
                        }
                        if ( date != another_date && Dates_and_Bonuses[ date ] == Dates_and_Bonuses[ another_date ] )
                        {
                            TimeSpan days_between = DateTime.Parse(date) - DateTime.Parse(another_date);
                            TimeSpanConverter tmc = new TimeSpanConverter();
                            string days = ( string ) tmc.ConvertTo(days_between, typeof(string));
                            try
                            {

                                days = days.Substring(0, days.IndexOf('.'));
                            }
                            catch
                            {
                                days = days.Substring(0, days.IndexOf(':'));

                            }
                            int actual_days_between = Math.Abs(Convert.ToInt32(days)) - 1;


                            if ( actual_days_between <= number_of_days )// && !found.Contains(actual_days_between) )
                            {
                                //found.Add(actual_days_between);
                                //string new_date = date.Substring(0, date.IndexOf(' '));
                                //string new_another_date = another_date.Substring(0, another_date.IndexOf(' '));

                                DateTime array_date = DateTime.Parse(date);
                                DateTime array_another_date = DateTime.Parse(another_date);

                                string new_date = $"{array_date.Day}/{array_date.Month}/{array_date.Year}";
                                string new_another_date = $"{array_another_date.Day}/{array_another_date.Month}/{array_another_date.Year}";

                                List<string> a = new List<string>(date_AND_combination[ date ].OrderBy(x => Convert.ToInt32(x)).ToList());
                                List<string> b = new List<string>(date_AND_combination[ another_date ].OrderBy(x => Convert.ToInt32(x)).ToList());

                                string State1 = date.Contains(" ") ? "T" : "L";
                                string State2 = another_date.Contains(" ") ? "T" : "L";

                                switch ( actual_days_between )
                                {
                                    case 0:
                                        result[ 0 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 0 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;

                                    case 1:
                                        result[ 1 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 1 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;

                                    case 2:
                                        result[ 2 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 2 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;

                                    case 3:
                                        result[ 3 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 3 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;
                                    case 4:
                                        result[ 4 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 4 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;

                                    case 5:
                                        result[ 5 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 5 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;

                                    case 6:
                                        result[ 6 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 6 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;
                                    case 7:
                                        result[ 7 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 7 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;

                                    case 8:
                                        result[ 8 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 8 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;

                                    case 9:
                                        result[ 9 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 9 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;

                                    case 10:
                                        result[ 10 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 10 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;

                                    case 11:
                                        result[ 11 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 11 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;

                                    case 12:
                                        result[ 12 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 12 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;

                                    case 13:
                                        result[ 13 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 13 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;

                                    case 14:
                                        result[ 14 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 14 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;

                                    //************************************************************************************************************************************************

                                    case 15:
                                        result[ 15 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 15 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;

                                    case 16:
                                        result[ 16 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 16 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;

                                    case 17:
                                        result[ 17 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 17 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;

                                    case 18:
                                        result[ 18 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 18 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;
                                    case 19:
                                        result[ 19 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 19 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;

                                    case 20:
                                        result[ 20 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 20 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;

                                    case 21:
                                        result[ 21 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 21 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;
                                    case 22:
                                        result[ 22 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 22 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;

                                    case 23:
                                        result[ 23 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 23 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;

                                    case 24:
                                        result[ 24 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 24 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;

                                    case 25:
                                        result[ 25 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 25 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;

                                    case 26:
                                        result[ 26 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 26 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;

                                    case 27:
                                        result[ 27 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 27 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;

                                    case 28:
                                        result[ 28 ].Add($"{new_another_date} ==>  {CombinationFormatting(b[ 0 ], b[ 1 ], b[ 2 ], b[ 3 ], b[ 4 ], b[ 5 ], b[ 6 ], b.IndexOf(date_AND_combination[ another_date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{b[ 0 ]}-{b[ 1 ]}-{b[ 2 ]}-{b[ 3 ]}-{b[ 4 ]}-{b[ 5 ]}-{b[ 6 ]}" ]}\t{State2}");
                                        result[ 28 ].Add($"{new_date} ==>  {CombinationFormatting(a[ 0 ], a[ 1 ], a[ 2 ], a[ 3 ], a[ 4 ], a[ 5 ], a[ 6 ], a.IndexOf(date_AND_combination[ date ][ 6 ])).Replace(" ", "")} Line: {Indexes_85m[ $"{a[ 0 ]}-{a[ 1 ]}-{a[ 2 ]}-{a[ 3 ]}-{a[ 4 ]}-{a[ 5 ]}-{a[ 6 ]}" ]}\t{State1}\n");
                                        break;

                                }
                            }
                        }
                    }
                }

            }

            return result;

        }
        //FIND REPETITIONS
        private List<string>[] Repetitions()
        {
            List<string>[] result = new List<string>[ 2 ] { new List<string>(), new List<string>() };
            Dictionary<string, List<string>> Numbers = new Dictionary<string, List<string>>(date_AND_combination);
            int count = 0;
            int NumberOfCombinations = 0;

            for ( int i = 0; i < Numbers.Count; i++ )
            {
                //check if its a new month and reset count
                int today_month = DateTime.Parse(Numbers.ElementAt(i).Key).Month;
                int yesterday_month = 0;
                try
                {
                    yesterday_month = DateTime.Parse(Numbers.ElementAt(i - 1).Key).Month;

                }
                catch
                {
                    yesterday_month = today_month;
                }
                if ( today_month != yesterday_month )
                {
                    count = 0;
                }

                List<string> CurrentCombination = Numbers.ElementAt(i).Value;
                List<string> NextCombination = new List<string>();
                try
                {
                    NextCombination = Numbers.ElementAt(i + 1).Value;
                }
                catch
                {
                    break;
                }

                List<string> intersect = CurrentCombination.Intersect(NextCombination).ToList();

                if ( intersect.Count != 0 )//if repeated
                {
                    NumberOfCombinations++;
                }

                else if ( intersect.Count == 0 )//if not repeated
                {

                    count++;
                    result[ 0 ].Add($"{NumberOfCombinations + 1}");//repetitions
                    result[ 1 ].Add($"{count}");//skipped before repetitions
                    NumberOfCombinations = 0;
                }

            }


            return result;
        }

        //the two methods below are only for combined states
        //save all combinations as typed
        private List<string> AsTyped()
        {
            List<string> result = new List<string>();
            List<string> dates_to_subract = new List<string>();
            List<int> values = new List<int>();
            int count = 0;

            int index_count = 0;
            if ( Indexes_85m.Count != CombTypedAsStrings.Count )
            {
                FindIndexesin85m();
            }

            foreach ( string key in date_AND_combination.Keys )
            {
                count++;

                //get combinations as string
                List<int> ThoseNumbers = new List<int>();
                foreach ( string item in date_AND_combination[ key ] )
                {
                    ThoseNumbers.Add(Convert.ToInt32(item));
                }

                ThoseNumbers.Sort();
                if ( true )
                {
                    values.Add(Indexes_85m[ $"{ThoseNumbers[ 0 ]}-{ThoseNumbers[ 1 ]}-{ThoseNumbers[ 2 ]}-{ThoseNumbers[ 3 ]}-{ThoseNumbers[ 4 ]}-{ThoseNumbers[ 5 ]}-{ThoseNumbers[ 6 ]}" ]);
                    string days = null;
                    int actual_days_between = 0;
                    int index_difference = 0;

                    dates_to_subract.Add(key);

                    if ( dates_to_subract.Count >= 2 )
                    {
                        TimeSpan days_between = DateTime.Parse(dates_to_subract[ dates_to_subract.Count - 2 ]) - DateTime.Parse(dates_to_subract[ dates_to_subract.Count - 1 ]);
                        TimeSpanConverter tmc = new TimeSpanConverter();
                        days = ( string ) tmc.ConvertTo(days_between, typeof(string));
                        try
                        {
                            //daysb = daysb.Substring(0, daysb.IndexOf('.'));
                            days = days.Substring(0, days.IndexOf('.'));

                        }
                        catch
                        {
                            days = days.Substring(0, days.IndexOf(':'));
                            //daysb = daysb.Substring(0, daysb.IndexOf(':'));
                        }

                        actual_days_between = Math.Abs(Convert.ToInt32(days)) - 1;
                        try
                        {
                            index_difference = values[ index_count ] - values[ index_count + 1 ];
                            //index_difference = Indexes_85m.Values.ElementAt(index_count) - Indexes_85m.Values.ElementAt(index_count + 1);
                            index_count++;

                        }
                        catch
                        {
                            index_difference = 0;
                        }
                    }

                    string thoseNumbersString = CombinationFormatting(ThoseNumbers[ 0 ].ToString(), ThoseNumbers[ 1 ].ToString(), ThoseNumbers[ 2 ].ToString(), ThoseNumbers[ 3 ].ToString(), ThoseNumbers[ 4 ].ToString(), ThoseNumbers[ 5 ].ToString(), ThoseNumbers[ 6 ].ToString(), ThoseNumbers.IndexOf(Convert.ToInt32(date_AND_combination[ key ][ 6 ])));

                    //reformat the date
                    //string date = key.Substring(0, key.IndexOf(' '));
                    DateTime dates = DateTime.Parse(key);
                    string date = $"{dates.Day}/{dates.Month}/{dates.Year}";

                    string State1 = key.Contains(" ") ? "T" : "L";

                    result.Add($"({count}), {date},\t{thoseNumbersString},\t==>, index: {Indexes_85m[ $"{ThoseNumbers[ 0 ]}-{ThoseNumbers[ 1 ]}-{ThoseNumbers[ 2 ]}-{ThoseNumbers[ 3 ]}-{ThoseNumbers[ 4 ]}-{ThoseNumbers[ 5 ]}-{ThoseNumbers[ 6 ]}" ]},\n\t{actual_days_between} day(s) between, {actual_days_between + 2} day(s) axact,\n\tdifference from top: {index_difference } {State1}\n");
                }
            }

            return result;
        }
        //save all combinations in order (sorted)
        private List<string> InOrder()
        {

            List<string> result = new List<string>();

            //dict with sorted comb of (selected number eg 1)
            Dictionary<string, List<int>> Sorted_Nums = new Dictionary<string, List<int>>();

            List<int> values = new List<int>();


            if ( Indexes_85m.Count != CombTypedAsStrings.Count )
            {
                FindIndexesin85m();
            }

            //get all the ones
            foreach ( string key in date_AND_combination.Keys )
            {
                //get combinations as string
                List<int> ThoseNumbers = new List<int>();
                foreach ( string item in date_AND_combination[ key ] )
                {
                    ThoseNumbers.Add(Convert.ToInt32(item));
                }
                ThoseNumbers.Sort();

                values.Add(Indexes_85m[ $"{ThoseNumbers[ 0 ]}-{ThoseNumbers[ 1 ]}-{ThoseNumbers[ 2 ]}-{ThoseNumbers[ 3 ]}-{ThoseNumbers[ 4 ]}-{ThoseNumbers[ 5 ]}-{ThoseNumbers[ 6 ]}" ]);
                Sorted_Nums.Add(key, new List<int>(ThoseNumbers));

            }
            //sort 85m
            values.Sort();

            //sort the dictionary
            Sorted_Nums = Sorted_Nums.OrderBy(x => x.Value[ 0 ]).ThenBy(x => x.Value[ 1 ]).ThenBy(x => x.Value[ 2 ]).ThenBy(x => x.Value[ 3 ]).ThenBy(x => x.Value[ 4 ]).ThenBy(x => x.Value[ 5 ]).ThenBy(x => x.Value[ 6 ]).ToDictionary(x => x.Key, y => y.Value);

            int count = 0;
            int index_count = 0;
            List<string> dates_to_subract = new List<string>();

            //calculate results
            foreach ( string key in Sorted_Nums.Keys )
            {
                count++;

                //get combinations as string
                List<int> ThoseNumbers = new List<int>(Sorted_Nums[ key ]);

                string days = null;
                int actual_days_between = 0;
                int index_difference = 0;

                dates_to_subract.Add(key);

                if ( dates_to_subract.Count >= 2 )
                {
                    TimeSpan days_between = DateTime.Parse(dates_to_subract[ dates_to_subract.Count - 2 ]) - DateTime.Parse(dates_to_subract[ dates_to_subract.Count - 1 ]);
                    TimeSpanConverter tmc = new TimeSpanConverter();
                    days = ( string ) tmc.ConvertTo(days_between, typeof(string));
                    try
                    {


                        days = days.Substring(0, days.IndexOf('.'));
                    }
                    catch
                    {
                        days = days.Substring(0, days.IndexOf(':'));

                    }
                    actual_days_between = Math.Abs(Convert.ToInt32(days)) - 1;
                    try
                    {
                        //index_difference = Indexes_85m.Values.ElementAt(index_count) - Indexes_85m.Values.ElementAt(index_count + 1);
                        index_difference = values[ index_count ] - values[ index_count + 1 ];
                        index_count++;

                    }
                    catch
                    {
                        index_difference = 0;
                    }
                }

                string thoseNumbersString = CombinationFormatting(ThoseNumbers[ 0 ].ToString(), ThoseNumbers[ 1 ].ToString(), ThoseNumbers[ 2 ].ToString(), ThoseNumbers[ 3 ].ToString(), ThoseNumbers[ 4 ].ToString(), ThoseNumbers[ 5 ].ToString(), ThoseNumbers[ 6 ].ToString(), ThoseNumbers.IndexOf(Convert.ToInt32(date_AND_combination[ key ][ 6 ])));

                //reformat the date
                //string date = key.Substring(0, key.IndexOf(' '));
                DateTime dates = DateTime.Parse(key);
                string date = $"{dates.Day}/{dates.Month}/{dates.Year}";

                string State1 = key.Contains(" ") ? "T" : "L";

                result.Add($"({count}), ({date_AND_combination.Keys.ToList().IndexOf(key) + 1}),\t{date},\t{thoseNumbersString},\t==>, index: {Indexes_85m[ $"{ThoseNumbers[ 0 ]}-{ThoseNumbers[ 1 ]}-{ThoseNumbers[ 2 ]}-{ThoseNumbers[ 3 ]}-{ThoseNumbers[ 4 ]}-{ThoseNumbers[ 5 ]}-{ThoseNumbers[ 6 ]}" ]} {State1},\n\t{actual_days_between} day(s) between, {actual_days_between + 2} day(s) axact,\n\tdifference from top: {index_difference }\n");

            }

            return result;
        }


        //Methods for tasks************************************************************************************************
        private void Notify( string text )//add info in the listview
        {
            ListViewItem lvi = listView1__Output_Info.Items.Add(DateTime.Now.ToLongTimeString());
            lvi.SubItems.Add(text);
            lvi.EnsureVisible();
        }

        private bool AreNumbersCorrect()
        {
            TextBox[] AllTextBoxes = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7 };
            int n = 0;
            //bool res = false;
            //check if any of the textboxes are empty
            foreach ( TextBox text_box in AllTextBoxes )
            {
                if ( !int.TryParse(text_box.Text, out n) )
                {
                    return false;
                }
                else if(n <= 0 || n >= 50 )
				{
                    return false;
				}
            }

            //check  if numbers are not repeated
            for ( int i = 0; i < AllTextBoxes.Length; i++ )
            {
                for ( int j = i + 1; j < AllTextBoxes.Length; j++ )
                {
                    if ( AllTextBoxes[ i ].Text == AllTextBoxes[ j ].Text )
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        private string ReturnCorrectComb( string state, string raw_date_text )
        {

            string result = null;
            switch ( state.ToUpper() )
            {

                case "L":
                    result = DateTime.Parse(raw_date_text).ToShortDateString();
                    break;
                case "T":
                    result = DateTime.Parse(raw_date_text).AddHours(1).ToString();
                    break;
                case "N/A":
                    result = DateTime.Parse(raw_date_text).ToString();
                    break;
            }

            return result;
        }

        
        public void Generate_85m()
        {
            Directory.CreateDirectory(@"C:\85_Million");
            using ( StreamWriter sw = new StreamWriter(@"C:\85_Million\85_Million.txt") )
                for ( int a = 1; a < 44; a++ )
                {
                    for ( int b = a + 1; b < 45; b++ )
                    {
                        for ( int c = b + 1; c < 46; c++ )
                        {
                            for ( int d = c + 1; d < 47; d++ )
                            {
                                for ( int e = d + 1; e < 48; e++ )
                                {
                                    for ( int f = e + 1; f < 49; f++ )
                                    {
                                        for ( int g = f + 1; g < 50; g++ )
                                        {
                                            sw.WriteLine($"{a}-{b}-{c}-{d}-{e}-{f}-{g}");
                                        }
                                    }

                                }

                            }

                        }
                    }
                }
        }
        private void ClearFormData(bool clear_unsorted )
        {
            try
            {
                
                date_AND_combination.Clear();
                NumberAndDate.Clear();
                Indexes_85m.Clear();
                CombTypedAsStrings.Clear();
                listView1_Date_and_Numbers.Items.Clear();
                listView1_Date_and_Numbers.Groups.Clear();
                if ( clear_unsorted )
                {

                    listView1_Unsorted.Items.Clear();
                }
                listView1__Output_Info.Items.Clear();
                MostRecentDate = DateTime.MinValue;
                state = null;
                EnableButtons(true);
                menuStrip1_Toolbar.BackColor = Color.Gainsboro;
            }
			catch(Exception e)
			{
				MessageBox.Show(e.Message, "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
        }

        //this method returns the combination with the bonus enclosed in square brackets
        private string CombinationFormatting( string a, string b, string c, string d, string e, string f, string g, int SquaredNumberIndex )
        {
            switch ( Convert.ToInt32(SquaredNumberIndex) )
            {
                case 0:
                    return $"[{a}] - {b} - {c} - {d} - {e} - {f} - {g}";

                case 1:
                    return $"{a} - [{b}] - {c} - {d} - {e} - {f} - {g}";

                case 2:
                    return $"{a} - {b} - [{c}] - {d} - {e} - {f} - {g}";

                case 3:
                    return $"{a} - {b} - {c} - [{d}] - {e} - {f} - {g}";

                case 4:
                    return $"{a} - {b} - {c} - {d} - [{e}] - {f} - {g}";

                case 5:
                    return $"{a} - {b} - {c} - {d} - {e} - [{f}] - {g}";

                case 6:
                    return $"{a} - {b} - {c} - {d} - {e} - {f} - [{g}]";
            }
            return "An Error occurred";
        }
        private string ConvertToMonth( int month )
        {
            string result = null;
            switch ( month )
            {
                case 1:
                    result = "January";
                    break;
                case 2:
                    result = "February";
                    break;
                case 3:
                    result = "March";
                    break;
                case 4:
                    result = "April";
                    break;
                case 5:
                    result = "May";
                    break;
                case 6:
                    result = "June";
                    break;
                case 7:
                    result = "July";
                    break;
                case 8:
                    result = "August";
                    break;
                case 9:
                    result = "September";
                    break;
                case 10:
                    result = "October";
                    break;
                case 11:
                    result = "November";
                    break;
                case 12:
                    result = "December";
                    break;
            }
            return result != null ? result : "unknown month";
        }
        private int ConvertToMonth( string month )
        {
            int result = 0;
            switch ( month )
            {
                case "January":
                    result = 1;
                    break;

                case "February":
                    result = 2;
                    break;

                case "March":
                    result = 3;
                    break;

                case "April":
                    result = 4;
                    break;

                case "May":
                    result = 5;
                    break;

                case "June":
                    result = 6;
                    break;

                case "July":
                    result = 7;
                    break;

                case "August":
                    result = 8;
                    break;

                case "September":
                    result = 9;
                    break;

                case "October":
                    result = 10;
                    break;

                case "November":
                    result = 11;
                    break;

                case "December":
                    result = 12;
                    break;
            }
            return result;
        }
		
        public void FindIndexesin85m()
        {
            if ( CombTypedAsStrings.Count != Indexes_85m.Count )
            {
                Get_index(CombTypedAsStrings);
            }

        }
        private int Index_For_One_Comb(string combination)
        {
            //get comb as int in order
            List<int> nums = combination.Split('-').Select(x => int.Parse(x)).ToList();
            //nums.Sort();

            int new_count = 0;
            int result = 0;
            for (int i = nums[0]; i < 44; i++)
            {
                if (i == nums[0] + 1)
                {
                    break;
                }
                for (int j = new_count == 0 ? nums[1] : i + 1; j < 45; j++)
                {
                    for (int k = new_count == 0 ? nums[2] : j + 1; k < 46; k++)
                    {
                        for (int l = new_count == 0 ? nums[3] : k + 1; l < 47; l++)
                        {
                            for (int m = new_count == 0 ? nums[4] : l + 1; m < 48; m++)
                            {
                                for (int n = new_count == 0 ? nums[5] : m + 1; n < 49; n++)
                                {
                                    for (int o = new_count == 0 ? nums[6] : n + 1; o < 50; o++)
                                    {
                                        new_count++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            try
            {
                result = values[nums[0]] - new_count;
            }
            catch
            {
                result = values[values.Length - 1];
            }
            return result;
        }
        private void Get_index( List<string> combinations )
        {
            if ( CombTypedAsStrings.Count != Indexes_85m.Count )
            {
                Indexes_85m.Clear();
                foreach ( string combination in combinations )
                {
                    //get comb as int in order
                    List<int> nums = combination.Split('-').Select(x => int.Parse(x)).ToList();
                    nums.Sort();

                    int new_count = 0;

                    for ( int i = nums[ 0 ]; i < 44; i++ )
                    {
                        if ( i == nums[ 0 ] + 1 )
                        {
                            break;
                        }
                        for ( int j = new_count == 0 ? nums[ 1 ] : i + 1; j < 45; j++ )
                        {
                            for ( int k = new_count == 0 ? nums[ 2 ] : j + 1; k < 46; k++ )
                            {
                                for ( int l = new_count == 0 ? nums[ 3 ] : k + 1; l < 47; l++ )
                                {
                                    for ( int m = new_count == 0 ? nums[ 4 ] : l + 1; m < 48; m++ )
                                    {
                                        for ( int n = new_count == 0 ? nums[ 5 ] : m + 1; n < 49; n++ )
                                        {
                                            for ( int o = new_count == 0 ? nums[ 6 ] : n + 1; o < 50; o++ )
                                            {
                                                new_count++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    try
                    {
                        Indexes_85m.Add(combination, values[ nums[ 0 ] ] - new_count);
                    }
                    catch
                    {
                        Indexes_85m.Add(combination, values[ values.Length - 1 ]);
                    }
                }
            }
        }

        //for getting latest results on lottonumbers.com
        private async void GetLatestResults()
		{
            string this_state = state == "L" ? "lunchtime" : state == "T" ? "teatime" : string.IsNullOrEmpty(state) ? "unknown" : "both";
            List<List<string>> Correct_Data = new List<List<string>>();

            try
            {
                if ( !string.IsNullOrEmpty(ImportedFile) )
                {
                    if ( this_state != "both" && this_state != "unknown" )
                    {
                        ListViewItem lw = listView1__Output_Info.Items.Add($"{DateTime.Now.ToLongTimeString()}");
                        lw.SubItems.Add($"Getting latest results...");
                        lw.EnsureVisible();

                        HttpClient client = new HttpClient();
                        string uri = $"https://www.lottonumbers.com/past-uk-49s-{this_state}-results";
                        var string_data = await client.GetStringAsync(uri);

                        var html_document = new HtmlAgilityPack.HtmlDocument();
                        html_document.LoadHtml(string_data);

                        var Numbers = html_document.DocumentNode.Descendants("tbody")
                            .Where(node => node.ParentNode.GetAttributeValue("class", "")
                            .Equals("lotteryTable"))
                            .ToList();

                        var NumbersList = Numbers[ 0 ].Descendants("tr").ToList();

                        List<string> Wrong_Output = new List<string>();

                        foreach ( var item in NumbersList )
                        {
                            string[] s = item.InnerText.Split('\n');
                            foreach ( string str in s )
                            {
                                string st = str.Trim();//remove spaces
                                if ( st.Length >= 5 )
                                {
                                    st = st.Substring(st.IndexOf(" ") + 1); //remove day of week
                                    st = st.Remove(st.IndexOf(" ") - 2, 2); //remove formating (st,nd,rt,th)
                                }
                                if ( !string.IsNullOrWhiteSpace(st) )
                                {
                                    Wrong_Output.Add(st);
                                }
                            }
                        }
                        //only get results that latest i.e > last added date
                        for ( int j = 0; j < Wrong_Output.Count - 6; j += 8 )
                        {
                            if ( DateTime.Parse(Wrong_Output[ j ]) <= MostRecentDate )
                            {
                                continue; //dont add 
                            }
                            Correct_Data.Add(new List<string>() { Wrong_Output[ j ] });//add date

                            for ( int i = j + 1; i < j + 8; i++ )
                            {
                                Correct_Data[ Correct_Data.Count - 1 ].Add(Wrong_Output[ i ]);//add numbers
                            }
                        }

                        Correct_Data.Reverse();


                        if ( Correct_Data.Count > 0 )
                        {
                            //add data to listview
                            TextBox[] boxes = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7 };

                            using ( StreamWriter sw = new StreamWriter(ImportedFile, true) )
                            {
                                sw.WriteLine();
                                for ( int i = 0; i < Correct_Data.Count; i++ )
                                {
                                    List<string> data = Correct_Data[ i ];
                                    dateTimePicker1_Date.Value = DateTime.Parse(data[ 0 ]);
                                    boxes[ 0 ].Text = data[ 1 ];
                                    boxes[ 1 ].Text = data[ 2 ];
                                    boxes[ 2 ].Text = data[ 3 ];
                                    boxes[ 3 ].Text = data[ 4 ];
                                    boxes[ 4 ].Text = data[ 5 ];
                                    boxes[ 5 ].Text = data[ 6 ];
                                    boxes[ 6 ].Text = data[ 7 ];

                                    if ( button1_Add.Enabled )
                                    {
                                        AddNumbers();
                                        //update import file
                                        if ( i == Correct_Data.Count - 1 )
                                        {
                                            sw.Write($"{data[ 0 ].Trim()},{data[ 1 ]},{data[ 2 ]},{data[ 3 ]},{data[ 4 ]},{data[ 5 ]},{data[ 6 ]},{data[ 7 ]}");
                                        }
                                        else sw.WriteLine($"{data[ 0 ].Trim()},{data[ 1 ]},{data[ 2 ]},{data[ 3 ]},{data[ 4 ]},{data[ 5 ]},{data[ 6 ]},{data[ 7 ]}");
                                    }
                                    else Notify($"Error on {data[ 0 ]}. Numbers not added.");

                                }
                            }
                            string plural = Correct_Data.Count > 1 ? "Results" : "Result";
                            Notify($"{Correct_Data.Count} {plural} added.");
                            listView1_Date_and_Numbers.Items[ listView1_Date_and_Numbers.Items.Count - 1 ].EnsureVisible();
                            dateTimePicker1_Date.Value = dateTimePicker1_Date.Value.AddDays(1);

                        }
                        else
                        {

                            Notify($"No latest results found!");

                        }

                    }
                    else
                    {
                        Notify($"Error retrieving results.");

                        MessageBox.Show("Cannot get latest results in 'Combined' or 'Unknown' state. Import Lunchtime or Teatime separately and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    Notify($"Error retrieving results.");

                    //if import file is null or empty
                    MessageBox.Show("Cannot get latest results, please 'Import' some results from your local machine and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                
            }
            catch ( Exception f )
            {
                Notify($"Error retrieving results.");

                MessageBox.Show(f.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //for getting all results on lottonumbers.com
        private async Task GetAllResults(string this_state )
		{
            DateTime july19 = DateTime.Parse("19 July 2000");
			List<List<string>> RELEASE_Data = new List<List<string>>();

            //choose where to save
            SaveFileDialog ofd = new SaveFileDialog
			{
				Filter = "Text files (*.txt)|*.txt"
			};
			ofd.ShowDialog();
            string filename = ofd.FileName;
            ofd.Dispose();
            try
            {
                if ( !string.IsNullOrEmpty(filename) )
                {
                    //Notify($"Getting all results...");
                    ListViewItem lvi = listView1__Output_Info.Items.Add(DateTime.Now.ToLongTimeString());
                    lvi.SubItems.Add("Fetching results..."+ $"{0} / {DateTime.Now.Year - 1999}.");


                    if ( !string.IsNullOrEmpty(this_state) )
                    {
                        int count = 1;
                        for ( int year = 2000; year <= DateTime.Now.Year; year++ )
                        {
			                List<List<string>> BETA_Data = new List<List<string>>();
                            HttpClient client = new HttpClient();
                            string uri = $"https://www.lottonumbers.com/uk-49s-{this_state}-results-{year}";
                            var string_data = await client.GetStringAsync(uri);

                            var html_document = new HtmlAgilityPack.HtmlDocument();
                            html_document.LoadHtml(string_data);

                            var Numbers = html_document.DocumentNode.Descendants("tbody")
                                .Where(node => node.ParentNode.GetAttributeValue("class", "")
                                .Equals("lotteryTable"))
                                .ToList();

                            var NumbersList = Numbers[ 0 ].Descendants("tr").ToList();

                            List<string> Wrong_Output = new List<string>();

                            foreach ( var item in NumbersList )
                            {
                                string[] s = item.InnerText.Split('\n');
                                foreach ( string str in s )
                                {
                                    string st = str.Trim();//remove spaces

                                    if ( st.Length >= 5 )
                                    {
                                        st = st.Substring(st.IndexOf(" ") + 1); //remove day of week
                                        st = st.Remove(st.IndexOf(" ") - 2, 2); //remove formating (st,nd,rt,th)

                                    }
                                    if ( !string.IsNullOrWhiteSpace(st) )
                                    {
                                        Wrong_Output.Add(st);
                                    }
                                }
                            }

                            for ( int j = 0; j < Wrong_Output.Count; j++ )
                            {
                                if ( Wrong_Output[ j ].Length > 5 )
                                {
                                    if ( DateTime.Parse(Wrong_Output[ j ]) < july19 )
                                    {
                                        break; //dont add 
                                    }
                                    if ( BETA_Data.Count > 0 )
                                    {
                                        if ( BETA_Data[ BETA_Data.Count - 1 ].Count < 8 )
                                        {
                                            BETA_Data.RemoveAt(BETA_Data.Count - 1);
                                        }
                                    }
                                    //NotifyInResultsList(Wrong_Output[ j ]);
                                    BETA_Data.Add(new List<string>() { Wrong_Output[ j ] });//add date
                                }
                                else
                                {
                                    BETA_Data[ BETA_Data.Count - 1 ].Add(Wrong_Output[ j ]);//add number
                                }
                            }
                            BETA_Data.Reverse();
                            RELEASE_Data.AddRange(BETA_Data);
                            //count++;
                            lvi.SubItems[ 1 ].Text = "Fetching results..." + $"{count++} / {DateTime.Now.Year - 1999}.";
                        }
                    }
                    else
                    {
                        //Notify($"Error retrieving results.");
                        MessageBox.Show("Something went wrong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        success = "false";
                        return;
                    }
                }
                else
                {
                    //Notify($"Error retrieving results.");
                    //if import file is null or empty
                    MessageBox.Show("Please specify a valid file name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    success = "false";
                    return;
                }
                
                //add numbers to program
                //Correct_Data.Reverse();

                if ( RELEASE_Data.Count > 0 )
                {
                    //add data to listview
                    TextBox[] boxes = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7 };

                    using ( StreamWriter sw = new StreamWriter(filename) )
                    {
                        //sw.WriteLine();
                        for ( int i = 0; i < RELEASE_Data.Count; i++ )
                        {
                            List<string> data = RELEASE_Data[ i ];
                            dateTimePicker1_Date.Value = DateTime.Parse(data[ 0 ]);
                            boxes[ 0 ].Text = data[ 1 ];
                            boxes[ 1 ].Text = data[ 2 ];
                            boxes[ 2 ].Text = data[ 3 ];
                            boxes[ 3 ].Text = data[ 4 ];
                            boxes[ 4 ].Text = data[ 5 ];
                            boxes[ 5 ].Text = data[ 6 ];
                            boxes[ 6 ].Text = data[ 7 ];

                            if ( button1_Add.Enabled )
                                AddNumbers();
                            else Notify($"Error on {data[ 0 ]}. Numbers not added.");
                            //write to file
                            if ( i == RELEASE_Data.Count - 1 )
                            {
                                sw.Write($"{data[ 0 ].Trim()},{data[ 1 ]},{data[ 2 ]},{data[ 3 ]},{data[ 4 ]},{data[ 5 ]},{data[ 6 ]},{data[ 7 ]}");
                            }
                            else sw.WriteLine($"{data[ 0 ].Trim()},{data[ 1 ]},{data[ 2 ]},{data[ 3 ]},{data[ 4 ]},{data[ 5 ]},{data[ 6 ]},{data[ 7 ]}");
                        }
                    }
                    listView1_Date_and_Numbers.Items[ listView1_Date_and_Numbers.Items.Count - 1 ].EnsureVisible();
                    dateTimePicker1_Date.Value = dateTimePicker1_Date.Value.AddDays(1);

                }
                ImportedFile = filename;
            }
            catch ( Exception f )
            {
                //Notify($"Error retrieving results.");
                MessageBox.Show(f.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                success = "false";
                return;
            }
            success = success==null? "true":"false";
        }

        private void PossibleFives()
		{
            if ( date_AND_combination.Count > 0 )
            {
                //BeginInvoke( ListViewItem lvi = listView1__Output_Info.Items.Add($"0 of 1 906 884"));
                //BeginInvoke(( Myd ) delegate { ListViewItem lvi = listView1__Output_Info.Items.Add($"{DateTime.Now.ToShortDateString()}");
                //lvi.SubItems.Add($"0 of 1 906 884"); });

                using ( StreamWriter sw = new StreamWriter("./PossibleFives.txt", false) )
                {
                    int count = 0;
                    int bcount = 0;
                    for ( int c = 1; c < 46; c++ )
                    {
                        for ( int d = c + 1; d < 47; d++ )
                        {
                            for ( int e = d + 1; e < 48; e++ )
                            {
                                for ( int f = e + 1; f < 49; f++ )
                                {
                                    for ( int g = f + 1; g < 50; g++ )
                                    {
                                        HashSet<int> hset = new HashSet<int>();
                                        int[] nums = { c, d, e, f, g };
                                        for ( int i = 1; i < 5; i++ )
                                        {
                                            hset.Add(nums[ i ] - nums[ i - 1 ]);
                                        }
                                            int PlayCount = date_AND_combination.Values.Where(lst => lst.Contains(nums[ 0 ].ToString()) && lst.Contains(nums[ 1 ].ToString()) && lst.Contains(nums[ 2 ].ToString()) && lst.Contains(nums[ 3 ].ToString()) && lst.Contains(nums[ 4 ].ToString())).Count();
                                        if ( PlayCount >0)
                                        {

                                            count++;
                                            string pcount = PlayCount > 0 ? $"{PlayCount} time(s)" : "";
                                            sw.WriteLine($"{count}. {nums[ 0 ]} - {nums[ 1 ]} - {nums[ 2 ]} - {nums[ 3 ]} - {nums[ 4 ]}\t{pcount}");
                                        }
                                        bcount++;
                                        //BeginInvoke(lvi.Text = $"{bcount} of 1 906 884") ;
                                        //BeginInvoke(( Myd ) delegate { lvi. = $"{bcount} of 1 906 884"; });

                                    }
                                }

                            }

                        }

                    }
                }
            }
            else
            {
                Notify("Please import some results.");
            }
        }

		private async void savePossibleFivesToolStripMenuItem_Click( object sender, EventArgs ev )
		{
            Notify("Saving Possible Five Combs...");

            //search asynchronously
            Task task = new Task(PossibleFives);
            //Task<bool> task = new Task<bool>(arg => { return PossibleFives(( string ) arg); }, filename);
            task.Start();
            await task;
            if ( task.IsCompleted )
            {
                Notify("Task completed.");
            }
            
        }
	}
}