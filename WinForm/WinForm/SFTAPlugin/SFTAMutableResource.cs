using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using Platform.Core;
using Platform.Core.Data;
using Platform.Core.UI;
using Platform.Core.Services;
using WeifenLuo.WinFormsUI.Docking;
using System.Data.OleDb;
using System.Data;
using System.Drawing;


namespace SFTAPlugin
{
    public delegate void CreateTreeHandler();

    public class SFTAMutableResource:MutableResource
    {
        private BaseForm viewform = new SFTATreeViewForm();
        private BaseForm tabform = new SFTATabViewForm();
        private BaseForm infoform = new SFTAInfoViewForm();
        private BaseForm serverform = new SFTAServerViewForm();
        private ToolStripMenuItem[] menus;
        private ToolStrip[] tools;
        private Dictionary<string, Platform.Core.UI.BaseForm> toolformdictionary = new Dictionary<string, BaseForm>();
        private Dictionary<string, FormLoc> formlocationdictionary = new Dictionary<string, FormLoc>();
        public CreateTreeHandler CreateTree;

        public SFTAMutableResource()
        {
            //菜单
            menus = new ToolStripMenuItem[9];

            menus[0] = new ToolStripMenuItem("已有事件");
            menus[0].DropDownItems.Add("显示已有事件");
            menus[0].DropDownItems[0].ToolTipText = "显示本工程中已经创建的事件以及数据库中存在的事件";
            menus[0].DropDownItems[0].Click += new EventHandler(ExistedEvents);

            menus[1] = new ToolStripMenuItem("输出");

            menus[2] = new ToolStripMenuItem("添加");

            menus[3] = new ToolStripMenuItem("配置");

            menus[4] = new ToolStripMenuItem("辅助分析");
            menus[4].DropDownItems.Add("IPO分解");
            menus[4].DropDownItems[0].ToolTipText = "利用IPO方法辅助进行故障树分析";
            menus[4].DropDownItems[0].Click += new EventHandler(IPOAnalysis);
            menus[4].DropDownItems.Add("选择嵌入式软件分析模板创建");
            menus[4].DropDownItems[1].ToolTipText = "选择嵌入式软件分析模板创建故障树";
            menus[4].DropDownItems[1].Click += new EventHandler(SelectTemplate_Click);

            menus[5] = new ToolStripMenuItem("库管理");
            menus[5].DropDownItems.Add("导入本地FTA库信息");
            //menus[5].DropDownItems[0].Click += new EventHandler(ImportFTADatabase);

            menus[6] = new ToolStripMenuItem("帮助");
            menus[6].DropDownItems.Add("功能说明");
            menus[6].DropDownItems.Add("版权");

            menus[7] = new ToolStripMenuItem("报表生成");
            menus[7].DropDownItems.Add("输出");
            menus[7].DropDownItems[0].ToolTipText = "输出故障树分析报表";
            menus[7].DropDownItems[0].Click += new EventHandler(ToWord);

            menus[8] = new ToolStripMenuItem("其它");

            //工具栏
            tools = new ToolStrip[2];
            
            tools[0] = new ToolStrip();
            tools[0].ShowItemToolTips = true;
            tools[0].Items.Add(Properties.Resources.image_newnode);
            tools[0].Items[0].ToolTipText = "新建故障树";
            tools[0].Items[0].Click += new EventHandler(CreateFailureTree);

            tools[1] = new ToolStrip();
            tools[1].ShowItemToolTips = true;
            tools[1].Items.Add(Properties.Resources.image_savescreen);
            tools[1].Items[0].ToolTipText = "截图";
            tools[1].Items[0].Click += new EventHandler(SaveScreenShot);
            tools[1].Items.Add(Properties.Resources.image_minicut);
            tools[1].Items[1].ToolTipText = "生成最小割集";
            tools[1].Items[1].Click += new EventHandler(GenerateMiniCut);

            //setMenuStatus();//设置菜单、工具栏显示状态

            //建议平台启动时窗体，将所有需启动时显示的窗体加入词典中，包括平台提供的四个窗体
            viewform.Name = "TreeViewFrom";//为窗体设置ID
            toolformdictionary.Add(viewform.Name, viewform);//将窗体加入平台窗体词典中，以ID为键
            formlocationdictionary.Add(viewform.Name, new FormLoc(DockState.DockRight));//将窗体位置加入平台窗体位置词典中，以ID为键

            tabform.Name = "TabViewFrom";
            toolformdictionary.Add(tabform.Name, tabform);
            formlocationdictionary.Add(tabform.Name, new FormLoc(DockState.Document));

            infoform.Name = "InfoViewForm";
            toolformdictionary.Add(infoform.Name, infoform);
            formlocationdictionary.Add(infoform.Name, new FormLoc("TabViewForm", DockAlignment.Bottom, 0.3));

            serverform.Name = "ServerViewForm";
            toolformdictionary.Add(serverform.Name, serverform);
            formlocationdictionary.Add(serverform.Name, new FormLoc(DockState.DockBottomAutoHide));

            ((SFTATreeViewForm)viewform).SelectedNodeChange = new SelectedNodeChangeHandler(RefreshMenu);//窗体选中的节点发生变化，刷菜单类型
        }

