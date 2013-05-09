using System;

using Platform.Core.Data;

namespace Platform.Core.Services
{
    public interface IProjectService : IService
    {
        /// <summary>
        /// 打开路径path的工程文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        AbstractProject OpenProject(string path);

        /// <summary>
        /// 按照project的路径配置保存工程
        /// </summary>
        /// <param name="project"></param>
        bool SaveProject(AbstractProject project);

        /// <summary>
        /// 工程文件另存为saveaspath路径下
        /// </summary>
        /// <param name="project">工程文件</param>
        /// <param name="saveasPath">保存的路径</param>
        void SavaAsProject(AbstractProject project, string saveasPath);

        AbstractProject ActiveProject { get; set; }
    }
}