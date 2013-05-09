using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SFTAPlugin
{
    [Serializable]
    public class FTATreeInfo//:System.Collections.CollectionBase,ISerializable
    {
        public Dictionary<int, List<FTATreeNodeInfo>> cutsetdic = new Dictionary<int, List<FTATreeNodeInfo>>();
        int cutnum = 0;
        public List<FTATreeNodeInfo> ftatreenodeinfolist = new List<FTATreeNodeInfo>();
        List<FTAEventNodeData> ftatreenodedatalist = new List<FTAEventNodeData>();
        
        
        public void Add(FTATreeNodeInfo tni)
        {
            ftatreenodeinfolist.Add(tni);
        }

        public void Merge(List<FTATreeNodeInfo> anotherlist)
        {
            if (anotherlist != null)
            {
                foreach (FTATreeNodeInfo tni in anotherlist)
                    this.ftatreenodeinfolist.Add(tni);
            }
        }


        public FTATreeInfo()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public FTATreeInfo(SerializationInfo info, StreamingContext context)
        {
            for (int i = 0; i < info.MemberCount; i++)
                ftatreenodeinfolist.Add((FTATreeNodeInfo)info.GetValue("TreeNodeInfo " + i.ToString(), typeof(FTATreeNodeInfo)));
        }
        /// <summary>
        /// 创建新事件
        /// </summary>
        /// <param name="parentID">父节点ID</param>
        /// <param name="level">父节点level</param>
        /// <param name="eventtype">事件类型</param>
        public void CreateEvent(string parentID,int level,EventType eventtype)
        {
            FTATreeNodeInfo parentNode = this.SearchTreeNodeInfo(parentID);//获取父节点
            int nodelevel = parentNode.level + 1;
            if (level < nodelevel)
                level = nodelevel;
            string childnodeID = Guid.NewGuid().ToString();//节点及数据ID
            FTATreeNodeInfo tni = new FTATreeNodeInfo(childnodeID, nodelevel, new FTAEventNodeData(childnodeID, "新节点"+this.TotalEvents().ToString(), eventtype, "节点描述信息", string.Empty), new FTANodeRelation(childnodeID, parentID, new List<string>()));
            this.Add(tni);
            parentNode.noderelation.childrenNodesID.Add(tni.noderelation.nodeRelationID);
        }
        /// <summary>
        /// 创建新门
        /// </summary>
        /// <param name="parentID">父节点id</param>
        /// <param name="level">父节点level</param>
        /// <param name="gatetype">门类型</param>
        public void CreateGate(string parentID, int level, GateType gatetype,string name)
        {
            FTATreeNodeInfo parentNode = this.SearchTreeNodeInfo(parentID);//获取父节点
            int nodelevel = parentNode.level + 1;
            if (level < nodelevel)
                level = nodelevel;
            string childnodeID = Guid.NewGuid().ToString();
            FTATreeNodeInfo tni = new FTATreeNodeInfo(childnodeID, nodelevel, new FTAGateNodeData(childnodeID, name, gatetype, "节点描述信息"), new FTANodeRelation(childnodeID, parentID, new List<string>()));
            this.Add(tni);
            parentNode.noderelation.childrenNodesID.Add(tni.noderelation.nodeRelationID);
        }
        /// <summary>
        /// 获取叶子节点数的最大值，用于设置画布上每一个节点的大小和位置
        /// </summary>
        /// <returns>叶子节点的最大值</returns>
        public int GetMaximumNodes()
        {
            int count = 0;
            foreach (FTATreeNodeInfo info in ftatreenodeinfolist)
            {
                if (info.noderelation.childrenNodesID.Count == 0)
                    count++;
            }
            return count;
        }

        /// <summary>
        /// 全部节点数量
        /// </summary>
        /// <returns></returns>
        public int TotalNodes()
        {
            return this.ftatreenodeinfolist.Count;
        }

        /// <summary>
        /// 门数量
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public int TotalGates()
        {
            int count=0;
            foreach(FTATreeNodeInfo tni in this.ftatreenodeinfolist)
            {
                if (tni.nodedata is FTAGateNodeData)
                    count++;
            }
            return count;
        }

        /// <summary>
        /// 计算事件元素数量
        /// </summary>
        /// <returns>事件元素数量</returns>
        public int TotalEvents()
        {
            int count = 0;
            foreach (FTATreeNodeInfo tni in this.ftatreenodeinfolist)
            {
                if (tni.nodedata is FTAEventNodeData)
                    count++;
            }
            return count;
        }

        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    for (int i = 0; i < Count; i++)
        //        info.AddValue("TreeNodeInfo " + i.ToString(), ftatreenodeinfolist[i]);
        //}

        /// <summary>
        /// 根据ID搜索节点信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns>节点信息</returns>
        public FTATreeNodeInfo SearchTreeNodeInfo(string id)
        {
            //搜索节点id
            FTATreeNodeInfo tni = null;
            foreach (FTATreeNodeInfo info in ftatreenodeinfolist)
            {
                if (info.nodeID.Equals(id))
                {
                    return info;
                }
            }
            return tni;
            //List<FTATreeNodeInfo> tnilist = new List<FTATreeNodeInfo>();
            //foreach (FTATreeNodeInfo info in ftatreenodeinfolist)
            //{
            //    if (info.noderelation.nodeRelationID.Equals(id))
            //    {
            //        tnilist.Add(info);
            //    }               
            //}
            //if (tnilist.Count == 1)
            //    return tnilist[0];
            //else
            //    if (tnilist[0].nodeID == tnilist[0].noderelation.nodeRelationID)
            //        return tnilist[0];
            //    else
            //        return tnilist[1];
        }

        public FTATreeNodeInfo SearchTreeNodeData(string id)
        {
            FTATreeNodeInfo tni = null;
            foreach (FTATreeNodeInfo info in ftatreenodeinfolist)
            {
                if (info.nodedata.nodeDataID.Equals(id))
                {
                    return info;
                }
            }
            return tni;
        }

        /// <summary>
        /// 搜索包含id的树的树根
        /// </summary>
        /// <param name="id">节点id</param>
        /// <returns>树根id</returns>
        public string SearchRootIDIfContain(string id)
        {
            string rootid = string.Empty;
            foreach (FTATreeNodeInfo info in ftatreenodeinfolist)
            {
                if (info.nodeID.Equals(id))
                {
                    return this.SearchRootNode().nodeID;
                }
            }
            return rootid;
        }
        /// <summary>
        /// 搜索根节点
        /// </summary>
        /// <returns></returns>
        public FTATreeNodeInfo SearchRootNode()
        {
            FTATreeNodeInfo tni = null;
            foreach (FTATreeNodeInfo info in ftatreenodeinfolist)
            {
                if (info.level==0)
                {
                    return info;
                }
            }
            return tni;
        }

        /// <summary>
        /// 搜索本树的全部子树，即所有"出事件"的数量
        /// </summary>
        /// <returns></returns>
        public List<FTATreeNodeInfo> SearchChildTrees()
        {
            List<FTATreeNodeInfo> childtrees = new List<FTATreeNodeInfo>();
            foreach (FTATreeNodeInfo info in ftatreenodeinfolist)
            {
                if ((info.nodedata is FTAEventNodeData)&&((FTAEventNodeData)info.nodedata).eventType==EventType.EventOut)
                {
                    childtrees.Add(info);
                }
            }
            return childtrees;
        }
        /// <summary>
        /// 根据ID查找子树，并制作子树的副本（即除ID和事件名称外，其他信息相同）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<FTATreeNodeInfo> GetDescendant(string id)
        {
            List<FTATreeNodeInfo> descendant = new List<FTATreeNodeInfo>();
            FTATreeNodeInfo parentnode = this.SearchTreeNodeInfo(id);

            string parentduplicateid = Guid.NewGuid().ToString();
            FTANodeData parentduplicatedata;
            if (parentnode.nodedata is FTAEventNodeData)
                parentduplicatedata = ((FTAEventNodeData)parentnode.nodedata).makeDuplicate();//拷贝节点data
            else
                parentduplicatedata = ((FTAGateNodeData)parentnode.nodedata).makeDuplicate();//拷贝节点data

            //修改节点data的id和名称，并创建新的节点副本
            FTATreeNodeInfo parentduplicate = new FTATreeNodeInfo(parentduplicateid, -1, parentduplicatedata, new FTANodeRelation(parentduplicateid, string.Empty, new List<string>()));//更改id、名称，其它信息保持不变
            descendant.Add(parentduplicate);
            if(parentnode.noderelation.childrenNodesID.Count!=0)
                GetDescendant(parentnode,ref parentduplicate, ref descendant);
            return descendant;
        }

        public void GetDescendant(FTATreeNodeInfo parentnode, ref FTATreeNodeInfo parentduplicate,ref List<FTATreeNodeInfo> descendant)
        {
            if (parentnode.noderelation.siblingnodeID != null && parentnode.noderelation.siblingnodeID != string.Empty)
            {
                FTATreeNodeInfo siblingnode= this.SearchTreeNodeInfo(parentnode.noderelation.siblingnodeID);
                string siblingduplicateid = Guid.NewGuid().ToString();
                FTANodeData siblingduplicatedata;
                if (siblingnode.nodedata is FTAEventNodeData)
                    siblingduplicatedata = ((FTAEventNodeData)siblingnode.nodedata).makeDuplicate();//拷贝节点data
                else
                    siblingduplicatedata = ((FTAGateNodeData)siblingnode.nodedata).makeDuplicate();//拷贝节点data

                //修改节点data的id和名称，并创建新的节点副本
                FTATreeNodeInfo siblingduplicate = new FTATreeNodeInfo(siblingduplicateid, -1, siblingduplicatedata, new FTANodeRelation(Guid.NewGuid().ToString(), siblingduplicateid, new List<string>()));//更改id、名称，其它信息保持不变
                descendant.Add(siblingduplicate);
                siblingduplicate.noderelation.siblingnodeID = parentduplicate.noderelation.nodeRelationID;
                parentduplicate.noderelation.siblingnodeID=siblingduplicate.noderelation.nodeRelationID;
            }

            foreach (string childid in parentnode.noderelation.childrenNodesID)
            {
                FTATreeNodeInfo childnode = this.SearchTreeNodeInfo(childid);

                string childduplicateid = Guid.NewGuid().ToString();
                FTANodeData childduplicatedata;
                if (childnode.nodedata is FTAEventNodeData)
                    childduplicatedata = ((FTAEventNodeData)childnode.nodedata).makeDuplicate();//拷贝节点data
                else
                    childduplicatedata = ((FTAGateNodeData)childnode.nodedata).makeDuplicate();//拷贝节点data

                //修改节点data的id和名称，并创建新的节点副本
                FTATreeNodeInfo childduplicate = new FTATreeNodeInfo(childduplicateid, -2, childduplicatedata, new FTANodeRelation(childduplicateid, string.Empty, new List<string>()));//更改id、名称，其它信息保持不变
                descendant.Add(childduplicate);
                childduplicate.noderelation.parentNodeID = parentduplicate.noderelation.nodeRelationID;
                parentduplicate.noderelation.childrenNodesID.Add(childduplicate.noderelation.nodeRelationID);
                if(childnode.noderelation.childrenNodesID.Count!=0)
                    GetDescendant(childnode, ref childduplicate, ref descendant);
            }
        }
        /// <summary>
        /// 递归更改子树的全部level，在粘贴子树时调用
        /// </summary>
        /// <param name="parentNode">子树的根节点</param>
        public void ChangeOffSpringLevel(FTATreeNodeInfo parentNode)
        {
            if (parentNode.noderelation.siblingnodeID!=null&&parentNode.noderelation.siblingnodeID != string.Empty)
                this.SearchTreeNodeInfo(parentNode.noderelation.siblingnodeID).level = parentNode.level;
            if (parentNode.noderelation.childrenNodesID.Count != 0)
            {
                foreach (string childid in parentNode.noderelation.childrenNodesID)
                {
                    FTATreeNodeInfo childnode = this.SearchTreeNodeInfo(childid);
                    childnode.level = parentNode.level + 1;
                    if (childnode.noderelation.childrenNodesID.Count != 0)
                        ChangeOffSpringLevel(childnode);
                }
            }
        }
        /// <summary>
        /// 递归获取全部子节点
        /// </summary>
        /// <returns></returns>
        public void GetAllDescendants(FTATreeNodeInfo  parentNode,ref List<FTATreeNodeInfo> treeinfo)
        {
            if (parentNode.noderelation.siblingnodeID != null && parentNode.noderelation.siblingnodeID != string.Empty)
                treeinfo.Add(this.SearchTreeNodeInfo(parentNode.noderelation.siblingnodeID));//添加兄弟节点
            if (parentNode.noderelation.childrenNodesID.Count != 0)
            {
                foreach (string childid in parentNode.noderelation.childrenNodesID)
                {
                    FTATreeNodeInfo childnode = this.SearchTreeNodeInfo(childid);
                    treeinfo.Add(childnode);//添加子节点
                    if (childnode.noderelation.childrenNodesID.Count != 0)
                        GetAllDescendants(childnode,ref treeinfo);
                }
            }
        }

        public List<string> GetAllAncestors(string id)
        {
            List<string> ancestorsid = new List<string>();
            FTATreeNodeInfo tni = this.SearchTreeNodeInfo(id);
            ancestorsid.Add(id);
            GetAncestor(id, ref ancestorsid);

            return ancestorsid;
        }

        private void GetAncestor(string id,ref List<string>ancestors)
        {
            string parentid=this.SearchTreeNodeInfo(id).noderelation.parentNodeID;
            ancestors.Add(parentid);

            if (parentid!=null)
            {
                GetAncestor(parentid, ref ancestors);
            }
        }
        //public List<FTATreeNodeInfo> SearchChildTrees(FTATreeNodeInfo tni)
        //{
        //    List<FTATreeNodeInfo> childtrees = new List<FTATreeNodeInfo>();
        //    foreach(string childid in tni.childrenNodesID)
        //    {
        //        if (SearchTreeNodeInfo(childid).siblingnodeID == null)
        //        {
        //            SearchChildTrees(SearchTreeNodeInfo(childid));                 
        //        }
        //        else if(SearchTreeNodeInfo(SearchTreeNodeInfo(childid).siblingnodeID).nodedata.nodeType == NodeType.EventOut)
        //        {
        //            childtrees.Add(SearchTreeNodeInfo(childid)); 
        //        }
        //        else
        //            SearchChildTrees(SearchTreeNodeInfo(childid));                 
        //    }
        //    return childtrees;
        //}

        
        ///// <summary>
        ///// 广度优先遍历
        ///// </summary>
        ///// <param name="temproot"></param>
        ///// <returns></returns>
        //public string SearchChildTree(string temproot)
        //{
        //    string childtreeid=string.Empty;
        //    foreach (string childid in SearchTreeNodeInfo(temproot).childrenNodesID)
        //    {
        //        FTATreeNodeInfo childnode = SearchTreeNodeInfo(childid);
        //        if (childnode.nodedata.nodeType == NodeType.EventOut)
        //            return childnode.siblingnodeID;
        //    }
        //    return childtreeid;
        //}
        /// <summary>
        /// 计算最小割集并返回
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, List<FTATreeNodeInfo>> GenerateMinimumCutSet()
        {
            cutsetdic.Clear();
            FTATreeInfo duplicatedtree = new FTATreeInfo();//一个树的副本，用于最小割集生成时的算法
            duplicatedtree.ftatreenodeinfolist = new List<FTATreeNodeInfo>();     //制作树的副本
            this.ftatreenodeinfolist.ForEach((item) =>
            {
                duplicatedtree.Add(new FTATreeNodeInfo(item));
            });
            foreach (FTATreeNodeInfo tni in duplicatedtree.ftatreenodeinfolist)
            {
                if (tni.nodedata is FTAEventNodeData && tni.noderelation.parentNodeID == null)//节点为事件，且父节点为空，则此节点为顶事件
                {
                    List<FTATreeNodeInfo> tnilist = new List<FTATreeNodeInfo>();//创建最小割集
                    tnilist.Add(tni);//加入顶事件
                    cutsetdic.Add(0, tnilist);//将第一个集合加入词典中
                    MinimumCutSet(tni, 0, tnilist,duplicatedtree);//计算最小割集
                }
                break;
            }
            List<int> duplicatedset = new List<int>();
            foreach (KeyValuePair<int, List<FTATreeNodeInfo>> pair1 in cutsetdic)
            {
                foreach (KeyValuePair<int, List<FTATreeNodeInfo>> pair2 in cutsetdic)
                {
                    if (!pair1.Equals(pair2)&&pair1.Value.Count>pair2.Value.Count)//若集合1不等于集合2，且集合1中元素多于集合2中元素
                    {
                        Boolean isequal = true;
                        foreach (FTATreeNodeInfo tni in pair2.Value)
                        {
                            if (!pair1.Value.Contains(tni))
                            {
                                isequal = false;
                                break;
                            }
                        }
                        if(isequal==true)
                            duplicatedset.Add(pair1.Key);
                    }
                }
            }
            foreach (int i in duplicatedset)
                cutsetdic.Remove(i);
            return cutsetdic;
        }

        /// <summary>
        /// 最小割集生成算法
        /// </summary>
        /// <param name="tni">现节点</param>
        /// <param name="previouslistnum">现节点所在list的key</param>
        /// <param name="previouslist">现节点所在的集合</param>
        private void MinimumCutSet(FTATreeNodeInfo tni, int previouslistnum, List<FTATreeNodeInfo> previouslist,FTATreeInfo duplicatedtree)
        {
            List<string> childnodesid = tni.noderelation.childrenNodesID;//获取子节点ID
            if (childnodesid.Count > 0)//判断是否为叶子节点，非叶子节点进入下一步
                foreach (string childnodeid in childnodesid)
                {
                    FTATreeNodeInfo childnode = duplicatedtree.SearchTreeNodeInfo(childnodeid);//获取每一个子节点
                    if (childnode.nodedata is FTAGateNodeData)//若为门
                    {
                        switch (((FTAGateNodeData)childnode.nodedata).gateType)//判断子节点类型
                        {
                            case GateType.GateAnd://遇见与门，增加阶数
                                {
                                    previouslist.Remove(tni);//从当前集合中移除当前节点
                                    foreach (string childofgateid in childnode.noderelation.childrenNodesID)//
                                    {
                                        FTATreeNodeInfo treenodeinfo = duplicatedtree.SearchTreeNodeInfo(childofgateid);//搜索门下的所有事件
                                        previouslist.Add(treenodeinfo);//将全部子节点一一存入当前集合中
                                        cutsetdic.Remove(previouslistnum);//从词典中删除父节点所在的集合
                                        Boolean isExisted = false;
                                        foreach (List<FTATreeNodeInfo> existedlist in cutsetdic.Values)
                                        {
                                            if (existedlist.Count == previouslist.Count)     //检查元素数量相同的集合
                                                foreach (FTATreeNodeInfo info in previouslist)
                                                    if (!existedlist.Contains(info))       //检测
                                                    {
                                                        isExisted = false;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        isExisted = true;
                                                    }
                                            if (isExisted == true)
                                                break;
                                        }
                                        if (isExisted == false)
                                            cutsetdic.Add(previouslistnum, previouslist);//将新集合加入割集
                                        //MinimumCutSet(treenodeinfo, cutnum, previouslist);//递归，继续计算
                                    }
                                    foreach (FTATreeNodeInfo eventnode in previouslist)
                                    {
                                        List<FTATreeNodeInfo> templist = new List<FTATreeNodeInfo>();
                                        foreach (FTATreeNodeInfo tempnode in previouslist)
                                            templist.Add(tempnode);
                                        MinimumCutSet(eventnode, previouslistnum, templist,duplicatedtree);//将待递归的序列拷贝副本
                                    }
                                    break;
                                }
                            case GateType.GateOr:
                                {
                                    previouslist.Remove(tni);//从当前集合中移除当前节点，以便加入其子节点
                                    cutsetdic.Remove(previouslistnum);//从最小割集中移除当前集合
                                    //cutnum--;//减小当前集合key
                                    foreach (string childofgateid in childnode.noderelation.childrenNodesID)
                                    {
                                        cutnum++;//增加集合key值
                                        List<FTATreeNodeInfo> tnilist = new List<FTATreeNodeInfo>();//创建新集合
                                        foreach (FTATreeNodeInfo othernode in previouslist)//代替下一被注释行
                                            tnilist.Add(othernode);
                                        //tnilist.AddRange(previouslist);//将当前集合拷贝至新集合
                                        FTATreeNodeInfo treenodeinfo = duplicatedtree.SearchTreeNodeInfo(childofgateid);
                                        tnilist.Add(treenodeinfo);//将子节点加入新集合
                                        Boolean isExisted = false;
                                        foreach (List<FTATreeNodeInfo> existedlist in cutsetdic.Values)
                                        {
                                            if(existedlist.Count==tnilist.Count)
                                            foreach (FTATreeNodeInfo info in tnilist)
                                                if (!existedlist.Contains(info))
                                                {
                                                    isExisted = false;
                                                    break;
                                                }
                                                else
                                                {
                                                    isExisted = true;
                                                }
                                            if (isExisted == true)
                                                break;
                                        }
                                        if (isExisted == false)
                                            cutsetdic.Add(cutnum, tnilist);//将新集合加入割集
                                        //MinimumCutSet(treenodeinfo, cutnum, tnilist);//递归，计算计算子节点生成的割集
                                    }
                                    Dictionary<int, List<FTATreeNodeInfo>> tempsetdic = new Dictionary<int, List<FTATreeNodeInfo>>();
                                    foreach (KeyValuePair<int, List<FTATreeNodeInfo>> pair in cutsetdic)
                                        tempsetdic.Add(pair.Key,pair.Value);
                                    foreach(KeyValuePair<int,List<FTATreeNodeInfo>> pair in tempsetdic)
                                    {
                                        foreach (FTATreeNodeInfo eventnode in pair.Value)
                                        {
                                            List<FTATreeNodeInfo> templist = new List<FTATreeNodeInfo>();
                                            foreach (FTATreeNodeInfo tempnode in pair.Value)//将待递归的序列拷贝副本
                                                templist.Add(tempnode);
                                            MinimumCutSet(eventnode, pair.Key, templist,duplicatedtree);

                                        }
                                    }
                                    break;
                                }
                            case GateType.GateInhibit://遇见禁门，把开关作为子节点，禁门变成与门，增加阶数  ？？？？？？？？？
                                {
                                    previouslist.Remove(tni);//从当前集合中移除当前节点
                                    foreach (string childofgateid in childnode.noderelation.childrenNodesID)//
                                    {
                                        FTATreeNodeInfo treenodeinfo = duplicatedtree.SearchTreeNodeInfo(childofgateid);//搜索门下的所有事件
                                        previouslist.Add(treenodeinfo);//将全部子节点一一存入当前集合中
                                        cutsetdic.Remove(previouslistnum);//从词典中删除父节点所在的集合
                                        Boolean isExisted = false;
                                        foreach (List<FTATreeNodeInfo> existedlist in cutsetdic.Values)
                                        {
                                            if (existedlist.Count == previouslist.Count)     //检查元素数量相同的集合
                                                foreach (FTATreeNodeInfo info in previouslist)
                                                    if (!existedlist.Contains(info))       //检测
                                                    {
                                                        isExisted = false;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        isExisted = true;
                                                    }
                                            if (isExisted == true)
                                                break;
                                        }
                                        if (isExisted == false)
                                            cutsetdic.Add(previouslistnum, previouslist);//将新集合加入割集
                                        //MinimumCutSet(treenodeinfo, cutnum, previouslist);//递归，继续计算
                                    }
                                    if (childnode.noderelation.siblingnodeID != null)
                                        previouslist.Add(this.SearchTreeNodeInfo(childnode.noderelation.siblingnodeID));//将条件事件作为兄弟节点加入
                                    foreach (FTATreeNodeInfo eventnode in previouslist)
                                    {
                                        List<FTATreeNodeInfo> templist = new List<FTATreeNodeInfo>();
                                        foreach (FTATreeNodeInfo tempnode in previouslist)
                                            templist.Add(tempnode);
                                        MinimumCutSet(eventnode, previouslistnum, templist, duplicatedtree);//将待递归的序列拷贝副本
                                    }
                                    break;
                                }
                            case GateType.GateSequenceAnd://遇见顺序与门，将条件事件作为子节点，并将顺序与门转为与门增加阶数
                                {
                                    previouslist.Remove(tni);//从当前集合中移除当前节点
                                    foreach (string childofgateid in childnode.noderelation.childrenNodesID)//
                                    {
                                        FTATreeNodeInfo treenodeinfo = duplicatedtree.SearchTreeNodeInfo(childofgateid);//搜索门下的所有事件
                                        previouslist.Add(treenodeinfo);//将全部子节点一一存入当前集合中
                                        cutsetdic.Remove(previouslistnum);//从词典中删除父节点所在的集合
                                        Boolean isExisted = false;
                                        foreach (List<FTATreeNodeInfo> existedlist in cutsetdic.Values)
                                        {
                                            if (existedlist.Count == previouslist.Count)     //检查元素数量相同的集合
                                                foreach (FTATreeNodeInfo info in previouslist)
                                                    if (!existedlist.Contains(info))       //检测
                                                    {
                                                        isExisted = false;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        isExisted = true;
                                                    }
                                            if (isExisted == true)
                                                break;
                                        }
                                        if (isExisted == false)
                                        {
                                            
                                            cutsetdic.Add(previouslistnum, previouslist);//将新集合加入割集
                                        }
                                        //MinimumCutSet(treenodeinfo, cutnum, previouslist);//递归，继续计算
                                    }
                                    if (childnode.noderelation.siblingnodeID != null)
                                        previouslist.Add(duplicatedtree.SearchTreeNodeInfo(childnode.noderelation.siblingnodeID));

                                    foreach (FTATreeNodeInfo eventnode in previouslist)
                                    {
                                        List<FTATreeNodeInfo> templist = new List<FTATreeNodeInfo>();
                                        foreach (FTATreeNodeInfo tempnode in previouslist)
                                            templist.Add(tempnode);

                                        MinimumCutSet(eventnode, previouslistnum, templist, duplicatedtree);//将待递归的序列拷贝副本
                                    }
                                    break;
                                }
                            case GateType.GateElect://遇见表决门，变成与或门再进行计算
                                {
                                    //previouslist.Remove(tni);//从当前集合中移除当前事件节点
                                    //开始分解r与n
                                    string r=string.Empty, n=string.Empty;
                                    int rvalue = 0, nvalue = 0;
                                    r = childnode.nodedata.nodeName.Split('/')[0];
                                    n = childnode.nodedata.nodeName.Split('/')[1];
                                    try
                                    {//将r与n转换为数值类型，并判断
                                        rvalue = int.Parse(r);
                                        nvalue = int.Parse(n);
                                        
                                    }
                                    catch (FormatException ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                        return;
                                    }
                                    if (rvalue > nvalue || r == n)//判定是否满足约束条件
                                    {
                                        MessageBox.Show("错误！r必须小于n！");
                                        return;
                                    }

                                    ConvertElectToOr(tni, rvalue, nvalue,ref duplicatedtree);
                                    MinimumCutSet(tni, previouslistnum, previouslist, duplicatedtree);

                                    //foreach (string childofgateid in childnode.noderelation.childrenNodesID)//
                                    //{
                                    //    FTATreeNodeInfo treenodeinfo = duplicatedtree.SearchTreeNodeInfo(childofgateid);//搜索门下的所有事件
                                    //    previouslist.Add(treenodeinfo);//将全部子节点一一存入当前集合中
                                    //    cutsetdic.Remove(previouslistnum);//从词典中删除父节点所在的集合
                                    //    Boolean isExisted = false;
                                    //    foreach (List<FTATreeNodeInfo> existedlist in cutsetdic.Values)
                                    //    {
                                    //        if (existedlist.Count == previouslist.Count)     //检查元素数量相同的集合
                                    //            foreach (FTATreeNodeInfo info in previouslist)
                                    //                if (!existedlist.Contains(info))       //检测
                                    //                {
                                    //                    isExisted = false;
                                    //                    break;
                                    //                }
                                    //                else
                                    //                {
                                    //                    isExisted = true;
                                    //                }
                                    //        if (isExisted == true)
                                    //            break;
                                    //    }
                                    //    if (isExisted == false)
                                    //        cutsetdic.Add(previouslistnum, previouslist);//将新集合加入割集
                                    //    //MinimumCutSet(treenodeinfo, cutnum, previouslist);//递归，继续计算
                                    //}
                                    //foreach (FTATreeNodeInfo eventnode in previouslist)
                                    //{
                                    //    List<FTATreeNodeInfo> templist = new List<FTATreeNodeInfo>();
                                    //    foreach (FTATreeNodeInfo tempnode in previouslist)
                                    //        templist.Add(tempnode);
                                    //    MinimumCutSet(eventnode, previouslistnum, templist, duplicatedtree);//将待递归的序列拷贝副本
                                    //}
                                    break;
                                }
                            case GateType.GateXor: //异或门需要转换为非门和与或门再进行最小割集的计算
                                {
                                    //previouslist.Remove(tni);//从当前集合中移除当前事件节点
                                    ConvertXorToOr(tni, ref duplicatedtree);
                                    MinimumCutSet(tni, previouslistnum, previouslist, duplicatedtree);
                                    break;
                                }
                            //case NodeType.EventOut:
                            //    {
                            //        previouslist.Remove(tni);//移除当前节点
                            //        foreach (string childofgateid in childnode.childrenNodesID)//
                            //        {
                            //            FTATreeNodeInfo treenodeinfo = SearchTreeNodeInfo(childofgateid);
                            //            previouslist.Add(treenodeinfo);//将全部子节点一一存入集合中
                            //            MinimumCutSet(treenodeinfo, cutnum, previouslist);//递归，继续计算
                            //        }
                            //        break;
                            //    }
                            //case NodeType.EventIn:
                            //    {
                            //        previouslist.Remove(tni);//移除当前节点
                            //        foreach (string childofgateid in childnode.childrenNodesID)//
                            //        {
                            //            FTATreeNodeInfo treenodeinfo = SearchTreeNodeInfo(childofgateid);
                            //            previouslist.Add(treenodeinfo);//将全部子节点一一存入集合中
                            //            MinimumCutSet(treenodeinfo, cutnum, previouslist);//递归，继续计算
                            //        }
                            //        break;
                            //    }
                        }
                    }
                }
        }
        /// <summary>
        /// 将表决门转换为与或门
        /// </summary>
        /// <param name="tni">表决门的父级事件节点</param>
        private void ConvertElectToOr(FTATreeNodeInfo tni,int r,int n,ref FTATreeInfo duplicatedtree)
        {
            FTATreeNodeInfo electgate= duplicatedtree.SearchTreeNodeInfo(tni.noderelation.childrenNodesID[0]);//获取表决门
            duplicatedtree.ftatreenodeinfolist.Remove(electgate);
            ((FTAGateNodeData)electgate.nodedata).gateType = GateType.GateOr;//将门转换为或门
            List<string> tempchildlist = new List<string>();
            int tempgatenum = factorial(n) / factorial(r) / factorial(n - r);//算出待添加的临时事件的数量
            List<int> index = new List<int>();
            for (int i = 0; i < r; i++)
            {
                index.Add(i);
            }
            for (int i = 0; i < tempgatenum; i++)
            { //创建临时事件，添加至或门下
                string nodeid = Guid.NewGuid().ToString();//临时事件的id
                FTATreeNodeInfo node=new FTATreeNodeInfo(nodeid,tni.level+1,new FTAEventNodeData(nodeid,"临时事件",EventType.EventIntermediate,string.Empty,string.Empty),new FTANodeRelation(nodeid,electgate.nodeID,new List<string>()));                
                
                string gateid = Guid.NewGuid().ToString();//临时门的id
                FTATreeNodeInfo gate = new FTATreeNodeInfo(gateid, tni.level + 2, new FTAGateNodeData(gateid, "与门", GateType.GateAnd, string.Empty), new FTANodeRelation(gateid, nodeid, new List<string>()));
                
                //将原异或门下的事件作为子节点链接至临时门
                for(int tempchildnum=0;tempchildnum<r;tempchildnum++)
                {                          
                    gate.noderelation.childrenNodesID.Add(electgate.noderelation.childrenNodesID[index[tempchildnum]]);
                    
                }
                int loop = 0;
                for (loop = r - 1; loop > 0||loop==0; loop--)//将最后一位加1，若加到最大值，则继续进位
                {
                    if (index[loop] < n-1&&(loop==r-1)||loop<r-1&&index[loop]+1!=index[loop+1])
                    { 
                        index[loop]++;
                        break;
                    }                       
                }
                loop++;
                for (; loop < r; loop++)//若产生进位，则将进位的后一位变为进位值加1的值
                {
                    if(loop>0&&index[loop-1]+1<n-1)
                        index[loop] = index[loop - 1] + 1;
                }
                //需要等到移除了electgate.noderelation.childrenNodesID.Add(nodeid);//将或门的子节点指向新的临时中间事件///
                tempchildlist.Add(nodeid);
                node.noderelation.childrenNodesID.Add(gateid);//将临时事件的子节点指向新的与门
                //最后再将新的节点添加至树中
                duplicatedtree.ftatreenodeinfolist.Add(node);                                  
                duplicatedtree.ftatreenodeinfolist.Add(gate);
            }
            electgate.noderelation.childrenNodesID.Clear();
            foreach(string nodeid in tempchildlist)
                electgate.noderelation.childrenNodesID.Add(nodeid);//将或门的子节点指向新的临时中间事件    
            duplicatedtree.ftatreenodeinfolist.Add(electgate);
        }
        /// <summary>
        /// 将异或门转换为非门和与或门
        /// </summary>
        /// <param name="tni">异或门的父节点</param>
        /// <param name="duplicatedtree">待更改的树</param>
        private void ConvertXorToOr(FTATreeNodeInfo tni, ref FTATreeInfo duplicatedtree)
        {
            FTATreeNodeInfo xorgate = duplicatedtree.SearchTreeNodeInfo(tni.noderelation.childrenNodesID[0]);//获取异或
            duplicatedtree.ftatreenodeinfolist.Remove(xorgate); //将异或门先移除，待更改完成后再添加上去
            ((FTAGateNodeData)xorgate.nodedata).gateType = GateType.GateOr;//将异或门转为或门

            string nodeid = Guid.NewGuid().ToString();//临时事件的id
            FTATreeNodeInfo node = new FTATreeNodeInfo(nodeid, tni.level + 1, new FTAEventNodeData(nodeid, "临时事件", EventType.EventIntermediate, string.Empty, string.Empty), new FTANodeRelation(nodeid, xorgate.nodeID, new List<string>()));
            string gateid = Guid.NewGuid().ToString();//临时门的id
            node.noderelation.childrenNodesID.Add(gateid);
            FTATreeNodeInfo gate = new FTATreeNodeInfo(gateid, tni.level + 2, new FTAGateNodeData(gateid, "与门", GateType.GateAnd, string.Empty), new FTANodeRelation(gateid, nodeid, new List<string>()));
            duplicatedtree.Add(node);
            duplicatedtree.Add(gate);
            //子事件1保持不变，子事件2及子树变非
            gate.noderelation.childrenNodesID.Add(xorgate.noderelation.childrenNodesID[0]);//获取子事件1，并作为子节点加到与门下
            FTATreeNodeInfo childnode2 = duplicatedtree.SearchTreeNodeInfo(xorgate.noderelation.childrenNodesID[1]);//获取子事件2
            FTATreeNodeInfo falsenode2 = childnode2.ConvertToNot();//制作子事件2的含非副本
            gate.noderelation.childrenNodesID.Add(falsenode2.nodeID);//将子事件2的含非副本作为子节点加到与门下
            ConvertToNot(falsenode2, ref duplicatedtree);//将子事件2的子树全部变非
            duplicatedtree.Add(falsenode2);

            string nodeid2 = Guid.NewGuid().ToString();//临时事件的id
            FTATreeNodeInfo node2 = new FTATreeNodeInfo(nodeid2, tni.level + 1, new FTAEventNodeData(nodeid2, "临时事件", EventType.EventIntermediate, string.Empty, string.Empty), new FTANodeRelation(nodeid2, xorgate.nodeID, new List<string>()));
            string gateid2 = Guid.NewGuid().ToString();//临时门的id
            node2.noderelation.childrenNodesID.Add(gateid2);
            FTATreeNodeInfo gate2 = new FTATreeNodeInfo(gateid2, tni.level + 2, new FTAGateNodeData(gateid2, "与门", GateType.GateAnd, string.Empty), new FTANodeRelation(gateid2, nodeid2, new List<string>()));
            duplicatedtree.Add(node2);
            duplicatedtree.Add(gate2);
            //子事件1及子树变非，子事件2保持不变
            gate2.noderelation.childrenNodesID.Add(xorgate.noderelation.childrenNodesID[1]);
            FTATreeNodeInfo childnode = duplicatedtree.SearchTreeNodeInfo(xorgate.noderelation.childrenNodesID[0]);//获取子事件1
            FTATreeNodeInfo falsenode = childnode.ConvertToNot();//制作子事件1的含非副本
            gate2.noderelation.childrenNodesID.Add(falsenode.nodeID);//将子事件1的含非副本作为子节点加到与门下
            ConvertToNot(falsenode, ref duplicatedtree);//将子事件1的子树全部变非
            duplicatedtree.Add(falsenode);

            xorgate.noderelation.childrenNodesID.Clear();
            xorgate.noderelation.childrenNodesID.Add(nodeid);
            xorgate.noderelation.childrenNodesID.Add(nodeid2);
            duplicatedtree.Add(xorgate);
        }

        void ConvertToNot(FTATreeNodeInfo tni, ref FTATreeInfo duplicatedtree)
        {
            //if (tni.noderelation.siblingnodeID != null && tni.noderelation.siblingnodeID != string.Empty)
            //    treeinfo.Add(this.SearchTreeNodeInfo(parentNode.noderelation.siblingnodeID));//添加兄弟节点
            List<string> children = new List<string>();
            if (tni.noderelation.childrenNodesID.Count != 0)//当不是叶子节点时，继续循环
            {
                foreach (string childid in tni.noderelation.childrenNodesID)
                {
                    FTATreeNodeInfo childnode =duplicatedtree.SearchTreeNodeInfo(childid);//获取每一个子节点
                    FTATreeNodeInfo falsenode = childnode.ConvertToNot();//制作子事件的含非副本；
                    duplicatedtree.Add(falsenode);//添加子节点
                    children.Add(falsenode.nodeID);//将新的变非子节点加入临时列表
                    if (falsenode.noderelation.childrenNodesID.Count != 0)
                        ConvertToNot(falsenode, ref duplicatedtree);
                }
                tni.noderelation.childrenNodesID.Clear();//清空原子节点
                foreach (string childid in children)
                    tni.noderelation.childrenNodesID.Add(childid);
            }
        }
        private int factorial(int i)
        {
            if (i == 1)
                return 1;
            else
                return i * factorial(i - 1);
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="currentid">待删除的节点id</param>
        public void DeleteTreeNode(string currentid)
        {
            FTATreeNodeInfo currentNode =SearchTreeNodeInfo(currentid);//获取当前节点
            if (currentNode.noderelation.childrenNodesID.Count == 0)//没有子节点，为叶子了
            {
                FTATreeNodeInfo parentNode = SearchTreeNodeInfo(currentNode.noderelation.parentNodeID);//父节点
                if (parentNode != null && !currentid.Equals(currentNode.noderelation.parentNodeID))//存在父节点
                {
                    parentNode.noderelation.childrenNodesID.Remove(currentid);//从父节点的儿子节点中移除本身
                    ftatreenodeinfolist.Remove(currentNode);
                    if (currentNode.noderelation.siblingnodeID != string.Empty)
                    {
                        FTATreeNodeInfo siblingNode = SearchTreeNodeInfo(currentNode.noderelation.siblingnodeID);
                        ftatreenodeinfolist.Remove(siblingNode);
                    }
                }
                else if (currentid.Equals(currentNode.noderelation.parentNodeID))//父节点本身，即为条件类事件
                {
                    FTATreeNodeInfo siblingNode = SearchTreeNodeInfo(currentNode.noderelation.siblingnodeID);
                    FTATreeNodeInfo sibparentNode = SearchTreeNodeInfo(siblingNode.noderelation.parentNodeID);
                    sibparentNode.noderelation.childrenNodesID.Remove(currentNode.noderelation.siblingnodeID);
                    ftatreenodeinfolist.Remove(currentNode);
                    ftatreenodeinfolist.Remove(siblingNode);
                } 
                ftatreenodeinfolist.Remove(currentNode);
            }  
            //中间节点暂时不允许删除
        }
        /// <summary>
        /// 完整性及正确性自动校验
        /// </summary>
        public Boolean AutoCheck()
        {
            Boolean isFinished = true;
            foreach (FTATreeNodeInfo tni in this.ftatreenodeinfolist)
            {
                tni.nodedata.isFinished = true;
                if (tni.nodedata is FTAGateNodeData)
                {
                    if (((FTAGateNodeData)tni.nodedata).gateType == GateType.GateXor)//异或门下有且只有两个节点
                        if (tni.noderelation.childrenNodesID.Count != 2)
                        {
                            tni.nodedata.isFinished = false;
                            isFinished = false;
                        }
                    if (((FTAGateNodeData)tni.nodedata).gateType == GateType.GateElect)//表决门内r/n必须r<n且确实具有n个儿子
                    {
                        string nodename = ((FTAGateNodeData)tni.nodedata).nodename;
                        int slash=nodename.IndexOf('/');
                        string r = nodename.Remove(slash);
                        string n = nodename.Substring(slash + 1, nodename.Length - 1 - slash);
                        int rvalue, nvalue;
                        try
                        {
                            rvalue = int.Parse(r);
                            nvalue = int.Parse(n);
                            if (rvalue > nvalue || rvalue == nvalue)
                            {
                                tni.nodedata.isFinished = false;
                                isFinished = false;
                            }
                            else
                            {
                                if (tni.noderelation.childrenNodesID.Count == nvalue)
                                {
                                    tni.nodedata.isFinished = true;
                                    isFinished = true;
                                }
                                else
                                {
                                    tni.nodedata.isFinished = false;
                                    isFinished = false;
                                }
                            }
                        }
                        catch (FormatException ex)
                        {
                            tni.nodedata.isFinished = false;
                            isFinished = false;
                        }
                    }
                }
                if (tni.noderelation.childrenNodesID.Count == 0)
                {
                    if (tni.nodedata is FTAGateNodeData)//门元素不能作为叶子节点
                    {
                        tni.nodedata.isFinished = false;
                        isFinished  = false;
                    }
                    //事件元素类型中，只有基本事件与未展开事件可以作为叶子节点
                    else if ((((FTAEventNodeData)tni.nodedata).eventType != EventType.EventBasic) && (((FTAEventNodeData)tni.nodedata).eventType != EventType.EventUndeveloped) && (((FTAEventNodeData)tni.nodedata).eventType != EventType.EventOut) && (((FTAEventNodeData)tni.nodedata).eventType != EventType.EventIn) && (((FTAEventNodeData)tni.nodedata).eventType != EventType.EventConditioning))
                    {
                        tni.nodedata.isFinished = false;
                        isFinished = false;
                    }                  
                }
            }
            return isFinished;
        }
    }
}

