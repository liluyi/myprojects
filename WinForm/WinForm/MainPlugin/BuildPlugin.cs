using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Platform.Core;
namespace MainPlugin
{
    public partial class BuildPlugin : Form
    {
        public BuildPlugin(List<PluginInfo> infos)
        {
            InitializeComponent();
            int i = 0;
            foreach (PluginInfo info in infos)
            {
                i++;
                ListViewItem item=new ListViewItem(new string[] {i.ToString(),info.Author,info.Type.ToString(),info.Description});
                this.listView1.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.pathBox.Text == null)
            {
                MessageBox.Show("请输入保存路径！");
                return;
            }
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
            if (this.listView1.FocusedItem != null )
            {
                try
                {
                    index = int.Parse(this.listView1.FocusedItem.SubItems[0].Text) - 1;
                }
                catch
                {
                    index = -1;
                    return;
                }
            }
            buildproject = true;
            this.Close();
            
        }
        public int index = 0;
        public string projectname;
        public string projectpath;
        public bool buildproject=false;

        private void browsebutton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browsedialog = new FolderBrowserDialog();
            browsedialog.RootFolder = Environment.SpecialFolder.MyComputer;

            if (browsedialog.ShowDialog() == DialogResult.OK)
                pathBox.Text = browsedialog.SelectedPath;
        }
    }
}
