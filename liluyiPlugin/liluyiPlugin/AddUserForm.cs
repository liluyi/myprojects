using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Platform.Core.UI;
using Platform.Core.Services;
using Platform.Core;
using Platform.Core.Data;

namespace liluyiPlugin
{
    public partial class AddUserForm : BaseForm
    {
        public AddUserForm()
        {
            InitializeComponent();
        }

        private void subbutton_Click(object sender, EventArgs e)
        {
            liluyiProject project = new liluyiProject();

            AbstractProject currentproject = ProjectManager.ProjectManagerSington.GetProject(ProjectManager.ProjectManagerSington.GetProjectUUID());
            
            project = currentproject as liluyiProject;
            MessageBox.Show("已存在用户名：" + project.username + "\n密码：" + project.password);

            //MessageBox.Show(project.username );
            project.username = this.usernametextBox.Text;
            project.password = this.passwordBox.Text;

            liluyiInfoViewForm form=(liluyiInfoViewForm)ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(null,new UserUIEventArgs("InfoViewForm",null));
            form.outputLabel.Text = "用户名:" + project.username + "\n密码" + project.password;
        }
    }
}