        public override Dictionary<string, Platform.Core.Services.FormLoc> FormLocationDictionary
        {
            get { return formlocationdictionary; }
        }

        public override Platform.Core.UI.BaseForm InfoForm
        {
            get
            {
                return infoform;
            }
            set
            {
            }
        }

        public override ToolStripMenuItem[] PluginMenus
        {
            get { return menus; }
        }

        public override ToolStrip[] PluginTools
        {
            get { return tools; }
        }

        public override Platform.Core.UI.BaseForm ServerForm
        {
            get
            {
                return serverform;
            }
            set
            {
            }
        }

        public override Platform.Core.UI.BaseForm TabForm
        {
            get
            {
                return tabform;
            }
            set
            {
            }
        }

        public override Dictionary<string, Platform.Core.UI.BaseForm> ToolFormDictionary
        {
            get { return toolformdictionary; }
        }

        public override Platform.Core.UI.BaseForm ViewForm
        {
            get
            {
                return viewform;
            }
            set
            {
            }
        }

        //根据TreeView点击的节点类型，刷新工具栏的状态
        public void RefreshMenu(string tag)
        {
            ToolStripItem item = ServicesManager.ServicesManagerSingleton.UIService.GetMenuStrip(null, new MenuArgs(0, 0));
          //  ToolStrip tool = ServicesManager.ServicesManagerSingleton.UIService.GetToolStrip(null, new MenuArgs(0, 0));

            if (tag.Equals("SoftwareName"))
            {
                if (item != null)
                    item.Visible = false;
              //  tool.Enabled = false;
                //ServicesManager.ServicesManagerSingleton.UIService.RefreshMainUI(null,null);
            }
            else
                if (item != null)
                    item.Enabled = true;

        }

        public void CreateFailureTree(object sender, EventArgs e)
        {
            if (ProjectManager.ProjectManagerSington.GetCurrentProject() != null)
            {
                SFTATreeViewForm currenttreeform = (SFTATreeViewForm)ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(null, new UserUIEventArgs("TreeViewForm"));
                currenttreeform.AddTopEventToolStripMenuItem_Click(null, null);
            }
        }

        public void SaveScreenShot(object sender, EventArgs e)
        {
            SFTAProject currentpj=(SFTAProject)ProjectManager.ProjectManagerSington.GetCurrentProject();
            if (currentpj != null)
            {
                FTAForm currentftaform = (FTAForm)ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(null, new UserUIEventArgs(currentpj.activeformid));
                if (currentftaform != null)
                    currentftaform.exportDiagramToolStripMenuItem_Click(null, null);
            }
        }

        public void GenerateMiniCut(object sender, EventArgs e)
        {
            SFTAProject currentpj = (SFTAProject)ProjectManager.ProjectManagerSington.GetCurrentProject();
            if (currentpj != null)
            {
                FTAForm currentftaform = (FTAForm)ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(null, new UserUIEventArgs(currentpj.activeformid));
                if (currentftaform != null)
                    currentftaform.generateMinimalCutToolStripMenuItem_Click(null, null);
            }
        }

        public void IPOAnalysis(object sender, EventArgs e)
        {
            IPOForm ipoform = new IPOForm();
            ServicesManager.ServicesManagerSingleton.UIService.AddMutableResourceSelf(ipoform,new UserUIEventArgs("ipoform"));
            ipoform.Show();
        }

