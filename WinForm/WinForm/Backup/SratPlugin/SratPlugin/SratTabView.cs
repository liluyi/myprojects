using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Platform.Core.Data;
using Platform.Core.UI;
using WeifenLuo.WinFormsUI.Docking;
using Platform.Core;
using Platform.Core.Services;
using Lassalle.Flow;

namespace SratPlugin
{
    public partial class SratTabView : BaseForm
    {
        public SratTabView()
        {
            InitializeComponent();
            InitializeAddFlow();

            systemContextMenu.Items[0].Click+=new EventHandler(AddSubSystem);

            subSystemContextMenu.Items[0].Click+=new EventHandler(AddModule);

            systemContextMenu.Items[2].Click += new EventHandler(AddNewAllocationSolution);

            subSystemContextMenu.Items[3].Click += new EventHandler(AddNewAllocationSolution);

            subSystemContextMenu.Items[2].Click += new EventHandler(DelNode);

            moduleContextMenu.Items[1].Click+=new EventHandler(DelNode);

            subSystemContextMenu.Items[1].Click+=new EventHandler(ChangeNodeName);

            moduleContextMenu.Items[0].Click+=new EventHandler(ChangeNodeName);

            addFlow1.AfterEdit+=new AddFlow.AfterEditEventHandler(change);
        }

        public List<Node> mNodeList = new List<Node>();

        

        SratProject mProject = (SratProject)ProjectManager.ProjectManagerSington.GetProject(ProjectManager.ProjectManagerSington.GetProjectUUID());

        

        public void InitializeAddFlow()
        {
            addFlow1.Parent = this;
            addFlow1.Dock = DockStyle.Fill;
            addFlow1.AutoScroll = true;
            addFlow1.BackColor = SystemColors.Window;
            addFlow1.CanMoveNode = true;
            addFlow1.CanDrawNode = false;
            addFlow1.CanDrawLink = false;
            addFlow1.CanMoveNode = false;

            //Node dn = (Node)addFlow1.DefNodeProp.Clone();
            //Link dl = (Link)addFlow1.DefLinkProp.Clone();
            // 设置默认node的属性
            //dn.FillColor = Color.LightYellow;
            //dn.Shadow.Style = ShadowStyle.RightBottom;
            // 设置默认link的属性
            //dl.DrawColor = Color.Blue;
            //dl.BackMode = BackMode.Opaque;
            addFlow1.Grid.Style = GridStyle.DottedLines;
            addFlow1.DefLinkProp.Jump = Jump.None;
            addFlow1.DefLinkProp.AdjustDst = true;
            addFlow1.DefLinkProp.AdjustOrg = true;
            addFlow1.DefNodeProp.Shape.Style = ShapeStyle.Rectangle;
            addFlow1.CanSizeNode = false;
        }

