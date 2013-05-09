using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace ClientBase
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientTest CT = new ClientTest();

            string solutionName = "CASREE";
            string projectName = "Assign";
            
           //if (Directory.Exists(solutionName)) //判断当前目录下是否存在该项目的文件夹
           //{
           //    if (Directory.Exists(solutionName))
           //    {
           //        Console.WriteLine(solutionName + " exist.");
           //    }
           //    else
           //    {
           //        Console.WriteLine("Create "+ solutionName);
           //    }
           //}

           //if (Directory.Exists(solutionName + "\\" + projectName))
           //{
           //    Console.WriteLine( projectName + " EXIST1");
           //}
           //else
           //{
           //    Console.WriteLine("create " + projectName);
           //    Directory.CreateDirectory(solutionName + "\\" + projectName);

           //}

            //if (File.Exists(solutionName + "\\" + projectName + "\\" + projectName))
            //{
            //    Console.WriteLine("Test");
            //}
            //else
            //{
            //    Console.WriteLine("haha");
            //    FileStream fs = File.Create(solutionName + "\\" + projectName + "\\" + projectName + DateTime.Now.ToString("__yyyy_m_dd#hh_mm_ss"));
            //    fs.Close();
            //}

            //if (Directory.GetFiles(solutionName + "\\" + projectName).Count() == 0)
            //{
            //    Console.WriteLine("file");
            //    File.Create(solutionName + "\\" + projectName + "\\justatest");
            //}

            //DateTime datetime = DateTime.Now;
            //Console.WriteLine(datetime);
            //datetime = datetime.addmonths(1);
            //Console.WriteLine(datetime.ToString().Replace(':','_').Replace(' ','_').Replace('/','_'));
            //using (StreamReader sr = new StreamReader(Directory.GetFiles(solutionName + "\\" + projectName).First(), Encoding.Default))
            //{
            //    string line = string.Empty;
            //    while ((line = sr.ReadLine()) != null)
            //    {
            //        Console.WriteLine(line);
            //    }
            //}
            //string projectNameXmlWithPath = Directory.GetFiles(solutionName + "\\" + projectName).First();
            //Console.WriteLine(projectNameXmlWithPath.Split('\\')[projectNameXmlWithPath.Split('\\').Length - 1]);
            //Console.WriteLine(Directory.GetFiles(solutionName + "\\" + projectName).First());

           Console.Read();
        }
    }
}
