namespace Brain
{
	partial class DreamGuide
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
			this.TableLayoutPanel_Searchbox_and_Button = new System.Windows.Forms.TableLayoutPanel();
			this.RichTextBox_Searchbox = new System.Windows.Forms.RichTextBox();
			this.TableLayoutPanel_Results_ListView = new System.Windows.Forms.TableLayoutPanel();
			this.ListView_Results = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel_Info = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.TableLayoutPanel_Searchbox_and_Button.SuspendLayout();
			this.TableLayoutPanel_Results_ListView.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// TableLayoutPanel_Searchbox_and_Button
			// 
			this.TableLayoutPanel_Searchbox_and_Button.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.TableLayoutPanel_Searchbox_and_Button.ColumnCount = 1;
			this.TableLayoutPanel_Searchbox_and_Button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
			this.TableLayoutPanel_Searchbox_and_Button.Controls.Add(this.RichTextBox_Searchbox, 0, 0);
			this.TableLayoutPanel_Searchbox_and_Button.Dock = System.Windows.Forms.DockStyle.Top;
			this.TableLayoutPanel_Searchbox_and_Button.Location = new System.Drawing.Point(0, 0);
			this.TableLayoutPanel_Searchbox_and_Button.Name = "TableLayoutPanel_Searchbox_and_Button";
			this.TableLayoutPanel_Searchbox_and_Button.RowCount = 1;
			this.TableLayoutPanel_Searchbox_and_Button.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TableLayoutPanel_Searchbox_and_Button.Size = new System.Drawing.Size(684, 28);
			this.TableLayoutPanel_Searchbox_and_Button.TabIndex = 3;
			// 
			// RichTextBox_Searchbox
			// 
			this.RichTextBox_Searchbox.AutoWordSelection = true;
			this.RichTextBox_Searchbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.RichTextBox_Searchbox.CausesValidation = false;
			this.RichTextBox_Searchbox.DetectUrls = false;
			this.RichTextBox_Searchbox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RichTextBox_Searchbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.RichTextBox_Searchbox.Location = new System.Drawing.Point(4, 4);
			this.RichTextBox_Searchbox.MaxLength = 100;
			this.RichTextBox_Searchbox.Multiline = false;
			this.RichTextBox_Searchbox.Name = "RichTextBox_Searchbox";
			this.RichTextBox_Searchbox.RightMargin = 3;
			this.RichTextBox_Searchbox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			this.RichTextBox_Searchbox.ShortcutsEnabled = false;
			this.RichTextBox_Searchbox.Size = new System.Drawing.Size(676, 20);
			this.RichTextBox_Searchbox.TabIndex = 1;
			this.RichTextBox_Searchbox.Text = "";
			this.RichTextBox_Searchbox.TextChanged += new System.EventHandler(this.RichTextBox_Searchbox_TextChanged);
			// 
			// TableLayoutPanel_Results_ListView
			// 
			this.TableLayoutPanel_Results_ListView.ColumnCount = 1;
			this.TableLayoutPanel_Results_ListView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TableLayoutPanel_Results_ListView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TableLayoutPanel_Results_ListView.Controls.Add(this.ListView_Results, 0, 0);
			this.TableLayoutPanel_Results_ListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TableLayoutPanel_Results_ListView.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
			this.TableLayoutPanel_Results_ListView.Location = new System.Drawing.Point(0, 28);
			this.TableLayoutPanel_Results_ListView.Name = "TableLayoutPanel_Results_ListView";
			this.TableLayoutPanel_Results_ListView.RowCount = 1;
			this.TableLayoutPanel_Results_ListView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TableLayoutPanel_Results_ListView.Size = new System.Drawing.Size(684, 328);
			this.TableLayoutPanel_Results_ListView.TabIndex = 4;
			// 
			// ListView_Results
			// 
			this.ListView_Results.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ListView_Results.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.ListView_Results.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ListView_Results.FullRowSelect = true;
			this.ListView_Results.GridLines = true;
			this.ListView_Results.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.ListView_Results.HideSelection = false;
			this.ListView_Results.Location = new System.Drawing.Point(3, 3);
			this.ListView_Results.Name = "ListView_Results";
			this.ListView_Results.Size = new System.Drawing.Size(678, 322);
			this.ListView_Results.TabIndex = 0;
			this.ListView_Results.TabStop = false;
			this.ListView_Results.UseCompatibleStateImageBehavior = false;
			this.ListView_Results.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Keywords";
			this.columnHeader1.Width = 336;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Lucky Number";
			this.columnHeader2.Width = 124;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_Info});
			this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.statusStrip1.Size = new System.Drawing.Size(684, 5);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 5;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel_Info
			// 
			this.toolStripStatusLabel_Info.Name = "toolStripStatusLabel_Info";
			this.toolStripStatusLabel_Info.Size = new System.Drawing.Size(0, 0);
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.BottomToolStripPanel
			// 
			this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.AutoScroll = true;
			this.toolStripContainer1.ContentPanel.Controls.Add(this.TableLayoutPanel_Results_ListView);
			this.toolStripContainer1.ContentPanel.Controls.Add(this.TableLayoutPanel_Searchbox_and_Button);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(684, 356);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.LeftToolStripPanelVisible = false;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.RightToolStripPanelVisible = false;
			this.toolStripContainer1.Size = new System.Drawing.Size(684, 361);
			this.toolStripContainer1.TabIndex = 6;
			this.toolStripContainer1.Text = "toolStripContainer1";
			this.toolStripContainer1.TopToolStripPanelVisible = false;
			// 
			// DreamGuide
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(684, 361);
			this.Controls.Add(this.toolStripContainer1);
			this.MinimumSize = new System.Drawing.Size(700, 400);
			this.Name = "DreamGuide";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Dream Guide";
			this.Shown += new System.EventHandler(this.DreamGuide_Shown);
			this.TableLayoutPanel_Searchbox_and_Button.ResumeLayout(false);
			this.TableLayoutPanel_Results_ListView.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel TableLayoutPanel_Searchbox_and_Button;
		private System.Windows.Forms.RichTextBox RichTextBox_Searchbox;
		private System.Windows.Forms.TableLayoutPanel TableLayoutPanel_Results_ListView;
		private System.Windows.Forms.ListView ListView_Results;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Info;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
	}
}