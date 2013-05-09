using System;
using System.Reflection;
using System.Diagnostics;
using System.IO;

using Platform.Core;
using Platform.Core.Exceptions;
using Platform.Core.Data;

namespace Platform.Core.Services
{
    /// <summary>
    /// 插件服务
    /// </summary>
    internal sealed class PluginsService :IPluginService
    {
        private string description = string.Empty;
        private ServiceState state = ServiceState.UnLoad;
        private string pluginroot = string.Empty;

        private InitServiceHandler initService = null;

        private PluginInsertHandler insertplugin = null;
        private PluginRemoveHandler removeplugin = null;

        private CreatePluginEventHandler createplugin = null;

        private DealRequstHandler dealrequst = null;

        #region IService Members

        /// <summary>
        /// 服务描述
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string Name
        {
            get
            {
                return "PluginService";
            }
        }

        /// <summary>
        /// 服务级别
        /// </summary>
        public ServiceLevel Level
        {
            get
            {
                return ServiceLevel.High;
            }
        }

        /// <summary>
        /// 服务状态
        /// </summary>
        public ServiceState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

        /// <summary>
        /// 初始化服务委托
        /// </summary>
        public InitServiceHandler InitService
        {
            get
            {
                return initService;
            }
        }

        /// <summary>
        /// 加载服务委托
        /// </summary>
        public EventHandler LoadingService
        {
            get
            {
                return null;
            }
        }

        #endregion

        #region IPluginService Members

        /// <summary>
        /// 插件根目录
        /// </summary>
        public string PluginsRoot
        {
            get
            {
                return pluginroot;
            }
            set
            {
                pluginroot = value;
            }
        }

        /// <summary>
        /// 插入插件至插件树委托
        /// </summary>
        public PluginInsertHandler InsertPlugin
        {
            get
            {
                return insertplugin;
            }
        }

        /// <summary>
        /// 插件树中移除插件委托
        /// </summary>
        public PluginRemoveHandler RemovePlugin
        {
            get
            {
                return removeplugin;
            }
        }

        /// <summary>
        /// 构建插件时委托
        /// </summary>
        public CreatePluginEventHandler CreatePlugin
        {
            get
            {
                return createplugin;
            }
        }

        /// <summary>
        /// 处理插件请求时委托
        /// </summary>
        public DealRequstHandler DealRequst
        {
            get
            {
                return dealrequst;
            }
        }

        #endregion

        /// <summary>
        /// 构造函数,赋值委托
        /// </summary>
        public PluginsService()
        {
            initService = new InitServiceHandler(Init);

            createplugin = new CreatePluginEventHandler(CreatePluginFromAddin);

            insertplugin = new PluginInsertHandler(OnInsertPlugin);

            removeplugin = new PluginRemoveHandler(OnRemovePlugin);

            dealrequst = new DealRequstHandler(OnDealRequst);
        }

        /// <summary>
        /// 初始化服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Init(StartUpSettings sus)
        {
            if (sus == null )
            {
                return;
            }

            Properties p = sus.ServiceProperties["PluginsService"] as Properties;

            if (p == null)
            {
                return;
            }

            pluginroot = p["root"] as string;
            description = p["description"] as string;
        }

