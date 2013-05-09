namespace TrafficJudgingSystem
{
    partial class JudgeResultForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.exportbutton = new System.Windows.Forms.Button();
            this.mapbutton = new System.Windows.Forms.Button();
            this.yearColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.routenameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sectionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.j0Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jstarColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.suggestionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.yearColumn,
            this.routenameColumn,
            this.sectionColumn,
            this.j0Column,
            this.jstarColumn,
            this.suggestionColumn});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(758, 398);
            this.dataGridView1.TabIndex = 0;
            // 
            // exportbutton
            // 
            this.exportbutton.Location = new System.Drawing.Point(249, 405);
            this.exportbutton.Name = "exportbutton";
            this.exportbutton.Size = new System.Drawing.Size(75, 23);
            this.exportbutton.TabIndex = 1;
            this.exportbutton.Text = "导出";
            this.exportbutton.UseVisualStyleBackColor = true;
            this.exportbutton.Click += new System.EventHandler(this.exportbutton_Click);
            // 
            // mapbutton
            // 
            this.mapbutton.Location = new System.Drawing.Point(568, 405);
            this.mapbutton.Name = "mapbutton";
            this.mapbutton.Size = new System.Drawing.Size(75, 23);
            this.mapbutton.TabIndex = 2;
            this.mapbutton.Text = "地图显示";
            this.mapbutton.UseVisualStyleBackColor = true;
            this.mapbutton.Click += new System.EventHandler(this.mapbutton_Click);
            // 
            // yearColumn
            // 
            this.yearColumn.HeaderText = "年份";
            this.yearColumn.Name = "yearColumn";
            this.yearColumn.Width = 54;
            // 
            // routenameColumn
            // 
            this.routenameColumn.HeaderText = "路线名称";
            this.routenameColumn.Name = "routenameColumn";
            this.routenameColumn.Width = 78;
            // 
            // sectionColumn
            // 
            this.sectionColumn.HeaderText = "评价区间";
            this.sectionColumn.Name = "sectionColumn";
            this.sectionColumn.Width = 78;
            // 
            // j0Column
            // 
            this.j0Column.HeaderText = "评价等级（j0)";
            this.j0Column.Name = "j0Column";
            this.j0Column.Width = 72;
            // 
            // jstarColumn
            // 
            this.jstarColumn.HeaderText = "评价等级特征值（j*）";
            this.jstarColumn.Name = "jstarColumn";
            this.jstarColumn.Width = 94;
            // 
            // suggestionColumn
            // 
            this.suggestionColumn.HeaderText = "养护建议";
            this.suggestionColumn.Name = "suggestionColumn";
            this.suggestionColumn.Width = 61;
            // 
            // JudgeResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 440);
            this.Controls.Add(this.mapbutton);
            this.Controls.Add(this.exportbutton);
            this.Controls.Add(this.dataGridView1);
            this.Name = "JudgeResultForm";
            this.Text = "评价结果";
            this.Load += new System.EventHandler(this.JudgeResultForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button exportbutton;
        private System.Windows.Forms.Button mapbutton;
        private System.Windows.Forms.DataGridViewTextBoxColumn yearColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn routenameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sectionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn j0Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn jstarColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn suggestionColumn;
    }
}