using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Platform.Core.UI;
using Platform.Core.Services;
using Platform.Core;
using Platform.Core.Exceptions;
using Platform.Core.Data;
using WeifenLuo.WinFormsUI.Docking;

namespace SFTAPlugin
{                                      
    public delegate void SelectedNodeChangeHandler(string tag);
    public partial class SFTATreeViewForm : BaseForm
    {
        public TreeNode currentnode;// = new TreeNode();//当前选中的树节点
        private FTAForm ftaform;//节点对应的故障树分析窗体
        private Dictionary<string, FTATreeInfo> SFTATreesDic; //故障树分析窗体词典，id为键，树信息为值

        public SelectedNodeChangeHandler SelectedNodeChange;  //选中节点变化的监听委托

        public SFTATreeViewForm()
        {
            InitializeComponent();
            //this.DockPanel.DockLeftPortion = 0.2;//将停靠比例设置为1/5
            this.imageList1.Images.Add(global::SFTAPlugin.Properties.Resources.image_tree);
            this.imageList1.Images.Add(global::SFTAPlugin.Properties.Resources.image_software);
            //LoadFromProject();         //移至窗体load 时进行
        }
        /// <summary>
        /// 从工程文件中获取树状信息列表
        /// </summary>
        public void LoadFromProject()
        {
            if(this.treeView1.TopNode==null)
                this.treeView1.TopNode = new TreeNode("软件A");
            this.treeView1.TopNode.Nodes.Clear();
            SFTATreesDic = ((SFTAProject)ProjectManager.ProjectManagerSington.GetCurrentProject()).SFTATreesDic;//获取工程下的故障树分析词典
            if (SFTATreesDic.Count != 0)
            {//检查当前工程是否已存在故障树，若存在，则将其根节点加载进来
                foreach (KeyValuePair<string, FTATreeInfo> pair in SFTATreesDic)
                {//搜索工程下的全部树
                    TreeNode tn = new TreeNode();//创建树状列表节点
                    FTATreeNodeInfo tni=pair.Value.SearchRootNode();//搜索故障树的根节点
                    tn.Name = tni.nodeID;//将故障树节点ID赋予树状节点的名称
                    tn.Text = tni.nodedata.nodeName;//将故障树节点名称赋予树状节点的显示文本
                    tn.Tag = "TopEvent";//树节点类型为顶事件
                    treeView1.TopNode.Nodes.Add(tn);//将树节点添加至treeview中
                    CreateTreeFromProject(tn, tni,pair.Value);
                }
            }
            treeView1.ExpandAll();
        }

        private void CreateTreeFromProject(TreeNode parentnode,FTATreeNodeInfo tni,FTATreeInfo treeinfo)
        {
            foreach (string childid in tni.noderelation.childrenNodesID)
            {
                FTATreeNodeInfo childftanodeinfo = treeinfo.SearchTreeNodeInfo(childid);
                if ((childftanodeinfo.nodedata is FTAEventNodeData) && (((FTAEventNodeData)childftanodeinfo.nodedata).eventType == EventType.EventIntermediate && childftanodeinfo.noderelation.siblingnodeID != null))
                {
                    TreeNode childtn = new TreeNode();//创建树状列表节点
                    childtn.Name = childftanodeinfo.nodeID;//将故障树节点ID赋予树状节点的名称
                    childtn.Text = treeinfo.SearchTreeNodeInfo(childftanodeinfo.noderelation.siblingnodeID).nodedata.nodeName;//将故障树节点名称赋予树状节点的显示文本
                    childtn.Tag = "ChildTree";//树节点类型为顶事件
                    parentnode.Nodes.Add(childtn);
                    CreateTreeFromProject(childtn, childftanodeinfo, treeinfo);
                }
                else
                    CreateTreeFromProject(parentnode, childftanodeinfo, treeinfo);
            }         
        }
        /// <summary>
        /// 递归，查找树窗体的所有节点
        /// </summary>
        /// <param name="functiontreenode">待查找的高一级节点</param>
        /// <param name="tni">当前的节点信息</param>
        public void searchTreeNode(TreeNode functiontreenode, FTATreeNodeInfo tni,string formid,string text)
        {
            foreach (TreeNode tn in functiontreenode.Nodes)//搜索全部子节点
            {
                if (tn.Name == formid)//判定子节点名称是否与当前窗体（故障树分析窗体）名称相同（GUID）
                {
                    TreeNode childtree = new TreeNode(text);//名字改为上一级节点.1.2.3等//tni.nodedata.nodeName);//将当前节点名称作为新节点的text
                    childtree.Name = tni.nodedata.nodeDataID;//当前节点ID作为新节点的name
                    childtree.Tag = "ChildTree";
                    tn.Nodes.Add(childtree);
                    tn.Expand();
                    treeView1.ExpandAll();
                    break;
                }
                searchTreeNode(tn, tni,formid,text);
            }
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//右键单击
            {
                TreeNode tn = treeView1.GetNodeAt(e.X, e.Y);
                if (tn != null)
                {
                    treeView1.SelectedNode = tn;
                    currentnode = tn;
                    if (tn.Name == "SoftwareName")
                        contextMenuStrip1.Show(PointToScreen(new Point(e.X,e.Y)));
                    else if (tn.Tag.ToString() == "TopEvent")//有问题，未初始化
                        contextMenuStrip2.Show(PointToScreen(new Point(e.X, e.Y)));
                }
            }
            else if (e.Button == MouseButtons.Left)//左键单击
            {
                currentnode = treeView1.SelectedNode;
            }
        }

