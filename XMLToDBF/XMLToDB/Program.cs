using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace XMLToDB
{
    class Program
    {
        static void Main(string[] args)
        {
            int input;
            while (true)
            {
                Console.WriteLine("\n1.xml读入到数据库；2.数据库生成xml;0,退出\n");
                input = Int32.Parse(Console.ReadLine());
                if (input == 1)
                {
                    DealXML dm = new DealXML("E:\\Project\\Visual Studio 2010\\XMLToDB\\XMLToDB\\output.xml");
                    dm.XmlToDatabase();
                }
                else if(input==2)
                {
                    DealXML dm2 = new DealXML();
                    dm2.DatabaseToXml("E:\\Project\\Visual Studio 2010\\XMLToDB\\XMLToDB\\output.xml");
                }
                else if (input == 0)
                {
                    break;
                }
            }
        }
    }
}
