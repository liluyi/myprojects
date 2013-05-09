using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace ServerBase
{
    class ServerManager
    {
        ClientThreadManager clientManager;
  
        public void ServerStart(IPAddress ip, int port)
        {
            Console.WriteLine("Server is running ... ");
            TcpListener listener = new TcpListener(ip, port);
            listener.Start();//开始侦听
            Console.WriteLine("Start Listening ...");
            clientManager = new ClientThreadManager();

            //开启定时检测客户端状态函数，每分钟检测一次
            ClientThreadManager.checkClientActive(1,ClientThreadManager.ClientOverTime);

            while (true)
            {
                // 获取一个连接，同步方法，在此处中断
                TcpClient client = listener.AcceptTcpClient();

                ClientInfo newClientInfo = new ClientInfo(
                    client.Client.RemoteEndPoint.ToString(),
                    DateTime.Now,
                    client);

                ServerBase dealClient = new ServerBase(newClientInfo);

                //在此需要将客户端连接信息添加到clientList，动态监控整个服务器的连接状况
                clientManager.AddClientConnect(newClientInfo);
                //clientManager.AddClientConnect(
                //    new ClientInfo(client.Client.RemoteEndPoint.ToString(),
                //        DateTime.Now,dealClient.permission,client,dealClient.clientThread));
                Console.WriteLine("已连接客户端数目：" + ClientThreadManager.GetClientNumber());//测试

            }
        }

        public void SeverStop() 
        {
            //遍历clienList,查看是否还有活动线程，先等待固定时间? 超时销毁它
            throw new NotImplementedException();
        }

        
        
    }
}
