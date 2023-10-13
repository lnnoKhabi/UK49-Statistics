
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
			this.statusStrip1_Info = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1_text = new System.Windows.Forms.ToolStripStatusLabel();
			this.listView1_Pairings = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.richTextBox1_TypeNums = new System.Windows.Forms.RichTextBox();
			this.button1_Pair = new System.Windows.Forms.Button();
			this.comboBox1_PairIn = new System.Windows.Forms.ComboBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.label3_sample = new System.Windows.Forms.Label();
			this.label2_objects = new System.Windows.Forms.Label();
			this.textBox1_n_objects = new System.Windows.Forms.TextBox();
			this.textBox1_r_sample = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.button1_clear = new System.Windows.Forms.Button();
			this.button1_calculate = new System.Windows.Forms.Button();
			this.label4_Answer = new System.Windows.Forms.Label();
			this.tableLayoutPanel1_Main.SuspendLayout();
			this.statusStrip1_Info.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1_Main
			// 
			this.tableLayoutPanel1_Main.ColumnCount = 1;
			this.tableLayoutPanel1_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1_Main.Controls.Add(this.statusStrip1_Info, 0, 3);
			this.tableLayoutPanel1_Main.Controls.Add(this.listView1_Pairings, 0, 1);
			this.tableLayoutPanel1_Main.Controls.Add(this.tableLayoutPanel1, 0, 0);
			this.tableLayoutPanel1_Main.Controls.Add(this.tableLayoutPanel2, 0, 2);
			this.tableLayoutPanel1_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1_Main.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1_Main.Name = "tableLayoutPanel1_Main";
			this.tableLayoutPanel1_Main.RowCount = 4;
			this.tableLayoutPanel1_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel1_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
			this.tableLayoutPanel1_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.tableLayoutPanel1_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1_Main.Size = new System.Drawing.Size(684, 361);
			this.tableLayoutPanel1_Main.TabIndex = 0;
			// 
			// statusStrip1_Info
			// 
			this.statusStrip1_Info.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1_text});
			this.statusStrip1_Info.Location = new System.Drawing.Point(0, 340);
			this.statusStrip1_Info.Name = "statusStrip1_Info";
			this.statusStrip1_Info.Size = new System.Drawing.Size(684, 21);
			this.statusStrip1_Info.TabIndex = 1;
			this.statusStrip1_Info.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1_text
			// 
			this.toolStripStatusLabel1_text.Name = "toolStripStatusLabel1_text";
			this.toolStripStatusLabel1_text.Size = new System.Drawing.Size(0, 16);
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
			this.listView1_Pairings.Location = new System.Drawing.Point(3, 37);
			this.listView1_Pairings.Name = "listView1_Pairings";
			this.listView1_Pairings.Size = new System.Drawing.Size(678, 198);
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
			this.tableLayoutPanel1.Size = new System.Drawing.Size(678, 28);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// richTextBox1_TypeNums
			// 
			this.richTextBox1_TypeNums.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBox1_TypeNums.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.richTextBox1_TypeNums.CausesValidation = false;
			this.richTextBox1_TypeNums.DetectUrls = false;
			this.richTextBox1_TypeNums.Location = new System.Drawing.Point(3, 3);
			this.richTextBox1_TypeNums.MaxLength = 150;
			this.richTextBox1_TypeNums.Multiline = false;
			this.richTextBox1_TypeNums.Name = "richTextBox1_TypeNums";
			this.richTextBox1_TypeNums.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			this.richTextBox1_TypeNums.ShortcutsEnabled = false;
			this.richTextBox1_TypeNums.Size = new System.Drawing.Size(460, 22);
			this.richTextBox1_TypeNums.TabIndex = 4;
			this.richTextBox1_TypeNums.Text = "";
			// 
			// button1_Pair
			// 
			this.button1_Pair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.button1_Pair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1_Pair.Location = new System.Drawing.Point(536, 3);
			this.button1_Pair.Name = "button1_Pair";
			this.button1_Pair.Size = new System.Drawing.Size(139, 22);
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
			this.comboBox1_PairIn.Location = new System.Drawing.Point(469, 3);
			this.comboBox1_PairIn.Name = "comboBox1_PairIn";
			this.comboBox1_PairIn.Size = new System.Drawing.Size(61, 21);
			this.comboBox1_PairIn.TabIndex = 3;
			this.comboBox1_PairIn.Text = "Choose";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Controls.Add(this.label2, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.label1);
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 1, 1);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 241);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.04167F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 73.95834F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(678, 96);
			this.tableLayoutPanel2.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Left;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label2.Location = new System.Drawing.Point(342, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 24);
			this.label2.TabIndex = 3;
			this.label2.Text = "Calculator";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Right;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label1.Location = new System.Drawing.Point(260, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Combination";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 167F));
			this.tableLayoutPanel3.Controls.Add(this.label3_sample, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.label2_objects, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.textBox1_n_objects, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.textBox1_r_sample, 1, 1);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 27);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(333, 66);
			this.tableLayoutPanel3.TabIndex = 1;
			// 
			// label3_sample
			// 
			this.label3_sample.AutoSize = true;
			this.label3_sample.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3_sample.Location = new System.Drawing.Point(4, 33);
			this.label3_sample.Name = "label3_sample";
			this.label3_sample.Size = new System.Drawing.Size(157, 32);
			this.label3_sample.TabIndex = 3;
			this.label3_sample.Text = "r (sample)";
			this.label3_sample.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2_objects
			// 
			this.label2_objects.AutoSize = true;
			this.label2_objects.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2_objects.Location = new System.Drawing.Point(4, 1);
			this.label2_objects.Name = "label2_objects";
			this.label2_objects.Size = new System.Drawing.Size(157, 31);
			this.label2_objects.TabIndex = 0;
			this.label2_objects.Text = "n (objects)";
			this.label2_objects.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// textBox1_n_objects
			// 
			this.textBox1_n_objects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1_n_objects.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox1_n_objects.Location = new System.Drawing.Point(168, 6);
			this.textBox1_n_objects.Name = "textBox1_n_objects";
			this.textBox1_n_objects.Size = new System.Drawing.Size(161, 20);
			this.textBox1_n_objects.TabIndex = 1;
			// 
			// textBox1_r_sample
			// 
			this.textBox1_r_sample.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1_r_sample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox1_r_sample.Location = new System.Drawing.Point(168, 39);
			this.textBox1_r_sample.Name = "textBox1_r_sample";
			this.textBox1_r_sample.Size = new System.Drawing.Size(161, 20);
			this.textBox1_r_sample.TabIndex = 2;
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel4.ColumnCount = 2;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel4.Controls.Add(this.button1_clear, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.button1_calculate, 0, 1);
			this.tableLayoutPanel4.Controls.Add(this.label4_Answer, 1, 1);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(342, 27);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 2;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(333, 66);
			this.tableLayoutPanel4.TabIndex = 2;
			// 
			// button1_clear
			// 
			this.button1_clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.button1_clear.Location = new System.Drawing.Point(4, 5);
			this.button1_clear.Name = "button1_clear";
			this.button1_clear.Size = new System.Drawing.Size(159, 23);
			this.button1_clear.TabIndex = 0;
			this.button1_clear.Text = "Clear";
			this.button1_clear.UseVisualStyleBackColor = true;
			this.button1_clear.Click += new System.EventHandler(this.button1_clear_Click);
			// 
			// button1_calculate
			// 
			this.button1_calculate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.button1_calculate.Location = new System.Drawing.Point(4, 37);
			this.button1_calculate.Name = "button1_calculate";
			this.button1_calculate.Size = new System.Drawing.Size(159, 23);
			this.button1_calculate.TabIndex = 1;
			this.button1_calculate.Text = "Calculate";
			this.button1_calculate.UseVisualStyleBackColor = true;
			this.button1_calculate.Click += new System.EventHandler(this.button1_calculate_Click);
			// 
			// label4_Answer
			// 
			this.label4_Answer.AutoSize = true;
			this.label4_Answer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4_Answer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4_Answer.ForeColor = System.Drawing.Color.Green;
			this.label4_Answer.Location = new System.Drawing.Point(170, 33);
			this.label4_Answer.Name = "label4_Answer";
			this.label4_Answer.Size = new System.Drawing.Size(159, 32);
			this.label4_Answer.TabIndex = 2;
			this.label4_Answer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Pairing
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(684, 361);
			this.Controls.Add(this.tableLayoutPanel1_Main);
			this.Name = "Pairing";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Pairing";
			this.tableLayoutPanel1_Main.ResumeLayout(false);
			this.tableLayoutPanel1_Main.PerformLayout();
			this.statusStrip1_Info.ResumeLayout(false);
			this.statusStrip1_Info.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			this.ResumeLayout(false);

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
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.Label label3_sample;
		private System.Windows.Forms.Label label2_objects;
		private System.Windows.Forms.TextBox textBox1_n_objects;
		private System.Windows.Forms.TextBox textBox1_r_sample;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.Button button1_clear;
		private System.Windows.Forms.Button button1_calculate;
		private System.Windows.Forms.Label label4_Answer;
		private System.Windows.Forms.Label label2;
	}
}