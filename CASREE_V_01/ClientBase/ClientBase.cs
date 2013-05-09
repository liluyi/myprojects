﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Timers;
using System.Diagnostics;

namespace ClientBase
{
    public delegate void StatusChangeHandler(string result);
    public class ClientBase
    {
        private TcpClient client;//客户端实例
        private NetworkStream dataStream;//客户端与服务器之间的数据流
        Timer clientTimer;//定时器
        public StatusChangeHandler StatusChange;

        public ClientBase() 
        {
            this.client = new TcpClient();
        }

        /// <summary>
        /// 连接服务器并认证
        /// </summary>
        /// <param name="hostname"></param>
        /// <param name="port"></param>
        /// <param name="name"></param>
        /// <param name="passwd"></param>
        /// <returns></returns>
        public Boolean ConnectServer(string hostname,int port, string name ,string passwd)
        {
            string authresult = string.Empty;
            try
            {
                this.client.Connect(hostname,port);
                Console.WriteLine("connecting the  Server {0} successfully ...", client.Client.RemoteEndPoint);

                authresult=CheckAuth(name, passwd,"ADMIN","ADMIN");
                if (authresult == "allow")//用户认证
                {
                    Console.WriteLine("Auth is successful...");
                    /*
                     * name,passwd验证通过，接收server发送的接收xml信号，想server回发信号
                     */

                    //需要再接受一个xml格式文件，含有服务器所有项目工程名称
                    GetSolutionProjectListXml();

                    SayHelloToServer(1);//连接成功后，每隔1分钟发一个心跳包
                }
                else 
                {
                    DisConnectServer();
                    return false;//断开连接，并回复消息
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ConnectServer: "+ ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 定时发送心跳包,告诉服务器我还活着
        /// </summary>
        public void SayHelloToServer(int minutes)
        {
            clientTimer = new Timer();
            clientTimer.Interval = 1000 * 60 * minutes;//定时
            clientTimer.Elapsed += new ElapsedEventHandler(SendHelloMessage);
            clientTimer.AutoReset = true;
            clientTimer.Enabled = true;
        }

        /// <summary>
        /// 发送心跳包
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void SendHelloMessage(object source,ElapsedEventArgs e)
        {
            Message hello_message = new Message();
            hello_message.Command = Message.CommandHeader.Hello;
            hello_message.MessageBody = Encoding.Unicode.GetBytes(
                this.client.Client.LocalEndPoint.ToString() + " is Alive");
            //判断连接、即使是断开了，也可能返回true，所以改判断无效
            //if (this.client.Connected)
            //{
            //    Console.WriteLine(this.client.Client.RemoteEndPoint.ToString() + " still connected.");//测试
            //}
            
            Message back_message = StreamDataOutAndInHelper(hello_message);
            if (back_message.Command == Message.CommandHeader.PushXMLAck)
            {
                clientTimer.Enabled = false;
                PushXMLAck(back_message);
                clientTimer.Enabled = true;
            }
        }
     
        /// <summary>
        /// 断开服务器
        /// </summary>
        /// <param name="hostname"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public Boolean DisConnectServer()
        {
            try
            {
                Console.WriteLine("disconnect with the server {0}...", this.client.Client.RemoteEndPoint);
                
                //枚举当前程序的所有线程
                //ProcessThreadCollection ptCollection = Process.GetCurrentProcess().Threads;
                //foreach (ProcessThread pt in ptCollection)
                //{
                //    Console.WriteLine("ID:{0},State:{1},Priority:{2}", pt.Id, pt.ThreadState, pt.PriorityLevel);
                //}

                //向服务器发送一个offline数据包
                Message out_message = new Message();
                out_message.Command = Message.CommandHeader.Offline;
                out_message.MessageBody = Encoding.Unicode.GetBytes("offline");
                //获得输出流
                dataStream = client.GetStream();
                while (!dataStream.CanWrite)
                {
                    System.Threading.Thread.Sleep(1000);//sleep 1s
                }
                //将打包成message的信息写入输出流
                dataStream.Write(out_message.ToBytes(), 0, out_message.MessageLength);

                this.client.Close();
                Console.WriteLine("the client is offline...");

            }
            catch (Exception ex)
            {
                Console.WriteLine("DisConnectServer: "+ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 通过用户名、密码进行认证
        /// </summary>
        /// <param name="name"></param>
        /// <param name="passwd"></param>
        /// <returns></returns>
        public string CheckAuth(string name, string passwd,string tooltype,string projectid)
        {
            Message out_message = new Message();
            Message in_message;
            out_message.Command = Message.CommandHeader.LoginAuth;

            //消息体携带用户名、密码信息
            out_message.MessageBody = Encoding.Unicode.GetBytes(name + ":" + passwd+":"+tooltype+":"+projectid);

            //获取服务器反馈 -->//可以加个定时器设置认证超时
            in_message = StreamDataOutAndInHelper(out_message);

            //假定认证成功服务器消息体返回allow,失败返回deny
             //假定认证成功服务器消息体返回allow,失败返回deny
            if (in_message.Command == Message.CommandHeader.LoginAck
                && Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("allow") == 0)
            {
                Console.WriteLine((in_message.Command == Message.CommandHeader.LoginAck
                && Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("allow") == 0));

                // dataStream.Write(Encoding.Unicode.GetBytes("ready"),0,"ready".Length);
                out_message.MessageBody = Encoding.Unicode.GetBytes("ready");
                dataStream.Write(out_message.ToBytes(), 0, out_message.MessageLength);
                return "allow";
            }
            else if (in_message.Command == Message.CommandHeader.LoginAck
                && Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("deny") == 0)
            {
                return "deny";
            }
            else if (in_message.Command == Message.CommandHeader.LoginAck
                && Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("deny_pw") == 0)
            {
                return "wrongidpw";
            }
            else
                return "fail";
        }

        /// <summary>
        /// 客户端发送从服务器上获取指定项目工程xml描述文件的请求，得到服务器的反馈
        /// 服务器传输给客户端亦需要客户端的同意接受后，服务器才发送文件给客户端 
        /// </summary>
        /// <param name="solutionName"></param>
        /// <param name="projectName">仅仅是工程名，不是工程描述文件</param>
        public Message GetXmlRequest(string solutionName,string projectName,string version)
        {
            Message out_message = new Message();
            out_message.Command = Message.CommandHeader.GetXmlRequest;
            out_message.MessageBody = Encoding.Unicode.GetBytes(solutionName + ":" + projectName +":"+version);//项目名+工程名
            //获取服务器反馈 
            return StreamDataOutAndInHelper(out_message);
        }

        public Message GetProjectVersionRequest(string solutionName, string projectName)
        {
            Message out_message = new Message();
            out_message.Command = Message.CommandHeader.GetProjectVersionRequest;
            out_message.MessageBody = Encoding.Unicode.GetBytes(solutionName + ":" + projectName);//项目名+工程名
            //获取服务器反馈 
            return StreamDataOutAndInHelper(out_message);
        }

        /// <summary>
        /// 获取指定项目工程的版本历史信息
        /// </summary>
        /// <returns></returns>
        public Boolean GetProjectVersionListXml()
        {
            return GetXml();
        }

        /// <summary>
        /// 获取项目工程列表
        /// </summary>
        /// <returns></returns>
        public Boolean GetSolutionProjectListXml() 
        {
            //整个文件接收工作需要在这全部完成，阻塞方式
            FileStream fs;
            Message in_message;
            Message out_message;
            string fileName = string.Empty;
            string fileNameAddTimestampWithRelativePath = string.Empty;
            
            do
            {
                in_message = Message.Parse(dataStream);
                switch (in_message.MessageFlag)
                {
                    case Message.MessageFlagHeader.FileBegin:
                        //得到项目名和文件名，文件名不带后缀.xml
                        fileName = Encoding.Unicode.GetString(in_message.MessageBody);
                        
                        //生成时间戳 格式：__2011_12_2_19_48_37
                        string timestamp = "__" + DateTime.Now.ToString().Replace(':', '_').Replace(' ', '_').Replace('/', '_');
                        //生成文件名
                        fileNameAddTimestampWithRelativePath = fileName + timestamp;

                        if (!File.Exists(fileNameAddTimestampWithRelativePath))
                        {
                            Console.WriteLine(fileNameAddTimestampWithRelativePath);
                            fs = File.Create(fileNameAddTimestampWithRelativePath);
                            if (File.Exists(fileNameAddTimestampWithRelativePath)) {
                            }
                            fs.Close();//需要关闭该文件，否则下面无法获取文件锁，进行文件读写
                        }
                        break;
                    case Message.MessageFlagHeader.FileMiddle://得到文件内容,采用追加方式写入文件
                        if (!File.Exists(fileNameAddTimestampWithRelativePath))
                        {
                            Console.WriteLine(fileNameAddTimestampWithRelativePath + " not exist");

                            //发送接收失败的确认数据包
                            out_message = new Message();
                            out_message.Command = Message.CommandHeader.ReceivedXml;
                            out_message.MessageBody = Encoding.Unicode.GetBytes("no");
                            dataStream.Write(out_message.ToBytes(), 0, out_message.MessageLength);
                            return false;
                        }
                        fs = File.Open(fileNameAddTimestampWithRelativePath, FileMode.Append);
                        StreamWriter sw = new StreamWriter(fs, Encoding.Unicode, in_message.MessageBody.Length);
                        sw.WriteLine(Encoding.Unicode.GetString(in_message.MessageBody));
                        sw.Flush();
                        sw.Close();
                        fs.Close();
                        break;
                    case Message.MessageFlagHeader.FileEnd://文件传送结束,需要判断第一个数据包直接跳到这项的 
                        //发送成功接收的确认数据包
                        out_message = new Message();
                        out_message.Command = Message.CommandHeader.ReceivedXml;
                        out_message.MessageBody = Encoding.Unicode.GetBytes("yes");
                        dataStream.Write(out_message.ToBytes(), 0, out_message.MessageLength);
                        return true;
                    default:
                        //发送接收失败的确认数据包
                        out_message = new Message();
                        out_message.Command = Message.CommandHeader.ReceivedXml;
                        out_message.MessageBody = Encoding.Unicode.GetBytes("no");
                        dataStream.Write(out_message.ToBytes(), 0, out_message.MessageLength);

                        Console.WriteLine(fileName + " trasfer failed...");
                        //若存在接受不全的文件，删除
                        if (File.Exists(fileNameAddTimestampWithRelativePath))
                        {
                            File.Delete(fileNameAddTimestampWithRelativePath);
                        }
                        break;
                }
            } while (in_message.MessageFlag == Message.MessageFlagHeader.FileBegin ||
                in_message.MessageFlag == Message.MessageFlagHeader.FileMiddle);

            return false;
        }
        /// <summary>
        /// 客户端接受服务器发过来的指定项目工程xml描述文件
        /// </summary>
        /// <returns></returns>
        public Boolean GetXml()
        {
            //整个文件接收工作需要在这全部完成，阻塞方式
            FileStream fs;
            Message in_message;
            Message out_message;
            string solutionName = string.Empty;
            string projectName = string.Empty;
            //格式@主目录\\子目录\\工程描述文件
            string projectNameAddTimestampWithRelativePath = string.Empty;

            do
            {                
                in_message = Message.Parse(dataStream);
                switch (in_message.MessageFlag)
                {
                    case Message.MessageFlagHeader.FileBegin:
                        //得到项目名和文件名，文件名不带后缀.xml
                        solutionName = Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[0];
                        projectName = Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[1];

                        //判断当前目录下是否存在该项目及工程文件夹
                        if (!Directory.Exists(solutionName + "\\" + projectName)) 
                        {
                            //在当前路径下创建项目文件夹                           
                            Directory.CreateDirectory(solutionName + "\\" + projectName);
                        }

                        //生成时间戳 格式：__2011_12_2_19_48_37
                        string timestamp = "__" + DateTime.Now.ToString().Replace(':', '_').Replace(' ', '_').Replace('/', '_');
                        projectNameAddTimestampWithRelativePath = solutionName + "\\" + projectName + "\\" + projectName + timestamp;
                        if (!File.Exists(projectNameAddTimestampWithRelativePath))
                        {
                            fs = File.Create(projectNameAddTimestampWithRelativePath);
                            fs.Close();//需要关闭该文件，否则下面无法获取文件锁，进行文件读写
                        }
                        break;
                    case Message.MessageFlagHeader.FileMiddle://得到文件内容,采用追加方式写入文件
                        if (!File.Exists(projectNameAddTimestampWithRelativePath))
                        {
                            Console.WriteLine(projectNameAddTimestampWithRelativePath + " not exist");

                            //发送接收失败的确认数据包
                            out_message = new Message();
                            out_message.Command = Message.CommandHeader.ReceivedXml;
                            out_message.MessageBody = Encoding.Unicode.GetBytes("no");
                            dataStream.Write(out_message.ToBytes(), 0, out_message.MessageLength);
                            return false;
                        }
                        fs = File.Open(projectNameAddTimestampWithRelativePath, FileMode.Append);
                        StreamWriter sw = new StreamWriter(fs, Encoding.Unicode, in_message.MessageBody.Length);
                        sw.WriteLine(Encoding.Unicode.GetString(in_message.MessageBody));
                        sw.Flush();
                        sw.Close();
                        fs.Close();
                        break;
                    case Message.MessageFlagHeader.FileEnd://文件传送结束,需要判断第一个数据包直接跳到这项的 
                        //发送成功接收的确认数据包
                        out_message = new Message();
                        out_message.Command = Message.CommandHeader.ReceivedXml;
                        out_message.MessageBody = Encoding.Unicode.GetBytes("yes");
                        dataStream.Write(out_message.ToBytes(), 0, out_message.MessageLength);
                        return true;
                    default:
                        //发送接收失败的确认数据包
                        out_message = new Message();
                        out_message.Command = Message.CommandHeader.ReceivedXml;
                        out_message.MessageBody = Encoding.Unicode.GetBytes("no");
                        dataStream.Write(out_message.ToBytes(), 0, out_message.MessageLength);

                        Console.WriteLine("The xml file trasfer failed...");
                        //若存在接受不全的文件，删除
                        if (File.Exists(projectNameAddTimestampWithRelativePath))
                        {
                            File.Delete(projectNameAddTimestampWithRelativePath);
                        }
                        break;
                }
            } while (in_message.MessageFlag == Message.MessageFlagHeader.FileBegin ||
                in_message.MessageFlag == Message.MessageFlagHeader.FileMiddle);

            return false;
        }

        /// <summary>
        /// 客户端发送从客户端传指定工程项目xml描述文件projectname到服务器的请求，得到服务器的反馈
        /// 客户端传输文件给服务器需要服务器的权限确认后，客户端才能发送文件给服务器
        /// </summary>
        /// <param name="solutionName">项目名</param>
        /// <param name="projectName">工程名</param>
        /// <returns></returns>
        public Message SendXmlRequest(string solutionName,string projectName)
        {
            Message out_message = new Message();
            out_message.Command = Message.CommandHeader.SendXmlRequest;
            out_message.MessageBody = Encoding.Unicode.GetBytes(solutionName+":"+projectName);
            //获取服务器反馈 
            return StreamDataOutAndInHelper(out_message);
        }

        /// <summary>
        /// 发送客户端项目工程xml描述文件projectNameXml给服务器，整个文件的发送均在此方法内完成
        /// </summary>
        /// <param name="projectNameXml"></param>
        /// <returns></returns>
        public Boolean SendXml(string solutionName,string projectName,string xmlFilePath) {
            //打开本地文件，读取内容，分行写到message中,连续发送，不需客户端的确认
            //第一个数据包消息体：文件名
            //最后一个数据包消息体：#
            try
            {
                Message out_message;
                
                string solutionProjectDirectory = solutionName+"\\"+projectName;
                //相对客户端运行目录的路径
                string projectNameWithRelativePath = string.Empty;

                //若传入的绝对路径参数不为空
                if (xmlFilePath != null &&  File.Exists(xmlFilePath)) {
                    projectNameWithRelativePath = xmlFilePath;
                }
                else if (Directory.GetFiles(solutionProjectDirectory).Count() == 0)
                {   
                    //若传入的绝对路径参数为空，判断solutionProjectName目录下由xml描述文件存在
                    Console.WriteLine("there is no project file existing in the " + solutionProjectDirectory);
                }
                else 
                {
                    //找到一个最近时间戳的xml描述文件

                    //获取目录下所有文件的信息
                    FileInfo[] files = (new DirectoryInfo(solutionProjectDirectory)).GetFiles();
                    FileInfo theLastestFile = files.First();
                    foreach (var file in files) {
                        if (file.LastWriteTime > theLastestFile.LastWriteTime) {
                            theLastestFile = file;
                        }
                    }
                    projectNameWithRelativePath = solutionProjectDirectory + "\\" + theLastestFile;
                }

                using (StreamReader sr = new StreamReader(projectNameWithRelativePath, Encoding.Default))//当前目录下的projectNameXml
                {
                    //传送文件开始的第一个数据包
                    out_message = new Message();
                    //获得文件名
                    string filename = projectNameWithRelativePath.Split('\\')[projectNameWithRelativePath.Split('\\').Length - 1];
                    
                    //文件名包含项目及工程solutonName:projectName
                    out_message.MessageBody = Encoding.Unicode.GetBytes(solutionName+":"+projectName);
                    Console.WriteLine("The file name: " + filename);
                    out_message.Command = Message.CommandHeader.SendXml;
                    out_message.MessageFlag = Message.MessageFlagHeader.FileBegin;
                    dataStream.Write(out_message.ToBytes(), 0, out_message.MessageLength);

                    //下面传送文件内容
                    string line = string.Empty;
                    int packetNum = 1;
                    while ((line = sr.ReadLine()) != null)
                    {
                        out_message = new Message();
                        out_message.MessageBody = Encoding.Unicode.GetBytes(line);
                        out_message.Command = Message.CommandHeader.SendXml;
                        out_message.FilePacketNumber = packetNum++;//数据包添加序号
                        out_message.MessageFlag = Message.MessageFlagHeader.FileMiddle;
                        dataStream.Write(out_message.ToBytes(), 0, out_message.MessageLength);
                    }

                    //文件读取结束
                    out_message = new Message();
                    out_message.MessageBody = Encoding.Unicode.GetBytes("#");//该消息体最终被舍弃
                    out_message.Command = Message.CommandHeader.SendXml;
                    out_message.MessageFlag = Message.MessageFlagHeader.FileEnd;
                    dataStream.Write(out_message.ToBytes(), 0, out_message.MessageLength);
                }
                //读取服务器反馈，以判断服务器是否正确接收文件
                Message in_message = Message.Parse(dataStream);
                if (in_message.Command == Message.CommandHeader.ReceivedXml
                    && Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("yes") == 0)
                {
                    return true;
                }   
            }
            catch (Exception ex) 
            {
                Console.WriteLine("SendXml: "+ex.Message);
            }
            return false;
        }
        public string PushXMLAck(Message in_message)
        {
            if (in_message.Command == Message.CommandHeader.PushXMLAck)
            {
                if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("succeed") == 0)
                {
                    Console.WriteLine("推送成功！");
                    if (StatusChange != null)
                        StatusChange("目标客户端已经成功接收");
                    return "目标客户端已经成功接收";
                }
                else if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("fail") == 0)
                {
                    Console.WriteLine("目标已经下线！");
                    if (StatusChange != null)
                        StatusChange("推送失败，请重新推送");
                    return "推送失败，请重新推送";
                }
            }
            return "";
        }
        /// <summary>
        /// 要求服务器向客户端推送消息
        /// </summary>
        /// <param name="sourceprjid">消息来源工程id</param>
        /// <param name="destinationprjid">推送目标工程id</param>
        /// <param name="solutionName">项目id</param>
        /// <returns></returns>
        public string PushXML(string sourceprjid,string destinationprjid,string solutionName)
        {
            Message out_message = new Message();
            Message in_message;
            out_message.Command = Message.CommandHeader.PushXML;

            //消息体携带用户名、工程id、权限级别
            out_message.MessageBody = Encoding.Unicode.GetBytes(sourceprjid + ":" + destinationprjid+":"+solutionName);

            //获取服务器反馈 -->//可以加个定时器设置认证超时
            in_message = StreamDataOutAndInHelper(out_message);

            //权限添加成功succeed，失败fail，用户已存在exist
            if (in_message.Command == Message.CommandHeader.PushXML)
            {
                if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("succeed") == 0)
                {
                    Console.WriteLine("推送成功！");
                    return "目标客户端已经成功接收";
                }
                else if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("offline") == 0)
                {
                    Console.WriteLine("目标已经下线！");
                    return "目标客户端脱机，无法推送";
                }
                else if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("notexisted") == 0)
                {
                    Console.WriteLine("项目不存在！");
                    return "项目不存在";
                }
                else if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("waiting") == 0)
                {
                }
                return "服务器正在向目标客户端推送数据，请稍后……";
            }
            else
            {
                Console.WriteLine("数据推送失败！");
                return "数据推送失败";
            }
        }

