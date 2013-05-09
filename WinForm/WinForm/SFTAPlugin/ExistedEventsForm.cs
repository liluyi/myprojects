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
using System.Data.OleDb;


namespace SFTAPlugin
{
    public partial class ExistedEventsForm : BaseForm
    {
        FTATreeInfo ftatreeinfo=new FTATreeInfo();
        DataGridViewRow copyedrow;
        DataGridViewRow IPOcopyedrow;

        public ExistedEventsForm()
        {
            InitializeComponent();
            RefreshContent();
            this.CurrentProjectTab.SelectedIndex = 1;
        }
        public void RefreshTree()
        {
            SFTAProject currentpj = (SFTAProject)ProjectManager.ProjectManagerSington.GetCurrentProject();
            if (currentpj != null)
            {
                this.ftatreeinfo.ftatreenodeinfolist.Clear();
                foreach (FTATreeInfo tree in currentpj.SFTATreesDic.Values)
                {
                    this.ftatreeinfo.Merge(tree.ftatreenodeinfolist);
                    foreach (FTATreeNodeInfo tni in tree.ftatreenodeinfolist)
                    {
                        //if ((tni.nodedata is FTAEventNodeData)&&((FTAEventNodeData)tni.nodedata).eventType == EventType.EventBasic)//搜索基本事件
                        if (tni.nodedata is FTAEventNodeData)//搜索全部事件
                        {
                            currentpjdataGridView.Rows.Add(tni.nodeID, tni.nodedata.nodeDataID, tni.nodedata.nodeName, tni.nodedata.Decription);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 刷新表格内容
        /// </summary>
        /// <param name="treeinfo">待填充的故障树信息</param>
        public void RefreshContent()
        {
            //设置本工程标签
            //this.ftatreeinfo = treeinfo;
            currentpjdataGridView.Rows.Clear();

            SFTAProject currentpj = (SFTAProject)ProjectManager.ProjectManagerSington.GetCurrentProject();
            if (currentpj != null)
            {
                foreach (FTATreeInfo tree in currentpj.SFTATreesDic.Values)
                    foreach (FTATreeNodeInfo tni in tree.ftatreenodeinfolist)
                    {
                        //if ((tni.nodedata is FTAEventNodeData)&&((FTAEventNodeData)tni.nodedata).eventType == EventType.EventBasic)//搜索基本事件
                        if (tni.nodedata is FTAEventNodeData)//搜索全部事件
                        {
                            currentpjdataGridView.Rows.Add(tni.nodeID, tni.nodedata.nodeDataID, tni.nodedata.nodeName, tni.nodedata.Decription);
                        }
                    }
            }

            //设置FTA库标签
            DBdataGridView.Rows.Clear();
            String connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=SFTADataBase.mdb";
            OleDbConnection connection = new OleDbConnection(connectionString);
            string searchsql = "select * from SFTATreeInfo";
            OleDbCommand cmd = new OleDbCommand(searchsql, connection);
            connection.Open();//打开连接
            int result = cmd.ExecuteNonQuery();

            ///查询
            DataSet ds = new DataSet();//DataSet是表的集合
            OleDbDataAdapter da = new OleDbDataAdapter(searchsql, connection);//从数据库中查询
            da.Fill(ds);//将数据填充到DataSet

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                DBdataGridView.Rows.Add(row[1],row[2], row[3], row[4]);
            }

            ///更新
            //string nodeID = string.Empty;
            //string nodeName = string.Empty;
            //string insertsql = string.Format("insert into SFTATreeInfo values('{0}','{1}')", nodeID, nodeName);
            //OleDbCommand oc = new OleDbCommand();//表示要对数据源执行的SQL语句或存储过程
            //oc.CommandText = insertsql;//设置命令的文本
            //oc.CommandType = CommandType.Text;//设置命令的类型
            //oc.Connection = connection;//设置命令的连接
            //int x = oc.ExecuteNonQuery();//执行SQL语句

            connection.Close();//关闭连接


            //设置IPO分析结果标签
            IPOdataGridView.Rows.Clear();
            String IPOconnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=SFTAIPODataBase.mdb";
            OleDbConnection IPOconnection = new OleDbConnection(IPOconnectionString);
            string IPOsearchsql = "select * from SFTATreeInfo";
            OleDbCommand IPOcmd = new OleDbCommand(IPOsearchsql, IPOconnection);
            IPOconnection.Open();//打开连接
            int IPOresult = IPOcmd.ExecuteNonQuery();

            ///查询 DataGridViewRow 
            DataSet IPOds = new DataSet();//DataSet是表的集合
            OleDbDataAdapter IPOda = new OleDbDataAdapter(IPOsearchsql, IPOconnection);//从数据库中查询
            IPOda.Fill(IPOds);//将数据填充到DataSet

            foreach (DataRow row in IPOds.Tables[0].Rows)
            {
                IPOdataGridView.Rows.Add(row[1], row[2], row[3], row[4]);
            }



            IPOconnection.Close();//关闭连接
        }

        private void ExistedEventsForm_Click(object sender, EventArgs e)
        {
            //this.keytextBox.Text = string.Empty;
        }

        private void keytextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//按下回车键开始搜索，已废弃
            {
                //string searchword = this.keytextBox.Text;
                //currentpjdataGridView.Rows.Clear();
                //foreach (FTATreeNodeInfo tni in ftatreeinfo.ftatreenodeinfolist)
                //{
                //    if (tni.nodedata.nodeName.Contains(searchword) || tni.nodedata.Decription.Contains(searchword)) //)//暂时不搜索类型
                //        currentpjdataGridView.Rows.Add(tni.nodedata.nodeName, tni.nodedata.Decription);
                //}
            }
        }
        /// <summary>
        /// 单击文本域，清空默认提示信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void keytextBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.keytextBox.Text.Equals("键入内容以搜索"))
                this.keytextBox.Text = string.Empty;
        }

        
        /// <summary>
        /// 搜索FTA库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchword = this.searchDBtextBox.Text;
            if (!searchword.Equals(string.Empty))
            {
                DBdataGridView.Rows.Clear();//清空列表

                String connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=SFTADataBase.mdb";
                OleDbConnection connection = new OleDbConnection(connectionString);
                string searchsql = string.Format("select * from SFTATreeInfo where nodename like '%{0}%' or description like '%{0}%' or FMEAInfo like '%{0}%'", searchword);//搜索包含searchword的行
                OleDbCommand cmd = new OleDbCommand(searchsql, connection);
                connection.Open();//打开连接
                int result = cmd.ExecuteNonQuery();

                ///查询
                DataSet ds = new DataSet();//DataSet是表的集合
                OleDbDataAdapter da = new OleDbDataAdapter(searchsql, connection);//从数据库中查询
                da.Fill(ds);//将数据填充到DataSet

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DBdataGridView.Rows.Add(row[1],row[2], row[3], row[4]);
                }
            }
            else
            {
                this.RefreshContent();
            }
        }
        /// <summary>
        /// 搜索当前工程的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void keytextBox_TextChanged(object sender, EventArgs e)
        {
            string searchword = this.keytextBox.Text;
            currentpjdataGridView.Rows.Clear();
            //if(ftatreeinfo.ftatreenodeinfolist.Count==0)
            //    this.RefreshTree();
            SFTAProject currentpj = (SFTAProject)ProjectManager.ProjectManagerSington.GetCurrentProject();
            if (currentpj != null)
            {
                foreach (FTATreeInfo tree in currentpj.SFTATreesDic.Values)
                    foreach (FTATreeNodeInfo tni in tree.ftatreenodeinfolist)
                    {
                        if (tni.nodedata is FTAEventNodeData&&(tni.nodedata.nodeName.Contains(searchword) || tni.nodedata.Decription.Contains(searchword))) //暂时不搜索类型，只搜索事件名称和描述信息
                            currentpjdataGridView.Rows.Add(tni.nodeID, tni.nodedata.nodeDataID, tni.nodedata.nodeName, tni.nodedata.Decription);
                    }
            }
        }

