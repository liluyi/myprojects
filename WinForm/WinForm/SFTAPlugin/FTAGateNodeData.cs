using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SFTAPlugin
{
    /// <summary>
    /// 故障树的门节点类
    /// </summary>
    [System.Serializable]
    //节点类型
    //public enum NodeType
    //{
    //    //事件
    //    EventIntermediate,//中间事件
    //    EventBasic,//基本事件
    //    EventInitiating,//正常比事件
    //    EventUndeveloped,//省略事件
    //    EventConditioning,//条件事件，施于门右侧
    //    EventIn,
    //    EventOut,

    //    //逻辑门
    //    GateOr,//或
    //    GateAnd,//与
    //    GateInhibit,//禁门
    //    GatePri,
    //    GateXor,
    //    GateSequenceAnd,//顺序与门
    //    GateElect//表决门
    //}

    public enum GateType
    {
        //逻辑门
        GateOr,//或
        GateAnd,//与
        GateInhibit,//禁门
        GatePri,
        GateXor,
        GateSequenceAnd,//顺序与门
        GateElect,//表决门
        Error
    }

    [DefaultPropertyAttribute("nodeDataID")]
    [System.Serializable]
    public class FTAGateNodeData : FTANodeData
    {
        //private string nodedataid;
        //private string description;
        //private string nodename;

        //[CategoryAttribute("事件信息"), BrowsableAttribute(false), ReadOnlyAttribute(true), DescriptionAttribute("事件ID（只读）")]
        //public string nodeDataID { get { return nodedataid; } set { nodedataid = value; } }//数据ID

        //[CategoryAttribute("事件信息"), DescriptionAttribute("设置事件名称")]
        //public string nodeName { get { return nodename; } set { nodename = value; } }//节点名称       
        public GateType gateType { get; set; }//节点类型

        //[CategoryAttribute("其它信息"), DescriptionAttribute("设置事件详细描述信息")]
        //public string Decription 
        //{ 
        //    get 
        //    { 
        //        return description;
        //    } 
        //    set 
        //    { 
        //        description = value; 
        //    } 
        //}//备注
        [CategoryAttribute("状态"), ReadOnlyAttribute(true), DescriptionAttribute("标记是否已完成分析")]
        //public Boolean isFinished { get; set; }//分析是否已完成

        /// <summary>
        /// 数据节点的构造函数
        /// </summary>
        /// <param name="nodedataid">数据节点ID</param>
        /// <param name="nodename">节点名称</param>
        /// <param name="nodetype">节点类型</param>
        /// <param name="description">节点描述</param>
        /// <param name="fmeainfo">节点的FMEA信息</param>
        public FTAGateNodeData() { }

        public FTAGateNodeData(string nodedataid, string nodename, GateType gatetype, string description)
        {
            this.nodedataid = nodedataid;
            this.nodename = nodename;
            this.gateType = gatetype;
            this.description = description;
            this.isFinished = false;
        }
        /// <summary>
        /// 更改节点类型
        /// </summary>
        /// <param name="nodetype">新节点类型</param>
        public void changeNodeType(GateType gatetype)
        {
            this.gateType = gatetype;
        }
        public FTAGateNodeData makeDuplicate()
        {
            FTAGateNodeData newnodedata = new FTAGateNodeData(Guid.NewGuid().ToString(), this.nodeName + "副本", this.gateType, this.description);
            newnodedata.isFinished = this.isFinished;
            return newnodedata;
        }
    }
}