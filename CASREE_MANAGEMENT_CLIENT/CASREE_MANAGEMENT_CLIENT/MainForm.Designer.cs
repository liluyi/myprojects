namespace CASREE_MANAGEMENT_CLIENT
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tasktabControl = new System.Windows.Forms.TabControl();
            this.usertabPage = new System.Windows.Forms.TabPage();
            this.searchusergroupBox = new System.Windows.Forms.GroupBox();
            this.searchresultlabel = new System.Windows.Forms.Label();
            this.searchuserbutton = new System.Windows.Forms.Button();
            this.groupidlabel = new System.Windows.Forms.Label();
            this.pwlabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.deletebutton = new System.Windows.Forms.Button();
            this.searchusernametextBox = new System.Windows.Forms.TextBox();
            this.addusergroupBox = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupidtextBox = new System.Windows.Forms.TextBox();
            this.resultlabel = new System.Windows.Forms.Label();
            this.adduserbutton = new System.Windows.Forms.Button();
            this.addusernametextBox = new System.Windows.Forms.TextBox();
            this.adduserpwtextBox2 = new System.Windows.Forms.TextBox();
            this.adduserpwtextBox = new System.Windows.Forms.TextBox();
            this.permissiontabPage = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.searchpmdataGridView = new System.Windows.Forms.DataGridView();
            this.PrjidColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PmlevelColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchpmresultlabel = new System.Windows.Forms.Label();
            this.searchpermissionbutton = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.searchpmusertextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.prjlistcomboBox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.addpmresultlabel = new System.Windows.Forms.Label();
            this.addpermissionbutton = new System.Windows.Forms.Button();
            this.addpmusertextBox = new System.Windows.Forms.TextBox();
            this.addpmleveltextBox = new System.Windows.Forms.TextBox();
            this.addpmprjidtextBox = new System.Windows.Forms.TextBox();
            this.solutiontabPage = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.destinationlistBox = new System.Windows.Forms.ListBox();
            this.sourcelistBox = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.searchsolutionresultlabel = new System.Windows.Forms.Label();
            this.solutiondataGridView = new System.Windows.Forms.DataGridView();
            this.tooltypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.projectidColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isActiveColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchsolutionbutton = new System.Windows.Forms.Button();
            this.solutiontextBox = new System.Windows.Forms.TextBox();
            this.pushresultlabel = new System.Windows.Forms.Label();
            this.tasktabControl.SuspendLayout();
            this.usertabPage.SuspendLayout();
            this.searchusergroupBox.SuspendLayout();
            this.addusergroupBox.SuspendLayout();
            this.permissiontabPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchpmdataGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.solutiontabPage.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.solutiondataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tasktabControl
            // 
            this.tasktabControl.Controls.Add(this.usertabPage);
            this.tasktabControl.Controls.Add(this.permissiontabPage);
            this.tasktabControl.Controls.Add(this.solutiontabPage);
            this.tasktabControl.Location = new System.Drawing.Point(0, 0);
            this.tasktabControl.Multiline = true;
            this.tasktabControl.Name = "tasktabControl";
            this.tasktabControl.SelectedIndex = 0;
            this.tasktabControl.Size = new System.Drawing.Size(948, 347);
            this.tasktabControl.TabIndex = 0;
            // 
            // usertabPage
            // 
            this.usertabPage.Controls.Add(this.searchusergroupBox);
            this.usertabPage.Controls.Add(this.addusergroupBox);
            this.usertabPage.Location = new System.Drawing.Point(4, 22);
            this.usertabPage.Name = "usertabPage";
            this.usertabPage.Padding = new System.Windows.Forms.Padding(3);
            this.usertabPage.Size = new System.Drawing.Size(940, 321);
            this.usertabPage.TabIndex = 0;
            this.usertabPage.Text = "用户管理";
            this.usertabPage.UseVisualStyleBackColor = true;
            // 
            // searchusergroupBox
            // 
            this.searchusergroupBox.Controls.Add(this.searchresultlabel);
            this.searchusergroupBox.Controls.Add(this.searchuserbutton);
            this.searchusergroupBox.Controls.Add(this.groupidlabel);
            this.searchusergroupBox.Controls.Add(this.pwlabel);
            this.searchusergroupBox.Controls.Add(this.label5);
            this.searchusergroupBox.Controls.Add(this.label7);
            this.searchusergroupBox.Controls.Add(this.label8);
            this.searchusergroupBox.Controls.Add(this.label9);
            this.searchusergroupBox.Controls.Add(this.deletebutton);
            this.searchusergroupBox.Controls.Add(this.searchusernametextBox);
            this.searchusergroupBox.Location = new System.Drawing.Point(284, 6);
            this.searchusergroupBox.Name = "searchusergroupBox";
            this.searchusergroupBox.Size = new System.Drawing.Size(296, 309);
            this.searchusergroupBox.TabIndex = 5;
            this.searchusergroupBox.TabStop = false;
            this.searchusergroupBox.Text = "查找用户信息";
            // 
            // searchresultlabel
            // 
            this.searchresultlabel.AutoSize = true;
            this.searchresultlabel.ForeColor = System.Drawing.Color.Red;
            this.searchresultlabel.Location = new System.Drawing.Point(115, 96);
            this.searchresultlabel.Name = "searchresultlabel";
            this.searchresultlabel.Size = new System.Drawing.Size(0, 12);
            this.searchresultlabel.TabIndex = 13;
            // 
            // searchuserbutton
            // 
            this.searchuserbutton.Location = new System.Drawing.Point(222, 51);
            this.searchuserbutton.Name = "searchuserbutton";
            this.searchuserbutton.Size = new System.Drawing.Size(42, 23);
            this.searchuserbutton.TabIndex = 12;
            this.searchuserbutton.Text = "搜索";
            this.searchuserbutton.UseVisualStyleBackColor = true;
            this.searchuserbutton.Click += new System.EventHandler(this.searchuserbutton_Click);
            // 
            // groupidlabel
            // 
            this.groupidlabel.AutoSize = true;
            this.groupidlabel.Location = new System.Drawing.Point(119, 176);
            this.groupidlabel.Name = "groupidlabel";
            this.groupidlabel.Size = new System.Drawing.Size(0, 12);
            this.groupidlabel.TabIndex = 11;
            // 
            // pwlabel
            // 
            this.pwlabel.AutoSize = true;
            this.pwlabel.Location = new System.Drawing.Point(119, 140);
            this.pwlabel.Name = "pwlabel";
            this.pwlabel.Size = new System.Drawing.Size(0, 12);
            this.pwlabel.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "用户组";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(51, 140);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "密码";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(39, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 6;
            this.label8.Text = "用户名";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(113, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 12);
            this.label9.TabIndex = 4;
            // 
            // deletebutton
            // 
            this.deletebutton.Location = new System.Drawing.Point(215, 278);
            this.deletebutton.Name = "deletebutton";
            this.deletebutton.Size = new System.Drawing.Size(75, 23);
            this.deletebutton.TabIndex = 3;
            this.deletebutton.Text = "删除用户";
            this.deletebutton.UseVisualStyleBackColor = true;
            this.deletebutton.Click += new System.EventHandler(this.deletebutton_Click);
            // 
            // searchusernametextBox
            // 
            this.searchusernametextBox.Location = new System.Drawing.Point(115, 51);
            this.searchusernametextBox.Name = "searchusernametextBox";
            this.searchusernametextBox.Size = new System.Drawing.Size(100, 21);
            this.searchusernametextBox.TabIndex = 0;
            // 
            // addusergroupBox
            // 
            this.addusergroupBox.Controls.Add(this.label4);
            this.addusergroupBox.Controls.Add(this.label3);
            this.addusergroupBox.Controls.Add(this.label2);
            this.addusergroupBox.Controls.Add(this.label1);
            this.addusergroupBox.Controls.Add(this.groupidtextBox);
            this.addusergroupBox.Controls.Add(this.resultlabel);
            this.addusergroupBox.Controls.Add(this.adduserbutton);
            this.addusergroupBox.Controls.Add(this.addusernametextBox);
            this.addusergroupBox.Controls.Add(this.adduserpwtextBox2);
            this.addusergroupBox.Controls.Add(this.adduserpwtextBox);
            this.addusergroupBox.Location = new System.Drawing.Point(8, 6);
            this.addusergroupBox.Name = "addusergroupBox";
            this.addusergroupBox.Size = new System.Drawing.Size(270, 309);
            this.addusergroupBox.TabIndex = 3;
            this.addusergroupBox.TabStop = false;
            this.addusergroupBox.Text = "添加用户";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "用户组";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "重复密码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "密码";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "用户名";
            // 
            // groupidtextBox
            // 
            this.groupidtextBox.Location = new System.Drawing.Point(115, 176);
            this.groupidtextBox.Name = "groupidtextBox";
            this.groupidtextBox.Size = new System.Drawing.Size(100, 21);
            this.groupidtextBox.TabIndex = 5;
            // 
            // resultlabel
            // 
            this.resultlabel.AutoSize = true;
            this.resultlabel.ForeColor = System.Drawing.Color.Red;
            this.resultlabel.Location = new System.Drawing.Point(113, 30);
            this.resultlabel.Name = "resultlabel";
            this.resultlabel.Size = new System.Drawing.Size(0, 12);
            this.resultlabel.TabIndex = 4;
            // 
            // adduserbutton
            // 
            this.adduserbutton.Location = new System.Drawing.Point(189, 278);
            this.adduserbutton.Name = "adduserbutton";
            this.adduserbutton.Size = new System.Drawing.Size(75, 23);
            this.adduserbutton.TabIndex = 3;
            this.adduserbutton.Text = "添加";
            this.adduserbutton.UseVisualStyleBackColor = true;
            this.adduserbutton.Click += new System.EventHandler(this.adduserbutton_Click);
            // 
            // addusernametextBox
            // 
            this.addusernametextBox.Location = new System.Drawing.Point(115, 54);
            this.addusernametextBox.Name = "addusernametextBox";
            this.addusernametextBox.Size = new System.Drawing.Size(100, 21);
            this.addusernametextBox.TabIndex = 0;
            // 
            // adduserpwtextBox2
            // 
            this.adduserpwtextBox2.Location = new System.Drawing.Point(115, 137);
            this.adduserpwtextBox2.Name = "adduserpwtextBox2";
            this.adduserpwtextBox2.Size = new System.Drawing.Size(100, 21);
            this.adduserpwtextBox2.TabIndex = 2;
            this.adduserpwtextBox2.UseSystemPasswordChar = true;
            // 
            // adduserpwtextBox
            // 
            this.adduserpwtextBox.Location = new System.Drawing.Point(115, 97);
            this.adduserpwtextBox.Name = "adduserpwtextBox";
            this.adduserpwtextBox.Size = new System.Drawing.Size(100, 21);
            this.adduserpwtextBox.TabIndex = 1;
            this.adduserpwtextBox.UseSystemPasswordChar = true;
            // 
            // permissiontabPage
            // 
            this.permissiontabPage.Controls.Add(this.groupBox1);
            this.permissiontabPage.Controls.Add(this.groupBox2);
            this.permissiontabPage.Location = new System.Drawing.Point(4, 22);
            this.permissiontabPage.Name = "permissiontabPage";
            this.permissiontabPage.Padding = new System.Windows.Forms.Padding(3);
            this.permissiontabPage.Size = new System.Drawing.Size(940, 321);
            this.permissiontabPage.TabIndex = 1;
            this.permissiontabPage.Text = "权限管理";
            this.permissiontabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.searchpmdataGridView);
            this.groupBox1.Controls.Add(this.searchpmresultlabel);
            this.groupBox1.Controls.Add(this.searchpermissionbutton);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.searchpmusertextBox);
            this.groupBox1.Location = new System.Drawing.Point(282, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 310);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "搜索用户权限";
            // 
            // searchpmdataGridView
            // 
            this.searchpmdataGridView.AllowUserToAddRows = false;
            this.searchpmdataGridView.AllowUserToDeleteRows = false;
            this.searchpmdataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.searchpmdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.searchpmdataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PrjidColumn,
            this.PmlevelColumn});
            this.searchpmdataGridView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.searchpmdataGridView.Location = new System.Drawing.Point(3, 84);
            this.searchpmdataGridView.Name = "searchpmdataGridView";
            this.searchpmdataGridView.RowTemplate.Height = 23;
            this.searchpmdataGridView.Size = new System.Drawing.Size(290, 223);
            this.searchpmdataGridView.TabIndex = 14;
            // 
            // PrjidColumn
            // 
            this.PrjidColumn.HeaderText = "工程ID";
            this.PrjidColumn.Name = "PrjidColumn";
            this.PrjidColumn.ReadOnly = true;
            // 
            // PmlevelColumn
            // 
            this.PmlevelColumn.HeaderText = "权限级别";
            this.PmlevelColumn.Name = "PmlevelColumn";
            this.PmlevelColumn.ReadOnly = true;
            // 
            // searchpmresultlabel
            // 
            this.searchpmresultlabel.AutoSize = true;
            this.searchpmresultlabel.ForeColor = System.Drawing.Color.Red;
            this.searchpmresultlabel.Location = new System.Drawing.Point(29, 54);
            this.searchpmresultlabel.Name = "searchpmresultlabel";
            this.searchpmresultlabel.Size = new System.Drawing.Size(0, 12);
            this.searchpmresultlabel.TabIndex = 13;
            // 
            // searchpermissionbutton
            // 
            this.searchpermissionbutton.Location = new System.Drawing.Point(232, 27);
            this.searchpermissionbutton.Name = "searchpermissionbutton";
            this.searchpermissionbutton.Size = new System.Drawing.Size(42, 23);
            this.searchpermissionbutton.TabIndex = 12;
            this.searchpermissionbutton.Text = "搜索";
            this.searchpermissionbutton.UseVisualStyleBackColor = true;
            this.searchpermissionbutton.Click += new System.EventHandler(this.searchpermissionbutton_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(16, 30);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 12);
            this.label17.TabIndex = 6;
            this.label17.Text = "用户名";
            // 
            // searchpmusertextBox
            // 
            this.searchpmusertextBox.Location = new System.Drawing.Point(63, 27);
            this.searchpmusertextBox.Name = "searchpmusertextBox";
            this.searchpmusertextBox.Size = new System.Drawing.Size(163, 21);
            this.searchpmusertextBox.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.prjlistcomboBox);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.addpmresultlabel);
            this.groupBox2.Controls.Add(this.addpermissionbutton);
            this.groupBox2.Controls.Add(this.addpmusertextBox);
            this.groupBox2.Controls.Add(this.addpmleveltextBox);
            this.groupBox2.Controls.Add(this.addpmprjidtextBox);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(270, 310);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "添加权限";
            // 
            // prjlistcomboBox
            // 
            this.prjlistcomboBox.FormattingEnabled = true;
            this.prjlistcomboBox.Location = new System.Drawing.Point(115, 157);
            this.prjlistcomboBox.Name = "prjlistcomboBox";
            this.prjlistcomboBox.Size = new System.Drawing.Size(100, 20);
            this.prjlistcomboBox.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(39, 239);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 8;
            this.label11.Text = "权限级别";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(39, 100);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 7;
            this.label12.Text = "工程id";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(39, 57);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 6;
            this.label13.Text = "用户名";
            // 
            // addpmresultlabel
            // 
            this.addpmresultlabel.AutoSize = true;
            this.addpmresultlabel.ForeColor = System.Drawing.Color.Red;
            this.addpmresultlabel.Location = new System.Drawing.Point(113, 30);
            this.addpmresultlabel.Name = "addpmresultlabel";
            this.addpmresultlabel.Size = new System.Drawing.Size(0, 12);
            this.addpmresultlabel.TabIndex = 4;
            // 
            // addpermissionbutton
            // 
            this.addpermissionbutton.Location = new System.Drawing.Point(140, 276);
            this.addpermissionbutton.Name = "addpermissionbutton";
            this.addpermissionbutton.Size = new System.Drawing.Size(75, 23);
            this.addpermissionbutton.TabIndex = 3;
            this.addpermissionbutton.Text = "添加";
            this.addpermissionbutton.UseVisualStyleBackColor = true;
            this.addpermissionbutton.Click += new System.EventHandler(this.addpermissionbutton_Click);
            // 
            // addpmusertextBox
            // 
            this.addpmusertextBox.Location = new System.Drawing.Point(115, 54);
            this.addpmusertextBox.Name = "addpmusertextBox";
            this.addpmusertextBox.Size = new System.Drawing.Size(100, 21);
            this.addpmusertextBox.TabIndex = 0;
            // 
            // addpmleveltextBox
            // 
            this.addpmleveltextBox.Location = new System.Drawing.Point(115, 236);
            this.addpmleveltextBox.Name = "addpmleveltextBox";
            this.addpmleveltextBox.Size = new System.Drawing.Size(100, 21);
            this.addpmleveltextBox.TabIndex = 2;
            this.addpmleveltextBox.UseSystemPasswordChar = true;
            // 
            // addpmprjidtextBox
            // 
            this.addpmprjidtextBox.Location = new System.Drawing.Point(115, 97);
            this.addpmprjidtextBox.Name = "addpmprjidtextBox";
            this.addpmprjidtextBox.Size = new System.Drawing.Size(100, 21);
            this.addpmprjidtextBox.TabIndex = 1;
            this.addpmprjidtextBox.UseSystemPasswordChar = true;
            // 
            // solutiontabPage
            // 
            this.solutiontabPage.Controls.Add(this.groupBox4);
            this.solutiontabPage.Controls.Add(this.groupBox3);
            this.solutiontabPage.Location = new System.Drawing.Point(4, 22);
            this.solutiontabPage.Name = "solutiontabPage";
            this.solutiontabPage.Size = new System.Drawing.Size(940, 321);
            this.solutiontabPage.TabIndex = 2;
            this.solutiontabPage.Text = "项目管理";
            this.solutiontabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.pushresultlabel);
            this.groupBox4.Controls.Add(this.pictureBox1);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.destinationlistBox);
            this.groupBox4.Controls.Add(this.sourcelistBox);
            this.groupBox4.Location = new System.Drawing.Point(295, 16);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(278, 294);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "数据推送";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CASREE_MANAGEMENT_CLIENT.Properties.Resources.pushxml;
            this.pictureBox1.Location = new System.Drawing.Point(115, 139);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(45, 43);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label10.Location = new System.Drawing.Point(176, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 12);
            this.label10.TabIndex = 3;
            this.label10.Text = "推送目标工具";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "数据来源工具";
            // 
            // destinationlistBox
            // 
            this.destinationlistBox.FormattingEnabled = true;
            this.destinationlistBox.ItemHeight = 12;
            this.destinationlistBox.Location = new System.Drawing.Point(166, 71);
            this.destinationlistBox.Name = "destinationlistBox";
            this.destinationlistBox.Size = new System.Drawing.Size(106, 220);
            this.destinationlistBox.TabIndex = 1;
            // 
            // sourcelistBox
            // 
            this.sourcelistBox.FormattingEnabled = true;
            this.sourcelistBox.ItemHeight = 12;
            this.sourcelistBox.Location = new System.Drawing.Point(6, 71);
            this.sourcelistBox.Name = "sourcelistBox";
            this.sourcelistBox.Size = new System.Drawing.Size(103, 220);
            this.sourcelistBox.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.searchsolutionresultlabel);
            this.groupBox3.Controls.Add(this.solutiondataGridView);
            this.groupBox3.Controls.Add(this.searchsolutionbutton);
            this.groupBox3.Controls.Add(this.solutiontextBox);
            this.groupBox3.Location = new System.Drawing.Point(8, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(269, 297);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "项目查询";
            // 
            // searchsolutionresultlabel
            // 
            this.searchsolutionresultlabel.AutoSize = true;
            this.searchsolutionresultlabel.ForeColor = System.Drawing.Color.Red;
            this.searchsolutionresultlabel.Location = new System.Drawing.Point(82, 52);
            this.searchsolutionresultlabel.Name = "searchsolutionresultlabel";
            this.searchsolutionresultlabel.Size = new System.Drawing.Size(0, 12);
            this.searchsolutionresultlabel.TabIndex = 4;
            // 
            // solutiondataGridView
            // 
            this.solutiondataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.solutiondataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.solutiondataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tooltypeColumn,
            this.projectidColumn,
            this.isActiveColumn});
            this.solutiondataGridView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.solutiondataGridView.Location = new System.Drawing.Point(3, 70);
            this.solutiondataGridView.Name = "solutiondataGridView";
            this.solutiondataGridView.RowTemplate.Height = 23;
            this.solutiondataGridView.Size = new System.Drawing.Size(263, 224);
            this.solutiondataGridView.TabIndex = 3;
            // 
            // tooltypeColumn
            // 
            this.tooltypeColumn.HeaderText = "联网工具";
            this.tooltypeColumn.Name = "tooltypeColumn";
            this.tooltypeColumn.Width = 78;
            // 
            // projectidColumn
            // 
            this.projectidColumn.HeaderText = "工程名称";
            this.projectidColumn.Name = "projectidColumn";
            this.projectidColumn.Width = 78;
            // 
            // isActiveColumn
            // 
            this.isActiveColumn.HeaderText = "联网状态";
            this.isActiveColumn.Name = "isActiveColumn";
            this.isActiveColumn.Width = 78;
            // 
            // searchsolutionbutton
            // 
            this.searchsolutionbutton.Location = new System.Drawing.Point(188, 18);
            this.searchsolutionbutton.Name = "searchsolutionbutton";
            this.searchsolutionbutton.Size = new System.Drawing.Size(75, 23);
            this.searchsolutionbutton.TabIndex = 2;
            this.searchsolutionbutton.Text = "搜索";
            this.searchsolutionbutton.UseVisualStyleBackColor = true;
            this.searchsolutionbutton.Click += new System.EventHandler(this.searchsolutionbutton_Click);
            // 
            // solutiontextBox
            // 
            this.solutiontextBox.Location = new System.Drawing.Point(6, 20);
            this.solutiontextBox.Name = "solutiontextBox";
            this.solutiontextBox.Size = new System.Drawing.Size(176, 21);
            this.solutiontextBox.TabIndex = 1;
            this.solutiontextBox.Text = "DefaultSolution";
            // 
            // pushresultlabel
            // 
            this.pushresultlabel.AutoSize = true;
            this.pushresultlabel.ForeColor = System.Drawing.Color.Red;
            this.pushresultlabel.Location = new System.Drawing.Point(15, 23);
            this.pushresultlabel.Name = "pushresultlabel";
            this.pushresultlabel.Size = new System.Drawing.Size(0, 12);
            this.pushresultlabel.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 347);
            this.Controls.Add(this.tasktabControl);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CASREE项目管理客户端";
            this.tasktabControl.ResumeLayout(false);
            this.usertabPage.ResumeLayout(false);
            this.searchusergroupBox.ResumeLayout(false);
            this.searchusergroupBox.PerformLayout();
            this.addusergroupBox.ResumeLayout(false);
            this.addusergroupBox.PerformLayout();
            this.permissiontabPage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchpmdataGridView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.solutiontabPage.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.solutiondataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tasktabControl;
        private System.Windows.Forms.TabPage usertabPage;
        private System.Windows.Forms.TabPage permissiontabPage;
        private System.Windows.Forms.GroupBox addusergroupBox;
        private System.Windows.Forms.Button adduserbutton;
        private System.Windows.Forms.TextBox addusernametextBox;
        private System.Windows.Forms.TextBox adduserpwtextBox2;
        private System.Windows.Forms.TextBox adduserpwtextBox;
        private System.Windows.Forms.Label resultlabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox groupidtextBox;
        private System.Windows.Forms.GroupBox searchusergroupBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button deletebutton;
        private System.Windows.Forms.TextBox searchusernametextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label addpmresultlabel;
        private System.Windows.Forms.Button addpermissionbutton;
        private System.Windows.Forms.TextBox addpmusertextBox;
        private System.Windows.Forms.TextBox addpmleveltextBox;
        private System.Windows.Forms.TextBox addpmprjidtextBox;
        private System.Windows.Forms.Label groupidlabel;
        private System.Windows.Forms.Label pwlabel;
        private System.Windows.Forms.Button searchuserbutton;
        private System.Windows.Forms.Label searchresultlabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label searchpmresultlabel;
        private System.Windows.Forms.Button searchpermissionbutton;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox searchpmusertextBox;
        private System.Windows.Forms.DataGridView searchpmdataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrjidColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PmlevelColumn;
        private System.Windows.Forms.ComboBox prjlistcomboBox;
        private System.Windows.Forms.TabPage solutiontabPage;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox destinationlistBox;
        private System.Windows.Forms.ListBox sourcelistBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView solutiondataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn tooltypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn projectidColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn isActiveColumn;
        private System.Windows.Forms.Button searchsolutionbutton;
        private System.Windows.Forms.TextBox solutiontextBox;
        private System.Windows.Forms.Label searchsolutionresultlabel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label pushresultlabel;
    }
}

