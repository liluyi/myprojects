using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Platform.Core;
using Platform.Core.UI;
using Platform.Core.Services;
using Platform.Core.Data;
using WeifenLuo.WinFormsUI.Docking;
using System.Threading;
using System.Runtime.Remoting.Messaging;
using System.Xml;
using System.IO;

namespace MainPlugin
{
    public class SamplePlugin : IPlugin
    {

        #region IPlugin Members

        public string MutableResourceClassFullName
        {
            get
            {
                return "MainPlugin.MainMutableResource";
            }
            set
            {
                
            }
        }

       

        public string Token
        {
            get
            {
                return token;
            }
            set
            {
                token = value;
            }
        }
        public string ProjectSuffix
        {
            get
            {
                return "wsp";
            }
            set
            {

            }
        }
        public string ProjectClassFullName
        {
            get
            {
                return "Null";
            }
            set
            {

            }
        }
        public Platform.Core.Data.AbstractProject GetProjectFromPath(string path)
        {
            return null;
        }

        public Platform.Core.Data.AbstractProject GetDefaultProject()
        {
            return null;
        }

        public Platform.Core.Data.AbstractProjectData GetProjectDataFromPath(string path)
        {
            return null;
        }

        public Platform.Core.Data.AbstractProjectData GetDefaultProjectData()
        {
            return null;
        }

        #endregion

       
        private string token;
        
        public SamplePlugin()
        {
            
        }

        
    }

    public class MainMutableResource : MutableResource
    {
        public Boolean isConnected;
        private ToolStripMenuItem[] menus;
        private ToolStrip[] tools;
        private ProjectView view = new ProjectView();
        private Dictionary<string, Platform.Core.UI.BaseForm> toolformdictionary;
        private Dictionary<string, FormLoc> formlocationdictionary = new Dictionary<string, FormLoc>();
        private string historydir=null;

