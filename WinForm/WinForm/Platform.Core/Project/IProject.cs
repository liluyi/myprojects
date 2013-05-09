using System;
using Platform.Core;
using System.Reflection;
using System.Xml;

namespace Platform.Core.Data
{
    [Serializable()]
   // [System.Xml.Serialization.XmlInclude(Assembly.LoadFrom(ServicesManager.ServicesManagerSingleton.PluginsService.AssemblyPath))]//typeof(liluyiPlugin.liluyiProject))]
    public class GUID
    {
        public GUID() { }
        /// <summary>
        /// 工程本身的标识，由系统自动生成
        /// </summary>
        private string projectuuid = Guid.NewGuid().ToString();

        /// <summary>
        /// 工程所关联的plugin类型，这个pluginid是与plugin一一对应的，
        /// 若pluginid有改动则相应的此工程可能无法打开
        /// </summary>
        private string pluginuuid;

        /// <summary>
        /// 工程本身标识
        /// </summary>
        public string UUID
        {
            get
            {
                return projectuuid;
            }
        }

        /// <summary>
        /// 工程所关联的Plugin标识
        /// </summary>
        public string PluginUUID
        {
            get
            {
                return pluginuuid;
            }
            set
            {
                pluginuuid = value;
            }
        }
    }

    /// <summary>
    /// 工程基类
    /// </summary>
    [Serializable]
    //[System.Xml.Serialization.XmlInclude(typeof(Assembly.LoadFrom(ServicesManager.ServicesManagerSingleton.PluginsService.AssemblyPath)))]//typeof(liluyiPlugin.liluyiProject))]
    public abstract class AbstractProject : GUID
    {
        /// <summary>
        /// 工程文件后缀，由外部调用者赋值
        /// </summary>
        public abstract string Suffix { get; }

        /// <summary>
        /// 工程所属项目名称
        /// </summary>
        public string Solution = "DefaultSolution";

        /// <summary>
        /// 工程文件名，由外部调用者赋值
        /// </summary>
        public string Name = string.Empty;

        /// <summary>
        /// 工程路径，由外部调用者赋值
        /// </summary>
        public string Path = string.Empty;
    }

    /// <summary>
    /// 工程数据基类
    /// </summary>
    [Serializable]
    public abstract class AbstractProjectData : GUID
    {
        public AbstractProjectData() { }
        /// <summary>
        /// 工程文件后缀，由外部调用者赋值
        /// </summary>
        public abstract string Suffix { get; }

        /// <summary>
        /// 工程所属项目名称
        /// </summary>
        public string Solution = "DefaultSolution";

        /// <summary>
        /// 工程文件名，由外部调用者赋值
        /// </summary>
        public string Name = string.Empty;

        /// <summary>
        /// 工程路径，由外部调用者赋值
        /// </summary>
        public string Path = string.Empty;
        /// <summary>
        /// 生成工程数据，由工具实现，平台在序列化为XML前调用
        /// </summary>
        public virtual void GenerateProjectData() { }
    }
}