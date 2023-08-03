namespace Brain
{
	partial class Search
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.CustomLabel customLabel1 = new System.Windows.Forms.DataVisualization.Charting.CustomLabel();
			System.Windows.Forms.DataVisualization.Charting.CustomLabel customLabel2 = new System.Windows.Forms.DataVisualization.Charting.CustomLabel();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
			this.listView1_Numbers = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.button1_Search = new System.Windows.Forms.Button();
			this.dateTimePicker1_from = new System.Windows.Forms.DateTimePicker();
			this.dateTimePicker2_to = new System.Windows.Forms.DateTimePicker();
			this.label1_from = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.richTextBox1_TypeNums = new System.Windows.Forms.RichTextBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.statusStrip1_info = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1_info_label = new System.Windows.Forms.ToolStripStatusLabel();
			this.tableLayoutPanel1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
			this.tableLayoutPanel3.SuspendLayout();
			this.statusStrip1_info.SuspendLayout();
			this.SuspendLayout();
			// 
			// listView1_Numbers
			// 
			this.listView1_Numbers.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listView1_Numbers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
			this.listView1_Numbers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1_Numbers.FullRowSelect = true;
			this.listView1_Numbers.GridLines = true;
			this.listView1_Numbers.HideSelection = false;
			this.listView1_Numbers.Location = new System.Drawing.Point(3, 3);
			this.listView1_Numbers.Name = "listView1_Numbers";
			this.listView1_Numbers.Size = new System.Drawing.Size(664, 271);
			this.listView1_Numbers.TabIndex = 0;
			this.listView1_Numbers.TabStop = false;
			this.listView1_Numbers.UseCompatibleStateImageBehavior = false;
			this.listView1_Numbers.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Date";
			this.columnHeader1.Width = 156;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Numbers";
			this.columnHeader2.Width = 224;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "State";
			this.columnHeader3.Width = 104;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.Controls.Add(this.button1_Search, 3, 1);
			this.tableLayoutPanel1.Controls.Add(this.dateTimePicker1_from, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.dateTimePicker2_to, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label1_from, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.richTextBox1_TypeNums, 2, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.09804F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.90196F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(684, 52);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// button1_Search
			// 
			this.button1_Search.Dock = System.Windows.Forms.DockStyle.Fill;
			this.button1_Search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1_Search.Location = new System.Drawing.Point(548, 27);
			this.button1_Search.Name = "button1_Search";
			this.button1_Search.Size = new System.Drawing.Size(132, 21);
			this.button1_Search.TabIndex = 1;
			this.button1_Search.Text = "Search";
			this.button1_Search.UseVisualStyleBackColor = true;
			this.button1_Search.Click += new System.EventHandler(this.button1_Search_Click);
			// 
			// dateTimePicker1_from
			// 
			this.dateTimePicker1_from.CustomFormat = "dd/MM/yyyy";
			this.dateTimePicker1_from.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dateTimePicker1_from.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker1_from.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dateTimePicker1_from.Location = new System.Drawing.Point(4, 27);
			this.dateTimePicker1_from.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
			this.dateTimePicker1_from.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
			this.dateTimePicker1_from.Name = "dateTimePicker1_from";
			this.dateTimePicker1_from.Size = new System.Drawing.Size(197, 20);
			this.dateTimePicker1_from.TabIndex = 4;
			this.dateTimePicker1_from.TabStop = false;
			// 
			// dateTimePicker2_to
			// 
			this.dateTimePicker2_to.CustomFormat = "dd/MM/yyyy";
			this.dateTimePicker2_to.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dateTimePicker2_to.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker2_to.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dateTimePicker2_to.Location = new System.Drawing.Point(208, 27);
			this.dateTimePicker2_to.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
			this.dateTimePicker2_to.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
			this.dateTimePicker2_to.Name = "dateTimePicker2_to";
			this.dateTimePicker2_to.Size = new System.Drawing.Size(163, 20);
			this.dateTimePicker2_to.TabIndex = 5;
			this.dateTimePicker2_to.TabStop = false;
			// 
			// label1_from
			// 
			this.label1_from.AutoSize = true;
			this.label1_from.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1_from.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label1_from.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.label1_from.Location = new System.Drawing.Point(4, 1);
			this.label1_from.Name = "label1_from";
			this.label1_from.Size = new System.Drawing.Size(197, 22);
			this.label1_from.TabIndex = 2;
			this.label1_from.Text = "from";
			this.label1_from.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.label1.Location = new System.Drawing.Point(208, 1);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(163, 22);
			this.label1.TabIndex = 3;
			this.label1.Text = "up to";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// richTextBox1_TypeNums
			// 
			this.richTextBox1_TypeNums.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.richTextBox1_TypeNums.CausesValidation = false;
			this.richTextBox1_TypeNums.DetectUrls = false;
			this.richTextBox1_TypeNums.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1_TypeNums.Location = new System.Drawing.Point(378, 27);
			this.richTextBox1_TypeNums.MaxLength = 20;
			this.richTextBox1_TypeNums.Multiline = false;
			this.richTextBox1_TypeNums.Name = "richTextBox1_TypeNums";
			this.richTextBox1_TypeNums.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			this.richTextBox1_TypeNums.ShortcutsEnabled = false;
			this.richTextBox1_TypeNums.Size = new System.Drawing.Size(163, 21);
			this.richTextBox1_TypeNums.TabIndex = 0;
			this.richTextBox1_TypeNums.Text = "";
			this.richTextBox1_TypeNums.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.richTextBox1_TypeNums_KeyPress);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(3, 3);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(678, 303);
			this.tabControl1.TabIndex = 0;
			this.tabControl1.TabStop = false;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.listView1_Numbers);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(670, 277);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Found Results";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.chart1);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(670, 277);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Found Frequency Graph";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// chart1
			// 
			this.chart1.BackColor = System.Drawing.Color.Transparent;
			this.chart1.BorderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.chart1.BorderSkin.BackSecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			chartArea1.AlignmentOrientation = ((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations)((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical | System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Horizontal)));
			chartArea1.AxisX.InterlacedColor = System.Drawing.Color.Transparent;
			chartArea1.AxisX.Interval = 1D;
			chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
			chartArea1.AxisX.IsMarksNextToAxis = false;
			chartArea1.AxisX.IsStartedFromZero = false;
			chartArea1.AxisX.LabelAutoFitMinFontSize = 5;
			chartArea1.AxisX.LabelAutoFitStyle = ((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles)((((((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.IncreaseFont | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.DecreaseFont) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.StaggeredLabels) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep30) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep45) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.WordWrap)));
			chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			chartArea1.AxisX.LabelStyle.Interval = 1D;
			chartArea1.AxisX.LabelStyle.IntervalOffset = 1D;
			chartArea1.AxisX.LabelStyle.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
			chartArea1.AxisX.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
			chartArea1.AxisX.MajorGrid.Enabled = false;
			chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.DimGray;
			chartArea1.AxisX.MajorTickMark.Interval = 1D;
			chartArea1.AxisX.MajorTickMark.IntervalOffset = 1D;
			chartArea1.AxisX.MajorTickMark.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
			chartArea1.AxisX.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
			chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Gray;
			chartArea1.AxisX.MajorTickMark.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
			chartArea1.AxisX.MajorTickMark.Size = 3F;
			chartArea1.AxisX.Maximum = 50D;
			chartArea1.AxisX.MaximumAutoSize = 100F;
			chartArea1.AxisX.Minimum = 0D;
			chartArea1.AxisX.ScaleBreakStyle.BreakLineStyle = System.Windows.Forms.DataVisualization.Charting.BreakLineStyle.None;
			chartArea1.AxisX.ScaleBreakStyle.Spacing = 1D;
			chartArea1.AxisX.ScrollBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			chartArea1.AxisX.ScrollBar.ButtonColor = System.Drawing.Color.White;
			chartArea1.AxisX.ScrollBar.LineColor = System.Drawing.Color.Black;
			chartArea1.AxisX.ScrollBar.Size = 20D;
			chartArea1.AxisX.Title = "Number";
			chartArea1.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			chartArea1.AxisX.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			customLabel1.ForeColor = System.Drawing.Color.Black;
			customLabel1.FromPosition = 4D;
			customLabel1.MarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			chartArea1.AxisX2.CustomLabels.Add(customLabel1);
			chartArea1.AxisX2.CustomLabels.Add(customLabel2);
			chartArea1.AxisX2.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
			chartArea1.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
			chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			chartArea1.AxisY.LabelStyle.Interval = 0D;
			chartArea1.AxisY.LabelStyle.IntervalOffset = 0D;
			chartArea1.AxisY.LabelStyle.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
			chartArea1.AxisY.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
			chartArea1.AxisY.MajorGrid.Interval = 0D;
			chartArea1.AxisY.MajorGrid.IntervalOffset = 0D;
			chartArea1.AxisY.MajorGrid.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
			chartArea1.AxisY.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
			chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
			chartArea1.AxisY.MajorTickMark.Interval = 0D;
			chartArea1.AxisY.MajorTickMark.IntervalOffset = 0D;
			chartArea1.AxisY.MajorTickMark.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
			chartArea1.AxisY.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
			chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.Silver;
			chartArea1.AxisY.MajorTickMark.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
			chartArea1.AxisY.Maximum = 10D;
			chartArea1.AxisY.Minimum = 0D;
			chartArea1.AxisY.ScrollBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			chartArea1.AxisY.ScrollBar.ButtonColor = System.Drawing.Color.White;
			chartArea1.AxisY.ScrollBar.LineColor = System.Drawing.Color.Black;
			chartArea1.AxisY.ScrollBar.Size = 20D;
			chartArea1.AxisY.Title = "Number of times played";
			chartArea1.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			chartArea1.BackColor = System.Drawing.Color.Transparent;
			chartArea1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
			chartArea1.CursorX.IsUserEnabled = true;
			chartArea1.CursorX.IsUserSelectionEnabled = true;
			chartArea1.CursorX.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			chartArea1.CursorY.IsUserEnabled = true;
			chartArea1.CursorY.IsUserSelectionEnabled = true;
			chartArea1.CursorY.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			chartArea1.Name = "ChartArea1";
			this.chart1.ChartAreas.Add(chartArea1);
			this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
			legend1.BackColor = System.Drawing.Color.Transparent;
			legend1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			legend1.DockedToChartArea = "ChartArea1";
			legend1.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Column;
			legend1.MaximumAutoSize = 25F;
			legend1.Name = "Legend1";
			this.chart1.Legends.Add(legend1);
			this.chart1.Location = new System.Drawing.Point(3, 3);
			this.chart1.Name = "chart1";
			this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
			this.chart1.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255))))),
        System.Drawing.Color.Yellow,
        System.Drawing.Color.Cyan,
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0))))),
        System.Drawing.Color.Lime,
        System.Drawing.Color.Red,
        System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))))};
			series1.ChartArea = "ChartArea1";
			series1.Color = System.Drawing.Color.RoyalBlue;
			series1.CustomProperties = "DrawingStyle=Cylinder, EmptyPointValue=Zero, PointWidth=0.7, LabelStyle=Top";
			series1.Legend = "Legend1";
			series1.LegendText = "Main Set";
			series1.Name = "Series2";
			series2.ChartArea = "ChartArea1";
			series2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			series2.CustomProperties = "DrawingStyle=Cylinder, EmptyPointValue=Zero, PointWidth=0.7, LabelStyle=Top";
			series2.Legend = "Legend1";
			series2.LegendText = "Bonus";
			series2.Name = "Series3";
			series3.ChartArea = "ChartArea1";
			series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
			series3.Color = System.Drawing.Color.Fuchsia;
			series3.Legend = "Legend1";
			series3.Name = "Overall";
			this.chart1.Series.Add(series1);
			this.chart1.Series.Add(series2);
			this.chart1.Series.Add(series3);
			this.chart1.Size = new System.Drawing.Size(664, 271);
			this.chart1.TabIndex = 0;
			this.chart1.TabStop = false;
			this.chart1.Text = "chart1";
			title1.BackColor = System.Drawing.Color.Transparent;
			title1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
			title1.DockedToChartArea = "ChartArea1";
			title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			title1.IsDockedInsideChartArea = false;
			title1.Name = "Title1";
			title1.Text = "Graph is based on Found Results.";
			this.chart1.Titles.Add(title1);
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Controls.Add(this.tabControl1, 0, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 52);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(684, 309);
			this.tableLayoutPanel3.TabIndex = 5;
			// 
			// statusStrip1_info
			// 
			this.statusStrip1_info.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
			this.statusStrip1_info.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1_info_label});
			this.statusStrip1_info.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.statusStrip1_info.Location = new System.Drawing.Point(0, 356);
			this.statusStrip1_info.Name = "statusStrip1_info";
			this.statusStrip1_info.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.statusStrip1_info.Size = new System.Drawing.Size(684, 5);
			this.statusStrip1_info.SizingGrip = false;
			this.statusStrip1_info.TabIndex = 6;
			this.statusStrip1_info.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1_info_label
			// 
			this.toolStripStatusLabel1_info_label.Name = "toolStripStatusLabel1_info_label";
			this.toolStripStatusLabel1_info_label.Size = new System.Drawing.Size(0, 0);
			// 
			// Search
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(684, 361);
			this.Controls.Add(this.statusStrip1_info);
			this.Controls.Add(this.tableLayoutPanel3);
			this.Controls.Add(this.tableLayoutPanel1);
			this.MinimumSize = new System.Drawing.Size(700, 400);
			this.Name = "Search";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Search";
			this.Shown += new System.EventHandler(this.Search_Shown);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.statusStrip1_info.ResumeLayout(false);
			this.statusStrip1_info.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ListView listView1_Numbers;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button button1_Search;
		private System.Windows.Forms.RichTextBox richTextBox1_TypeNums;
		private System.Windows.Forms.Label label1_from;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DateTimePicker dateTimePicker1_from;
		private System.Windows.Forms.DateTimePicker dateTimePicker2_to;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.StatusStrip statusStrip1_info;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1_info_label;
	}
}