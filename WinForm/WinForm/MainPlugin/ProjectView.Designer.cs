namespace MainPlugin
{
    partial class ProjectView
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
            this.listProjectView = new System.Windows.Forms.ListView();
            this.index = new System.Windows.Forms.ColumnHeader();
            this.pluginID = new System.Windows.Forms.ColumnHeader();
            this.UUID = new System.Windows.Forms.ColumnHeader();
            this.UpdateProjectView = new System.Windows.Forms.Button();
            this.RemoveProject = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listProjectView
            // 
            this.listProjectView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listProjectView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.index,
            this.pluginID,
            this.UUID});
            this.listProjectView.Location = new System.Drawing.Point(12, 12);
            this.listProjectView.Name = "listProjectView";
            this.listProjectView.Size = new System.Drawing.Size(492, 178);
            this.listProjectView.TabIndex = 0;
            this.listProjectView.UseCompatibleStateImageBehavior = false;
            this.listProjectView.View = System.Windows.Forms.View.Details;
            // 
            // index
            // 
            this.index.Text = "index";
            this.index.Width = 188;
            // 
            // pluginID
            // 
            this.pluginID.Text = "插件标识";
            this.pluginID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UUID
            // 
            this.UUID.Text = "工程UUID";
            this.UUID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.UUID.Width = 235;
            // 
            // UpdateProjectView
            // 
            this.UpdateProjectView.Location = new System.Drawing.Point(114, 241);
            this.UpdateProjectView.Name = "UpdateProjectView";
            this.UpdateProjectView.Size = new System.Drawing.Size(97, 23);
            this.UpdateProjectView.TabIndex = 1;
            this.UpdateProjectView.Text = "更新工程试图";
            this.UpdateProjectView.UseVisualStyleBackColor = true;
            this.UpdateProjectView.Click += new System.EventHandler(this.UpdateProjectView_Click);
            // 
            // RemoveProject
            // 
            this.RemoveProject.Location = new System.Drawing.Point(293, 241);
            this.RemoveProject.Name = "RemoveProject";
            this.RemoveProject.Size = new System.Drawing.Size(107, 23);
            this.RemoveProject.TabIndex = 2;
            this.RemoveProject.Text = "移除所选工程";
            this.RemoveProject.UseVisualStyleBackColor = true;
            this.RemoveProject.Click += new System.EventHandler(this.RemoveProject_Click);
            // 
            // ProjectView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 362);
            this.Controls.Add(this.RemoveProject);
            this.Controls.Add(this.UpdateProjectView);
            this.Controls.Add(this.listProjectView);
            this.Name = "ProjectView";
            this.Text = "ProjectView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listProjectView;
        private System.Windows.Forms.Button UpdateProjectView;
        private System.Windows.Forms.ColumnHeader index;
        private System.Windows.Forms.ColumnHeader UUID;
        private System.Windows.Forms.ColumnHeader pluginID;
        private System.Windows.Forms.Button RemoveProject;
    }
}