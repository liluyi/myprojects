using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

using Platform.Core.Data;
using Platform.Core;
using Platform.Core.UI;
using Platform.Core.Services;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;

namespace Platform.Core
{
    /// <summary>
    /// 插件接口
    /// </summary>
    public interface IPlugin
    {
        

        /// <summary>
        /// 插件本身的Token标识，这个标识是唯一的（可对此Token添加检查，防止没授权的插件添加至平台中来）
        /// </summary>
        string Token { get;set; }

        /// <summary>
        /// 其它可活动资源的类全名，用于反射时构造这些资源时使用
        /// </summary>
        string MutableResourceClassFullName { get; set; }

        /// <summary>
        /// 插件关联的工程类全名，用于反射时构造工程资源时使用
        /// </summary>
        string ProjectClassFullName { get; set; }

        /// <summary>
        /// 插件关联的工程文件后缀
        /// </summary>
        string ProjectSuffix { get; set; }

        /// <summary>
        /// 从工程文件反序列化得到对应的工程
        /// </summary>
        /// <param name="path">工程文件路径</param>
        /// <returns>工程</returns>
        AbstractProject GetProjectFromPath(string path);

        /// <summary>
        /// 从工程数据文件反序列化得到对应的工程数据
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        AbstractProjectData GetProjectDataFromPath(string path);

        /// <summary>
        /// 建立一个默认的工程
        /// </summary>
        /// <returns>工程</returns>
        AbstractProject GetDefaultProject();

        /// <summary>
        /// 建立一个默认的工程数据文件
        /// </summary>
        /// <returns>工程数据</returns>
        AbstractProjectData GetDefaultProjectData();            

    }
   


}