        public MainMutableResource()
        {
            //公共菜单，共四列
            menus = new ToolStripMenuItem[4];

            //工程文件菜单
            menus[0] = new ToolStripMenuItem("工程");

            menus[0].DropDownItems.Add("新建工程");
            menus[0].DropDownItems[0].Click += new EventHandler(BuildProject_Click);

            menus[0].DropDownItems.Add("打开工程");
            menus[0].DropDownItems[1].Click += new EventHandler(OpenProject_Click);

            menus[0].DropDownItems.Add("保存工程");
            menus[0].DropDownItems[2].Click += new EventHandler(SaveProject_Click);

            menus[0].DropDownItems.Add("工程另存为");
            menus[0].DropDownItems[3].Click += new EventHandler(SaveAsProject_Click);

            menus[0].DropDownItems.Add("关闭工程");
            menus[0].DropDownItems[4].Click += new EventHandler(CloseProject_Click);

            menus[0].DropDownItems.Add("工程另存为XML");
            menus[0].DropDownItems[5].Click += new EventHandler(SaveProjectAsXML_Click);
            ////////
            //menus[0].DropDownItems.Add("选择嵌入式软件分析模板创建");
            //menus[0].DropDownItems[6].Click += new EventHandler(SelectTemplate_Click);

            //视图菜单，即将取消，展示平台公共视图面板
            menus[1] = new ToolStripMenuItem("视图");
            //menus[1].DropDownItems.Add("树状项目面板");
            //menus[1].DropDownItems[0].Click += new EventHandler(CreateTreeView);
            //menus[1].DropDownItems.Add("主功能面板");
            //menus[1].DropDownItems[1].Click += new EventHandler(CreateTabView);
            //menus[1].DropDownItems.Add("信息面板");
            //menus[1].DropDownItems[2].Click += new EventHandler(CreateInfoView);
            //menus[1].DropDownItems.Add("服务器面板");
            //menus[1].DropDownItems[3].Click += new EventHandler(CreateServerView);

            //服务器连接菜单
            menus[2] = new ToolStripMenuItem("连接");

            menus[2].DropDownItems.Add("连接服务器");
            menus[2].DropDownItems[0].Click += new EventHandler(ConnecttoServer_Click);

            menus[2].DropDownItems.Add("连接服务器项目");
            menus[2].DropDownItems[1].Click += new EventHandler(GetServerProgram_Click);
            menus[2].DropDownItems[1].Visible = false;

            menus[2].DropDownItems.Add("下载服务器文件");
            menus[2].DropDownItems[2].Click += new EventHandler(GetDocument_Click);
            menus[2].DropDownItems[2].Visible = false;

            menus[2].DropDownItems.Add("发送文件");
            menus[2].DropDownItems[3].Click += new EventHandler(SendDocument_Click);
            menus[2].DropDownItems[3].Visible = false;

            menus[2].DropDownItems.Add("下载服务器端数据");
            menus[2].DropDownItems[4].Click += new EventHandler(GetXML_Click);
            menus[2].DropDownItems[4].Visible = false;

            menus[2].DropDownItems.Add("发送本地数据");
            menus[2].DropDownItems[5].Click += new EventHandler(SendXML_Click);
            menus[2].DropDownItems[5].Visible = false;

            menus[2].DropDownItems.Add("断开服务器连接");
            menus[2].DropDownItems[6].Click += new EventHandler(DisConnectServer_Click);
            menus[2].DropDownItems[6].Visible = false;

            //版本控制菜单
            menus[3] = new ToolStripMenuItem("版本控制");
            menus[3].DropDownItems.Add("标记当前版本");
            menus[3].DropDownItems[0].Click += new EventHandler(MarkProject_Click);
            menus[3].DropDownItems.Add("恢复历史版本");
            menus[3].DropDownItems[1].Click += new EventHandler(RestoreProject_Click);

            //工具栏们
            tools = new ToolStrip[1];
            tools[0] = new ToolStrip();
            tools[0].ShowItemToolTips = true;
            tools[0].Items.Add(Properties.Resources.Create);
            tools[0].Items[0].ToolTipText = "创建新工程";
            tools[0].Items[0].Click += new EventHandler(BuildProject_Click);
            tools[0].Items.Add(Properties.Resources.OpenPro);
            tools[0].Items[1].ToolTipText = "打开工程";
            tools[0].Items[1].Click += new EventHandler(OpenProject_Click);
            tools[0].Items.Add(Properties.Resources.Save);
            tools[0].Items[2].ToolTipText = "保存工程";
            tools[0].Items[2].Click += new EventHandler(SaveProject_Click);
            tools[0].Items.Add(Properties.Resources.SaveAs);
            tools[0].Items[3].ToolTipText = "工程另存为";
            tools[0].Items[3].Click += new EventHandler(SaveAsProject_Click);
            tools[0].Items.Add(Properties.Resources.Close);
            tools[0].Items[4].ToolTipText = "关闭工程";
            tools[0].Items[4].Click += new EventHandler(CloseProject_Click);
        }

        #region MutableResource

        public override BaseForm ViewForm
        {
            get 
            { 
                return new TreeViewForm(); 
            }
            set
            {

            }
        }

        public override BaseForm TabForm
        {
            get 
            { 
                return new TabViewForm(); 
            }
            set
            {

            }
        }

        public override BaseForm InfoForm
        {
            get 
            { 
                return new InfoViewForm(); 
            }
            set
            {

            }
        }

        public override BaseForm ServerForm
        {
            get
            {
                return new ServerViewForm();
            }
            set
            {

            }
        }

        public override Dictionary<string, Platform.Core.UI.BaseForm> ToolFormDictionary
        {
            get { return toolformdictionary; }
        }
        public override Dictionary<string, FormLoc> FormLocationDictionary
        {
            get { return formlocationdictionary; }
        }
        public override System.Windows.Forms.ToolStripMenuItem[] PluginMenus
        {
            get { return menus; }
        }

        public override System.Windows.Forms.ToolStrip[] PluginTools
        {
            get { return tools; }
        }

        #endregion
        //   void SelectTemplate_Click(object sender, EventArgs e)
        //{
        //    if (ProjectManager.ProjectManagerSington.GetAllProjects().Count != 0)
        //        MessageBox.Show("当前存在已打开工程！请关闭后再打开新工程！");
        //    else
        //    {
        //        //当前无打开的本地工程
        //        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        //        List<PluginInfo> plugintypes = PluginsManager.PluginsManagerSington.GetAllPluginInfo();
        //        string pluginname = null;
        //        string suffixes = null;
        //        foreach (PluginInfo plugininfo in plugintypes)
        //        {
        //            pluginname = plugininfo.Suffix;
        //            suffixes = suffixes + "CASREE文件(*." + pluginname + ")|*." + pluginname + "|";
        //        }
        //        suffixes = suffixes.Remove(suffixes.Length - 1);
        //        openFileDialog1.Filter = suffixes;//指定文件的扩展名
        //        openFileDialog1.RestoreDirectory = true;
        //        string filepath;
        //        filepath = Application.StartupPath + "\\EmbeddedSoftware.fta";//选择已创建的模板
        //        //filepath = "C:\\Users\\laijing\\Desktop\\1.fta";//openFileDialog1.FileName;//			"C:\\Users\\laijing\\Desktop\\1.fta"	

