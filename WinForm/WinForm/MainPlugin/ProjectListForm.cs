using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MainPlugin
{
    public partial class ProjectListForm : Form
    {
        public string filename = null;
        public FileInfo[] fileinfo;
        public int index = 0;

        public ProjectListForm(FileInfo[] fileinfo)
        {
            InitializeComponent();
            this.fileinfo = fileinfo;
        }

        private void ProjectListForm_Load(object sender, EventArgs e)
        {
            foreach (FileInfo f in fileinfo)
            {
                string filetime = f.Name.Split('%')[1];
                filetime=filetime.Split('.')[0];
                projectlistBox.Items.Add(filetime);
            }
        }

        private void projectlistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            index = projectlistBox.SelectedIndex;
        }
    }
}