        void SelectTemplate_Click(object sender, EventArgs e)
        {
            if (ProjectManager.ProjectManagerSington.GetAllProjects().Count != 0)
                MessageBox.Show("当前存在已打开工程！请关闭后再打开新工程！");
            else
            {
                //当前无打开的本地工程
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                List<PluginInfo> plugintypes = PluginsManager.PluginsManagerSington.GetAllPluginInfo();
                string pluginname = null;
                string suffixes = null;
                foreach (PluginInfo plugininfo in plugintypes)
                {
                    pluginname = plugininfo.Suffix;
                    suffixes = suffixes + "CASREE文件(*." + pluginname + ")|*." + pluginname + "|";
                }
                suffixes = suffixes.Remove(suffixes.Length - 1);
                openFileDialog1.Filter = suffixes;//指定文件的扩展名
                openFileDialog1.RestoreDirectory = true;
                string filepath;
                filepath = Application.StartupPath + "\\EmbeddedSoftware.fta";//选择已创建的模板
                //filepath = "C:\\Users\\laijing\\Desktop\\1.fta";//openFileDialog1.FileName;//			"C:\\Users\\laijing\\Desktop\\1.fta"	

                AbstractProject project = ServicesManager.ServicesManagerSingleton.ProjectService.OpenProject(filepath);
                IPlugin plugin = PluginsManager.PluginsManagerSington.GetPlugin(project.Suffix);
                //打开工程
                ServicesManager.ServicesManagerSingleton.PluginsService.DealRequst(null, new OpenProjectArgs("Main", project.Suffix, project));
                //加载工程UI
                ServicesManager.ServicesManagerSingleton.UIService.LoadUI_CreateProject(plugin, new ProjectUIArgs(project.Name, project.Path, plugin.MutableResourceClassFullName, project.UUID));
            }
        }

        public void ImportFTADatabase(object sender, EventArgs e)
        {
            String connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=SFTADataBase.mdb";
            OleDbConnection connection = new OleDbConnection(connectionString);
            string searchsql = "select * from SFTATreeInfo";
            OleDbCommand cmd = new OleDbCommand(searchsql, connection);
            connection.Open();//打开连接
            int result=cmd.ExecuteNonQuery();

            ///查询
            DataSet ds = new DataSet();//DataSet是表的集合
            OleDbDataAdapter da = new OleDbDataAdapter(searchsql, connection);//从数据库中查询
            da.Fill(ds);//将数据填充到DataSet
            

            ///更新
            string nodeID = string.Empty;
            string nodeName = string.Empty;
            string insertsql = string.Format("insert into SFTATreeInfo values('{0}','{1}')", nodeID,nodeName);
            OleDbCommand oc = new OleDbCommand();//表示要对数据源执行的SQL语句或存储过程
            oc.CommandText = insertsql;//设置命令的文本
            oc.CommandType = CommandType.Text;//设置命令的类型
            oc.Connection = connection;//设置命令的连接
            int x = oc.ExecuteNonQuery();//执行SQL语句

            connection.Close();//关闭连接
        }

        public void ExistedEvents(object sender, EventArgs e)
        {
            ExistedEventsForm existedform = (ExistedEventsForm)ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(null, new UserUIEventArgs("ExistedEventsForm"));
            if (existedform == null)
            {
                existedform = new ExistedEventsForm();
                ServicesManager.ServicesManagerSingleton.UIService.AddMutableResourceSelf(existedform, new UserUIEventArgs("ExistedEventsForm", new FormLoc(DockState.DockLeft)));
            }
            else
            {
                existedform.RefreshContent();
                existedform.Show();
            }
        }

        public void ToWord(object sender, EventArgs e)
        {
            SFTAProject currentpj = (SFTAProject)ProjectManager.ProjectManagerSington.GetCurrentProject();
            if (currentpj != null)
            {
                Dictionary<string, FTATreeInfo> ftatreeDic = currentpj.SFTATreesDic;

                FTAForm currentftaform = (FTAForm)ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(null, new UserUIEventArgs(currentpj.activeformid));
                if (currentftaform != null)
                    currentftaform.produceWord(ftatreeDic,null);
            }
            MessageBox.Show("生成报表成功");
        }
       
    }
}
