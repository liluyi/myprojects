using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Platform.Core;
using Platform.Core.Services;
using Platform.Core.UI;
using System.Reflection;
namespace WinForm
{
    public partial class WinForm : Form
    {
        public WinForm(string[] args)
        {
            InitializeComponent();

            Processing(args);
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
