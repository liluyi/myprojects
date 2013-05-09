using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace MainPlugin
{
    //更新进度列表
    public delegate void SetPos(int ipos);  
    public partial class ConnectProgressForm : Form
    {

        public ConnectProgressForm()
        {
            InitializeComponent();
        }
        private void SetTextMessage(int ipos)
        {
            if (this.InvokeRequired)
            {
                SetPos setpos = new SetPos(SetTextMessage);
                this.Invoke(setpos, new object[] { ipos });
            }
            else
            {
                this.progressBar1.Value = Convert.ToInt32(ipos);
            }
        }
        private void SleepT()
        {
            for (int i = 0; i < 500; i++)
            {
                System.Threading.Thread.Sleep(10);//没什么意思，单纯的执行延时
                SetTextMessage(100 * i / 500);
            }
        }

        private void ConnectProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void ConnectProgressForm_Load(object sender, EventArgs e)
        {
            Thread fThread = new Thread(new ThreadStart(SleepT));//开辟一个新的线程
            fThread.Start();
        }
    }
}
