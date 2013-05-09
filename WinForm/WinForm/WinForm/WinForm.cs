using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Platform.Core;
using Platform.Core.Services;
using Platform.Core.UI;
using System.Reflection;
using WeifenLuo.WinFormsUI.Docking;
using Platform.Core.Data;
namespace WinForm
{
    public partial class WinForm : Form
    {
        public WinForm(string[] args)
        {
            InitializeComponent();
            //SplashForm splash = new SplashForm(args);
            //splash.ShowDialog();
            Processing(args);
        }

        /// <summary>
        /// 处理平台请求
        /// </summary>
        /// <param name="args"></param>
        private void Processing(string[] args)
        {
            Runtime rt = new Runtime("Core.xml", this);

            if (args == null || args.Length == 0)
            {
                rt.DealRequst(this, new RuntimeEventArgs(RequstType.ExcutePlatform));
            }
            else
            {
                foreach (string path in args)
                {
                    try
                    {
                        rt.DealRequst(this, new RuntimeEventArgs(RequstType.OpenProject, path));
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void WinForm_Load(object sender, EventArgs e)
        {           
        }

        private void WinForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(ProjectManager.ProjectManagerSington!=null)
            {
                string projectuuid = null;
                projectuuid = ProjectManager.ProjectManagerSington.GetProjectUUID();
                if (projectuuid != null)
                {
                    AbstractProject currentproject = ProjectManager.ProjectManagerSington.GetCurrentProject();
                    //先询问是否保存工程
                    DialogResult dr = MessageBox.Show("是否保存工程文件？", "警告", MessageBoxButtons.YesNoCancel);//提示是否共享事件
                    if (dr == DialogResult.Yes)  //保存
                    {

                        //获取平台当前维护的工程
                        currentproject = ProjectManager.ProjectManagerSington.GetCurrentProject();
                        //保存工程的界面布局
                        PluginsManager.PluginsManagerSington.SaveMutableResource(projectuuid);
                        //保存工程文件
                        ServicesManager.ServicesManagerSingleton.ProjectService.SaveProject(currentproject);


                        ///保存成XML文档
                        AbstractProjectData currentprojectdata = null;

                        currentprojectdata = ProjectManager.ProjectManagerSington.GetCurrentProjectData();
                        currentprojectdata.GenerateProjectData();//先生成待序列化的数据

                        string filepath = null;
                        filepath = currentproject.Path + currentproject.Name + "\\" + currentproject.Name + ".xml";
                        ServicesManager.ServicesManagerSingleton.ProjectService.SavaAsXML(currentprojectdata, filepath);

                        //再关闭工程
                        ServicesManager.ServicesManagerSingleton.PluginsService.DealRequst(this, new RemoveProjectArgs("Main", projectuuid));
                    }
                    else if (dr == DialogResult.No)
                        //再关闭工程
                        ServicesManager.ServicesManagerSingleton.PluginsService.DealRequst(this, new RemoveProjectArgs("Main", projectuuid));
                    else if (dr == DialogResult.Cancel) //取消关闭
                        e.Cancel = true;
                    
                }
            }
        }
    }
}
