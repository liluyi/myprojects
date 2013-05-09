using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace ClientBase
{
    class ClientTest
    {
        public ClientTest()
        {

            Boolean isConnectSucceed = false;    //连接服务器成功与否的标志
            Boolean isDisconnectSucceed = false;       //断开服务器成功与否的标志
            ClientBase CB_Test = new ClientBase();

            Message in_message;


           


           
            //测试服务器连接
            /*
             * 用户名，密码正确登录后，服务器端发送一个整体的xml请求，客户端处理请求，client then send a ack.When server receive
             * the ack,begin to send a primary xml.
             */
            isConnectSucceed = CB_Test.ConnectServer("192.168.241.48", 8888, "yeqian", "admin");
            Console.WriteLine("Connect: " + isConnectSucceed);
          
/*
            //测试添加project
            Console.WriteLine("********Test AddProject to Server********");
            if (CB_Test.AddProject("project12", "program12", "string project", 12) == true)
            {
                Console.WriteLine("添加工程成功");
            }
            else
            {
                Console.WriteLine("添加工程失败");
            }
*/

/*
            //测试删除project
            Console.WriteLine("********Test DeleteProject to Server********");
            Console.WriteLine("project0","program0");
            if (CB_Test.DeleteProject("yeqian", "project0", "program0") == true)
            {
                Console.WriteLine("工程删除成功");
            }
 */         

/*
            //测试查询project
            Console.WriteLine("********Test SearchProject to Server********");
            if (CB_Test.SearchProject("project0", "program0") == true)
            {
                Console.WriteLine("查找工程成功");
            }
            else
            {
                Console.WriteLine("查找工程失败");
            }

            


            

           

            //测试从服务器上获取项目工程文件

            Console.WriteLine("\n********Test GetXml from Server********");
            in_message = CB_Test.GetXmlRequest("CASREE", "Assign");
            String ack = UnicodeEncoding.Unicode.GetString(in_message.MessageBody);

            Console.WriteLine(ack);

            if (ack.CompareTo("allow") == 0)
            {
                if (CB_Test.GetXml())
                {
                    Console.WriteLine("成功接收xml文件");
                }
            }




      
            ////测试向服务器传项目工程文件

            Console.WriteLine("********Test SendXml to Server********");
            in_message = CB_Test.SendXmlRequest("CASREE", "Design");//发情求
            if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("allow") == 0)
            {
                //发文件
                if (CB_Test.SendXml("CASREE", "Design", null))
                {
                    Console.WriteLine("成功发送xml文件\n");
                }

            }


            //测试从服务器上获取任意格式文档  
            Console.WriteLine("********Test GetDocument from Server********");
            in_message = CB_Test.GetDocumentRequest("CASREE", "Build", "jnStyle.mp3");
            if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("allow") == 0)
            {
                if (CB_Test.GetDocument())
                {
                    Console.WriteLine("成功接收jnStyle.mp3\n");
                }

            }



            ////测试向服务器传任意格式的文档
            Console.WriteLine("********Test SendDocument to Server********");
            in_message = CB_Test.SendDocumentRequest("CASREE", "Design", "jnStyle.mp3");
            if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("allow") == 0)
            {
                if (CB_Test.SendDocument("CASREE", "Design", "jnStyle.mp3", null))
                {
                    Console.WriteLine("发送成功.\n");
                }

            }

            ///测试添加用户
            Console.WriteLine("********Test Adduser to Server********");
            //public Boolean AddUser(string name, string passwd, int groupid)
            if (CB_Test.AddUser("bhs", "123", 2) == true)
            {
                Console.WriteLine("添加用户成功");
            }
            else
            {
                Console.WriteLine("添加用户失败");
            }

            ///测试删除用户
            Console.WriteLine("********Test Deleteuser to Server********");

            ///测试搜索用户
            Console.WriteLine("********Test Searchuser to Server********");
            if (CB_Test.SearchUser("yeqian") == true)
            {
                Console.WriteLine("搜索用户成功");
            }
            else
            {
                Console.WriteLine("搜索用户失败");
            }
*/
            
            ///测试添加权限
            Console.WriteLine("********Test AddPermission to Server********");
            //string username,userproject, level
            if (CB_Test.AddPermission("bhs", "project10", 3).CompareTo("succeed") == 0)
            {
                Console.WriteLine("添加权限成功");
            }
            else
            {
                Console.WriteLine("添加权限失败");
            }

/*
           
            Console.WriteLine("********Test Disconnected to Server********");
            isDisconnectSucceed = CB_Test.DisConnectServer();
            if (isDisconnectSucceed)
            {
                Console.WriteLine("Disconnect successfully.");
            }
            else
            {
                Console.WriteLine("Disconnect unsuccessfully.");
            }
*/
        }
    }
}