        public void refreshChart()
        {
            float pixelSysX = 350;
            float pixelSysY = 20;
            float pixelSubSysX = 100;
            float pixelSubSysY = 200;
            float pixelModuleX = 30;
            float pixelModuleY = 350;
            bool rootState = true;
            foreach (TreeNodeInfo tni in mProject.mTreeCollection)
            {
                try
                {
                    if (tni.nodeTag == "system")
                    {
                        rootState = tni.foldOrExpand;
                        if (tni.foldOrExpand)
                        {
                            Node node = new Node(pixelSysX, pixelSysY, 75, 75, tni.nodeName);
                            node.Shape.Style = ShapeStyle.Rectangle;
                            node.Tag = tni.nodeTag;
                            addFlow1.Nodes.Add(node);
                            mNodeList.Add(node);
                            foreach (TreeNodeInfo mNode in mProject.mTreeCollection)
                            {
                                if (mNode.nodeTag.Substring(0, 6) == "subsys")
                                {
                                    if (mNode.foldOrExpand)
                                    {
                                        Node node2 = new Node(pixelSubSysX, pixelSubSysY, 50, 50, mNode.nodeName);
                                        node2.Shape.Style = ShapeStyle.Rectangle;
                                        node2.Tag = mNode.nodeTag;
                                        addFlow1.Nodes.Add(node2);
                                        mNodeList.Add(node2);
                                        foreach (TreeNodeInfo mTreeNodeInfo in mProject.mTreeCollection)
                                        {
                                            if (mTreeNodeInfo.parentNodeName == mNode.nodeTag)
                                            {
                                                Node n = new Node(pixelModuleX, pixelModuleY, 50, 50, mTreeNodeInfo.nodeName);
                                                //node.FillColor = Color.LightGray;
                                                n.Shape.Style = ShapeStyle.Rectangle;
                                                n.Tag = mTreeNodeInfo.nodeTag;
                                                addFlow1.Nodes.Add(n);
                                                mNodeList.Add(n);
                                                pixelModuleX += 60;
                                            }
                                        }
                                        pixelModuleX += 40;

                                        pixelSubSysX += 200;
                                    }
                                    else
                                    {
                                        Node node2 = new Node(pixelSubSysX, pixelSubSysY, 50, 50, mNode.nodeName);
                                        node2.Shape.Style = ShapeStyle.Rectangle;
                                        node2.Tag = mNode.nodeTag;
                                        node2.DrawColor = Color.LightGray;
                                        node2.FillColor = Color.LightGray;
                                        addFlow1.Nodes.Add(node2);
                                        mNodeList.Add(node2);
                                        foreach (TreeNodeInfo mTreeNodeInfo in mProject.mTreeCollection)
                                        {
                                            if (mTreeNodeInfo.parentNodeName == mNode.nodeTag)
                                            {
                                                //Node n = new Node(pixelModuleX, pixelModuleY, 80, 80, mTreeNodeInfo.nodeName);
                                                //node.FillColor = Color.LightGray;
                                                //n.Tag = mTreeNodeInfo.nodeTag;
                                                //addFlow1.Nodes.Add(n);
                                                //mNodeList.Add(n);
                                                pixelModuleX += 60;
                                            }
                                        }
                                        pixelModuleX += 40;
                                        pixelSubSysX += 200;
                                        continue;
                                    }
                                }
                            }
                            
                        }
                        else
                        {
                            Node node = new Node(pixelSysX, pixelSysY, 75, 75, tni.nodeName);
                            node.Tag = tni.nodeTag;
                            node.Shape.Style = ShapeStyle.Rectangle;
                            node.DrawColor = Color.LightGray;
                            node.FillColor = Color.LightGray;
                            addFlow1.Nodes.Add(node);
                            mNodeList.Add(node);
                            break;
                        }
                    }
                }
                catch (Exception r)
                {
                    MessageBox.Show(r.ToString());
                }
            }

            /*foreach (TreeNodeInfo tni in mProject.mTreeCollection)
            {
                try
                {
                    if (rootState)
                    {
                        if (tni.nodeTag.Substring(0, 6) == "subsys")
                        {
                            if (tni.foldOrExpand)
                            {
                                Node node = new Node(pixelSubSysX, pixelSubSysY, 80, 80, tni.nodeName);

                                node.Tag = tni.nodeTag;
                                addFlow1.Nodes.Add(node);
                                mNodeList.Add(node);
                                foreach (TreeNodeInfo mTreeNodeInfo in mProject.mTreeCollection)
                                {
                                    if (mTreeNodeInfo.parentNodeName == tni.nodeTag)
                                    {
                                        Node n = new Node(pixelModuleX, pixelModuleY, 80, 80, tni.nodeName);
                                        //node.FillColor = Color.LightGray;
                                        n.Tag = mTreeNodeInfo.nodeTag;
                                        addFlow1.Nodes.Add(n);
                                        mNodeList.Add(n);
                                        pixelModuleX += 100;
                                    }
                                }

                                pixelSubSysX += 200;
                            }
                            else
                            {
                                Node node = new Node(pixelSubSysX, pixelSubSysY, 80, 80, tni.nodeName);

                                node.Tag = tni.nodeTag;
                                node.DrawColor = Color.LightGray;
                                addFlow1.Nodes.Add(node);
                                mNodeList.Add(node);
                                
                                pixelSubSysX += 200;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                catch (Exception r)
                {
                    MessageBox.Show(r.ToString());
                }
            }

            /*foreach (TreeNodeInfo tni in mProject.mTreeCollection)
            {
                try
                {
                    if (tni.nodeTag.Substring(0, 6) == "module")
                    {
                        Node node = new Node(pixelModuleX, pixelModuleY, 80, 80, tni.nodeName);
                        //node.FillColor = Color.LightGray;
                        node.Tag = tni.nodeTag;
                        addFlow1.Nodes.Add(node);
                        mNodeList.Add(node);
                        pixelModuleX += 100;
                    }
                }
                catch (Exception r)
                {
                    MessageBox.Show(r.ToString());
                }
            }*/

            foreach (Node node in mNodeList)
            {
                try
                {
                    if (node.Tag.ToString().Substring(0, 6) == "subsys")
                    {
                        Link link = new Link();
                        link.Line.OrthogonalDynamic = true;
                        link.Line.Style = LineStyle.VHV;
                        
                        //link.Org = node;
                        //link.Dst = findAddFlowNode("system");
                        addFlow1.AddLink(link,node,findAddFlowNode("system"));
                        //addFlow1.CreateLink(node, findAddFlowNode("system"));
                    }
                    else if (node.Tag.ToString().Substring(0, 6) == "module")
                    {
                        string parentNode = "";
                        foreach (TreeNodeInfo tni in mProject.mTreeCollection)
                        {
                            if (tni.nodeTag == node.Tag.ToString())
                            {
                                parentNode = tni.parentNodeName;
                                break;
                            }
                        }
                        Link link = new Link();
                        link.Line.OrthogonalDynamic = true;
                        link.Line.Style = LineStyle.VHV;
                        //link.Org = node;
                        //link.Dst = findAddFlowNode("system");
                        addFlow1.AddLink(link, node, findAddFlowNode(parentNode));
                        //addFlow1.CreateLink(node, findAddFlowNode(parentNode));
                    }
                }
                catch (Exception r)
                {
                    MessageBox.Show(r.ToString());
                }
            }
        }

