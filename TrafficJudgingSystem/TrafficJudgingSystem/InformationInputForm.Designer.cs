namespace TrafficJudgingSystem
{
    partial class InformationInputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InformationInputForm));
            this.inputTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.nextbutton = new System.Windows.Forms.Button();
            this.addbutton = new System.Windows.Forms.Button();
            this.dsttextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.srctextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.routetypetextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.routenametextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.yeartextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lastinfobutton = new System.Windows.Forms.Button();
            this.downbutton = new System.Windows.Forms.Button();
            this.upbutton = new System.Windows.Forms.Button();
            this.firstinfobutton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.routenameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.routetypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.destination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yearColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.inputTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // inputTabControl
            // 
            this.inputTabControl.Controls.Add(this.tabPage1);
            this.inputTabControl.Controls.Add(this.tabPage2);
            this.inputTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputTabControl.Location = new System.Drawing.Point(0, 0);
            this.inputTabControl.Name = "inputTabControl";
            this.inputTabControl.SelectedIndex = 0;
            this.inputTabControl.Size = new System.Drawing.Size(620, 349);
            this.inputTabControl.TabIndex = 0;
            this.inputTabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.inputTabControl_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.nextbutton);
            this.tabPage1.Controls.Add(this.addbutton);
            this.tabPage1.Controls.Add(this.dsttextBox);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.srctextBox);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.routetypetextBox);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.routenametextBox);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.yeartextBox);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(612, 323);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "路线信息输入";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // nextbutton
            // 
            this.nextbutton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nextbutton.Location = new System.Drawing.Point(357, 286);
            this.nextbutton.Name = "nextbutton";
            this.nextbutton.Size = new System.Drawing.Size(75, 29);
            this.nextbutton.TabIndex = 15;
            this.nextbutton.Text = "下一步";
            this.nextbutton.UseVisualStyleBackColor = true;
            this.nextbutton.Click += new System.EventHandler(this.nextbutton_Click);
            // 
            // addbutton
            // 
            this.addbutton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addbutton.Location = new System.Drawing.Point(175, 286);
            this.addbutton.Name = "addbutton";
            this.addbutton.Size = new System.Drawing.Size(75, 29);
            this.addbutton.TabIndex = 14;
            this.addbutton.Text = "添加";
            this.addbutton.UseVisualStyleBackColor = true;
            this.addbutton.Click += new System.EventHandler(this.addbutton_Click);
            // 
            // dsttextBox
            // 
            this.dsttextBox.Location = new System.Drawing.Point(88, 224);
            this.dsttextBox.Name = "dsttextBox";
            this.dsttextBox.Size = new System.Drawing.Size(100, 21);
            this.dsttextBox.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoEllipsis = true;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(56, 224);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 21);
            this.label6.TabIndex = 10;
            this.label6.Text = "至";
            // 
            // srctextBox
            // 
            this.srctextBox.Location = new System.Drawing.Point(88, 197);
            this.srctextBox.Name = "srctextBox";
            this.srctextBox.Size = new System.Drawing.Size(100, 21);
            this.srctextBox.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoEllipsis = true;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(8, 197);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 21);
            this.label5.TabIndex = 8;
            this.label5.Text = "评价区间";
            // 
            // routetypetextBox
            // 
            this.routetypetextBox.Location = new System.Drawing.Point(88, 149);
            this.routetypetextBox.Name = "routetypetextBox";
            this.routetypetextBox.Size = new System.Drawing.Size(100, 21);
            this.routetypetextBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(8, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "路线形式";
            // 
            // routenametextBox
            // 
            this.routenametextBox.Location = new System.Drawing.Point(88, 95);
            this.routenametextBox.Name = "routenametextBox";
            this.routenametextBox.Size = new System.Drawing.Size(100, 21);
            this.routenametextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(8, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "路线名称";
            // 
            // yeartextBox
            // 
            this.yeartextBox.Location = new System.Drawing.Point(88, 43);
            this.yeartextBox.Name = "yeartextBox";
            this.yeartextBox.Size = new System.Drawing.Size(100, 21);
            this.yeartextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(8, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "年份";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lastinfobutton);
            this.tabPage2.Controls.Add(this.downbutton);
            this.tabPage2.Controls.Add(this.upbutton);
            this.tabPage2.Controls.Add(this.firstinfobutton);
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(612, 323);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "路线信息浏览";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lastinfobutton
            // 
            this.lastinfobutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lastinfobutton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lastinfobutton.Location = new System.Drawing.Point(529, 285);
            this.lastinfobutton.Name = "lastinfobutton";
            this.lastinfobutton.Size = new System.Drawing.Size(75, 30);
            this.lastinfobutton.TabIndex = 4;
            this.lastinfobutton.Text = "末条记录";
            this.lastinfobutton.UseVisualStyleBackColor = true;
            this.lastinfobutton.Click += new System.EventHandler(this.lastinfobutton_Click);
            // 
            // downbutton
            // 
            this.downbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.downbutton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.downbutton.Location = new System.Drawing.Point(361, 285);
            this.downbutton.Name = "downbutton";
            this.downbutton.Size = new System.Drawing.Size(86, 30);
            this.downbutton.TabIndex = 3;
            this.downbutton.Text = "下一条";
            this.downbutton.UseVisualStyleBackColor = true;
            this.downbutton.Click += new System.EventHandler(this.downbutton_Click);
            // 
            // upbutton
            // 
            this.upbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.upbutton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.upbutton.Location = new System.Drawing.Point(183, 285);
            this.upbutton.Name = "upbutton";
            this.upbutton.Size = new System.Drawing.Size(84, 30);
            this.upbutton.TabIndex = 2;
            this.upbutton.Text = "上一条";
            this.upbutton.UseVisualStyleBackColor = true;
            this.upbutton.Click += new System.EventHandler(this.upbutton_Click);
            // 
            // firstinfobutton
            // 
            this.firstinfobutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.firstinfobutton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.firstinfobutton.Location = new System.Drawing.Point(8, 285);
            this.firstinfobutton.Name = "firstinfobutton";
            this.firstinfobutton.Size = new System.Drawing.Size(75, 30);
            this.firstinfobutton.TabIndex = 1;
            this.firstinfobutton.Text = "首条记录";
            this.firstinfobutton.UseVisualStyleBackColor = true;
            this.firstinfobutton.Click += new System.EventHandler(this.firstinfobutton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.routenameColumn,
            this.routetypeColumn,
            this.regionColumn,
            this.destination,
            this.yearColumn});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(606, 276);
            this.dataGridView1.TabIndex = 0;
            // 
            // routenameColumn
            // 
            this.routenameColumn.HeaderText = "路线名称";
            this.routenameColumn.Name = "routenameColumn";
            // 
            // routetypeColumn
            // 
            this.routetypeColumn.HeaderText = "路线形式";
            this.routetypeColumn.Name = "routetypeColumn";
            // 
            // regionColumn
            // 
            this.regionColumn.HeaderText = "评价区间起点";
            this.regionColumn.MinimumWidth = 9;
            this.regionColumn.Name = "regionColumn";
            // 
            // destination
            // 
            this.destination.HeaderText = "评价区间终点";
            this.destination.Name = "destination";
            // 
            // yearColumn
            // 
            this.yearColumn.HeaderText = "年份";
            this.yearColumn.Name = "yearColumn";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(209, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(402, 267);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // InformationInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 349);
            this.Controls.Add(this.inputTabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "InformationInputForm";
            this.Text = "信息录入";
            this.inputTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl inputTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox dsttextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox srctextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox routetypetextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox routenametextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox yeartextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button nextbutton;
        private System.Windows.Forms.Button addbutton;
        private System.Windows.Forms.Button lastinfobutton;
        private System.Windows.Forms.Button downbutton;
        private System.Windows.Forms.Button upbutton;
        private System.Windows.Forms.Button firstinfobutton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn routenameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn routetypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn regionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn destination;
        private System.Windows.Forms.DataGridViewTextBoxColumn yearColumn;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}