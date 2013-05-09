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
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Reflection;
using WeifenLuo.WinFormsUI.Docking;

namespace liluyiPlugin
{
   
    public class liluyiPlugin:IPlugin
    {
        
        private string token;
        private string projectsuffix = string.Empty;
        private string projectclassfullname = string.Empty;
        private string mutableresourcefullname = string.Empty;
        //private ICommonLogging Log;

        #region IPlugin Members

        public AbstractProject GetDefaultProject()
        {
            return new liluyiProject();
        }

        public AbstractProject GetProjectFromPath(string path)
        {
            liluyiProject project = new liluyiProject();
            return project;
        }

        public AbstractProjectData GetDefaultProjectData()
        {
            return new liluyiProjectData();
        }

        public AbstractProjectData GetProjectDataFromPath(string path)
        {
            liluyiProjectData projectdata = new liluyiProjectData();
            return projectdata;
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
        #endregion

        

        public liluyiPlugin()
        {
        }
    }

   // [System.Serializable]
    public class liluyiMutableResource : MutableResource
    {
        private BaseForm viewform = new liluyiTreeViewForm();
        private BaseForm tabform = new liluyiTabViewForm();
        private BaseForm infoform = new liluyiInfoViewForm();
        private BaseForm serverform = new liluyiServerViewForm();
        private ToolStripMenuItem[] menus;
        private ToolStrip[] tools;
        private AddUserForm adduserform1=new AddUserForm();
        private AddUserForm adduserform2=new AddUserForm();
        private Dictionary<string, Platform.Core.UI.BaseForm> toolformdictionary=new Dictionary<string,BaseForm>();
        private Dictionary<string, FormLoc> formlocationdictionary = new Dictionary<string, FormLoc>();

        
        public liluyiMutableResource()
        {
            //Log = ServicesManager.ServicesManagerSingleton.LoggingService.GetModuleLogging("CommonLog");
            menus = new ToolStripMenuItem[2];

            for (int i = 0; i < 2; i++)
            {
                menus[i] = new ToolStripMenuItem("自定义菜单" + i.ToString());
                for (int j = 0; j < 3; j++)
                {
                    menus[i].DropDownItems.Add("自定义子菜单" + i.ToString() + "_" + j.ToString());
                }
            }

            //建议平台启动时窗体，将所有需启动时显示的窗体加入词典中，包括平台提供的四个窗体
            viewform.Name = "TreeViewForm";//为窗体设置ID
            toolformdictionary.Add(viewform.Name, viewform);//将窗体加入平台窗体词典中，以ID为键
            formlocationdictionary.Add(viewform.Name, new FormLoc(DockState.DockLeft));//将窗体位置加入平台窗体位置词典中，以ID为键

            tabform.Name = "TabViewFrom";
            toolformdictionary.Add(tabform.Name, tabform);
            formlocationdictionary.Add(tabform.Name, new FormLoc(DockState.Document));

            infoform.Name = "InfoViewForm";
            toolformdictionary.Add(infoform.Name, infoform);
            formlocationdictionary.Add(infoform.Name, new FormLoc("TabViewForm", DockAlignment.Bottom, 0.3));

            serverform.Name = "ServerViewForm";
            toolformdictionary.Add(serverform.Name, serverform);
            formlocationdictionary.Add(serverform.Name, new FormLoc(DockState.DockLeftAutoHide));
            //添加工具自定义启动时窗体及其位置
            adduserform1.Name = "AddUserFormIni";
            toolformdictionary.Add(adduserform1.Name, adduserform1);
            formlocationdictionary.Add(adduserform1.Name, new FormLoc(DockState.DockRight));
            //不设置自定义窗体位置时默认显示于文档区
            adduserform2.Name = "AddUserFormIni2";
            toolformdictionary.Add(adduserform2.Name, adduserform2);
            formlocationdictionary.Add(adduserform2.Name, new FormLoc(adduserform1.Name,DockAlignment.Bottom,0.5));

            //Log.Info("liluyiPlugin菜单加载中！");

            //tools = new ToolStrip[1];
            //    tools[1] = new ToolStrip();
            //        tools[1].Items.Add(Properties.Resources.Temp);
            //            tools[1].Items[1].Click += new EventHandler(Tool_Click);
        }
        #region MutableResource

        public override Dictionary<string, Platform.Core.UI.BaseForm> ToolFormDictionary
        {
            get { return toolformdictionary; }
        }

        public override Dictionary<string, FormLoc> FormLocationDictionary
        {
            get { return formlocationdictionary; }
        }

        public override BaseForm ViewForm
        {
            get 
            { 
                return viewform; 
            }
            set
            {

            }
        }

        public override BaseForm TabForm
        {
            get 
            {
                return tabform;
            }
            set
            {

            }
        }

        public override BaseForm InfoForm
        {
            get 
            {
                return infoform;
            }
            set
            {

            }
        }

        public override BaseForm ServerForm
        {
            get
            {
                return serverform;
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
        #endregion
    }

    [System.Serializable]
    public class liluyiProject : AbstractProject
    {
        public liluyiProject()
        {
        }
        public override string Suffix
        {
            get { return "liluyi"; }
        }
        [XmlIgnore]
        public int liluyiform = 10;
        public string username=null;
        public string password=null;
        public int liluyiformend = 1;
    }

    [System.Serializable]
    public class liluyiProjectData : AbstractProjectData
    {
        
        public int nodenum=0;
        public string username = "jdslfjdsla;gh";
        public string password = "pwpwpwpwpw ";

        public liluyiProjectData() { }
        
        public override string Suffix
        {
            get { return "liluyi"; }
        }
        public int NodeNum
        {
            get { return nodenum; }
            set { nodenum = value; }
        }
        public string UserName
        {
            get { return username; }
            set { username = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}