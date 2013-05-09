using System;
using System.IO;
using Platform.Core.Data;
using Platform.Core;
namespace Platform.Core.Services
{
    internal class ProjectService: IProjectService
    {
        private ServiceState state = ServiceState.UnLoad;
        private AbstractProject activeproject = null;
        
        #region IProjectService Members

        public AbstractProject OpenProject(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }
            int pos = path.LastIndexOf('.');
            string suffix = path.Substring(pos + 1);

            IPlugin plugin = PluginsManager.PluginsManagerSington.GetPluginFromSuffix(suffix);

            if(plugin==null)
            {
                return null;
            }

            AbstractProject project=plugin.GetProjectFromPath(path);
            if (project == null)
            {
                return null;
            }
            project.PluginUUID = plugin.Token;
            return project;
        }

        public bool SaveProject(AbstractProject project)
        {
            throw new NotImplementedException();
        }

        public void SavaAsProject(AbstractProject project, string saveasPath)
        {
            throw new NotImplementedException();
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