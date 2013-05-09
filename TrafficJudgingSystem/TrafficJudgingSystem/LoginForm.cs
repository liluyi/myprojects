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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.ToLower().Equals("bgd") && textBox2.Text.ToLower().Equals("bgd"))
            {
                //GuideForm guideform = new GuideForm();
                //guideform.Show();
                //this.Hide();
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("用户名或密码错误！\n请重新输入！");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
