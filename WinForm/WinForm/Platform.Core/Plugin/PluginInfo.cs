using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Platform.Core
{
    /// <summary>
    /// 插件类别
    /// </summary>
    public enum PluginType
    {
        Others,
        Test,
        Develop,
        Unknown
    }

    /// <summary>
    /// 插件信息
    /// </summary>
    public class PluginInfo
    {
        /// <summary>
        /// 工具名称
        /// </summary>
        public string Name;

        /// <summary>
        /// 工具描述
        /// </summary>
        public string Description;

        /// <summary>
        /// 工具类别
        /// </summary>
        public PluginType Type;

        /// <summary>
        /// 工具作者
        /// </summary>
        public string Author;

        /// <summary>
        /// 插件Token标识
        /// </summary>
        public string Token;

        /// <summary>
        /// 插件工程标识文件路径(即不同的插件具有不同的展示图片)
        /// </summary>
        public string Ico;

        public string Suffix;
        /// <summary>
        /// 插件信息默认构造函数
        /// </summary>
        public PluginInfo()
        {
            Author = "Core.CASREE";
            Type = PluginType.Unknown;
            Description = string.Empty;
            Token = string.Empty;
            Ico = string.Empty;
            Name = string.Empty;
            Suffix = string.Empty;
        }
    }
}