        /// <summary>
        /// 客户端发送任意格式文档到服务器的请求，得到服务器的反馈
        /// 客户端传输文件给服务器需要服务器的权限确认后，客户端才能发送文件给服务器
        /// </summary>
        /// <param name="solutionName"></param>
        /// <param name="projectName"></param>
        /// <param name="documentName"></param>
        /// <returns></returns>
        public Message SendDocumentRequest(string solutionName,string projectName,string documentName) {
            Message out_message = new Message();
            out_message.Command = Message.CommandHeader.SendDocumentRequest;
            out_message.MessageBody = Encoding.Unicode.GetBytes(solutionName+":"+projectName+":"+documentName);
            //获取服务器反馈 
            return StreamDataOutAndInHelper(out_message);
        }

        /// <summary>
        /// 客户端发送任意格式文档给服务器，整个文件的发送均在此方法内完成
        /// </summary>
        /// <param name="solutionName"></param>
        /// <param name="projectName"></param>
        /// <param name="documentName"></param>
        /// <returns></returns>
        public Boolean SendDocument(string solutionName, string projectName, string documentName,string documentFilePath)
        {
            try
            {
                Message out_message;
                string solutionProjectDirectory = solutionName + "\\" + projectName;

                //文档的绝对路径
                string projectDocumentFilePath = string.Empty;

                //相对客户端运行目录的路径
                //string projectNameWithRelativePath = solutionProjectDirectory+"\\"+documentName;

                if (documentFilePath != null && File.Exists(documentFilePath)) {
                    projectDocumentFilePath = documentFilePath;
                }
                else if(Directory.GetFiles(solutionProjectDirectory).Count() == 0){
                    Console.WriteLine(documentName + " not exist.");
                    return false;
                }
                else if(File.Exists(solutionProjectDirectory + "\\" + documentName)){
                    projectDocumentFilePath = solutionProjectDirectory + "\\" + documentName;
                }
                else
                {
                    //找到一个最近时间戳的文档

                    //获取目录下所有文件的信息
                    FileInfo[] files = (new DirectoryInfo(solutionProjectDirectory)).GetFiles();
                    FileInfo theLastestFile = files.First();
                    foreach (var file in files)
                    {
                        if (file.LastWriteTime > theLastestFile.LastWriteTime)
                        {
                            theLastestFile = file;
                        }
                    }
                    projectDocumentFilePath = solutionProjectDirectory + "\\" + theLastestFile;
                }

               

 
                //传送文件开始的第一个数据包
                out_message = new Message();
                out_message.MessageBody = Encoding.Unicode.GetBytes(solutionName + ":" + projectName + ":" + documentName);
                out_message.Command = Message.CommandHeader.SendDocument;
                out_message.MessageFlag = Message.MessageFlagHeader.FileBegin;
                dataStream.Write(out_message.ToBytes(), 0, out_message.MessageLength);

                //传送文件内容
                using (FileStream fs = new FileStream(projectDocumentFilePath, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader br = new BinaryReader(fs);
                    byte[] buffer = new byte[8192];//2^13=8kb
                    try
                    {
                        int checksize;
                        do
                        {
                            checksize = br.Read(buffer, 0, 8192);
                            if (checksize > 0)
                            {
                                out_message = new Message();
                                out_message.Command = Message.CommandHeader.SendDocument;
                                out_message.MessageFlag = Message.MessageFlagHeader.FileMiddle;
                                out_message.MessageBody = new byte[checksize];
                                if (checksize < 8192)//最后一个数据包字节数不够
                                {
                                    for (int i = 0; i < checksize; i++)
                                        out_message.MessageBody[i] = buffer[i];
                                }
                                else
                                {
                                    buffer.CopyTo(out_message.MessageBody, 0);
                                }

                                dataStream.Write(out_message.ToBytes(), 0, out_message.MessageLength);
                            }
                        } while (checksize > 0);

                        //最后一个数据包
                        out_message = new Message();
                        out_message.Command = Message.CommandHeader.SendDocument;
                        out_message.MessageFlag = Message.MessageFlagHeader.FileEnd;
                        out_message.MessageBody = Encoding.Unicode.GetBytes("#");//该消息体最终被舍弃
                        dataStream.Write(out_message.ToBytes(), 0, out_message.MessageLength);

                        //读取服务器反馈，以判断服务器是否正确接收文件
                        Message in_message = Message.Parse(dataStream);
                        if (in_message.Command == Message.CommandHeader.ReceivedDocument
                            && Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("yes") == 0)
                        {
                            return true;
                        } 
                    }
                    catch (EndOfStreamException ex)
                    {
                        Console.WriteLine("SendDocument: "+ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SendDocument: " + ex.Message);
            }
            return false;
        }

        /// <summary>
        /// 客户端发送从服务器上获取任意格式文档的请求，得到服务器的反馈
        /// </summary>
        /// <param name="documentName"></param>
        /// <returns></returns>
        public Message GetDocumentRequest(string solutionName,string projectName,string documentName)
        {
            Message out_message = new Message();
            out_message.Command = Message.CommandHeader.GetDocumentRequest;
            out_message.MessageBody = Encoding.Unicode.GetBytes(solutionName+":"+projectName+":"+documentName);
            //获取服务器反馈 
            return StreamDataOutAndInHelper(out_message);
        }
        
        /// <summary>
        /// 客户端获取服务器任意格式的文件
        /// </summary>
        public Boolean GetDocument() 
        {
            //整个文件接收工作需要在这全部完成，阻塞方式
            try
            {
                FileStream fs;
                Message in_message;
                Message out_message;
                string solutionName = string.Empty;
                string projectName = string.Empty;
                string documentName = string.Empty;
                string documentNameAddTimestampWithRelativePath = string.Empty;

                do
                {                    
                    in_message = Message.Parse(dataStream);
                    switch (in_message.MessageFlag)
                    {
                        case Message.MessageFlagHeader.FileBegin:
                            //得到项目名
                            solutionName = Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[0];
                            //得到工程名
                            projectName = Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[1];
                            //得到文档名
                            documentName = Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[2];

                            //判断当前目录下是否存在该项目及工程文件夹
                            if (!Directory.Exists(solutionName + "\\" + projectName))
                            {
                                //在当前路径下创建项目文件夹                           
                                Directory.CreateDirectory(solutionName + "\\" + projectName);
                            }
                            //生成时间戳 格式：__2011_12_2_19_48_37
                            string timestamp = "__"+DateTime.Now.ToString().Replace(':', '_').Replace(' ', '_').Replace('/', '_');
                            documentNameAddTimestampWithRelativePath = solutionName + "\\" + projectName + "\\" + documentName + timestamp;
                            if (!File.Exists(documentNameAddTimestampWithRelativePath))
                            {
                                fs = File.Create(documentNameAddTimestampWithRelativePath);
                                fs.Close();
                            }
                            break;
                        case Message.MessageFlagHeader.FileMiddle:
                            if (!File.Exists(documentNameAddTimestampWithRelativePath))
                            {
                                Console.WriteLine(documentNameAddTimestampWithRelativePath + " not exist.");
                            }
                            using (fs = File.Open(documentNameAddTimestampWithRelativePath, FileMode.Append))
                            {
                                BinaryWriter bw = new BinaryWriter(fs);
                                bw.Write(in_message.MessageBody,0,in_message.MessageBody.Length);
                                bw.Close();
                                fs.Close();
                            }
                            break;
                        case Message.MessageFlagHeader.FileEnd:
                            //发送成功接收的确认数据包
                            out_message = new Message();
                            out_message.Command = Message.CommandHeader.ReceivedDocument;
                            out_message.MessageBody = Encoding.Unicode.GetBytes("yes");
                            dataStream.Write(out_message.ToBytes(),0,out_message.MessageLength);
                            return true;
                        default:
                            //发送接收失败的确认数据包
                            out_message = new Message();
                            out_message.Command = Message.CommandHeader.ReceivedDocument;
                            out_message.MessageBody = Encoding.Unicode.GetBytes("no");
                            dataStream.Write(out_message.ToBytes(), 0, out_message.MessageLength);
                            Console.WriteLine("The document file trasfer failed...");
                            //若存在接受不全的文件，删除
                            if (File.Exists(documentNameAddTimestampWithRelativePath))
                            {
                                File.Delete(documentNameAddTimestampWithRelativePath);
                            }
                            break;
                    }
                } while (in_message.MessageFlag == Message.MessageFlagHeader.FileBegin
                    || in_message.MessageFlag == Message.MessageFlagHeader.FileMiddle);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetDocument: "+ex.Message);
            }

            return false;
        }


        /// <summary>
        /// 请求添加用户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="passwd"></param>
        /// <param name="otherinfo"></param>
        /// <returns></returns>
        public Boolean AddUser(string name, string passwd, int groupid)
        {
            Message out_message = new Message();
            Message in_message;
            out_message.Command = Message.CommandHeader.AddUser;

            //消息体携带用户名、密码、其它信息
            out_message.MessageBody = Encoding.Unicode.GetBytes(name + ":" + passwd + ":" + groupid);

            //获取服务器反馈 -->//可以加个定时器设置认证超时
            in_message = StreamDataOutAndInHelper(out_message);

            //用户添加成功succeed，失败fail，用户已存在exist
            if (in_message.Command == Message.CommandHeader.AddUser)
            {
                if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("succeed") == 0)
                {
                    Console.WriteLine("用户添加成功！");
                    return true;
                }
                else if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("existed") == 0)
                {
                    Console.WriteLine("用户已存在！");
                    return false;
                }
                return false;
            }
            else
            {
                Console.WriteLine("用户添加失败！");
                return false;
            }
        }
        ///下面版本未在服务器端实现对应功能
        //public string AddUser(string name, string passwd,int groupid)
        //{
        //    Message out_message = new Message();
        //    Message in_message;
        //    out_message.Command = Message.CommandHeader.AddUser;

        //    //消息体携带用户名、密码、其它信息
        //    out_message.MessageBody = Encoding.Unicode.GetBytes(name + ":" + passwd+":"+groupid);

        //    //获取服务器反馈 -->//可以加个定时器设置认证超时
        //    in_message = StreamDataOutAndInHelper(out_message);

        //    //用户添加成功succeed，失败fail，用户已存在exist
        //    if (in_message.Command == Message.CommandHeader.AddUser)
        //    {
        //        if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("succeed") == 0)
        //        {
        //            Console.WriteLine("用户添加成功！");
        //            return "succeed";
        //        }
        //        else if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("existed") == 0)
        //        {
        //            Console.WriteLine("用户已存在！");
        //            return "existed";
        //        }
        //        return "fail";
        //    }
        //    else
        //    {
        //        Console.WriteLine("用户添加失败！");
        //        return "fail";
        //    }
        //}

        /// <summary>
        /// 请求添加用户权限
        /// </summary>
        /// <param name="name"></param>
        /// <param name="passwd"></param>
        /// <param name="otherinfo"></param>
        /// <returns></returns>
        public string AddPermission(string name, string projectid, int permissionlevel)
        {
            Message out_message = new Message();
            Message in_message;
            out_message.Command = Message.CommandHeader.AddPermission;

            //消息体携带用户名、工程id、权限级别
            out_message.MessageBody = Encoding.Unicode.GetBytes(name + ":" + projectid + ":" + permissionlevel);

            //获取服务器反馈 -->//可以加个定时器设置认证超时
            in_message = StreamDataOutAndInHelper(out_message);

            //权限添加成功succeed，失败fail，用户已存在exist
            if (in_message.Command == Message.CommandHeader.AddPermission)
            {
                if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("succeed") == 0)
                {
                    Console.WriteLine("权限添加成功！");
                    return "succeed";
                }
                else if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("notexisted") == 0)
                {
                    Console.WriteLine("用户不存在！");
                    return "notexisted";
                }
                else if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("existed") == 0)
                {
                    Console.WriteLine("权限已经存在！");
                    return "existed";
                }
                return "fail";
            }
            else
            {
                Console.WriteLine("权限添加失败！");
                return "fail";
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="passwd"></param>
        /// <param name="otherinfo"></param>
        /// <returns></returns>
        public string SearchUser(string name)
        {
            Message out_message = new Message();
            Message in_message;
            out_message.Command = Message.CommandHeader.SearchUser;

            //消息体携带用户名、密码、其它信息
            out_message.MessageBody = Encoding.Unicode.GetBytes(name);

            //获取服务器反馈 -->//可以加个定时器设置认证超时
            in_message = StreamDataOutAndInHelper(out_message);

            //用户添加成功succeed，失败fail，用户已存在exist
            if (in_message.Command == Message.CommandHeader.SearchUser)
            {
                if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("notexisted") == 0)
                {
                    Console.WriteLine("用户"+name+"不存在");
                    return "notexisted";
                }
                else
                {
                    string username = Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[0];
                    string passwd = Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[1];
                    string groupid = Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[2];
                    Console.WriteLine("用户名："+username+" 密码:"+passwd+" 用户组："+groupid);
                    return Encoding.Unicode.GetString(in_message.MessageBody);
                }
                return "fail";
            }
            else
            {
                Console.WriteLine("用户查询失败！");
                return "fail";
            }
        }

