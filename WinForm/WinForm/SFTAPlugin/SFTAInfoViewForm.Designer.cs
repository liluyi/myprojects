namespace SFTAPlugin
{
    partial class SFTAInfoViewForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.infoTabPage = new System.Windows.Forms.TabPage();
            this.outputRichTextBox = new System.Windows.Forms.RichTextBox();
            this.miniCutTabPage = new System.Windows.Forms.TabPage();
            this.copyminicutset = new System.Windows.Forms.Button();
            this.minicutdataGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NaviColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tabControl1.SuspendLayout();
            this.infoTabPage.SuspendLayout();
            this.miniCutTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minicutdataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.infoTabPage);
            this.tabControl1.Controls.Add(this.miniCutTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(720, 156);
            this.tabControl1.TabIndex = 1;
            // 
            // infoTabPage
            // 
            this.infoTabPage.Controls.Add(this.outputRichTextBox);
            this.infoTabPage.Location = new System.Drawing.Point(4, 4);
            this.infoTabPage.Name = "infoTabPage";
            this.infoTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.infoTabPage.Size = new System.Drawing.Size(712, 130);
            this.infoTabPage.TabIndex = 0;
            this.infoTabPage.Text = "信息";
            this.infoTabPage.ToolTipText = "信息";
            this.infoTabPage.UseVisualStyleBackColor = true;
            // 
            // outputRichTextBox
            // 
            this.outputRichTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.outputRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.outputRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputRichTextBox.Location = new System.Drawing.Point(3, 3);
            this.outputRichTextBox.Name = "outputRichTextBox";
            this.outputRichTextBox.ReadOnly = true;
            this.outputRichTextBox.Size = new System.Drawing.Size(706, 124);
            this.outputRichTextBox.TabIndex = 0;
            this.outputRichTextBox.Text = "欢迎使用CASREE平台软件故障树分析工具！\n";
            // 
            // miniCutTabPage
            // 
            this.miniCutTabPage.Controls.Add(this.copyminicutset);
            this.miniCutTabPage.Controls.Add(this.minicutdataGridView);
            this.miniCutTabPage.Location = new System.Drawing.Point(4, 4);
            this.miniCutTabPage.Name = "miniCutTabPage";
            this.miniCutTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.miniCutTabPage.Size = new System.Drawing.Size(712, 130);
            this.miniCutTabPage.TabIndex = 1;
            this.miniCutTabPage.Text = "最小割集";
            this.miniCutTabPage.ToolTipText = "最小割集";
            this.miniCutTabPage.UseVisualStyleBackColor = true;
            // 
            // copyminicutset
            // 
            this.copyminicutset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.copyminicutset.Location = new System.Drawing.Point(590, 101);
            this.copyminicutset.Name = "copyminicutset";
            this.copyminicutset.Size = new System.Drawing.Size(116, 23);
            this.copyminicutset.TabIndex = 2;
            this.copyminicutset.Text = "复制最小割集信息";
            this.copyminicutset.UseVisualStyleBackColor = true;
            this.copyminicutset.Visible = false;
            this.copyminicutset.Click += new System.EventHandler(this.copyminicutset_Click);
            // 
            // minicutdataGridView
            // 
            this.minicutdataGridView.AllowUserToAddRows = false;
            this.minicutdataGridView.AllowUserToDeleteRows = false;
            this.minicutdataGridView.AllowUserToResizeRows = false;
            this.minicutdataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.minicutdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.minicutdataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.NaviColumn});
            this.minicutdataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.minicutdataGridView.Location = new System.Drawing.Point(3, 3);
            this.minicutdataGridView.MultiSelect = false;
            this.minicutdataGridView.Name = "minicutdataGridView";
            this.minicutdataGridView.ReadOnly = true;
            this.minicutdataGridView.RowTemplate.Height = 23;
            this.minicutdataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.minicutdataGridView.Size = new System.Drawing.Size(706, 124);
            this.minicutdataGridView.TabIndex = 1;
            this.minicutdataGridView.Visible = false;
            this.minicutdataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.minicutdataGridView_CellContentClick);
            this.minicutdataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.minicutdataGridView_CellDoubleClick);
            this.minicutdataGridView.Click += new System.EventHandler(this.minicutdataGridView_Click);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "序号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.ToolTipText = "最小割集序号";
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "元素";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.ToolTipText = "每个最小割集内的基本事件";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "key";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Visible = false;
            // 
            // NaviColumn
            // 
            this.NaviColumn.HeaderText = "导航";
            this.NaviColumn.Name = "NaviColumn";
            this.NaviColumn.ReadOnly = true;
            this.NaviColumn.Text = "下一节点";
            this.NaviColumn.ToolTipText = "点击定位至故障树节点";
            this.NaviColumn.UseColumnTextForButtonValue = true;
            // 
            // SFTAInfoViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 156);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HideOnClose = true;
            this.Name = "SFTAInfoViewForm";
            this.Text = "输出";
            this.tabControl1.ResumeLayout(false);
            this.infoTabPage.ResumeLayout(false);
            this.miniCutTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.minicutdataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage infoTabPage;
        private System.Windows.Forms.TabPage miniCutTabPage;
        private System.Windows.Forms.DataGridView minicutdataGridView;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.RichTextBox outputRichTextBox;
        private System.Windows.Forms.Button copyminicutset;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewButtonColumn NaviColumn;

    }
}