        /// <summary>
        /// 添加新顶事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddTopEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentnode = this.treeView1.SelectedNode;
            if (currentnode!=null&&currentnode.Tag.ToString()=="SoftwareName")
            {
                string lastdefaultname="新顶事件0";
                foreach (TreeNode tempnode in currentnode.Nodes)//判断最后一个默认命名的子节点的名称
                {
                    if (tempnode.Text.Contains("新顶事件"))
                        lastdefaultname = tempnode.Text;
                }
                int index;
                unchecked
                {
                    index=int.Parse(lastdefaultname.Substring(4)) + 1;//有风险，可能无法转换
                }
                TreeNode childnode = new TreeNode("新顶事件" +index.ToString());
                childnode.Name = Guid.NewGuid().ToString();//节点ID
                childnode.Tag = "TopEvent";
                currentnode.Nodes.Add(childnode);
                currentnode = childnode;
                this.treeView1.SelectedNode = childnode;

                //添加了两个子节点
                //TreeNode grandchildnode1 = new TreeNode("改进措施最小割集");
                //TreeNode grandchildnode2 = new TreeNode("故障树验证结果");
                //grandchildnode1.Name = grandchildnode1.Index.ToString();
                //grandchildnode1.Tag = "改进措施最小隔集";
                //childnode.Nodes.Add(grandchildnode1);
                //grandchildnode2.Name = grandchildnode2.Index.ToString();
                //grandchildnode2.Tag = "故障树验证结果";
                //childnode.Nodes.Add(grandchildnode2);
            }
            treeView1.ExpandAll();
        }

        /// <summary>
        /// 双击创建或打开信窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            ///已进行单例处理!!!!!!!
            ToolForm toolform= (ToolForm)ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(null, new UserUIEventArgs("ToolForm"));
            if (toolform != null)//看窗体是否已经加载至平台
            {//若已经加载，显示
                toolform.Show();
            }
            else
            {//若未加载，创建窗体、注册至平台并显示
                toolform = new ToolForm();
                toolform.Name = "ToolForm";
                ServicesManager.ServicesManagerSingleton.UIService.AddMutableResourceSelf(toolform, new UserUIEventArgs(toolform.Name, new FormLoc(DockState.DockLeft)));
            }

