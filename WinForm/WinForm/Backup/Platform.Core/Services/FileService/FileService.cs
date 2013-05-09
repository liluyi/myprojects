using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Platform.Core.Data;
using Platform.Core.Exceptions;

namespace Platform.Core.Services
{
    /// <summary>
    /// 文件服务，继承于文件服务接口
    /// </summary>
    public sealed class FileService : IFileService
    {
        /// <summary>
        /// 文件服务描述
        /// </summary>
        private string description = string.Empty;

        /// <summary>
        /// 文件服务状态
        /// </summary>
        private ServiceState state = ServiceState.UnLoad;

        /// <summary>
        /// 初始化服务委托
        /// </summary>
        private InitServiceHandler initService;

        #region IService Members

        /// <summary>
        /// 服务描述属性
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
                return "FileService";
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
        /// 初始化服务
        /// </summary>
        public InitServiceHandler InitService
        {
            get
            {
                return initService;
            }
        }

        /// <summary>
        /// 加载服务
        /// </summary>
        public EventHandler LoadingService
        {
            get
            {
                return null;
            }
        }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public FileService()
        {
            //添加初始化服务
            initService = new InitServiceHandler(Init);
        }

        /// <summary>
        /// 初始化服务
        /// </summary>
        /// <param name="sender">StartUpSeetings</param>
        /// <param name="args"></param>
        private void Init(StartUpSettings sus)
        {
            if (sus == null)
            {
                return;
            }

            Properties p = sus.ServiceProperties["FileService"] as Properties;

            if (p == null)
            {
                return;
            }

            this.description = p["description"] as string;
        }


        #region IFileService Members

        /// <summary>
        /// 收集所有的插件Addin配置文件
        /// </summary>
        /// <param name="root">插件根目录</param>
        /// <param name="ignoreMain">是否忽略Main插件</param>
        /// <returns>所有插件Addin配置文件List</returns>
        public List<string> CollectPluginsAddin(string root)
        {
            try
            {
                string[] dir = Directory.GetFiles(root, "*.addin", SearchOption.AllDirectories);

                List<string> Addins = new List<string>();

                foreach (string Addin in dir)
                {
                    Addins.Add(Addin);
                }

                return Addins;
            }
            catch(Exception ex)
            {
                throw new FileServiceException("目录"+root+"下查询插件文件出现异常", ex);
            }
        }
        #endregion 
    }
}