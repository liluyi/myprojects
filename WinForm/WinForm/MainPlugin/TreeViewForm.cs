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

namespace MainPlugin
{
    public partial class TreeViewForm : BaseForm
    {
        public TreeViewForm()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TabViewForm tabview1 = new TabViewForm();
            TabViewForm tabview2 = new TabViewForm();
            if (treeView1.SelectedNode.Text == "Sub_1")
            {
                
                tabview1.Text = treeView1.SelectedNode.Text;
                tabview1.label1.Text = "information received"; 
                tabview1.ShowHint = DockState.DockLeft;
                tabview1.Show(DockPanel);
            }
            if (treeView1.SelectedNode.Text == "Sub_2")
            {
                
                tabview2.Text = treeView1.SelectedNode.Text;
                tabview2.label1.Text = "information received";
                tabview2.Show(DockPanel);
                tabview2.ShowHint = DockState.DockRight;
            }
        }
    }
}
