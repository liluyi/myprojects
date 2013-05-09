namespace SFTAPlugin
{
    partial class IPOForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxModName = new System.Windows.Forms.TextBox();
            this.btnOKIPO = new System.Windows.Forms.Button();
            this.treeViewFunc = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddTopEventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RenameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddModuleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridCtrlIPOFMEA = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnMod = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFMEA = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCtrlIPOFMEA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(305, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "选中的分析模块为：";
            // 
            // textBoxModName
            // 
            this.textBoxModName.Location = new System.Drawing.Point(414, 35);
            this.textBoxModName.Name = "textBoxModName";
            this.textBoxModName.Size = new System.Drawing.Size(182, 21);
            this.textBoxModName.TabIndex = 2;
            // 
            // btnOKIPO
            // 
            this.btnOKIPO.Location = new System.Drawing.Point(294, 474);
            this.btnOKIPO.Name = "btnOKIPO";
            this.btnOKIPO.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnOKIPO.Size = new System.Drawing.Size(120, 23);
            this.btnOKIPO.TabIndex = 12;
            this.btnOKIPO.Text = "完成本模块分析";
            this.btnOKIPO.UseVisualStyleBackColor = true;
            this.btnOKIPO.Click += new System.EventHandler(this.btnOKIPO_Click);
            // 
            // treeViewFunc
            // 
            this.treeViewFunc.LabelEdit = true;
            this.treeViewFunc.Location = new System.Drawing.Point(12, 7);
            this.treeViewFunc.Name = "treeViewFunc";
            this.treeViewFunc.Size = new System.Drawing.Size(266, 501);
            this.treeViewFunc.TabIndex = 0;
            this.treeViewFunc.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeViewFunc_AfterLabelEdit);
            this.treeViewFunc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeViewFunc_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddTopEventToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 26);
            this.contextMenuStrip1.Click += new System.EventHandler(this.AddTopEventToolStripMenuItem_Click);
            // 
            // AddTopEventToolStripMenuItem
            // 
            this.AddTopEventToolStripMenuItem.Name = "AddTopEventToolStripMenuItem";
            this.AddTopEventToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.AddTopEventToolStripMenuItem.Text = "添加模块信息";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RenameToolStripMenuItem,
            this.AddModuleMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(161, 48);
            this.contextMenuStrip2.Click += new System.EventHandler(this.RenameToolStripMenuItem_Click);
            // 
            // RenameToolStripMenuItem
            // 
            this.RenameToolStripMenuItem.Name = "RenameToolStripMenuItem";
            this.RenameToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.RenameToolStripMenuItem.Text = "重命名";
            // 
            // AddModuleMenuItem
            // 
            this.AddModuleMenuItem.Name = "AddModuleMenuItem";
            this.AddModuleMenuItem.Size = new System.Drawing.Size(160, 22);
            this.AddModuleMenuItem.Text = "添加子模块信息";
            this.AddModuleMenuItem.Click += new System.EventHandler(this.AddModuleMenuItem_Click);
            // 
            // gridCtrlIPOFMEA
            // 
            this.gridCtrlIPOFMEA.Location = new System.Drawing.Point(12, 61);
            this.gridCtrlIPOFMEA.MainView = this.gridView1;
            this.gridCtrlIPOFMEA.Name = "gridCtrlIPOFMEA";
            this.gridCtrlIPOFMEA.Size = new System.Drawing.Size(420, 358);
            this.gridCtrlIPOFMEA.TabIndex = 13;
            this.gridCtrlIPOFMEA.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn5,
            this.gridColumn2,
            this.gridColumn3});
            this.gridView1.CustomizationFormBounds = new System.Drawing.Rectangle(762, 378, 216, 187);
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.GridControl = this.gridCtrlIPOFMEA;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.AllowCellMerge = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "所属过程";
            this.gridColumn1.FieldName = "IPOType";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn1.ToolTip = "testtest";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 70;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "过程信息";
            this.gridColumn5.FieldName = "Process";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            this.gridColumn5.Width = 83;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "失效模式";
            this.gridColumn2.FieldName = "FailureName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 112;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "是否选为建树元素";
            this.gridColumn3.FieldName = "check";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            this.gridColumn3.Width = 117;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(175, 428);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnMod
            // 
            this.btnMod.Location = new System.Drawing.Point(267, 427);
            this.btnMod.Name = "btnMod";
            this.btnMod.Size = new System.Drawing.Size(75, 23);
            this.btnMod.TabIndex = 13;
            this.btnMod.Text = "修改";
            this.btnMod.UseVisualStyleBackColor = true;
            this.btnMod.Click += new System.EventHandler(this.btnMod_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(356, 427);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 14;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFMEA);
            this.groupBox1.Controls.Add(this.btnOKIPO);
            this.groupBox1.Controls.Add(this.gridCtrlIPOFMEA);
            this.groupBox1.Controls.Add(this.btnDel);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.btnMod);
            this.groupBox1.Location = new System.Drawing.Point(295, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(438, 496);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "软件IPO失效分析";
            // 
            // btnFMEA
            // 
            this.btnFMEA.Location = new System.Drawing.Point(307, 21);
            this.btnFMEA.Name = "btnFMEA";
            this.btnFMEA.Size = new System.Drawing.Size(125, 23);
            this.btnFMEA.TabIndex = 16;
            this.btnFMEA.Text = "读入该模块FMEA信息";
            this.btnFMEA.UseVisualStyleBackColor = true;
            this.btnFMEA.Click += new System.EventHandler(this.btnFMEA_Click);
            // 
            // IPOForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 520);
            this.Controls.Add(this.textBoxModName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeViewFunc);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "IPOForm";
            this.Text = "IPOForm";
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCtrlIPOFMEA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxModName;
        private System.Windows.Forms.Button btnOKIPO;
        private System.Windows.Forms.TreeView treeViewFunc;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem AddTopEventToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem RenameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddModuleMenuItem;
        private DevExpress.XtraGrid.GridControl gridCtrlIPOFMEA;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnMod;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnFMEA;
    }
}