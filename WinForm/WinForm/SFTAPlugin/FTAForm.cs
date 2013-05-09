using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lassalle.Flow;
using Platform.Core.UI;
using Platform.Core.Services;
using Platform.Core;
using WeifenLuo.WinFormsUI.Docking;
using System.Drawing.Imaging;
using System.Threading;
using System.IO;
using SFTAPlugin.ToWord;

namespace SFTAPlugin
{
    public partial class FTAForm:BaseForm
    {
        /// <summary>
        /// 公共属性
        /// </summary>
        #region 属性
        public List<Node> mNodeList = new List<Node>();//AddFlow节点集合
        public FTATreeInfo treeinfocollection;// = new FTATreeInfo();//FTA树
        public int level = 0;//当前节点所在FTA的层级
        public int nameindex = 0;//节点名称后缀
        public int maxlevelCount = 1;
        public float index = 0;//纵向绘制索引
        public float parentindex = 0;//
        public float treewidth = 0;//树的宽度，用于计算滚轮的滚动长度
        public int depth = 0;
        public FTATreeNodeInfo topnode;//FTA顶事件
        FTATreeNodeInfo clipboard;//剪贴板，存储复制粘贴的节点信息
        FTATreeNodeInfo duplicateboard;//副本板，存储节点的复本：但是会重命名名称
        Dictionary<int, List<FTATreeNodeInfo>> cutsetdic = new Dictionary<int, List<FTATreeNodeInfo>>();//最小割集
        Stack<float> childrenstack = new Stack<float>();//建树堆栈，存储各节点的纵坐标       
        List<Color> statuscolor = new List<Color>();
        //int clicknum = 0;
        MinimumCutSetForm mcs;
        string treerootname;//整棵故障树的树根

        #endregion

        /// <summary>
        /// 默认构造方法，供启动时构造窗体调用
        /// </summary>
        public FTAForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 重写的构造方法
        /// </summary>
        /// <param name="treeid">整棵树的顶事件ID</param>
        /// <param name="treenodeinfo">子树的顶事件</param>
        public FTAForm(string treeid,FTATreeNodeInfo treenodeinfo)
        {
            InitializeComponent();
            
            this.treerootname = treeid;
            ((SFTAProject)ProjectManager.ProjectManagerSington.GetCurrentProject()).SFTATreesDic.TryGetValue(treeid, out  treeinfocollection);
      
            this.topnode = treenodeinfo;
            if (this.topnode.noderelation.siblingnodeID != null)//查看顶事件是否存在兄弟节点，若存在，将入门的名称设置为窗体text
                this.Text = treeinfocollection.SearchTreeNodeInfo(this.topnode.noderelation.siblingnodeID).nodedata.nodeName;
            else//若不存在，则顶事件为整棵树的树根
                this.Text = this.topnode.nodedata.nodeName;
        }

        /// <summary>
        /// 初始化addflow
        /// </summary>
        public void InitializeAddFlow()
        {
            addFlow1.Parent = this;
            addFlow1.Dock = DockStyle.Fill;
            addFlow1.AutoScroll = true;
            addFlow1.BackColor = SystemColors.Window;
            addFlow1.CanDrawNode = false;//不允许用户自动绘制
            addFlow1.CanDrawLink = false;
            addFlow1.CanMoveNode = false;

            addFlow1.Grid.Style = GridStyle.DottedLines;
            addFlow1.DefLinkProp.Jump = Jump.None;
            addFlow1.DefLinkProp.AdjustDst = true;
            addFlow1.DefLinkProp.AdjustOrg = true;
            addFlow1.DefNodeProp.Shape.Style = ShapeStyle.Rectangle;
            addFlow1.CanSizeNode = false;
            addFlow1.AllowDrop = true;

            addFlow1.Width = this.Width;
            addFlow1.Height = this.Height;

            changeColorSetting();//初始化颜色设置
            //刷新整张画布
            RefreshContent();
        }

        /// <summary>
        /// 界面层刷新
        /// </summary>
        public void RefreshContent()
        {
            //changeColorSetting();
            this.treeinfocollection.AutoCheck();
            Point currentposition = this.addFlow1.ScrollPosition;//存储当前滚动条位置

            if (this.topnode.noderelation.siblingnodeID != null)
                this.Text = treeinfocollection.SearchTreeNodeInfo(this.topnode.noderelation.siblingnodeID).nodedata.nodeName;
            else
                this.Text = this.topnode.nodedata.nodeName;
            
            ClearAddFlow();
            ClearNodeList();
            refreshGraph();

            if (treewidth * 100-this.Width/2< this.Width)//当树的宽度小于画布宽度时，将顶事件居中显示
                this.addFlow1.AutoScrollPosition =new Point((int)parentindex*100-this.Width/2, currentposition.Y);
            else//当树的宽度不小于画布的宽度时，停止滚动
                this.addFlow1.AutoScrollPosition = currentposition;//设置滚动条位置new Point((this.addFlow1.DisplayRectangle.Width - this.ClientSize.Width) / 2, (this.addFlow1.DisplayRectangle.Height - this.ClientSize.Width) / 2);


            ExistedEventsForm existedform = (ExistedEventsForm)ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(null, new UserUIEventArgs("ExistedEventsForm"));
            if (existedform == null)
            {
            }
            else
            {
                existedform.RefreshContent();
            }
        }

        /// <summary>
        /// 导航至对应节点
        /// </summary>
        /// <param name="tni"></param>
        private void ScrollToNode(FTATreeNodeInfo tni)
        {
            Point currentposition = this.addFlow1.ScrollPosition;//存储当前滚动条位置

            //if (treewidth * 100 < this.Width)//当树的宽度小于画布宽度时，将顶事件居中显示
            //    this.addFlow1.AutoScrollPosition = new Point((int)parentindex * 100 - this.Width / 2, currentposition.Y);
            //else//当树的宽度不小于画布的宽度时，停止滚动
            //{
                Node node=this.SearchAddFlowNode(tni.nodeID);
                float x = node.Location.X;
                float y = node.Location.Y;
                if (x > (int)(currentposition.X+this.Width)||x<currentposition.X)
                    currentposition.X = (int)(x-this.Width+100);
                if (y > (int)(currentposition.Y+this.Height)||y<currentposition.Y)
                    currentposition.Y = (int)(y-this.Height+100);
                this.addFlow1.AutoScrollPosition = currentposition;
            //}
        }