        public string SearchPermission(string name)
        {
            Message out_message = new Message();
            Message in_message;
            out_message.Command = Message.CommandHeader.SearchPermission;

            //消息体携带用户名、密码、其它信息
            out_message.MessageBody = Encoding.Unicode.GetBytes(name);

            //获取服务器反馈 -->//可以加个定时器设置认证超时
            in_message = StreamDataOutAndInHelper(out_message);

            //项目搜索成功succeed，失败fail，项目不存在notexisted
            if (in_message.Command == Message.CommandHeader.SearchPermission)
            {
                if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("notexisted") == 0)
                {
                    Console.WriteLine("项目" + name + "不存在");
                    return "notexisted";
                }
                else
                {
                    int permissionnum =Convert.ToInt32(Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[0]);
                    List<string> tooltypelist = new List<string>();
                    List<string> projectidlist = new List<string>();
                    List<string> statuslist = new List<string>();
                    for (int i = 0; i < permissionnum*3; i=i+3)
                    {
                        tooltypelist.Add(Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[i+1]);
                        projectidlist.Add(Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[i+2]);
                        statuslist.Add(Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[i+3]);
                    }
                    for(int i=0;i<permissionnum;i++)
                        Console.WriteLine("工具名称：" + tooltypelist[i] + " 工程ID:" + projectidlist[i] + " 在线状态:" + statuslist[i]);
                    return Encoding.Unicode.GetString(in_message.MessageBody);
                }
                return "fail";
            }
            else
            {
                Console.WriteLine("用户查询失败！");
                return "fail";
            }
        }


