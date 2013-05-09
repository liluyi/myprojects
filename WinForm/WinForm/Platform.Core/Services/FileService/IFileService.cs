using System;
using System.Collections;
using System.Collections.Generic;

using Platform.Core.Data;
namespace Platform.Core.Services
{
    /// <summary>
    /// 文件服务接口
    /// </summary>
    public interface IFileService : IService
    {
        /// <summary>
        /// 寻找目录root下所有的插件配置Addin文件
        /// </summary>
        /// <param name="root">插件根目录</param>
        /// <returns>所有Addin文件的路径</returns>
        List<string> CollectPluginsAddin(string root);
    }
}