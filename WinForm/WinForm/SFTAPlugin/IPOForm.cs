using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Platform.Core;
using Platform.Core.Data;
using Platform.Core.UI;
using Platform.Core.Services;
using WeifenLuo.WinFormsUI.Docking;
using System.Data.OleDb;

using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using System.Data.Common;

namespace SFTAPlugin
{
    public partial class IPOForm : BaseForm
    {
        public TreeNode currentnode;// = new TreeNode();//当前选中的树节点
        
        public IPOForm()
        {
            InitializeComponent();
            if (this.treeViewFunc.TopNode == null)
            {
                TreeNode topnode = new TreeNode("仿外挂物管理系统");
                //topnode.Name = "SoftwareName";
                topnode.Tag = "SoftwareName";
                this.treeViewFunc.TopNode = topnode;

                //TreeNode tn = new TreeNode();//创建树状列表节点
               // FTATreeNodeInfo tni =new FTATreeNodeInfo(,,"软件A",);//搜索故障树的根节点
               // tn.Name = tni.nodeID;//将故障树节点ID赋予树状节点的名称
              //  tn.Text = "软件B";
                // tni.nodedata.nodeName;//将故障树节点名称赋予树状节点的显示文本
               // tn.Tag = "TopEvent";//树节点类型为顶事件
              //  treeViewFunc.TopNode.Nodes.Add(tn);//将树节点添加至treeview中
                this.treeViewFunc.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            topnode});
            }
           