        public string SearchProjectsBySolution(string name)
        {
            Message out_message = new Message();
            Message in_message;
            out_message.Command = Message.CommandHeader.SearchSolution;

            //消息体携带用户名、密码、其它信息
            out_message.MessageBody = Encoding.Unicode.GetBytes(name);

            //获取服务器反馈 -->//可以加个定时器设置认证超时
            in_message = StreamDataOutAndInHelper(out_message);

            //用户添加成功succeed，失败fail，用户已存在exist
            if (in_message.Command == Message.CommandHeader.SearchSolution)
            {
                if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("notexisted") == 0)
                {
                    Console.WriteLine("用户" + name + "不存在");
                    return "notexisted";
                }
                else
                {
                    int permissionnum = Convert.ToInt32(Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[0]);
                    List<string> projectidlist = new List<string>();
                    List<string> levellist = new List<string>();
                    for (int i = 0; i < permissionnum * 2; i = i + 2)
                    {
                        projectidlist.Add(Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[i + 1]);
                        levellist.Add(Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[i + 2]);
                    }
                    for (int i = 0; i < permissionnum; i++)
                        Console.WriteLine("工程名称：" + projectidlist[i] + " 权限级别:" + levellist[i]);
                    return Encoding.Unicode.GetString(in_message.MessageBody);
                }
                return "fail";
            }
            else
            {
                Console.WriteLine("用户查询失败！");
                return "fail";
            }
        }
        /// <summary>
        /// 传入要发送的消息，发送给服务器，返回解析后服务器反馈消息
        /// </summary>
        /// <param name="out_message"></param>
        /// <returns></returns>
        private Message StreamDataOutAndInHelper(Message out_message){
            Message temp;

            //获得输出流
            dataStream = client.GetStream();
            

            while (!dataStream.CanWrite)
            {
                System.Threading.Thread.Sleep(1000);//sleep 1s
            }
            //将打包成message的信息写入输出流
            dataStream.Write(out_message.ToBytes(), 0, out_message.MessageLength);

            //读取服务器反馈
            temp = Message.Parse(dataStream);

            return temp;
        }


        //定时监控数据流，查看是否有推送信息
        public Boolean CheckPushMessage() 
        {
            dataStream = client.GetStream();
            while (!dataStream.CanRead) 
            {
                System.Threading.Thread.Sleep(1000);
            }

            throw new NotImplementedException();
        }

        //请求同步信息
    }
}