        public Node findAddFlowNode(string tag)
        {
            SratProject mProject = (SratProject)ProjectManager.ProjectManagerSington.GetProject(ProjectManager.ProjectManagerSington.GetProjectUUID());
            Node n = new Node();
            foreach (Node node in mNodeList)
            {
                if (node.Tag.ToString() == tag)
                {
                    return node;
                }
            }
            return n;
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

        public void AddSubSystem(object sender, EventArgs e)
        {
            UserUIEventArgs args = new UserUIEventArgs("TreeViewForm");
            SratTreeView mForm = ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(this, args) as SratTreeView;
            
            
            mForm.addNewNodeInfo("NewNode", "subsystem" + mForm.GetNumSubSys().ToString(), "system");
            mForm.findNode(mForm.GetTreeViewCollection(), this.addFlow1.SelectedItem.Tag.ToString());
            mForm.addNewNode("subsystem" + mForm.GetNumSubSys().ToString());
            mForm.upNumSubSys();

            ClearAddFlow();
            ClearNodeList();
            refreshChart();
        }

        public void AddModule(object sender, EventArgs e)
        {
            UserUIEventArgs args = new UserUIEventArgs("TreeViewForm");
            SratTreeView mForm = ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(this, args) as SratTreeView;

            DateTime dt = DateTime.Now;
            mForm.addNewNodeInfo("NewNode", "module" + Convert.ToString(dt),this.addFlow1.SelectedItem.Tag.ToString());
            mForm.findNode(mForm.GetTreeViewCollection(), this.addFlow1.SelectedItem.Tag.ToString());
            mForm.addNewNode("module" + Convert.ToString(dt));
            

            ClearAddFlow();
            ClearNodeList();
            refreshChart();
        }

        public void RightButtonClicked(object sender, MouseEventArgs e)
        {
            //Point p = PointToScreen(new Point(e.X,e.Y));
            //Lassalle.Flow.Item item = this.addFlow1.GetItemAt(new Point(e.X,e.Y));
            //Lassalle.Flow.Item item = this.addFlow1.GetItemAt(p);
            Lassalle.Flow.Item item = addFlow1.PointedItem;
            addFlow1.SelectedItem = item;
            if (item is Node)
            {
                if (item.Tag.ToString() == "system")
                {
                    Point p = PointToScreen(new Point(e.X, e.Y));
                    systemContextMenu.Show(p);
                }
                else if (item.Tag.ToString().Substring(0, 6) == "subsys")
                {
                    Point p = PointToScreen(new Point(e.X, e.Y));
                    subSystemContextMenu.Show(p);
                }
                else if (item.Tag.ToString().Substring(0, 6) == "module")
                {
                    Point p = PointToScreen(new Point(e.X, e.Y));
                    moduleContextMenu.Show(p);
                }
            }
        }

        private void addFlow1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                RightButtonClicked(this,e);
            }
        }



