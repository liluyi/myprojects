using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using WeifenLuo.WinFormsUI.Docking;
using Platform.Core.UI;
using System.Collections.Generic;
using Platform.Core.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Platform.Core.Services
{
    /// <summary>
    /// UI服务
    /// </summary>
    internal sealed class UIService : IUIService
    {
        private string description = string.Empty;
        private ServiceState state = ServiceState.UnLoad;
        private InitServiceHandler initService;
        private MainUIFrame mainUIFrame;
        private EventHandler loadingService;
        private EventHandler disposeUI;
        private EventHandler refreshmainUI;
        private MutableResource mutableresource;
        private UIComHandler addMutableResource;
        private UIComHandler deleteMutableResource;
        private UIComHandler showForm;
        private UIComHandler getForm;
        private UIComHandler getLocation;
        private UIDataHandler getFormNames;
        private UIHandler loadui_CreatePlugin;
        private UIHandler loadui_CreateProject;
        private IPlugin mainplugin;
        private MenuHandler getmenu;
        private ToolHandler gettool;


        #region IService Members
        
        public string Description
        {
            get 
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        public string Name
        {
            get
            {
                return "UIService";
            }
        }

        public ServiceLevel Level
        {
            get
            {
                return ServiceLevel.High;
            }
        }

        public ServiceState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

        public InitServiceHandler InitService
        {
            get
            {
                return initService;
            }
        }

        public UIHandler LoadUI_CreatePlugin
        {
            get
            {
                return loadui_CreatePlugin;
            }
        }


        public UIHandler LoadUI_CreateProject
        {
            get
            {
                return loadui_CreateProject;
            }

        }

        public EventHandler LoadingService
        {
            get
            {
                return loadingService;
            }
        }
        public EventHandler DisposingUI
        {
            get
            {
                return disposeUI;
            }
        }
        public EventHandler RefreshMainUI 
        {
            get
            {
                return refreshmainUI;
            }
        }
        public UIComHandler AddMutableResourceSelf
        {
            get
            {
                return addMutableResource;
            }
        }

        public UIComHandler DeleteMutableResource
        {
            get
            {
                return deleteMutableResource;
            }
        }
        public UIComHandler ShowForm
        {
            get
            {
                return showForm;
            }
        }
        public UIComHandler GetUserForm
        {
            get
            {
                return getForm;
            }
        }

        public UIDataHandler GetAllFormNames
        {
            get
            {
                return getFormNames;
            }
        }

        public MenuHandler GetMenuStrip
        {
            get
            {
                return getmenu;
            }
        }
        public ToolHandler GetToolStrip
        {
            get
            {
                return gettool;
            }
        }
        #endregion

        public UIService()
        {
            initService = new InitServiceHandler(Init);
            loadingService = new EventHandler(OnLoadingService);
            disposeUI = new EventHandler(OnDisposeUI);
            refreshmainUI = new EventHandler(OnRefreshMainUI);
            addMutableResource = new UIComHandler(AddMutableResource);
            getForm = new UIComHandler(GetForm);
            showForm = new UIComHandler(OnShowingForm);
            loadui_CreatePlugin = new UIHandler(OnLoadUI_BeforeBuildProject);
            loadui_CreateProject = new UIHandler(OnLoadUI_BuildProject);
            getFormNames = new UIDataHandler(GetAllUserFormNames);
            deleteMutableResource = new UIComHandler(DeleteForm);
            getmenu = new MenuHandler(OnGetMenuStrip);
            gettool = new ToolHandler(OnGetToolStrip);
        }

        private void Init(StartUpSettings sus )
        {
            if (sus == null)
            {
                return;
            }
            Properties p = sus.ServiceProperties["UIService"] as Properties;

            if (p == null)
            {
                return;
            }

            description = p["description"] as string;
        }

        /// <summary>
        /// 初始化UI
        /// </summary>
        /// <param name="sender">调用者</param>
        /// <param name="args">参数</param>
        private void OnLoadingService(object sender, EventArgs args)
        {
            Form MainForm = sender as Form;//主窗体

            if (MainForm == null)
            {
                throw new Exception("error");
            }
        
            MainForm.IsMdiContainer = true;//设置为多文档格式
            MainForm.WindowState = FormWindowState.Maximized;

            mainUIFrame = new MainUIFrame();//窗体内容器

            MainForm.MainMenuStrip = mainUIFrame.MenuStrip;//设置主窗体的菜单，以添加子窗体的菜单响应

            MainForm.Controls.Add(mainUIFrame.DockPanel);//子容器，承载TreeView、InfoView、TabView、ServerView等
            MainForm.Controls.Add(mainUIFrame.StripPanel);//子容器，承载菜单栏与工具栏
            MainForm.Controls.Add(mainUIFrame.StatusStrip);//子容器，承担状态栏

        }

        public void OnRefreshMainUI(object sender, EventArgs args)
        {
            //if (mainUIFrame.MenuStrip.Items != null)
            //{
            //    mainUIFrame.MenuStrip.Items.Clear();
            //    mainUIFrame.StripPanel.Controls.Clear();
            //    mainUIFrame.StripPanel.Join(mainUIFrame.MenuStrip, 0);
            //}
            ////加载主插件的菜单和工具栏
            //IPlugin plugin = mainplugin;
            //Type type = plugin.GetType();//获取插件类型
            //MutableResource r = type.Assembly.CreateInstance(plugin.MutableResourceClassFullName) as MutableResource;//通过反射生成MutableResource实例

            //if (r != null)
            //{
            //    LoadPluginMenu(r);
            //    LoadPluginTool(r);
            //}
            ////加载工具插件的菜单和工具栏
            //foreach (IPlugin toolplugin in PluginsManager.PluginsManagerSington.GetAllPlugins())
            //{
            //    Type type2 = toolplugin.GetType();//获取插件类型
            //    MutableResource r2 = type2.Assembly.CreateInstance(toolplugin.MutableResourceClassFullName) as MutableResource;//通过反射生成MutableResource实例

            //    if (r2 != null)
            //    {
            //        LoadPluginMenu(r2);
            //        LoadPluginTool(r2);
            //    }
            //}
            
        }
        /// <summary>
        /// 启动平台时加载界面项目
        /// </summary>
        /// <param name="plugin"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private bool OnLoadUI_BeforeBuildProject(IPlugin plugin, UIEventArgs args)
        {
            if (plugin == null)
            {
                return false;
            }
            if (plugin.Token == "Main")
            {//加载main插件的界面
                try
                {
                    mainplugin = plugin;
                    Type type = plugin.GetType();//获取插件类型
                    MutableResource r = type.Assembly.CreateInstance(plugin.MutableResourceClassFullName) as MutableResource;//通过反射生成MutableResource实例
             
                    if (r != null)
                    {
                        //不再加载main插件的面板
                        //mainUIFrame.BuildMainMutableResource(r);
                        LoadPluginMenu(r);
                        LoadPluginTool(r);
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("主插件活动区域加载异常！");
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            else
            {//加载其他插件组界面
                Type type = plugin.GetType();//获取插件类型
                MutableResource r = type.Assembly.CreateInstance(plugin.MutableResourceClassFullName) as MutableResource;//通过反射生成MutableResource实例
                //mutableresource = r;
                try
                {
                    //修改：加载全部插件的menu和tool，待创建项目时加载新建工程时无法加载窗体
                    /// PluginsManager.PluginsManagerSington.insertMutableResource(r);//将浮动资源加载至PluginManager维护的浮动资源列表中
                    LoadPluginMenu(r);
                    LoadPluginTool(r);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            
        }

    
        /// <summary>
        /// 创建工程时加载界面
        /// </summary>
        /// <param name="plugin">插件</param>
        /// <param name="UIargs">参数</param>
        /// <returns></returns>
        private bool OnLoadUI_BuildProject(IPlugin plugin, UIEventArgs UIargs)
        {
            try
            {
                ProjectUIArgs args = UIargs as ProjectUIArgs;

                if(args==null)
                {
                    return false;
                }
                    
                Type type = plugin.GetType();
                MutableResource r = type.Assembly.CreateInstance(args.fullclassname) as MutableResource;
                mutableresource = r;

                if (r== null)
                {
                    throw new Exception("生成活动资源出错");
                }
                r.UUID = args.uuid;

                //SetFormText(args.projectname, r);//设置窗体名称
                //必须在mainUIFrame加载窗体前就将浮动资源插入插件管理器所维护的插件词典中
                PluginsManager.PluginsManagerSington.insertMutableResource(r);//将浮动资源加载至PluginManager维护的浮动资源列表中
                mainUIFrame.BuildMutableResource(r);//加载插件全部浮动资源
                //LoadPluginMenu(plugin);//加载插件独有菜单
                //LoadPluginTool(plugin);//加载插件独有工具栏
               // LoadTreeView(r.ViewForm);
                //LoadTabView(r.TabForm);
                //LoadInfoView(r.InfoForm);
                //LoadServerView(r.ServerForm);

                return true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("生成工具界面出错，请检查工具MutableResource子类\n"+ex.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// 销毁UI前保存窗体布局信息
        /// </summary>
        public void OnDisposeUI(object sender, EventArgs args)
        {
            string proname = ProjectManager.ProjectManagerSington.GetCurrentProject().Name;
            string filename = proname + "\\" + proname + ".config";
            string configFile = Path.Combine(ProjectManager.ProjectManagerSington.GetCurrentProject().Path, filename);
            //configFile 配置文件保存的路径
            mainUIFrame.DockPanel.SaveAsXml(configFile);
        }

        
        /// <summary>
        /// 加载插件自定义窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public BaseForm AddMutableResource(Object sender,UserUIEventArgs args)
        {
            try
            {
                BaseForm form = sender as BaseForm;//获取自定义的窗体实例
                form.Name = args.UIItemID;
                PluginsManager.PluginsManagerSington.AddFormInMutableResource(args.UIItemID, form);
                PluginsManager.PluginsManagerSington.AddLocationInMutableResource(args.UIItemID, args.ItemLocation);
                ShowUserForm(form, args.ItemLocation);
              
                return form;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("加载自定义窗体错误！\n" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 通过窗体名称获取窗体实例
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public BaseForm GetForm(Object sender, UserUIEventArgs args)
        {
            string formname = args.UIItemID;
            BaseForm form = null;
            form=PluginsManager.PluginsManagerSington.GetFormFromMutableResource(formname);
            return form;
        }

        public BaseForm DeleteForm(Object sender, UserUIEventArgs args)
        {
            string formname = args.UIItemID;
            BaseForm form = null;
            form = PluginsManager.PluginsManagerSington.GetFormFromMutableResource(formname);
            PluginsManager.PluginsManagerSington.RemoveFormInMutableResource(formname);
            return form;
        }

        public List<string> GetAllUserFormNames(Object sender, UserUIEventArgs args)
        {
            List<string> names = new List<string>();
            names=PluginsManager.PluginsManagerSington.GetAllFormNames();
            return names;
        }

        public BaseForm OnShowingForm(Object sender, UserUIEventArgs args)
        {
            string formname = args.UIItemID;
            BaseForm form = null;
            form = PluginsManager.PluginsManagerSington.GetFormFromMutableResource(formname);
            FormLoc loc = PluginsManager.PluginsManagerSington.GetLocationFromMutableResource(formname);
            ShowUserForm(form, loc);
            return form;
        }

        private void ShowUserForm(BaseForm form, FormLoc loc)
        {
            if (form != null)
            {
                if (loc.State != DockState.Unknown)
                {
                    form.ShowHint = loc.State;
                    form.Show(mainUIFrame.DockPanel);
                }
                else if (!loc.BeforePaneName.Equals(String.Empty))
                {
                    BaseForm preform = GetForm(this, new UserUIEventArgs(loc.PreviousPaneName, null));
                    BaseForm beforeform = GetForm(this, new UserUIEventArgs(loc.BeforePaneName, null));
                    form.Show(preform.Pane, beforeform);
                }
                else if (!loc.PreviousPaneName.Equals(String.Empty))
                {
                    BaseForm preform = GetForm(this, new UserUIEventArgs(loc.PreviousPaneName, null));
                    DockPane pane = preform.Pane;
                    form.Show(pane, loc.Alignment, loc.Proportion);
                }              
                else
                {
                    form.ShowHint = DockState.Document;
                    form.Show(mainUIFrame.DockPanel);
                }
            }
            else
                Debug.WriteLine("显示窗体失败！窗体为空！");
        }


        /// <summary>
        /// 设置窗体标题
        /// </summary>
        /// <param name="plugin"></param>
        private void SetFormText(string text,MutableResource r)
        {
            if (r.TabForm != null)
            {
                r.TabForm.Text = text;
            }
            if (r.ViewForm != null)
            {
                r.ViewForm.Text = text;
            }
            if (r.InfoForm != null)
            {
                r.InfoForm.Text = text;
            }
            if (r.ServerForm != null)
            {
                r.ServerForm.Text = text;
            }
        }

        /// <summary>
        /// 加载plugin菜单
        /// </summary>
        /// <param name="plugin"></param>
        private void LoadPluginMenu(MutableResource mutableresource)
        {
            if (mutableresource.PluginMenus != null && mutableresource.PluginMenus.Length != 0)
            {
                foreach (ToolStripMenuItem menuItem in mutableresource.PluginMenus)
                {
                    mainUIFrame.MenuStrip.Items.Add(menuItem);
                }
            }
        }

        /// <summary>
        /// 加载plugin工具栏
        /// </summary>
        /// <param name="plugin"></param>
        private void LoadPluginTool(MutableResource mutableresource)
        {
            if (mutableresource.PluginTools != null && mutableresource.PluginTools.Length != 0)
            {
                foreach (ToolStrip strip in mutableresource.PluginTools)
                {
                    mainUIFrame.StripPanel.Join(strip,2);
                }
            }
        }

        private ToolStripItem OnGetMenuStrip(object sender,MenuArgs args)
        {
            ToolStripItem menuitem;
            menuitem = (ToolStripItem)mainUIFrame.MenuStrip.Items[args.x+4];
            return menuitem;
        }
        private ToolStrip OnGetToolStrip(object sender, MenuArgs args)
        {
            ToolStrip tool=new ToolStrip();
            tool = (ToolStrip)mainUIFrame.StripPanel.Controls[args.x+1].Controls[args.y];
            return tool;
        }
    /*    /// <summary>
        /// 加载工程活动区域Tree视图
        /// </summary>
        /// <param name="bf"></param>
        private void LoadTreeView(BaseForm bf)
        {
            if (bf != null)
            {
                //bf.Show(mainUIFrame.TreeView.Pane, mainUIFrame.TreeView);     
                bf.ShowHint = DockState.DockLeft;
                bf.Show(mainUIFrame.DockPanel);
            }
        }

        /// <summary>
        /// 加载工程活动区域Tab视图
        /// </summary>
        /// <param name="bf"></param>
        private void LoadTabView(BaseForm bf)
        {
            if (bf != null)
            {
                //bf.Show(mainUIFrame.TabView.Pane, mainUIFrame.TabView);
                bf.ShowHint = DockState.Document;
                bf.Show(mainUIFrame.DockPanel);
            }
        }

        /// <summary>
        /// 加载工程活动区域Info视图
        /// </summary>
        /// <param name="bf"></param>
        private void LoadInfoView(BaseForm bf)
        {
            if (bf != null)
            {
                //bf.Show(mainUIFrame.InfoView.Pane, mainUIFrame.InfoView);
                bf.ShowHint = DockState.DockBottom;
                //bf.Show(mainUIFrame.DockPanel);
                bf.Show(mainUIFrame.TabView.Pane, DockAlignment.Bottom, 0.3);
                //bf.Show(
            }
        }

        /// <summary>
        /// 加载工程活动区域Server视图
        /// </summary>
        /// <param name="bf"></param>
        private void LoadServerView(BaseForm bf)
        {
            if (bf != null)
            {
                //bf.Show(mainUIFrame.ServerView.Pane, mainUIFrame.ServerView);
                bf.ShowHint = DockState.DockLeftAutoHide;
                bf.Show(mainUIFrame.DockPanel);
            }
        }*/

        
    }
    

}