using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;

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

        //private int one_thirty = 0;//for 1-30 count in addNumbers

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


        private void refreshToolStripMenuItem_Click( object sender, EventArgs e )
        {
            Refresh_Unsorted_Listview();
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
            state = null;
            //ImportedFile = "both";
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
                List<ResultsModel> results = ImportFromDatabase();
                if(results.Count <= 0 ) 
                {
                    Notify("Database is empty. Importing from text file. This may take a while.");
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
                        //Task task = new Task(arg => { ShowProgress(( bool ) arg); }, true);
                        //task.Start();

                        //Cursor = Cursors.No;

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

                        listView1_Date_and_Numbers.Items[ listView1_Date_and_Numbers.Items.Count - 1 ].EnsureVisible();
					}
                    else
				    {
                        //Cursor = Cursors.Default;
                        return false;
                    }
					
                }
				else
				{
                    AddNumbers(results);
                }
            }
            catch ( Exception error )
            {
                //ShowProgress(false);
                //Cursor = Cursors.Default;

                MessageBox.Show(error.Message, "Error Importing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //ShowProgress(false);
            //Cursor = Cursors.Default;

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
        //takes numbers from text file
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
					int ind = Index_For_One_Comb($"{CurrentNumbers[ 0 ]}-{CurrentNumbers[ 1 ]}-{CurrentNumbers[ 2 ]}-{CurrentNumbers[ 3 ]}-{CurrentNumbers[ 4 ]}-{CurrentNumbers[ 5 ]}-{CurrentNumbers[ 6 ]}");

					//add tag
					AddedItem.Tag = ind;
                    
                    try
					{
                        Indexes_85m.Add($"{CurrentNumbers[ 0 ]}-{CurrentNumbers[ 1 ]}-{CurrentNumbers[ 2 ]}-{CurrentNumbers[ 3 ]}-{CurrentNumbers[ 4 ]}-{CurrentNumbers[ 5 ]}-{CurrentNumbers[ 6 ]}", ind);
					}
					catch { }

                    ResultsModel r = new ResultsModel();
                    r.date = dateTimePicker1_Date.Value.ToShortDateString();
                    r.first = int.Parse(textBox1.Text);
                    r.second = int.Parse(textBox2.Text);
                    r.third = int.Parse(textBox3.Text);
                    r.forth = int.Parse(textBox4.Text);
                    r.fifth = int.Parse(textBox5.Text);
                    r.sixth = int.Parse(textBox6.Text);
                    r.bonus = int.Parse(textBox7.Text);
                    r.position = ind;
                    SaveToDatabase(r);

                    //add items to unsorted list
                    //string p_30 = ind % 30 == 0 ? "30" : ( ind % 30 ).ToString();
                    ListViewItem UnsortedItem = listView1_Unsorted.Items.Add($"{listView1_Unsorted.Items.Count + 1}");//count
                    //one_thirty++;
                    //one_thirty = one_thirty <= 30 ? one_thirty: 1;
                    //UnsortedItem.SubItems.Add(one_thirty.ToString());//1-30 count
                    UnsortedItem.SubItems.Add(AddedItem.Text);//date
                    UnsortedItem.SubItems.Add(AddedItem.SubItems[ 1 ].Text);//combination
                    //UnsortedItem.SubItems.Add(p_30);//1-30 in 85m
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

        //takes numbers from database
        private void AddNumbers(List<ResultsModel> results)
        {
            //array with all the textboxes
            try
            {
                foreach ( ResultsModel result in results )
                {
                    bool CanAdd = true;
                    List<int> CurrentNumbers = new List<int> {result.first, result.second, result.third, result.forth, result.fifth, result.sixth, result.bonus };
                    CurrentNumbers.Sort();
                    string NumbersString = $"{CurrentNumbers[ 0 ]}-{CurrentNumbers[ 1 ]}-{CurrentNumbers[ 2 ]}-{CurrentNumbers[ 3 ]}-{CurrentNumbers[ 4 ]}-{CurrentNumbers[ 5 ]}-{CurrentNumbers[ 6 ]}";

                    //check if numbers already exist
                    if ( !CombTypedAsStrings.Contains(NumbersString) )
                    {
                        CombTypedAsStrings.Add(NumbersString);
                    }
                    else
                    {
                        DialogResult dr = MessageBox.Show($"Numbers on [{dateTimePicker1_Date.Value.Day} {ConvertToMonth(dateTimePicker1_Date.Value.Month)} {dateTimePicker1_Date.Value.Year}] were previously added. Do you want to add them again?", "Duplicate Numbers", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if ( dr == DialogResult.No )
                        {
                            CanAdd = false;
                        }
                    }

                    if ( CanAdd )
                    {
                        DateTime theDate = DateTime.Parse(result.date);
                        //handle squared number
                        WhereIsSquaredNumber = CurrentNumbers.IndexOf(result.bonus);

                        //add date to items
                        ListViewItem AddedItem = listView1_Date_and_Numbers.Items.Add(result.date);
                        MostRecentDate = theDate > MostRecentDate ? theDate : MostRecentDate;
                        //add item to group
                        string NewGroupHeader = $"{ ConvertToMonth(theDate.Month)}, {theDate.Year}";

                        AddedItem.Group = listView1_Date_and_Numbers.Groups[ NewGroupHeader ] == null ? listView1_Date_and_Numbers.Groups.Add(NewGroupHeader, NewGroupHeader) : listView1_Date_and_Numbers.Groups[ NewGroupHeader ];

                        AddedItem.Group.Tag = theDate.Month;

                        AddedItem.Name = NewGroupHeader;

                        //keep track of the added numbers bonus
                        AddedItem.ToolTipText = result.bonus < 10 ? "0" + result.bonus : result.bonus.ToString();

                        //add combination to subitems
                        AddedItem.SubItems.Add(CombinationFormatting($"{CurrentNumbers[ 0 ]}", $"{CurrentNumbers[ 1 ]}", $"{CurrentNumbers[ 2 ]}", $"{CurrentNumbers[ 3 ]}", $"{CurrentNumbers[ 4 ]}", $"{CurrentNumbers[ 5 ]}", $"{CurrentNumbers[ 6 ]}", WhereIsSquaredNumber));

                        //add state to subitems
                        AddedItem.SubItems.Add(state.ToUpper());

                        //add index in 85m
                        int ind = result.position;

                        //add tag
                        AddedItem.Tag = ind;

                        try
                        {
                            Indexes_85m.Add($"{CurrentNumbers[ 0 ]}-{CurrentNumbers[ 1 ]}-{CurrentNumbers[ 2 ]}-{CurrentNumbers[ 3 ]}-{CurrentNumbers[ 4 ]}-{CurrentNumbers[ 5 ]}-{CurrentNumbers[ 6 ]}", ind);
                        }
                        catch { }

                        //add items to unsorted list
                        //string p_30 = ind % 30 == 0 ? "30" : ( ind % 30 ).ToString();
                        ListViewItem UnsortedItem = listView1_Unsorted.Items.Add($"{listView1_Unsorted.Items.Count + 1}");//count
                        //one_thirty++;
                        //one_thirty = one_thirty <= 30 ? one_thirty : 1;
                        //UnsortedItem.SubItems.Add(one_thirty.ToString());//1-30 count
                        UnsortedItem.SubItems.Add(AddedItem.Text);//date
                        UnsortedItem.SubItems.Add(AddedItem.SubItems[ 1 ].Text);//combination
                        //UnsortedItem.SubItems.Add(p_30);//1-30 in 85m
                        UnsortedItem.SubItems.Add($"{ind}");//index
                        UnsortedItem.SubItems.Add(state.ToUpper());//state
                        UnsortedItem.Tag = AddedItem.Tag;
                        UnsortedItem.ToolTipText = theDate.ToShortDateString();

                        //add date and combination for saving data to json RED
                        date_AND_combination.Add(ReturnCorrectComb(AddedItem.SubItems[ 2 ].Text, theDate.ToShortDateString()),Array.ConvertAll( new int[] { result.first, result.second, result.third, result.forth, result.fifth, result.sixth, result.bonus }, a => a.ToString()).ToList());

                    }
                }
            }

            catch ( Exception e )
            {
                MessageBox.Show(e.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                ImportedFile = null; //file used when importing

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

        //for getting latest results on lottonumbers.com [up to past 6 months]
        private async void GetLatestResults()
		{
            string this_state = state == "L" ? "lunchtime" : state == "T" ? "teatime" : string.IsNullOrEmpty(state) ? "unknown" : "both";
            List<List<string>> Correct_Data = new List<List<string>>();

            try
            {
                if (listView1_Date_and_Numbers.Items.Count > 0)
                {
                    if ( this_state != "both" && this_state != "unknown" )
                    {
                        string filename = null;
                        if ( string.IsNullOrEmpty(ImportedFile) ) {
                            //choose where to append results

                            OpenFileDialog ofd = new OpenFileDialog();

                            ofd.Filter = "Text files (*.txt)|*.txt";
                            ofd.Title = "Choose file to add results to.";
                            ofd.Multiselect = false;
                            ofd.ShowDialog();
                            filename = ofd.FileName;
                            ofd.Dispose();

                        } else filename = ImportedFile;

                        ListViewItem lw = listView1__Output_Info.Items.Add($"{DateTime.Now.ToLongTimeString()}");
                        lw.SubItems.Add($"Fetching latest {this_state} results...");
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

                            using ( StreamWriter sw = new StreamWriter(filename, true) )
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

                        MessageBox.Show("Cannot get latest results. Please import Lunchtime or Teatime separately and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    
                    Notify($"Error retrieving results.");

                    MessageBox.Show("Cannot get latest results. Please import Lunchtime or Teatime and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    lvi.SubItems.Add("Fetching results "+ $"({0} / {DateTime.Now.Year - 1999})");


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
                            lvi.SubItems[ 1 ].Text = "Fetching results " + $"({count++} / {DateTime.Now.Year - 1999})";
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
                        using ( IDbConnection cnn = new SQLiteConnection(LoadConnectionString()) )
                        {
                            string s = this_state == "lunchtime" ? "Lunchtime" : "Teatime";
                            //return res.ToList();

                            for ( int i = 0; i < RELEASE_Data.Count; i++ )
                            {
								List<string> data = RELEASE_Data[ i ];
                                DateTime ddate = DateTime.Parse(data[ 0 ]);

                                List<ResultsModel> res = cnn.Query<ResultsModel>($"SELECT * FROM {s} WHERE date = '{ddate.ToShortDateString()}'", new DynamicParameters()).ToList();

								
                                if ( res.Count != 1 )
								{

									dateTimePicker1_Date.Value = ddate;
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
								else
								{
                                    AddNumbers(res);
								}

								//write to file
								if ( i == RELEASE_Data.Count - 1 )
								{
									sw.Write($"{data[ 0 ].Trim()},{data[ 1 ]},{data[ 2 ]},{data[ 3 ]},{data[ 4 ]},{data[ 5 ]},{data[ 6 ]},{data[ 7 ]}");
								}
								else sw.WriteLine($"{data[ 0 ].Trim()},{data[ 1 ]},{data[ 2 ]},{data[ 3 ]},{data[ 4 ]},{data[ 5 ]},{data[ 6 ]},{data[ 7 ]}");

                            }
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

        private string LoadConnectionString(string name = "Default" )
		{
            return ConfigurationManager.ConnectionStrings[ name ].ConnectionString;
		}

        private List<ResultsModel> ImportFromDatabase()
		{
            using(IDbConnection cnn = new SQLiteConnection(LoadConnectionString()) )
			{
                string s = state == "L" ? "Lunchtime" : "Teatime";
                var res = cnn.Query<ResultsModel>($"select * from {s}", new DynamicParameters());
                return res.ToList();
			}
		}

        private void SaveToDatabase(ResultsModel r)
        {
            //ImportFromDatabase();
			//try
			//{
				using ( IDbConnection cnn = new SQLiteConnection(LoadConnectionString()) )
                {
                    string s = state == "L" ? "Lunchtime" : "Teatime";
                    //cnn.Execute("show tables;");
                    cnn.Execute($"INSERT INTO {s} (date, first, second, third, forth, fifth, sixth, bonus, position) VALUES (@date, @first, @second, @third, @forth, @fifth, @sixth, @bonus, @position)",r);
                }
		//}
		//	catch {  }
        }

		private void pairingToolStripMenuItem_Click( object sender, EventArgs e )//pair numbrs event
		{
            Form pairing_form = new Pairing();
            pairing_form.Show();
        }
	}
}