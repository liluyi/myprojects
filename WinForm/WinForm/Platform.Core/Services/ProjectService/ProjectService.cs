using System;
using System.IO;
using Platform.Core.Data;
using Platform.Core;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Reflection;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using Platform.Core.Exceptions;
using System.Windows.Forms;

namespace Platform.Core.Services
{
    internal class ProjectService: IProjectService
    {
        private ServiceState state = ServiceState.UnLoad;
        private AbstractProject activeproject = null;

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs e)
        {
            //手动指定程序集的位置
            string assemblypath = ServicesManager.ServicesManagerSingleton.PluginsService.AssemblyPath;
            return Assembly.LoadFrom(assemblypath);
        }
        
        #region IProjectService Members

        /// <summary>
        /// 打开工程
        /// </summary>
        /// <param name="path">本地磁盘路径</param>
        /// <returns>已打开的工程</returns>
        public AbstractProject OpenProject(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }
            int pos = path.LastIndexOf('.');
            string suffix = path.Substring(pos + 1);

            IPlugin plugin = PluginsManager.PluginsManagerSington.GetPluginFromSuffix(suffix);

            if (plugin == null)
            {
                return null;
            }

            AbstractProject project = plugin.GetProjectFromPath(path);

            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);//加载plugin下所属文件夹程序集
            FileStream fileStream = new FileStream(path, FileMode.Open);//, FileAccess.Read, FileShare.Read);
            BinaryFormatter b = new BinaryFormatter();
            project = b.Deserialize(fileStream) as AbstractProject;
            fileStream.Close();

            //project.Name = path.Substring(0, pos);

            if (project == null)
            {
                return null;
            }
            return project;
        }

        /// <summary>
        /// 保存序列化对象
        /// </summary>
        /// <param name="fileName">文件完整路径包括名称（c:/test.xxj）</param>
        /// <returns></returns>
        public bool SaveProject(AbstractProject project)
        {
            //首先保存窗体布局，在mainplugin中保存
            //PluginsManager.PluginsManagerSington.SaveMutableResource(project.UUID);
            BinaryFormatter bf = new BinaryFormatter();
            string filename = Path.Combine(project.Path, project.Name + "." + project.Suffix);
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                bf.Serialize(fs, project);
                fs.Close();
            }
            return true;
        }

        /// <summary>
        /// 工程文件另存为
        /// </summary>
        /// <param name="project">工程</param>
        /// <param name="saveasPath">路径</param>
        public void SavaAsProject(AbstractProject project, string saveasPath)
        {
            BinaryFormatter bf = new BinaryFormatter();
            string filepath =  saveasPath;

            try
            {
                using (FileStream fs = new FileStream(filepath, FileMode.Create)) //用户没有权限时会抛出异常unauthorizedaccessexception
                {
                    bf.Serialize(fs, project);
                    fs.Close();
                }
            }
            catch(Exception ex)                                  
            {    
                MessageBox.Show("没有权限保存在此路径！");
                return;
            }
        }

        /// <summary>
        /// 工程数据另存为XML
        /// </summary>
        /// <param name="projectdata">工程</param>
        /// <param name="saveasPath">路径</param>
        public void SavaAsXML(AbstractProjectData projectdata, string saveasPath)
        {
            //保存成XML
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);//加载plugin下所属文件夹程序集
            //Assembly ass=Assembly.LoadFrom(ServicesManager.ServicesManagerSingleton.PluginsService.AssemblyPath);//反射获取子类所在程序集
            //序列化器需要初始为子类的初始化器
            XmlSerializer xmlFormat = new XmlSerializer(projectdata.GetType());
            using (Stream fStream = new FileStream(saveasPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                try
                {
                    XmlTextWriter xmlTextWriter = new XmlTextWriter(fStream, Encoding.Unicode);
               // xmlFormat.Serialize(fStream, projectdata);
                    xmlFormat.Serialize(xmlTextWriter, projectdata);
                }
                catch (Exception ex)
                {
                    DumpException(ex);
                }  
            }

            //读取
            //StringReader sr = new StringReader(strXml);
            //AbstractProject project0 = xs.Deserialize(sr) as AbstractProject;
        }

        public AbstractProject ActiveProject
        {
            get
            {
                return activeproject;
            }
            set
            {
                activeproject = value;
            }
        }
        #endregion
        public static void DumpException(Exception ex)
        {
            Console.WriteLine("--------- Outer Exception Data ---------");
            WriteExceptionInfo(ex);
            ex = ex.InnerException;
            if (null != ex)
            {
                Console.WriteLine("--------- Inner Exception Data ---------");
                WriteExceptionInfo(ex.InnerException);
                ex = ex.InnerException;
            }
        }
        public static void WriteExceptionInfo(Exception ex)
        {
            Console.WriteLine("Message: {0}", ex.Message);
            Console.WriteLine("Exception Type: {0}", ex.GetType().FullName);
            Console.WriteLine("Source: {0}", ex.Source);
            Console.WriteLine("StrackTrace: {0}", ex.StackTrace);
            Console.WriteLine("TargetSite: {0}", ex.TargetSite);
        }

        #region IService Members

        public string Description
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get { return "ProjectService"; }
        }

        public ServiceLevel Level
        {
            get {  return ServiceLevel.High; }
        }

        public ServiceState State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }

        public InitServiceHandler InitService
        {
            get {  return null; }
        }

        public EventHandler LoadingService
        {
            get { return  null; }
        }

        #endregion
    }
}