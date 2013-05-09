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

        #endregion

        private ToolStripMenuItem[] menus;
        private ToolStrip[] tools;
        private string token;
        private ProjectView view = new ProjectView();
        public SamplePlugin()
        {
            menus = new ToolStripMenuItem[1];

            menus[0] = new ToolStripMenuItem("MainMenu");
            menus[0].DropDownItems.Add("获取插件信息");

            menus[0].DropDownItems[0].Click += new EventHandler(SamplePlugin_Click);

            menus[0].DropDownItems.Add("新建工程");
            menus[0].DropDownItems[1].Click += new EventHandler(BuildPlugin_Click);


            tools = new ToolStrip[1];

            tools[0] = new ToolStrip();
            tools[0].Items.Add("ProjectView");
            tools[0].Items[0].Click += new EventHandler(ProjectView_Click);
            
            
        }

        void SamplePlugin_Click(object sender, EventArgs e)
        {
            List<PluginInfo> plugininfos = PluginsManager.PluginsManagerSington.GetAllPluginInfo();
            foreach (PluginInfo info in plugininfos)
            {
                MessageBox.Show(info.ToString());
            }
        }
        void ProjectView_Click(object sender, EventArgs e)
        {
            view.ShowDialog();
        }
        void BuildPlugin_Click(object sender, EventArgs e)
        {
            List<PluginInfo> plugininfos = PluginsManager.PluginsManagerSington.GetAllPluginInfo();

            BuildPlugin form = new BuildPlugin(plugininfos);

            form.ShowDialog();
            if(form.index>=0 && plugininfos.Count>0)
            {
                string token = plugininfos[form.index].Token;

                ServicesManager.ServicesManagerSingleton.PluginsService.DealRequst(null,new BuildProjectArgs(this.token,token,form.projectname,null));
            }
        }

        private void Tool_Click(object sender, EventArgs args)
        {
            MessageBox.Show("MainPlugin响应操作！");
        }
        
    }
    public class MainMutableResource : MutableResource
    {
        public MainMutableResource()
        {
            ViewForm.Click+=new EventHandler(ViewForm_Click);
        }

        void  ViewForm_Click(object sender, EventArgs e)
        {
 	        TabForm.Text="sdfjsdfjdsfjdjf";
        }

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
    }
}