        private void addFlow1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Lassalle.Flow.Item item = addFlow1.PointedItem;//this.addFlow1.GetItemAt(new Point(e.X, e.Y));
            addFlow1.SelectedItem = item;
            int count = 0;
            if (item != null && item is Node)
            {
                foreach (TreeNodeInfo mTreeNodeInfo in mProject.mTreeCollection)
                {
                    if (item.Tag.ToString() == mTreeNodeInfo.nodeTag && (item.Tag.ToString().Substring(0, 6) == "system" || item.Tag.ToString().Substring(0, 6) == "subsys"))
                    {
                        mTreeNodeInfo.foldOrExpand = !mTreeNodeInfo.foldOrExpand;
                        count++;
                        break;
                    }
                }
                if (count == 0)
                {
                    return;
                }
                ClearAddFlow();
                ClearNodeList();
                refreshChart();
            }
        }

        private void addFlow1_Scroll(object sender, ScrollEventArgs e)
        {
            //addFlow1.Location = this.poin
            
        }

        public void AddNewAllocationSolution(object sender,EventArgs e)
        {
            UserUIEventArgs args = new UserUIEventArgs("TreeViewForm");
            SratTreeView mForm = ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(this, args) as SratTreeView;
            mForm.SetSelectedNode(mForm.GetTreeViewCollection(),this.addFlow1.SelectedItem.Tag.ToString());
            //mForm.添加分配任务ToolStripMenuItem_Click(this,e);
           
            mForm.findNode(mForm.GetTreeViewCollection());    
        }

        public void DelNode(object sender, EventArgs e)
        {
            try
            {
                string Tag = this.addFlow1.SelectedItem.Tag.ToString();
                int index=0;
                foreach (TreeNodeInfo mTreeNodeInfo in mProject.mTreeCollection)
                {
                    if (mTreeNodeInfo.parentNodeName == Tag)
                    {
                        MessageBox.Show("当前子系统含有子模块，请先删除！");
                        return;
                    }
                    
                }
                foreach (TreeNodeInfo mTreeNodeInfo in mProject.mTreeCollection)
                {
                    if (mTreeNodeInfo.nodeTag == Tag)
                    {

                        mProject.mTreeCollection.RemoveAt(index);
                        break;
                    }
                    index++;
                }
                
                
                UserUIEventArgs args = new UserUIEventArgs("TreeViewForm");
                SratTreeView mForm = ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(this, args) as SratTreeView;

                mForm.SetSelectedNode(mForm.GetTreeViewCollection(), this.addFlow1.SelectedItem.Tag.ToString());
                mForm.DelNode();
                ClearAddFlow();
                ClearNodeList();
                refreshChart();
                //mForm.DelNode();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void ChangeNodeName(object sender, EventArgs e)
        {
            addFlow1.CanLabelEdit = true;
            Lassalle.Flow.Item item = this.addFlow1.SelectedItem;
            if (item is Node)
            {
                Node node=(Node)item;
                if (!node.IsEditing)
                {
                    node.BeginEdit();
                }
                
            }
            
        }

        private void addFlow1_AfterEdit(object sender, AfterEditEventArgs e)
        {
           
        }
         /*foreach (TreeNodeInfo mTreeNodeInfo in mProject.mTreeCollection)
            {
                if (mTreeNodeInfo.nodeTag == this.addFlow1.SelectedItem.Tag.ToString())
                {
                    mTreeNodeInfo.nodeName = this.addFlow1.SelectedItem.Text;
                    break;
                }
            }

            UserUIEventArgs args = new UserUIEventArgs("TreeViewForm");
            SratTreeView mForm = ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(this, args) as SratTreeView;
            mForm.SetSelectedNode(mForm.GetTreeViewCollection(), this.addFlow1.SelectedItem.Tag.ToString());
            mForm._changeNodeName(addFlow1.SelectedItem.Tag.ToString());

            ClearAddFlow();
            ClearNodeList();
            refreshChart();*/

        public void change(object sender,EventArgs e)
        {
            foreach (TreeNodeInfo mTreeNodeInfo in mProject.mTreeCollection)
            {
                if (mTreeNodeInfo.nodeTag == this.addFlow1.SelectedItem.Tag.ToString())
                {
                    mTreeNodeInfo.nodeName = this.addFlow1.SelectedItem.Text;
                    break;
                }
            }

            UserUIEventArgs args = new UserUIEventArgs("TreeViewForm");
            SratTreeView mForm = ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(this, args) as SratTreeView;
            mForm.SetSelectedNode(mForm.GetTreeViewCollection(), this.addFlow1.SelectedItem.Tag.ToString());
            mForm._changeNodeName(addFlow1.SelectedItem.Text);

            //ClearAddFlow();
            //ClearNodeList();
            //refreshChart();
        }
    }
}