        //            AbstractProject project = ServicesManager.ServicesManagerSingleton.ProjectService.OpenProject(filepath);
        //            IPlugin plugin = PluginsManager.PluginsManagerSington.GetPlugin(project.Suffix);
        //            //打开工程
        //            ServicesManager.ServicesManagerSingleton.PluginsService.DealRequst(null, new OpenProjectArgs("Main", project.Suffix, project));
        //            //加载工程UI
        //            ServicesManager.ServicesManagerSingleton.UIService.LoadUI_CreateProject(plugin, new ProjectUIArgs(project.Name, project.Path, plugin.MutableResourceClassFullName, project.UUID));
        //            menus[0].DropDownItems[2].Visible = false;
        //            tools[0].Items[2].Visible = false;
                  
        //    }
        //}

        void NetworkMenuStatus(Boolean isConnected)
        {
            //BaseForm form = ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(null, new UserUIEventArgs("ServerViewForm", DockState.DockLeftAutoHide));
            //if (form != null)
            //{
            //    Label sendstatus = new Label();
            //    form.Controls.Add(sendstatus);
            //    sendstatus.Text = "网络连接状态：网络已连接！";
            //    form.Refresh();
            //}

            if (isConnected == false)
            {
                menus[2].DropDownItems[0].Visible = true;//断开服务器成功后，重新显示连网菜单
                menus[2].DropDownItems[1].Visible = false;
                menus[2].DropDownItems[2].Visible = false;
                menus[2].DropDownItems[3].Visible = false;
                menus[2].DropDownItems[4].Visible = false;
                menus[2].DropDownItems[5].Visible = false;
                menus[2].DropDownItems[6].Visible = false;
            }
            else
            {
                menus[2].DropDownItems[0].Visible = false;//连接服务器成功后，将该菜单项隐藏
                menus[2].DropDownItems[1].Visible = true;
                menus[2].DropDownItems[2].Visible = true;
                menus[2].DropDownItems[3].Visible = true;
                menus[2].DropDownItems[4].Visible = true;
                menus[2].DropDownItems[5].Visible = true;
                menus[2].DropDownItems[6].Visible = true;
            }
        }
        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ConnecttoServer_Click(object sender, EventArgs e)
        {
            string toolname=string.Empty;;
            string projectid=string.Empty;
            foreach (PluginInfo info in PluginsManager.PluginsManagerSington.GetAllPluginInfo())
            {
                if (!info.Token.Equals("Main"))
                    toolname = info.Token;//获取当前CASREE平台运行的工具token，从配置文件中读取
            }
            if (toolname.Equals(string.Empty))
            {//工具名称不能为空
                MessageBox.Show("非法的CASREE工具！！！");
            }
            else
            {
                AbstractProject currentprj = ProjectManager.ProjectManagerSington.GetCurrentProject();
                if (currentprj == null)
                    projectid = string.Empty;
                else
                    projectid = currentprj.Name;//////注意，此处传送的为工程名称
                isConnected = false;
                AuthenticationForm form = new AuthenticationForm(toolname,projectid);
                if (form.ShowDialog() == DialogResult.OK)//确定连接
                {
                    Console.WriteLine("正在连接，请稍候……");
                    //待完善连接成功后的菜单显示
                    isConnected = true;
                    NetworkMenuStatus(isConnected);
                }
                else if (form.ShowDialog() == DialogResult.Cancel)//取消连接
                    isConnected = false;
            }
        }

