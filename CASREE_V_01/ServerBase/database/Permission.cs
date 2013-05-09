using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerBase.database
{
    class Permission
    {
        public string name;//数据库主键
        public string solutionName;
        public string projectName;
        public List<string> SolutionProjectList = new List<string>();

        //public Dictionary<string, int> readSolutions= new Dictionary<string, int>();
        //public Dictionary<string, int> writeSolutions= new Dictionary<string, int>();
        //public Dictionary<string, int> readProjects = new Dictionary<string, int>();
        //public Dictionary<string, int> writeProjects = new Dictionary<string, int>();
        //private Dictionary<string, string> readAndWriteProjects = new Dictionary<string, string>();
        //public Dictionary<string, int> readDocument = new Dictionary<string, int>();
        //public Dictionary<string, int> writeDocument = new Dictionary<string, int>();
        //private Dictionary<string, string> readAndWriteDocument = new Dictionary<string, string>();

        public Permission() { }


        public Permission(string name, string solutionName,string projectName)
        {
            this.name = name;
            this.solutionName = solutionName;
            this.projectName = projectName;
        }

        public void AddPermission(string solutionName,string projectName) 
        {
            string pm = solutionName + ":" + projectName;
            this.SolutionProjectList.Add(pm);
        }
    }
}