        /// <summary>
        /// 后续遍历整棵树，实现节点顺序绘制
        /// </summary>
        /// <param name="tni"></param>
        public void TreeTranverse(FTATreeNodeInfo tni)
        {
            foreach (string treenodeID in tni.noderelation.childrenNodesID)//获取tni的全部子节点ID
            {
                FTATreeNodeInfo childnode = treeinfocollection.SearchTreeNodeInfo(treenodeID);//根据ID获取子节点

                if (childnode.noderelation.childrenNodesID.Count != 0 && (childnode.noderelation.siblingnodeID == null || (treeinfocollection.SearchTreeNodeInfo(childnode.noderelation.siblingnodeID).nodedata is FTAGateNodeData) || (treeinfocollection.SearchTreeNodeInfo(childnode.noderelation.siblingnodeID).nodedata is FTAEventNodeData) && ((FTAEventNodeData)treeinfocollection.SearchTreeNodeInfo(childnode.noderelation.siblingnodeID).nodedata).eventType != EventType.EventOut))//子节点如果还有子节点，且此节点的兄弟节点不是出事件，则进行递归
                {
                    depth++;
                    TreeTranverse(childnode);//递归
                    depth--;
                }
                else//子节点已经为叶子节点，绘制
                {
                    depth++;//纵坐标+1
                    if ((childnode.nodedata is FTAGateNodeData) || (childnode.nodedata is FTAEventNodeData) && ((FTAEventNodeData)childnode.nodedata).eventType != EventType.EventConditioning && ((FTAEventNodeData)childnode.nodedata).eventType != EventType.EventOut)//排除为条件事件的情况
                    {
                        if (childnode.noderelation.siblingnodeID == null)//判定此子节点是否存在兄弟节点（条件事件、出事件或入事件）
                        {//若不存在兄弟节点，直接绘制
                            Node node = new Node(index * 100, depth* 100 + 10, 60, 60, childnode.nodeID);//先绘制叶子节点//(childnode.level - topnode.level) * 100 + 10, 60, 60, childnode.nodeID);//先绘制叶子节点
                            childrenstack.Push(index);//将此子节点压入栈
                            node.Text = childnode.nodedata.nodeName;
                            paint(node, childnode);
                            index++;
                        }
                        else //存在兄弟节点，需要判断兄弟节点类型
                        {
                            FTATreeNodeInfo siblingnode = treeinfocollection.SearchTreeNodeInfo(childnode.noderelation.siblingnodeID);//先查找兄弟节点
                            if ((siblingnode.nodedata is FTAEventNodeData) && ((FTAEventNodeData)siblingnode.nodedata).eventType == EventType.EventConditioning)//若兄弟节点为条件事件
                            {
                                //先绘制本身
                                Node node = new Node(index * 100, depth * 100 + 10, 60, 60, childnode.nodeID);//先绘制叶子节点//(childnode.level - topnode.level) * 100 + 10, 60, 60, childnode.nodeID);//先绘制叶子节点
                                childrenstack.Push(index);//将此子节点压入栈
                                node.Text = childnode.nodedata.nodeName;
                                paint(node, childnode);
                                index++;
                                //再绘制兄弟节点
                                Node sibnode = new Node(index * 100, depth * 100 + 10, 60, 60, siblingnode.nodeID);//绘制出兄弟节点//(childnode.level - topnode.level) * 100 + 10, 60, 60, siblingnode.nodeID);//绘制出兄弟节点
                                childrenstack.Push(index);//将兄弟节点的纵坐标压入栈
                                sibnode.Text = siblingnode.nodedata.nodeName;
                                paint(sibnode, siblingnode);
                                index++;
                                //绘制此节点与兄弟节点的连接线
                                Link link = new Link();
                                link.Line.OrthogonalDynamic = false;
                                //link.Line.Style = LineStyle.Polyline;
                                //link.Line.Style = LineStyle.HV;
                                addFlow1.AddLink(link, sibnode, node);
                            }
                            else if ((siblingnode.nodedata is FTAEventNodeData) && ((FTAEventNodeData)siblingnode.nodedata).eventType == EventType.EventOut)//若兄弟节点为出事件
                            {
                                if (childnode.nodeID != this.topnode.nodeID)//若为出事件，即当前节点为待引出的子树
                                {
                                    //先绘制本身
                                    Node node = new Node(index * 100, depth* 100 + 10, 60, 60, childnode.nodeID);//先绘制叶子节点//(childnode.level - topnode.level) * 100 + 10, 60, 60, childnode.nodeID);//先绘制叶子节点
                                    childrenstack.Push(index);//将此子节点压入栈
                                    node.Text = childnode.nodedata.nodeName;
                                    paint(node, childnode);
                                    index++;
                                    //再绘制兄弟节点
                                    Node sibnode = new Node(index * 100,depth * 100 + 10, 60, 60, siblingnode.nodeID);//绘制出兄弟节点//(childnode.level - topnode.level) * 100 + 10, 60, 60, siblingnode.nodeID);//绘制出兄弟节点
                                    childrenstack.Push(index);//将兄弟节点的纵坐标压入栈
                                    sibnode.Text = siblingnode.nodedata.nodeName;
                                    paint(sibnode, siblingnode);
                                    index++;
                                    //绘制此节点与兄弟节点的连接线
                                    Link link = new Link();
                                    link.Line.OrthogonalDynamic = false;
                                    //link.Line.Style = LineStyle.Polyline;
                                    //link.Line.Style = LineStyle.HV;
                                    addFlow1.AddLink(link, sibnode, node);
                                }
                                else if (childnode.nodeID == this.topnode.nodeID)//若本节点为子树的根节点
                                {
                                    //先绘制兄弟节点，但是应该改为绘制入事件
                                    Node sibnode = new Node((index-1) * 100, depth * 100 + 10, 60, 60, siblingnode.nodeID);//绘制出兄弟节点//(childnode.level - topnode.level) * 100 + 10, 60, 60, siblingnode.nodeID);//绘制出兄弟节点
                                    childrenstack.Push(index-1);//将兄弟节点的纵坐标压入栈
                                    sibnode.Text = siblingnode.nodedata.nodeName;
                                    paint(sibnode, siblingnode);
                                    //index++;

                                    //再绘制本身
                                    Node node = new Node(index * 100, depth * 100 + 10, 60, 60, childnode.nodeID);//先绘制叶子节点//(childnode.level - topnode.level) * 100 + 10, 60, 60, childnode.nodeID);//先绘制叶子节点
                                    childrenstack.Push(index);//将此子节点压入栈
                                    node.Text = childnode.nodedata.nodeName;
                                    paint(node, childnode);
                                    index++;

                                    //绘制此节点与兄弟节点的连接线
                                    Link link = new Link();
                                    link.Line.OrthogonalDynamic = false;
                                    //link.Line.Style = LineStyle.Polyline;
                                    //link.Line.Style = LineStyle.HV;
                                    addFlow1.AddLink(link, node, sibnode);
                                }
                            }
                        }
                    }
                    depth--;//叶子节点绘制完成，退回上一级
                }
            }
            //tni全部子节点以及子节点的兄弟节点绘制完成
            //准备绘制tni本身

            parentindex=0 ;
            if (this.treeinfocollection.ftatreenodeinfolist.Count == 1)
                parentindex = this.Width / 2 / 100;//横坐标右移至画布中心

            if (tni.nodeID == this.topnode.nodeID && tni.noderelation.siblingnodeID != null&&tni.noderelation.childrenNodesID.Count==0)//当本节点为子树的树根时，则为入事件预留空位
                parentindex= this.Width / 2 / 100;//++;//横坐标右移至画布中心
            if (tni.noderelation.childrenNodesID.Count != 0)//若此节点存在子节点
            {
                FTATreeNodeInfo lastchildnode;
                float lastsiblingindex=0;
                int siblingnum = 0;
                foreach (string childnodeid in tni.noderelation.childrenNodesID)
                {
                    FTATreeNodeInfo treenode = treeinfocollection.SearchTreeNodeInfo(childnodeid);
                    if (treenode.noderelation.siblingnodeID != null && ((treenode.nodedata is FTAGateNodeData) || (treenode.nodedata is FTAEventNodeData) && ((FTAEventNodeData)treenode.nodedata).eventType != EventType.EventConditioning))//若兄弟节点不为空，其本身不是条件事件
                        siblingnum++;//计算全部兄弟节点
                }
                if (childrenstack.Count != 0)
                    lastsiblingindex = childrenstack.Peek();
                for (int i = 0; i < tni.noderelation.childrenNodesID.Count + siblingnum; i++)
                {
                    if (childrenstack.Count != 0)
                    {
                        parentindex += childrenstack.Pop();//子节点坐标出栈，并加之父节点纵坐标中
                    }
                }
                lastchildnode = treeinfocollection.SearchTreeNodeInfo(tni.noderelation.childrenNodesID.ElementAt<string>(tni.noderelation.childrenNodesID.Count - 1));
                if (lastchildnode.noderelation.siblingnodeID == null)//若节点右儿子无兄弟节点，则父节点只需平均分布
                {
                    parentindex = (parentindex) / (tni.noderelation.childrenNodesID.Count + siblingnum);//计算当前节点坐标
                    childrenstack.Push(parentindex);//当前节点坐标入栈
                }
                else//若右儿子有兄弟节点，则需要减去该节点的index
                {
                    parentindex = (parentindex - lastsiblingindex) / (tni.noderelation.childrenNodesID.Count + siblingnum-1);
                    childrenstack.Push(parentindex);//当前节点坐标入栈
                }
            }

            Node rootnode;//=new Node(parentindex * 100, (tni.level - topnode.level) * 100 + 10, 60, 60, tni.nodeID);//默认值，后面肯定会更改！
            if (tni.noderelation.siblingnodeID == null)//兄弟节点为空，则直接绘制本身
            {
                rootnode = new Node(parentindex * 100, depth * 100 + 10, 60, 60, tni.nodeID);//正常 new Node((index * 2 - tni.childrenNodesID.Count-1) / 2 * 100, tni.level * 100 + 10, 60, 60, tni.nodeID);//Node((index * 2 - tni.childrenNodesID.Count ) / 2 * 100, tni.level * 100 + 10, 60, 60, tni.nodeID);//(tni.level - topnode.level) * 100 + 10, 60, 60, tni.nodeID);//正常 new Node((index * 2 - tni.childrenNodesID.Count-1) / 2 * 100, tni.level * 100 + 10, 60, 60, tni.nodeID);//Node((index * 2 - tni.childrenNodesID.Count ) / 2 * 100, tni.level * 100 + 10, 60, 60, tni.nodeID);(tni.level - topnode.level) * 100 + 10, 60, 60, tni.nodeID);//正常 new Node((index * 2 - tni.childrenNodesID.Count-1) / 2 * 100, tni.level * 100 + 10, 60, 60, tni.nodeID);//Node((index * 2 - tni.childrenNodesID.Count ) / 2 * 100, tni.level * 100 + 10, 60, 60, tni.nodeID);
                rootnode.Text = tni.nodedata.nodeName;
                paint(rootnode, tni);//绘制本节点
            }
            else//兄弟节点不为空，则需要判断兄弟节点为条件事件还是出入三角形
            {
                FTATreeNodeInfo siblingnode = treeinfocollection.SearchTreeNodeInfo(tni.noderelation.siblingnodeID);

                if ((siblingnode.nodedata is FTAEventNodeData)&&((FTAEventNodeData)siblingnode.nodedata).eventType==EventType.EventConditioning)//若兄弟节点为条件事件
                {
                    //先绘制本身
                    rootnode = new Node(parentindex * 100, depth * 100 + 10, 60, 60, tni.nodeID);//(tni.level - topnode.level) * 100 + 10, 60, 60, tni.nodeID);//正常 new Node((index * 2 - tni.childrenNodesID.Count-1) / 2 * 100, tni.level * 100 + 10, 60, 60, tni.nodeID);//Node((index * 2 - tni.childrenNodesID.Count ) / 2 * 100, tni.level * 100 + 10, 60, 60, tni.nodeID);
                    rootnode.Text = tni.nodedata.nodeName;
                    paint(rootnode, tni);//绘制本节点
                    //再绘制兄弟节点
                    parentindex++;
                    Node sibnode = new Node(parentindex * 100, depth  * 100 + 10, 60, 60, siblingnode.nodeID);//(tni.level - topnode.level) * 100 + 10, 60, 60, siblingnode.nodeID);//先绘制叶子节点
                    childrenstack.Push(parentindex);//将此子节点压入栈
                    sibnode.Text = siblingnode.nodedata.nodeName;
                    paint(sibnode, siblingnode);
                }
                else// if (siblingnode.nodedata.nodeType == NodeType.EventOut)//若兄弟节点为出三角形
                {
                    if (tni.nodeID != this.topnode.nodeID)//若本节点不是本画布的顶事件
                    {
                        //先绘制本身
                        rootnode = new Node(parentindex * 100, depth * 100 + 10, 60, 60, tni.nodeID);//(tni.level - topnode.level) * 100 + 10, 60, 60, tni.nodeID);//正常 new Node((index * 2 - tni.childrenNodesID.Count-1) / 2 * 100, tni.level * 100 + 10, 60, 60, tni.nodeID);//Node((index * 2 - tni.childrenNodesID.Count ) / 2 * 100, tni.level * 100 + 10, 60, 60, tni.nodeID);
                        rootnode.Text = tni.nodedata.nodeName;
                        paint(rootnode, tni);//绘制本节点
                        //再绘制兄弟节点
                        parentindex++;
                        Node sibnode = new Node(parentindex * 100, depth * 100 + 10, 60, 60, siblingnode.nodeID);//(tni.level - topnode.level) * 100 + 10, 60, 60, siblingnode.nodeID);//先绘制叶子节点
                        childrenstack.Push(parentindex);//将此子节点压入栈
                        sibnode.Text = siblingnode.nodedata.nodeName;
                        paint(sibnode, siblingnode);
                    }
                    else// if (tni.nodeID == this.topnode.nodeID)//若本事件为本画布的顶事件
                    {
                        //先绘制兄弟节点                        
                        /////Node sibnode = new Node(parentindex * 100, (tni.level - topnode.level) * 100 + 10, 60, 60, siblingnode.nodeID);//先绘制叶子节点
                        Node sibnode = new Node((parentindex-1)*100, depth * 100 + 10, 60, 60, siblingnode.nodeID);//(tni.level - topnode.level) * 100 + 10, 60, 60, siblingnode.nodeID);//固定！
                        childrenstack.Push(parentindex-1);//将此子节点压入栈
                        sibnode.Text = siblingnode.nodedata.nodeName;
                        paint(sibnode, siblingnode);
                        //再绘制本身
                        /////parentindex++;//还是为了固定！！！
                        rootnode = new Node(parentindex * 100, depth * 100 + 10, 60, 60, tni.nodeID);//(tni.level - topnode.level) * 100 + 10, 60, 60, tni.nodeID);//正常 new Node((index * 2 - tni.childrenNodesID.Count-1) / 2 * 100, tni.level * 100 + 10, 60, 60, tni.nodeID);//Node((index * 2 - tni.childrenNodesID.Count ) / 2 * 100, tni.level * 100 + 10, 60, 60, tni.nodeID);
                        rootnode.Text = tni.nodedata.nodeName;
                        paint(rootnode, tni);//绘制本节点                      
                    }
                }
            }
            parentindex++;
            if(parentindex>index)//由于兄弟节点的存在，需要检测是否存在直接的父一级节点的个数比子一级节点多的情况
                index = parentindex;//若父节点存在兄弟节点，则父节点的父节点的另一个子节点排版需要从父节点及本身的较大的一个纵坐标向右开始进行
            if (index > treewidth)
                treewidth = index;
            //绘制连接线
            foreach (string treenodeID in tni.noderelation.childrenNodesID)
            {
                Node childnode = SearchAddFlowNode(treenodeID);
                Link link = new Link();
                link.Line.OrthogonalDynamic = true;
                link.Line.Style = LineStyle.VHV;//线性
                addFlow1.AddLink(link, rootnode, childnode);
            }
            if (tni.noderelation.siblingnodeID != null)
            {
                Node siblingnode = SearchAddFlowNode(tni.noderelation.siblingnodeID);
                Link link = new Link();
                link.Line.OrthogonalDynamic = false;
                //link.Line.Style = LineStyle.VHV;//线性
                addFlow1.AddLink(link, siblingnode, rootnode);
            }
        }