            treeViewFunc.ExpandAll();

          

        }
        //


        private void treeViewFunc_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//右键单击
            {
                TreeNode tn = treeViewFunc.GetNodeAt(e.X, e.Y);
                if (tn != null)
                {
                    treeViewFunc.SelectedNode = tn;
                    currentnode = tn;
                    if (tn.Tag.ToString() == "SoftwareName")
                        contextMenuStrip1.Show(PointToScreen(new Point(e.X, e.Y)));
                    else if (tn.Tag.ToString() == "TopEvent")//有问题，未初始化
                        contextMenuStrip2.Show(PointToScreen(new Point(e.X, e.Y)));
                }
            }
            else if (e.Button == MouseButtons.Left)//左键单击
            {
                 currentnode = treeViewFunc.SelectedNode;
                 TreeNode tn = treeViewFunc.GetNodeAt(e.X, e.Y);
                 if (tn != null)
                 {
                     if (tn.Tag.ToString() == "TopEvent")
                     {
                         textBoxModName.Text = tn.Text;
                     }

                 }

             }
        }

        public void AddTopEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentnode = this.treeViewFunc.SelectedNode;
            if (currentnode != null && currentnode.Tag.ToString() == "SoftwareName")
            {
                string lastdefaultname = "新模块0";
                foreach (TreeNode tempnode in currentnode.Nodes)//判断最后一个默认命名的子节点的名称
                {
                    if (tempnode.Text.Contains("新模块"))
                        lastdefaultname = tempnode.Text;
                }
                int index;
                unchecked
                {
                    index = int.Parse(lastdefaultname.Substring(3)) + 1;//有风险，可能无法转换
                }
                TreeNode childnode = new TreeNode("新模块" + index.ToString());
                childnode.Name = Guid.NewGuid().ToString();//节点ID
                childnode.Tag = "TopEvent";
                currentnode.Nodes.Add(childnode);
                currentnode = childnode;
                this.treeViewFunc.SelectedNode = childnode;


            }
            treeViewFunc.ExpandAll();
        }

        private void AddModuleMenuItem_Click(object sender, EventArgs e)
        {
            currentnode = this.treeViewFunc.SelectedNode;
            if (currentnode != null && currentnode.Tag.ToString() == "TopEvent")
            {
                string lastdefaultname = "新子模块0";
                foreach (TreeNode tempnode in currentnode.Nodes)//判断最后一个默认命名的子节点的名称
                {
                    if (tempnode.Text.Contains("新子模块"))
                        lastdefaultname = tempnode.Text;
                }
                int index;
                unchecked
                {
                    index = int.Parse(lastdefaultname.Substring(4)) + 1;//有风险，可能无法转换
                }
                TreeNode childnode = new TreeNode("新子模块" + index.ToString());
                childnode.Name = Guid.NewGuid().ToString();//节点ID
                childnode.Tag = "TopEvent";
                currentnode.Nodes.Add(childnode);
                currentnode = childnode;
                this.treeViewFunc.SelectedNode = childnode;

    
            }
            treeViewFunc.ExpandAll();
        }

        /// <summary>
        /// 重命名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeViewFunc.SelectedNode.BeginEdit();
        }



        /// <summary>
        /// 更改节点名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewFunc_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)//此时编辑后尚未将新文本赋予对应的text
        {
            if (e.Label != null && !e.Label.Equals(string.Empty))
            {
                treeViewFunc.SelectedNode.Text = e.Label;//获取更改后的文本
                //更改对应的FTAForm的名称
                FTAForm tempform = (FTAForm)ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(null, new UserUIEventArgs(treeViewFunc.SelectedNode.Name));
                if (tempform != null)
                {
                    tempform.Text = treeViewFunc.SelectedNode.Text;
                    tempform.topnode.nodedata.nodename = treeViewFunc.SelectedNode.Text;
                    tempform.Refresh();
                    tempform.RefreshContent();
                }
            }
            else
                e.CancelEdit = true;//若label更改后未输入文字，则取消编辑
        }


        private void btnFMEA_Click(object sender, EventArgs e)
        {
    
            BindDataSource(InitDtFromFMEADB());//调用初始化gridctrl程序 加载数据库
           
        }


        //初始化失效分析表格
        //private DataTable InitDt()
        //{
        //    DataTable dt = new DataTable("失效分析表格");
        //    dt.Columns.Add("IPOType", typeof(string));
        //    dt.Columns.Add("Process", typeof(string));
        //    dt.Columns.Add("FailureName", typeof(string));
        //    dt.Columns.Add("check", typeof(bool));


        //    dt.Rows.Add(new object[] { "0", "条件满足SMS向显控返回ACK", "条件不满足时，SMS误判返回ACK", 1 });
        //    dt.Rows.Add(new object[] { "0", "条件不满足，SMS向显控返回NAK", "条件满足时，SMS误判返回NAK", 1 });
        //    dt.Rows.Add(new object[] { "0", "条件不满足，SMS向显控返回NAK", "无法判断是否满足情况", 0 });
        //    dt.Rows.Add(new object[] { "P", "DTE计算加载当前外挂物清单", "计算数据不正确，武器数量等计算结果与实际不符", 1 });
        //    dt.Rows.Add(new object[] { "P", "DTE计算加载当前外挂物清单", "计算数据无效", 0 });
        //    dt.Rows.Add(new object[] { "I", "DTE状态", "DTE状态无法处于ON状态（前提条件为ON）", 1 });
        //    dt.Rows.Add(new object[] { "I", "WOW位", "当前WOW状态为地面，程序读取为空中（前提条件WOW位为1）", 1 });
        //    dt.Rows.Add(new object[] { "I", "WOW位", "WOW状态不为空中也不为地面", 0 });
        //    dt.Rows.Add(new object[] { "I", "挂点存在状态", "挂点存在，但判断为不存在", 1 });
        //    dt.Rows.Add(new object[] { "I", "挂点类型总数量", "挂点类型总数量超过5种（前提条件不得超过5种）", 1 });
        //    dt.Rows.Add(new object[] { "I", "挂点类型总数量", "挂点类型总数量不为整形类型", 0 });
        //    dt.Rows.Add(new object[] { "I", "1,5号挂点炸弹数量", "挂点超过规定数量", 1 });
        //    dt.Rows.Add(new object[] { "I", "外挂物类型", "1,2,4,5挂点外挂物为规定外未知武器）", 1 });
        //    dt.Rows.Add(new object[] { "I", "对称挂点外挂物类型", "对称挂点外挂物类型不一致", 1 });
        //    dt.Rows.Add(new object[] { "I", "对称挂点外挂物类型", "对称挂点外挂物类型不一致时误判为一致", 0 });
        //    return dt;
        //}


        private DataTable InitDtFromFMEADB()
        {
            string ModName = textBoxModName.Text;

            String connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=FMEA.mdb";
            OleDbConnection connection = new OleDbConnection(connectionString);

            OleDbCommand cmd = new OleDbCommand();
            connection.Open();//打开连接

            string searchrootsql = string.Format("select FailureMode_IPO,FailureMode_IPODescription,FailureMode_Name from tb_FailureMode where FailureMode_ModuleName='{0}'", ModName);//搜索包含nodeid的行


            cmd.CommandText = searchrootsql;//设置命令的文本
            cmd.CommandType = CommandType.Text;//设置命令的类型
            cmd.Connection = connection;//设置命令的连接

            int result = cmd.ExecuteNonQuery();



            /////查询
            DataSet ds = new DataSet();//DataSet是表的集合
            OleDbDataAdapter da = new OleDbDataAdapter(searchrootsql, connection);//从数据库中查询

            connection.Close();//数据库关闭

            //  DataTable FMEAdt = new DataTable("失效分析表格");
            //  FMEAdt.Columns.Add("IPOType", typeof(string));
            //  FMEAdt.Columns.Add("Process", typeof(string));
            //  FMEAdt.Columns.Add("FailureName", typeof(string));
            ////  FMEAdt.Columns.Add("check", typeof(bool));
            da.Fill(ds);//将数据填充到DataSet


            DataTable newFMEAdt = new DataTable("FMEA查询结果表格");
            DataTable FMEAdt = ds.Tables[0];

            newFMEAdt.Columns.Add("IPOType", typeof(string));
            newFMEAdt.Columns.Add("Process", typeof(string));
            newFMEAdt.Columns.Add("FailureName", typeof(string));
            newFMEAdt.Columns.Add("check", typeof(bool));
            if (FMEAdt.Rows.Count == 0)//没有查到相关数据时
            {
                MessageBox.Show("无此模块FMEA信息，请手动添加失效信息");
            }

            for (int i = 0; i < FMEAdt.Rows.Count; i++)
            {
                string strIPOType = FMEAdt.Rows[i]["FailureMode_IPO"].ToString();
                string strProcess = FMEAdt.Rows[i]["FailureMode_IPODescription"].ToString();
                string strFailureName = FMEAdt.Rows[i]["FailureMode_Name"].ToString();
                newFMEAdt.Rows.Add(new object[] { strIPOType, strProcess, strFailureName, 0 });

            }


            return newFMEAdt;
        }

        private void BindDataSource(DataTable dt)
        {
            //绑定DataTable  
            gridCtrlIPOFMEA.DataSource = dt;
            //绑定DataSet  
            //gridControl1.DataSource = ds;  
            //gridControl1.DataMember = "表名";  
        }  






        private void btnOKIPO_Click(object sender, EventArgs e)
        {
            
            FTATreeInfo IPOtree = new FTATreeInfo();

            string Modid = Guid.NewGuid().ToString();//加入模块为父节点
            string ModName = textBoxModName.Text;
            FTATreeNodeInfo Modtni = new FTATreeNodeInfo(Modid, 4, new FTAEventNodeData(Modid, ModName, EventType.EventIntermediate, "sdsfsdfasd", "sdfsdf"), new FTANodeRelation(Modid, string.Empty, new List<string>()));
            IPOtree.Add(Modtni);



            string OrGateid = Guid.NewGuid().ToString();//加入或门
            FTATreeNodeInfo OrGatetni = new FTATreeNodeInfo(OrGateid, 4, new FTAGateNodeData(OrGateid, "或门", GateType.GateOr, "sdsfsdfasd"), new FTANodeRelation(OrGateid, Modid, new List<string>()));
            Modtni.noderelation.childrenNodesID.Add(OrGateid);

            IPOtree.Add(OrGatetni);



            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if ((gridView1.GetRowCellDisplayText(i, gridView1.Columns[3])) == "Checked")// 被选为建树元素的话
                {
                    string FM=gridView1.GetRowCellDisplayText(i, gridView1.Columns[2]);
                    string FMid = Guid.NewGuid().ToString();//加入失效模式作为建树的底事件
                    FTATreeNodeInfo FMtni = new FTATreeNodeInfo(FMid, 4, new FTAEventNodeData(FMid, FM, EventType.EventBasic, "事件信息", "sdfsdf"), new FTANodeRelation(FMid, OrGateid, new List<string>()));
                    OrGatetni.noderelation.childrenNodesID.Add(FMid);
                    IPOtree.Add(FMtni);
                    
                    //MessageBox.Show("checked");
                }
            }  


            InsertIntoDatabase(IPOtree.ftatreenodeinfolist);
            
            
            
            //加入事件，父节点
            //   FTATreeInfo IPOtree = new FTATreeInfo();
            //string id2=Guid.NewGuid().ToString();

            //FTATreeNodeInfo Modtni = new FTATreeNodeInfo(id2, 4, new FTAEventNodeData(id2, "功能1", EventType.EventIntermediate, "sdsfsdfasd", "sdfsdf"), new FTANodeRelation(id2, string.Empty, new List<string>()));
            //IPOtree.Add(Modtni);
            //加入子节点
            //string id3 = Guid.NewGuid().ToString();
            //FTATreeNodeInfo tnitemp2 = new FTATreeNodeInfo(id3, 4, new FTAGateNodeData(id3, "功能1",GateType.GateAnd, "sdsfsdfasd"), new FTANodeRelation(id3, id2, new List<string>()));
            //tnitemp.noderelation.childrenNodesID.Add(id3);
            //tree.Add(tnitemp2);

            //InsertIntoDatabase(tree.ftatreenodeinfolist);

