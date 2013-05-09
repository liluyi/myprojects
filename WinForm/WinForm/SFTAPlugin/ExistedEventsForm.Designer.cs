namespace SFTAPlugin
{
    partial class ExistedEventsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
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
            this.components = new System.ComponentModel.Container();
            this.currentpjdataGridView = new System.Windows.Forms.DataGridView();
            this.cpnodeIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventDescriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.keytextBox = new System.Windows.Forms.TextBox();
            this.CurrentProjectTab = new System.Windows.Forms.TabControl();
            this.SFTADatabaseTab = new System.Windows.Forms.TabPage();
            this.DBdataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NodeDataIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchDBtextBox = new System.Windows.Forms.TextBox();
            this.currentProjectTabPage = new System.Windows.Forms.TabPage();
            this.IPOtabPage = new System.Windows.Forms.TabPage();
            this.IPOdataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddToDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.currentpjdataGridView)).BeginInit();
            this.CurrentProjectTab.SuspendLayout();
            this.SFTADatabaseTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBdataGridView)).BeginInit();
            this.currentProjectTabPage.SuspendLayout();
            this.IPOtabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IPOdataGridView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // currentpjdataGridView
            // 
            this.currentpjdataGridView.AllowDrop = true;
            this.currentpjdataGridView.AllowUserToAddRows = false;
            this.currentpjdataGridView.AllowUserToDeleteRows = false;
            this.currentpjdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.currentpjdataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cpnodeIDColumn,
            this.dataIDColumn,
            this.EventNameColumn,
            this.EventDescriptionColumn});
            this.currentpjdataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentpjdataGridView.Location = new System.Drawing.Point(3, 24);
            this.currentpjdataGridView.MultiSelect = false;
            this.currentpjdataGridView.Name = "currentpjdataGridView";
            this.currentpjdataGridView.ReadOnly = true;
            this.currentpjdataGridView.RowTemplate.Height = 23;
            this.currentpjdataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.currentpjdataGridView.Size = new System.Drawing.Size(430, 209);
            this.currentpjdataGridView.TabIndex = 0;
            this.currentpjdataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            // 
            // cpnodeIDColumn
            // 
            this.cpnodeIDColumn.HeaderText = "Column1";
            this.cpnodeIDColumn.Name = "cpnodeIDColumn";
            this.cpnodeIDColumn.ReadOnly = true;
            this.cpnodeIDColumn.Visible = false;
            // 
            // dataIDColumn
            // 
            this.dataIDColumn.HeaderText = "Column1";
            this.dataIDColumn.Name = "dataIDColumn";
            this.dataIDColumn.ReadOnly = true;
            this.dataIDColumn.Visible = false;
            // 
            // EventNameColumn
            // 
            this.EventNameColumn.HeaderText = "事件名称";
            this.EventNameColumn.Name = "EventNameColumn";
            this.EventNameColumn.ReadOnly = true;
            this.EventNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.EventNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // EventDescriptionColumn
            // 
            this.EventDescriptionColumn.HeaderText = "事件描述信息";
            this.EventDescriptionColumn.Name = "EventDescriptionColumn";
            this.EventDescriptionColumn.ReadOnly = true;
            this.EventDescriptionColumn.Width = 300;
            // 
            // keytextBox
            // 
            this.keytextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.keytextBox.Location = new System.Drawing.Point(3, 3);
            this.keytextBox.Name = "keytextBox";
            this.keytextBox.Size = new System.Drawing.Size(430, 21);
            this.keytextBox.TabIndex = 1;
            this.keytextBox.Text = "键入内容以搜索";
            this.keytextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.keytextBox_MouseClick);
            this.keytextBox.TextChanged += new System.EventHandler(this.keytextBox_TextChanged);
            this.keytextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keytextBox_KeyUp);
            // 
            // CurrentProjectTab
            // 
            this.CurrentProjectTab.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.CurrentProjectTab.AllowDrop = true;
            this.CurrentProjectTab.Controls.Add(this.SFTADatabaseTab);
            this.CurrentProjectTab.Controls.Add(this.currentProjectTabPage);
            this.CurrentProjectTab.Controls.Add(this.IPOtabPage);
            this.CurrentProjectTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CurrentProjectTab.HotTrack = true;
            this.CurrentProjectTab.Location = new System.Drawing.Point(0, 0);
            this.CurrentProjectTab.Name = "CurrentProjectTab";
            this.CurrentProjectTab.SelectedIndex = 0;
            this.CurrentProjectTab.Size = new System.Drawing.Size(444, 262);
            this.CurrentProjectTab.TabIndex = 1;
            // 
            // SFTADatabaseTab
            // 
            this.SFTADatabaseTab.AllowDrop = true;
            this.SFTADatabaseTab.Controls.Add(this.DBdataGridView);
            this.SFTADatabaseTab.Controls.Add(this.searchDBtextBox);
            this.SFTADatabaseTab.Location = new System.Drawing.Point(4, 4);
            this.SFTADatabaseTab.Name = "SFTADatabaseTab";
            this.SFTADatabaseTab.Padding = new System.Windows.Forms.Padding(3);
            this.SFTADatabaseTab.Size = new System.Drawing.Size(436, 236);
            this.SFTADatabaseTab.TabIndex = 1;
            this.SFTADatabaseTab.Text = "SFTA库";
            this.SFTADatabaseTab.UseVisualStyleBackColor = true;
            // 
            // DBdataGridView
            // 
            this.DBdataGridView.AllowDrop = true;
            this.DBdataGridView.AllowUserToAddRows = false;
            this.DBdataGridView.AllowUserToDeleteRows = false;
            this.DBdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DBdataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.NodeDataIDColumn,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.DBdataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DBdataGridView.Location = new System.Drawing.Point(3, 24);
            this.DBdataGridView.MultiSelect = false;
            this.DBdataGridView.Name = "DBdataGridView";
            this.DBdataGridView.ReadOnly = true;
            this.DBdataGridView.RowTemplate.Height = 23;
            this.DBdataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DBdataGridView.Size = new System.Drawing.Size(430, 209);
            this.DBdataGridView.TabIndex = 2;
            this.DBdataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DBdataGridView_CellMouseDown);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // NodeDataIDColumn
            // 
            this.NodeDataIDColumn.HeaderText = "Column1";
            this.NodeDataIDColumn.Name = "NodeDataIDColumn";
            this.NodeDataIDColumn.ReadOnly = true;
            this.NodeDataIDColumn.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "事件名称";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "事件描述信息";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 300;
            // 
            // searchDBtextBox
            // 
            this.searchDBtextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchDBtextBox.Location = new System.Drawing.Point(3, 3);
            this.searchDBtextBox.Name = "searchDBtextBox";
            this.searchDBtextBox.Size = new System.Drawing.Size(430, 21);
            this.searchDBtextBox.TabIndex = 3;
            this.searchDBtextBox.Text = "键入内容以搜索";
            this.searchDBtextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.searchDBtextBox_MouseClick);
            this.searchDBtextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // currentProjectTabPage
            // 
            this.currentProjectTabPage.AllowDrop = true;
            this.currentProjectTabPage.Controls.Add(this.currentpjdataGridView);
            this.currentProjectTabPage.Controls.Add(this.keytextBox);
            this.currentProjectTabPage.Location = new System.Drawing.Point(4, 4);
            this.currentProjectTabPage.Name = "currentProjectTabPage";
            this.currentProjectTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.currentProjectTabPage.Size = new System.Drawing.Size(436, 236);
            this.currentProjectTabPage.TabIndex = 0;
            this.currentProjectTabPage.Text = "当前工程";
            this.currentProjectTabPage.UseVisualStyleBackColor = true;
            // 
            // IPOtabPage
            // 
            this.IPOtabPage.Controls.Add(this.IPOdataGridView);
            this.IPOtabPage.Location = new System.Drawing.Point(4, 4);
            this.IPOtabPage.Name = "IPOtabPage";
            this.IPOtabPage.Size = new System.Drawing.Size(436, 236);
            this.IPOtabPage.TabIndex = 2;
            this.IPOtabPage.Text = "IPO分析结果";
            this.IPOtabPage.UseVisualStyleBackColor = true;
            // 
            // IPOdataGridView
            // 
            this.IPOdataGridView.AllowDrop = true;
            this.IPOdataGridView.AllowUserToAddRows = false;
            this.IPOdataGridView.AllowUserToDeleteRows = false;
            this.IPOdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.IPOdataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7});
            this.IPOdataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IPOdataGridView.Location = new System.Drawing.Point(0, 0);
            this.IPOdataGridView.MultiSelect = false;
            this.IPOdataGridView.Name = "IPOdataGridView";
            this.IPOdataGridView.ReadOnly = true;
            this.IPOdataGridView.RowTemplate.Height = 23;
            this.IPOdataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.IPOdataGridView.Size = new System.Drawing.Size(436, 236);
            this.IPOdataGridView.TabIndex = 1;
            this.IPOdataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.IPOdataGridView_CellMouseDown);
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Visible = false;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "事件名称";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "事件描述信息";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 300;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddToDBToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(243, 26);
            // 
            // AddToDBToolStripMenuItem
            // 
            this.AddToDBToolStripMenuItem.Name = "AddToDBToolStripMenuItem";
            this.AddToDBToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.AddToDBToolStripMenuItem.Text = "将事件及全部子节点加入工具库";
            this.AddToDBToolStripMenuItem.Click += new System.EventHandler(this.AddToDBToolStripMenuItem_Click);
            // 
            // ExistedEventsForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 262);
            this.Controls.Add(this.CurrentProjectTab);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HideOnClose = true;
            this.Name = "ExistedEventsForm";
            this.Text = "已有事件";
            this.Click += new System.EventHandler(this.ExistedEventsForm_Click);
            ((System.ComponentModel.ISupportInitialize)(this.currentpjdataGridView)).EndInit();
            this.CurrentProjectTab.ResumeLayout(false);
            this.SFTADatabaseTab.ResumeLayout(false);
            this.SFTADatabaseTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBdataGridView)).EndInit();
            this.currentProjectTabPage.ResumeLayout(false);
            this.currentProjectTabPage.PerformLayout();
            this.IPOtabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.IPOdataGridView)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView currentpjdataGridView;
        private System.Windows.Forms.TextBox keytextBox;
        private System.Windows.Forms.TabControl CurrentProjectTab;
        private System.Windows.Forms.TabPage SFTADatabaseTab;
        private System.Windows.Forms.TabPage currentProjectTabPage;
        private System.Windows.Forms.DataGridView DBdataGridView;
        private System.Windows.Forms.TextBox searchDBtextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn cpnodeIDColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataIDColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventDescriptionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NodeDataIDColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem AddToDBToolStripMenuItem;
        private System.Windows.Forms.TabPage IPOtabPage;
        private System.Windows.Forms.DataGridView IPOdataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

    }
}