        /// <summary>
        /// 绘制节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="tni"></param>
        public void paint(Node node,FTATreeNodeInfo tni)
        {
            if (tni.nodedata.isFinished == true)
                node.FillColor = statuscolor[2];
            else
                node.FillColor = statuscolor[1];

            if (tni.nodedata is FTAEventNodeData)
            {
                if ((((FTAEventNodeData)tni.nodedata).isDuplicated) == true)
                    node.DrawColor = statuscolor[3];//共享事件的轮廓为红色
                if ((((FTAEventNodeData)tni.nodedata).belongtoMiniCut) == true)
                    node.FillColor= statuscolor[4];//共享事件的轮廓为红色

                EventType nt = ((FTAEventNodeData)tni.nodedata).eventType;
                switch (nt)
                {
                    case EventType.EventIntermediate://矩形，中间事件和顶事件
                        {
                            node.Shape.Style = ShapeStyle.Rectangle;//节点形状
                            break;
                        }
                    case EventType.EventInitiating://初始事件
                        {
                            node.Shape.Style = ShapeStyle.Custom;
                            node.Shape.GraphicsPath = new FTAShapes(nt).Path;
                            node.Shape.Orientation = ShapeOrientation.so_180;
                            break;
                        }
                    case EventType.EventBasic://基本事件
                        {
                            node.Shape.Style = ShapeStyle.Ellipse;
                            break;
                        }
                    case EventType.EventUndeveloped://未展开事件
                        {
                            node.Shape.Style = ShapeStyle.Losange;
                            node.Size = new Size(60, 50);
                            break;
                        }
                    case EventType.EventConditioning://未展开事件
                        {
                            node.Shape.Style = ShapeStyle.Custom;
                            node.Shape.GraphicsPath = new FTAShapes(EventType.EventConditioning).Path;
                            node.Size = new Size(60, 45);
                            break;
                        }
                    default://其它元素
                        {
                            node.Shape.Style = ShapeStyle.Custom;
                            node.Shape.GraphicsPath = new FTAShapes(nt).Path;
                            break;
                        }
                }
            }
            else if (tni.nodedata is FTAGateNodeData)
            {
                GateType nt = ((FTAGateNodeData)tni.nodedata).gateType;
                switch (nt)
                {
                    case GateType.GateOr://或门
                        {
                            node.Shape.Style = ShapeStyle.OrGate;
                            node.Shape.Orientation = ShapeOrientation.so_90;
                            break;
                        }
                    case GateType.GateInhibit://禁门
                        {
                            node.Shape.Style = ShapeStyle.Hexagon;
                            node.Shape.Orientation = ShapeOrientation.so_180;
                            break;
                        }
                    case GateType.GateElect://表决门
                        {
                            node.Shape.Style = ShapeStyle.Custom;
                            node.Shape.GraphicsPath = new FTAShapes(GateType.GateAnd).Path;
                            ///需要加入r/n的对应数值，n为全部子节点数量，r需要用户定义
                            break;
                        }
                    case GateType.GateSequenceAnd://顺序与门
                        {
                            node.Shape.Style = ShapeStyle.Custom;
                            node.Shape.GraphicsPath = new FTAShapes(GateType.GateAnd).Path;
                            ///需要加入r/n的对应数值，n为全部子节点数量，r需要用户定义
                            break;
                        }
                    default://其它元素
                        {
                            node.Shape.Style = ShapeStyle.Custom;
                            node.Shape.GraphicsPath = new FTAShapes(nt).Path;
                            break;
                        }
                }
            }

            if (tni.nodedata is FTAEventNodeData)
                node.Tag = ((FTAEventNodeData)tni.nodedata).eventType;
            else if (tni.nodedata is FTAGateNodeData)
                node.Tag = ((FTAGateNodeData)tni.nodedata).gateType;
            node.Url = tni.nodeID;
            //添加至addflow
            addFlow1.Nodes.Add(node);
            mNodeList.Add(node);
        }
        /// <summary>
        /// 刷新AddFlow
        /// </summary>
        public void refreshGraph()
        {
            index = this.Width/2/100;
            childrenstack.Clear();

            TreeTranverse(topnode);
        }

        public AddFlow GetAddFlow()
        {
            return addFlow1;
        }

        public void ClearNodeList()
        {
            this.mNodeList.Clear();
        }

        public void ClearAddFlow()
        {
            this.addFlow1.Nodes.Clear();
        }

        /// <summary>
        /// 根据节点ID搜索AddFlow节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Node SearchAddFlowNode(string id)
        {
            Node nd = null;
            foreach (Node node in mNodeList)
                if (node.Url.Equals(id))
                    nd = node;
            return nd;
        }

