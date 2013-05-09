using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using WeifenLuo.WinFormsUI.Docking;
using Platform.Core.UI;
using Platform.Core.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Platform.Core.Services
{
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

        /// <summary>
        /// 主Server视图，从Main插件加载而来
        /// </summary>
        BaseForm mainServerView;

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
        public BaseForm ServerView
        {
            get
            {
                return mainServerView;
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
        private bool HasMutableResourceBuild = false;
        private DeserializeDockContent ddc;//全局变量
        private MutableResource resource;
        List<FormInfo> uiinflist = new List<FormInfo>();

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

            mainDockPanel.Dock = DockStyle.Fill;//填充整个窗体
            mainDockPanel.DocumentStyle = DocumentStyle.DockingMdi;

            mainStatusStrip.Dock = DockStyle.Bottom;//将状态栏置于底部
            //mainStatusStrip.BackColor = Color.MediumSlateBlue;

            topStripPanel.Orientation = Orientation.Horizontal;//水平放置空间
            //topStripPanel.BackColor = Color.MidnightBlue;

            topStripPanel.Join(mainMenuStrip, 0);

            mainDockPanel.RightToLeftLayout = true;
            mainDockPanel.DockRightPortion = 0.2;//右侧停靠比例为0.2
            mainDockPanel.DockLeftPortion = 0.15;//左侧停靠比例为0.2
            this.
            mainMenuStrip.AllowMerge = true;
        }

        /// <summary>
        /// 读取uilist中程序上次关闭时全部窗体类型，并与config配置文件中程序上次关闭时全部窗体实例相对应添加
        /// </summary>
        /// <param name="persistString"></param>
        /// <returns></returns>
        private IDockContent GetContentFromPersistString(string persistString)//MutableResource Resource)
        {
            //反序列化出的UI类型存储列表
            
            //
            foreach (FormInfo forminfo in uiinflist)
            {
                Type type = forminfo.FormType;
                string t = type.ToString();
                if (persistString == t)
                {
                    BaseForm bf = type.Assembly.CreateInstance(t) as BaseForm;
                    bf.Text = forminfo.FormText;
                    bf.Name = forminfo.FormID;
                    //将窗体加入到当前进行词典中
                    this.resource.ToolFormDictionary.Add(bf.Name, bf);
                    //移除当前信息，因为可能还有同类型的窗体需要继续轮询，如果不移除将会出现词典中出现同键信息导致异常
                    uiinflist.Remove(forminfo);
                    //ServicesManager.ServicesManagerSingleton.UIService.
                    //ServicesManager.ServicesManagerSingleton.UIService.AddMutableResourceSelf(this, new UserUIEventArgs(t, DockState.Hidden));
                    return bf;
                }
            }


            return null;
        }

        //加载各视图面板
        public void BuildMutableResource(MutableResource Resource)
        {
            if (HasMutableResourceBuild) //如果已经加载，则无需重复加载主插件
            {
                return;
            }
            this.resource = Resource;
            ddc = new DeserializeDockContent(GetContentFromPersistString); // 放在后面的话会出错。
            //窗体配置文件的路径
            string proname = ProjectManager.ProjectManagerSington.GetCurrentProject().Name;
            string filename = proname + "\\" + proname + ".config";
            string configFile = Path.Combine(ProjectManager.ProjectManagerSington.GetCurrentProject().Path, filename);
            

            if (File.Exists(configFile))
            {
                //首先清空工具的初始化界面词典信息
                this.resource.ToolFormDictionary.Clear();
                this.resource.FormLocationDictionary.Clear();
                //配置文件存储地址
                //string proname = ProjectManager.ProjectManagerSington.GetCurrentProject().Name;//当前工程名
                string filepath = proname + "\\" + proname + ".uiinflist";
                string path = Path.Combine(ProjectManager.ProjectManagerSington.GetCurrentProject().Path, filepath);//工程上次关闭时uilist配置文件存储位置
                //反序列化配置文件
                FileStream fileStream = new FileStream(path, FileMode.Open);//
                BinaryFormatter b = new BinaryFormatter();
                uiinflist = b.Deserialize(fileStream) as List<FormInfo>;
                fileStream.Close();
                //如果配置文件存在，就调用该函数，读取配置文件信息
                mainDockPanel.LoadFromXml(configFile, ddc);
            }
            else
            {
                ////初始化工具的启动窗体
                //this.mainTreeView = Resource.ViewForm;
                //this.mainTabView = Resource.TabForm;
                //this.mainInfoView = Resource.InfoForm;
                //this.mainServerView = Resource.ServerForm;

                //mainTabView.Text = "主功能面板";
                //mainInfoView.Text = "输出信息";
                //mainTreeView.Text = "功能列表";
                //mainServerView.Text = "服务器面板";

                //mainTreeView.ShowHint = DockState.DockLeft;//树状列表至于左侧
                //mainTabView.ShowHint = DockState.Document;//功能面板置于右侧
                //mainServerView.ShowHint = DockState.DockLeftAutoHide;//服务器面板自动隐藏
                //mainInfoView.ShowHint = DockState.DockBottom;//输出信息面板置于地侧

                //mainTabView.Show(mainDockPanel);
                //mainTreeView.Show(mainDockPanel);
                //mainServerView.Show(mainDockPanel);
                //mainInfoView.Show(mainTabView.Pane, DockAlignment.Bottom, 0.3);

                //显示工具启动词典中
                foreach (KeyValuePair<string, BaseForm> pair in Resource.ToolFormDictionary)
                {
                    FormLoc location = null;
                    Resource.FormLocationDictionary.TryGetValue(pair.Key, out location);
                 

                    if (location.State != DockState.Unknown)//采用dockstate参数
                    {
                        pair.Value.ShowHint = location.State;
                        pair.Value.Show(mainDockPanel);
                    }
                    else if (!location.PreviousPaneName.Equals(String.Empty))//采用PrePane+Alignment+Proportion参数
                    {
                        BaseForm preform = ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(this, new UserUIEventArgs(location.PreviousPaneName, null));
                        pair.Value.Show(preform.Pane, location.Alignment, location.Proportion);
                    }
                    else
                    {//无参数
                        pair.Value.ShowHint = DockState.Document;
                        pair.Value.Show(mainDockPanel);
                    }
                }
            }
        }
    }
}
