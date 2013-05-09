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
            if (this.textBox.Text == null || this.textBox.Text == "")
            {
                MessageBox.Show("输入工程名称");
                return;
            }
            else
            {
                projectname = this.textBox.Text;
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
            this.Close();
            
        }
        public int index = 0;
        public string projectname;
    }
}