        /// <summary>
        /// 添加中间事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddEventIntermediate(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;//父节点ID
            FTATreeNodeInfo parentNode = treeinfocollection.SearchTreeNodeInfo(parentID);//获取父节点
            if (parentNode.noderelation.siblingnodeID == null || ((parentNode.nodedata is FTAGateNodeData) && ((FTAGateNodeData)parentNode.nodedata).gateType != GateType.GateInhibit) || ((parentNode.nodedata is FTAGateNodeData) && ((FTAGateNodeData)parentNode.nodedata).gateType == GateType.GateInhibit) && parentNode.noderelation.childrenNodesID.Count < 1 || ((FTAEventNodeData)treeinfocollection.SearchTreeNodeInfo(parentNode.noderelation.siblingnodeID).nodedata).eventType == EventType.EventOut)
            {
                treeinfocollection.CreateEvent(parentID, level, EventType.EventIntermediate);
            }
            else if (((parentNode.nodedata is FTAGateNodeData) && ((FTAGateNodeData)parentNode.nodedata).gateType == GateType.GateInhibit) && parentNode.noderelation.childrenNodesID.Count > 0)
                MessageBox.Show("禁门下只能连接一个事件！");
            else
                MessageBox.Show("请双击右侧出三角形，导航至子树进行下一步分析");

            //刷新整张画布
            RefreshContent();
        }

        public void AddEventInitiating(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;
            treeinfocollection.CreateEvent(parentID, level, EventType.EventInitiating);

            //刷新整张画布
            RefreshContent();
        }
        public void AddEventBasic(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;//获取当前选择的节点
            treeinfocollection.CreateEvent(parentID, level, EventType.EventBasic);//创建基本事件，传递
            //刷新整张画布
            RefreshContent();
        }

        /// <summary>
        /// 添加省略事件（即未展开事件）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddEventUnDeveloped(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;
            treeinfocollection.CreateEvent(parentID, level, EventType.EventUndeveloped);