            if (treeView1.SelectedNode.Tag.ToString() == "TopEvent")//新建顶事件
            {
                string name = string.Empty;
                currentnode = treeView1.SelectedNode;
                foreach (string formname in ServicesManager.ServicesManagerSingleton.UIService.GetAllFormNames(null, new UserUIEventArgs(string.Empty, null)))
                    if (formname.Equals(treeView1.SelectedNode.Name))//窗体已经创建
                    {                       
                        ftaform = (FTAForm)ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(null, new UserUIEventArgs(formname));
                        ftaform.Name = formname;
                        name = formname;
                        ftaform.Show(DockPanel);
                        break;
                    }
                if (name == string.Empty)
                {//窗体尚未创建
                    //string topid = Guid.NewGuid().ToString();
                    FTAEventNodeData topdata = new FTAEventNodeData(currentnode.Name, treeView1.SelectedNode.Text, EventType.EventIntermediate, string.Empty, string.Empty);//树根事件
                    FTATreeNodeInfo topnode = new FTATreeNodeInfo(currentnode.Name, 0, topdata, new FTANodeRelation(currentnode.Name, null, new List<string>()));//树根节点，父节点为null           
                    FTATreeInfo treeinfo=new FTATreeInfo();//大树
                    treeinfo.Add(topnode);//大树加入根节点
                    ((SFTAProject)ProjectManager.ProjectManagerSington.GetCurrentProject()).SFTATreesDic.Add(treeView1.SelectedNode.Name, treeinfo);//树加入工程词典
                    ftaform = new FTAForm(treeView1.SelectedNode.Name, topnode);//以根节点ID创建新面板
                    ftaform.Name = treeView1.SelectedNode.Name;//节点ID与对应的窗体ID相同
                    ftaform.Text = currentnode.Text;
                    ServicesManager.ServicesManagerSingleton.UIService.AddMutableResourceSelf(ftaform, new UserUIEventArgs(ftaform.Name, new FormLoc(DockState.Document)));
                }
            }
            else if (treeView1.SelectedNode.Tag.ToString() == "ChildTree")
            {
                string name = string.Empty;
                currentnode = treeView1.SelectedNode;
                //treeView1.SelectedNode.Name = treeView1.SelectedNode.Index.ToString();//传递node序号，有风险
                foreach (string formname in ServicesManager.ServicesManagerSingleton.UIService.GetAllFormNames(null, new UserUIEventArgs(string.Empty, null)))
                    if (formname.Equals(treeView1.SelectedNode.Name))
                    {
                        ftaform = (FTAForm)ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(null, new UserUIEventArgs(formname));
                        ftaform.Name = formname;
                        name = formname;
                        ftaform.Show(DockPanel);
                        break;
                    }
                if (name == string.Empty)
                {
                    FTATreeNodeInfo topnode;
                    FTATreeInfo ftainfo;
                    ((SFTAProject)ProjectManager.ProjectManagerSington.GetCurrentProject()).SFTATreesDic.TryGetValue(currentnode.Parent.Name, out ftainfo);
                    topnode = ftainfo.SearchTreeNodeInfo(currentnode.Name);
                    ftaform = new FTAForm(currentnode.Parent.Name,topnode);
                    ftaform.Name = treeView1.SelectedNode.Name;//节点ID与对应的窗体ID相同
                    ServicesManager.ServicesManagerSingleton.UIService.AddMutableResourceSelf(ftaform, new UserUIEventArgs(ftaform.Name, new FormLoc(DockState.Document)));
                }
            }
        }

        /// <summary>
        /// 重命名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.SelectedNode.BeginEdit();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //treeView1.SelectedNode.BeginEdit();
            treeView1.SelectedNode = e.Node;
            currentnode = e.Node;
            //点击任意节点，告知菜单选中的节点发生了变化，并将选中的节点类型参数传回主界面
            if (SelectedNodeChange != null)
                SelectedNodeChange(e.Node.Tag.ToString());
        }

        /// <summary>
        /// 更改节点名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)//此时编辑后尚未将新文本赋予对应的text
        {
            if (e.Label != null && !e.Label.Equals(string.Empty))
            {
                treeView1.SelectedNode.Text = e.Label;//获取更改后的文本
                //更改对应的FTAForm的名称
                FTAForm tempform = (FTAForm)ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(null, new UserUIEventArgs(treeView1.SelectedNode.Name));
                if (tempform != null)
                {
                    tempform.Text = treeView1.SelectedNode.Text;
                    tempform.topnode.nodedata.nodename = treeView1.SelectedNode.Text; 
                    tempform.Refresh();
                    tempform.RefreshContent();
                }
            }
            else
                e.CancelEdit = true;//若label更改后未输入文字，则取消编辑
        }

        private void SFTATreeViewForm_Load(object sender, EventArgs e)
        {
            
            //此段支持双层树结构
            //SFTATreesDic = ((SFTAProject)ProjectManager.ProjectManagerSington.GetCurrentProject()).SFTATreesDic;
            //if (SFTATreesDic.Count != 0)
            //{//检查当前工程是否已存在故障树，若存在，则将其根节点加载进来
            //    foreach (KeyValuePair<string, FTATreeInfo> pair in SFTATreesDic)
            //    {
            //        TreeNode tn = new TreeNode();
            //        tn.Name = pair.Value.SearchRootNode().nodeID;
            //        tn.Text = pair.Value.SearchRootNode().nodedata.nodeName;
            //        tn.Tag = "TopEvent";
            //        treeView1.TopNode.Nodes.Add(tn);
            //        List<FTATreeNodeInfo> childtrees = pair.Value.SearchChildTrees();//检查是否存在子树，若存在，加载进来
            //        if (childtrees.Count != 0)
            //        {
            //            foreach(FTATreeNodeInfo tni in childtrees)
            //            {
            //                TreeNode childtn=new TreeNode();
            //                childtn.Name=tni.nodeID;
            //                childtn.Text=tni.nodedata.nodeName;
            //                childtn.Tag="ChildTree";
            //                tn.Nodes.Add(childtn);
            //            }
            //        }
            //    }
            //}
            LoadFromProject();
            treeView1.ExpandAll();
            currentnode = this.treeView1.TopNode;
        }

        private void SFTATreeViewForm_Enter(object sender, EventArgs e)
        {
        }

        private void SFTATreeViewForm_Activated(object sender, EventArgs e)
        {
        }
    }
}
