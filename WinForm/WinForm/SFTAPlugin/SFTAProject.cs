using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Core;
using Platform.Core.Data;
using System.Drawing;

namespace SFTAPlugin
{
    [System.Serializable]
    public class SFTAProject:AbstractProject
    {
        public SFTAProject()
        {
        }
        public override string Suffix
        {
            get { return "fta"; }
        }
        //[XmlIgnore]
        //public int liluyiform = 10;
        //public string username=null;
        //public string password=null;
        //public int liluyiformend = 1;
        //public List<FTATreeInfo> SFTATrees = new List<FTATreeInfo>();
        public Dictionary<string, FTATreeInfo> SFTATreesDic = new Dictionary<string, FTATreeInfo>();
        public List<Color> statuscolors=new List<Color>();
        public string activeformid = string.Empty;
    }
}
