using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Platform.Core;
using Platform.Core.Services;
using Platform.Core.Data;
using WeifenLuo.WinFormsUI.Docking;
using WeifenLuo.WinFormsUI;
using Platform.Core.UI;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace MainPlugin
{
    public partial class AuthenticationForm : Form
    {
        public string username, password, ipaddress;//用户名，密码，ip地址
        public int port;//端口
        public string isConnected="deny";//连接状态
        BaseForm form=ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(null,new UserUIEventArgs("ServerViewForm",null));//获取serverform，以便更改一些全局状态
        Label label=new Label();//serverform上用于显示网络连接状态的标签
        string tooltype = string.Empty;//工具类型
        string projectid = string.Empty;//工程id

        public AuthenticationForm(string toolType,string prjid)
        {
            InitializeComponent();
            this.tooltype = toolType;//工具类型
            this.projectid = prjid;//工程id
        }

        private void accbutton_Click(object sender, EventArgs e)
        {
            this.Hide();//先隐藏当前登录窗体        
            username=usrnametextBox.Text;
            password=pwtextBox.Text;
            ipaddress=ipaddtextBox.Text;
            port = int.Parse(porttextBox.Text);//获取用户名、密码、服务器ip地址和端口
            //MessageBox.Show("开始显示进度条");
            //ConnectProgressForm progform = new ConnectProgressForm();
            //progform.Show();
            
            //isConnected=ServicesManager.ServicesManagerSingleton.NetworkService.ConnectServer(new ConnectingArgs(username,port,username,password));
            IAsyncResult iar=ServicesManager.ServicesManagerSingleton.NetworkService.ConnectServer.BeginInvoke(new ConnectingArgs(ipaddress, port, username, password,tooltype,projectid), new AsyncCallback(ConnectResult), null);//异步连接
            
            if(form!=null)//serverform存在，则更改显示
            {
                form.Controls.Add(label);
                label.Text = "联网中，请稍候";
                form.Show();
            }
            //System.Threading.Thread.Sleep(1000 * 5);//挂起当前线程
            //progform.Hide();
        }

        private void ConnectResult(IAsyncResult ias)
        {
            //获取异步线程的处理结果
            AsyncResult ar = (AsyncResult)ias;
            //获取异步线程中的委托
            ConnectingHandler a = (ConnectingHandler)ar.AsyncDelegate;
            //获取联网成功失败与否的标志
            isConnected = a.EndInvoke(ias);
            //PluginsManager.PluginsManagerSington.GetMutableResource().isConnected = isConnected;           
            //弹窗提示
            if (isConnected == "fail")
            {
                MessageBox.Show("联网失败！");
                if (form != null)
                {
                    if (label == null)
                        form.Controls.Add(label);
                    label.Text = "联网失败！脱机运行！";
                    form.Refresh();
                }
            }
            else if (isConnected == "deny")
                MessageBox.Show("权限不足！");
            else if (isConnected == "wrongidpw")
                MessageBox.Show("用户名或密码错误！");
            else
            {
                MessageBox.Show("联网成功！");
                //if (form != null)
                //{
                //    if (label == null)
                //        form.Controls.Add(label);
                //    label.Text = "联网已成功！";
                //    form.Refresh();
                //}
            }
        }

        private void cancelbutton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
