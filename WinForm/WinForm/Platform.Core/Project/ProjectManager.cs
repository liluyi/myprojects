using System;
using System.Collections;
using System.Collections.Generic;
using Platform.Core.Data;
using System.Reflection;

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
        private Dictionary<string, AbstractProjectData> projectdatadic = new Dictionary<string, AbstractProjectData>();

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
        /// 将工程数据插入队列中
        /// </summary>
        /// <param name="project">要插入的工程数据</param>
        /// <returns>是否成功插入</returns>
        internal bool InsertProjectData(AbstractProjectData projectdata)
        {
            if (projectdata == null || projectdatadic.ContainsKey(projectdata.UUID))
            {
                return false;
            }
            else
            {
                projectdatadic.Add(projectdata.UUID, projectdata);
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

        internal bool RemoveProjectData(string projectUUID)
        {
            if (!projectdatadic.ContainsKey(projectUUID))
            {
                return false;
            }
            else
            {
                projectdatadic.Remove(projectUUID);
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
            if (projectUUID == null)
                project = null;
            else
                projectdictionary.TryGetValue(projectUUID, out project);
            return project;
        }

        /// <summary>
        /// 获取当前工程
        /// </summary>
        /// <returns>返回当前工程实例</returns>
        public AbstractProject GetCurrentProject()
        {
            AbstractProject project;
            string id=GetProjectUUID();
            if(id==null)
                project=null;
            else 
                projectdictionary.TryGetValue(id, out project);
            return project;
        }

        /// <summary>
        /// 获取当前工程数据
        /// </summary>
        /// <returns>返回当前工程数据实例</returns>
        public AbstractProjectData GetCurrentProjectData()
        {
            AbstractProjectData projectdata=null;
            foreach (AbstractProjectData data in projectdatadic.Values)
                projectdata = data;
            return projectdata;
        }

        /// <summary>
        /// 获取所有工程，兼容多工程运行
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

        /// <summary>
        /// 获取当前词典中的工程ID
        /// </summary>
        /// <returns>当前工程ID</returns>
        public String GetProjectUUID()
        {
            String projectuuid = null;

            foreach (KeyValuePair<string, AbstractProject> pair in projectdictionary)
            {
                projectuuid= pair.Key;
            }
            return projectuuid;      
        }

        /// <summary>
        /// 获取当前词典中的工程ID
        /// </summary>
        /// <returns>当前工程ID</returns>
        public String GetProjectDataUUID()
        {
            String projectuuid = null;

            foreach (KeyValuePair<string, AbstractProjectData> pair in projectdatadic)
            {
                projectuuid = pair.Key;
            }
            return projectuuid;
        }

        
        /// <summary>
        /// 移除词典中全部工程
        /// </summary>
        public void RemoveProject()
        {
            projectdictionary.Clear();
        }

        /// <summary>
        /// 移除词典中全部工程数据
        /// </summary>
        public void RemoveProjectData()
        {
            projectdatadic.Clear();
        }
    }
}