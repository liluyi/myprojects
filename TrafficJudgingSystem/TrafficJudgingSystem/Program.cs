using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace TrafficJudgingSystem
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LogoForm logo = new LogoForm();
            logo.Show();
            Application.DoEvents();
            System.Threading.Thread.Sleep(3000);

            Application.DoEvents();
            logo.Close();
            //新建Login窗口（Login是自己定义的Form）
            LoginForm Log = new LoginForm();

            //使用模式对话框方法显示Log
            Log.ShowDialog();

            //DialogResult就是用来判断是否返回父窗体的
            if (Log.DialogResult == DialogResult.OK)
            {
                //在线程中打开主窗体
                Application.Run(new GuideForm());
            }           
            //Application.Run(new LoginForm());
            //Application.Run(new JudgementForm());
        }
        public static RouteInfoList routeinfolist = new RouteInfoList();
        public static RouteInfoList finallist = new RouteInfoList();

    }
}