        /// <summary>
        /// 检查Addin文件是否合法
        /// </summary>
        /// <param name="asm_property">程序集属性</param>
        /// <param name="plugininfo_property">Info属性</param>
        /// <param name="mutableResource_property">可活动资源属性</param>
        /// <param name="project_property">工程属性</param>
        /// <param name="service_proerty">服务属性(可以为空)</param>
        /// <returns></returns>
        private bool checkProperties(Properties asm_property, Properties plugininfo_property, 
                                     Properties mutableResource_property,Properties project_property,
                                     Properties service_proerty)
        {
            if (asm_property == null || plugininfo_property == null || mutableResource_property == null || project_property == null)
            {
                SystemLogging.SystemLoggingSingleton.Error("插件配置文件:缺少配置节点");
                return false;
            }

            //Assmebly检查
            if (asm_property["assembly"] == null)
            {
                SystemLogging.SystemLoggingSingleton.Error("插件配置文件:程序集节点不完整");
                return false;
            }

            //PluginInfo检查
            if(plugininfo_property["author"]==null || plugininfo_property["description"]==null
                || plugininfo_property["token"] ==null || plugininfo_property["icon"] ==null
                || plugininfo_property["name"] ==null || plugininfo_property["type"]==null
                || plugininfo_property["class"] ==null)
            {
                SystemLogging.SystemLoggingSingleton.Error("插件配置文件:Info节点不完整");
                return false;
            }
            
            //MutableResource检查
            if (mutableResource_property["class"] == null)
            {
                SystemLogging.SystemLoggingSingleton.Error("插件配置文件:可活动资源节点不完整");
                return false;
            }

            //ProjectProperty检查
            if (project_property["class"] == null || project_property["suffix"] == null )
            {
                SystemLogging.SystemLoggingSingleton.Error("插件配置文件:工程节点不完整");
                return false;
            }

            //ServiceProperty检查
            if (service_proerty != null &&
                ( (service_proerty["class"] ==null ) || service_proerty["name"]==null || service_proerty["description"]==null))
            {
                SystemLogging.SystemLoggingSingleton.Error("插件配置文件:自定义服务节点不完整");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 平台初始化时构建插件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private IPlugin CreatePluginFromAddin(CreatePluginArgs args)
        {
            Properties asm_property;
            Properties plugininfo_property;
            Properties mutableResource_property;
            Properties project_property;
            Properties service_property;
            string addin_file = args.AddinFile;

            //提取程序集信息
            asm_property = PropertyAnalyse.GetProperties(addin_file, "Property/Assembly");

            //提取PluginInfo信息
            plugininfo_property = PropertyAnalyse.GetProperties(addin_file, "Property/PluginInfo");

            //提取可活动资源信息
            mutableResource_property= PropertyAnalyse.GetProperties(addin_file,"Property/MutableResource");

            //提取工程信息
            project_property = PropertyAnalyse.GetProperties(addin_file, "Property/Project");

            //提取自定义服务信息
            try
            {
                service_property = PropertyAnalyse.GetProperties(addin_file, "Property/Service");
            }
            catch
            {
                service_property = null;
            }

            //如果Addin文件不合法，则抛出异常
            if (!checkProperties(asm_property, plugininfo_property, mutableResource_property, project_property, service_property))
            {
                throw new AddinFileNotInvaildException();
            }

            //设置PluginInfo信息
            args.Info.Author = plugininfo_property["author"] as string;
            args.Info.Description = plugininfo_property["description"] as string;
            args.Info.Token = plugininfo_property["token"] as string;
            args.Info.Ico = plugininfo_property["ico"] as string;
            args.Info.Name = plugininfo_property["name"] as string;
            args.Info.Suffix = project_property["suffix"] as string;

            //插件类型转换
            try
            {
                args.Info.Type = (PluginType)(Enum.Parse(typeof(PluginType), plugininfo_property["type"] as string));
            }
            catch
            {
                throw new AddinFileNotInvaildException();
            }

            //获取程序集目录和插件强名
            string dir = Path.GetDirectoryName(addin_file);
            string assemblyfile = asm_property["assembly"] as string;
            assemblyfile = Path.Combine(dir, assemblyfile);
            string pluginclassname = plugininfo_property["class"] as string;

            Assembly asm = null;
            IPlugin plugin = null;

            //加载程序集
            try
            {
                asm = Assembly.LoadFrom(assemblyfile);
            }
            catch
            {
                throw new AssemblyRefusedException();
            } 
            //构建插件
            try
            {
                plugin = (IPlugin)asm.CreateInstance(pluginclassname);
            }
            catch
            {
                throw new PluginBuildErrorException();
            }

            //有自定义服务则构建自定义服务
            if (service_property != null)
            {
                ServicesManager.ServicesManagerSingleton.RegisterUserService(service_property, asm);
            }
            //配置插件Token
            plugin.Token = plugininfo_property["token"] as string;

            //与可活动资源区关联
            plugin.MutableResourceClassFullName = mutableResource_property["class"] as string;

            //与工程相关联
            plugin.ProjectClassFullName = project_property["class"] as string;
            plugin.ProjectSuffix = project_property["suffix"] as string;

            return plugin;
        }

        /// <summary>
        /// 在插件构建完之后往PluginManger中插入相关信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private bool OnInsertPlugin(InsertPluginArgs args)
        {
            IPlugin plugin = args.Plugin;
            PluginInfo info = args.Info;

            if (plugin == null || info == null)
            {
                return false;
            }

            return PluginsManager.PluginsManagerSington.InsertPlugin(plugin, info);
        }
        /// <summary>
        /// 移除插件时从插件树中清除相关插件信息
        /// </summary>
        /// <param name="args"></param>
        private bool OnRemovePlugin(RemovePluginArgs args)
        {
            return false;
        }

        /// <summary>
        /// 插件服务处理来自插件的请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnDealRequst(object sender, DealRequstArgs args)
        {
            if (args == null || args.Type== PluginRequstType.Unknown)
            {
                return;
            }

            if (args.Type == PluginRequstType.BuildOtherProject)
            {
                Requst_BuildOtherProject(args as BuildProjectArgs);
            }
            else if (args.Type == PluginRequstType.CloseProject)
            {
                Requst_RemoveProject(args as RemoveProjectArgs);
            }
        }

        private void Requst_BuildOtherProject(BuildProjectArgs args)
        {
            if (args == null || args.FromToken!="Main")
            {
                return;
            }
            IPlugin plugin = PluginsManager.PluginsManagerSington.GetPlugin(args.BuildPluginToken);
            AbstractProject project = plugin.GetDefaultProject();
            project.PluginUUID = plugin.Token;
            project.Name = args.ProjectName;
            ProjectManager.ProjectManagerSington.InsertProject(project);
            ServicesManager.ServicesManagerSingleton.UIService.LoadUI_CreateProject(plugin, new ProjectUIArgs(args.ProjectName,args.ProjectPath,plugin.MutableResourceClassFullName,project.UUID));
        }

        private void Requst_RemoveProject(RemoveProjectArgs args)
        {
            if (args == null)
            {
                return;
            }
            try
            {
                ProjectManager.ProjectManagerSington.RemoveProject(args.ProjectUUID);
                PluginsManager.PluginsManagerSington.RemoveMutableResource(args.ProjectUUID);
            }
            catch
            {

            }
        }
    }
}