using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClientBase;

namespace CASREE_MANAGEMENT_CLIENT
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void okbutton_Click(object sender, EventArgs e)
        {
            string username = nametextBox.Text;
            string password = passwordtextBox.Text;

            Boolean isConnectSucceed = false;    //连接服务器成功与否的标志
            Boolean isDisconnectSucceed = false;       //断开服务器成功与否的标志

            ClientBase.Message in_message;

            //测试服务器连接
            //isConnectSucceed = Program.cb.ConnectServer("192.168.241.48", 8888, username, password);
            isConnectSucceed = Program.cb.ConnectServer("localhost", 8500, "liluyi", "liluyi");
            //MessageBox.Show("Connect: " + isConnectSucceed);


            if (isConnectSucceed)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("用户名或密码错误！\n请重新输入！");
        }

        private void cancelbutton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
