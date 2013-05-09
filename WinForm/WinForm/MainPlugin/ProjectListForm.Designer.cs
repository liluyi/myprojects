namespace MainPlugin
{
    partial class ProjectListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectListForm));
            this.projectlistBox = new System.Windows.Forms.ListBox();
            this.restorebutton = new System.Windows.Forms.Button();
            this.cancelbutton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // projectlistBox
            // 
            this.projectlistBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.projectlistBox.FormattingEnabled = true;
            this.projectlistBox.ItemHeight = 12;
            this.projectlistBox.Location = new System.Drawing.Point(12, 109);
            this.projectlistBox.Name = "projectlistBox";
            this.projectlistBox.Size = new System.Drawing.Size(356, 244);
            this.projectlistBox.TabIndex = 0;
            this.projectlistBox.SelectedIndexChanged += new System.EventHandler(this.projectlistBox_SelectedIndexChanged);
            // 
            // restorebutton
            // 
            this.restorebutton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.restorebutton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.restorebutton.Location = new System.Drawing.Point(69, 364);
            this.restorebutton.Name = "restorebutton";
            this.restorebutton.Size = new System.Drawing.Size(90, 36);
            this.restorebutton.TabIndex = 1;
            this.restorebutton.Text = "恢复";
            this.restorebutton.UseVisualStyleBackColor = true;
            // 
            // cancelbutton
            // 
            this.cancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbutton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cancelbutton.Location = new System.Drawing.Point(221, 364);
            this.cancelbutton.Name = "cancelbutton";
            this.cancelbutton.Size = new System.Drawing.Size(90, 36);
            this.cancelbutton.TabIndex = 2;
            this.cancelbutton.Text = "取消";
            this.cancelbutton.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(94, 94);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(131, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 70);
            this.label1.TabIndex = 4;
            this.label1.Text = "版本控制：将当前工程恢复至历史版本，当前工程数据将全部删除。";
            // 
            // ProjectListForm
            // 
            this.AcceptButton = this.restorebutton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelbutton;
            this.ClientSize = new System.Drawing.Size(380, 412);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cancelbutton);
            this.Controls.Add(this.restorebutton);
            this.Controls.Add(this.projectlistBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "历史版本";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ProjectListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox projectlistBox;
        private System.Windows.Forms.Button restorebutton;
        private System.Windows.Forms.Button cancelbutton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
    }
}