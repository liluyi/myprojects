using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Platform.Core;
using Platform.Core.UI;
using Platform.Core.Data;
using Platform.Core.Services;
using Lassalle.Flow;

namespace SratPlugin
{
    /// <summary>
    /// plugin初始化，继承自IPlugin
    /// </summary>
    /// <param></param>
    /// <date>2011.11.11</date>>
    /// <returns></returns>
    public class SratPlugin:IPlugin
    {
        private string projectsuffix = string.Empty;
        private string projectclassfullname = string.Empty;
        private string mutableresourcefullname = string.Empty;
        private string token;
        private ICommonLogging Log;

        #region IPlugin Members



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

        public string MutableResourceClassFullName
        {
            get
            {
                return mutableresourcefullname;
            }
            set
            {
                mutableresourcefullname = value;
            }
        }

        public string ProjectClassFullName
        {
            get
            {
                return projectclassfullname;
            }
            set
            {
                projectclassfullname = value;
            }
        }

        public string ProjectSuffix
        {
            get
            {
                return projectsuffix;
            }
            set
            {
                projectsuffix = value;
            }
        }

        public Platform.Core.Data.AbstractProject GetProjectFromPath(string path)
        {
            return new SratProject();
        }

        public Platform.Core.Data.AbstractProject GetDefaultProject()
        {
            return new SratProject();
        }

        #endregion

        public SratPlugin()
        {
            Log = ServicesManager.ServicesManagerSingleton.LoggingService.GetModuleLogging("CommonLog");

            /*menus = new ToolStripMenuItem[2];

            for (int i = 0; i < 2; i++)
            {
                menus[i] = new ToolStripMenuItem("FirstMenu_" + i.ToString());
                for (int j = 0; j < 3; j++)
                {
                    menus[i].DropDownItems.Add("SubMenu_" + i.ToString() + "_" + j.ToString());
                }
            }*/
            Log.Info("菜单加载");
            /*tools = new ToolStrip[2];

            for (int i = 0; i < 2; i++)
            {
                tools[i] = new ToolStrip();
                for (int j = 0; j < 2; j++)
                {
                    tools[i].Items.Add("CommonLog");
                    
                    //if (j > 3)
                    {
                        tools[i].Items[j].Click += new EventHandler(Tool_Click);
                    }
                }
            }*/
            
        }

    }
    /// <summary>
    /// 加载的窗体
    /// </summary>
    /// <param></param>
    /// <date>2011.11.11</date>>
    /// <returns></returns>
    public class SratMutableResource : MutableResource
    {
        private System.Windows.Forms.ToolStripMenuItem[] menus;
        private System.Windows.Forms.ToolStrip[] tools;
        private string token;

        private BaseForm viewform = new SratTreeView();
        private BaseForm tabform = new SratTabView();
        private BaseForm infoform = new SratInfoView();
        private BaseForm serverform = new SratServerView();

        public SratMutableResource()
        {
            menus = new ToolStripMenuItem[3];
            menus[0] = new ToolStripMenuItem("编辑");
            menus[0].DropDownItems.Add("添加子系统");
            menus[0].DropDownItems.Add("添加模块");
            menus[0].DropDownItems.Add("添加分配方法");
            menus[0].DropDownItems.Add("添加系统结构图");
            menus[0].DropDownItems.Add("更新系统结构图");
            menus[0].DropDownItems.Add("执行当前分配任务");
            menus[0].DropDownItems.Add("执行所有可用分配任务");

            menus[1] = new ToolStripMenuItem("工具");
            menus[1].DropDownItems.Add("服务器推送数据...");
            menus[2] = new ToolStripMenuItem("帮助");
            menus[2].DropDownItems.Add("使用说明");
            menus[2].DropDownItems.Add("关于...");
        }
        public override System.Windows.Forms.ToolStripMenuItem[] PluginMenus
        {
            get { return menus; }
        }

        public override System.Windows.Forms.ToolStrip[] PluginTools
        {
            get { return tools; }
        }
        

        public void setTag()
        {
            viewform.Tag = "";
            tabform.Tag = "";
            infoform.Tag = "";
            serverform.Tag = "";
        }
        public override Platform.Core.UI.BaseForm ViewForm
        {
            get
            {
                return viewform;
            }
            set
            {
                viewform = value;
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
                tabform = value;
            }
        }

        public override Platform.Core.UI.BaseForm InfoForm
        {
            get
            {
                return infoform;
            }
            set
            {
                infoform = value;
            }
        }
        public override Platform.Core.UI.BaseForm ServerForm
        {
            get
            {
                return serverform;
            }
            set
            {
                infoform = value;
            }
        }
    }
    /// <summary>
    /// 虚工程，可序列化
    /// </summary>
    /// <param></param>
    /// <date>2011.11.11</date>>
    /// <returns></returns>
    [Serializable]
    public class SratProject : AbstractProject
    {
        public override string Suffix
        {
            get { return "Srat"; }
        }
        public string file = "工具需要保存的数据";

        //public List<TreeNodeInfo> mTreeNodeList = new List<TreeNodeInfo>();

        //public List<Node> mFlowNodeList = new List<Node>();
        public TreeInfoCollection mTreeCollection = new TreeInfoCollection();
    }
}
