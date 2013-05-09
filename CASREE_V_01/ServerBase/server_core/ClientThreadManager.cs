using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;

namespace ServerBase
{
    class ClientThreadManager
    {
        //当前连接的客户端列表,临界资源，加锁读写
        private static Dictionary<string, ClientInfo> clientList = new Dictionary<string, ClientInfo>();

        //定时处理函数委托
        public delegate void handleFunction(object source, ElapsedEventArgs e);

        public ClientThreadManager()
        {

        }

        public static int GetClientNumber()
        {//获取所有连接客户端的数目
            int clientNum = -1;
            lock (clientList)
            {
                clientNum = clientList.Count;
            }
            return clientNum;
        }

        public Dictionary<string, ClientInfo> GetClientList()
        {
            return clientList;
        }

        public static Boolean SetClientAliveTime(string ipWithPort)
        {
            Boolean modifyFlag = false;
            lock (clientList)
            {
                foreach (KeyValuePair<string, ClientInfo> s in clientList)
                {
                    if (s.Value.clientIPWithPort.CompareTo(ipWithPort) == 0)
                    {
                        //修改客户端最近心跳包接受时间
                        s.Value.SetLastAliveTime(DateTime.Now);
                        modifyFlag = true;
                    }
                }
            }
            return modifyFlag;
        }

        public void AddClientConnect(ClientInfo clientInfo)
        {
            lock (clientList)
            {
                clientList.Add(clientInfo.clientIPWithPort, clientInfo);
            }
        }

        public static Boolean RemoveClientConnect(string ip)
        {   //关掉指定ip&port客户端连接(考虑到同一IP的多个客户端连接，需要端口来区别)
            //或用托管线程的唯一标识符thread.ManagedThreadId来判断
            Boolean rmflag = false;
            lock (clientList)
            {
                foreach (KeyValuePair<string, ClientInfo> s in clientList)
                {
                    if (s.Key.CompareTo(ip) == 0)
                    {
                        //销毁线程，释放资源
                        try
                        {
                            s.Value.clientThread.Abort();
                            s.Value.clientThread.Join();
                        }
                        catch (ThreadAbortException ex)
                        {
                            Console.WriteLine("RemoveClientConnect: " + ex.Message);
                            Thread.ResetAbort();
                        }
                        //finally
                        //{
                        //    Console.Write("T3:");
                        //    Console.WriteLine(GetClientNumber());
                        //    Console.WriteLine(s.Key + " is offline. - Single");
                        //}
                    }
                }
                //从字典中移除
                if (clientList.Remove(ip))
                {
                    rmflag = true;
                }
                else
                {
                    rmflag = false;
                }

                return rmflag;
            }
        }

        public Boolean RemoveAllClientConnect()
        {//关闭所有客户端连接 

            Boolean rmAllFlag = false;
            lock (clientList)
            {
                foreach (KeyValuePair<string, ClientInfo> s in clientList)
                {
                    try
                    {
                        s.Value.clientThread.Abort();
                        s.Value.clientThread.Join();
                        Console.WriteLine(s.Key + " shutdown.");
                    }
                    catch (ThreadAbortException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Thread.ResetAbort();
                    }
                    finally
                    {
                        Console.WriteLine(s.Key + " failed to abort. -All");
                    }
                }
                if (clientList.Count == 0)
                    rmAllFlag = true;
                else
                    rmAllFlag = false;
            }

            return rmAllFlag;
        }
        
        //检查客户端连接状态定时器
        public static void checkClientActive(int minutes, handleFunction handle)
        {
            System.Timers.Timer t = new System.Timers.Timer();
            t.Interval = 1000 * 60 * minutes;//定时
            t.Elapsed += new ElapsedEventHandler(handle);
            t.AutoReset = true;
            t.Enabled = true;
        }

        public static void ClientOverTime(object source, ElapsedEventArgs e)
        { //客户端超时判断
           // foreach (KeyValuePair<string, ClientInfo> s in clientList)//遍历时不能直接更改集合
                for(int i=0;i<clientList.Count;i++)
                {
                    KeyValuePair<string, ClientInfo> temppair=clientList.ElementAt<KeyValuePair<string, ClientInfo>>(i);
                    if (DateTime.Now.Subtract(temppair.Value.lastAliveTime).TotalSeconds > 50) //测试给出了50s
                {
                    RemoveClientConnect(temppair.Key);
                }
            }

        }

        //负责向指定客户端推送信息
        public static Boolean PushMessage(ClientInfo clientInfo , string message)
        {
            try
            {
                NetworkStream dataStream = clientInfo.client.GetStream();
                Message push_message = new Message();

                //生成推送消息
                push_message.Command = Message.CommandHeader.Push;
                push_message.MessageBody = Encoding.Unicode.GetBytes(message);

                //推送
                dataStream.Write(push_message.ToBytes(), 0, push_message.MessageLength);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static Boolean SendMessageToClient(string ip,string message)
        {//将消息推送给指定客户端
            Boolean pushFlag = false;

            //遍历集合，找到客户端
            foreach (KeyValuePair<string, ClientInfo> s in clientList)
            {
                if (s.Key.CompareTo(ip) == 0)//找到指定ip的客户端
                {                   
                    pushFlag = PushMessage(s.Value,message);
                }
            }
            return pushFlag;

        }

        public static void SendMessageToPartialClient(List<string> ipList , string message)
        {//将消息推送给部分客户端

            foreach(var ip in ipList)
            {
                foreach (KeyValuePair<string, ClientInfo> s in clientList)
                {
                    if (s.Key.CompareTo(ip) == 0)
                    {
                        PushMessage(s.Value, message);
                    }
                }
            }
        }

        public static void SendMessageToAllClient(string message)
        {//将消息推送给所有已连接客户端
            foreach (KeyValuePair<string, ClientInfo> s in clientList)
            {
                PushMessage(s.Value,message);
            }
        }

        //测试
        static int count_print = 0;
        public static void printState()
        {
            Console.WriteLine("printStateNum: " + (++count_print).ToString());
            lock(clientList)
            {
                foreach (KeyValuePair<string, ClientInfo> s in clientList)
                {
                    Console.WriteLine(s.Key + " lastAliveTime: " + s.Value.lastAliveTime.ToString());
                }
            }
        }
        
        /// 定时轮询当前已连接客户端，查看状态
        //public void PollConnectedClients(object source, ElapsedEventArgs e)
        //{
        //    foreach (KeyValuePair<string, ClientInfo> s in clientList)
        //    {
        //        if (!s.Value.clientThread.IsAlive)//线程异常
        //        {
        //            //销毁线程
        //            //删除数据库中相应数据
        //            //删除链表中相应数据

        //        }
        //        线程管理，相当于对客户端的连接管理
        //        Suspend、Resume、Abort
        //    }
        //}

        //数据库操作部分,需要"定时"将客户端连接信息同步到服务器，供管理客户端读取
    }
}
