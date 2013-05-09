using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using WeifenLuo.WinFormsUI.Docking;
using Platform.Core.UI;

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
        private UIHandler loadui_CreatePlugin;
        private UIHandler loadui_CreateProject;

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
        #endregion

        public UIService()
        {
            initService = new InitServiceHandler(Init);
            loadingService = new EventHandler(OnLoadingService);
            loadui_CreatePlugin = new UIHandler(OnLoadUI_BeforeBuildProject);
            loadui_CreateProject = new UIHandler(OnLoadUI_BuildProject);
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

        private void OnLoadingService(object sender, EventArgs args)
        {
            Form MainForm = sender as Form;

            if (MainForm == null)
            {
                throw new Exception("error");
            }

            
            MainForm.IsMdiContainer = true;
            MainForm.WindowState = FormWindowState.Maximized;

            mainUIFrame = new MainUIFrame();

            MainForm.Controls.Add(mainUIFrame.DockPanel);
            MainForm.Controls.Add(mainUIFrame.StripPanel);
            MainForm.Controls.Add(mainUIFrame.StatusStrip);

        }


        private bool OnLoadUI_BeforeBuildProject(IPlugin plugin, UIEventArgs args)
        {
            if (plugin == null)
            {
                return false;
            }
            if (plugin.Token == "Main")
            {
                try
                {
                    LoadPluginMenu(plugin);
                    LoadPluginTool(plugin);

                    Type type = plugin.GetType();
                    MutableResource r = type.Assembly.CreateInstance(plugin.MutableResourceClassFullName) as MutableResource;
                    if (r != null)
                    {
                        mainUIFrame.BuildMainMutableResource(r);
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
            {
                try
                {
                    LoadPluginMenu(plugin);
                    LoadPluginTool(plugin);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            
        }

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

                if (r == null)
                {
                    throw new Exception("生成活动资源出错");
                }
                r.UUID = args.uuid;

                SetFormText(args.projectname, r);

                LoadTreeView(r.ViewForm);
                LoadTabView(r.TabForm);
                LoadInfoView(r.InfoForm);

                PluginsManager.PluginsManagerSington.insertMutableResource(r);

                return true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("生成活动资源出错\n"+ex.StackTrace);
                return false;
            }
        }
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
        }
        /// <summary>
        /// 加载plugin菜单
        /// </summary>
        /// <param name="plugin"></param>
        private void LoadPluginMenu(IPlugin plugin)
        {
            if (plugin.PluginMenus != null && plugin.PluginMenus.Length!=0)
            {
                foreach (ToolStripMenuItem menuItem in plugin.PluginMenus)
                {
                    mainUIFrame.MenuStrip.Items.Add(menuItem);
                }
            }
        }

        /// <summary>
        /// 加载plugin工具栏
        /// </summary>
        /// <param name="plugin"></param>
        private void LoadPluginTool(IPlugin plugin)
        {
            if (plugin.PluginTools != null && plugin.PluginTools.Length != 0)
            {
                foreach (ToolStrip strip in plugin.PluginTools)
                {
                    mainUIFrame.StripPanel.Join(strip, 1);
                }
            }
        }
        /// <summary>
        /// 加载工程活动区域Tree视图
        /// </summary>
        /// <param name="bf"></param>
        private void LoadTreeView(BaseForm bf)
        {
            if (bf != null)
            {
                bf.Show(mainUIFrame.TreeView.Pane, mainUIFrame.TreeView);
                
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
                bf.Show(mainUIFrame.TabView.Pane, mainUIFrame.TabView);
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
                bf.Show(mainUIFrame.InfoView.Pane, mainUIFrame.InfoView);
            }
        }

    }
    
    /// <summary>
    /// 主活动界面UI框架
    /// </summary>
    internal sealed class MainUIFrame
    {
        #region 主框架结构变量属性

        /// <summary>
        /// 菜单和工具栏的容器
        /// </summary>
        ToolStripPanel topStripPanel;

        /// <summary>
        /// 主菜单，从Main插件加载而来
        /// </summary>
        MenuStrip mainMenuStrip;

        /// <summary>
        /// 状态栏，公共
        /// </summary>
        StatusStrip mainStatusStrip;

        /// <summary>
        /// 活动区域的Dockpanel
        /// </summary>
        DockPanel mainDockPanel;

        /// <summary>
        /// 主Tree视图，从Main插件加载而来
        /// </summary>
        BaseForm mainTreeView;

        /// <summary>
        /// 主Tab视图，从Main插件加载而来
        /// </summary>
        BaseForm mainTabView;

        /// <summary>
        /// 主Info视图，从Main插件加载而来
        /// </summary>
        BaseForm mainInfoView;

        #endregion

        #region UI框架的属性

        public ToolStripPanel StripPanel
        {
            get
            {
                return topStripPanel;
            }
        }
        public MenuStrip MenuStrip
        {
            get
            {
                return mainMenuStrip;
            }

        }

        public DockPanel DockPanel
        {
            get
            {
                return mainDockPanel;
            }
        }

        public BaseForm TreeView
        {
            get
            {
                return mainTreeView;
            }
        }

        public BaseForm TabView
        {
            get
            {
                return mainTabView;
            }
        }

        public BaseForm InfoView
        {
            get
            {
                return mainInfoView;
            }

        }
        public StatusStrip StatusStrip
        {
            get
            {
                return mainStatusStrip;
            }
        }

        #endregion

        /// <summary>
        /// 主活动区域是否加载（剖分）
        /// </summary>
        private bool HasmainMutableResourceBuild = false;

        /// <summary>
        /// UI框架，自动构建并划分UI区域
        /// </summary>
        public MainUIFrame()
        {
            topStripPanel = new ToolStripPanel();  

            mainMenuStrip = new MenuStrip();

            mainStatusStrip = new StatusStrip();

            mainDockPanel = new DockPanel();

            topStripPanel.Dock = DockStyle.Top;

            mainDockPanel.Dock = DockStyle.Fill;

            mainStatusStrip.Dock = DockStyle.Bottom;

            topStripPanel.Orientation = Orientation.Horizontal;

            topStripPanel.Join(mainMenuStrip, 0);

            mainDockPanel.RightToLeftLayout = true;

        }
        
        public void BuildMainMutableResource(MutableResource mainResource)
        {
            if (HasmainMutableResourceBuild) //如果已经加载，则无需重复加载主插件
            {
                return;
            }

            this.mainTreeView = mainResource.ViewForm;
            this.mainTabView = mainResource.TabForm;
            this.mainInfoView = mainResource.InfoForm;

            mainTabView.Text = "TabView";
            mainInfoView.Text = "InfoView";
            mainTreeView.Text = "TreeView";

            mainTreeView.ShowHint = DockState.DockLeft;
            mainTreeView.DockAreas = DockAreas.DockLeft;
            mainTabView.ShowHint = DockState.Document;

            mainInfoView.ShowHint = DockState.DockBottom;

            mainTabView.Show(mainDockPanel);
            mainTreeView.Show(mainDockPanel);
            mainInfoView.Show(mainTabView.Pane, DockAlignment.Bottom, 0.3);

        }
    }
}