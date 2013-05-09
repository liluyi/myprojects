using System;

using Platform.Core.UI;
using Platform.Core;
using Platform.Core.Data;

namespace Platform.Core.Services
{
    /// <summary>
    /// 插件与平台通讯类型
    /// </summary>
    public enum PluginRequstType
    {
        Unknown,                  //请求服务未知
        BuildOtherProject,        //创建其它插件工程,只对Main插件有效
        CloseProject,             //删除其它插件工程,只对Main插件有效
        UpdateProjectInfo,       //向server端（或者本地）更新工程信息
        DownloadProjectInfo      //从server端（或者本地）下载工程信息
    }

    /// <summary>
    /// 插件与平台通讯参数基类,标明是那个插件发起的Type请求
    /// </summary>
    public class DealRequstArgs : EventArgs
    {
        public string FromToken = string.Empty;

        public PluginRequstType Type = PluginRequstType.Unknown;

        public DealRequstArgs(string fromtoken,PluginRequstType type)
        {
            this.FromToken=fromtoken;
            this.Type=type;
        }
    }

    /// <summary>
    /// 构建其它插件工程参数
    /// </summary>
    public class BuildProjectArgs : DealRequstArgs
    {
        public readonly string BuildPluginToken;
        public string ProjectName;
        public string ProjectPath;
        public BuildProjectArgs(string FromToken, string BuildPluginToken,string ProjectName,string ProjectPath)
            : base(FromToken, PluginRequstType.BuildOtherProject)
        {
            this.BuildPluginToken = BuildPluginToken;
            this.ProjectName = ProjectName;
            this.ProjectPath = ProjectPath;
        }
    }

    public class RemoveProjectArgs : DealRequstArgs
    {
        public readonly string ProjectUUID;

        public RemoveProjectArgs(string fromtoken,string projectUUID):base(fromtoken, PluginRequstType.CloseProject)
        {
            this.ProjectUUID = projectUUID;
        }
    }
    /// <summary>
    /// 插件放入插件树中参数
    /// </summary>
    public class InsertPluginArgs
    {
        private PluginInfo info=null;
        private IPlugin plugin = null;

        public PluginInfo Info
        {
            get
            {
                return info;
            }
        }
        public IPlugin Plugin
        {
            get
            {
                return plugin;
            }
        }
        public InsertPluginArgs(IPlugin plugin,PluginInfo info)
        {
            this.info = info;
            this.plugin = plugin;
        }
    }

    /// <summary>
    /// 插件树移除插件参数
    /// </summary>
    public class RemovePluginArgs
    {
        private string token;

        public string Token
        {
            get
            {
                return Token;
            }
        }

        public RemovePluginArgs(string token)
        {
            this.token = token;
        }
    }

    /// <summary>
    /// 构建插件时参数
    /// </summary>
    public class CreatePluginArgs
    {
        private string addin_file = string.Empty;
        private PluginInfo info = new PluginInfo();

        /// <summary>
        /// 插件配置文件信息
        /// </summary>
        public string AddinFile
        {
            get
            {
                return addin_file;
            }
        }

        /// <summary>
        /// 插件构建时，提取插件的PluginInfo信息
        /// </summary>
        public PluginInfo Info
        {
            get
            {
                return info;
            }
            set
            {
                info = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="addin_file"></param>
        public CreatePluginArgs(string addin_file)
        {
            this.addin_file = addin_file;
        }
    }

    public delegate IPlugin CreatePluginEventHandler(CreatePluginArgs args);

    public delegate bool PluginInsertHandler(InsertPluginArgs args);

    public delegate bool PluginRemoveHandler(RemovePluginArgs args);

    public delegate void DealRequstHandler(object sender, DealRequstArgs args);

    /// <summary>
    /// 插件服务接口
    /// </summary>
    public interface IPluginService:IService
    {
        /// <summary>
        /// 插件根目录
        /// </summary>
        string PluginsRoot { get; set; }

        /// <summary>
        /// 插件放入插件树中委托
        /// </summary>
        PluginInsertHandler InsertPlugin { get; }

        /// <summary>
        /// 插件从插件树中移除委托
        /// </summary>
        PluginRemoveHandler RemovePlugin { get; }

        /// <summary>
        /// 构建插件时委托
        /// </summary>
        CreatePluginEventHandler CreatePlugin { get; }

        /// <summary>
        /// 平台与插件通讯委托
        /// </summary>
        DealRequstHandler DealRequst { get; }
    }
}