using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;
using Platform.Core.Services;
using Platform.Core.Exceptions;

namespace Platform.Core
{
    /// <summary>
    /// 服务管理者
    /// </summary>
    public sealed class ServicesManager
    {
        /// <summary>
        /// 自启动服务列表
        /// </summary>
        private List<IService> autostart_serviceList = new List<IService>();

        /// <summary>
        /// 自定义服务列表
        /// </summary>
        private List<IService> userdefined_servicesList = new List<IService>();

        /// <summary>
        /// 自定义服务列表名称
        /// </summary>
        private List<string> autostart_serviceListName = new List<string>();

        /// <summary>
        /// 日志服务
        /// </summary>
        private ILoggingService loggingService;

        /// <summary>
        /// 插件服务
        /// </summary>
        private IPluginService pluginService;

        /// <summary>
        /// UI服务
        /// </summary>
        private IUIService uiService;

        /// <summary>
        /// 文件服务
        /// </summary>
        private IFileService fileService;

        /// <summary>
        /// 工程服务
        /// </summary>
        private IProjectService projectService;

        /// <summary>
        /// 网络服务
        /// </summary>
        private INetworkService networkService;

        /// <summary>
        /// 加载自启动失败的任务
        /// </summary>
        public int FailedService = 0;

        
        /// <summary>
        /// 静态变量，即单例的实例
        /// </summary>
        private static ServicesManager defaultServicesManager = new ServicesManager();

        #region 常用Services

        /// <summary>
        /// 日志服务
        /// </summary>
        public ILoggingService LoggingService
        {
            get
            {
                if (loggingService == null)
                {
                    foreach (IService s in autostart_serviceList)
                    {
                        if (s is ILoggingService)
                        {
                            loggingService = s as ILoggingService;
                            break;
                        }
                    }
                    CheckServiceRunning(loggingService, true);
                }
                
                return loggingService;
            }
        }

        /// <summary>
        /// 插件服务
        /// </summary>
        public IPluginService PluginsService
        {
            get
            {
                if (pluginService == null)
                {
                    foreach (IService s in autostart_serviceList)
                    {
                        if (s is IPluginService)
                        {
                            pluginService = s as IPluginService;
                            break;
                        }
                    }
                    CheckServiceRunning(pluginService, true);
                }
                
                return pluginService;
            }
        }

        /// <summary>
        /// UI服务
        /// </summary>
        public IUIService UIService
        {
            get
            {
                if (uiService == null)
                {
                    foreach (IService s in autostart_serviceList)
                    {
                        if (s is IUIService)
                        {
                            uiService = s as IUIService;
                            break;
                        }
                    }
                    CheckServiceRunning(uiService, true);
                }
                
                return uiService;
            }
        }

        /// <summary>
        /// 文件服务
        /// </summary>
        public IFileService FileService
        {
            get
            {
                if (fileService == null)
                {
                    foreach (IService s in autostart_serviceList)
                    {
                        if (s is IFileService)
                        {
                            fileService = s as IFileService;
                            break;
                        }
                    }
                    CheckServiceRunning(fileService, true);
                }
                
                return fileService;
            }
        }

        /// <summary>
        /// 工程服务
        /// </summary>
        public IProjectService ProjectService
        {
            get
            {
                if (projectService == null)
                {
                    foreach (IService s in autostart_serviceList)
                    {
                        if (s is IProjectService)
                        {
                            projectService = s as IProjectService;
                            break;
                        }
                    } 
                    CheckServiceRunning(projectService, true);
                }
               
                return projectService;
            }
        }

        public INetworkService NetworkService
        {
            get
            {
                if (networkService == null)
                {
                    foreach (IService s in autostart_serviceList)
                    {
                        if (s is INetworkService)
                        {
                            networkService = s as INetworkService;
                            break;
                        }
                    }
                    CheckServiceRunning(networkService, true);
                }

                return networkService;
            }
        }

