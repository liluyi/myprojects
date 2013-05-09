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
    public partial class EvaluateForm : Form
    {

        public EvaluateForm()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString().Equals("路基本体"))
            {
                comboBox2.Enabled = true;
                comboBox2.Items.Clear();
                comboBox3.Enabled = false;
                comboBox2.Items.Add("基床");
                comboBox2.Items.Add("路肩");
                comboBox2.Items.Add("边坡");
            }
            else if (comboBox1.SelectedItem.ToString().Equals("路基排水"))
            {
                comboBox2.Items.Clear();
                comboBox2.Enabled = false;
                comboBox3.Enabled = true;
                comboBox3.Items.Clear();
                comboBox3.Items.Add("破损程度");
                comboBox3.Items.Add("排水性能");
                comboBox3.Items.Add("清洁性");
            }
            else if (comboBox1.SelectedItem.ToString().Equals("路基防护"))
            {
                comboBox2.Items.Clear();
                comboBox2.Enabled = false;
                comboBox3.Enabled = true;
                comboBox3.Items.Clear();
                comboBox3.Items.Add("沉降程度");
                comboBox3.Items.Add("破损程度");
                comboBox3.Items.Add("裂缝宽度");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.ToString().Equals("基床"))
            {
                comboBox3.Enabled = true;
                comboBox3.Items.Clear();
                comboBox3.Items.Add("沉降程度");
                comboBox3.Items.Add("破损程度");
                comboBox3.Items.Add("翻浆冒泥面积");
            }
            else if (comboBox2.SelectedItem.ToString().Equals("路肩"))
            {
                comboBox3.Enabled = true;
                comboBox3.Items.Clear();
                comboBox3.Items.Add("破损程度");
                comboBox3.Items.Add("平整性");
                comboBox3.Items.Add("清洁性");
            }
            else if (comboBox2.SelectedItem.ToString().Equals("边坡"))
            {
                comboBox3.Enabled = true;
                comboBox3.Items.Clear();
                comboBox3.Items.Add("破损程度");
                comboBox3.Items.Add("平整性");
                comboBox3.Items.Add("裂缝宽度");
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckBox cb111 = new CheckBox();
            CheckBox cb112 = new CheckBox();
            CheckBox cb113 = new CheckBox();

            CheckBox cb121 = new CheckBox();
            CheckBox cb122 = new CheckBox();
            CheckBox cb123 = new CheckBox();

            CheckBox cb131=new CheckBox();
            CheckBox cb132 = new CheckBox();
            CheckBox cb133 = new CheckBox();

            CheckBox cb21 = new CheckBox();
            CheckBox cb22 = new CheckBox();
            CheckBox cb23 = new CheckBox();

            CheckBox cb31 = new CheckBox();
            CheckBox cb32 = new CheckBox();
            CheckBox cb33 = new CheckBox();
            
            if (comboBox2.Enabled == false)
            {
                if (comboBox1.SelectedItem.ToString().Equals("路基排水"))
                {
                    if (comboBox3.SelectedItem.ToString().Equals("破损程度"))
                    {
                        cb21.Text = "破损程度";
                        cb21.Select();
                        panel2.Controls.Add(cb21);
                    }
                    else if (comboBox3.SelectedItem.ToString().Equals("排水性能"))
                    {
                        cb22.Text = "排水性能";
                        cb22.Select();
                        panel2.Controls.Add(cb22);
                    }
                    else if (comboBox3.SelectedItem.ToString().Equals("清洁性"))
                    {
                        cb23.Text = "清洁性";
                        cb23.Select();
                        panel2.Controls.Add(cb23);
                    }
                }
                else if (comboBox1.SelectedItem.ToString().Equals("路基防护"))
                {
                    if (comboBox3.SelectedItem.ToString().Equals("沉降程度"))
                    {
                        cb31.Text = "沉降程度";
                        cb31.Select();
                        panel3.Controls.Add(cb31);
                    }
                    else if (comboBox3.SelectedItem.ToString().Equals("破损程度"))
                    {
                        cb32.Text = "破损程度";
                        cb32.Select();
                        panel3.Controls.Add(cb32);
                    }
                    else if (comboBox3.SelectedItem.ToString().Equals("裂缝宽度"))
                    {
                        cb33.Text = "裂缝宽度";
                        cb33.Select();
                        panel3.Controls.Add(cb33);
                    }
                }
            }
            else
            {
                if (comboBox2.SelectedItem.ToString().Equals("边坡"))
                {
                    if (comboBox3.SelectedItem.ToString().Equals("破损程度"))
                    {
                        cb131.Text = "破损程度";
                        cb131.Checked=true;
                        panel1.Controls.Add(cb131);
                    }
                    else if (comboBox3.SelectedItem.ToString().Equals("平整性"))
                    {
                        cb132.Text = "平整性";
                        cb132.Select();
                        panel1.Controls.Add(cb132);
                    }
                    else if (comboBox3.SelectedItem.ToString().Equals("裂缝宽度"))
                    {
                        cb133.Text = "裂缝宽度";
                        cb133.Select();
                        panel1.Controls.Add(cb133);
                    }
                }
                else if (comboBox2.SelectedItem.ToString().Equals("路肩"))
                {
                    if (comboBox3.SelectedItem.ToString().Equals("破损程度"))
                    {
                        cb121.Text = "破损程度";
                        cb121.Select();
                        panel1.Controls.Add(cb121);
                    }
                    else if (comboBox3.SelectedItem.ToString().Equals("平整性"))
                    {
                        cb122.Text = "平整性";
                        cb122.Select();
                        panel1.Controls.Add(cb122);
                    }
                    else if (comboBox3.SelectedItem.ToString().Equals("清洁性"))
                    {
                        cb123.Text = "清洁性";
                        cb123.Select();
                        panel1.Controls.Add(cb123);
                    }
                }
                else if (comboBox2.SelectedItem.ToString().Equals("基床"))
                {
                    if (comboBox3.SelectedItem.ToString().Equals("沉降程度"))
                    {
                        cb111.Text = "沉降程度";
                        cb111.Select();
                        panel1.Controls.Add(cb111);
                    }
                    else if (comboBox3.SelectedItem.ToString().Equals("破损程度"))
                    {
                        cb112.Text = "破损程度";
                        cb112.Select();
                        panel1.Controls.Add(cb112);
                    }
                    else if (comboBox3.SelectedItem.ToString().Equals("翻浆冒泥面积"))
                    {
                        cb113.Text = "翻浆冒泥面积";
                        cb113.Select();
                        panel1.Controls.Add(cb113);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            //DataInputForm dataform = new DataInputForm();
        }
    }
}
