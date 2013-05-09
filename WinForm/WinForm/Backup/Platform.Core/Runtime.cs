using System;
using System.Windows.Forms;

using Platform.Core.Services;
using Platform.Core.UI;
using Platform.Core.Data;

namespace Platform.Core
{
    /// <summary>
    /// 平台与运行时请求交互的类型
    /// </summary>
    public enum RequstType
    {
        ExcutePlatform,
        OpenProject,
        CloseSingleProject,
        CloseAllProject
    }

    /// <summary>
    /// 交互时运行参数
    /// </summary>
    public class RuntimeEventArgs : EventArgs
    {
        /// <summary>
        /// 请求类别
        /// </summary>
        public readonly RequstType requstType;

        /// <summary>
        /// 请求的工程路径，用于直接打开工程用
        /// </summary>
        public readonly string projectpath;

        public RuntimeEventArgs()
        {
            requstType = RequstType.ExcutePlatform;
            projectpath = string.Empty;
        }

        public RuntimeEventArgs(RequstType type)
        {
            requstType = type;
            projectpath = string.Empty;
        }

        public RuntimeEventArgs(RequstType type, string projectpath)
        {
            requstType = type;
            this.projectpath = projectpath;
        }
    }

    /// <summary>
    /// 平台运行时交互的委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args">请求运行时交互的参数</param>
    public delegate void RuntimeEventHandler(object sender,RuntimeEventArgs args);

    /// <summary>
    /// 平台运行时
    /// </summary>
    public sealed class Runtime
    {
        /// <summary>
        /// 运行时引擎
        /// </summary>
        private Engine engine;

        /// <summary>
        /// 运行时配置文件
        /// </summary>
        private string configname;

        /// <summary>
        /// 处理Form界面与Runtime的交互的委托
        /// </summary>
        private RuntimeEventHandler dealrequst;

        /// <summary>
        /// 交互委托属性
        /// </summary>
        public RuntimeEventHandler DealRequst
        {
            get
            {
                return dealrequst;
            }
        }

        /// <summary>
        /// 运行时构造函数
        /// </summary>
        /// <param name="configname">运行时配置文件</param>
        /// <param name="mainForm">交互主窗体</param>
        public Runtime(string configname, Form mainForm)
        {
            SystemLogging.SystemLoggingSingleton.Info("运行时初始化,加载配置文件:" + configname);

            this.configname = configname;

            SystemLogging.SystemLoggingSingleton.Info("分析配置文件,引导引擎启动");

            StartUpSettings sus = new StartUpSettings(configname);

            SystemLogging.SystemLoggingSingleton.Info("配置文件分析成功完成,启动引擎");

            engine = new Engine(mainForm, sus);

            SystemLogging.SystemLoggingSingleton.Info("引擎成功加载,接受平台用户请求");

            dealrequst = new RuntimeEventHandler(OnDealRequst);
        }

        /// <summary>
        /// 平台处理用户请求，所有的请求都转发至Engine，由Engine来处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnDealRequst(object sender, RuntimeEventArgs args)
        {
            if (args == null)
            {
                return;
            }

            if (args.requstType == RequstType.ExcutePlatform)
            {
                SystemLogging.SystemLoggingSingleton.Info("处理平台用户请求，运行平台");
                 engine.ExcutePlatform(sender, args);
            }
            else if (args.requstType == RequstType.OpenProject)
            {
                SystemLogging.SystemLoggingSingleton.Info("处理平台用户请求，打开工程文件" + args.projectpath);
                engine.OpenProject(sender, args);
            }
        }
       
    }

}