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
using Platform.Core.UI;
using System.Reflection;
using WeifenLuo.WinFormsUI.Docking;
using Platform.Core.Data;

namespace WinForm
{
    public partial class SplashForm : Form
    {
        string[] args;
        public SplashForm(string[] args)
        {
            InitializeComponent();
            this.args = args;
        }

        private void SplashForm_Load(object sender, EventArgs e)
        {
            this.Show();

            this.Refresh();

            //Processing(args);      //加载数据的方法

            this.Close();
        }
        /// <summary>
        /// 处理平台请求
        /// </summary>
        /// <param name="args"></param>
        private void Processing(string[] args)
        {
            Runtime rt = new Runtime("Core.xml", this);

            if (args == null || args.Length == 0)
            {
                rt.DealRequst(this, new RuntimeEventArgs(RequstType.ExcutePlatform));
            }
            else
            {
                foreach (string path in args)
                {
                    try
                    {
                        rt.DealRequst(this, new RuntimeEventArgs(RequstType.OpenProject, path));
                    }
                    catch
                    {

                    }
                }
            }
        }
    }
}
