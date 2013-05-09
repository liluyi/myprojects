using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Platform.Core;
using Platform.Core.UI;
using Platform.Core.Services;
using Platform.Core.Data;
using WeifenLuo.WinFormsUI.Docking;
using System.Xml;
using System.IO;

namespace MainPlugin
{
    public partial class CreateProjectForm : Form
    {
        public CreateProjectForm()
        {
            InitializeComponent();
        }

        private void browsebutton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browsedialog = new FolderBrowserDialog();
            browsedialog.ShowNewFolderButton = true;
            browsedialog.RootFolder = Environment.SpecialFolder.MyComputer;

            if (browsedialog.ShowDialog() == DialogResult.OK)
                pathBox.Text = browsedialog.SelectedPath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.pathBox.Text == null)
            {
                MessageBox.Show("请输入保存路径！");
                return;
            }
            else if(!Directory.Exists(this.pathBox.Text))
            {
                MessageBox.Show("保存路径不存在！");
                return;
            }
            //else if (this.pathBox.Text.Contains("C:\\"))
            //{
            //    MessageBox.Show("没有权限保存在此路径!");
            //    return;
            //}
            else
                projectpath = this.pathBox.Text;

            if (this.nameBox.Text == null || this.nameBox.Text == "")
            {
                MessageBox.Show("工程名称不能为空！");
                return;
            }
            else
            {
                projectname = this.nameBox.Text;
            }
            //if (this.listView1.FocusedItem != null)
            //{
            //    try
            //    {
                    //index = int.Parse(this.listView1.FocusedItem.SubItems[0].Text) - 1;
            //    }
            //    catch
            //    {
            //        index = -1;
            //        return;
            //    }
            //}
            List<PluginInfo> plugininfos = PluginsManager.PluginsManagerSington.GetAllPluginInfo();
            if (plugininfos.Count > 0)
            {
                for (int i = 0; i < plugininfos.Count; i++)
                    if (!plugininfos[i].Equals("main") && !plugininfos[i].Equals("Main"))
                        token = plugininfos[i].Token;
            }
            string filepath = projectpath + "\\" + projectname + "." + token;
            if (File.Exists(filepath))
            {
                DialogResult dr=MessageBox.Show("目录下存在同名文件，是否覆盖？", "警告", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No)
                    return;
                else
                {
                    if (Directory.Exists(projectpath + "\\" + projectname)) 
                        Directory.Delete(projectpath + "\\" + projectname, true);
                    buildproject = true;
                    this.Close();
                }
            }
            else
            {
                buildproject = true;
                this.Close();
            }

        }
        public int index = 0;
        public string token = null;
        public string projectname;
        public string projectpath;
        public bool buildproject = false;

        private void serverConfig_Click(object sender, EventArgs e)
        {
            string toolname = string.Empty;
            string projectid = string.Empty;
            foreach (PluginInfo info in PluginsManager.PluginsManagerSington.GetAllPluginInfo())
            {
                if (!info.Token.Equals("Main"))
                    toolname = info.Token;//获取当前CASREE平台运行的工具token
            }
            if (toolname.Equals(string.Empty))
            {
            }
            else
            {
                AbstractProject currentprj = ProjectManager.ProjectManagerSington.GetCurrentProject();
                if (currentprj == null)
                    projectid = string.Empty;
                else
                    projectid = currentprj.Name;//////注意，此处传送的为工程名称
                AuthenticationForm authform = new AuthenticationForm(toolname,projectid);
                if (authform.ShowDialog() == DialogResult.OK)//确定连接
                {
                    //authform.isConnected = true;
                    if (authform.isConnected == "allow")
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load("ServerDatabase.xml");
                        XmlNode xn = doc.SelectSingleNode("/Solutions");
                        XmlNodeList solutionlist = xn.ChildNodes;
                        ProgramListForm form = new ProgramListForm(solutionlist);
                        if (form.ShowDialog() == DialogResult.OK)
                            MessageBox.Show(form.programname);//获取选择的服务器项目名称
                    }
                }
            }
        }

        private void cancelbutton_Click(object sender, EventArgs e)
        {
            this.buildproject = false;
        }
    }
}
