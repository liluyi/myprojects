using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

namespace SratPlugin
{
    [Serializable]
    public class TreeNodeInfo:ISerializable
    {
        public TreeNodeInfo(string nodeName, string nodeTag, string parentNodeName)
        {
            this.nodeName = nodeName;
            this.nodeTag = nodeTag;
            this.parentNodeName = parentNodeName;
            this.foldOrExpand = true;
            
        }

        public string nodeName { get; set; }
        public string nodeTag { get; set; }
        public string parentNodeName { get; set; }
        public bool foldOrExpand { get; set; }
        

        public TreeNodeInfo(SerializationInfo info, StreamingContext context)   
        {
            this.nodeName = (string)info.GetValue("nodeName",typeof(string));
            this.nodeTag = (string)info.GetValue("nodeTag", typeof(string));
            this.parentNodeName = (string)info.GetValue("parentNodeName", typeof(string));
            this.foldOrExpand = (bool)info.GetValue("foldOrExpand",typeof(bool));
            
        }
        public   void   GetObjectData(SerializationInfo info,StreamingContext context) 
        { 
            info.AddValue("nodeName",this.nodeName); 
            info.AddValue("nodeTag",this.nodeTag);
            info.AddValue("parentNodeName",this.parentNodeName);
            info.AddValue("foldOrExpand",this.foldOrExpand);
        }
    }
}
