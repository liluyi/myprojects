using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Platform.Core.UI;
using WeifenLuo.WinFormsUI.Docking;
using Platform.Core;
using Platform.Core.Data;
using Platform.Core.Services;

namespace liluyiPlugin
{
    public partial class liluyiTreeViewForm : BaseForm
    {
        AddUserForm adduser = new AddUserForm();
        AddUserForm adduser2 = new AddUserForm();

        public liluyiTreeViewForm()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.Name=="AddUserNode")
            {
                //获取当前程序全部窗体
                //List<string> names=ServicesManager.ServicesManagerSingleton.UIService.GetAllFormNames(null, null);
                adduser.Text = treeView1.SelectedNode.Text;
                //查看是否已存在同名窗体
                AddUserForm form = (AddUserForm)ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(null, new UserUIEventArgs(adduser.Text,null));
                if (form == null)
                {
                    //将自定义窗体加载至平台，并传递位置参数
                    //PluginsManager.PluginsManagerSington.AddFormInMutableResource(adduser.Text, adduser);
                    //PluginsManager.PluginsManagerSington.
                    //废弃方法
                    ServicesManager.ServicesManagerSingleton.UIService.AddMutableResourceSelf(adduser, new UserUIEventArgs(adduser.Text,new FormLoc(DockState.Document)));//将用户自定义窗体加载至平台
                }
                else
                    ServicesManager.ServicesManagerSingleton.UIService.ShowForm(this, new UserUIEventArgs(form.Text));
            }
            if (treeView1.SelectedNode.Name == "SetAccessNode")
            {
                adduser2.Text = treeView1.SelectedNode.Text;
                AddUserForm form = (AddUserForm)ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(null, new UserUIEventArgs(adduser2.Text,null));//根据窗体名称返回平台所维护的窗体
                if (form == null)
                {
                    ServicesManager.ServicesManagerSingleton.UIService.AddMutableResourceSelf(adduser2, new UserUIEventArgs(adduser2.Text, new FormLoc(DockState.DockBottom)));//将用户自定义窗体加载至平台
                }
                else
                    ServicesManager.ServicesManagerSingleton.UIService.ShowForm(this,new UserUIEventArgs(form.Text));
            }
        }
    }
}
