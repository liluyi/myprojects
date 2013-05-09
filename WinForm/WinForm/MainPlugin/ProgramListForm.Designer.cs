using System.Xml;

namespace MainPlugin
{
    partial class ProgramListForm
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
        private void InitializeComponent(XmlNodeList nodelist)
        {
            this.accButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // accButton
            // 
            this.accButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.accButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.accButton.Location = new System.Drawing.Point(120, 325);
            this.accButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.accButton.Name = "accButton";
            this.accButton.Size = new System.Drawing.Size(87, 33);
            this.accButton.TabIndex = 0;
            this.accButton.Text = "连接";
            this.accButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(242, 325);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(83, 33);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            foreach (XmlNode node in nodelist)
            {
                foreach(XmlAttribute att in node.Attributes)
                    this.listBox1.Items.Add(att.Value);
            }

            this.listBox1.Location = new System.Drawing.Point(31, 15);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(385, 284);
            this.listBox1.TabIndex = 2;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // ProgramListForm
            // 
            this.AcceptButton = this.accButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(461, 371);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.accButton);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ProgramListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "服务器项目列表";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ProgramListForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button accButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ListBox listBox1;
    }
}