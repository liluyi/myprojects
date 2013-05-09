using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Platform.Core.Data;

namespace MainPlugin
{
    public partial class SaveProject : Form
    {
        string filepath = null;
        AbstractProject currentproject=null;
        public SaveProject(string filepath,AbstractProject project)
        {
            InitializeComponent();
            this.filepath = filepath;
            this.currentproject = project;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            filepath = filepath + this.textBox1.Text;
            Platform.Core.ServicesManager.ServicesManagerSingleton.ProjectService.SavaAsProject(currentproject, filepath);
            this.Close();
        }
    }
}
