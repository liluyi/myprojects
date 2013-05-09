using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.IO;

using ServerBase.database;

namespace ServerBase
{
    class ClientBusinessManager
    {
        private static string DirectoryPath = System.Environment.CurrentDirectory + "\\Workspace\\";

        //用户名密码认证，权限分配读取
        public static Boolean Authenticate(NetworkStream dataStream,Message in_message,ClientInfo newClientInfo)
        {
            Message out_message = new Message();

            //登陆确认命令
            out_message.Command = Message.CommandHeader.LoginAck;

            //从输入信息中获取用户名、密码
            string name = Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[0];
            string passwd = Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[1];

            //用户名密码校验 
            Boolean isAuthSucceed = CheckUser(name, passwd);
            if (isAuthSucceed)//消息体需要根据数据库检索结果//同时初始化permission
            {//验证通过

                //初始化ClientInfo中name、passwd相应属性
                newClientInfo.name = name;
                newClientInfo.passwd = passwd;

                //初始化ClientInfo中permission相应属性
                newClientInfo.permissionList = Database.queryPermission(name);
                

                string allowSolutionsAndProjects = "allow";               
                out_message.MessageBody = Encoding.Unicode.GetBytes(allowSolutionsAndProjects);
                
                //打包输出信息,将输出信息写入输出流
                dataStream.Write(out_message.ToBytes(), 0, out_message.MessageLength);

                //确认之后发一个含有服务器所有项目工程名称的xml描述文件
                //xml描述文件由服务器动态生成
                //generateXml()
                ClientBusinessManager.SendSolutionProjectListXml(dataStream);

                return true;
            }
            else
            {  //验证未通过
                out_message.MessageBody = Encoding.Unicode.GetBytes("deny");
                //打包输出信息,将输出信息写入输出流
                dataStream.Write(out_message.ToBytes(), 0, out_message.MessageLength);
                return false;
            }
        }


        public static Boolean CheckUser(string name, string passd)
        {
            //数据库查询等操作
            User user = Database.queryUser(name);
            if (user != null && user.passwd.CompareTo(passd) == 0)
            {
                Console.WriteLine("name:" + user.name);
                Console.WriteLine("passwd:" + user.passwd);
                Console.WriteLine("groupId:" + user.groupId);
                return true;
            }

            return false;
        }

        public static Boolean SendAckToClientGetProjectVersionRequest(NetworkStream dataStream,Message in_message) 
        {
            string solutionName = string.Empty;
            string projectName = string.Empty;
            string solutionProjectDirectory = string.Empty;

            Boolean isAck = false;
            Message out_message = new Message();
            out_message.Command = Message.CommandHeader.GetProjectVersionAck;

            solutionName = Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[0];
            projectName = Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[1];
            if (CreateProjectVersionListXml(solutionName, projectName)) {
                out_message.MessageBody = Encoding.Unicode.GetBytes("allow");
                isAck = true;
            }

            dataStream.Write(out_message.ToBytes(), 0, out_message.MessageLength);

            return isAck;

        }

