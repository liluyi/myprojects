using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFTAPlugin
{
     [System.Serializable]
    public class FTANodeRelation
    {
        public string nodeRelationID { get; set; }//节点关系ID
        public string siblingnodeID { get; set; }//兄弟节点（仅限于条件事件）ID
        public string parentNodeID { get; set; }//父节点ID
        public List<string> childrenNodesID { get; set; }//子节点们的ID

        public FTANodeRelation()
        {
        }

        public FTANodeRelation(string noderelationid, string parentnodeid, List<string> childrennodesid)
        {
            this.nodeRelationID = noderelationid;
            this.parentNodeID = parentnodeid;
            this.childrenNodesID = childrennodesid;
        }
    }
}
