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
    public partial class JudgementForm : Form
    {
        List<string> metriclist = new List<string>();
        public JudgementForm()
        {
            InitializeComponent();
            checkboxlist.Add(checkBox111);
            checkboxlist.Add(checkBox112);
            checkboxlist.Add(checkBox113);
            checkboxlist.Add(checkBox121);
            checkboxlist.Add(checkBox122);
            checkboxlist.Add(checkBox123);
            checkboxlist.Add(checkBox131);
            checkboxlist.Add(checkBox132);
            checkboxlist.Add(checkBox133);
            checkboxlist.Add(checkBox21);
            checkboxlist.Add(checkBox22);
            checkboxlist.Add(checkBox23);
            checkboxlist.Add(checkBox31);
            checkboxlist.Add(checkBox32);
            checkboxlist.Add(checkBox33);
        }

        private void nextbutton_Click(object sender, EventArgs e)
        {
            metriclist.Clear();
            foreach (CheckBox cb in this.checkboxlist)
            {
                if (cb.Checked == true)
                    metriclist.Add(cb.Name);
            }
            Program.finallist.infolist.Clear();
            DataImportForm datainputform = new DataImportForm(metriclist);
            datainputform.Show();
        }
    }
}