        /// <summary>
        /// 发送项目工程版本的历史信息给客户端
        /// </summary>
        /// <param name="dataStream"></param>
        /// <param name="solutionName"></param>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public static Boolean SendProjectVersionXmlToClient(NetworkStream dataStream, string solutionName,string projectName)
        {
            //打开本地文件，读取内容，分行写到message中,连续发送
            //第一个数据包消息体：文件名
            //最后一个数据包消息体：#
            try
            {
                Message out_message;
                using (StreamReader sr = new StreamReader(DirectoryPath +"\\"+solutionName+"\\"+projectName+"\\"
                    +solutionName + "_" + projectName, Encoding.Default))
//                using (StreamReader sr = new StreamReader(solutionName+"_"+projectName, Encoding.Default))
                {
                    //传送文件开始的第一个数据包
                    out_message = new Message();
                    out_message.MessageBody = Encoding.Unicode.GetBytes(solutionName + ":" + projectName);//项目名+工程名
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

                //读取客户端反馈，以判断客户端是否正确接收文件
                Message in_message = Message.Parse(dataStream);
                if (in_message.Command == Message.CommandHeader.ReceivedXml
                    && Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("yes") == 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SendXml: " + ex.Message);
            }
            return false;
        }


        /// <summary>
        /// 针对客户端获取xml文件的请求GetXmlRequest，服务器首先要确认客户端请求的合理性，然后决定是否发xml文件
        /// 判断文件的存在性，判断客户端的权限是否足够
        /// </summary>
        /// <param name="dataStream"></param>
        /// <param name="in_message"></param>
        /// <returns></returns>
        public static Boolean SendAckToClientGetXmlRequest(NetworkStream dataStream, Message in_message)
        {
            string solutionName = string.Empty;
            string projectName = string.Empty;
            string solutionProjectDirectory = string.Empty;

            Boolean isAck;
            Message out_message = new Message();
            out_message.Command = Message.CommandHeader.GetXmlAck;

            solutionName = Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[0];
            projectName = Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[1];
            
            //默认在当前目录下搜索项目及工程文件夹
            //solutionProjectDirectory = solutionName + "\\" + projectName;
            solutionProjectDirectory =DirectoryPath + solutionName + "\\" + projectName;
            
            //还需要添加权限判断


            //判断文件夹及存在性
            if (Directory.Exists(solutionProjectDirectory) // 文件夹存在性
                &&Directory.GetFiles(solutionProjectDirectory).Count() == 0)//客户端需要的xml描述文件存在性
            {
                Console.WriteLine(projectName + " is not exist.");
                isAck = false;
            }
            else
            {
                out_message.MessageBody = Encoding.Unicode.GetBytes("allow");
                isAck = true;
            }
            dataStream.Write(out_message.ToBytes(), 0, out_message.MessageLength);

            return isAck;
        }

        /// <summary>
        /// 生成项目工程列表
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Boolean CreateSolutionProjectListXml(string fileName) 
        {
            List<string> solutions =database.Database.querySolution();
            FileStream fs;

            fs = File.Open(fileName,FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs,Encoding.Unicode);
            sw.WriteLine("<Solutions>");
            foreach (var s in solutions) {
                sw.WriteLine("\t<Solution name=\"" + s.TrimEnd() + "\">");
                Solution projects = database.Database.queryProjects(s);
                if(projects != null)
                {
                    if (projects.predict > 0)
                        sw.WriteLine("\t\t<Project>predict</Project>");
                    if(projects.assign > 0)
                        sw.WriteLine("\t\t<Project>assign</Project>");
                    if (projects.analysis > 0)
                        sw.WriteLine("\t\t<Project>analysis</Project>");
                    if(projects.design > 0)
                        sw.WriteLine("\t\t<Project>design</Project>");
                    if (projects.test > 0)
                        sw.WriteLine("\t\t<Project>test</Project>");
                    if (projects.assess > 0)
                        sw.WriteLine("\t\t<Project>assess</Project>");

                }
                sw.WriteLine("\t</Solution>");
            }
            sw.WriteLine("</Solutions>");
            sw.Close();
            fs.Close();
            return true;
        }

        /// <summary>
        /// 生成工程历史版本信息xml文件
        /// </summary>
        /// <returns></returns>
        public static Boolean CreateProjectVersionListXml(string solutionName,string projectName)
        {
            FileStream fs;

            List<string> versions; 
            versions = database.Database.queryProjectVersion(solutionName, projectName);

            if (versions == null)
            {
                return false;
            }
            else
            {
                if (!Directory.Exists(DirectoryPath + "\\" + solutionName + "\\" + projectName))
                {
                    Directory.CreateDirectory(DirectoryPath + "\\" + solutionName + "\\" + projectName);
                }

                fs = File.Open(DirectoryPath +"\\"+solutionName+"\\"+projectName+"\\"
                    + solutionName+"_"+projectName, FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
                sw.WriteLine("<Solution name=\"" + solutionName + "\">");
                sw.WriteLine("\t<Project name=\"" + projectName + "\">");
                foreach (var v in versions)
                {
                    sw.WriteLine("\t<Version>"+v+"</Version>");
                }
                sw.WriteLine("\t</Project>");
                sw.WriteLine("</Solution>");
                sw.Close();
                fs.Close();
                return true;
            }
        }


        /// <summary>
        /// 权限范围内的发送项目及工程列表
        /// </summary>
        /// <param name="dataStream"></param>
        /// <returns></returns>
        public static Boolean SendSolutionProjectListXml(NetworkStream dataStream)
        {

            //当前程序运行主目录工作区文件下
            string fileName = DirectoryPath + "SolutionProjectList";

            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }
            if (!File.Exists(fileName))
            {
                CreateSolutionProjectListXml(fileName);
            }

            //打开本地文件，读取内容，分行写到message中,连续发送
            //第一个数据包消息体：文件名
            //最后一个数据包消息体：#
            try
            {
                Message out_message;
                using (StreamReader sr = new StreamReader(fileName, Encoding.Default))
                {
                    //传送文件开始的第一个数据包
                    out_message = new Message();
                    out_message.MessageBody = Encoding.Unicode.GetBytes(fileName.Split('\\')[fileName.Split('\\').Length-1]);
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

                //读取客户端反馈，以判断客户端是否正确接收文件
                Message in_message = Message.Parse(dataStream);
                if (in_message.Command == Message.CommandHeader.ReceivedXml
                    && Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("yes") == 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SendXml: " + ex.Message);
                return false;
            }
            
            return true;
        }


        /// <summary>
        /// 发送工程服务器端项目工程xml描述文件projectNameXml给客户端，整个文件的发送均在此方法内完成
        /// </summary>
        /// <param name="dataStream"></param>
        /// <param name="solutionName"></param>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public static Boolean SendXml(NetworkStream dataStream, string solutionName ,string projectName,string version)
        {
            string solutionProjectDirectory =DirectoryPath + solutionName+"\\"+projectName;
            string fileName = solutionProjectDirectory + "\\" + projectName + "_" + version;

            //找一个xml描述文件
            if (!File.Exists(fileName))
            {
                return false;
            }

            //打开本地文件，读取内容，分行写到message中,连续发送
            //第一个数据包消息体：文件名
            //最后一个数据包消息体：#
            try
            {
                Message out_message;
                using (StreamReader sr = new StreamReader(fileName, Encoding.Default))
                {
                    //传送文件开始的第一个数据包
                    out_message = new Message();
                    out_message.MessageBody = Encoding.Unicode.GetBytes(solutionName+":"+projectName);//项目名+工程名
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

                //读取客户端反馈，以判断客户端是否正确接收文件
                Message in_message = Message.Parse(dataStream);
                if (in_message.Command == Message.CommandHeader.ReceivedXml
                    && Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("yes") == 0)
                {
                    return true;
                }   
            }
            catch (Exception ex)
            {
                Console.WriteLine("SendXml: " + ex.Message);
            }
            return false;
        }

        /// <summary>
        /// 客户端发送项目工程XML描述文件给服务器请求，服务器加以确认，并将确认信息发送给客户端
        /// </summary>
        /// <param name="dataStream"></param>
        /// <param name="in_message"></param>
        /// <returns></returns>
        public static Boolean SendAckToClientSendXmlRequest(NetworkStream dataStream, Message in_message)
        {
            //判断客户端上传项目工程文件的权限

            Boolean isAck;
            Message out_message = new Message();
            out_message.Command = Message.CommandHeader.SendXmlAck;

            if (true)//检查permission
            {
                out_message.MessageBody = Encoding.Unicode.GetBytes("allow");
                isAck = true;
            }
            else
            {
                out_message.MessageBody = Encoding.Unicode.GetBytes("deny");
                isAck = false;
            }
            dataStream.Write(out_message.ToBytes(), 0, out_message.MessageLength);
            return isAck;

        }

        /// <summary>
        /// 服务器接受客户端传来的项目工程xml描述文件
        /// </summary>
        /// <param name="dataStream"></param>
        public static Boolean GetXml(NetworkStream dataStream)
        {
            //整个文件接收工作需要在这全部完成，阻塞方式
            FileStream fs;
            Message in_message;
            Message out_message;
            string solutionName = string.Empty;
            string projectName = string.Empty;
            string solutionProjectDirectory = string.Empty;
            //格式@主目录\\子目录\\工程描述文件
            string projectNameAddTimestampWithRelativePath = string.Empty;
            string timestamp = string.Empty;

            do
            {
                in_message = Message.Parse(dataStream);
                switch (in_message.MessageFlag)
                {
                    case Message.MessageFlagHeader.FileBegin://得到文件名，新建一个以该文件名命名的文件
                        solutionName = Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[0];
                        projectName = Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[1];

                        //项目工程文件夹
                        solutionProjectDirectory = DirectoryPath + solutionName + "\\" + projectName;

                        //判断当前目录下是否存在该项目及工程文件夹
                        if (!Directory.Exists(solutionProjectDirectory))
                        {
                            //在当前路径下创建项目文件夹                           
                            Directory.CreateDirectory(solutionProjectDirectory);
                        }

                        //生成时间戳 格式：__2011_12_2_19_48_37
                        timestamp = "__" + DateTime.Now.ToString().Replace(':', '_').Replace(' ', '_').Replace('/', '_');
                        projectNameAddTimestampWithRelativePath = solutionProjectDirectory + "\\" + projectName + timestamp;
                        if (!File.Exists(projectNameAddTimestampWithRelativePath))
                        {
                            fs = File.Create(projectNameAddTimestampWithRelativePath);
                            fs.Close();//需要关闭该文件，否则下面无法获取文件锁，进行文件读写
                        }
                        break;
                    case Message.MessageFlagHeader.FileMiddle://得到文件内容,采用追加方式写入文件
                        if (!File.Exists(projectNameAddTimestampWithRelativePath))
                        {
                            Console.WriteLine(projectName + " not exist." );
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

                        Console.WriteLine("database");
                        //更新数据库
                        database.Database.UpdateProject(solutionName,projectName,timestamp);

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
        /// 客户端请求获取任意文档，服务器给予确认
        /// </summary>
        /// <param name="dataStream"></param>
        /// <param name="in_message"></param>
        /// <returns></returns>
        public static Boolean SendAckToClientGetDocumentRequest(NetworkStream dataStream, Message in_message)
        {
            string solutionName = string.Empty;
            string projectName = string.Empty;
            string documentName = string.Empty;
            string solutionProjectDirectory = string.Empty;

            Boolean isAck;
            Message out_message = new Message();
            out_message.Command = Message.CommandHeader.GetDocumentAck;

            solutionName = Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[0];
            projectName = Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[1];
            documentName = Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[2];
            solutionProjectDirectory =DirectoryPath + solutionName + "\\" + projectName;
            //默认在当前目录下搜索项目工程xml描述文件

            //还需要添加权限


            //判断文件存在性
            if (Directory.GetFiles(solutionProjectDirectory).Count() == 0 
                || !File.Exists(solutionProjectDirectory+"\\"+documentName))//客户端需要的当前服务器无法提供
            {
                out_message.MessageBody = Encoding.Unicode.GetBytes("deny");
                Console.WriteLine(documentName + " is not exist.");
                isAck = false;
            }
            else
            {
                out_message.MessageBody = Encoding.Unicode.GetBytes("allow"); ;
                isAck = true;
            }
            dataStream.Write(out_message.ToBytes(), 0, out_message.MessageLength);

            return isAck;
        }

        /// <summary>
        /// 发送项目工程下所有文件
        /// </summary>
        /// <param name="dataStream"></param>
        /// <param name="solutionName"></param>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public static Boolean SendDocument(NetworkStream dataStream, string solutionName,string projectName){

            string solutionProjectDirectory = DirectoryPath + solutionName + "\\" + projectName;
            if (!Directory.Exists(solutionProjectDirectory))
            {
                return false;
            }
            else
            {
                //遍历文件夹发送所有文件
                foreach (var s in Directory.GetFiles(solutionProjectDirectory))
                {
                    SendDocument(dataStream, solutionName, projectName, s.Split('_')[0]);//获得文件名
                }
                return true;
            }
        }


        /// <summary>
        /// 服务器发送任意格式文档给客户端
        /// </summary>
        /// <param name="dataStream"></param>
        /// <param name="documentName"></param>
        public static Boolean SendDocument(NetworkStream dataStream, string solutionName,string projectName,string documentName)
        {
            string solutionProjectDirectory = DirectoryPath + solutionName + "\\" + projectName;
            string filenameWithRelativePath = DirectoryPath + solutionProjectDirectory + "\\" + documentName;

            try
            {
                Message out_message;

                //传送文件开始的第一个数据包
                out_message = new Message();
                out_message.MessageBody = Encoding.Unicode.GetBytes(solutionName + ":" + projectName + ":" + documentName);
                out_message.Command = Message.CommandHeader.SendDocument;
                out_message.MessageFlag = Message.MessageFlagHeader.FileBegin;
                dataStream.Write(out_message.ToBytes(), 0, out_message.MessageLength);

                //传送文件内容
                using (FileStream fs = new FileStream(filenameWithRelativePath, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader br = new BinaryReader(fs);
                    byte[] buffer = new byte[8192];//2^13
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
                        //out_message.MessageBody = new byte[1];
                        out_message.MessageBody = Encoding.Unicode.GetBytes("#");//该消息体最终被舍弃
                        dataStream.Write(out_message.ToBytes(), 0, out_message.MessageLength);

                        //读取客户端反馈，以判断客户端是否正确接收文件
                        Message in_message = Message.Parse(dataStream);
                        if (in_message.Command == Message.CommandHeader.ReceivedDocument
                            && Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("yes") == 0)
                        {
                            return true;
                        }  
                    }
                    catch (EndOfStreamException ex)
                    {
                        Console.WriteLine("SendDocument: " + ex.Message);
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
        /// 客户端请求发送任意格式文档到服务器，服务器给予确认
        /// </summary>
        /// <param name="dataStream"></param>
        /// <param name="in_message"></param>
        /// <returns></returns>
        public static Boolean SendAckToClientSendDocumentRequest(NetworkStream dataStream, Message in_message)
        {
            //判断客户端上传项目工程文件的权限

            Boolean isAck;
            Message out_message = new Message();
            out_message.Command = Message.CommandHeader.SendXmlAck;

            if (true)//检查permission
            {
                out_message.MessageBody = Encoding.Unicode.GetBytes("allow");
                isAck = true;
            }
            else
            {
                out_message.MessageBody = Encoding.Unicode.GetBytes("deny");
                isAck = false;
            }
            dataStream.Write(out_message.ToBytes(), 0, out_message.MessageLength);
            return isAck;
        }

        /// <summary>
        /// 服务器接受客户端发送过来的任意格式的文档
        /// </summary>
        /// <param name="dataStream"></param>
        /// <param name="?"></param>
        public static Boolean GetDocument(NetworkStream dataStream)
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
                        case Message.MessageFlagHeader.FileBegin://得到文件名，新建一个以该文件名命名的文件
                            solutionName = Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[0];
                            projectName = Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[1];
                            documentName = Encoding.Unicode.GetString(in_message.MessageBody).Split(':')[2];

                            //判断当前目录下是否存在该项目及工程文件夹
                            if (!Directory.Exists(DirectoryPath + solutionName + "\\" + projectName))
                            {
                                //在当前路径下创建项目文件夹                           
                                Directory.CreateDirectory(DirectoryPath + solutionName + "\\" + projectName);
                            }
                            //生成时间戳 格式：__2011_12_2_19_48_37
                            string timestamp = "__" + DateTime.Now.ToString().Replace(':', '_').Replace(' ', '_').Replace('/', '_');
                            documentNameAddTimestampWithRelativePath = DirectoryPath + solutionName + "\\" + projectName + "\\" + documentName + timestamp;
                            
                            if (!File.Exists(documentNameAddTimestampWithRelativePath))
                            {
                                fs = File.Create(documentNameAddTimestampWithRelativePath);
                                fs.Close();
                            }
                            break;
                        case Message.MessageFlagHeader.FileMiddle:
                            if (!File.Exists(documentNameAddTimestampWithRelativePath))
                            {
                                Console.WriteLine(documentName + " not exist.");
                            }
                            using (fs = File.Open(documentNameAddTimestampWithRelativePath, FileMode.Append))
                            {
                                BinaryWriter bw = new BinaryWriter(fs);
                                bw.Write(in_message.MessageBody, 0, in_message.MessageBody.Length);
                                bw.Close();
                                fs.Close();
                            }
                            break;
                        case Message.MessageFlagHeader.FileEnd:
                            //发送成功接收的确认数据包
                            out_message = new Message();
                            out_message.Command = Message.CommandHeader.ReceivedDocument;
                            out_message.MessageBody = Encoding.Unicode.GetBytes("yes");
                            dataStream.Write(out_message.ToBytes(), 0, out_message.MessageLength);
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
                Console.WriteLine("GetDocument: " + ex.Message);
            }

            return false;
        }

    }
}
