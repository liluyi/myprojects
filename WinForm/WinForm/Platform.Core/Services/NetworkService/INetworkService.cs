using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Core.UI;
using Platform.Core;
using Platform.Core.Data;

namespace Platform.Core.Services
{
    /// <summary>
    /// 客户端连接服务器参数
    /// </summary>
    public class ConnectingArgs : EventArgs
    {
        public string hostname;
        public int port;
        public string username;
        public string password;
        public string tooltype;
        public string projectid;
        public ConnectingArgs(string hostname, int port, string username, string password,string tooltype,string prjid)
        {
            //服务器地址
            this.hostname = hostname;
            //服务器端口
            this.port = port;
            //用户名
            this.username = username;
            //密码
            this.password = password;
            //工具类型
            this.tooltype = tooltype;
            //工程id
            this.projectid = prjid;
        }
    }

    /// <summary>
    /// 权限验证参数
    /// </summary>
    public class AuthArgs : EventArgs
    {
        public string username;//用户名
        public string password;//密码
        public string tooltype;//工具类型
        public string projectid;//工程id
        public AuthArgs(string username, string password,string tooltype,string projectid)
        {
            //用户名
            this.username = username;
            //密码
            this.password = password;
            this.tooltype=tooltype;
            this.projectid=projectid;
        }
    }
    
    /// <summary>
    /// 请求信息参数
    /// </summary>
    public class RequestArgs : EventArgs
    {
        public string solutionname,projectname;
        public RequestArgs(string solutionName, string projectName)
        {
            this.solutionname = solutionName;
            this.projectname = projectName;
        }
    }

    public class SendDocArgs : EventArgs
    {
        public string solutionname, projectname, documentname, documentfilepath;
        public SendDocArgs(string solutionName, string projectName, string documentName, string documentFilePath)
        {
            this.solutionname = solutionName;
            this.projectname = projectName;
            this.documentname = documentName;
            this.documentfilepath = documentFilePath;
        }
    }
    
    public delegate string ConnectingHandler(ConnectingArgs args);
    public delegate Boolean DisConnectingHandler(ConnectingArgs args);
    public delegate string CheckAuthHandler(AuthArgs args);
    public delegate Message RequestHandler(RequestArgs  args);
    public delegate Boolean SendingHandler(SendDocArgs args);
    public delegate Message SendDocRequestHandler(SendDocArgs args);
    public delegate Boolean AcceptingHandler();

    public interface INetworkService:IService
    {
        //连接/断开服务器
        ConnectingHandler ConnectServer { get; }
        DisConnectingHandler DisConnectServer { get; }
        //验证权限
        CheckAuthHandler CheckAuth { get; }
        //发送/接受消息请求
        RequestHandler GetXMLRequest { get; }
        RequestHandler SendXMLRequest { get; }
        SendDocRequestHandler SendDocumentRequest { get; }
        SendDocRequestHandler GetDocumentRequest { get; }
        //发送/接收文件
        SendingHandler SendXML { get; }
        AcceptingHandler GetXML { get; }
        SendingHandler SendDocument { get; }
        AcceptingHandler GetDocument { get; }
    }
}
