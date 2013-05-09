namespace SFTAPlugin
{
    partial class PropertyForm
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
            this.FTANodepropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // FTANodepropertyGrid
            // 
            this.FTANodepropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FTANodepropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.FTANodepropertyGrid.Name = "FTANodepropertyGrid";
            this.FTANodepropertyGrid.Size = new System.Drawing.Size(284, 262);
            this.FTANodepropertyGrid.TabIndex = 0;
            this.FTANodepropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.FTANodepropertyGrid_PropertyValueChanged);
            // 
            // PropertyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.FTANodepropertyGrid);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "PropertyForm";
            this.Text = "节点信息";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid FTANodepropertyGrid;
    }
}