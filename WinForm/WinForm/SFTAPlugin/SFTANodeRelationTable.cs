using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFTAPlugin
{
    public class SFTANodeRelationTable
    {
        public string nodeid = string.Empty;
        public string childid = string.Empty;
        public SFTANodeRelationTable()
        {
        }
        public SFTANodeRelationTable(string nodeID, string childID)
        {
            this.nodeid = nodeID;
            this.childid = childID;
        }
    }
}
