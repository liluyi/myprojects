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
    public class ClientTest
    {
        public ClientTest() 
        {
            
            Boolean isConnectSucceed = false;    //连接服务器成功与否的标志
            Boolean isDisconnectSucceed = false;       //断开服务器成功与否的标志
            ClientBase CB_Test = new ClientBase();
            
            Message in_message;

            //测试服务器连接
            isConnectSucceed = CB_Test.ConnectServer("192.168.241.48", 8888,"admin","admin");
            //isConnectSucceed = CB_Test.ConnectServer("localhost", 8500, "xu", "1234");
            Console.WriteLine("Connect: "+isConnectSucceed);

            //获取历史版本信息
            //in_message = CB_Test.GetProjectVersionRequest("casree", "Design");
            //if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("allow") == 0)
            //{
            //    if (CB_Test.GetXml())
            //    {
            //        Console.WriteLine("GetXml successfully.\n");
            //    }
            //}
            if (isConnectSucceed)//只有连接成功才允许其它操作
            {
                CB_Test.AddUser("liluyi", "liluyi", 1);
                CB_Test.SearchUser("liluyi");
                CB_Test.AddPermission("liluyi", "project1", 3);
                CB_Test.AddPermission("liluyi", "project1", 4);
                CB_Test.SearchPermission("liluyi");
            }

            ////测试从服务器上获取项目工程文件
            //Console.WriteLine("\n********Test GetXml from Server********");
            //in_message = CB_Test.GetXmlRequest("CASREE", "Design","001");//发请求
            //Console.WriteLine(Encoding.Unicode.GetString(in_message.MessageBody));
            //if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("allow") == 0)
            //{
            //    //接受文件
            //    if (CB_Test.GetXml())
            //    {
            //        Console.WriteLine("GetXml successfully.\n");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("oh...shit");
            //}
            

            //////测试向服务器传项目工程文件
            //Console.WriteLine("********Test SendXml to Server********");
            //in_message = CB_Test.SendXmlRequest("CASREE", "Design");//发情求
            ////Console.WriteLine(Encoding.Unicode.GetString(in_message.MessageBody));
            //if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("allow") == 0)
            //{
            //    //发文件
            //    if (CB_Test.SendXml("CASREE", "Design", null))
            //    {
            //        Console.WriteLine("Send xml successfully.\n");
            //    }

            //}

            ////测试从服务器上获取任意格式文档
            //Console.WriteLine("********Test GetDocument from Server********");
            //in_message = CB_Test.GetDocumentRequest("CASREE", "Assign", "海阔天空001_Server.mp3");
            ////Console.WriteLine(Encoding.Unicode.GetString(in_message.MessageBody));
            //if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("allow") == 0)
            //{
            //    if (CB_Test.GetDocument())
            //    {
            //        Console.WriteLine("Get Document 海阔天空001_Server.mp3 successfully.\n");
            //    }

            //}

            ////测试向服务器传任意格式的文档
            //Console.WriteLine("********Test SendDocument to Server********");
            //in_message = CB_Test.SendDocumentRequest("CASREE", "Design", "Document.txt");
            ////Console.WriteLine(Encoding.Unicode.GetString(in_message.MessageBody));
            //if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("allow") == 0)
            //{
            //    if (CB_Test.SendDocument("CASREE", "Design", "Document.txt", null))
            //    {
            //        Console.WriteLine("send Document.txt successfully.\n");
            //    }
            //}

            //Console.WriteLine("********Test Disconnected to Server********");
            //isDisconnectSucceed = CB_Test.DisConnectServer();
            //if (isDisconnectSucceed)
            //{
            //    Console.WriteLine("Disconnect successfully.");
            //}
            //else
            //{
            //    Console.WriteLine("Disconnect unsuccessfully.");
            //}
            
        }
    }
}
