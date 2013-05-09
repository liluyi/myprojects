using System;
namespace SFTAPlugin
{
    partial class FTAForm
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
            this.addFlow1 = new Lassalle.Flow.AddFlow();
            this.FTAcontextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddGateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GateAndToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GateOrToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GateXorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GatePriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GateInhibitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddGateSequenceAnd = new System.Windows.Forms.ToolStripMenuItem();
            this.AddGateElect = new System.Windows.Forms.ToolStripMenuItem();
            this.AddEventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EventIntermediateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EventBasicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EventConditioningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EventUndevelopedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EventInitiatingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EventInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EventOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.标记ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MarkUnfinishedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MarkFinishedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MarkImportantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateChildTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.DeleteNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DuplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteDuplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ftaformmenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gateAndToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.gateOrToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.gateXorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.gateInhibitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.gatePriToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.gateSequenceAndToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gateElectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eventBasicToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.eventIntermediateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.eventInitiatingToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.eventUndevelopedToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.eventConditioningToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.eventOutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.eventInToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.minimalCutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateMinimalCutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDiagramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FTAcontextMenuStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addFlow1
            // 
            this.addFlow1.AllowDrop = true;
            this.addFlow1.AutoSize = true;
            this.addFlow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addFlow1.Location = new System.Drawing.Point(0, 0);
            this.addFlow1.Name = "addFlow1";
            this.addFlow1.Size = new System.Drawing.Size(805, 291);
            this.addFlow1.TabIndex = 0;
            this.addFlow1.AfterEdit += new Lassalle.Flow.AddFlow.AfterEditEventHandler(this.addFlow1_AfterEdit);
            this.addFlow1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.addFlow1_Scroll);
            this.addFlow1.DragDrop += new System.Windows.Forms.DragEventHandler(this.addFlow1_DragDrop);
            this.addFlow1.DragEnter += new System.Windows.Forms.DragEventHandler(this.addFlow1_DragEnter);
            this.addFlow1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.addFlow1_MouseDoubleClick);
            this.addFlow1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.addFlow1_MouseDown);
            // 
            // FTAcontextMenuStrip
            // 
            this.FTAcontextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddGateToolStripMenuItem,
            this.AddEventToolStripMenuItem,
            this.toolStripSeparator1,
            this.标记ToolStripMenuItem,
            this.CreateChildTreeToolStripMenuItem,
            this.toolStripSeparator2,
            this.CopyToolStripMenuItem,
            this.PasteToolStripMenuItem,
            this.toolStripSeparator3,
            this.DeleteNodeToolStripMenuItem,
            this.DuplicateToolStripMenuItem,
            this.PasteDuplicateToolStripMenuItem});
            this.FTAcontextMenuStrip.Name = "FTAcontextMenuStrip";
            this.FTAcontextMenuStrip.Size = new System.Drawing.Size(207, 220);
            // 
            // AddGateToolStripMenuItem
            // 
            this.AddGateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GateAndToolStripMenuItem,
            this.GateOrToolStripMenuItem,
            this.GateXorToolStripMenuItem,
            this.GatePriToolStripMenuItem,
            this.GateInhibitToolStripMenuItem,
            this.AddGateSequenceAnd,
            this.AddGateElect});
            this.AddGateToolStripMenuItem.Name = "AddGateToolStripMenuItem";
            this.AddGateToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.AddGateToolStripMenuItem.Text = "添加逻辑门";
            // 
            // GateAndToolStripMenuItem
            // 
            this.GateAndToolStripMenuItem.Name = "GateAndToolStripMenuItem";
            this.GateAndToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.GateAndToolStripMenuItem.Text = "与门";
            this.GateAndToolStripMenuItem.Click += new System.EventHandler(this.AddGateAnd);
            // 
            // GateOrToolStripMenuItem
            // 
            this.GateOrToolStripMenuItem.Name = "GateOrToolStripMenuItem";
            this.GateOrToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.GateOrToolStripMenuItem.Text = "或门";
            this.GateOrToolStripMenuItem.Click += new System.EventHandler(this.AddGateOr);
            // 
            // GateXorToolStripMenuItem
            // 
            this.GateXorToolStripMenuItem.Name = "GateXorToolStripMenuItem";
            this.GateXorToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.GateXorToolStripMenuItem.Text = "异或门";
            this.GateXorToolStripMenuItem.Click += new System.EventHandler(this.AddGateXor);
            // 
            // GatePriToolStripMenuItem
            // 
            this.GatePriToolStripMenuItem.Name = "GatePriToolStripMenuItem";
            this.GatePriToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.GatePriToolStripMenuItem.Text = "优先门（已废弃）";
            this.GatePriToolStripMenuItem.Click += new System.EventHandler(this.AddGatePri);
            // 
            // GateInhibitToolStripMenuItem
            // 
            this.GateInhibitToolStripMenuItem.Name = "GateInhibitToolStripMenuItem";
            this.GateInhibitToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.GateInhibitToolStripMenuItem.Text = "禁门";
            this.GateInhibitToolStripMenuItem.Click += new System.EventHandler(this.AddGateInhibit);
            // 
            // AddGateSequenceAnd
            // 
            this.AddGateSequenceAnd.Name = "AddGateSequenceAnd";
            this.AddGateSequenceAnd.Size = new System.Drawing.Size(170, 22);
            this.AddGateSequenceAnd.Text = "顺序与门";
            this.AddGateSequenceAnd.Click += new System.EventHandler(this.AddGateSequenceAnd_Click);
            // 
            // AddGateElect
            // 
            this.AddGateElect.Name = "AddGateElect";
            this.AddGateElect.Size = new System.Drawing.Size(170, 22);
            this.AddGateElect.Text = "表决门";
            this.AddGateElect.Click += new System.EventHandler(this.AddGateElect_Click);
            // 
            // AddEventToolStripMenuItem
            // 
            this.AddEventToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EventIntermediateToolStripMenuItem,
            this.EventBasicToolStripMenuItem,
            this.EventConditioningToolStripMenuItem,
            this.EventUndevelopedToolStripMenuItem,
            this.EventInitiatingToolStripMenuItem,
            this.EventInToolStripMenuItem,
            this.EventOutToolStripMenuItem});
            this.AddEventToolStripMenuItem.Name = "AddEventToolStripMenuItem";
            this.AddEventToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.AddEventToolStripMenuItem.Text = "添加事件";
            // 
            // EventIntermediateToolStripMenuItem
            // 
            this.EventIntermediateToolStripMenuItem.Name = "EventIntermediateToolStripMenuItem";
            this.EventIntermediateToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.EventIntermediateToolStripMenuItem.Text = "中间事件";
            this.EventIntermediateToolStripMenuItem.Click += new System.EventHandler(this.AddEventIntermediate);
            // 
            // EventBasicToolStripMenuItem
            // 
            this.EventBasicToolStripMenuItem.Name = "EventBasicToolStripMenuItem";
            this.EventBasicToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.EventBasicToolStripMenuItem.Text = "基本事件";
            this.EventBasicToolStripMenuItem.Click += new System.EventHandler(this.AddEventBasic);
            // 
            // EventConditioningToolStripMenuItem
            // 
            this.EventConditioningToolStripMenuItem.Name = "EventConditioningToolStripMenuItem";
            this.EventConditioningToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.EventConditioningToolStripMenuItem.Text = "条件事件";
            this.EventConditioningToolStripMenuItem.Click += new System.EventHandler(this.AddEventConditioning);
            // 
            // EventUndevelopedToolStripMenuItem
            // 
            this.EventUndevelopedToolStripMenuItem.Name = "EventUndevelopedToolStripMenuItem";
            this.EventUndevelopedToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.EventUndevelopedToolStripMenuItem.Text = "省略事件";
            this.EventUndevelopedToolStripMenuItem.Click += new System.EventHandler(this.AddEventUnDeveloped);
            // 
            // EventInitiatingToolStripMenuItem
            // 
            this.EventInitiatingToolStripMenuItem.Name = "EventInitiatingToolStripMenuItem";
            this.EventInitiatingToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.EventInitiatingToolStripMenuItem.Text = "正常事件";
            this.EventInitiatingToolStripMenuItem.Click += new System.EventHandler(this.AddEventInitiating);
            // 
            // EventInToolStripMenuItem
            // 
            this.EventInToolStripMenuItem.Name = "EventInToolStripMenuItem";
            this.EventInToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.EventInToolStripMenuItem.Text = "入事件";
            this.EventInToolStripMenuItem.Click += new System.EventHandler(this.AddEventIn);
            // 
            // EventOutToolStripMenuItem
            // 
            this.EventOutToolStripMenuItem.Name = "EventOutToolStripMenuItem";
            this.EventOutToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.EventOutToolStripMenuItem.Text = "出事件";
            this.EventOutToolStripMenuItem.Click += new System.EventHandler(this.AddEventOut);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(203, 6);
            // 
            // 标记ToolStripMenuItem
            // 
            this.标记ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MarkUnfinishedToolStripMenuItem,
            this.MarkFinishedToolStripMenuItem,
            this.MarkImportantToolStripMenuItem});
            this.标记ToolStripMenuItem.Name = "标记ToolStripMenuItem";
            this.标记ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.标记ToolStripMenuItem.Text = "标记";
            // 
            // MarkUnfinishedToolStripMenuItem
            // 
            this.MarkUnfinishedToolStripMenuItem.Name = "MarkUnfinishedToolStripMenuItem";
            this.MarkUnfinishedToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.MarkUnfinishedToolStripMenuItem.Text = "标记为未完成";
            this.MarkUnfinishedToolStripMenuItem.Click += new System.EventHandler(this.MarkUnfinishedToolStripMenuItem_Click);
            // 
            // MarkFinishedToolStripMenuItem
            // 
            this.MarkFinishedToolStripMenuItem.Name = "MarkFinishedToolStripMenuItem";
            this.MarkFinishedToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.MarkFinishedToolStripMenuItem.Text = "标记为已完成";
            this.MarkFinishedToolStripMenuItem.Click += new System.EventHandler(this.MarkFinishedToolStripMenuItem_Click);
            // 
            // MarkImportantToolStripMenuItem
            // 
            this.MarkImportantToolStripMenuItem.Name = "MarkImportantToolStripMenuItem";
            this.MarkImportantToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.MarkImportantToolStripMenuItem.Text = "标记为重要";
            this.MarkImportantToolStripMenuItem.Click += new System.EventHandler(this.MarkImportantToolStripMenuItem_Click);
            // 
            // CreateChildTreeToolStripMenuItem
            // 
            this.CreateChildTreeToolStripMenuItem.Name = "CreateChildTreeToolStripMenuItem";
            this.CreateChildTreeToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.CreateChildTreeToolStripMenuItem.Text = "创建子树";
            this.CreateChildTreeToolStripMenuItem.Click += new System.EventHandler(this.CreateChildTreeToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(203, 6);
            // 
            // CopyToolStripMenuItem
            // 
            this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            this.CopyToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.CopyToolStripMenuItem.Text = "复制事件";
            this.CopyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // PasteToolStripMenuItem
            // 
            this.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem";
            this.PasteToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.PasteToolStripMenuItem.Text = "粘贴事件信息至本节点下";
            this.PasteToolStripMenuItem.Visible = false;
            this.PasteToolStripMenuItem.Click += new System.EventHandler(this.PasteToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(203, 6);
            // 
            // DeleteNodeToolStripMenuItem
            // 
            this.DeleteNodeToolStripMenuItem.Name = "DeleteNodeToolStripMenuItem";
            this.DeleteNodeToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.DeleteNodeToolStripMenuItem.Text = "删除节点";
            this.DeleteNodeToolStripMenuItem.Click += new System.EventHandler(this.DeleteNodeToolStripMenuItem_Click);
            // 
            // DuplicateToolStripMenuItem
            // 
            this.DuplicateToolStripMenuItem.Name = "DuplicateToolStripMenuItem";
            this.DuplicateToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.DuplicateToolStripMenuItem.Text = "制作事件信息副本";
            this.DuplicateToolStripMenuItem.Click += new System.EventHandler(this.DuplicateToolStripMenuItem_Click);
            // 
            // PasteDuplicateToolStripMenuItem
            // 
            this.PasteDuplicateToolStripMenuItem.Name = "PasteDuplicateToolStripMenuItem";
            this.PasteDuplicateToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.PasteDuplicateToolStripMenuItem.Text = "粘贴事件信息副本";
            this.PasteDuplicateToolStripMenuItem.Visible = false;
            this.PasteDuplicateToolStripMenuItem.Click += new System.EventHandler(this.PasteDuplicateToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ftaformmenuToolStripMenuItem,
            this.minimalCutToolStripMenuItem,
            this.configurationToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(805, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // ftaformmenuToolStripMenuItem
            // 
            this.ftaformmenuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gateToolStripMenuItem,
            this.eventToolStripMenuItem});
            this.ftaformmenuToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.ftaformmenuToolStripMenuItem.Name = "ftaformmenuToolStripMenuItem";
            this.ftaformmenuToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.ftaformmenuToolStripMenuItem.Text = "添加";
            // 
            // gateToolStripMenuItem
            // 
            this.gateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gateAndToolStripMenuItem1,
            this.gateOrToolStripMenuItem1,
            this.gateXorToolStripMenuItem1,
            this.gateInhibitToolStripMenuItem1,
            this.gatePriToolStripMenuItem1,
            this.gateSequenceAndToolStripMenuItem,
            this.gateElectToolStripMenuItem});
            this.gateToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.gateToolStripMenuItem.MergeIndex = 2;
            this.gateToolStripMenuItem.Name = "gateToolStripMenuItem";
            this.gateToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.gateToolStripMenuItem.Text = "门节点";
            this.gateToolStripMenuItem.Visible = false;
            // 
            // gateAndToolStripMenuItem1
            // 
            this.gateAndToolStripMenuItem1.Name = "gateAndToolStripMenuItem1";
            this.gateAndToolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
            this.gateAndToolStripMenuItem1.Text = "与门";
            this.gateAndToolStripMenuItem1.Click += new System.EventHandler(this.AddGateAnd);
            // 
            // gateOrToolStripMenuItem1
            // 
            this.gateOrToolStripMenuItem1.Name = "gateOrToolStripMenuItem1";
            this.gateOrToolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
            this.gateOrToolStripMenuItem1.Text = "或门";
            this.gateOrToolStripMenuItem1.Click += new System.EventHandler(this.AddGateOr);
            // 
            // gateXorToolStripMenuItem1
            // 
            this.gateXorToolStripMenuItem1.Name = "gateXorToolStripMenuItem1";
            this.gateXorToolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
            this.gateXorToolStripMenuItem1.Text = "异或门";
            this.gateXorToolStripMenuItem1.Click += new System.EventHandler(this.AddGateXor);
            // 
            // gateInhibitToolStripMenuItem1
            // 
            this.gateInhibitToolStripMenuItem1.Name = "gateInhibitToolStripMenuItem1";
            this.gateInhibitToolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
            this.gateInhibitToolStripMenuItem1.Text = "禁门";
            this.gateInhibitToolStripMenuItem1.Click += new System.EventHandler(this.AddGateInhibit);
            // 
            // gatePriToolStripMenuItem1
            // 
            this.gatePriToolStripMenuItem1.Name = "gatePriToolStripMenuItem1";
            this.gatePriToolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
            this.gatePriToolStripMenuItem1.Text = "优先门";
            this.gatePriToolStripMenuItem1.Click += new System.EventHandler(this.AddGatePri);
            // 
            // gateSequenceAndToolStripMenuItem
            // 
            this.gateSequenceAndToolStripMenuItem.Name = "gateSequenceAndToolStripMenuItem";
            this.gateSequenceAndToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.gateSequenceAndToolStripMenuItem.Text = "顺序与门";
            this.gateSequenceAndToolStripMenuItem.Click += new System.EventHandler(this.AddGateSequenceAnd_Click);
            // 
            // gateElectToolStripMenuItem
            // 
            this.gateElectToolStripMenuItem.Name = "gateElectToolStripMenuItem";
            this.gateElectToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.gateElectToolStripMenuItem.Text = "表决门";
            this.gateElectToolStripMenuItem.Click += new System.EventHandler(this.AddGateElect_Click);
            // 
            // eventToolStripMenuItem
            // 
            this.eventToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eventBasicToolStripMenuItem1,
            this.eventIntermediateToolStripMenuItem1,
            this.eventInitiatingToolStripMenuItem1,
            this.eventUndevelopedToolStripMenuItem1,
            this.eventConditioningToolStripMenuItem1,
            this.eventOutToolStripMenuItem1,
            this.eventInToolStripMenuItem1});
            this.eventToolStripMenuItem.Name = "eventToolStripMenuItem";
            this.eventToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.eventToolStripMenuItem.Text = "事件节点";
            this.eventToolStripMenuItem.Visible = false;
            // 
            // eventBasicToolStripMenuItem1
            // 
            this.eventBasicToolStripMenuItem1.Name = "eventBasicToolStripMenuItem1";
            this.eventBasicToolStripMenuItem1.Size = new System.Drawing.Size(134, 22);
            this.eventBasicToolStripMenuItem1.Text = "基本事件";
            this.eventBasicToolStripMenuItem1.Click += new System.EventHandler(this.AddEventBasic);
            // 
            // eventIntermediateToolStripMenuItem1
            // 
            this.eventIntermediateToolStripMenuItem1.Name = "eventIntermediateToolStripMenuItem1";
            this.eventIntermediateToolStripMenuItem1.Size = new System.Drawing.Size(134, 22);
            this.eventIntermediateToolStripMenuItem1.Text = "中间事件";
            this.eventIntermediateToolStripMenuItem1.Click += new System.EventHandler(this.AddEventIntermediate);
            // 
            // eventInitiatingToolStripMenuItem1
            // 
            this.eventInitiatingToolStripMenuItem1.Name = "eventInitiatingToolStripMenuItem1";
            this.eventInitiatingToolStripMenuItem1.Size = new System.Drawing.Size(134, 22);
            this.eventInitiatingToolStripMenuItem1.Text = "正常事件";
            this.eventInitiatingToolStripMenuItem1.Click += new System.EventHandler(this.AddEventInitiating);
            // 
            // eventUndevelopedToolStripMenuItem1
            // 
            this.eventUndevelopedToolStripMenuItem1.Name = "eventUndevelopedToolStripMenuItem1";
            this.eventUndevelopedToolStripMenuItem1.Size = new System.Drawing.Size(134, 22);
            this.eventUndevelopedToolStripMenuItem1.Text = "未展开事件";
            this.eventUndevelopedToolStripMenuItem1.Click += new System.EventHandler(this.AddEventUnDeveloped);
            // 
            // eventConditioningToolStripMenuItem1
            // 
            this.eventConditioningToolStripMenuItem1.Name = "eventConditioningToolStripMenuItem1";
            this.eventConditioningToolStripMenuItem1.Size = new System.Drawing.Size(134, 22);
            this.eventConditioningToolStripMenuItem1.Text = "条件事件";
            this.eventConditioningToolStripMenuItem1.Click += new System.EventHandler(this.AddEventConditioning);
            // 
            // eventOutToolStripMenuItem1
            // 
            this.eventOutToolStripMenuItem1.Name = "eventOutToolStripMenuItem1";
            this.eventOutToolStripMenuItem1.Size = new System.Drawing.Size(134, 22);
            this.eventOutToolStripMenuItem1.Text = "出事件";
            this.eventOutToolStripMenuItem1.Click += new System.EventHandler(this.AddEventOut);
            // 
            // eventInToolStripMenuItem1
            // 
            this.eventInToolStripMenuItem1.Name = "eventInToolStripMenuItem1";
            this.eventInToolStripMenuItem1.Size = new System.Drawing.Size(134, 22);
            this.eventInToolStripMenuItem1.Text = "入事件";
            this.eventInToolStripMenuItem1.Click += new System.EventHandler(this.AddEventIn);
            // 
            // minimalCutToolStripMenuItem
            // 
            this.minimalCutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateMinimalCutToolStripMenuItem,
            this.exportDiagramToolStripMenuItem});
            this.minimalCutToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.minimalCutToolStripMenuItem.Name = "minimalCutToolStripMenuItem";
            this.minimalCutToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.minimalCutToolStripMenuItem.Text = "输出";
            // 
            // generateMinimalCutToolStripMenuItem
            // 
            this.generateMinimalCutToolStripMenuItem.Name = "generateMinimalCutToolStripMenuItem";
            this.generateMinimalCutToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.generateMinimalCutToolStripMenuItem.Text = "计算最小割集";
            this.generateMinimalCutToolStripMenuItem.Click += new System.EventHandler(this.generateMinimalCutToolStripMenuItem_Click);
            // 
            // exportDiagramToolStripMenuItem
            // 
            this.exportDiagramToolStripMenuItem.Name = "exportDiagramToolStripMenuItem";
            this.exportDiagramToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.exportDiagramToolStripMenuItem.Text = "导出故障树图像";
            this.exportDiagramToolStripMenuItem.Click += new System.EventHandler(this.exportDiagramToolStripMenuItem_Click);
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorConfigToolStripMenuItem});
            this.configurationToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.configurationToolStripMenuItem.Text = "配置";
            // 
            // colorConfigToolStripMenuItem
            // 
            this.colorConfigToolStripMenuItem.Name = "colorConfigToolStripMenuItem";
            this.colorConfigToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.colorConfigToolStripMenuItem.Text = "节点颜色配置";
            this.colorConfigToolStripMenuItem.ToolTipText = "更改故障树节点的颜色配置";
            this.colorConfigToolStripMenuItem.Click += new System.EventHandler(this.colorConfigToolStripMenuItem_Click);
            // 
            // FTAForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(805, 291);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.addFlow1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HideOnClose = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FTAForm";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
            this.Text = "故障树分析";
            this.Activated += new System.EventHandler(this.FTAForm_Activated);
            this.Load += new System.EventHandler(this.FTAForm_Load);
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.FTAForm_Scroll);
            this.Enter += new System.EventHandler(this.FTAForm_Enter);
            this.FTAcontextMenuStrip.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.ToolStripMenuItem AddGateSequenceAnd;
        private System.Windows.Forms.ToolStripMenuItem AddGateElect;
        private System.Windows.Forms.ToolStripMenuItem 标记ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MarkUnfinishedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MarkFinishedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MarkImportantToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CreateChildTreeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem DuplicateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PasteDuplicateToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ftaformmenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gateAndToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem gateOrToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem gateXorToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem gateInhibitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem eventToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gatePriToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem gateSequenceAndToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gateElectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eventBasicToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem eventIntermediateToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem eventInitiatingToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem eventUndevelopedToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem eventConditioningToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem eventOutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem eventInToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem minimalCutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateMinimalCutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportDiagramToolStripMenuItem;
    }
}

