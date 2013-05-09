using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Platform.Core.Data;
using Platform.Core.UI;
using WeifenLuo.WinFormsUI.Docking;
using Platform.Core;
using Platform.Core.Services;

namespace FirstPlugin
{
    public partial class FirstTreeView : BaseForm
    {
        public FirstTreeView()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            BaseForm adduser = new BaseForm();

            if (treeView1.SelectedNode.Name == "节点0")
            {
                adduser.Text = treeView1.SelectedNode.Text;
                //ServicesManager.ServicesManagerSingleton.UIService
            }
            if (treeView1.SelectedNode.Name == "SetAccessNode")
            {
            }
        }
    }
}
