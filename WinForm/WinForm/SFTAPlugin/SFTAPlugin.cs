using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Core;
using Platform.Core.UI;
using Platform.Core.Data;
using Platform.Core.Services;

namespace SFTAPlugin
{
    public class SFTAPlugin:IPlugin
    {
                
        private string token;
        private string projectsuffix = string.Empty;
        private string projectclassfullname = string.Empty;
        private string mutableresourcefullname = string.Empty;
        //private ICommonLogging Log;

        #region IPlugin Members

        public AbstractProject GetDefaultProject()
        {
            return new SFTAProject();
        }

        public AbstractProject GetProjectFromPath(string path)
        {
            SFTAProject project = new SFTAProject();
            return project;
        }

        public AbstractProjectData GetDefaultProjectData()
        {
            return new SFTAProjectData();
        }

        public AbstractProjectData GetProjectDataFromPath(string path)
        {
            SFTAProjectData projectdata = new SFTAProjectData();
            return projectdata;
        }

        public string MutableResourceClassFullName
        {
            get
            {
                return mutableresourcefullname;
            }
            set
            {
                mutableresourcefullname = value;
            }
        }

        

        public string ProjectClassFullName
        {
            get
            {
                return projectclassfullname;
            }
            set
            {
                projectclassfullname = value;
            }
        }

        public string ProjectSuffix
        {
            get
            {
                return projectsuffix;
            }
            set
            {
                projectsuffix = value;
            }
        }

        public string Token
        {
            get
            {
                return token;
            }
            set
            {
                token = value;
            }
        }
        #endregion

        public SFTAPlugin()
        {
        }
    }
}
