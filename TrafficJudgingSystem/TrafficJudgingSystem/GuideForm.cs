using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TrafficJudgingSystem
{
    public partial class GuideForm : Form
    {
        public GuideForm()
        {
            InitializeComponent();
        }

        private void QuitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        private void BuildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InformationInputForm inputform = new InformationInputForm();
            inputform.Show();
            //this.Hide();
        }

        private void GuideForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }
    }
}
