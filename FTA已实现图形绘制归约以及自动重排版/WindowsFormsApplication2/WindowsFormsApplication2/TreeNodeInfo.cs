using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

namespace WindowsFormsApplication2
{
    public enum NodeType
    {
        //事件
        EventIntermediate,//中间事件
        EventBasic,//基本事件
        EventInitiating,//初始事件
        EventUndeveloped,
        EventConditioning,
        EventIn,
        EventOut,

        //逻辑门
        GateOr,
        GateAnd,
        GateInhibit,
        GatePri,
        GateXor
    }
    [Serializable]
    public class TreeNodeInfo//:ISerializable
    {
        public TreeNodeInfo(string nodeID, string nodeName,NodeType nodeType,string parentNodeID,List<string> childrenNodesID,string description,int level)
        {
            this.nodeName = nodeName;
            this.nodeID = nodeID;
            this.nodeType = nodeType;
            this.parentNodeID = parentNodeID;
            this.childrenNodesID = childrenNodesID;
            this.decription = description;
            this.level = level;    
        }

        public string nodeName { get; set; }//节点名称
        public string nodeID { get; set; }//节点ID
        public NodeType nodeType { get; set; }//节点类型
        public string parentNodeID { get; set; }//父节点ID
        public List<string> childrenNodesID { get; set; }//子节点们的ID
        public string decription { get; set; }//备注
        public int level { get; set; }//层次

        
        

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

