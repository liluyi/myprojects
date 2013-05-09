using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SFTAPlugin
{
     [System.Serializable]
    public abstract class FTANodeData
    {
        public string nodedataid;
        public string description;
        public string nodename;

        [CategoryAttribute("事件信息"), BrowsableAttribute(false), ReadOnlyAttribute(true), DescriptionAttribute("事件ID（只读）")]
        public string nodeDataID { get { return nodedataid; } set { nodedataid = value; } }//数据ID

        [CategoryAttribute("事件信息"), DescriptionAttribute("设置事件名称")]
        public string nodeName { get { return nodename; } set { nodename = value; } }//节点名称       

        [CategoryAttribute("其它信息"), DescriptionAttribute("设置事件详细描述信息")]
        public string Decription 
        { 
            get 
            { 
                return description;
            } 
            set 
            { 
                description = value; 
            } 
        }//备注
        [CategoryAttribute("状态"), ReadOnlyAttribute(true), DescriptionAttribute("标记是否已完成分析")]
        public Boolean isFinished { get; set; }//分析是否已完成
        /// <summary>
        /// 数据节点的构造函数
        /// </summary>
        /// <param name="nodedataid">数据节点ID</param>
        /// <param name="nodename">节点名称</param>
        /// <param name="description">节点描述</param>
        public FTANodeData(string nodedataid, string nodename, string description)
        {
            this.nodedataid = nodedataid;
            this.nodename = nodename;
            this.description = description;
        }

        public FTANodeData() { }
   
        /// <summary>
        /// 更改节点名称
        /// </summary>
        /// <param name="nodename">新节点名称</param>
        public FTANodeData changeNodeName(string newname)
        {
            this.nodeName = newname;
            return this;
        } 
    }
}
