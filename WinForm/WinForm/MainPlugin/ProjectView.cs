using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Platform.Core;
using Platform.Core.Data;
using Platform.Core.Services;
namespace MainPlugin
{
    public partial class ProjectView : Form
    {
        public ProjectView()
        {
            InitializeComponent();
        }

        private void UpdateProjectView_Click(object sender, EventArgs e)
        {
            listProjectView.Items.Clear();

            List<AbstractProject> projects = ProjectManager.ProjectManagerSington.GetAllProjects();

            foreach (AbstractProject p in projects)
            {
                listProjectView.Items.Add(new ListViewItem(new string[] { p.Name, p.PluginUUID, p.UUID }));
            }

            listProjectView.Update();
        }

        private void RemoveProject_Click(object sender, EventArgs e)
        {
            if (listProjectView.FocusedItem != null)
            {
                string projectuuid = listProjectView.FocusedItem.SubItems[2].Text.ToString();

                ServicesManager.ServicesManagerSingleton.PluginsService.DealRequst(this,new RemoveProjectArgs("Main",projectuuid));

                UpdateProjectView_Click(null, null);
            }
        }
    }
}
