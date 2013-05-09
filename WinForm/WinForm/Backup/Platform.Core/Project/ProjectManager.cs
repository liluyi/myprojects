using System;
using System.Collections;
using System.Collections.Generic;
using Platform.Core.Data;

namespace Platform.Core
{
    /// <summary>
    /// 工程管理器
    /// </summary>
    public class ProjectManager
    {
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private ProjectManager()
        {

        }

        /// <summary>
        /// 静态变量，为线程安全的
        /// </summary>
        private static ProjectManager projectmanager = new ProjectManager();

        /// <summary>
        /// 工程词典集合
        /// </summary>
        private Dictionary<string, AbstractProject> projectdictionary = new Dictionary<string, AbstractProject>();

        /// <summary>
        /// 工程管理器(单例模式)
        /// </summary>
        public static ProjectManager ProjectManagerSington
        {
            get
            {
                return projectmanager;
            }
        }

        /// <summary>
        /// 将工程插入队列中
        /// </summary>
        /// <param name="project">要插入的工程</param>
        /// <returns>是否成功插入</returns>
        internal bool InsertProject(AbstractProject project)
        {
            if (project == null || projectdictionary.ContainsKey(project.UUID))
            {
                return false;
            }
            else
            {
                projectdictionary.Add(project.UUID, project);
                return true;
            }
        }

        /// <summary>
        /// 在工程结束时从队列中移除
        /// </summary>
        /// <param name="projectUUID">移除的工程的标识</param>
        /// <returns>是否成功移除</returns>
        internal bool RemoveProject(string projectUUID)
        {
            if (!projectdictionary.ContainsKey(projectUUID))
            {
                return false;
            }
            else
            {
                projectdictionary.Remove(projectUUID);
                return true;
            }
        }

        /// <summary>
        /// 通过UUID获取工程
        /// </summary>
        /// <param name="projectUUID"></param>
        /// <returns></returns>
        public AbstractProject GetProject(string projectUUID)
        {
            AbstractProject project;
            projectdictionary.TryGetValue(projectUUID, out project);
            return project;
        }

        /// <summary>
        /// 获取所有工程
        /// </summary>
        /// <returns></returns>
        public List<AbstractProject> GetAllProjects()
        {
            List<AbstractProject> projects = new List<AbstractProject>();

            foreach (KeyValuePair<string, AbstractProject> pair in projectdictionary)
            {
                projects.Add(pair.Value);
            }

            return projects;
        }
    }
}