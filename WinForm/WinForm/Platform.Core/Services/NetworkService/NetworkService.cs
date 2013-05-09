using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Core.Services;
using Platform.Core;
using Platform.Core.Data;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Timers;

namespace Platform.Core.Services
{
    internal sealed class NetworkService:INetworkService
    {
        private string description = string.Empty;
        private ServiceState state = ServiceState.UnLoad;
        private InitServiceHandler initService;
        private EventHandler loadingService;
        private ConnectingHandler connectingServer;
        private DisConnectingHandler disconnectServer;
        private CheckAuthHandler checkAuth;
        private RequestHandler getXMLRequest;
        private RequestHandler sendXMLRequest;
        private SendDocRequestHandler getDocumentRequest;
        private SendDocRequestHandler sendDocumentRequest;
        private SendingHandler sendXML;
        private AcceptingHandler getXML;
        private SendingHandler sendDocument;
        private AcceptingHandler getDocument;
        ClientBase client;//=new ClientBase();//客户端实例
        NetworkStream dataStream;//客户端与服务器之间的数据流

        #region IService
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description=value;
            }
        }

        public string Name
        {
            get { return "NetworkService"; }
        }

        public ServiceLevel Level
        {
            get { return ServiceLevel.High; }
        }

        public ServiceState State
        {
            get
            {
                return state;
            }
            set
            {
                state=value;
            }
        }

        public InitServiceHandler InitService
        {
            get { return initService; }
        }

        public EventHandler LoadingService
        {
            get { return loadingService; }
        }
        #endregion

        #region INetworkService
        public ConnectingHandler ConnectServer
        {
            get { return connectingServer; }
        }

        public DisConnectingHandler DisConnectServer
        {
            get { return disconnectServer; }
        }

        public CheckAuthHandler CheckAuth
        {
            get { return checkAuth; }
        }

        public RequestHandler GetXMLRequest
        {
            get { return getXMLRequest; }
        }

        public RequestHandler SendXMLRequest
        {
            get { return sendXMLRequest; }
        }

        public SendingHandler SendXML
        {
            get { return sendXML; }
        }

        public AcceptingHandler GetXML
        {
            get { return getXML; }
        }

        public SendDocRequestHandler SendDocumentRequest
        {
            get { return sendDocumentRequest; }
        }

        public SendingHandler SendDocument
        {
            get { return sendDocument; }
        }

        public SendDocRequestHandler GetDocumentRequest
        {
            get { return getDocumentRequest; }
        }

        public AcceptingHandler GetDocument
        {
            get { return getDocument; }
        }
        #endregion

        public NetworkService()
        {
            initService = new InitServiceHandler(Init);
            loadingService = new EventHandler(OnLoadingService);
            connectingServer = new ConnectingHandler(OnConnectingServer);
            disconnectServer = new DisConnectingHandler(OnDisConnectServer);
            checkAuth = new CheckAuthHandler(OnCheckingAuth);
            getXMLRequest = new RequestHandler(OnGettingXmlRequest);
            getXML = new AcceptingHandler(OnGettingXml);
            sendXML = new SendingHandler(OnSendingXml);
            sendXMLRequest = new RequestHandler(OnSendingXmlRequest);
            getDocumentRequest = new SendDocRequestHandler(OnGettingDocumentRequest);
            sendDocumentRequest = new SendDocRequestHandler(OnSendingDocumentRequest);
            sendDocument = new SendingHandler(OnSendingDocument);
            getDocument = new AcceptingHandler(OnGettingDocument);
        }

        private void Init(StartUpSettings sus)
        {
            if (sus == null)
            {
                return;
            }
            Properties p = sus.ServiceProperties["NetworkService"] as Properties;

            if (p == null)
            {
                return;
            }

            description = p["description"] as string;
        }

        /// <summary>
        /// 初始化网络服务
        /// </summary>
        /// <param name="sender">调用者</param>
        /// <param name="args">参数</param>
        private void OnLoadingService(object sender, EventArgs args)
        {
            

        }

        /// <summary>
        /// 连接服务器并验证权限（用户名、密码、对工具的访问权限）
        /// </summary>
        /// <param name="args">服务器地址、端口、用户名、密码</param>
        /// <returns></returns>
        public string OnConnectingServer(ConnectingArgs args)
        {
            client = new ClientBase();
            return client.ConnectServer(args.hostname, args.port, args.username, args.password,args.tooltype,args.projectid);
        }

        /// <summary>
        /// 通过用户名、密码进行认证
        /// </summary>
        /// <param name="name"></param>
        /// <param name="passwd"></param>
        /// <returns></returns>
        public string OnCheckingAuth(AuthArgs args)
        {
            return client.CheckAuth(args.username, args.password,args.tooltype,args.projectid);
        }

        /// <summary>
        /// 断开服务器连接
        /// </summary>
        /// <param name="hostname"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public Boolean OnDisConnectServer(ConnectingArgs args)
        {
            return client.DisConnectServer();
        }

        /// <summary>
        /// 客户端发送从服务器上获取指定工程项目xml描述文件的请求，得到服务器的反馈
        /// 服务器传输给客户端亦需要客户端的同意接受后，服务器才发送文件给客户端 
        /// </summary>
        /// <param name="projectname"></param>
        public Message OnGettingXmlRequest(RequestArgs args)
        {
            return client.GetXmlRequest(args.solutionname,args.projectname);
        }

        /// <summary>
        /// 客户端接受服务器发过来的指定项目工程xml描述文件
        /// </summary>
        /// <returns></returns>
        public Boolean OnGettingXml()
        {
            return client.GetXml();
        }

        /// <summary>
        /// 客户端发送从客户端传指定工程项目xml描述文件projectname到服务器的请求，得到服务器的反馈
        /// 客户端传输文件给服务器需要服务器的权限确认后，客户端才能发送文件给服务器
        /// </summary>
        /// <param name="projectname"></param>
        /// <returns></returns>
        public Message OnSendingXmlRequest(RequestArgs args)
        {
            return client.SendXmlRequest(args.solutionname,args.projectname);
        }

        /// <summary>
        /// 发送客户端项目工程xml描述文件projectNameXml给服务器，整个文件的发送均在此方法内完成
        /// </summary>
        /// <param name="projectNameXml"></param>
        /// <returns></returns>
        public Boolean OnSendingXml(SendDocArgs args)
        {
            Boolean sent=false;
            Message in_message = OnSendingXmlRequest(new RequestArgs(args.solutionname,args.projectname));//发请求
            if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("allow") == 0)
            {
                sent=client.SendXml(args.solutionname,args.projectname,args.documentfilepath);
                Console.WriteLine("sending xml ...");
            }
            return sent;
        }

        /// <summary>
        /// 客户端发送任意格式文档到服务器的请求，得到服务器的反馈
        /// 客户端传输文件给服务器需要服务器的权限确认后，客户端才能发送文件给服务器
        /// </summary>
        /// <param name="documentName"></param>
        /// <returns></returns>
        public Message OnSendingDocumentRequest(SendDocArgs args)
        {
            return client.SendDocumentRequest(args.solutionname,args.projectname,args.documentname);
        }

        /// <summary>
        /// 客户端发送任意格式文档给服务器，整个文件的发送均在此方法内完成
        /// </summary>
        /// <param name="documentName"></param>
        public Boolean OnSendingDocument(SendDocArgs args)
        {
            Boolean sent = false;
            //请求发送文件
            Message in_message = OnSendingDocumentRequest(args);
            Console.WriteLine(Encoding.Unicode.GetString(in_message.MessageBody));
            //判断是否允许发送
            if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("allow") == 0)
            {
                //发送文件
                sent = client.SendDocument(args.solutionname,args.projectname,args.documentname,args.documentfilepath);
            }
            return sent;
        }

        /// <summary>
        /// 客户端发送从服务器上获取任意格式文档的请求，得到服务器的反馈
        /// </summary>
        /// <param name="documentName"></param>
        /// <returns></returns>
        public Message OnGettingDocumentRequest(SendDocArgs args)
        {
            return client.GetDocumentRequest(args.solutionname,args.projectname,args.documentname);
        }

        /// <summary>
        /// 客户端获取服务器任意格式的文件
        /// </summary>
        public Boolean OnGettingDocument()
        {
            Boolean received = false;
            //请求获取服务器端文件
            Message in_message = OnGettingDocumentRequest(new SendDocArgs("","","document001_Server.pdf",""));//文件名如何处理待定
            Console.WriteLine(Encoding.Unicode.GetString(in_message.MessageBody));

            //判断是否允许获取
            if (Encoding.Unicode.GetString(in_message.MessageBody).CompareTo("allow") == 0)
            {
                received=client.GetDocument();
            }
            return received;
        }
    }
}
