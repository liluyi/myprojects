using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFTAPlugin
{
    public class SFTATreeInfoTable
    {
        public string nodeid=string.Empty;
        public string nodedataid = string.Empty;
        public string nodename = string.Empty;
        public string description = string.Empty;
        public string nodetype = string.Empty;
        public string parentid = string.Empty;
        public string siblingid = string.Empty;
        public string fmeainfo = string.Empty;

        public SFTATreeInfoTable()
        {
        }
        public SFTATreeInfoTable(string nodeID, string nodedataID, string nodeName, string Descripition, string NodeType, string ParentID, string siblingID, string FMEAInfo)
        {
            this.nodeid = nodeID;
            this.nodedataid = nodedataID;
            this.nodename = nodeName;
            this.description = Descripition;
            this.nodetype = NodeType;
            this.parentid = ParentID;
            this.siblingid = siblingID;
            this.fmeainfo = FMEAInfo;
        }
    }
}
