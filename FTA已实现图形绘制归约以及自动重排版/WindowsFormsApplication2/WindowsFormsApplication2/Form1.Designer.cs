namespace WindowsFormsApplication2
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.addFlow1 = new Lassalle.Flow.AddFlow();
            this.FTAcontextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddEventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EventIntermediateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EventBasicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EventConditioningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EventInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EventOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EventUndevelopedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EventInitiatingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddGateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GateAndToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GateOrToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GateXorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GatePriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GateInhibitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FTAcontextMenuStrip.SuspendLayout();
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
            this.addFlow1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.addFlow1_MouseDown);
            // 
            // FTAcontextMenuStrip
            // 
            this.FTAcontextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddEventToolStripMenuItem,
            this.AddGateToolStripMenuItem});
            this.FTAcontextMenuStrip.Name = "FTAcontextMenuStrip";
            this.FTAcontextMenuStrip.Size = new System.Drawing.Size(153, 70);
            // 
            // AddEventToolStripMenuItem
            // 
            this.AddEventToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EventIntermediateToolStripMenuItem,
            this.EventBasicToolStripMenuItem,
            this.EventConditioningToolStripMenuItem,
            this.EventInToolStripMenuItem,
            this.EventOutToolStripMenuItem,
            this.EventUndevelopedToolStripMenuItem,
            this.EventInitiatingToolStripMenuItem});
            this.AddEventToolStripMenuItem.Name = "AddEventToolStripMenuItem";
            this.AddEventToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.AddEventToolStripMenuItem.Text = "添加事件";
            // 
            // EventIntermediateToolStripMenuItem
            // 
            this.EventIntermediateToolStripMenuItem.Name = "EventIntermediateToolStripMenuItem";
            this.EventIntermediateToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.EventIntermediateToolStripMenuItem.Text = "中间事件";
            // 
            // EventBasicToolStripMenuItem
            // 
            this.EventBasicToolStripMenuItem.Name = "EventBasicToolStripMenuItem";
            this.EventBasicToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.EventBasicToolStripMenuItem.Text = "基本事件";
            // 
            // EventConditioningToolStripMenuItem
            // 
            this.EventConditioningToolStripMenuItem.Name = "EventConditioningToolStripMenuItem";
            this.EventConditioningToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.EventConditioningToolStripMenuItem.Text = "条件事件";
            // 
            // EventInToolStripMenuItem
            // 
            this.EventInToolStripMenuItem.Name = "EventInToolStripMenuItem";
            this.EventInToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.EventInToolStripMenuItem.Text = "入事件";
            // 
            // EventOutToolStripMenuItem
            // 
            this.EventOutToolStripMenuItem.Name = "EventOutToolStripMenuItem";
            this.EventOutToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.EventOutToolStripMenuItem.Text = "出事件";
            // 
            // EventUndevelopedToolStripMenuItem
            // 
            this.EventUndevelopedToolStripMenuItem.Name = "EventUndevelopedToolStripMenuItem";
            this.EventUndevelopedToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.EventUndevelopedToolStripMenuItem.Text = "未展开事件";
            // 
            // EventInitiatingToolStripMenuItem
            // 
            this.EventInitiatingToolStripMenuItem.Name = "EventInitiatingToolStripMenuItem";
            this.EventInitiatingToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.EventInitiatingToolStripMenuItem.Text = "正常事件";
            // 
            // AddGateToolStripMenuItem
            // 
            this.AddGateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GateAndToolStripMenuItem,
            this.GateOrToolStripMenuItem,
            this.GateXorToolStripMenuItem,
            this.GatePriToolStripMenuItem,
            this.GateInhibitToolStripMenuItem});
            this.AddGateToolStripMenuItem.Name = "AddGateToolStripMenuItem";
            this.AddGateToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.AddGateToolStripMenuItem.Text = "添加逻辑门";
            // 
            // GateAndToolStripMenuItem
            // 
            this.GateAndToolStripMenuItem.Name = "GateAndToolStripMenuItem";
            this.GateAndToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.GateAndToolStripMenuItem.Text = "与门";
            // 
            // GateOrToolStripMenuItem
            // 
            this.GateOrToolStripMenuItem.Name = "GateOrToolStripMenuItem";
            this.GateOrToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.GateOrToolStripMenuItem.Text = "或门";
            // 
            // GateXorToolStripMenuItem
            // 
            this.GateXorToolStripMenuItem.Name = "GateXorToolStripMenuItem";
            this.GateXorToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.GateXorToolStripMenuItem.Text = "异或门";
            // 
            // GatePriToolStripMenuItem
            // 
            this.GatePriToolStripMenuItem.Name = "GatePriToolStripMenuItem";
            this.GatePriToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.GatePriToolStripMenuItem.Text = "优先门";
            // 
            // GateInhibitToolStripMenuItem
            // 
            this.GateInhibitToolStripMenuItem.Name = "GateInhibitToolStripMenuItem";
            this.GateInhibitToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.GateInhibitToolStripMenuItem.Text = "禁门";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.addFlow1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Form1";
            this.Text = "FTA";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FTAcontextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Lassalle.Flow.AddFlow addFlow1;
        private System.Windows.Forms.ContextMenuStrip FTAcontextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem AddEventToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EventIntermediateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EventBasicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EventConditioningToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EventInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EventOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EventUndevelopedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddGateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GateAndToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GateOrToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GateXorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GatePriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GateInhibitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EventInitiatingToolStripMenuItem;
    }
}