//接下来就是微软的事了。
//if (ds.HasChanges())
//{
//    DataSet dsModify = this.ds.GetChanges();
//    if (dsModify != null)
//       {
//           this.adapter.Update(dsModify.Tables[0]);
//           this.ds.AcceptChanges();
//           MessageBox.Show("保存成功.", "提示", MessageBoxButtons.OK,MessageBoxIcon.Information);
//        }
//}
    //        string ss = gridView1.GetRowCellDisplayText(0, gridView1.Columns[0]);

           // string ss2 = gridView1.GetRowCellValue(0, gridView1.Columns[0]);

            //for (int i = 0; i < gridView1.RowCount; i++)
            //{
            //   // string checkornot=gridView1.GetRowCellDisplayText(i, gridView1.Columns[3]);

            //    if((gridView1.GetRowCellDisplayText(i, gridView1.Columns[3]))=="Checked")// 被选为建树元素的话
            //    {
            //        //MessageBox.Show("checked");
            //    }
            //}  




            gridCtrlIPOFMEA.RefreshDataSource();//
            this.gridView1.CloseEditor();
            this.gridView1.UpdateCurrentRow();

        }

        private void InsertIntoDatabase(List<FTATreeNodeInfo> treenodelist)
        {
            String IPOconnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=SFTAIPODataBase.mdb";
            OleDbConnection connection = new OleDbConnection(IPOconnectionString);

            OleDbCommand cmd = new OleDbCommand();
            connection.Open();//打开连接

            foreach (FTATreeNodeInfo tni in treenodelist)
            {
                string nodetype, fmeainfo;
                if (tni.nodedata is FTAEventNodeData)
                {
                    nodetype = ((FTAEventNodeData)tni.nodedata).eventType.ToString();
                    fmeainfo = ((FTAEventNodeData)tni.nodedata).FMEAInfo;
                }
                else
                {
                    nodetype = ((FTAGateNodeData)tni.nodedata).gateType.ToString();
                    fmeainfo = string.Empty;
                }
                string insertsql = string.Format("insert into SFTATreeInfo (nodeID,nodedataID,nodename,description,NodeType,parentID,siblingID,FMEAInfo) Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", tni.nodeID, tni.nodedata.nodeDataID, tni.nodedata.nodename, tni.nodedata.description, nodetype, tni.noderelation.parentNodeID, tni.noderelation.siblingnodeID, fmeainfo);

                cmd.CommandText = insertsql;//设置命令的文本
                cmd.CommandType = CommandType.Text;//设置命令的类型
                cmd.Connection = connection;//设置命令的连接
                int x = cmd.ExecuteNonQuery();//执行SQL语句

                foreach (string childid in tni.noderelation.childrenNodesID)
                {
                    insertsql = string.Format("insert into SFTANodeRelation (nodeID,childID) Values ('{0}','{1}')", tni.nodeID, childid);

                    cmd.CommandText = insertsql;//设置命令的文本
                    cmd.CommandType = CommandType.Text;//设置命令的类型
                    cmd.Connection = connection;//设置命令的连接
                    x = cmd.ExecuteNonQuery();//执行SQL语句
                }
            }

            connection.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            gridView1.AddNewRow();
            gridView1.OptionsBehavior.Editable = true;
            //设置类型为可选类型
            //GridColumn column = gridView1.Columns.AddField("转换类型");
            //column.VisibleIndex = gridView1.Columns.Count;
            //column.UnboundType = DevExpress.Data.UnboundColumnType.String;
            //// Disable editing.  
            //column.OptionsColumn.AllowEdit = true;
            //column.OptionsFilter.AllowFilter = false;
            //column.OptionsColumn.AllowMove = false;
            //column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            //// Specify format settings.  
            //column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            ////column.DisplayFormat.FormatString = "c";
            //// Customize the appearance settings.  
            ////column.AppearanceCell.BackColor = Color.LemonChiffon;
            

        }
        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            ColumnView View = sender as ColumnView;
            View.SetRowCellValue(e.RowHandle, View.Columns[0], gridView1.GetRowCellValue(gridView1.GetRowHandle(gridView1.RowCount - 2), gridView1.Columns[0])); //复制最后一行的数据到新行

            View.SetRowCellValue(e.RowHandle, View.Columns[1], gridView1.GetRowCellValue(gridView1.GetRowHandle(gridView1.RowCount - 2), gridView1.Columns[1])); //复制最后一行的数据到新行

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要删除选中的记录吗？", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, 0, false) == DialogResult.Yes)
            {
                int iSelectRowCount = gridView1.SelectedRowsCount;
                if (iSelectRowCount > 0)
                {
                    gridView1.DeleteSelectedRows();
                }
            }
            gridCtrlIPOFMEA.RefreshDataSource();

        }


        private void btnMod_Click(object sender, EventArgs e)
        {
            gridView1.OptionsBehavior.Editable = true;
        }












    }
}
