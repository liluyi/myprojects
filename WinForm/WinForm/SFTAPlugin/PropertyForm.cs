using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Platform.Core.UI;
using Platform.Core.Services;
using Platform.Core;
using Platform.Core.Data;

namespace SFTAPlugin
{
    public delegate void PropertyChangeHandler();
    
    public partial class PropertyForm : BaseForm
    {
        public PropertyChangeHandler PropertyChange;
        FTANodeData ftanodedata;
        FTANodeRelation ftanoderelation;
        string nodeID;
        public PropertyForm()
        {
            InitializeComponent();
        }
        public PropertyForm(string nodeid,FTANodeData ftadata,FTANodeRelation ftanoderelation)
        {
            InitializeComponent();
            this.ftanodedata = ftadata;
            this.nodeID = nodeid;
            this.ftanoderelation=ftanoderelation;
            RefreshForm(this.nodeID,this.ftanodedata);
        }
        public void RefreshForm(string nodeid,FTANodeData ftadata0)
        {
            this.nodeID = nodeid;
            this.ftanodedata = ftadata0;
            FTANodepropertyGrid.SelectedObject = ftadata0;
            this.Refresh();
        }

        /// <summary>
        /// 更改属性值时发生响应
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e">更改的属性组</param>
        private void FTANodepropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "nodeName")//事件共享
            {
                string nodename=e.ChangedItem.Value.ToString();//属性更改后的值
                //寻找是否存在同名事件，如果存在，则二者共享此同名事件
                SFTAProject currentproject = (SFTAProject)ProjectManager.ProjectManagerSington.GetCurrentProject();//当前工程
                //FTATreeInfo oldtree, newtree;
                bool hasfind = false;//标志是否已经找到同名事件
                foreach (FTATreeInfo ftatree in currentproject.SFTATreesDic.Values)//查找本工程下全部树
                {
                    foreach (FTATreeNodeInfo ftatni in ftatree.ftatreenodeinfolist)//查找树中全部节点信息
                    {
                        if (ftatni.nodedata.nodeName == nodename&&ftatni.nodeID!=this.nodeID)//判定节点事件名称是否与更改后的名称相同且不是本节点
                        {
                            DialogResult dr = MessageBox.Show("当前工程存在同名事件，是否将本事件设置为同一事件？", "提示", MessageBoxButtons.YesNo);//提示是否共享事件
                            if (dr == DialogResult.No)
                            {//不共享
                                this.ftanodedata.nodeName = e.OldValue.ToString();//将属性值更改回原始值
                                return;
                            }
                            else
                            {
                                //不需要，当前ftatree以及ftatni等均为引用！！！！！！！！
                                //oldtree = ftatree;
                                //FTATreeNodeInfo temptni = ftatree.SearchTreeNodeInfo(this.nodeID);//根据当前节点ID查找当前节点
                                //ftatree.ftatreenodeinfolist.Remove(temptni);//移除当前节点
                                //temptni.nodedata = ftatni.nodedata;//将当前节点的事件绑定至同一事件
                                //ftatree.ftatreenodeinfolist.Add(temptni);//将当前事件重新加入故障树节点列表中
                                //newtree = ftatree;
                                //currentproject.SFTATrees.Remove(oldtree);
                                //currentproject.SFTATrees.Add(newtree);//将更改后的故障树添加至本工程的故障树列表中
                                foreach (FTATreeNodeInfo tni in ftatree.ftatreenodeinfolist)
                                {
                                    if (tni.noderelation.childrenNodesID.Contains(this.nodeID))
                                    {
                                        string parentID = tni.nodeID;
                                        FTATreeNodeInfo parentNode = ftatree.SearchTreeNodeInfo(parentID);//获取父节点
                                        if (ftatree.GetAllAncestors(parentID).Contains(ftatni.nodeID) || ftatree.SearchTreeNodeInfo(parentID).noderelation.childrenNodesID.Contains(ftatni.nodeID))//原来：clipboard.noderelation.parentNodeID == parentID||
                                        {//检测是否粘贴位置的节点的子节点中已包含本人，或者粘贴位置的节点的祖先中已包含本人
                                            MessageBox.Show("子树冗余共享错误！", "禁止");
                                        }
                                        else
                                        {
                                            if (parentNode.nodedata is FTAGateNodeData)//判断是否为门元素
                                            {
                                                ((FTAEventNodeData)ftatni.nodedata).isDuplicated = true;//设置节点类型为共享
                                                parentNode.noderelation.childrenNodesID.Add(ftatni.nodeID);//粘贴子树
                                                parentNode.noderelation.childrenNodesID.Remove(this.nodeID);
                                            }
                                        }
                                    }
                                }
                


                                hasfind = true;//标识已找到同名事件
                                break;
                            }
                        }
                    }
                    if (hasfind)
                        break;//已找到同名事件，返回
                }                
            }
            ///分离了门节点与事件节点，因此不再在此进行强制归约
            else if (e.ChangedItem.Label == "eventType")//事件节点类型转换时的约束
            {
                string oldtype = e.OldValue.ToString();//原始节点类型
                string newtype = e.ChangedItem.Value.ToString();//属性更改后的值
                ///约束条件
                ///顶事件：不能转为任何其它事件；子树的顶事件：不能转为任何其它事件；出事件、入事件：不能转为任何其它事件
                ///转基本事件：必须没有子节点；转未展开事件：必须没有子节点
                if (oldtype.Equals("EventIntermediate") && this.ftanoderelation.parentNodeID == null)
                {
                    MessageBox.Show("类型转换不合法！顶事件不能转换任何其它事件");
                    ((FTAEventNodeData)this.ftanodedata).eventType = (EventType)e.OldValue;//将属性值更改回原始值
                    return;
                }
                else if (oldtype.Equals("EventIntermediate") && this.ftanoderelation.siblingnodeID != null)
                {
                    MessageBox.Show("类型转换不合法！子树的顶事件不能转换任何其它事件");
                    ((FTAEventNodeData)this.ftanodedata).eventType = (EventType)e.OldValue;//将属性值更改回原始值
                    return;
                }
                if (oldtype.Equals("EventIn") || oldtype.Equals("EventOut"))
                {
                    MessageBox.Show("类型转换不合法！出三角形和入三角形不能转换任何其它类型");
                    ((FTAEventNodeData)this.ftanodedata).eventType = (EventType)e.OldValue;//将属性值更改回原始值
                    return;
                }
                if (newtype.Equals("EventIn") || newtype.Equals("EventOut") || newtype.Equals("EventConditioning")||newtype.Equals("Error"))
                {
                    MessageBox.Show("类型转换不合法！事件类型不能转换为出三角形和入三角形或条件事件！");
                    ((FTAEventNodeData)this.ftanodedata).eventType = (EventType)e.OldValue;//将属性值更改回原始值
                    return;
                }
                if ((newtype.Equals("EventBasic") || newtype.Equals("EventUndeveloped") || newtype.Equals("EventInitiating")) && ftanoderelation.childrenNodesID.Count != 0)
                {
                    MessageBox.Show("类型转换不合法！基本事件、未展开事件、正常事件不能有任何子节点，请先删除子节点再进行转换！");
                    ((FTAEventNodeData)this.ftanodedata).eventType = (EventType)e.OldValue;//将属性值更改回原始值
                    return;
                }

                //((FTAEventNodeData)this.ftanodedata).eventType = (EventType)e.OldValue;//将属性值更改回原始值
                //return;
            }
            else if (e.ChangedItem.Label == "gateType")//事件节点类型转换时的约束
            {
                string oldtype = e.OldValue.ToString();//原始节点类型
                string newtype = e.ChangedItem.Value.ToString();//属性更改后的值
                ///约束条件
                if (newtype.Equals("GateXor") && this.ftanoderelation.childrenNodesID.Count > 2)
                {
                    MessageBox.Show("类型转换不合法！异或门下有且只能有2个事件！");
                    ((FTAGateNodeData)this.ftanodedata).gateType = (GateType)e.OldValue;//将属性值更改回原始值
                    return;
                }
                else if (newtype.Equals("GateXor") && this.ftanoderelation.childrenNodesID.Count < 3)
                {
                    this.ftanodedata.nodeName = "异或门";
                }
                else if (newtype.Equals("GateElect"))
                {//转为表决门时，必须填入m/n的值
                    ElectForm rnform = new ElectForm();
                    if (rnform.ShowDialog() == DialogResult.Yes)
                    {
                        this.ftanodedata.nodeName = rnform.r + "/" + rnform.n;
                    }
                }
                else if (newtype.Equals("GateInhibit"))    //其它门转为禁止门，只有顺序与门允许，其它均不允许
                {
                    if (!oldtype.Equals("GateSequenceAnd"))
                    {
                        MessageBox.Show("类型转换不合法！禁门只能与顺序与门互相转换！");
                        ((FTAGateNodeData)this.ftanodedata).gateType = (GateType)e.OldValue;//将属性值更改回原始值
                        return;
                    }
                    else
                        this.ftanodedata.nodeName = "禁门";
                }
                else if (newtype.Equals("GateSequenceAnd"))
                {
                    if (!oldtype.Equals("GateInhibit"))
                    {
                        MessageBox.Show("类型转换不合法！顺序与门只能与禁门互相转换！");
                        ((FTAGateNodeData)this.ftanodedata).gateType = (GateType)e.OldValue;//将属性值更改回原始值
                        return;
                    }
                    else
                        this.ftanodedata.nodeName = "顺序与门";
                }
                if (oldtype.Equals("GateSequenceAnd"))
                {
                    if (!newtype.Equals("GateInhibit"))
                    {
                        MessageBox.Show("类型转换不合法！禁门只能与顺序与门互相转换！");
                        ((FTAGateNodeData)this.ftanodedata).gateType = (GateType)e.OldValue;//将属性值更改回原始值
                        return;
                    }
                    else
                        this.ftanodedata.nodeName = "禁门";
                }
                
                if (oldtype.Equals("GateInhibit"))
                {
                    if (!newtype.Equals("GateSequenceAnd"))
                    {
                        MessageBox.Show("类型转换不合法！顺序与门只能与禁门互相转换！");
                        ((FTAGateNodeData)this.ftanodedata).gateType = (GateType)e.OldValue;//将属性值更改回原始值
                        return;
                    }
                    else
                        this.ftanodedata.nodeName = "顺序与门";
                }
                if (newtype.Equals("Error"))
                {
                    MessageBox.Show("类型转换不合法！");
                    ((FTAGateNodeData)this.ftanodedata).gateType = (GateType)e.OldValue;//将属性值更改回原始值
                    return;
                }
                //((FTAGateNodeData)this.ftanodedata).gateType = (GateType)e.OldValue;//将属性值更改回原始值
                //return;
                this.changeNodeName((GateType)e.ChangedItem.Value);
            }
            if (PropertyChange != null)
                PropertyChange();
        }
        private void changeNodeName(GateType gate)
        {
            switch(gate)
            {
                case GateType.GateAnd:
                    {
                        this.ftanodedata.nodeName = "与门";
                        break;
                    }
                case GateType.GateOr:
                    {
                        this.ftanodedata.nodeName = "或门";
                        break;
                    }
                case GateType.GateInhibit:
                    {
                        this.ftanodedata.nodeName = "禁门";
                        break;
                    }
                case GateType.GatePri:
                    {
                        this.ftanodedata.nodeName = "优先门";
                        break;
                    }
                case GateType.GateSequenceAnd:
                    {
                        this.ftanodedata.nodeName = "顺序与门";
                        break;
                    }
                case GateType.GateXor:
                    {
                        this.ftanodedata.nodeName = "异或门";
                        break;
                    }
            }
        }
    }
}
