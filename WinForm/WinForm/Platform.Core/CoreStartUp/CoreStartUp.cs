using System;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using Platform.Core.Data;
using Platform.Core.Services;
using Platform.Core.Exceptions;

namespace Platform.Core
{
    /// <summary>
    /// Core启动配置
    /// </summary>
    public sealed class StartUpSettings
    {
        /// <summary>
        /// Core启动配置文件
        /// </summary>
        private string xmlFileName= string.Empty;

        /// <summary>
        /// 插件属性，主要信息为插件的根目录等
        /// </summary>
        public Properties PluginProperties;

        /// <summary>
        /// 工程属性，主要信息为工程目录信息等
        /// </summary>
        public Properties ProjectProperties;

        /// <summary>
        /// 平台系统服务配置信息
        /// </summary>
        public Dictionary<string, object> ServiceProperties=new Dictionary<string,object>();

        /// <summary>
        /// 平台服务列表
        /// </summary>
        public List<string> serviceList = new List<string>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xmlFileName">Core配置文件</param>
        public StartUpSettings(string xmlFileName)
        {
            this.xmlFileName = xmlFileName;

            //添加自启动服务
            serviceList.Add("LoggingService"); 
            serviceList.Add("UIService");
            serviceList.Add("PluginsService");
            serviceList.Add("FileService");
            serviceList.Add("ProjectService");
            serviceList.Add("NetworkService");

            //分析配置文件
            AnalyseXml();
        }

        /// <summary>
        /// Core分析配置文件
        /// </summary>
        public void AnalyseXml()
        {
            try
            {
                PluginProperties = PropertyAnalyse.GetProperties(xmlFileName, "configuration/Runtime/Plugin");

                foreach (string service in serviceList)
                {
                    ServiceProperties.Add(service, PropertyAnalyse.GetProperties(xmlFileName, "configuration/Runtime/Services/" + service));
                }

                ProjectProperties = PropertyAnalyse.GetProperties(xmlFileName, "configuration/Runtime/Project");
            }
            catch(Exception ex)
            {
                throw new RuntimeConfigInvaildException("配置文件格式不合法,退出", ex);
            }
        }
        
    }

    /// <summary>
    /// Core启动类
    /// </summary>
    internal sealed class CoreStartUp
    {
        /// <summary>
        /// 启动配置
        /// </summary>
        private StartUpSettings startupsetting;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="startupsetting">引导Core启动的配置</param>
        public CoreStartUp(StartUpSettings startupsetting)
        {
            this.startupsetting = startupsetting;
        }

        /// <summary>
        /// 初始化服务管理者
        /// </summary>
        /// <param name="ssm">服务管理者</param>
        /// <param name="mainForm">Core寄宿的主窗体</param>
        public void InitServicesManager(out ServicesManager ssm,Form mainForm)
        {
            ssm = ServicesManager.ServicesManagerSingleton;
            
            //初始化服务
            ssm.InitAutoStartService(startupsetting , EventArgs.Empty);

            //加载服务
            ssm.LoadAutoStartService(mainForm, EventArgs.Empty);

            if (ssm.FailedService > 0)
            {
                throw new Exception("error");
            }
        }

        /// <summary>
        /// 初始化插件管理者
        /// </summary>
        /// <param name="pm">插件管理者</param>
        public void InitPluginsManager(out PluginsManager pm)
        {
            pm = PluginsManager.PluginsManagerSington;
        }

        public void InitProjectManager(out ProjectManager pm)
        {
            pm = ProjectManager.ProjectManagerSington;
        }

    }
}

