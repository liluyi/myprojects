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

namespace FirstPlugin
{
    public class FirstPlugin:IPlugin
    {
        private string projectsuffix = string.Empty;
        private string projectclassfullname = string.Empty;
        private string mutableresourcefullname = string.Empty;
        private System.Windows.Forms.ToolStripMenuItem[] menus;
        private System.Windows.Forms.ToolStrip[] tools;
        private string token;
        private ICommonLogging Log;

        #region IPlugin Members

        public System.Windows.Forms.ToolStripMenuItem[] PluginMenus
        {
            get { return menus; }
        }

        public System.Windows.Forms.ToolStrip[] PluginTools
        {
            get { return tools; }
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
            return new FirstProject();
        }

        public Platform.Core.Data.AbstractProject GetDefaultProject()
        {
            return new FirstProject();
        }

        #endregion

        public FirstPlugin()
        {
            Log = ServicesManager.ServicesManagerSingleton.LoggingService.GetModuleLogging("CommonLog");

            menus = new ToolStripMenuItem[2];

            for (int i = 0; i < 2; i++)
            {
                menus[i] = new ToolStripMenuItem("FirstMenu_" + i.ToString());
                for (int j = 0; j < 3; j++)
                {
                    menus[i].DropDownItems.Add("SubMenu_" + i.ToString() + "_" + j.ToString());
                }
            }
            Log.Info("菜单加载");
            tools = new ToolStrip[2];

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
            }
        }
        private void Tool_Click(object sender, EventArgs args)
        {
            MessageBox.Show("FirstPlugin响应操作！");
        }

        
    }

    public class FirstMutableResource : MutableResource
    {
        private BaseForm viewform = new FirstTreeView();
        private BaseForm tabform = new FirstTabView();
        private BaseForm infoform = new FirstInfoView();
        private BaseForm serverform = new FirstServerView();


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


    public class FirstProject : AbstractProject
    {
        public override string Suffix
        {
            get { return "first"; }
        }
        public string file = "工具需要保存的数据";
        
    }
}
