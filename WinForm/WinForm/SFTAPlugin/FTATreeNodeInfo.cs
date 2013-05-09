using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

namespace SFTAPlugin
{
    /// <summary>
    /// 故障树的关系节点类
    /// </summary>
    [Serializable]
    public class FTATreeNodeInfo//:ISerializable
    {
        public string nodeID { get; set; }//节点ID
        public FTANodeRelation noderelation { get; set; }//节点关系：包括父节点id、兄弟节点id、子节点id集合
        public int level { get; set; }//层次
        public FTANodeData nodedata { get; set; }
        public int childrennum = 0;//子树数量，默认为0，只设置一张画布的顶事件的子树数量
        public Boolean hasNotGate = false;//是否含有非门，默认不含

        public FTATreeNodeInfo()
        {
        }
        public FTATreeNodeInfo(string nodeID, int level, FTANodeData ftanodedata, FTANodeRelation ftanoderelation)
        {
            this.nodeID = nodeID;
            this.nodedata = ftanodedata;
            this.noderelation = ftanoderelation;
            this.level = level;    
        }

        public FTATreeNodeInfo(FTATreeNodeInfo item)
        {
            // TODO: Complete member initialization
            if(item.nodedata is FTAEventNodeData)
            {
                FTAEventNodeData newdata=(FTAEventNodeData)item.nodedata;
                this.nodeID=item.nodeID;
                this.level=item.level;
                this.hasNotGate = item.hasNotGate;
                this.nodedata=new FTAEventNodeData(newdata.nodeDataID, newdata.nodename, newdata.eventType, newdata.description,newdata.FMEAInfo);
                List<string> childrenid = new List<string>();
                if (item.noderelation.childrenNodesID.Count != 0)
                {
                    foreach (string childid in item.noderelation.childrenNodesID)
                        childrenid.Add(childid);
                }
                this.noderelation=new FTANodeRelation(item.noderelation.nodeRelationID, item.noderelation.parentNodeID, childrenid);
            }
            else if (item.nodedata is FTAGateNodeData)
            {
                FTAGateNodeData newdata = (FTAGateNodeData)item.nodedata;
                this.nodeID=item.nodeID;
                this.level=item.level;
                this.hasNotGate = item.hasNotGate;
                this.nodedata = new FTAGateNodeData(newdata.nodeDataID, newdata.nodename, newdata.gateType, newdata.description);
                List<string> childrenid = new List<string>();
                if (item.noderelation.childrenNodesID.Count != 0)
                {
                    foreach (string childid in item.noderelation.childrenNodesID)
                        childrenid.Add(childid);
                }
                this.noderelation=new FTANodeRelation(item.noderelation.nodeRelationID, item.noderelation.parentNodeID, childrenid);
            }
        }

        public void ChangeNodeData(ref FTAEventNodeData newnodedata)
        {
            this.nodedata=newnodedata;
        }

        public FTATreeNodeInfo MakeDuplicate(int level)
        {
            FTATreeNodeInfo tni = new FTATreeNodeInfo(Guid.NewGuid().ToString(), level, this.nodedata, this.noderelation);//更改节点ID以及level，其它信息保持不变
            return tni;
        }

        public FTATreeNodeInfo ConvertToNot()
        {
            FTATreeNodeInfo duplicatednode = new FTATreeNodeInfo(this);
            duplicatednode.nodeID = Guid.NewGuid().ToString();
            if(duplicatednode.hasNotGate==false)
                duplicatednode.hasNotGate = true;
            else
                duplicatednode.hasNotGate=false;
            if(duplicatednode.nodedata is FTAGateNodeData)
            {
                GateType type=((FTAGateNodeData)duplicatednode.nodedata).gateType;
                switch (type)
                {
                    case GateType.GateAnd:
                        {
                            ((FTAGateNodeData)duplicatednode.nodedata).gateType = GateType.GateOr;
                            break;
                        }
                    case GateType.GateOr:
                        {
                            ((FTAGateNodeData)duplicatednode.nodedata).gateType = GateType.GateAnd;
                            break;
                        }
                }
            }
            return duplicatednode;
        }

        



        
        

        //public TreeNodeInfo(SerializationInfo info, StreamingContext context)   
        //{
        //    this.nodeName = (string)info.GetValue("nodeName",typeof(string));
        //    this.nodeTag = (string)info.GetValue("nodeTag", typeof(string));
        //    this.parentNodeName = (string)info.GetValue("parentNodeName", typeof(string));
        //    this.foldOrExpand = (bool)info.GetValue("foldOrExpand",typeof(bool));
            
        //}
        //public   void   GetObjectData(SerializationInfo info,StreamingContext context) 
        //{ 
        //    info.AddValue("nodeName",this.nodeName); 
        //    info.AddValue("nodeTag",this.nodeTag);
        //    info.AddValue("parentNodeName",this.parentNodeName);
        //    info.AddValue("foldOrExpand",this.foldOrExpand);
        //}
    }
}

