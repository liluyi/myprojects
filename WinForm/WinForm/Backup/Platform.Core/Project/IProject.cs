using System;

using Platform.Core;
namespace Platform.Core.Data
{
    [Serializable()]
    public class GUID
    {
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
    [Serializable()]
    public abstract class AbstractProject : GUID
    {
        /// <summary>
        /// 工程文件后缀，由外部调用者赋值
        /// </summary>
        public abstract string Suffix { get; }

        /// <summary>
        /// 工程文件名，由外部调用者赋值
        /// </summary>
        public string Name = string.Empty;

        /// <summary>
        /// 工程路径，由外部调用者赋值
        /// </summary>
        public string Path = string.Empty;
    }
}