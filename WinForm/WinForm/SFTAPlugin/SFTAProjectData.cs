using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Core.Data;
using Platform.Core;
using System.Xml;
using System.Xml.Serialization;

namespace SFTAPlugin
{
    [System.Serializable]

    [XmlInclude(typeof(SFTATreeInfoTable)),XmlInclude( typeof(SFTANodeRelationTable)),XmlInclude(typeof(FTAGateNodeData)),XmlInclude(typeof(FTANodeData)),XmlInclude(typeof(FTANodeRelation))]
    public class SFTAProjectData:AbstractProjectData
    {
        public SFTAProjectData() { }

        public override string Suffix
        {
            get { return "fta"; }
        }

        public List<SFTATreeInfoTable> SFTATreeNodes = new List<SFTATreeInfoTable>();
        public List<SFTANodeRelationTable> SFTARelations = new List<SFTANodeRelationTable>();

        //public override void GenerateProjectData()
        //{
        //    SFTAProject project =(SFTAProject)ProjectManager.ProjectManagerSington.GetCurrentProject();
        //    foreach(KeyValuePair<string,FTATreeInfo> tree in project.SFTATreesDic)
        //    {
        //        foreach (FTATreeNodeInfo node in tree.Value.ftatreenodeinfolist)
        //            SFTATreeNodes.Add(node);
        //    }
        //}

        public override void GenerateProjectData()
        {
            SFTAProject project = (SFTAProject)ProjectManager.ProjectManagerSington.GetCurrentProject();
            foreach (KeyValuePair<string, FTATreeInfo> tree in project.SFTATreesDic)
            {
                foreach (FTATreeNodeInfo node in tree.Value.ftatreenodeinfolist)
                {
                    //先加入SFTATreeInfoTable
                    if(node.nodedata is FTAEventNodeData)
                        SFTATreeNodes.Add(new SFTATreeInfoTable(node.nodeID,node.nodedata.nodeDataID,node.nodedata.nodeName,node.nodedata.description,((FTAEventNodeData)node.nodedata).eventType.ToString(),node.noderelation.parentNodeID,node.noderelation.siblingnodeID,((FTAEventNodeData)node.nodedata).FMEAInfo));
                    else if(node.nodedata is FTAGateNodeData)
                        SFTATreeNodes.Add(new SFTATreeInfoTable(node.nodeID,node.nodedata.nodeDataID,node.nodedata.nodeName,node.nodedata.description,((FTAGateNodeData)node.nodedata).gateType.ToString(),node.noderelation.parentNodeID,node.noderelation.siblingnodeID,string.Empty));
                    foreach(string childid in node.noderelation.childrenNodesID)
                        SFTARelations.Add(new SFTANodeRelationTable(node.nodeID,childid));
                }
            }
        }
    }
}
