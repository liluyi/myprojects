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
    public partial class SratTreeView : BaseForm
    {
        public SratTreeView()
        {
            InitializeComponent();

            SratProject project = (SratProject)ProjectManager.ProjectManagerSington.GetProject(ProjectManager.ProjectManagerSington.GetProjectUUID());
            if (project.mTreeCollection.Count == 0)
            {
                InitTree();
            }
            else
            {
                LoadTree();
            }


        }

        public List<Node> mNodeList = new List<Node>();
        public int num = 0;
        public int numSubSys = 0;

        public int GetNumSubSys()
        {
            return numSubSys;
        }

        public void upNumSubSys()
        {
            numSubSys++;
        }
        /// <summary>
        /// 从文件导入工程时TreeView初始化
        /// </summary>
        /// <param></param>
        /// <date>2011.11.16</date>>
        /// <returns></returns>
        /*public void LoadTreeView()
        {
            SratProject mProject = (SratProject)ProjectManager.ProjectManagerSington.GetProject(ProjectManager.ProjectManagerSington.GetProjectUUID());

            foreach (TreeNodeInfo tni in mProject.mTreeNodeList)
            {
                if (tni.nodeTag == "project")
                {
                    TreeNode tn = new TreeNode(tni.nodeName);
                    this.treeView1.Nodes.Add(tn);
                    this.treeView1.SelectedNode = tn;
                    tn.Tag = tni.nodeTag;
                }
            }

            foreach (TreeNodeInfo tni in mProject.mTreeNodeList)
            {
                if (tni.parentNodeName == "project")
                {
                    TreeNode tn = new TreeNode(tni.nodeName);
                    this.treeView1.SelectedNode.Nodes.Add(tn);
                    tn.Tag = tni.nodeTag;

                }
            }
            foreach (TreeNodeInfo tni in mProject.mTreeNodeList)
            {
                try
                {
                    if (tni.parentNodeName == "systemarch")
                    {
                        findNode(treeView1.Nodes, "systemarch");
                        TreeNode tn = new TreeNode(tni.nodeName);
                        this.treeView1.SelectedNode.Nodes.Add(tn);
                        tn.Tag = tni.nodeTag;
                    }
                }
                catch (Exception r)
                {
                    MessageBox.Show(r.ToString());
                }
            }
            
            foreach (TreeNodeInfo tni in mProject.mTreeNodeList)
            {
                try
                {
                    if (tni.parentNodeName == "system")
                    {
                        findNode(treeView1.Nodes, "system");
                        TreeNode tn = new TreeNode(tni.nodeName);
                        this.treeView1.SelectedNode.Nodes.Add(tn);
                        tn.Tag = tni.nodeTag;
                    }
                }
                catch (Exception r)
                {
                    MessageBox.Show(r.ToString());
                }
            }
            foreach (TreeNodeInfo tni in mProject.mTreeNodeList)
            {
                try
                {
                    if (tni.nodeTag.Substring(0, 6) == "module")
                    {
                        findNode(treeView1.Nodes, tni.parentNodeName);
                        TreeNode tn = new TreeNode(tni.nodeName);
                        this.treeView1.SelectedNode.Nodes.Add(tn);
                        tn.Tag = tni.nodeTag;
                    }
                }
                catch (Exception r)
                {
                    MessageBox.Show(r.ToString());
                }
            }
            treeView1.ExpandAll();
        }*/
        public void LoadTree()
        {
            SratProject mProject = (SratProject)ProjectManager.ProjectManagerSington.GetProject(ProjectManager.ProjectManagerSington.GetProjectUUID());

            foreach (TreeNodeInfo tni in mProject.mTreeCollection)
            {
                if (tni.nodeTag == "project")
                {
                    TreeNode tn = new TreeNode(tni.nodeName);
                    this.treeView1.Nodes.Add(tn);
                    this.treeView1.SelectedNode = tn;
                    tn.Tag = tni.nodeTag;
                }
            }

            foreach (TreeNodeInfo tni in mProject.mTreeCollection)
            {
                if (tni.parentNodeName == "project")
                {
                    TreeNode tn = new TreeNode(tni.nodeName);
                    this.treeView1.SelectedNode.Nodes.Add(tn);
                    tn.Tag = tni.nodeTag;

                }
            }
            foreach (TreeNodeInfo tni in mProject.mTreeCollection)
            {
                try
                {
                    if (tni.parentNodeName == "systemarch")
                    {
                        findNode(treeView1.Nodes, "systemarch");
                        TreeNode tn = new TreeNode(tni.nodeName);
                        this.treeView1.SelectedNode.Nodes.Add(tn);
                        tn.Tag = tni.nodeTag;
                    }
                }
                catch (Exception r)
                {
                    MessageBox.Show(r.ToString());
                }
            }

            foreach (TreeNodeInfo tni in mProject.mTreeCollection)
            {
                try
                {
                    if (tni.parentNodeName == "system")
                    {
                        findNode(treeView1.Nodes, "system");
                        TreeNode tn = new TreeNode(tni.nodeName);
                        this.treeView1.SelectedNode.Nodes.Add(tn);
                        tn.Tag = tni.nodeTag;
                    }
                }
                catch (Exception r)
                {
                    MessageBox.Show(r.ToString());
                }
            }
            foreach (TreeNodeInfo tni in mProject.mTreeCollection)
            {
                try
                {
                    if (tni.nodeTag.Substring(0, 6) == "module")
                    {
                        findNode(treeView1.Nodes, tni.parentNodeName);
                        TreeNode tn = new TreeNode(tni.nodeName);
                        this.treeView1.SelectedNode.Nodes.Add(tn);
                        tn.Tag = tni.nodeTag;
                    }
                }
                catch (Exception r)
                {
                    MessageBox.Show(r.ToString());
                }
            }
            treeView1.ExpandAll();
        }

        /// <summary>
        /// TreeView初始化
        /// </summary>
        /// <param></param>
        /// <date>2011.11.13</date>>
        /// <returns></returns>
        /*public void InitTreeView()
        {
            try
            {
                SratProject project = (SratProject)ProjectManager.ProjectManagerSington.GetProject(ProjectManager.ProjectManagerSington.GetProjectUUID());
                TreeNode tn = new TreeNode("当前工程 " + "'" + project.Name + "'");
                this.treeView1.Nodes.Add(tn);
                tn.Tag = "当前工程 " + "'" + project.Name + "'";
                TreeNodeInfo tni = new TreeNodeInfo("当前工程 " + "'" + project.Name + "'", "工程", "");
                addNewNode("当前工程 " + "'" + project.Name + "'", "project", "");

                TreeNode tnSys = new TreeNode("系统结构");
                tn.Nodes.Add(tnSys);
                tnSys.Tag = "systemarch";
                addNewNode("系统结构", "systemarch", "project");

                TreeNode tnSysRoot = new TreeNode(project.Name + "系统");
                tnSys.Nodes.Add(tnSysRoot);
                tnSysRoot.Tag = "system";
                addNewNode(project.Name + "系统", "system", "systemarch");

                TreeNode tnTask = new TreeNode("分配任务列表");
                tn.Nodes.Add(tnTask);
                tnTask.Tag = "tasklist";
                addNewNode("分配任务列表", "tasklist", "project");

                TreeNode tnOther = new TreeNode("项目其他结果");
                tn.Nodes.Add(tnOther);
                tnOther.Tag = "othersprojectinformation";
                addNewNode("项目其他结果", "othersprojectinformation", "project");

                treeView1.ExpandAll();
            }
            catch(Exception r)
            {
                MessageBox.Show(r.ToString());
            }
        }*/

        public void InitTree()
        {
            try
            {
                SratProject project = (SratProject)ProjectManager.ProjectManagerSington.GetProject(ProjectManager.ProjectManagerSington.GetProjectUUID());
                TreeNode tn = new TreeNode("当前工程 " + "'" + project.Name + "'");
                this.treeView1.Nodes.Add(tn);
                tn.Tag = "project";
                TreeNodeInfo tni = new TreeNodeInfo("当前工程 " + "'" + project.Name + "'", "工程", "");
                addNewNodeInfo("当前工程 " + "'" + project.Name + "'", "project", "");

                TreeNode tnSys = new TreeNode("系统结构");
                tn.Nodes.Add(tnSys);
                tnSys.Tag = "systemarch";
                addNewNodeInfo("系统结构", "systemarch", "project");

                TreeNode tnSysRoot = new TreeNode(project.Name + "系统");
                tnSys.Nodes.Add(tnSysRoot);
                tnSysRoot.Tag = "system";
                addNewNodeInfo(project.Name + "系统", "system", "systemarch");

                TreeNode tnTask = new TreeNode("分配任务列表");
                tn.Nodes.Add(tnTask);
                tnTask.Tag = "tasklist";
                addNewNodeInfo("分配任务列表", "tasklist", "project");

                TreeNode tnOther = new TreeNode("项目其他结果");
                tn.Nodes.Add(tnOther);
                tnOther.Tag = "othersprojectinformation";
                addNewNodeInfo("项目其他结果", "othersprojectinformation", "project");

                treeView1.ExpandAll();
            }
            catch (Exception r)
            {
                MessageBox.Show(r.ToString());
            }
        }

        /// <summary>
        /// 查找节点
        /// </summary>
        /// <param></param>
        /// <date>2011.11.16</date>>
        /// <returns></returns>
        public void findNode(TreeNodeCollection treenode, string name)
        {
            foreach (TreeNode tn in treenode)
            {
                if (tn.Tag.ToString() == name)
                {
                    this.treeView1.SelectedNode = tn;
                }
                else if (tn.Nodes.Count > 0)
                {
                    findNode(tn.Nodes, name);
                }
            }
        }

        public TreeNodeCollection GetTreeViewCollection()
        {
            return this.treeView1.Nodes;
        }
        /// <summary>
        /// 递归法寻找添加的分配任务节点位置，添加新任务窗体，并进行窗体的唯一性确定
        /// </summary>
        /// <param></param>
        /// <date>2011.11.13</date>>
        /// <returns></returns>
        public void findNode(TreeNodeCollection treenode)
        {
            string nodeName = treeView1.SelectedNode.Text + "分配方法";
            TreeNode Tn = new TreeNode();
            Tn = treeView1.SelectedNode;
            foreach (TreeNode tn in treenode)
            {
                if (tn.Tag.ToString()=="tasklist")
                {
                    addNewTabViewForm(nodeName,tn,Tn);
                }
                if (tn.Nodes.Count > 0)
                {
                    findNode(tn.Nodes);
                }
            }
        }

        public void findNodeandAddChart(TreeNodeCollection treenode, string index)
        {
            foreach (TreeNode tn in treenode)
            {
                if (tn.Tag.ToString() == "index")
                {
                   
                    
                }
                if (tn.Nodes.Count > 0)
                {
                    findNodeandAddChart(tn.Nodes,index);
                }
            }
        }

        /// <summary>
        /// TreeView右键菜单创建
        /// </summary>
        /// <param></param>
        /// <date>2011.11.13</date>>
        /// <returns></returns>
        public void rightButtonContextStrip(object sender, MouseEventArgs e)
        {
            SratProject project = (SratProject)ProjectManager.ProjectManagerSington.GetProject(ProjectManager.ProjectManagerSington.GetProjectUUID());
            if (e.Button == MouseButtons.Right)
            {
                TreeNode tn = treeView1.GetNodeAt(e.X, e.Y);
                treeView1.SelectedNode = tn;
                if (tn != null && tn.Text == (project.Name + "系统"))
                {
                    Point p = PointToScreen(new Point(e.X, e.Y));
                    contextMenuStrip1.Show(p);
                    contextMenuStrip1.Items[1].Enabled = false;
                }
                else if (tn != null && (string)tn.Tag == "systemarch")
                {
                    Point p = PointToScreen(new Point(e.X, e.Y));
                    contextMenuStrip4.Show(p);
                }
                else if (tn != null && tn.Text == "分配任务列表")
                {
                    Point p = PointToScreen(new Point(e.X, e.Y));
                    contextMenuStrip3.Show(p);
                }
                else if (tn != null && ((string)tn.Tag).Substring(0, 6) == "module")
                {
                    Point p = PointToScreen(new Point(e.X, e.Y));
                    contextMenuStrip5.Show(p);
                    contextMenuStrip5.Items[3].Enabled = false;
                    contextMenuStrip5.Items[0].Enabled = false;
                }
                else if (tn != null && ((string)tn.Tag).Substring(0, 9) == "subsystem")
                {
                    Point p = PointToScreen(new Point(e.X, e.Y));
                    contextMenuStrip2.Show(p);
                }
                else
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 添加新窗体，使用到ServicesManager.ServicesManagerSingleton.UIService.AddMutableResourceSelf(form,args)
        /// </summary>
        /// <param></param>
        /// <date>2011.11.12</date>>
        /// <returns></returns>
        private void addNewForm(string fullclassname, string uuid)
        {
            TreeNode tn = new TreeNode(fullclassname);
            treeView1.SelectedNode.Nodes.Add(tn);
            tn.Tag = uuid;
            
            SratTabView form = new SratTabView();
            form.Tag = uuid;
            form.Text = fullclassname;

            UserUIEventArgs args = new UserUIEventArgs(uuid);
            ServicesManager.ServicesManagerSingleton.UIService.AddMutableResourceSelf(form,args);
            form.ShowHint = DockState.Document;
            form.Show(DockPanel);
            //UserUIEventArgs arg = new UserUIEventArgs("InfoViewForm");
            //SratInfoView mForm = ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(this, arg) as SratInfoView;
            
            //form.Show(mForm.Pane,null);
            
        }

        private BaseForm addNewForm(string fullclassname,string uuid,int n)
        {
            TreeNode tn = new TreeNode(fullclassname);
            treeView1.SelectedNode.Nodes.Add(tn);
            tn.Tag = uuid;

            SratInfoView form = new SratInfoView();
            form.Tag = uuid;
            form.Text = fullclassname;
            UserUIEventArgs args = new UserUIEventArgs(uuid);
            //UIEventArgs args = new UIEventArgs(fullclassname, uuid);
            ServicesManager.ServicesManagerSingleton.UIService.AddMutableResourceSelf(form, args);

            UserUIEventArgs arg = new UserUIEventArgs("InfoViewForm");
            SratInfoView mForm = ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(this, arg) as SratInfoView;

            form.Show(mForm.Pane,null);
            return form;
        }

        private void addNewTabViewForm(string nodeName,TreeNode tn,TreeNode TN)
        {
            treeView1.SelectedNode = tn;
            BaseForm form = new SratTabView();
            form=addNewForm(nodeName, "allocation"+num.ToString(), 1) as BaseForm;
            num++;
            treeView1.ExpandAll();



            DataGridView dgv = new DataGridView();
            form.Controls.Add(dgv);
            dgv.Dock = DockStyle.Fill;
            dgvInitialize(dgv,TN);
        }

        public void dgvInitialize(DataGridView dgv, TreeNode tn)
        {
            dgv.Columns.Add("name", "名称");
            dgv.Columns.Add("factor1", "参数1");
            dgv.Columns.Add("factor2", "参数2");
            dgv.Columns.Add("factor3", "参数3");
            dgv.Columns.Add("factor4", "参数4");
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            int line = 0;
            SratProject mProject = (SratProject)ProjectManager.ProjectManagerSington.GetProject(ProjectManager.ProjectManagerSington.GetProjectUUID());
            foreach (TreeNodeInfo tni in mProject.mTreeCollection)
            {
                if (tn.Tag.ToString() == tni.parentNodeName)
                {
                    dgv.Rows.Add();

                    dgv.Rows[line].Cells[0].Value = tni.nodeName;
                    line++;
                }
            }
        }

        private void addFlowForm(string fullclassname, string uuid)
        {
            
        }

        /// <summary>
        /// 获得窗体，使用ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(this, args) as BaseForm
        /// </summary>
        /// <param></param>
        /// <date>2011.11.12</date>>
        /// <returns></returns>
        private void getOneForm(string uuid)
        {
            UserUIEventArgs args = new UserUIEventArgs(uuid);
            //UIEventArgs args = new UIEventArgs("",uuid);
            BaseForm form = ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(this, args) as BaseForm;
            if (form != null)
            {
                form.Activate();
            }
            else if (uuid.Substring(0, 6) == "system" || uuid.Substring(0, 6) == "subsys" || uuid.Substring(0, 6) == "module")
            {
                UserUIEventArgs argss = new UserUIEventArgs("systemarchmap");
                //UIEventArgs argss=new UIEventArgs("","systemarchmap");
                form = ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(this, argss) as BaseForm;
                if (form != null)
                {
                    form.Activate();
                }
            }
        }

        public BaseForm getOneForm(string fullclassname,string uuid)
        {
            UserUIEventArgs args = new UserUIEventArgs(uuid);
            BaseForm form = ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(this, args) as BaseForm;
            return form;
        }
        /// <summary>
        /// 添加节点及Tag
        /// </summary>
        /// <param></param>
        /// <date>2011.11.12</date>>
        /// <returns></returns>
        public void addNewNode(string tnTag)
        {
            TreeNode tn = new TreeNode("NewNode");
            treeView1.SelectedNode.Nodes.Add(tn);
            tn.Tag = tnTag;
            treeView1.ExpandAll();
        }

        /*public void addNewNode(string nodeName, string nodeTag, string parentNodeName)
        {
            TreeNodeInfo tni = new TreeNodeInfo(nodeName,nodeTag,parentNodeName);
            SratProject mProject = (SratProject)ProjectManager.ProjectManagerSington.GetProject(ProjectManager.ProjectManagerSington.GetProjectUUID());
            mProject.mTreeNodeList.Add(tni);
        }*/

        public void addNewNodeInfo(string nodeName, string nodeTag, string parentNodeName)
        {
            TreeNodeInfo tni = new TreeNodeInfo(nodeName, nodeTag, parentNodeName);
            SratProject mProject = (SratProject)ProjectManager.ProjectManagerSington.GetProject(ProjectManager.ProjectManagerSington.GetProjectUUID());
            mProject.mTreeCollection.Add(tni);
        }

        public void changeNodeName()
        {
            treeView1.LabelEdit = true;
            if (!treeView1.SelectedNode.IsEditing)
            {
                treeView1.SelectedNode.BeginEdit();
            }
        }

        public void addSystemArchMap()
        {

        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                rightButtonContextStrip(this,e);
            }
            catch (Exception r)
            {
                MessageBox.Show(r.ToString());
            }
        }
        
        private void 添加分配任务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findNode(this.treeView1.Nodes);
            //project.tnc = treeView1.Nodes;
        }
        private void 添加子系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addNewNode("subsystem"+numSubSys.ToString());
            addNewNodeInfo("NewNode", "subsystem" + numSubSys.ToString(), "system");
            numSubSys++;
            BaseForm form = getOneForm("", "systemarchmap");
            if (form != null)
            {
                UserUIEventArgs args = new UserUIEventArgs("systemarchmap");
                SratTabView mForm = ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(this, args) as SratTabView;
                mForm.ClearNodeList();
                mForm.ClearAddFlow();
                mForm.refreshChart();
            }
        }

        private void 添加模块ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tag=this.treeView1.SelectedNode.Tag.ToString();
            DateTime dt = DateTime.Now;
            addNewNode("module"+Convert.ToString(dt));
            addNewNodeInfo("NewNode", "module" + Convert.ToString(dt), tag);
            BaseForm form = getOneForm("", "systemarchmap");
            if (form != null)
            {
                UserUIEventArgs args = new UserUIEventArgs("systemarchmap");
                SratTabView mForm = ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(this, args) as SratTabView;
                mForm.ClearNodeList();
                mForm.ClearAddFlow();
                mForm.refreshChart();
            }
        }

        private void 添加系统结构图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addNewForm("系统结构图","systemarchmap");
            contextMenuStrip4.Items[0].Enabled = false;
            contextMenuStrip4.Items[1].Enabled = true;

            UserUIEventArgs args = new UserUIEventArgs("systemarchmap");
            SratTabView mForm = ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(this, args) as SratTabView;
            mForm.refreshChart();
        }
        
        

        private void 节点重命名ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            changeNodeName();
        }

        private void 节点重命名ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            changeNodeName();
        }

        private void 添加分配任务ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeNode tn = treeView1.SelectedNode;
            findNode(this.treeView1.Nodes);
        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                TreeNode tn = treeView1.GetNodeAt(e.X,e.Y);
                getOneForm(tn.Tag.ToString());
            }
            catch (Exception r)
            {
                MessageBox.Show(r.ToString());
            }
        }

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            SratProject project = (SratProject)ProjectManager.ProjectManagerSington.GetProject(ProjectManager.ProjectManagerSington.GetProjectUUID());
            foreach (TreeNodeInfo tni in project.mTreeCollection)
            {
                if ((string)treeView1.SelectedNode.Tag == tni.nodeTag)
                {
                    tni.nodeName = e.Label;
                }
            }
            
            UserUIEventArgs args = new UserUIEventArgs("systemarchmap");
            SratTabView mForm = ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(this, args) as SratTabView;
            mForm.ClearNodeList();
            mForm.ClearAddFlow();
            mForm.refreshChart();
        }

        private void 更新系统结构图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            UserUIEventArgs args = new UserUIEventArgs("systemarchmap");
            SratTabView mForm = ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(this, args) as SratTabView;
            mForm.ClearNodeList();
            mForm.ClearAddFlow();
            mForm.refreshChart();
            
        }

        public void SetSelectedNode(TreeNodeCollection mNodeCollection,string Tag)
        {
            foreach (TreeNode tn in mNodeCollection)
            {
                if (tn.Tag.ToString() == Tag)
                {
                    treeView1.SelectedNode = tn;
                    return;
                }
                if(tn.Nodes.Count>0)
                {
                    SetSelectedNode(tn.Nodes,Tag);
                }
            }
        }

        public void DelNode()
        {
            treeView1.SelectedNode.Remove();
        }

        public void _changeNodeName(string name)
        {
            this.treeView1.SelectedNode.Text = name;
            treeView1.Refresh();
            treeView1.ExpandAll();
        }

        private void SratTreeView_Load(object sender, EventArgs e)
        {
            

            //form.Show(mForm.Pane, DockAlignment.Right,0.2);
            //form.ShowHint=DockState.DockRight;
            //form.Show(mForm.Pane);
        }
    }
}