        /// <summary>
        /// 断开服务器连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DisConnectServer_Click(object sender, EventArgs e)
        {
            if (isConnected == true)
            {
                if (ServicesManager.ServicesManagerSingleton.NetworkService.DisConnectServer(new ConnectingArgs(null, 0, null, null,null,null)) == true)
                {
                    isConnected = false;
                    NetworkMenuStatus(isConnected);
                }
            }
            else
                MessageBox.Show("尚未连接服务器！");
        }

        #region 下载服务器端任意文件
        /// <summary>
        /// 获取服务器端文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void GetDocument_Click(object sender, EventArgs e)
        {
            if (isConnected == true)
            {
                IAsyncResult iar = ServicesManager.ServicesManagerSingleton.NetworkService.GetDocument.BeginInvoke(new AsyncCallback(GetDocResult), null);
            }
            else
                MessageBox.Show("尚未连接服务器！");
        }
        /// <summary>
        /// 异步下载服务器端文件文件
        /// </summary>
        /// <param name="ias">异步线程下载结果</param>
        void GetDocResult(IAsyncResult ias)
        {
            //获取异步线程的处理结果
            AsyncResult ar = (AsyncResult)ias;
            //获取异步线程中的委托
            AcceptingHandler a = (AcceptingHandler)ar.AsyncDelegate;
            //获取下载成功失败与否的标志
            Boolean received = a.EndInvoke(ias);
            //弹窗提示
            if (received == false)
                MessageBox.Show("文件下载失败！");
            else
                MessageBox.Show("文件下载成功！");
        }
        #endregion

        #region 向服务器端发送文件
        /// <summary>
        /// 向服务器端发送本地文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SendDocument_Click(object sender, EventArgs e)
        {
            if (isConnected == true)
            {
                //文件选择窗体
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.RestoreDirectory = true;//记忆上次打开地址
                string filepath;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //文件路径
                    filepath = openFileDialog1.FileName;
                    string filename = openFileDialog1.SafeFileName;
                    //启动新线程，发送文件，发送结果将由SendDocResult异步获取
                    AbstractProject currentproject = ProjectManager.ProjectManagerSington.GetCurrentProject();
                    IAsyncResult iar = ServicesManager.ServicesManagerSingleton.NetworkService.SendDocument.BeginInvoke(new SendDocArgs(currentproject.Solution,currentproject.Suffix,filename,filepath),new AsyncCallback(SendDocResult),null);
                    Console.WriteLine("send ...");
                }           
            }
            else
                MessageBox.Show("尚未连接服务器！");
        }

        /// <summary>
        /// 从本地向服务器端发送文件时的处理结果
        /// </summary>
        /// <param name="ias">异步线程的处理结果</param>
        void SendDocResult(IAsyncResult ias)
        {
            //获取异步线程的处理结果
            AsyncResult ar = (AsyncResult)ias;
            //获取异步线程中的委托
            SendingHandler s = (SendingHandler)ar.AsyncDelegate;
            //获取发送成功失败与否的标志
            Boolean sent=s.EndInvoke(ias);
            //弹窗提示
            if (sent == false)
                MessageBox.Show("文件发送失败！");
            else
                MessageBox.Show("文件发送成功！");
        }
        #endregion

