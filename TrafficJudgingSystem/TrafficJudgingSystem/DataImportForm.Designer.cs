namespace TrafficJudgingSystem
{
    partial class DataImportForm
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
            this.importdatabutton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.YearColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RouteNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RouteSectionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.judgebutton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // importdatabutton
            // 
            this.importdatabutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.importdatabutton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.importdatabutton.Location = new System.Drawing.Point(217, 418);
            this.importdatabutton.Name = "importdatabutton";
            this.importdatabutton.Size = new System.Drawing.Size(106, 31);
            this.importdatabutton.TabIndex = 1;
            this.importdatabutton.Text = "导入数据";
            this.importdatabutton.UseVisualStyleBackColor = true;
            this.importdatabutton.Click += new System.EventHandler(this.importdatabutton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.YearColumn,
            this.RouteNameColumn,
            this.RouteSectionColumn});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(929, 412);
            this.dataGridView1.TabIndex = 2;
            // 
            // YearColumn
            // 
            this.YearColumn.HeaderText = "年份";
            this.YearColumn.Name = "YearColumn";
            // 
            // RouteNameColumn
            // 
            this.RouteNameColumn.HeaderText = "路线名称";
            this.RouteNameColumn.Name = "RouteNameColumn";
            // 
            // RouteSectionColumn
            // 
            this.RouteSectionColumn.HeaderText = "评价区间";
            this.RouteSectionColumn.Name = "RouteSectionColumn";
            // 
            // judgebutton
            // 
            this.judgebutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.judgebutton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.judgebutton.Location = new System.Drawing.Point(606, 418);
            this.judgebutton.Name = "judgebutton";
            this.judgebutton.Size = new System.Drawing.Size(106, 31);
            this.judgebutton.TabIndex = 3;
            this.judgebutton.Text = "评价";
            this.judgebutton.UseVisualStyleBackColor = true;
            this.judgebutton.Click += new System.EventHandler(this.judgebutton_Click);
            // 
            // DataImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 453);
            this.Controls.Add(this.judgebutton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.importdatabutton);
            this.Name = "DataImportForm";
            this.Text = "数据录入";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DataImportForm_FormClosed);
            this.Load += new System.EventHandler(this.DataImportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button importdatabutton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button judgebutton;
        private System.Windows.Forms.DataGridViewTextBoxColumn YearColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RouteNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RouteSectionColumn;
    }
}