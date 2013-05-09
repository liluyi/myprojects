using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

using ServerBase.database;

namespace ServerBase
{
    /// <summary>
    /// 存储已连接服务器的所有客户端信息
    /// </summary>
    class ClientInfo
    {
        public string name;
        public string passwd;
        public string clientIPWithPort = string.Empty;
        public DateTime lastAliveTime;//最近一次心跳包到达时间点
        public List<Permission> permissionList = new List<Permission>(); //客户端权限 //待考虑具体类型，构造一个权限类型类
        public TcpClient client;
        public Thread clientThread;

        public ClientInfo() 
        {
            
        }

        public ClientInfo(string clientIPWithPort, DateTime connectTime, TcpClient client)
        {
            this.clientIPWithPort = clientIPWithPort;
            this.lastAliveTime = connectTime;
            this.client = client;
        }
       
        public DateTime GetLastAliveTime()
        {
            return this.lastAliveTime;
        }

        public void SetLastAliveTime(DateTime newTime) {
            this.lastAliveTime = newTime;
        }

               
    }
}