        private void searchDBtextBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.searchDBtextBox.Text.Equals("键入内容以搜索"))
                this.searchDBtextBox.Text = string.Empty;
        }

        /// <summary>
        /// 从本工程事件列表中拖动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point p =PointToClient(new Point(e.X, e.Y));
                contextMenuStrip1.Show(p);
            }
            else if (e.Button == MouseButtons.Left)
            {
                copyedrow = this.currentpjdataGridView.SelectedRows[0];
                this.currentpjdataGridView.DoDragDrop((string)copyedrow.Cells[0].Value, DragDropEffects.Copy);
            }
        }
        /// <summary>
        /// 从数据库事件列表中拖动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DBdataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            copyedrow = this.DBdataGridView.SelectedRows[0];
            string copiedid = (string)copyedrow.Cells[0].Value;

            List<FTATreeNodeInfo> DBFTATree= GenerateChildTreeFromDB(copiedid);
            this.DBdataGridView.DoDragDrop(DBFTATree, DragDropEffects.Copy);

        }
        /// <summary>
        /// 右键将节点及其子树添加至数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddToDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RefreshTree();
            string id = (string)this.currentpjdataGridView.SelectedRows[0].Cells[0].Value;
            FTATreeNodeInfo tni=this.ftatreeinfo.SearchTreeNodeInfo(id);
            List<FTATreeNodeInfo> treenodelist=new List<FTATreeNodeInfo>();
            this.ftatreeinfo.GetAllDescendants(tni, ref treenodelist);
            treenodelist.Add(tni);

            InsertIntoDatabase(treenodelist);

            RefreshContent();
        }
        /// <summary>
        /// 将子树插入数据库
        /// </summary>
        /// <param name="treenodelist"></param>
        private void InsertIntoDatabase(List<FTATreeNodeInfo> treenodelist)
        {
            String connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=SFTADataBase.mdb";
            OleDbConnection connection = new OleDbConnection(connectionString);
            
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
        /// <summary>
        /// 从数据库生成子树
        /// </summary>
        private List<FTATreeNodeInfo> GenerateChildTreeFromDB(string nodeid)
        {
            List<FTATreeNodeInfo> temptree=new List<FTATreeNodeInfo>();
            FTATreeNodeInfo tni = GenerateTreeNodeFromDB(nodeid);
            temptree.Add(tni);
            GenerateTree(tni, ref temptree);

            return temptree;
        }

        private void GenerateTree(FTATreeNodeInfo tni,ref List<FTATreeNodeInfo> temptree)
        {           
            if (tni.noderelation.childrenNodesID.Count != 0)
            {
                foreach (string childid in tni.noderelation.childrenNodesID)
                {
                    FTATreeNodeInfo childnodeinfo = GenerateTreeNodeFromDB(childid);
                    temptree.Add(childnodeinfo);
                    GenerateTree(childnodeinfo, ref temptree);
                }
            } 
        }

        private FTATreeNodeInfo GenerateTreeNodeFromDB(string nodeid)
        {
            String connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=SFTADataBase.mdb";
            OleDbConnection connection = new OleDbConnection(connectionString);

            OleDbCommand cmd = new OleDbCommand();
            connection.Open();//打开连接

            string searchrootsql = string.Format("select * from SFTATreeInfo where nodeID='{0}'", nodeid);//搜索包含nodeid的行

            ////OleDbCommand cmd1 = new OleDbCommand(searchsql1, connection);
            cmd.CommandText = searchrootsql;//设置命令的文本
            cmd.CommandType = CommandType.Text;//设置命令的类型
            cmd.Connection = connection;//设置命令的连接

            int result = cmd.ExecuteNonQuery();

            /////查询
            DataSet ds = new DataSet();//DataSet是表的集合
            OleDbDataAdapter da = new OleDbDataAdapter(searchrootsql, connection);//从数据库中查询
            da.Fill(ds);//将数据填充到DataSet

            DataRow row = ds.Tables[0].Rows[0];//获取结果

            FTATreeNodeInfo tni;
            if (convertToEventType((string)row[5]) == EventType.Error)
                tni = new FTATreeNodeInfo((string)row[1], 99, new FTAGateNodeData((string)row[2], (string)row[3], convertToGateType((string)row[5]), (string)row[4]), new FTANodeRelation((string)row[1], (string)row[6], new List<string>()));
            else
                tni = new FTATreeNodeInfo((string)row[1], 99, new FTAEventNodeData((string)row[2], (string)row[3], convertToEventType((string)row[5]), (string)row[4], (string)row[8]), new FTANodeRelation((string)row[1], (string)row[6], new List<string>()));

            string searchsql = string.Format("select * from SFTANodeRelation where nodeID='{0}'", nodeid);//搜索包含nodeid的行
            cmd.CommandText = searchsql;//设置命令的文本
            cmd.CommandType = CommandType.Text;//设置命令的类型
            cmd.Connection = connection;//设置命令的连接

            result = cmd.ExecuteNonQuery();

            /////查询
            ds = new DataSet();//DataSet是表的集合
            da = new OleDbDataAdapter(searchsql, connection);//从数据库中查询
            da.Fill(ds);//将数据填充到DataSet

            foreach (DataRow relationrow in ds.Tables[0].Rows)
            {
                tni.noderelation.childrenNodesID.Add((string)relationrow[2]);
            }
            connection.Close();
            return tni;
        }

        /// <summary>
        /// 从IPO分析结果列表中拖动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IPOdataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            IPOcopyedrow = this.IPOdataGridView.SelectedRows[0];
            string IPOcopiedid = (string)IPOcopyedrow.Cells[0].Value;

            List<FTATreeNodeInfo> DBIPOFTATree = GenerateChildTreeFromIPODB(IPOcopiedid);
            this.IPOdataGridView.DoDragDrop(DBIPOFTATree, DragDropEffects.Copy);

        }

        //从已有事件数据库IPODB中生成子树

        private List<FTATreeNodeInfo> GenerateChildTreeFromIPODB(string nodeid)
        {
            List<FTATreeNodeInfo> temptree = new List<FTATreeNodeInfo>();
            FTATreeNodeInfo tni = GenerateTreeNodeFromIPODB(nodeid);
            temptree.Add(tni);
            GenerateIPOTree(tni, ref temptree);

            return temptree;
        }

        private void GenerateIPOTree(FTATreeNodeInfo tni, ref List<FTATreeNodeInfo> temptree)
        {
            if (tni.noderelation.childrenNodesID.Count != 0)
            {
                foreach (string childid in tni.noderelation.childrenNodesID)
                {
                    FTATreeNodeInfo childnodeinfo = GenerateTreeNodeFromIPODB(childid);
                    temptree.Add(childnodeinfo);
                    GenerateIPOTree(childnodeinfo, ref temptree);
                }
            }
        }

        private FTATreeNodeInfo GenerateTreeNodeFromIPODB(string nodeid)
        {
            String IPOconnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=SFTAIPODataBase.mdb";
            OleDbConnection IPOconnection = new OleDbConnection(IPOconnectionString);

            OleDbCommand IPOcmd = new OleDbCommand();
            IPOconnection.Open();//打开连接

            string IPOsearchrootsql = string.Format("select * from SFTATreeInfo where nodeID='{0}'", nodeid);//搜索包含nodeid的行

            ////OleDbCommand cmd1 = new OleDbCommand(searchsql1, connection);
            IPOcmd.CommandText = IPOsearchrootsql;//设置命令的文本
            IPOcmd.CommandType = CommandType.Text;//设置命令的类型
            IPOcmd.Connection = IPOconnection;//设置命令的连接

            int result = IPOcmd.ExecuteNonQuery();

            /////查询
            DataSet IPOds = new DataSet();//DataSet是表的集合
            OleDbDataAdapter IPOda = new OleDbDataAdapter(IPOsearchrootsql, IPOconnection);//从数据库中查询
            IPOda.Fill(IPOds);//将数据填充到DataSet

            DataRow row = IPOds.Tables[0].Rows[0];//获取结果

            FTATreeNodeInfo tni;
            if (convertToEventType((string)row[5]) == EventType.Error)
                tni = new FTATreeNodeInfo((string)row[1], 99, new FTAGateNodeData((string)row[2], (string)row[3], convertToGateType((string)row[5]), (string)row[4]), new FTANodeRelation((string)row[1], (string)row[6], new List<string>()));
            else
                tni = new FTATreeNodeInfo((string)row[1], 99, new FTAEventNodeData((string)row[2], (string)row[3], convertToEventType((string)row[5]), (string)row[4], (string)row[8]), new FTANodeRelation((string)row[1], (string)row[6], new List<string>()));

            string IPOsearchsql = string.Format("select * from SFTANodeRelation where nodeID='{0}'", nodeid);//搜索包含nodeid的行
            IPOcmd.CommandText = IPOsearchsql;//设置命令的文本
            IPOcmd.CommandType = CommandType.Text;//设置命令的类型
            IPOcmd.Connection = IPOconnection;//设置命令的连接

            result = IPOcmd.ExecuteNonQuery();

            /////查询
            IPOds = new DataSet();//DataSet是表的集合
            IPOda = new OleDbDataAdapter(IPOsearchsql, IPOconnection);//从数据库中查询
            IPOda.Fill(IPOds);//将数据填充到DataSet

            foreach (DataRow relationrow in IPOds.Tables[0].Rows)
            {
                tni.noderelation.childrenNodesID.Add((string)relationrow[2]);
            }
            IPOconnection.Close();
            return tni;
        }


        private EventType convertToEventType(string type)
        {
            switch(type)
            {
                case "EventBasic":
                    return EventType.EventBasic;
                case "EventConditioning":
                    return EventType.EventConditioning;
                case "EventIn":
                    return EventType.EventIn;
                case "EventInitiating":
                    return EventType.EventInitiating;
                case "EventIntermediate":
                    return EventType.EventIntermediate;
                case "EventOut":
                    return EventType.EventOut;
                case "EventUndeveloped":
                    return EventType.EventUndeveloped;
                default:
                    return EventType.Error;
            }
        }

        private GateType convertToGateType(string type)
        {
            switch (type)
            {
                case "GateAnd":
                    return GateType.GateAnd;
                case "GateElect":
                    return GateType.GateElect;
                case "GateInhibit":
                    return GateType.GateInhibit;
                case "GateOr":
                    return GateType.GateOr;
                case "GatePri":
                    return GateType.GatePri;
                case "GateSequenceAnd":
                    return GateType.GateSequenceAnd;
                case "GateXor":
                    return GateType.GateXor;
                default:
                    return GateType.Error;
            }
        }
    }
}
