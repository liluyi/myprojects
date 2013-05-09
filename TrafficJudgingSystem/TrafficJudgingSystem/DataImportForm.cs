using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace TrafficJudgingSystem
{
    public partial class DataImportForm : Form
    {
        public List<string> metriclist = new List<string>();
        public List<int> indexlist = new List<int>();
        public bool hasData = false;
        public RouteInfoList rilist = new RouteInfoList();

        public DataImportForm(List<string> list)
        {
            InitializeComponent();
            this.metriclist = list;
        }

        private void importdatabutton_Click(object sender, EventArgs e)
        {
            //dataGridView1.Columns.Remove(YearColumn);
            //dataGridView1.Columns.Remove(RouteNameColumn);
            //dataGridView1.Columns.Remove(RouteSectionColumn);

            //打开一个文件选择框
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Excel文件";
            ofd.FileName = "";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);//为了获取特定的系统文件夹，可以使用System.Environment类的静态方法GetFolderPath()。该方法接受一个Environment.SpecialFolder枚举，其中可以定义要返回路径的哪个系统目录
            ofd.Filter = "Excel03文件(*.xls)|*.xls|Excel07-10文件(*.xlsx)|*.xlsx";
            ofd.ValidateNames = true;     //文件有效性验证ValidateNames，验证用户输入是否是一个有效的Windows文件名
            ofd.CheckFileExists = true;  //验证路径有效性
            ofd.CheckPathExists = true; //验证文件有效性

            string strName = string.Empty;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                strName = ofd.FileName;
            }
            if (strName == "")
            {
                MessageBox.Show("没有选择Excel文件！无法进行数据导入");
                return;
            }
            rilist.infolist.Clear();
            string connectionString = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", strName);
            string query = String.Format("select * from [{0}$]", "Sheet1");
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connectionString);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);//将Excel中获取的数据存入dataset中
            //dataGridView1.DataSource = dataSet.Tables[0];
            int indexnum = indexlist.Count;//获取列表的项目数量
            string[] items=new string[indexnum];
            dataGridView1.Rows.Clear();
            
            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {
                int i = 0;
                RouteInfo ri = new RouteInfo();
                foreach (int index in indexlist)
                {
                    items[i] = datarow.ItemArray.GetValue(index).ToString();
                    i++;
                }
                ri.Year=Convert.ToInt32(datarow.ItemArray.GetValue(0));
                ri.RouteName=datarow.ItemArray.GetValue(1).ToString();
                string[] temp=datarow.ItemArray.GetValue(2).ToString().Split('-');                
                ri.Source=temp[0];
                ri.Destination=temp[1];
                foreach (RouteInfo routeinfo in Program.routeinfolist.infolist)
                {
                    string sectionname=routeinfo.Source+"-"+routeinfo.Destination;
                    if(sectionname.Equals(datarow.ItemArray.GetValue(2)))
                    {
                        //Program.routeinfolist.infolist.Remove(routeinfo);
                        rilist.AddRouteInfo(ri);
                        dataGridView1.Rows.Add(items);
                    }
                }
                
                hasData = true;
            }
        }

        private void judgebutton_Click(object sender, EventArgs e)
        {
            //this.Hide();
            if (hasData == true)
            {
                //Program.routeinfolist.infolist.Clear();
                foreach (RouteInfo routeinfor in rilist.infolist)
                    Program.finallist.AddRouteInfo(routeinfor);
                JudgeResultForm resultform = new JudgeResultForm();
                resultform.Show();
            }
            else
            {
                MessageBox.Show("请输入或导入数据！");
            }
        }

        private void DataImportForm_Load(object sender, EventArgs e)
        {
            indexlist.Add(0);
            indexlist.Add(1);
            indexlist.Add(2);
            foreach(string name in this.metriclist)//创建目录
            {
                if (name == "checkBox111")
                {
                    dataGridView1.Columns.Add("metric111", "路基本体\n基床\n沉降程度");
                    indexlist.Add(3);
                }
                else if (name == "checkBox112")
                {
                    dataGridView1.Columns.Add("metric112", "路基本体\n基床\n破损程度");
                    indexlist.Add(4);
                }
                else if (name == "checkBox113")
                {
                    dataGridView1.Columns.Add("metric113", "路基本体\n基床\n翻浆冒泥面积");
                    indexlist.Add(5);
                }
                else if (name == "checkBox121")
                {
                    dataGridView1.Columns.Add("metric121", "路基本体\n路肩\n破损程度");
                    indexlist.Add(6);
                }
                else if (name == "checkBox122")
                {
                    dataGridView1.Columns.Add("metric122", "路基本体\n路肩\n平整性");
                    indexlist.Add(7);
                }
                else if (name == "checkBox123")
                {
                    dataGridView1.Columns.Add("metric123", "路基本体\n路肩\n清洁性");
                    indexlist.Add(8);
                }
                else if (name == "checkBox131")
                {
                    dataGridView1.Columns.Add("metric131", "路基本体\n边坡\n破损程度");
                    indexlist.Add(9);
                }
                else if (name == "checkBox132")
                {
                    dataGridView1.Columns.Add("metric132", "路基本体\n边坡\n平整性");
                    indexlist.Add(10);
                }
                else if (name == "checkBox133")
                {
                    dataGridView1.Columns.Add("metric133", "路基本体\n边坡\n裂缝宽度");
                    indexlist.Add(11);
                }
                else if (name == "checkBox21")
                {
                    dataGridView1.Columns.Add("metric21", "路基排水\n破损程度");
                    indexlist.Add(12);
                }
                else if (name == "checkBox22")
                {
                    dataGridView1.Columns.Add("metric22", "路基排水\n排水性能");
                    indexlist.Add(13);
                }
                else if (name == "checkBox23")
                {
                    dataGridView1.Columns.Add("metric23", "路基排水\n清洁性");
                    indexlist.Add(14);
                }
                else if (name == "checkBox31")
                {
                    dataGridView1.Columns.Add("metric31", "路基防护\n沉降程度");
                    indexlist.Add(15);
                }
                else if (name == "checkBox32")
                {
                    dataGridView1.Columns.Add("metric32", "路基防护\n破损程度");
                    indexlist.Add(16);
                }
                else if (name == "checkBox33")
                {
                    dataGridView1.Columns.Add("metric33", "路基防护\n裂缝宽度");
                    indexlist.Add(17);
                }
            }
            foreach (RouteInfo ri in Program.routeinfolist.infolist)
            {
                dataGridView1.Rows.Add(ri.Year, ri.RouteName, ri.Source + "-" + ri.Destination);
                hasData = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //DataImportForm_FormClosed(null,null);
        }

        private void DataImportForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
