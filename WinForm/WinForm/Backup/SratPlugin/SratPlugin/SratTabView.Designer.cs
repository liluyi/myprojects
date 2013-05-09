namespace SratPlugin
{
    partial class SratTabView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SratTabView));
            this.addFlow1 = new Lassalle.Flow.AddFlow();
            this.systemContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.添加子系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重命名ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加分配任务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subSystemContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.添加模块ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重命名ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.删除该子系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加分配方法ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moduleContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.重命名ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.删除该模块ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.systemContextMenu.SuspendLayout();
            this.subSystemContextMenu.SuspendLayout();
            this.moduleContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // addFlow1
            // 
            this.addFlow1.DefLinkProp.ArrowDst = new Lassalle.Flow.Arrow(Lassalle.Flow.ArrowStyle.Arrow, Lassalle.Flow.ArrowSize.Small, Lassalle.Flow.ArrowAngle.deg15, false);
            this.addFlow1.DefLinkProp.CustomEndCap = null;
            this.addFlow1.DefLinkProp.CustomStartCap = null;
            this.addFlow1.DefLinkProp.Line = new Lassalle.Flow.Line(Lassalle.Flow.LineStyle.Polyline, true, false, false);
            this.addFlow1.DefLinkProp.Tag = null;
            this.addFlow1.DefLinkProp.ZOrder = -1;
            this.addFlow1.DefNodeProp.Location = ((System.Drawing.PointF)(resources.GetObject("resource.Location")));
            this.addFlow1.DefNodeProp.Rect = ((System.Drawing.RectangleF)(resources.GetObject("resource.Rect")));
            this.addFlow1.DefNodeProp.Shape = new Lassalle.Flow.Shape(Lassalle.Flow.ShapeStyle.Rectangle, Lassalle.Flow.ShapeOrientation.so_0);
            this.addFlow1.DefNodeProp.Tag = null;
            this.addFlow1.DefNodeProp.ZOrder = -1;
            this.addFlow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addFlow1.Location = new System.Drawing.Point(0, 0);
            this.addFlow1.Name = "addFlow1";
            this.addFlow1.Size = new System.Drawing.Size(284, 262);
            this.addFlow1.TabIndex = 0;
            this.addFlow1.AfterEdit += new Lassalle.Flow.AddFlow.AfterEditEventHandler(this.addFlow1_AfterEdit);
            this.addFlow1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.addFlow1_Scroll);
            this.addFlow1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.addFlow1_MouseDoubleClick);
            this.addFlow1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.addFlow1_MouseDown);
            // 
            // systemContextMenu
            // 
            this.systemContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加子系统ToolStripMenuItem,
            this.重命名ToolStripMenuItem,
            this.添加分配任务ToolStripMenuItem});
            this.systemContextMenu.Name = "systemContextMenu";
            this.systemContextMenu.Size = new System.Drawing.Size(149, 70);
            // 
            // 添加子系统ToolStripMenuItem
            // 
            this.添加子系统ToolStripMenuItem.Name = "添加子系统ToolStripMenuItem";
            this.添加子系统ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.添加子系统ToolStripMenuItem.Text = "添加子系统";
            // 
            // 重命名ToolStripMenuItem
            // 
            this.重命名ToolStripMenuItem.Name = "重命名ToolStripMenuItem";
            this.重命名ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.重命名ToolStripMenuItem.Text = "重命名";
            // 
            // 添加分配任务ToolStripMenuItem
            // 
            this.添加分配任务ToolStripMenuItem.Name = "添加分配任务ToolStripMenuItem";
            this.添加分配任务ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.添加分配任务ToolStripMenuItem.Text = "添加分配任务";
            // 
            // subSystemContextMenu
            // 
            this.subSystemContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加模块ToolStripMenuItem,
            this.重命名ToolStripMenuItem1,
            this.删除该子系统ToolStripMenuItem,
            this.添加分配方法ToolStripMenuItem});
            this.subSystemContextMenu.Name = "subSystemContextMenu";
            this.subSystemContextMenu.Size = new System.Drawing.Size(149, 92);
            // 
            // 添加模块ToolStripMenuItem
            // 
            this.添加模块ToolStripMenuItem.Name = "添加模块ToolStripMenuItem";
            this.添加模块ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.添加模块ToolStripMenuItem.Text = "添加模块";
            // 
            // 重命名ToolStripMenuItem1
            // 
            this.重命名ToolStripMenuItem1.Name = "重命名ToolStripMenuItem1";
            this.重命名ToolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.重命名ToolStripMenuItem1.Text = "重命名";
            // 
            // 删除该子系统ToolStripMenuItem
            // 
            this.删除该子系统ToolStripMenuItem.Name = "删除该子系统ToolStripMenuItem";
            this.删除该子系统ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.删除该子系统ToolStripMenuItem.Text = "删除该子系统";
            // 
            // 添加分配方法ToolStripMenuItem
            // 
            this.添加分配方法ToolStripMenuItem.Name = "添加分配方法ToolStripMenuItem";
            this.添加分配方法ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.添加分配方法ToolStripMenuItem.Text = "添加分配方法";
            // 
            // moduleContextMenu
            // 
            this.moduleContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.重命名ToolStripMenuItem2,
            this.删除该模块ToolStripMenuItem});
            this.moduleContextMenu.Name = "moduleContextMenu";
            this.moduleContextMenu.Size = new System.Drawing.Size(137, 48);
            // 
            // 重命名ToolStripMenuItem2
            // 
            this.重命名ToolStripMenuItem2.Name = "重命名ToolStripMenuItem2";
            this.重命名ToolStripMenuItem2.Size = new System.Drawing.Size(136, 22);
            this.重命名ToolStripMenuItem2.Text = "重命名";
            // 
            // 删除该模块ToolStripMenuItem
            // 
            this.删除该模块ToolStripMenuItem.Name = "删除该模块ToolStripMenuItem";
            this.删除该模块ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.删除该模块ToolStripMenuItem.Text = "删除该模块";
            // 
            // SratTabView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.addFlow1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HideOnClose = true;
            this.Name = "SratTabView";
            this.Text = "SratTabForm";
            this.systemContextMenu.ResumeLayout(false);
            this.subSystemContextMenu.ResumeLayout(false);
            this.moduleContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Lassalle.Flow.AddFlow addFlow1;
        private System.Windows.Forms.ContextMenuStrip systemContextMenu;
        private System.Windows.Forms.ToolStripMenuItem 添加子系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重命名ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加分配任务ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip subSystemContextMenu;
        private System.Windows.Forms.ToolStripMenuItem 添加模块ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重命名ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 删除该子系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加分配方法ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip moduleContextMenu;
        private System.Windows.Forms.ToolStripMenuItem 重命名ToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 删除该模块ToolStripMenuItem;

    }
}