
namespace Brain
{
	partial class Pairing
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
			this.tableLayoutPanel1_Main = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.button1_Pair = new System.Windows.Forms.Button();
			this.comboBox1_PairIn = new System.Windows.Forms.ComboBox();
			this.richTextBox1_TypeNums = new System.Windows.Forms.RichTextBox();
			this.listView1_Pairings = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.statusStrip1_Info = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1_text = new System.Windows.Forms.ToolStripStatusLabel();
			this.tableLayoutPanel1_Main.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.statusStrip1_Info.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1_Main
			// 
			this.tableLayoutPanel1_Main.ColumnCount = 1;
			this.tableLayoutPanel1_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1_Main.Controls.Add(this.listView1_Pairings, 0, 1);
			this.tableLayoutPanel1_Main.Controls.Add(this.tableLayoutPanel1, 0, 0);
			this.tableLayoutPanel1_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1_Main.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1_Main.Name = "tableLayoutPanel1_Main";
			this.tableLayoutPanel1_Main.RowCount = 2;
			this.tableLayoutPanel1_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.80332F));
			this.tableLayoutPanel1_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.19668F));
			this.tableLayoutPanel1_Main.Size = new System.Drawing.Size(684, 361);
			this.tableLayoutPanel1_Main.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 67F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
			this.tableLayoutPanel1.Controls.Add(this.richTextBox1_TypeNums, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.button1_Pair, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.comboBox1_PairIn, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(678, 32);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// button1_Pair
			// 
			this.button1_Pair.Dock = System.Windows.Forms.DockStyle.Fill;
			this.button1_Pair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1_Pair.Location = new System.Drawing.Point(536, 3);
			this.button1_Pair.Name = "button1_Pair";
			this.button1_Pair.Size = new System.Drawing.Size(139, 26);
			this.button1_Pair.TabIndex = 2;
			this.button1_Pair.Text = "Pair";
			this.button1_Pair.UseVisualStyleBackColor = true;
			this.button1_Pair.Click += new System.EventHandler(this.button1_Pair_Click);
			// 
			// comboBox1_PairIn
			// 
			this.comboBox1_PairIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox1_PairIn.FormattingEnabled = true;
			this.comboBox1_PairIn.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5"});
			this.comboBox1_PairIn.Location = new System.Drawing.Point(469, 5);
			this.comboBox1_PairIn.Name = "comboBox1_PairIn";
			this.comboBox1_PairIn.Size = new System.Drawing.Size(61, 21);
			this.comboBox1_PairIn.TabIndex = 3;
			this.comboBox1_PairIn.Text = "Choose";
			// 
			// richTextBox1_TypeNums
			// 
			this.richTextBox1_TypeNums.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.richTextBox1_TypeNums.CausesValidation = false;
			this.richTextBox1_TypeNums.DetectUrls = false;
			this.richTextBox1_TypeNums.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1_TypeNums.Location = new System.Drawing.Point(3, 3);
			this.richTextBox1_TypeNums.MaxLength = 150;
			this.richTextBox1_TypeNums.Multiline = false;
			this.richTextBox1_TypeNums.Name = "richTextBox1_TypeNums";
			this.richTextBox1_TypeNums.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			this.richTextBox1_TypeNums.ShortcutsEnabled = false;
			this.richTextBox1_TypeNums.Size = new System.Drawing.Size(460, 26);
			this.richTextBox1_TypeNums.TabIndex = 4;
			this.richTextBox1_TypeNums.Text = "";
			// 
			// listView1_Pairings
			// 
			this.listView1_Pairings.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listView1_Pairings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.listView1_Pairings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1_Pairings.FullRowSelect = true;
			this.listView1_Pairings.GridLines = true;
			this.listView1_Pairings.HideSelection = false;
			this.listView1_Pairings.Location = new System.Drawing.Point(3, 41);
			this.listView1_Pairings.Name = "listView1_Pairings";
			this.listView1_Pairings.Size = new System.Drawing.Size(678, 317);
			this.listView1_Pairings.TabIndex = 1;
			this.listView1_Pairings.TabStop = false;
			this.listView1_Pairings.UseCompatibleStateImageBehavior = false;
			this.listView1_Pairings.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Index";
			this.columnHeader1.Width = 83;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Pairings";
			this.columnHeader2.Width = 333;
			// 
			// statusStrip1_Info
			// 
			this.statusStrip1_Info.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1_text});
			this.statusStrip1_Info.Location = new System.Drawing.Point(0, 339);
			this.statusStrip1_Info.Name = "statusStrip1_Info";
			this.statusStrip1_Info.Size = new System.Drawing.Size(684, 22);
			this.statusStrip1_Info.TabIndex = 1;
			this.statusStrip1_Info.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1_text
			// 
			this.toolStripStatusLabel1_text.Name = "toolStripStatusLabel1_text";
			this.toolStripStatusLabel1_text.Size = new System.Drawing.Size(0, 17);
			// 
			// Pairing
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(684, 361);
			this.Controls.Add(this.statusStrip1_Info);
			this.Controls.Add(this.tableLayoutPanel1_Main);
			this.MaximumSize = new System.Drawing.Size(700, 400);
			this.Name = "Pairing";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Pairing";
			this.tableLayoutPanel1_Main.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.statusStrip1_Info.ResumeLayout(false);
			this.statusStrip1_Info.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1_Main;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button button1_Pair;
		private System.Windows.Forms.ComboBox comboBox1_PairIn;
		private System.Windows.Forms.RichTextBox richTextBox1_TypeNums;
		private System.Windows.Forms.ListView listView1_Pairings;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.StatusStrip statusStrip1_Info;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1_text;
	}
}