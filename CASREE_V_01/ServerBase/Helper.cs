using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Timers;

namespace ServerBase
{
    class Helper
    {

        public delegate void handleFunction(object source, ElapsedEventArgs e);

        public static void checkActive(int minutes,handleFunction handle)
        {
            Timer t = new Timer();
            t.Interval = 1000 * minutes;//定时
            t.Elapsed += new ElapsedEventHandler(handle);
            t.AutoReset = true;
            t.Enabled = true;
        }

        public void test(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("hello");
        }

        //test
        //Helper h = new Helper();
        //h.checkActive(1, h.test);
        //Thread.Sleep(100000);
    }

}