            //刷新整张画布
            RefreshContent();
        }
        /// <summary>
        /// 添加条件事件///////尚未提取至FTATreeInfo类！！！！！！
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddEventConditioning(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;//源节点ID
            FTATreeNodeInfo parentNode = treeinfocollection.SearchTreeNodeInfo(parentID);//获取源节点
            if (parentNode.noderelation.siblingnodeID == null)
            {
                int nodelevel = parentNode.level;
                if (level < nodelevel)
                    level = nodelevel;
                string siblingnodeID = Guid.NewGuid().ToString();//本身ID
                parentNode.noderelation.siblingnodeID = siblingnodeID;//将自己设置为源节点的兄弟节点
                FTATreeNodeInfo tni = new FTATreeNodeInfo(siblingnodeID, nodelevel, new FTAEventNodeData(siblingnodeID, "条件事件", EventType.EventConditioning, "节点描述信息", string.Empty), new FTANodeRelation(siblingnodeID, siblingnodeID, new List<string>()));
                //将本人的父节点ID指向自己
                tni.noderelation.siblingnodeID = parentNode.noderelation.nodeRelationID;//将源节点设为自己的兄弟的节点
                treeinfocollection.Add(tni);

                //刷新整张画布
                RefreshContent();
            }
            else
                MessageBox.Show("条件事件只能添加一个！");
        }                                                          

        public void AddEventIn(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;
            //FTATreeNodeInfo parentNode = treeinfocollection.SearchTreeNodeInfo(parentID);//获取父节点
            //int nodelevel = parentNode.level + 1;
            //if (level < nodelevel)
            //    level = nodelevel;
            //string childnodeID = Guid.NewGuid().ToString();
            //FTATreeNodeInfo tni = new FTATreeNodeInfo(childnodeID, new FTAEventNodeData(childnodeID,"新节点", NodeType.EventIn, "节点描述信息",string.Empty), parentID, new List<string>(), nodelevel);
            //treeinfocollection.Add(tni);
            //parentNode.childrenNodesID.Add(childnodeID);
            treeinfocollection.CreateEvent(parentID, level, EventType.EventIn);

            //刷新整张画布
            RefreshContent();
        }
        /// <summary>
        /// 创建出事件，添加子树
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddEventOut(object sender, EventArgs e)
        {
            
        }
        public void AddGateOr(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;
            treeinfocollection.CreateGate(parentID, level, GateType.GateOr, "或门");

            //刷新整张画布
            RefreshContent();
        }
        public void AddGateAnd(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;
            treeinfocollection.CreateGate(parentID, level, GateType.GateAnd, "与门");

            //刷新整张画布
            RefreshContent();
        }
        /// <summary>
        /// 添加禁门，同时添加条件事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddGateInhibit(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;
            //treeinfocollection.CreateGate(parentID, level, GateType.GateInhibit, "禁门");
            FTATreeNodeInfo parentNode = treeinfocollection.SearchTreeNodeInfo(parentID);//获取父节点
            int nodelevel = parentNode.level + 1;
            if (level < nodelevel)
                level = nodelevel;
            string childnodeID = Guid.NewGuid().ToString();
            FTATreeNodeInfo tni = new FTATreeNodeInfo(childnodeID, nodelevel, new FTAGateNodeData(childnodeID, "禁门", GateType.GateInhibit, "节点描述信息"), new FTANodeRelation(childnodeID, parentID, new List<string>()));
            treeinfocollection.Add(tni);
            parentNode.noderelation.childrenNodesID.Add(tni.noderelation.nodeRelationID);

            string siblingnodeID = Guid.NewGuid().ToString();//本身ID
            tni.noderelation.siblingnodeID = siblingnodeID;//将自己设置为当前门节点的兄弟节点
            FTATreeNodeInfo tniplus = new FTATreeNodeInfo(siblingnodeID, nodelevel, new FTAEventNodeData(siblingnodeID, "条件事件", EventType.EventConditioning, "节点描述信息", string.Empty), new FTANodeRelation(siblingnodeID, siblingnodeID, new List<string>()));
            //将本人的父节点ID指向自己
            tniplus.noderelation.siblingnodeID = tni.noderelation.nodeRelationID;//将源节点设为自己的兄弟的节点
            treeinfocollection.Add(tniplus);
            //刷新整张画布
            RefreshContent();
        }
        public void AddGatePri(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;
            treeinfocollection.CreateGate(parentID, level, GateType.GatePri, "优先门");

            //刷新整张画布
            RefreshContent();
        }
        /// <summary>
        /// 异或门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddGateXor(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;
            treeinfocollection.CreateGate(parentID, level, GateType.GateXor, "异或门");

            //刷新整张画布
            RefreshContent();
        }
 
        /// <summary>
        /// 右键单击各节点的响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RightButtonClicked(object sender, MouseEventArgs e)
        {
            //Point p = PointToScreen(new Point(e.X,e.Y));
            //Lassalle.Flow.Item item = this.addFlow1.GetItemAt(new Point(e.X,e.Y));
            //Lassalle.Flow.Item item = this.addFlow1.GetItemAt(p);
            Lassalle.Flow.Item item = addFlow1.PointedItem;
            addFlow1.SelectedItem = item;
            if (item is Node)
            {
                AddEventToolStripMenuItem.Visible = false;//隐藏事件菜单
                AddGateToolStripMenuItem.Visible = false;//隐藏门菜单
                CreateChildTreeToolStripMenuItem.Visible = false;//隐藏创建子树菜单
                if (item.Tag is EventType)
                {
                    switch ((EventType)item.Tag)//检查所选item的节点类型
                    {
                        case EventType.EventIntermediate://顶事件或中间事件不能向下添加条件事件、出事件、异或门
                            {
                                AddGateToolStripMenuItem.Visible = true;
                                AddEventToolStripMenuItem.Visible = false;
                                CreateChildTreeToolStripMenuItem.Visible = true;
                                //EventConditioningToolStripMenuItem.Visible = false;
                                //EventOutToolStripMenuItem.Visible = false;
                                //GateXorToolStripMenuItem.Visible = false;
                                break;
                            }
                        case EventType.EventOut:
                            {
                                AddGateToolStripMenuItem.Visible = false;
                                AddEventToolStripMenuItem.Visible = false;
                                break;
                            }
                        default:
                            {//基本事件圆形、正常事件屋形、条件事件椭圆形、未展开事件菱形、入事件三角竖线符号
                                break;
                            }
                    }
                }
                else if (item.Tag is GateType)//若节点为门类型
                {
                    switch ((GateType)item.Tag)//检查所选item的节点类型
                    {
                        case GateType.GateAnd:
                            {
                                AddEventToolStripMenuItem.Visible = true;
                                break;
                            }
                        case GateType.GateOr:
                            {
                                AddEventToolStripMenuItem.Visible = true;
                                break;
                            }
                        case GateType.GateXor:
                            {
                                if (item.Children.Count < 3)//异或门下有且只有两个事件，此处只满足了不能多于2个事件///////////
                                    AddEventToolStripMenuItem.Visible = true;
                                break;
                            }
                        case GateType.GatePri://除了禁门的其它逻辑门下可添加无限多事件
                            {
                                AddEventToolStripMenuItem.Visible = true;
                                break;
                            }
                        case GateType.GateInhibit://禁门下下层只能有一个事件
                            {
                                AddEventToolStripMenuItem.Visible = true;
                                break;
                            }
                        case GateType.GateElect:
                            {
                                AddEventToolStripMenuItem.Visible = true;
                                break;
                            }
                        case GateType.GateSequenceAnd:
                            {
                                AddEventToolStripMenuItem.Visible = true;
                                break;
                            }
                        default:
                            {//基本事件圆形、正常事件屋形、条件事件椭圆形、未展开事件菱形、入事件三角竖线符号
                                break;
                            }
                    }
                }
                Point p = PointToScreen(new Point(e.X, e.Y));
                FTAcontextMenuStrip.Show(p);
            }
        }
        /// <summary>
        /// Addflow的鼠标事件响应，包括右键菜单生成、属性窗体开启等
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addFlow1_MouseDown(object sender, MouseEventArgs e)           
        {
            if (e.Button == MouseButtons.Right)
            {//单击鼠标右键
                RightButtonClicked(this, e);
            }
            else if (e.Button == MouseButtons.Left)//开启属性窗体，控制菜单显示
            {//单击鼠标左键
                Lassalle.Flow.Item item = this.addFlow1.SelectedItem;
                if (item is Node)//若单击的是节点，则准备开启属性窗体
                {
                    string currentid = this.addFlow1.SelectedItem.Url;
                    SFTAProject currentproject = (SFTAProject)ProjectManager.ProjectManagerSington.GetCurrentProject();
                    FTATreeNodeInfo currentNode = treeinfocollection.SearchTreeNodeInfo(currentid);//获取当前节点

                    if (currentNode.nodedata is FTAEventNodeData)
                    {//选中节点为事件时，只允许添加门节点作为子节点
                        this.gateToolStripMenuItem.Visible = true;
                        this.eventToolStripMenuItem.Visible = false;
                    }
                    else if (currentNode.nodedata is FTAGateNodeData)
                    {//选中节点为门时，只允许添加事件节点作为子节点
                        this.eventToolStripMenuItem.Visible = true;
                        this.gateToolStripMenuItem.Visible = false;
                    }

                    string propformid = string.Empty;
                    PropertyForm propform = (PropertyForm)ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(null, new UserUIEventArgs("PropertyForm"));//查看属性窗体是否已加载到平台中
                    if (propform == null)
                    {//若尚未加载
                        propform = new PropertyForm(currentNode.nodeID, currentNode.nodedata,currentNode.noderelation);//新建属性窗体，并将当前单击的节点信息数据传输至属性窗体
                        ServicesManager.ServicesManagerSingleton.UIService.AddMutableResourceSelf(propform, new UserUIEventArgs("PropertyForm", new FormLoc("TreeViewForm", DockAlignment.Bottom, 0.5)));
                    }
                    else
                    {//若已经加载
                        propform.RefreshForm(currentNode.nodeID, currentNode.nodedata);//根据当前单击的节点信息刷新窗体
                        propform.Show();
                    }
                    propform.PropertyChange = new PropertyChangeHandler(RefreshContent);//属性窗体发生属性变化，刷新本窗体
                   // propform.PropertyChange += new PropertyChangeHandler(SetChildTreeText);//属性变化时，刷新本窗体子树名称
                    propform.PropertyChange += new PropertyChangeHandler(((SFTATreeViewForm)ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(null, new UserUIEventArgs("TreeViewForm"))).LoadFromProject);//属性变化，刷新左侧树状列表名称
                }
                else
                {
                    this.gateToolStripMenuItem.Visible =false;
                    this.eventToolStripMenuItem.Visible = false;
                }
            }
        }

        private void addFlow1_Scroll(object sender, ScrollEventArgs e)
        {
            //addFlow1.Location = this.poin
        }

        /// <summary>
        /// 更改节点名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChangeNodeName(object sender, EventArgs e)
        {
            addFlow1.CanLabelEdit = true;
            Lassalle.Flow.Item item = this.addFlow1.SelectedItem;
            if (item is Node)
            {
                Node node = (Node)item;
                if (!node.IsEditing)
                {
                    node.BeginEdit();
                }
            }
        }

        /// <summary>
        /// 修改节点名称，更改对应的FTA节点名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addFlow1_AfterEdit(object sender, AfterEditEventArgs e)
        {
            FTATreeNodeInfo tni = treeinfocollection.SearchTreeNodeInfo(e.Node.Url);
            tni.nodedata.nodeName= e.Text;
            e.Node.EndEdit(false);
        }

        /// <summary>
        /// 该子窗体成为系统的活动窗体时发生的处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FTAForm_Enter(object sender, EventArgs e)
        {
            //MessageBox.Show("我是FTA窗体！"+this.Name);
            SFTAProject currentproject = (SFTAProject)ProjectManager.ProjectManagerSington.GetCurrentProject();
            currentproject.activeformid = this.Name;
        }

        
        /// <summary>
        /// 添加顺序与门/////////方法尚未提取至FTATreeInfo类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddGateSequenceAnd_Click(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;
            FTATreeNodeInfo parentNode = treeinfocollection.SearchTreeNodeInfo(parentID);//获取父节点
            int nodelevel = parentNode.level + 1;
            if (level < nodelevel)
                level = nodelevel;
            string childnodeID = Guid.NewGuid().ToString();
            FTATreeNodeInfo tni = new FTATreeNodeInfo(childnodeID, nodelevel, new FTAGateNodeData(childnodeID, "顺序与门", GateType.GateSequenceAnd, "节点描述信息"), new FTANodeRelation(childnodeID, parentID, new List<string>()));
            treeinfocollection.Add(tni);
            parentNode.noderelation.childrenNodesID.Add(tni.noderelation.nodeRelationID);

            string siblingnodeID = Guid.NewGuid().ToString();//本身ID
            tni.noderelation.siblingnodeID = siblingnodeID;//将自己设置为当前门节点的兄弟节点
            FTATreeNodeInfo tniplus = new FTATreeNodeInfo(siblingnodeID,nodelevel, new FTAEventNodeData(siblingnodeID, "条件事件", EventType.EventConditioning, "节点描述信息", string.Empty), new FTANodeRelation(siblingnodeID, siblingnodeID, new List<string>()));
            //将本人的父节点ID指向自己
            tniplus.noderelation.siblingnodeID = tni.noderelation.nodeRelationID;//将源节点设为自己的兄弟的节点
            treeinfocollection.Add(tniplus);

            //刷新整张画布
            RefreshContent();
            //refreshChart();
        }
        /// <summary>
        /// 添加表决门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddGateElect_Click(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;

            ElectForm rnform = new ElectForm();
            if (rnform.ShowDialog() == DialogResult.Yes)
            {
                treeinfocollection.CreateGate(parentID, level, GateType.GateElect, rnform.r+"/"+rnform.n);

                //刷新整张画布
                RefreshContent();
            }   
        }

        #region Mark

        /// <summary>
        /// 标记节点为“重要”
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MarkImportantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nodeID = this.addFlow1.SelectedItem.Url;
            FTATreeNodeInfo ftaNode = treeinfocollection.SearchTreeNodeInfo(nodeID);//获取父节点
            ftaNode.nodedata.isFinished = false;
            if (this.addFlow1.SelectedItem is Node)
            {
                Node selectednode = (Node)this.addFlow1.SelectedItem;
                selectednode.FillColor = this.statuscolor[0];
            }
        }
        /// <summary>
        /// 标记为“未完成”
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MarkUnfinishedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nodeID = this.addFlow1.SelectedItem.Url;
            FTATreeNodeInfo ftaNode = treeinfocollection.SearchTreeNodeInfo(nodeID);//获取父节点
            ftaNode.nodedata.isFinished = false;
            if (this.addFlow1.SelectedItem is Node)
            {
                Node selectednode = (Node)this.addFlow1.SelectedItem;
                selectednode.FillColor = this.statuscolor[1];
            }
        }
        /// <summary>
        /// 标记为“已完成”
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MarkFinishedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nodeID = this.addFlow1.SelectedItem.Url;
            FTATreeNodeInfo ftaNode = treeinfocollection.SearchTreeNodeInfo(nodeID);//获取父节点
            ftaNode.nodedata.isFinished = true;
            if (this.addFlow1.SelectedItem is Node)
            {
                Node selectednode = (Node)this.addFlow1.SelectedItem;
                selectednode.FillColor = this.statuscolor[2];
            }
        }

        #endregion


        /// <summary>
        /// 双击树节点，目前只对出三角形节点有响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addFlow1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Lassalle.Flow.Item item = this.addFlow1.SelectedItem;
            if (item is Node)
            {
                string currentid = this.addFlow1.SelectedItem.Url;
                FTATreeNodeInfo currentNode = treeinfocollection.SearchTreeNodeInfo(currentid);//获取当前节点
                if ((currentNode.nodedata is FTAEventNodeData) && ((FTAEventNodeData)currentNode.nodedata).eventType == EventType.EventOut)
                {
                    string sibnodeid = currentNode.noderelation.siblingnodeID;
                    FTATreeNodeInfo sibnode = treeinfocollection.SearchTreeNodeInfo(sibnodeid);
                    FTAForm ftaform;
                    string name = string.Empty;
                    //treeView1.SelectedNode.Name = treeView1.SelectedNode.Index.ToString();//传递node序号，有风险
                    foreach (string formname in ServicesManager.ServicesManagerSingleton.UIService.GetAllFormNames(null, new UserUIEventArgs(string.Empty, null)))
                        if (formname.Equals(sibnode.nodeID))
                        {                           
                            name = formname;
                            ftaform = (FTAForm)ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(null, new UserUIEventArgs(formname));
                            ftaform.Name = formname;
                            ftaform.Show(DockPanel);
                            break;
                        }
                    if (name == string.Empty)
                    {
                        ftaform = new FTAForm(this.treerootname,sibnode);
                        ftaform.Name = sibnode.nodeID;//节点ID与对应的窗体ID相同
                        ServicesManager.ServicesManagerSingleton.UIService.AddMutableResourceSelf(ftaform, new UserUIEventArgs(ftaform.Name, new FormLoc(DockState.Document)));
                    }
                }
            }
        }

        public void FTAForm_Load(object sender, EventArgs e)//窗体加载时，初始化AddFlow控件
        {
            if (treeinfocollection == null)
            {
                string rootid=string.Empty;
                foreach (FTATreeInfo treeinfo in ((SFTAProject)ProjectManager.ProjectManagerSington.GetCurrentProject()).SFTATreesDic.Values)//查找全部子树
                {
                    if (treeinfo.SearchRootIDIfContain(this.Name) != string.Empty)//查找包含当前窗体ID（即节点ID）的树根节点ID
                        rootid = treeinfo.SearchRootIDIfContain(this.Name);
                }
                if (rootid != string.Empty)//若查找到了树根节点
                {
                    ((SFTAProject)ProjectManager.ProjectManagerSington.GetCurrentProject()).SFTATreesDic.TryGetValue(rootid, out  treeinfocollection);//设置本窗体的故障树信息
                    this.topnode = treeinfocollection.SearchTreeNodeInfo(this.Name);
                }
                InitializeAddFlow();
            }
            else
                InitializeAddFlow();
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lassalle.Flow.Item item = this.addFlow1.SelectedItem;
            if (item is Node)
            {
                string currentid = this.addFlow1.SelectedItem.Url;
                treeinfocollection.DeleteTreeNode(currentid);//删除节点
                RefreshContent();
            }
            else//存在子节点
                MessageBox.Show("请首先删除全部子节点，再删除本节点！");
        }

        
        /// <summary>
        /// 创建出三角形，添加子树
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateChildTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;
            FTATreeNodeInfo parentNode = treeinfocollection.SearchTreeNodeInfo(parentID);//获取父节点
            if (parentNode.noderelation.parentNodeID == null)
            {
                MessageBox.Show("只有中间事件可以创建子树！！！");
                return;
            }
            if (parentNode.noderelation.siblingnodeID == null || ((FTAEventNodeData)treeinfocollection.SearchTreeNodeInfo(parentNode.noderelation.siblingnodeID).nodedata).eventType != EventType.EventOut)
            {
                //将出三角形当作兄弟节点
                int nodelevel = parentNode.level;
                if (level < nodelevel)
                    level = nodelevel;
                string siblingnodeID = Guid.NewGuid().ToString();//本身ID
                parentNode.noderelation.siblingnodeID = siblingnodeID;//将自己设置为源节点的兄弟节点
                this.topnode.childrennum++;
                string childtreetext = this.Text + "." + (this.topnode.childrennum).ToString();
                FTATreeNodeInfo tni = new FTATreeNodeInfo(siblingnodeID, nodelevel, new FTAEventNodeData(siblingnodeID, childtreetext, EventType.EventOut, "节点描述信息", string.Empty), new FTANodeRelation(siblingnodeID, siblingnodeID, new List<string>()));
                //将本人的父节点ID指向自己
                tni.noderelation.siblingnodeID = parentNode.noderelation.nodeRelationID;//将源节点设为自己的兄弟的节点
                treeinfocollection.Add(tni);
                //此部分为将出门当作子节点的代码，已废弃
                //int nodelevel = parentNode.level + 1;
                //if (level < nodelevel)
                //    level = nodelevel;
                //string childnodeID = Guid.NewGuid().ToString();
                //FTATreeNodeInfo tni = new FTATreeNodeInfo(childnodeID, new FTANodeData(childnodeID, "新节点", NodeType.EventOut, "节点描述信息", string.Empty), parentID, new List<string>(), nodelevel);
                //treeinfocollection.Add(tni);
                //parentNode.childrenNodesID.Add(childnodeID);
                //添加子树
                CreateChildTree(parentNode);//tni);
                //刷新整张画布
                RefreshContent();
            }
            else
                MessageBox.Show("子树已创建！双击右侧出三角形导航至子树面板");
        }

        /// <summary>
        /// 创建子树
        /// </summary>
        /// <param name="tni">子树的根</param>
        private void CreateChildTree(FTATreeNodeInfo tni)
        {
            //创建子树，左侧添加新窗体
            ServicesManager.ServicesManagerSingleton.UIService.AddMutableResourceSelf(new FTAForm(this.treerootname, tni), new UserUIEventArgs(tni.nodeID));
            SFTATreeViewForm treeform = (SFTATreeViewForm)ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(null, new UserUIEventArgs("TreeViewForm"));

            treeform.searchTreeNode(treeform.treeView1.TopNode, tni, this.Name, treeinfocollection.SearchTreeNodeInfo(tni.noderelation.siblingnodeID).nodedata.nodeName);//递归查找全部树状节点，匹配完成后添加子树节点
            //注意，此处默认只有一个软件名称
        }

        /// <summary>
        /// 设置子树名称，便于实现信息的一致化
        /// </summary>
        public void SetChildTreeText()
        {
            foreach (FTATreeNodeInfo outeventnode in treeinfocollection.ftatreenodeinfolist)
            {
                if (outeventnode.nodedata is FTAEventNodeData&&(((FTAEventNodeData)outeventnode.nodedata).eventType== EventType.EventOut || ((FTAEventNodeData)outeventnode.nodedata).eventType == EventType.EventIn))
                {
                    string str_temp = outeventnode.nodedata.nodeName;
                    int loc = str_temp.IndexOf('.');
                    string str_old = str_temp.Substring(0, loc);
                    str_temp = str_temp.Replace(str_old, this.Text);
                    outeventnode.nodedata.nodeName = str_temp;
                }
            }
        }
        /// <summary>
        /// 复制节点信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;
            FTATreeNodeInfo parentNode = treeinfocollection.SearchTreeNodeInfo(parentID);//获取父节点
            if (parentNode.nodedata is FTAEventNodeData)// &&((FTAEventNodeData)parentNode.nodedata).eventType== EventType.EventBasic)
            {
                clipboard = parentNode;
                this.PasteToolStripMenuItem.Visible = true;
            }
        }
        /// <summary>
        /// 粘贴基本事件的信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clipboard != null)
            {
                string parentID = this.addFlow1.SelectedItem.Url;
                FTATreeNodeInfo parentNode = treeinfocollection.SearchTreeNodeInfo(parentID);//获取父节点
                if (treeinfocollection.GetAllAncestors(parentID).Contains(clipboard.nodeID)||treeinfocollection.SearchTreeNodeInfo(parentID).noderelation.childrenNodesID.Contains(clipboard.nodeID))//原来：clipboard.noderelation.parentNodeID == parentID||
                {//检测是否粘贴位置的节点的子节点中已包含本人，或者粘贴位置的节点的祖先中已包含本人
                    MessageBox.Show("子树冗余共享错误！", "禁止");
                }
                else
                {
                    if (parentNode.nodedata is FTAGateNodeData)//判断是否为门元素
                    {
                        int nodelevel = parentNode.level + 1;
                        if (level < nodelevel)
                            level = nodelevel;
                        //string childnodeID = Guid.NewGuid().ToString();
                        //FTATreeNodeInfo tni = new FTATreeNodeInfo(childnodeID, nodelevel, clipboard.nodedata, clipboard.noderelation);
                        //treeinfocollection.ChangeOffSpringLevel(tni);
                        //treeinfocollection.Add(tni);
                        //parentNode.noderelation.childrenNodesID.Add(tni.noderelation.nodeRelationID);
                        ///以上为粘贴基本事件的方法
                        ((FTAEventNodeData)clipboard.nodedata).isDuplicated = true;//设置节点类型为共享
                        parentNode.noderelation.childrenNodesID.Add(clipboard.nodeID);//粘贴子树
                        
                        //刷新整张画布
                        RefreshContent();

                    }
                    else
                        MessageBox.Show("只能将事件粘贴至门节点下！");
                }
            }
            else
                MessageBox.Show("请首先复制事件信息，再进行粘贴！");
        }
        
        /// <summary>
        /// 制作事件信息副本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DuplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string parentID = this.addFlow1.SelectedItem.Url;
            FTATreeNodeInfo parentNode = treeinfocollection.SearchTreeNodeInfo(parentID);//获取父节点
            if (parentNode.nodedata is FTAEventNodeData)// &&((FTAEventNodeData)parentNode.nodedata).eventType== EventType.EventBasic)
            {
                duplicateboard= parentNode;
                this.PasteDuplicateToolStripMenuItem.Visible = true;
            }
        }

        /// <summary>
        /// 将拖动的已完成事件中的事件信息放置到目标节点中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">内含拖动的数据内容</param>
        private void addFlow1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(string))) //拖动本工程事件，抽取出子树，并连接至画布下
            {
                string nodeid = (string)e.Data.GetData(typeof(string));
                Point itemposition = new Point(e.X, e.Y);
                itemposition = PointToClient(itemposition);//坐标系转换
                itemposition = new Point(itemposition.X - this.addFlow1.AutoScrollPosition.X, itemposition.Y - this.addFlow1.AutoScrollPosition.Y);
                if (this.addFlow1.GetItemAt(itemposition) != null)
                {
                    string parentID = this.addFlow1.GetItemAt(itemposition).Url;
                    if (parentID != null)
                    {
                        FTATreeNodeInfo parentNode = treeinfocollection.SearchTreeNodeInfo(parentID);//获取拖动的目标节点
                        FTATreeNodeInfo sourcenode = treeinfocollection.SearchTreeNodeInfo(nodeid);
                        if (sourcenode != null)
                        {
                            if (parentNode.nodedata is FTAEventNodeData)
                            {
                                MessageBox.Show("请将事件拖动至门元素以创建子事件", "错误");
                                //禁止将事件直接拖动到事件中，必须拖动至父节点的门元素中
                                //DialogResult dr = MessageBox.Show("是否将事件[" + sourcenode.nodedata.nodeName + "]的信息复制至[" + parentNode.nodedata.nodeName + "]并创建全部子节点?", "提示", MessageBoxButtons.YesNo);//提示是否共享事件
                                //if (dr == DialogResult.No)
                                //{//不共享
                                //    return;
                                //}
                                //else
                                //{
                                //    FTATreeNodeInfo ancestorNode = treeinfocollection.SearchTreeNodeInfo(parentNode.noderelation.parentNodeID);//获取拖动的目标节点的父节点
                                //    if (ancestorNode.noderelation.childrenNodesID.Contains(sourcenode.nodeID))
                                //        MessageBox.Show("同一门下不能有重复事件！");
                                //    else
                                //    {
                                //        ancestorNode.noderelation.childrenNodesID.Remove(parentID);
                                //        ancestorNode.noderelation.childrenNodesID.Add(sourcenode.nodeID);
                                //        RefreshContent();
                                //    }
                                //}
                            }
                            else if (parentNode.nodedata is FTAGateNodeData)//将事件创建至门元素下
                            {
                                DialogResult dr = MessageBox.Show("是否在[" + parentNode.nodedata.nodeName + "]下创建事件[" + sourcenode.nodedata.nodeName + "]的及其全部子节点?", "提示", MessageBoxButtons.YesNo);//提示是否共享事件
                                if (dr == DialogResult.No)
                                {//不共享
                                    return;
                                }
                                else
                                {
                                    //将事件以及子事件全部添加到门下                  
                                    if (parentNode.noderelation.childrenNodesID.Contains(sourcenode.nodeID) || treeinfocollection.GetAllAncestors(parentID).Contains(sourcenode.nodeID))
                                        MessageBox.Show("子树冗余共享错误！", "禁止");//若目标节点的子节点已包含本人，或者目标节点的祖先节点已包含本人
                                    else
                                    {
                                        ((FTAEventNodeData)sourcenode.nodedata).isDuplicated = true;
                                        parentNode.noderelation.childrenNodesID.Add(sourcenode.nodeID);
                                        RefreshContent();
                                    }
                                }

                            }
                        }
                    }
                }
                else
                    MessageBox.Show("目标节点为空！\n\r请将本事件节点拖动至父节点上以自动抽取和链接子树");
            }
            else if (e.Data.GetDataPresent(typeof(List<FTATreeNodeInfo>)))//拖动数据库产生的子树
            {
                List<FTATreeNodeInfo> temptree = (List<FTATreeNodeInfo>)e.Data.GetData(typeof(List<FTATreeNodeInfo>));
                Point itemposition = new Point(e.X, e.Y);
                itemposition = PointToClient(itemposition);//坐标系转换
                itemposition = new Point(itemposition.X - this.addFlow1.AutoScrollPosition.X, itemposition.Y - this.addFlow1.AutoScrollPosition.Y);
                if (this.addFlow1.GetItemAt(itemposition) != null)
                {
                    string parentID = this.addFlow1.GetItemAt(itemposition).Url;
                    if (parentID != null)
                    {
                        FTATreeNodeInfo parentNode = treeinfocollection.SearchTreeNodeInfo(parentID);//获取拖动的目标节点
                        this.treeinfocollection.Merge(temptree);
                        if (parentNode.nodedata is FTAGateNodeData && temptree[0].nodedata is FTAEventNodeData)
                        {
                            if (parentNode.noderelation.childrenNodesID.Contains(temptree[0].nodeID) || treeinfocollection.GetAllAncestors(parentID).Contains(temptree[0].nodeID))
                                MessageBox.Show("子树冗余共享错误！", "禁止");//若目标节点的子节点已包含本人，或者目标节点的祖先节点已包含本人
                            else
                            {  
                                temptree[0].noderelation.parentNodeID = parentID;
                                parentNode.noderelation.childrenNodesID.Add(temptree[0].nodeID);
                            }
                            
                        }
                        else if (parentNode.nodedata is FTAEventNodeData && temptree[0].nodedata is FTAGateNodeData)
                        {
                            if (parentNode.noderelation.childrenNodesID.Contains(temptree[0].nodeID) || treeinfocollection.GetAllAncestors(parentID).Contains(temptree[0].nodeID))
                                MessageBox.Show("子树冗余共享错误！", "禁止");//若目标节点的子节点已包含本人，或者目标节点的祖先节点已包含本人
                            else
                            {
                                temptree[0].noderelation.parentNodeID = parentID;
                                parentNode.noderelation.childrenNodesID.Add(temptree[0].nodeID);
                            }
                        }
                        ///
                        ///2013.01.16 注释 只能将门添加至事件下或将门添加至事件下
                        /*if ((parentNode.nodedata is FTAEventNodeData && temptree[0].nodedata is FTAEventNodeData) || (parentNode.nodedata is FTAGateNodeData && temptree[0].nodedata is FTAGateNodeData))
                        {
                            if (parentNode.noderelation.parentNodeID != null)
                            {
                                treeinfocollection.SearchTreeNodeInfo(parentNode.noderelation.parentNodeID).noderelation.childrenNodesID.Remove(parentNode.nodeID);
                                treeinfocollection.SearchTreeNodeInfo(parentNode.noderelation.parentNodeID).noderelation.childrenNodesID.Add(temptree[0].nodeID);
                                temptree[0].noderelation.parentNodeID = parentNode.noderelation.parentNodeID;
                            }

                        }
                        else
                        {
                            temptree[0].noderelation.parentNodeID = parentID;
                            parentNode.noderelation.childrenNodesID.Add(temptree[0].nodeID);
                        }         */ 
                    }
                }
                else
                    MessageBox.Show("目标节点为空！\n\r请将数据库中的事件节点拖动至父节点上以自动抽取和链接子树！");
                RefreshContent();
            }
            else if (e.Data.GetDataPresent(typeof(PictureBox)))//拖动工具箱的图标
            {
                //获取拖动的数据,分析拖动的元素名称
                string typename = ((PictureBox)e.Data.GetData(typeof(PictureBox))).Name;
                //获取目标节点元素
                Point itemposition = new Point(e.X, e.Y);
                itemposition = PointToClient(itemposition);//坐标系转换
                //滚轮坐标系转换，需要减去滚轮左上角坐标，换算成工作区可见坐标
                itemposition = new Point(itemposition.X - this.addFlow1.AutoScrollPosition.X, itemposition.Y - this.addFlow1.AutoScrollPosition.Y);
                if (this.addFlow1.GetItemAt(itemposition) != null)
                {
                    string parentID = this.addFlow1.GetItemAt(itemposition).Url;
                    if (parentID != null)
                    {
                        FTATreeNodeInfo parentNode = treeinfocollection.SearchTreeNodeInfo(parentID);//获取拖动的目标节点
                        this.addFlow1.SelectedItem = SearchAddFlowNode(parentID);
                        if (parentNode.nodedata is FTAEventNodeData)//目标节点为事件元素
                        {
                            if (((FTAEventNodeData)parentNode.nodedata).eventType != EventType.EventBasic)
                            {
                                switch (typename)
                                {
                                    case "pictureBox_gateand":
                                        {
                                            this.AddGateAnd(null, null);
                                            break;
                                        }
                                    case "pictureBox_gateor":
                                        {
                                            this.AddGateOr(null, null);
                                            break;
                                        }
                                    case "pictureBox_gateelect":
                                        {
                                            this.AddGateElect_Click(null, null);
                                            break;
                                        }
                                    case "pictureBox_gatexor":
                                        {
                                            this.AddGateXor(null, null);
                                            break;
                                        }
                                    case "pictureBox_gatepri":
                                        {
                                            this.AddGatePri(null, null);
                                            break;
                                        }
                                    case "pictureBox_gateinhibit":
                                        {
                                            this.AddGateInhibit(null, null);
                                            break;
                                        }
                                    case "pictureBox_gatesequenceand":
                                        {
                                            this.AddGateSequenceAnd_Click(null, null);
                                            break;
                                        }
                                    default:
                                        {
                                            MessageBox.Show("事件只能连接到门元素下！");
                                            break;
                                        }
                                }
                            }
                            else
                                MessageBox.Show("基本事件下不能添加任何元素！");
                        }
                        else if (parentNode.nodedata is FTAGateNodeData)//目标节点为门元素
                        {
                            switch (typename)
                            {
                                case "pictureBox_eventbasic":
                                    {
                                        this.AddEventBasic(null, null);
                                        break;
                                    }
                                case "pictureBox_eventintermediate":
                                    {
                                        this.AddEventIntermediate(null, null);
                                        break;
                                    }
                                case "pictureBox_eventundeveloped":
                                    {
                                        this.AddEventUnDeveloped(null, null);
                                        break;
                                    }
                                case "pictureBox_eventconditioning":
                                    {
                                        this.AddEventConditioning(null, null);
                                        break;
                                    }
                                case "pictureBox_eventnormal":
                                    {
                                        this.AddEventInitiating(null, null);
                                        break;
                                    }
                                case "pictureBox_eventout":
                                    {
                                        this.AddEventOut(null, null);
                                        break;
                                    }
                                case "pictureBox_eventin":
                                    {
                                        this.AddEventIn(null, null);
                                        break;
                                    }
                                default:
                                    {
                                        MessageBox.Show("门只能连接到事件元素下！");
                                        break;
                                    }
                            }
                        }
                    }
                }
                else
                    MessageBox.Show("目标节点为空！\n\r请将工具栏节点拖动至父节点上以自动创建子节点");
                //RefreshContent();
            }
            else
                MessageBox.Show("未获得数据！");
        }

        private void addFlow1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(string)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else if (e.Data.GetDataPresent(typeof(List<FTATreeNodeInfo>)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else if (e.Data.GetDataPresent(typeof(PictureBox)))
            {
                e.Effect=DragDropEffects.Copy;
            }
            else
                e.Effect = DragDropEffects.None;
        }
        /// <summary>
        /// 粘贴事件信息副本，更改事件id及名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasteDuplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (duplicateboard != null)//如果已经制作了副本
            {
                string parentID = this.addFlow1.SelectedItem.Url;
                FTATreeNodeInfo parentNode = treeinfocollection.SearchTreeNodeInfo(parentID);//获取父节点
                if (parentNode.nodedata is FTAGateNodeData)//判断是否为门元素
                {
                    int nodelevel = parentNode.level + 1;//获取父节点层级，+1即为待粘贴的节点层级
                    if (level < nodelevel)
                        level = nodelevel;
                    //string childnodeID = Guid.NewGuid().ToString();
                    //FTATreeNodeInfo tni = new FTATreeNodeInfo(childnodeID, clipboard.nodedata, parentID, new List<string>(), nodelevel);
                    //treeinfocollection.Add(tni);
                    //parentNode.childrenNodesID.Add(childnodeID);
                    ///以上为粘贴基本事件的方法
                    ///以下为粘贴中间事件的方法
                    List<FTATreeNodeInfo> descendants= treeinfocollection.GetDescendant(duplicateboard.nodeID);//制作副本节点以及全部子节点的副本
                    treeinfocollection.Merge(descendants);//将副本链表与原始链表合并
                    FTATreeNodeInfo parentofchildren = new FTATreeNodeInfo();//声明副本子树的树根
                    foreach (FTATreeNodeInfo temptni in descendants)//搜索副本子树的树根
                    {
                        if (temptni.noderelation.parentNodeID == string.Empty)//副本子树的树根level设置为了-1，查找
                        {
                            parentofchildren = temptni;
                            break;
                        }
                    }
                        parentofchildren.level = nodelevel;//设置层次

                        parentofchildren.noderelation.parentNodeID = parentNode.nodeID;
                        parentNode.noderelation.childrenNodesID.Add(parentofchildren.nodeID);//粘贴子树

                        if (parentNode.noderelation.childrenNodesID.Count != 0)//如果节点仍然有子节点
                        {
                            treeinfocollection.ChangeOffSpringLevel(parentofchildren);//递归更改全部子树的层级
                        }
                        //刷新整张画布
                        RefreshContent();
                    
                }
                else
                    MessageBox.Show("只能将事件粘贴至门节点下！");
            }
            else
                MessageBox.Show("请首先复制基本事件，再进行粘贴！");
        }

        /// <summary>
        /// 计算最小割集算法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void generateMinimalCutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SFTAInfoViewForm infoform = (SFTAInfoViewForm)ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(null, new UserUIEventArgs("InfoViewForm"));
            infoform.generateMinimalCut(this.Name, this.treeinfocollection);
            infoform.SelectedSetChange = new SelectedSetChangeHandler(RefreshContent);//属性窗体发生属性变化，刷新本窗体Change = new propform.PropertyChange = new PropertyChangeHandler(RefreshContent);//选中的最小割集发生属性变化，刷新本窗体ChangeHandler(RefreshContent);//属性窗体发生属性变化，刷新本窗体Change = new PropertyChangeHandler(RefreshContent);//属性窗体发生属性变化，刷新本窗体
            infoform.NaviChange=new NaviChangeHandler(ScrollToNode);
        }
        /// <summary>
        /// 更改各节点不同状态的颜色配置
        /// </summary>
        public void changeColorSetting()
        {
            SFTAProject currentproject = (SFTAProject)ProjectManager.ProjectManagerSington.GetCurrentProject();
            if (currentproject.statuscolors.Count != 0)
            {
               // this.statuscolor.Clear();
                this.statuscolor = currentproject.statuscolors;//Color.AliceBlue;
            }
            else
            {
                this.statuscolor.Add(Color.Yellow);//标记颜色
                this.statuscolor.Add(Color.Red);//未完成
                this.statuscolor.Add(Color.AliceBlue);//已完成
                this.statuscolor.Add(Color.SeaGreen);//复用的子树
                this.statuscolor.Add(Color.Lavender);//最小割集的节点
            }
        }

        /// <summary>
        /// 点击配置菜单的“节点颜色配置"，弹出配置菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colorConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingForm settingform = new SettingForm();
            if (settingform.ShowDialog() == DialogResult.OK)
            {
                SFTAProject currentproject = (SFTAProject)ProjectManager.ProjectManagerSington.GetCurrentProject();
                if (currentproject.statuscolors.Count == 0)
                {
                    currentproject.statuscolors.Add(settingform.color1);
                    currentproject.statuscolors.Add(settingform.color2);
                    currentproject.statuscolors.Add(settingform.color3);
                    currentproject.statuscolors.Add(settingform.color4);
                    currentproject.statuscolors.Add(settingform.color5);
                }
                else
                {
                    currentproject.statuscolors[0]=settingform.color1;
                    currentproject.statuscolors[1]=settingform.color2;
                    currentproject.statuscolors[2]=settingform.color3;
                    currentproject.statuscolors[3] = settingform.color4;
                    currentproject.statuscolors[4] = settingform.color5;
                }
            }
            changeColorSetting();
            this.RefreshContent();
        }

        public void exportDiagramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Metafile exportdiagram = this.addFlow1.ExportMetafile(false, true, false);//导出为图片源文件


            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "jpeg" + "文件|*." + "jpeg,jpg";

            string filepath = null;
            if ((saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                filepath = saveDialog.FileName;
                exportdiagram.Save(filepath);
            }
        }

        public void produceWord(object sender, EventArgs e)
        {
            Dictionary<string, FTATreeInfo> ftatreeDic = (Dictionary<string, FTATreeInfo>)sender;
            //SaveFileDialog saveDialog = new SaveFileDialog();
            string formname;
            FTAForm ftaform;

            string path=ProjectManager.ProjectManagerSington.GetCurrentProject().Path;

            WordServe ws = new WordServe();

            ws.insertText("              生成信息如下",1);

            int index = 1;

            foreach (var tftatree in ftatreeDic)
            {
                FTATreeInfo ftatree = tftatree.Value;
                FTATreeNodeInfo treenodeinfo = ftatree.SearchRootNode();

                string nodeName = treenodeinfo.nodedata.nodeName;
           
                ws.insertText(index.ToString()+"."+nodeName + "割集:",2);
                index++;

                Dictionary<int, List<FTATreeNodeInfo>> cutsetdic = ftatree.GenerateMinimumCutSet();
                int key = 1;
                foreach (KeyValuePair<int, List<FTATreeNodeInfo>> pair in cutsetdic)
                {
                    string namecollection = string.Empty;
                    foreach (FTATreeNodeInfo tni in pair.Value)
                        namecollection += tni.nodedata.nodeName + " ";
                    ws.insertText("    "+key.ToString() + "{ " + namecollection + "}",4);
                    key++;
                }

                ws.insertText("    生成图形如下:",3);
                formname = treenodeinfo.nodeID;
                ftaform = (FTAForm)ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(null, new UserUIEventArgs(formname));
                Metafile exportdiagram = ftaform.addFlow1.ExportMetafile(false, true, false);
                string picpath = path+"tmp.jpeg";
                exportdiagram.Save(picpath);
                ws.insertPic(picpath);
            }
            ws.initCount();
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private void FTAForm_Scroll(object sender, ScrollEventArgs e)
        {
            
        }

        private void FTAForm_Activated(object sender, EventArgs e)
        {
        }
    }
}