        /// <summary>
        /// 检查自定义服务是否合法
        /// </summary>
        /// <param name="service">服务</param>
        /// <returns>是否合法</returns>
        private bool CheckServiceRunning(IService service,bool throwOnerror)
        {
            if (service == null || service.State != ServiceState.Load)
            {
                if (throwOnerror)
                {
                    throw new ServiceNotInvaildException();
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        /// <summary>
        /// 服务管理者(单例)
        /// </summary>
        public static ServicesManager ServicesManagerSingleton
        {
            get
            {
                return defaultServicesManager;
            }
        }

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private ServicesManager()
        {
            //手动添加自启动服务，或者读取配置文件自动构建自启动服务
            autostart_serviceListName.Add("LoggingService");
            autostart_serviceListName.Add("UIService");
            autostart_serviceListName.Add("PluginsService");
            autostart_serviceListName.Add("FileService");
            autostart_serviceListName.Add("ProjectService");
            autostart_serviceListName.Add("NetworkService");

            //初始化并加载自动启服务
            InitCoreServices();
        }

        /// <summary>
        /// 加载自启动服务
        /// </summary>
        internal void InitCoreServices()
        {
            foreach (string s in autostart_serviceListName)
            {
                
                IService service = ServicesFactory.CreateAutoStartService(s,true); //服务工厂构建服务
                if (service != null)
                {
                    SystemLogging.SystemLoggingSingleton.Debug("加载服务:" + s + "成功");
                    service.State = ServiceState.UnLoad;
                    autostart_serviceList.Add(service);
                }
            }
        }

        /// <summary>
        /// 初始化自启动服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        internal void InitAutoStartService(object sender, EventArgs args)
        {
            foreach (IService s in autostart_serviceList)
            {
                if (s.InitService != null)
                {
                    try
                    {
                        s.InitService(sender as StartUpSettings);
                        s.State = ServiceState.Inital;
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine("加载服务" + s.Name + "失败\n"+ex.StackTrace);
                        s.State = ServiceState.Stop;
                    }
                }
            }
        }

        /// <summary>
        /// 加载服务管理器中注册的服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        internal void LoadAutoStartService(object sender, EventArgs args)
        {
            Form mainform= sender as Form;
            if(mainform==null)
            {
                return;
            }
            foreach (IService s in autostart_serviceList)
            {
                if (s.State != ServiceState.Stop)
                {
                    if (s.LoadingService != null)
                    {
                        try
                        {
                            s.LoadingService(sender, args);
                            s.State = ServiceState.Load;
                        }
                        catch
                        {
                            s.State = ServiceState.Stop;
                            FailedService++;
                        }
                    }
                    else//直接将状态转变为Load
                    {
                        s.State = ServiceState.Load;
                    }
                }
                else
                {
                    FailedService++;
                }
            }
        }

        /// <summary>
        /// 初始化自定义服务
        /// </summary>
        /// <param name="service"></param>
        internal void InitUserDefinedService(IService service)
        {
            try
            {
                if (service.InitService != null)
                {
                    service.InitService(null);
                }
                service.State = ServiceState.Inital;
            }
            catch
            {
                service.State = ServiceState.Stop;
                throw new UserDefinedServiceNotInvaildException();
            }
        }

        /// <summary>
        /// 加载自定义服务
        /// </summary>
        /// <param name="service">自定义服务</param>
        internal void LoadUserDefinedService(IService service)
        {
            try
            {
                if (service.LoadingService != null)
                {
                    service.LoadingService(null, null);
                }
                service.State = ServiceState.Load;
            }
            catch
            {
                service.State = ServiceState.Stop;
                throw new UserDefinedServiceNotInvaildException();
            }
        }
        /// <summary>
        /// 注册自定义服务，并将服务加载
        /// </summary>
        /// <param name="serviceProperty">自定义服务属性</param>
        /// <param name="asm">自定义服务所在程序集</param>
        /// <returns>是否成功注册</returns>
        internal bool RegisterUserService(Properties serviceProperty, Assembly asm)
        {
            //从服务工厂生成自定义服务
            IService service = ServicesFactory.CreateUserDefinedService(serviceProperty["class"] as string, asm);

            service.Description = serviceProperty["description"] as string;
            service.State = ServiceState.UnLoad;

            //初始化服务
            InitUserDefinedService(service);

            //加载服务
            LoadUserDefinedService(service);

            //添加服务
            userdefined_servicesList.Add(service);
            return true;
        }

        /// <summary>
        /// 通过服务名称获取自定义服务
        /// </summary>
        /// <param name="servicename">自定义服务名称</param>
        /// <returns>自定义服务</returns>
        public IService GetUserDefinedService(string servicename)
        {
            IService service = null;
            foreach (IService s in userdefined_servicesList)
            {
                if (s.Name == servicename)
                {
                    service = s;
                    break;
                }
            }
            return service;
        }
    }

    /// <summary>
    /// 服务工厂
    /// </summary>
    internal sealed class ServicesFactory
    {
        /// <summary>
        /// 创建自启动服务
        /// </summary>
        /// <param name="servicename">自启动服务</param>
        /// <param name="throwOnerror">出错是否抛出异常</param>
        /// <returns>自启动服务</returns>
        public static IService CreateAutoStartService(string servicename,bool throwOnerror)
        {
            IService service = null;

            switch (servicename)
            {
                case "LoggingService":
                    service = new CommonLoggingService() ;
                    break;
                case "UIService":
                    service = new UIService();
                    break;
                case "PluginsService":
                    service = new PluginsService();
                    break;
                case "FileService":
                    service = new FileService();
                    break;
                case "ProjectService":
                    service = new ProjectService();
                    break;
                case "NetworkService":
                    service = new NetworkService();
                    break;
                default:
                    if (throwOnerror)
                    {
                        throw new ServiceNotExsitException();
                    }
                    break;
            }
            return service;
        }

        /// <summary>
        /// 创建自启动服务
        /// </summary>
        /// <param name="servicename">服务名称</param>
        /// <returns>自启动服务</returns>
        public static IService CreateAutoStartService(string servicename)
        {
            return CreateAutoStartService(servicename, true);
        }

        /// <summary>
        /// 创建自定义服务
        /// </summary>
        /// <param name="serviceclass">自定义服务全名</param>
        /// <param name="asm">自定义服务程序集</param>
        /// <param name="throwOnerror">当出错时是否抛出异常</param>
        /// <returns>自定义服务</returns>
        public static IService CreateUserDefinedService(string serviceclass, Assembly asm, bool throwOnerror)
        {
            IService service=null;
            try
            {
                service = (IService)asm.CreateInstance(serviceclass);
            }
            catch
            {
                if (throwOnerror)
                {
                    throw new UserDefinedServiceNotInvaildException();
                }
            }
            return service;
        }

        /// <summary>
        /// 创建自定义服务
        /// </summary>
        /// <param name="classfullname">自定义服务全名</param>
        /// <param name="asm">自定义服务程序集</param>
        /// <returns>自定义服务</returns>
        public static IService CreateUserDefinedService(string classfullname, Assembly asm)
        {
            return CreateUserDefinedService(classfullname,asm, true);
        }
    }
}