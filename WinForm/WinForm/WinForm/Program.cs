using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WinForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ExceptionBox.HandleNotCatchedException);
            Application.Run(new WinForm(args));
        }

    }
}