        /// <summary>
        /// 获取服务器端工程数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void GetXML_Click(object sender, EventArgs e)
        {
            if (isConnected == true)
            {
                AbstractProject currentprj = ProjectManager.ProjectManagerSington.GetCurrentProject();
                if (currentprj != null)
                {
                    //IAsyncResult iar = ServicesManager.ServicesManagerSingleton.NetworkService.SendXML.BeginInvoke(new SendDocArgs(currentprj.Solution, currentprj.Name, currentprj.Name + ".xml", currentprj.Path + currentprj.Name + "\\" + currentprj.Name + ".xml"), new AsyncCallback(SendXmlResult), null);
                    Platform.Core.Services.Message in_message = ServicesManager.ServicesManagerSingleton.NetworkService.GetXMLRequest(new RequestArgs(currentprj.Solution,currentprj.Name));//发请求
                    if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("allow") == 0)
                    {
                        IAsyncResult iar = ServicesManager.ServicesManagerSingleton.NetworkService.GetXML.BeginInvoke(new AsyncCallback(GetXmlResult), null);
                        //ServicesManager.ServicesManagerSingleton.NetworkService.GetXML();
                        Console.WriteLine("get xml ...");
                    }
                }
            }
            else
                MessageBox.Show("尚未连接服务器！");
        }
        void GetXmlResult(IAsyncResult ias)
        {
            //获取异步线程的处理结果n
            AsyncResult ar = (AsyncResult)ias;
            //获取异步线程中的委托
            AcceptingHandler a = (AcceptingHandler)ar.AsyncDelegate;
            //获取下载成功失败与否的标志
            Boolean got = a.EndInvoke(ias);
            //弹窗提示                                                       
            if (got == false)
                MessageBox.Show("接收XML失败！");
            else
            {
                MessageBox.Show("接收XML成功！");
                //BaseForm form = ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(null, new UserUIEventArgs("ServerViewForm"));
                //if (form != null)
                //{
                //    Label sendstatus = new Label();
                //    form.Controls.Add(sendstatus);
                //    sendstatus.Text = "数据接收成功！正在处理，请稍候……";
                //    form.Refresh();

                //    //分析文件内容并显示到ServerForm窗口
                //    AbstractProject currentprj = ProjectManager.ProjectManagerSington.GetCurrentProject();
                //    if (currentprj != null)
                //    {
                //        string fileDirectory = currentprj.Path + "\\" + currentprj.Name + "\\" + "receivedXML";
                //        if (Directory.Exists(fileDirectory))
                //        {
                //            string fileName = Directory.GetFiles(fileDirectory).Last();

                //            if (fileName != null)
                //            {
                //                StreamReader sr = new StreamReader(fileName, Encoding.Default);
                //                while (sr.ReadLine() != null)
                //                    sendstatus.Text += sr.ReadLine();
                //            }
                //        }
                //    }
                //}
                    
            }
        }

        /// <summary>
        /// 向服务器端发送本地工程数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SendXML_Click(object sender, EventArgs e)
        {
            if (isConnected == true)
            {
                AbstractProject currentprj = ProjectManager.ProjectManagerSington.GetCurrentProject();
                if (currentprj != null)
                {
                    IAsyncResult iar = ServicesManager.ServicesManagerSingleton.NetworkService.SendXML.BeginInvoke(new SendDocArgs(currentprj.Solution, currentprj.Name, currentprj.Name + ".xml", currentprj.Path + currentprj.Name + "\\" + currentprj.Name + ".xml"), new AsyncCallback(SendXmlResult), null);
                    Console.WriteLine("sending xml ...");
                }
            }
            else
                MessageBox.Show("尚未连接服务器！");
        }
        void SendXmlResult(IAsyncResult ias)
        {
            //获取异步线程的处理结果n
            AsyncResult ar = (AsyncResult)ias;
            //获取异步线程中的委托
            SendingHandler a = (SendingHandler)ar.AsyncDelegate;
            //获取下载成功失败与否的标志
            Boolean sent = a.EndInvoke(ias);
            //弹窗提示
            if (sent == false)
                MessageBox.Show("发送XML失败！");
            else
                MessageBox.Show("发送XML成功！");
        }
        /// <summary>
        /// 打开树状面板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CreateTreeView(object sender, EventArgs e)
        {
            if(PluginsManager.PluginsManagerSington.GetViewForm()!=null)
                if (PluginsManager.PluginsManagerSington.GetViewForm().IsHidden == true)
                {
                    BaseForm form = PluginsManager.PluginsManagerSington.GetViewForm();
                    form.Text = "新打开的TreeViewForm";
                    form.Show();
                }
        }

        /// <summary>
        /// 打开主面板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CreateTabView(object sender, EventArgs e)
        {
            if (PluginsManager.PluginsManagerSington.GetTabForm() != null)
                if (PluginsManager.PluginsManagerSington.GetTabForm().IsHidden == true)
                {
                    BaseForm form = PluginsManager.PluginsManagerSington.GetTabForm();
                    form.Text = "新打开的TabViewForm";
                    form.Show();
                }
        }

        /// <summary>
        /// 打开信息面板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CreateInfoView(object sender, EventArgs e)
        {
            if (PluginsManager.PluginsManagerSington.GetInfoForm() != null)
                if (PluginsManager.PluginsManagerSington.GetInfoForm().IsHidden == true)
                {
                    BaseForm form = PluginsManager.PluginsManagerSington.GetInfoForm();
                    form.Text = "新打开的InfoViewForm";
                    form.Show();
                }
        }

        /// <summary>
        /// 打开服务器面板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CreateServerView(object sender, EventArgs e)
        {
            if (PluginsManager.PluginsManagerSington.GetViewForm() != null)
                if (PluginsManager.PluginsManagerSington.GetServerForm().IsHidden == true)
                {//若服务器面板被关闭，则打开
                    BaseForm form = PluginsManager.PluginsManagerSington.GetServerForm();
                    form.Text = "新打开的ServerViewForm";
                    form.Show();
                }
        }

        //void SamplePlugin_Click(object sender, EventArgs e)
        //{
        //    List<PluginInfo> plugininfos = PluginsManager.PluginsManagerSington.GetAllPluginInfo();
        //    foreach (PluginInfo info in plugininfos)
        //    {
        //        MessageBox.Show(info.ToString());
        //    }
        //}
 
        /// <summary>
        /// 创建新工程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BuildProject_Click(object sender, EventArgs e)
        {
            int count = ProjectManager.ProjectManagerSington.GetAllProjects().Count;

            if (count == 0)
            {//当前没有已打开的工程
                ///此段支持采用BuildPlugin窗体，动态选取工具并创建对应工程
                //List<PluginInfo> plugininfos = PluginsManager.PluginsManagerSington.GetAllPluginInfo();

                //BuildPlugin form = new BuildPlugin(plugininfos);
                //form.ShowDialog();

                //if (form.buildproject == true)
                //{
                //    if (form.index >= 0 && plugininfos.Count > 0)
                //    {
                //        string token = plugininfos[form.index].Token;
                //        ServicesManager.ServicesManagerSingleton.PluginsService.DealRequst(null, new BuildProjectArgs(this.token, token, form.projectname, form.projectpath));
                //        AbstractProject currentproject = ProjectManager.ProjectManagerSington.GetProject(ProjectManager.ProjectManagerSington.GetProjectUUID());
                //        Platform.Core.ServicesManager.ServicesManagerSingleton.ProjectService.SavaAsProject(currentproject, form.projectpath+form.projectname+"."+currentproject.Suffix);
                //    }
                //}
                ///更改为每次只能加载插件目录下的工具并直接创建其对应工程
                CreateProjectForm form = new CreateProjectForm();
                while ( form.buildproject == false) 
                {
                    if (form.ShowDialog() == DialogResult.Cancel)
                        return;
                }
               // if (form.ShowDialog() == DialogResult.OK)
                if(form.buildproject==true)
                {                   
                    string token = form.token;
                    
                    //创建新工程
                    ServicesManager.ServicesManagerSingleton.PluginsService.DealRequst(null, new BuildProjectArgs("Main", token, form.projectname, form.projectpath));

                    //加载工程UI
                    IPlugin plugin = PluginsManager.PluginsManagerSington.GetPlugin(token);
                    AbstractProject project = ProjectManager.ProjectManagerSington.GetCurrentProject();
                    ServicesManager.ServicesManagerSingleton.UIService.LoadUI_CreateProject(plugin, new ProjectUIArgs(form.projectname, form.projectpath, plugin.MutableResourceClassFullName, project.UUID));
                    
                    //本地保存工程数据
                    Platform.Core.ServicesManager.ServicesManagerSingleton.ProjectService.SavaAsProject(project, form.projectpath + "\\"+form.projectname + "." + project.Suffix);
                    if (!Directory.Exists(form.projectpath + "\\" + form.projectname))
                    {
                        Directory.CreateDirectory(form.projectpath + "\\" + form.projectname);
                        Directory.CreateDirectory(form.projectpath + "\\" + form.projectname+"\\"+"History");
                    }
                }
            }
            else
            {
                string ms = "工程" + ProjectManager.ProjectManagerSington.GetProjectUUID() + "已打开!!!请保存并关闭后再打开新工程！！！";
                MessageBox.Show(ms);
            }
        }

        /// <summary>
        /// 打开磁盘上的工程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OpenProject_Click(object sender, EventArgs e)
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
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filepath = openFileDialog1.FileName;
                    AbstractProject project = ServicesManager.ServicesManagerSingleton.ProjectService.OpenProject(filepath);
                    IPlugin plugin = PluginsManager.PluginsManagerSington.GetPlugin(project.Suffix);
                    //打开工程
                    ServicesManager.ServicesManagerSingleton.PluginsService.DealRequst(null, new OpenProjectArgs("Main", project.Suffix, project));
                    //加载工程UI
                    ServicesManager.ServicesManagerSingleton.UIService.LoadUI_CreateProject(plugin, new ProjectUIArgs(project.Name, project.Path, plugin.MutableResourceClassFullName, project.UUID));
                }
            }
        }
         
        /// <summary>
        /// 保存当前工程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SaveProject_Click(object sender, EventArgs e)
        {
            //获取平台当前维护的工程ID
            string pid = ProjectManager.ProjectManagerSington.GetProjectUUID();
            //初始化当前工程
            AbstractProject currentproject = null;
            if (pid == null)
                MessageBox.Show("当前没有已打开工程！");
            else
            {
                //获取平台当前维护的工程
                currentproject = ProjectManager.ProjectManagerSington.GetCurrentProject();
                //保存工程的界面布局
                PluginsManager.PluginsManagerSington.SaveMutableResource(pid);
                //保存工程文件
                ServicesManager.ServicesManagerSingleton.ProjectService.SaveProject(currentproject);


                ///保存成XML文档
                AbstractProjectData currentprojectdata = null;

                currentprojectdata = ProjectManager.ProjectManagerSington.GetCurrentProjectData();
                currentprojectdata.GenerateProjectData();//先生成待序列化的数据

                string filepath = null;
                filepath = Path.Combine(currentproject.Path,currentproject.Name + "\\" + currentproject.Name + ".xml");
                ServicesManager.ServicesManagerSingleton.ProjectService.SavaAsXML(currentprojectdata, filepath);
            }
        }

        /// <summary>
        /// 选择本地磁盘路径，输入工程名，保存当前工程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SaveAsProject_Click(object sender, EventArgs e)
        {
            string pid = ProjectManager.ProjectManagerSington.GetProjectUUID();
            AbstractProject currentproject = null;
            if (pid == null)
                MessageBox.Show("当前没有已打开工程！");
            else
            {
                currentproject = ProjectManager.ProjectManagerSington.GetProject(pid);
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = currentproject.Suffix + "文件|*." + currentproject.Suffix;

                string filepath = null;
                if ((saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    filepath = saveDialog.FileName;
                    //分割出工程名称
                    string filename=filepath.Split('\\')[filepath.Split('\\').Length-1].Split('.')[0];
                    currentproject.Name = filename;
                    //分割出保存路径
                    currentproject.Path=filepath.Remove(filepath.LastIndexOf('\\')+1);
                    //创建配置文件所在文件夹
                    if (!Directory.Exists(currentproject.Path + "\\" + currentproject.Name))
                    {
                        Directory.CreateDirectory(currentproject.Path + "\\" + currentproject.Name);
                        Directory.CreateDirectory(currentproject.Path + "\\" + currentproject.Name + "\\" + "History");
                    }
                    //保存界面布局
                    PluginsManager.PluginsManagerSington.SaveMutableResource(currentproject.UUID, currentproject.Path + "\\" + currentproject.Name + "\\" + "History\\");
                    //保存工程
                    Platform.Core.ServicesManager.ServicesManagerSingleton.ProjectService.SavaAsProject(currentproject, filepath);
                }
            }
        }

        /// <summary>
        /// 工程数据文件序列化为XML格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SaveProjectAsXML_Click(object sender, EventArgs e)
        {
            string pid = ProjectManager.ProjectManagerSington.GetProjectUUID();
            AbstractProjectData currentprojectdata = null;
            if (pid == null)
                MessageBox.Show("当前没有已打开工程！");
            else
            {
                currentprojectdata = ProjectManager.ProjectManagerSington.GetCurrentProjectData();
                currentprojectdata.GenerateProjectData();//先生成待序列化的数据
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "XML文件|*.xml";

                string filepath = null;
                if ((saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    filepath = saveDialog.FileName;
                    ServicesManager.ServicesManagerSingleton.ProjectService.SavaAsXML(currentprojectdata, filepath);
                }
            }
        }

        /// <summary>
        /// 关闭当前工程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void CloseProject_Click(object sender, EventArgs e)
        {
            string projectuuid = null;
            projectuuid = ProjectManager.ProjectManagerSington.GetProjectUUID();
            if (projectuuid != null)
            {
                //先询问是否保存工程
                DialogResult dr = MessageBox.Show("是否保存工程文件？", "警告", MessageBoxButtons.YesNoCancel);//提示是否共享事件
                if (dr == DialogResult.Yes)  //保存
                {
                    this.SaveProject_Click(null, null);
                }
                else if (dr == DialogResult.Cancel) //取消关闭
                    return;
                //再关闭工程
                ServicesManager.ServicesManagerSingleton.PluginsService.DealRequst(this, new RemoveProjectArgs("Main", projectuuid));
            }
            else
                MessageBox.Show("当前不存在已打开的工程！");
        }

        /// <summary>
        /// 显示服务器项目列表，并将当前工程链接、上传至服务器端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void GetServerProgram_Click(object sender, EventArgs e)
        {
            string pid = ProjectManager.ProjectManagerSington.GetProjectUUID();//查看当前是否存在工程
            if (pid == null)
                MessageBox.Show("当前没有已打开工程！");
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("ServerDatabase.xml");
                XmlNode xn = doc.SelectSingleNode("/Solutions");
                XmlNodeList solutionlist = xn.ChildNodes;
                ProgramListForm form = new ProgramListForm(solutionlist);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ProjectManager.ProjectManagerSington.GetCurrentProject().Solution = form.programname;
                    MessageBox.Show(form.programname);//获取选择的服务器项目名称
                }
            }
        }

        /// <summary>
        /// 标记当前工程版本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MarkProject_Click(object sender, EventArgs e)
        {
            string pid = ProjectManager.ProjectManagerSington.GetProjectUUID();
            AbstractProject currentproject = null;
            if (pid == null)
                MessageBox.Show("当前没有已打开工程！");
            else
            {
                currentproject = ProjectManager.ProjectManagerSington.GetProject(pid);
                string filepath = null;
                filepath = currentproject.Path+currentproject.Name+"\\"+"History"+"\\"+currentproject.Name+getTime()+ "."+currentproject.Suffix;
                Platform.Core.ServicesManager.ServicesManagerSingleton.ProjectService.SavaAsProject(currentproject, filepath);
            }
        }

        /// <summary>
        /// 恢复历史版本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RestoreProject_Click(object sender, EventArgs e)
        {
            string pid = ProjectManager.ProjectManagerSington.GetProjectUUID();
            AbstractProject currentproject = null;
            if (pid == null)
                MessageBox.Show("当前没有已打开工程！");
            else
            {
                currentproject = ProjectManager.ProjectManagerSington.GetProject(pid);
                string filepath = null;
                filepath = currentproject.Path + currentproject.Name + "\\" + "History";
                DirectoryInfo dir = new DirectoryInfo(filepath);
                FileInfo[] hisfiles = dir.GetFiles("*." + currentproject.Suffix, SearchOption.AllDirectories);
                if (hisfiles.Length != 0)
                {
                    ProjectListForm form = new ProjectListForm(hisfiles);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        string filename = hisfiles[form.index].Name;

                        ServicesManager.ServicesManagerSingleton.PluginsService.DealRequst(this, new RemoveProjectArgs("Main", pid));
                        AbstractProject project = ServicesManager.ServicesManagerSingleton.ProjectService.OpenProject(filepath + "\\" + filename);
                        IPlugin plugin = PluginsManager.PluginsManagerSington.GetPlugin(project.Suffix);
                        //打开工程
                        ServicesManager.ServicesManagerSingleton.PluginsService.DealRequst(null, new OpenProjectArgs("Main", project.Suffix, project));
                        //加载工程UI
                        ServicesManager.ServicesManagerSingleton.UIService.LoadUI_CreateProject(plugin, new ProjectUIArgs(project.Name, project.Path, plugin.MutableResourceClassFullName, project.UUID));
                    }
                }
                else
                    MessageBox.Show("无历史版本可供恢复");
            }
        }

        /// <summary>
        /// 获取当前系统时间
        /// </summary>
        /// <returns>时间的字符串表示</returns>
        private string getTime()
        {
            string ct=null;
            DateTime dt = System.DateTime.Now;
            ct = "%" + dt.ToLongDateString() + dt.Hour.ToString() + "时" + dt.Minute.ToString() + "分" + dt.Second.ToString() + "秒" + dt.Millisecond.ToString()+"微秒";
            return ct;
        }
    }
}
