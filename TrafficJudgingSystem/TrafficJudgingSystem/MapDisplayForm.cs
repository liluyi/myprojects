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
    public partial class MapDisplayForm : Form
    {
        public List<JudgeInfo> judgeinfolist = new List<JudgeInfo>();
        public List<Button> buttonlist = new List<Button>();
        public MapDisplayForm(List<JudgeInfo> judgelist)
        {
            InitializeComponent();
            this.judgeinfolist = judgelist;
            foreach (JudgeInfo ji in judgeinfolist)
            {
                if (ji.RouteSection.Equals("西直门-大钟寺"))
                {
                    if (ji.J0 == 1)
                        button1.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button1.BackColor = Color.Red;
                }
                else if (ji.RouteSection.Equals("大钟寺-西直门"))
                {
                    if (ji.J0 == 1)
                        button1.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button1.BackColor = Color.Red;
                }
                if (ji.RouteSection.Equals("大钟寺-知春路"))
                {
                    if (ji.J0 == 1)
                        button2.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button2.BackColor = Color.Red;
                }
                else if (ji.RouteSection.Equals("知春路-大钟寺"))
                {
                    if (ji.J0 == 1)
                        button2.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button2.BackColor = Color.Red;
                }
                if (ji.RouteSection.Equals("知春路-五道口"))
                {
                    if (ji.J0 == 1)
                        button3.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button3.BackColor = Color.Red;
                }
                else if (ji.RouteSection.Equals("五道口-知春路"))
                {
                    if (ji.J0 == 1)
                        button3.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button3.BackColor = Color.Red;
                }
                if (ji.RouteSection.Equals("五道口-上地"))
                {
                    if (ji.J0 == 1)
                        button4.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button4.BackColor = Color.Red;
                }
                else if (ji.RouteSection.Equals("上地-五道口"))
                {
                    if (ji.J0 == 1)
                        button4.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button4.BackColor = Color.Red;
                }
                if (ji.RouteSection.Equals("上地-西二旗"))
                {
                    if (ji.J0 == 1)
                        button5.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button5.BackColor = Color.Red;
                }
                else if (ji.RouteSection.Equals("西二旗-上地"))
                {
                    if (ji.J0 == 1)
                        button5.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button5.BackColor = Color.Red;
                }
                if (ji.RouteSection.Equals("西二旗-龙泽"))
                {
                    if (ji.J0 == 1)
                        button6.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button6.BackColor = Color.Red;
                }
                else if (ji.RouteSection.Equals("龙泽-西二旗"))
                {
                    if (ji.J0 == 1)
                        button6.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button6.BackColor = Color.Red;
                }
                if (ji.RouteSection.Equals("龙泽-回龙观"))
                {
                    if (ji.J0 == 1)
                        button7.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button7.BackColor = Color.Red;
                }
                else if (ji.RouteSection.Equals("回龙观-龙泽"))
                {
                    if (ji.J0 == 1)
                        button7.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button7.BackColor = Color.Red;
                }
                if (ji.RouteSection.Equals("回龙观-霍营"))
                {
                    if (ji.J0 == 1)
                        button8.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button8.BackColor = Color.Red;
                }
                else if (ji.RouteSection.Equals("霍营-回龙观"))
                {
                    if (ji.J0 == 1)
                        button8.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button8.BackColor = Color.Red;
                }
                if (ji.RouteSection.Equals("霍营-立水桥"))
                {
                    if (ji.J0 == 1)
                        button9.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button9.BackColor = Color.Red;
                }
                else if (ji.RouteSection.Equals("立水桥-霍营"))
                {
                    if (ji.J0 == 1)
                        button9.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button9.BackColor = Color.Red;
                }
                if (ji.RouteSection.Equals("立水桥-北苑"))
                {
                    if (ji.J0 == 1)
                        button10.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button10.BackColor = Color.Red;
                }
                else if (ji.RouteSection.Equals("北苑-立水桥"))
                {
                    if (ji.J0 == 1)
                        button10.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button10.BackColor = Color.Red;
                }
                if (ji.RouteSection.Equals("北苑-望京西"))
                {
                    if (ji.J0 == 1)
                        button11.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button11.BackColor = Color.Red;
                }
                else if (ji.RouteSection.Equals("望京西-北苑"))
                {
                    if (ji.J0 == 1)
                        button11.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button11.BackColor = Color.Red;
                }
                if (ji.RouteSection.Equals("望京西-芍药居"))
                {
                    if (ji.J0 == 1)
                        button12.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button12.BackColor = Color.Red;
                }
                else if (ji.RouteSection.Equals("芍药居-望京西"))
                {
                    if (ji.J0 == 1)
                        button12.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button12.BackColor = Color.Red;
                }
                if (ji.RouteSection.Equals("芍药居-光熙门"))
                {
                    if (ji.J0 == 1)
                        button13.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button13.BackColor = Color.Red;
                }
                else if (ji.RouteSection.Equals("光熙门-芍药居"))
                {
                    if (ji.J0 == 1)
                        button13.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button13.BackColor = Color.Red;
                }
                if (ji.RouteSection.Equals("光熙门-柳芳"))
                {
                    if (ji.J0 == 1)
                        button14.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button14.BackColor = Color.Red;
                }
                else if (ji.RouteSection.Equals("柳芳-光熙门"))
                {
                    if (ji.J0 == 1)
                        button14.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button14.BackColor = Color.Red;
                }
                if (ji.RouteSection.Equals("柳芳-东直门"))
                {
                    if (ji.J0 == 1)
                        button15.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button15.BackColor = Color.Red;
                }
                else if (ji.RouteSection.Equals("东直门-柳芳"))
                {
                    if (ji.J0 == 1)
                        button15.BackColor = Color.Blue;
                    else if (ji.J0 == 2)
                        button15.BackColor = Color.Red;
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (JudgeInfo ji in judgeinfolist)
            {
                if(ji.RouteSection.Equals("西直门-大钟寺"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
                else if (ji.RouteSection.Equals("大钟寺-西直门"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (JudgeInfo ji in judgeinfolist)
            {
                if (ji.RouteSection.Equals("大钟寺-知春路"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
                else if (ji.RouteSection.Equals("知春路-大钟寺"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (JudgeInfo ji in judgeinfolist)
            {
                if (ji.RouteSection.Equals("知春路-五道口"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
                else if (ji.RouteSection.Equals("五道口-知春路"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (JudgeInfo ji in judgeinfolist)
            {
                if (ji.RouteSection.Equals("五道口-上地"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
                else if (ji.RouteSection.Equals("上地-五道口"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (JudgeInfo ji in judgeinfolist)
            {
                if (ji.RouteSection.Equals("上地-西二旗"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
                else if (ji.RouteSection.Equals("西二旗-上地"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            foreach (JudgeInfo ji in judgeinfolist)
            {
                if (ji.RouteSection.Equals("西二旗-龙泽"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
                else if (ji.RouteSection.Equals("龙泽-西二旗"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            foreach (JudgeInfo ji in judgeinfolist)
            {
                if (ji.RouteSection.Equals("龙泽-回龙观"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
                else if (ji.RouteSection.Equals("回龙观-龙泽"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            foreach (JudgeInfo ji in judgeinfolist)
            {
                if (ji.RouteSection.Equals("回龙观-霍营"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
                else if (ji.RouteSection.Equals("霍营-回龙观"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            foreach (JudgeInfo ji in judgeinfolist)
            {
                if (ji.RouteSection.Equals("霍营-立水桥"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
                else if (ji.RouteSection.Equals("立水桥-霍营"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            foreach (JudgeInfo ji in judgeinfolist)
            {
                if (ji.RouteSection.Equals("立水桥-北苑"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
                else if (ji.RouteSection.Equals("北苑-立水桥"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            foreach (JudgeInfo ji in judgeinfolist)
            {
                if (ji.RouteSection.Equals("北苑-望京西"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
                else if (ji.RouteSection.Equals("望京西-北苑"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            foreach (JudgeInfo ji in judgeinfolist)
            {
                if (ji.RouteSection.Equals("望京西-芍药居"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
                else if (ji.RouteSection.Equals("芍药居-望京西"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            foreach (JudgeInfo ji in judgeinfolist)
            {
                if (ji.RouteSection.Equals("芍药居-光熙门"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
                else if (ji.RouteSection.Equals("光熙门-芍药居"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            foreach (JudgeInfo ji in judgeinfolist)
            {
                if (ji.RouteSection.Equals("光熙门-柳芳"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
                else if (ji.RouteSection.Equals("柳芳-光熙门"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            foreach (JudgeInfo ji in judgeinfolist)
            {
                if (ji.RouteSection.Equals("柳芳-东直门"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
                else if (ji.RouteSection.Equals("东直门-柳芳"))
                {
                    routenamelabel.Text = ji.RouteSection;
                    ranklabel.Text = "(" + ji.J.ToString("#0.000") + "," + ji.J0.ToString() + ")";
                    suggesttionlabel.Text = ji.Suggestion;
                    yearlabel.Text = ji.Year.ToString();
                    break;
                }
            }
        }

    }
}
