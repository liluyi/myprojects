using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lassalle.Flow;


namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeAddFlow();

            EventIntermediateToolStripMenuItem.Click += new EventHandler(AddEventIntermediate);
            EventInitiatingToolStripMenuItem.Click += new EventHandler(AddEventInitiating);
            EventBasicToolStripMenuItem.Click += new EventHandler(AddEventBasic);
            EventConditioningToolStripMenuItem.Click += new EventHandler(AddEventConditioning);
            EventInToolStripMenuItem.Click += new EventHandler(AddEventIn);
            EventOutToolStripMenuItem.Click += new EventHandler(AddEventOut);
            EventUndevelopedToolStripMenuItem.Click += new EventHandler(AddEventUnDeveloped);
            GateAndToolStripMenuItem.Click += new EventHandler(AddGateAnd);
            GateInhibitToolStripMenuItem.Click += new EventHandler(AddGateInhibit);
            GateOrToolStripMenuItem.Click += new EventHandler(AddGateOr);
            GatePriToolStripMenuItem.Click += new EventHandler(AddGatePri);
            GateXorToolStripMenuItem.Click += new EventHandler(AddGateXor);
        }

        public List<Node> mNodeList = new List<Node>();
        public TreeInfoCollection treeinfocollection = new TreeInfoCollection();
        public int level = 0;
        public int maxlevelCount = 1;
        public int index = 0;
        TreeNodeInfo topnode;

        //SratProject mProject = (SratProject)ProjectManager.ProjectManagerSington.GetProject(ProjectManager.ProjectManagerSington.GetProjectUUID());

        /// <summary>
        /// 初始化addflow
        /// </summary>
        public void InitializeAddFlow()
        {
            addFlow1.Parent = this;
            addFlow1.Dock = DockStyle.Fill;
            addFlow1.AutoScroll = true;
            addFlow1.BackColor = SystemColors.Window;
            addFlow1.CanMoveNode = true;
            addFlow1.CanDrawNode = false;//不允许用户自动绘制
            addFlow1.CanDrawLink = false;
            addFlow1.CanMoveNode = false;

            addFlow1.Grid.Style = GridStyle.DottedLines;
            addFlow1.DefLinkProp.Jump = Jump.None;
            addFlow1.DefLinkProp.AdjustDst = true;
            addFlow1.DefLinkProp.AdjustOrg = true;
            addFlow1.DefNodeProp.Shape.Style = ShapeStyle.Rectangle;
            addFlow1.CanSizeNode = false;

            //设置顶事件节点
            topnode= new TreeNodeInfo(Guid.NewGuid().ToString(), "顶事件", NodeType.EventIntermediate, string.Empty, new List<string>(), "FTA的顶节点", 0);
            treeinfocollection.Add(topnode);
            //刷新画布
            ClearAddFlow();
            ClearNodeList();
            refreshGraph();
        }

        /// <summary>
        /// 后续遍历整棵树，实现节点顺序绘制
        /// </summary>
        /// <param name="tni"></param>
        public void TreeTranverse(TreeNodeInfo tni)
        {
            foreach (string treenodeID in tni.childrenNodesID)
            {
                TreeNodeInfo childnode=SearchTreeNodeInfo(treenodeID);
                if (childnode.childrenNodesID.Count != 0)
                    TreeTranverse(childnode);//递归
                else
                {
                    //(addFlow1.Width / GetMaximumNodes())
                    Node node = new Node(index*100, childnode.level * 100 + 10, 60, 60, childnode.nodeID);//先绘制叶子节点
                    paint(node, childnode);
                    index++;
                }
            }
            Node rootnode = new Node((index * 2 - tni.childrenNodesID.Count ) / 2 * 100, tni.level * 100 + 10, 60, 60, tni.nodeID);
            paint(rootnode, tni);

            //绘制连接线
            foreach (string treenodeID in tni.childrenNodesID)
            {              
                Node childnode = SearchAddFlowNode(treenodeID);
                Link link = new Link();
                link.Line.OrthogonalDynamic = true;
                link.Line.Style = LineStyle.VHV;//线性
                addFlow1.AddLink(link, childnode, rootnode);
            }        
        }

        /// <summary>
        /// 绘制节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="tni"></param>
        public void paint(Node node,TreeNodeInfo tni)
        {
            NodeType nt = tni.nodeType;
            switch (nt)
            {
                case NodeType.EventIntermediate://矩形，中间事件和顶事件
                    {
                        node.Shape.Style = ShapeStyle.Rectangle;//节点形状
                        break;
                    }
                case NodeType.GateOr://获门
                    {
                        node.Shape.Style = ShapeStyle.OrGate;
                        node.Shape.Orientation = ShapeOrientation.so_90;
                        break;
                    }
                case NodeType.GateInhibit://禁门
                    {
                        node.Shape.Style = ShapeStyle.Hexagon;
                        node.Shape.Orientation = ShapeOrientation.so_180;
                        break;
                    }
                case NodeType.EventInitiating:
                    {
                        node.Shape.Style = ShapeStyle.Custom;
                        node.Shape.GraphicsPath = new FTAShapes(nt).Path;
                        node.Shape.Orientation = ShapeOrientation.so_180;
                        break;
                    }
                default://其它门
                    {
                        node.Shape.Style = ShapeStyle.Custom;
                        node.Shape.GraphicsPath = new FTAShapes(nt).Path;
                        break;
                    }
            }
            node.Tag = tni.nodeType;
            node.Url = tni.nodeID;
            //添加至addflow
            addFlow1.Nodes.Add(node);
            mNodeList.Add(node);
        }
        /// <summary>
        /// 刷新AddFlow
        /// </summary>
        public void refreshGraph()
        {
            index = 0;
            TreeTranverse(topnode);
        }

        public AddFlow GetAddFlow()
        {
            return addFlow1;
        }

        public void ClearNodeList()
        {
            this.mNodeList.Clear();
        }

        public void ClearAddFlow()
        {
            this.addFlow1.Nodes.Clear();
        }

        /// <summary>
        /// 根据ID搜索节点信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns>节点信息</returns>
        public TreeNodeInfo SearchTreeNodeInfo(string id)
        {
            TreeNodeInfo tni = null;
            foreach (TreeNodeInfo info in treeinfocollection)
            {
                if (info.nodeID.Equals(id))
                {
                    return info;
                }
            }
            return tni;
        }
        public Node SearchAddFlowNode(string id)
        {
            Node nd = null;
            foreach (Node node in mNodeList)
                if (node.Url.Equals(id))
                    nd = node;
            return nd;
        }

        /// <summary>
        /// 获取叶子节点数的最大值，用于设置画布上每一个节点的大小和位置
        /// </summary>
        /// <returns>叶子节点的最大值</returns>
        public int GetMaximumNodes()
        {
            int count = 0;
            foreach (TreeNodeInfo info in treeinfocollection)
            {
                if (info.childrenNodesID.Count==0)
                    count++;
            }
            return count;
        }
        /// <summary>
        /// 添加中间事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddEventIntermediate(object sender, EventArgs e)
        {
            //UserUIEventArgs args = new UserUIEventArgs("TreeViewForm");
            //SratTreeView mForm = ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(this, args) as SratTreeView;

            //DateTime dt = DateTime.Now;
            //mForm.addNewNodeInfo("NewNode", "module" + Convert.ToString(dt), this.addFlow1.SelectedItem.Tag.ToString());
            //mForm.findNode(mForm.GetTreeViewCollection(), this.addFlow1.SelectedItem.Tag.ToString());
            //mForm.addNewNode("module" + Convert.ToString(dt));
            string parentID = this.addFlow1.SelectedItem.Url;//父节点ID
            TreeNodeInfo parentNode = SearchTreeNodeInfo(parentID);//获取父节点
            int nodelevel = parentNode.level + 1;
            if (level < nodelevel)
                level = nodelevel;
            string childnodeID=Guid.NewGuid().ToString();
            TreeNodeInfo tni = new TreeNodeInfo(childnodeID, "新节点", NodeType.EventIntermediate, parentID, new List<string>(), "节点描述信息", nodelevel);
            treeinfocollection.Add(tni);
            parentNode.childrenNodesID.Add(childnodeID);

            //刷新整张画布
            ClearAddFlow();
            ClearNodeList();
            refreshGraph();
            //refreshChart();
        }

        public void AddEventInitiating(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;
            TreeNodeInfo parentNode = SearchTreeNodeInfo(parentID);//获取父节点
            int nodelevel = parentNode.level + 1;
            if (level < nodelevel)
                level = nodelevel;
            string childnodeID = Guid.NewGuid().ToString();
            TreeNodeInfo tni = new TreeNodeInfo(childnodeID, "新节点", NodeType.EventInitiating, parentID, new List<string>(), "节点描述信息", nodelevel);
            treeinfocollection.Add(tni);
            parentNode.childrenNodesID.Add(childnodeID);

            //刷新整张画布
            ClearAddFlow();
            ClearNodeList();
            refreshGraph();
            //refreshChart();
        }
        public void AddEventBasic(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;
            TreeNodeInfo parentNode = SearchTreeNodeInfo(parentID);//获取父节点
            int nodelevel = parentNode.level + 1;
            if (level < nodelevel)
                level = nodelevel;
            string childnodeID = Guid.NewGuid().ToString();
            TreeNodeInfo tni = new TreeNodeInfo(childnodeID, "新节点", NodeType.EventBasic, parentID, new List<string>(), "节点描述信息", nodelevel);
            treeinfocollection.Add(tni);
            parentNode.childrenNodesID.Add(childnodeID);
            //刷新整张画布
            ClearAddFlow();
            ClearNodeList();
            refreshGraph();
            //refreshChart();
        }
        public void AddEventUnDeveloped(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;
            TreeNodeInfo parentNode = SearchTreeNodeInfo(parentID);//获取父节点
            int nodelevel = parentNode.level + 1;
            if (level < nodelevel)
                level = nodelevel;
            string childnodeID = Guid.NewGuid().ToString();
            TreeNodeInfo tni = new TreeNodeInfo(childnodeID, "新节点", NodeType.EventUndeveloped, parentID, new List<string>(), "节点描述信息", nodelevel);
            treeinfocollection.Add(tni);
            parentNode.childrenNodesID.Add(childnodeID);

            //刷新整张画布
            ClearAddFlow();
            ClearNodeList();
            refreshGraph();
            //refreshChart();
        }
        public void AddEventConditioning(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;
            TreeNodeInfo parentNode = SearchTreeNodeInfo(parentID);//获取父节点
            int nodelevel = parentNode.level + 1;
            if (level < nodelevel)
                level = nodelevel;
            string childnodeID = Guid.NewGuid().ToString();
            TreeNodeInfo tni = new TreeNodeInfo(childnodeID, "新节点", NodeType.EventConditioning, parentID, new List<string>(), "节点描述信息", nodelevel);
            treeinfocollection.Add(tni);
            parentNode.childrenNodesID.Add(childnodeID);

            //刷新整张画布
            ClearAddFlow();
            ClearNodeList();
            refreshGraph();
            //refreshChart();
        }
        public void AddEventIn(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;
            TreeNodeInfo parentNode = SearchTreeNodeInfo(parentID);//获取父节点
            int nodelevel = parentNode.level + 1;
            if (level < nodelevel)
                level = nodelevel;
            string childnodeID = Guid.NewGuid().ToString();
            TreeNodeInfo tni = new TreeNodeInfo(childnodeID, "新节点", NodeType.EventIn, parentID, new List<string>(), "节点描述信息", nodelevel);
            treeinfocollection.Add(tni);
            parentNode.childrenNodesID.Add(childnodeID);


            //刷新整张画布
            ClearAddFlow();
            ClearNodeList();
            refreshGraph();
            //refreshChart();
        }
        public void AddEventOut(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;
            TreeNodeInfo parentNode = SearchTreeNodeInfo(parentID);//获取父节点
            int nodelevel = parentNode.level + 1;
            if (level < nodelevel)
                level = nodelevel;
            string childnodeID = Guid.NewGuid().ToString();
            TreeNodeInfo tni = new TreeNodeInfo(childnodeID, "新节点", NodeType.EventOut, parentID, new List<string>(), "节点描述信息", nodelevel);
            treeinfocollection.Add(tni);
            parentNode.childrenNodesID.Add(childnodeID);


            //刷新整张画布
            ClearAddFlow();
            ClearNodeList();
            refreshGraph();
            //refreshChart();
        }
        public void AddGateOr(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;
            TreeNodeInfo parentNode = SearchTreeNodeInfo(parentID);//获取父节点
            int nodelevel = parentNode.level + 1;
            if (level < nodelevel)
                level = nodelevel;
            string childnodeID = Guid.NewGuid().ToString();
            TreeNodeInfo tni = new TreeNodeInfo(childnodeID, "新节点", NodeType.GateOr, parentID, new List<string>(), "节点描述信息", nodelevel);
            treeinfocollection.Add(tni);
            parentNode.childrenNodesID.Add(childnodeID);


            //刷新整张画布
            ClearAddFlow();
            ClearNodeList();
            refreshGraph();
            //refreshChart();
        }
        public void AddGateAnd(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;
            TreeNodeInfo parentNode = SearchTreeNodeInfo(parentID);//获取父节点
            int nodelevel = parentNode.level + 1;
            if (level < nodelevel)
                level = nodelevel;
            string childnodeID = Guid.NewGuid().ToString();
            TreeNodeInfo tni = new TreeNodeInfo(childnodeID, "新节点", NodeType.GateAnd, parentID, new List<string>(), "节点描述信息", nodelevel);
            treeinfocollection.Add(tni);
            parentNode.childrenNodesID.Add(childnodeID);

            //刷新整张画布
            ClearAddFlow();
            ClearNodeList();
            refreshGraph();
            //refreshChart();
        }
        public void AddGateInhibit(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;
            TreeNodeInfo parentNode = SearchTreeNodeInfo(parentID);//获取父节点
            int nodelevel = parentNode.level + 1;
            if (level < nodelevel)
                level = nodelevel;
            string childnodeID = Guid.NewGuid().ToString();
            TreeNodeInfo tni = new TreeNodeInfo(childnodeID, "新节点", NodeType.GateInhibit, parentID, new List<string>(), "节点描述信息", nodelevel);
            treeinfocollection.Add(tni);
            parentNode.childrenNodesID.Add(childnodeID);

            //刷新整张画布
            ClearAddFlow();
            ClearNodeList();
            refreshGraph();
            //refreshChart();
        }
        public void AddGatePri(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;
            TreeNodeInfo parentNode = SearchTreeNodeInfo(parentID);//获取父节点
            int nodelevel = parentNode.level + 1;
            if (level < nodelevel)
                level = nodelevel;
            string childnodeID = Guid.NewGuid().ToString();
            TreeNodeInfo tni = new TreeNodeInfo(childnodeID, "新节点", NodeType.GatePri, parentID, new List<string>(), "节点描述信息", nodelevel);
            treeinfocollection.Add(tni);
            parentNode.childrenNodesID.Add(childnodeID);

            //刷新整张画布
            ClearAddFlow();
            ClearNodeList();
            refreshGraph();
            //refreshChart();
        }
        public void AddGateXor(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;
            TreeNodeInfo parentNode = SearchTreeNodeInfo(parentID);//获取父节点
            int nodelevel = parentNode.level + 1;
            if (level < nodelevel)
                level = nodelevel;
            string childnodeID = Guid.NewGuid().ToString();
            TreeNodeInfo tni = new TreeNodeInfo(childnodeID, "新节点", NodeType.GateXor, parentID, new List<string>(), "节点描述信息", nodelevel);
            treeinfocollection.Add(tni);
            parentNode.childrenNodesID.Add(childnodeID);

            //刷新整张画布
            ClearAddFlow();
            ClearNodeList();
            refreshGraph();
            //refreshChart();
        }
        /// <summary>
        /// 添加子系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public void AddSubSystem(object sender, EventArgs e)
        //{
        //    UserUIEventArgs args = new UserUIEventArgs("TreeViewForm");
        //    SratTreeView mForm = ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(this, args) as SratTreeView;


        //    mForm.addNewNodeInfo("NewNode", "subsystem" + mForm.GetNumSubSys().ToString(), "system");
        //    mForm.findNode(mForm.GetTreeViewCollection(), this.addFlow1.SelectedItem.Tag.ToString());
        //    mForm.addNewNode("subsystem" + mForm.GetNumSubSys().ToString());
        //    mForm.upNumSubSys();

        //    ClearAddFlow();
        //    ClearNodeList();
        //    refreshChart();
        //}

        /// <summary>
        /// 添加模块
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public void AddModule(object sender, EventArgs e)
        //{
        //    UserUIEventArgs args = new UserUIEventArgs("TreeViewForm");
        //    SratTreeView mForm = ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(this, args) as SratTreeView;

        //    DateTime dt = DateTime.Now;
        //    mForm.addNewNodeInfo("NewNode", "module" + Convert.ToString(dt),this.addFlow1.SelectedItem.Tag.ToString());
        //    mForm.findNode(mForm.GetTreeViewCollection(), this.addFlow1.SelectedItem.Tag.ToString());
        //    mForm.addNewNode("module" + Convert.ToString(dt));


        //    ClearAddFlow();
        //    ClearNodeList();
        //    refreshChart();
        //}

        public void RightButtonClicked(object sender, MouseEventArgs e)
        {
            //Point p = PointToScreen(new Point(e.X,e.Y));
            //Lassalle.Flow.Item item = this.addFlow1.GetItemAt(new Point(e.X,e.Y));
            //Lassalle.Flow.Item item = this.addFlow1.GetItemAt(p);
            Lassalle.Flow.Item item = addFlow1.PointedItem;
            addFlow1.SelectedItem = item;
            if (item is Node)
            {
                AddEventToolStripMenuItem.Visible = false;//隐藏事件菜单
                AddGateToolStripMenuItem.Visible = false;//隐藏门菜单
                switch ((NodeType)item.Tag)//检查所选item的节点类型
                {
                    case NodeType.EventIntermediate://顶事件或中间事件不能向下添加条件事件、出事件、异或门
                        {
                            AddGateToolStripMenuItem.Visible = true;
                            AddEventToolStripMenuItem.Visible = true;
                            EventConditioningToolStripMenuItem.Visible = false;
                            EventOutToolStripMenuItem.Visible = false;
                            GateXorToolStripMenuItem.Visible = false;
                            break;
                        }
                    case NodeType.GateAnd:
                        {
                            AddEventToolStripMenuItem.Visible = true;
                            break;
                        }
                    case NodeType.GateOr:
                        {
                            AddEventToolStripMenuItem.Visible = true;
                            break;
                        }
                    case NodeType.GateXor:
                        {
                            AddEventToolStripMenuItem.Visible = true;
                            break;
                        }
                    case NodeType.GatePri://除了禁门的其它逻辑门下可添加无限多事件
                        {
                            AddEventToolStripMenuItem.Visible = true;
                            break;
                        }
                    case NodeType.GateInhibit://禁门下下层只能有一个事件
                        {
                            break;
                        }
                    default:
                        {//基本事件圆形、正常事件屋形、条件事件椭圆形、未展开事件菱形、入事件三角竖线符号
                            break;
                        }
                }
                Point p = PointToScreen(new Point(e.X, e.Y));
                FTAcontextMenuStrip.Show(p);
            }
        }

        private void addFlow1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                RightButtonClicked(this, e);
            }
        }



        private void addFlow1_Scroll(object sender, ScrollEventArgs e)
        {
            //addFlow1.Location = this.poin

        }


        public void ChangeNodeName(object sender, EventArgs e)
        {
            addFlow1.CanLabelEdit = true;
            Lassalle.Flow.Item item = this.addFlow1.SelectedItem;
            if (item is Node)
            {
                Node node = (Node)item;
                if (!node.IsEditing)
                {
                    node.BeginEdit();
                }

            }

        }

        private void addFlow1_AfterEdit(object sender, AfterEditEventArgs e)
        {

        }
    }
}
