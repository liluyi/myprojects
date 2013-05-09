using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace MainPlugin
{
    
    public partial class ProgramListForm : Form
    {
        public string programname = "DefaultSolution";

        public ProgramListForm(XmlNodeList nodelist)
        {
            InitializeComponent(nodelist);
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //测试用，扩展为从服务器端动态获取项目名称列表
            programname = listBox1.SelectedItem.ToString();
            
        }

        private void ProgramListForm_Load(object sender, EventArgs e)
        {

        }
    }
}
