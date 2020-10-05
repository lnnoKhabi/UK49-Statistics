namespace Brain
{
	partial class ReadAloud
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
			this.tableLayoutPanel1_with_Listview = new System.Windows.Forms.TableLayoutPanel();
			this.listView1_Numbers = new System.Windows.Forms.ListView();
			this.columnHeader1_Date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader1_Numbers = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tableLayoutPanel2_Buttons = new System.Windows.Forms.TableLayoutPanel();
			this.dateTimePicker1_Start = new System.Windows.Forms.DateTimePicker();
			this.dateTimePicker1_End = new System.Windows.Forms.DateTimePicker();
			this.button2_Import = new System.Windows.Forms.Button();
			this.button1_Read = new System.Windows.Forms.Button();
			this.label1_End = new System.Windows.Forms.Label();
			this.label1_Start = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.button1 = new System.Windows.Forms.Button();
			this.trackBar1_SpeechSpeed = new System.Windows.Forms.TrackBar();
			this.tableLayoutPanel1_with_Listview.SuspendLayout();
			this.tableLayoutPanel2_Buttons.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1_SpeechSpeed)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1_with_Listview
			// 
			this.tableLayoutPanel1_with_Listview.ColumnCount = 1;
			this.tableLayoutPanel1_with_Listview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1_with_Listview.Controls.Add(this.listView1_Numbers, 0, 0);
			this.tableLayoutPanel1_with_Listview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1_with_Listview.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
			this.tableLayoutPanel1_with_Listview.Location = new System.Drawing.Point(0, 51);
			this.tableLayoutPanel1_with_Listview.Name = "tableLayoutPanel1_with_Listview";
			this.tableLayoutPanel1_with_Listview.RowCount = 1;
			this.tableLayoutPanel1_with_Listview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1_with_Listview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 280F));
			this.tableLayoutPanel1_with_Listview.Size = new System.Drawing.Size(684, 280);
			this.tableLayoutPanel1_with_Listview.TabIndex = 0;
			// 
			// listView1_Numbers
			// 
			this.listView1_Numbers.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listView1_Numbers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1_Date,
            this.columnHeader1_Numbers});
			this.listView1_Numbers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1_Numbers.FullRowSelect = true;
			this.listView1_Numbers.GridLines = true;
			this.listView1_Numbers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listView1_Numbers.HideSelection = false;
			this.listView1_Numbers.Location = new System.Drawing.Point(3, 3);
			this.listView1_Numbers.MultiSelect = false;
			this.listView1_Numbers.Name = "listView1_Numbers";
			this.listView1_Numbers.Size = new System.Drawing.Size(678, 274);
			this.listView1_Numbers.TabIndex = 0;
			this.listView1_Numbers.UseCompatibleStateImageBehavior = false;
			this.listView1_Numbers.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1_Date
			// 
			this.columnHeader1_Date.Text = "Date";
			this.columnHeader1_Date.Width = 136;
			// 
			// columnHeader1_Numbers
			// 
			this.columnHeader1_Numbers.Text = "Numbers";
			this.columnHeader1_Numbers.Width = 448;
			// 
			// tableLayoutPanel2_Buttons
			// 
			this.tableLayoutPanel2_Buttons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel2_Buttons.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel2_Buttons.ColumnCount = 4;
			this.tableLayoutPanel2_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.tableLayoutPanel2_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.tableLayoutPanel2_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel2_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel2_Buttons.Controls.Add(this.dateTimePicker1_Start, 0, 1);
			this.tableLayoutPanel2_Buttons.Controls.Add(this.dateTimePicker1_End, 1, 1);
			this.tableLayoutPanel2_Buttons.Controls.Add(this.button2_Import, 2, 1);
			this.tableLayoutPanel2_Buttons.Controls.Add(this.button1_Read, 3, 1);
			this.tableLayoutPanel2_Buttons.Controls.Add(this.label1_End, 1, 0);
			this.tableLayoutPanel2_Buttons.Controls.Add(this.label1_Start, 0, 0);
			this.tableLayoutPanel2_Buttons.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel2_Buttons.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2_Buttons.Name = "tableLayoutPanel2_Buttons";
			this.tableLayoutPanel2_Buttons.RowCount = 2;
			this.tableLayoutPanel2_Buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.1F));
			this.tableLayoutPanel2_Buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.9F));
			this.tableLayoutPanel2_Buttons.Size = new System.Drawing.Size(684, 51);
			this.tableLayoutPanel2_Buttons.TabIndex = 1;
			// 
			// dateTimePicker1_Start
			// 
			this.dateTimePicker1_Start.CustomFormat = "dd/MM/yyyy";
			this.dateTimePicker1_Start.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dateTimePicker1_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker1_Start.Location = new System.Drawing.Point(4, 26);
			this.dateTimePicker1_Start.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
			this.dateTimePicker1_Start.Name = "dateTimePicker1_Start";
			this.dateTimePicker1_Start.Size = new System.Drawing.Size(197, 20);
			this.dateTimePicker1_Start.TabIndex = 2;
			this.dateTimePicker1_Start.Value = new System.DateTime(2020, 7, 19, 2, 9, 13, 0);
			// 
			// dateTimePicker1_End
			// 
			this.dateTimePicker1_End.CustomFormat = "dd/MM/yyyy";
			this.dateTimePicker1_End.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dateTimePicker1_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker1_End.Location = new System.Drawing.Point(208, 26);
			this.dateTimePicker1_End.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
			this.dateTimePicker1_End.Name = "dateTimePicker1_End";
			this.dateTimePicker1_End.Size = new System.Drawing.Size(197, 20);
			this.dateTimePicker1_End.TabIndex = 3;
			this.dateTimePicker1_End.Value = new System.DateTime(2020, 12, 25, 23, 59, 59, 0);
			// 
			// button2_Import
			// 
			this.button2_Import.Dock = System.Windows.Forms.DockStyle.Fill;
			this.button2_Import.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2_Import.Location = new System.Drawing.Point(412, 26);
			this.button2_Import.Name = "button2_Import";
			this.button2_Import.Size = new System.Drawing.Size(129, 21);
			this.button2_Import.TabIndex = 5;
			this.button2_Import.Text = "Get Numbers";
			this.button2_Import.UseVisualStyleBackColor = true;
			this.button2_Import.Click += new System.EventHandler(this.button2_Import_Click);
			// 
			// button1_Read
			// 
			this.button1_Read.Dock = System.Windows.Forms.DockStyle.Fill;
			this.button1_Read.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1_Read.Location = new System.Drawing.Point(548, 26);
			this.button1_Read.Name = "button1_Read";
			this.button1_Read.Size = new System.Drawing.Size(132, 21);
			this.button1_Read.TabIndex = 4;
			this.button1_Read.Text = "Start";
			this.button1_Read.UseVisualStyleBackColor = true;
			this.button1_Read.Click += new System.EventHandler(this.button1_Read_Click);
			// 
			// label1_End
			// 
			this.label1_End.AutoSize = true;
			this.label1_End.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1_End.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label1_End.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.label1_End.Location = new System.Drawing.Point(205, 1);
			this.label1_End.Margin = new System.Windows.Forms.Padding(0);
			this.label1_End.Name = "label1_End";
			this.label1_End.Size = new System.Drawing.Size(203, 21);
			this.label1_End.TabIndex = 1;
			this.label1_End.Text = "up to";
			this.label1_End.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1_Start
			// 
			this.label1_Start.AutoSize = true;
			this.label1_Start.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1_Start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label1_Start.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.label1_Start.Location = new System.Drawing.Point(1, 1);
			this.label1_Start.Margin = new System.Windows.Forms.Padding(0);
			this.label1_Start.Name = "label1_Start";
			this.label1_Start.Size = new System.Drawing.Size(203, 21);
			this.label1_Start.TabIndex = 0;
			this.label1_Start.Text = "from";
			this.label1_Start.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.92157F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.07843F));
			this.tableLayoutPanel1.Controls.Add(this.button1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.trackBar1_SpeechSpeed, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 331);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(684, 30);
			this.tableLayoutPanel1.TabIndex = 6;
			// 
			// button1
			// 
			this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.button1.Enabled = false;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(542, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(139, 24);
			this.button1.TabIndex = 5;
			this.button1.Text = "Pause";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// trackBar1_SpeechSpeed
			// 
			this.trackBar1_SpeechSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
			this.trackBar1_SpeechSpeed.Enabled = false;
			this.trackBar1_SpeechSpeed.LargeChange = 1;
			this.trackBar1_SpeechSpeed.Location = new System.Drawing.Point(3, 3);
			this.trackBar1_SpeechSpeed.Name = "trackBar1_SpeechSpeed";
			this.trackBar1_SpeechSpeed.Size = new System.Drawing.Size(533, 24);
			this.trackBar1_SpeechSpeed.TabIndex = 6;
			this.trackBar1_SpeechSpeed.Scroll += new System.EventHandler(this.trackBar1_SpeechSpeed_Scroll);
			// 
			// ReadAloud
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(684, 361);
			this.Controls.Add(this.tableLayoutPanel1_with_Listview);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.tableLayoutPanel2_Buttons);
			this.HelpButton = true;
			this.MinimumSize = new System.Drawing.Size(700, 400);
			this.Name = "ReadAloud";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Reader";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReadAloud_FormClosing);
			this.Shown += new System.EventHandler(this.ReadAloud_Shown);
			this.tableLayoutPanel1_with_Listview.ResumeLayout(false);
			this.tableLayoutPanel2_Buttons.ResumeLayout(false);
			this.tableLayoutPanel2_Buttons.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1_SpeechSpeed)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1_with_Listview;
		private System.Windows.Forms.ListView listView1_Numbers;
		private System.Windows.Forms.ColumnHeader columnHeader1_Date;
		private System.Windows.Forms.ColumnHeader columnHeader1_Numbers;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2_Buttons;
		private System.Windows.Forms.Label label1_End;
		private System.Windows.Forms.Label label1_Start;
		private System.Windows.Forms.DateTimePicker dateTimePicker1_Start;
		private System.Windows.Forms.DateTimePicker dateTimePicker1_End;
		private System.Windows.Forms.Button button1_Read;
		private System.Windows.Forms.Button button2_Import;
		private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TrackBar trackBar1_SpeechSpeed;
	}
}