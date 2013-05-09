using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SFTAPlugin
{
    public partial class ElectForm : Form
    {
        public string r, n;
        public ElectForm()
        {
            InitializeComponent();
        }

        private void okbutton_Click(object sender, EventArgs e)
        {
            //未进行类型检测
            r = textBox1.Text;
            n = textBox2.Text;
            try
            {
                int rvalue = int.Parse(r);
                int nvalue = int.Parse(n);
            }
            catch(FormatException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            
            this.DialogResult = DialogResult.Yes;
        }

        private void cancelbutton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
    }
}
