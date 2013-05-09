using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Collections;

namespace TrafficJudgingSystem
{
    public partial class JudgeResultForm : Form
    {
        public List<JudgeInfo> judgeinfolist = new List<JudgeInfo>();
        public JudgeResultForm()
        {
            InitializeComponent();
        }

        private void exportbutton_Click(object sender, EventArgs e)
        {
            //导出到execl
            try
            {
                //没有数据的话就不往下执行
                if (dataGridView1.Rows.Count == 0)
                    return;
                //实例化一个Excel.Application对象
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

                //让后台执行设置为不可见，为true的话会看到打开一个Excel，然后数据在往里写
                excel.Visible = false;

                //新增加一个工作簿，Workbook是直接保存，不会弹出保存对话框，加上Application会弹出保存对话框，值为false会报错
                excel.Application.Workbooks.Add(true);
                //生成Excel中列头名称
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    excel.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
                }
                //把DataGridView当前页的数据保存在Excel中
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        if (dataGridView1[j, i].ValueType == typeof(string))
                        {
                            excel.Cells[i + 2, j + 1] = "'" + dataGridView1[j, i].Value.ToString();
                        }
                        else
                        {
                            excel.Cells[i + 2, j + 1] = dataGridView1[j, i].Value.ToString();
                        }
                    }
                }

                //设置禁止弹出保存和覆盖的询问提示框
                excel.DisplayAlerts = false;
                excel.AlertBeforeOverwriting = false;

                //保存工作簿
                excel.Application.Workbooks.Add(true).Save();
                //保存excel文件
                excel.Save("F:" + "\\export.xls");

                //确保Excel进程关闭
                excel.Quit();
                excel = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误提示");
            }
        }

        private void JudgeResultForm_Load(object sender, EventArgs e)
        {
            Hashtable hashtable = new Hashtable();
            double j = 0;
            int j0 = 1;
            if (Program.finallist.infolist.Count == 0)
            {
                foreach (RouteInfo ri in Program.routeinfolist.infolist)
                    Program.finallist.AddRouteInfo(ri);
            }

            foreach (RouteInfo ri in Program.finallist.infolist)
            {
                string judgeresult=string.Empty;

                hashtable.Add(j, j);
                while (hashtable.ContainsValue(j) || j == 0)
                {
                    j = new Random().NextDouble() / 2 + 1.3;
                }
                if (j > 1.5 || j == 1.5)
                    j0 = 2;
                else if (j < 1.5)
                    j0 = 1;
                if (j0 == 1)
                    judgeresult = "路基存在轻微病害，应进行日常养护维修";
                else if (j0 == 2)
                    judgeresult = "路基存在中等病害，应进行日常养护维修，并加强检查";
                else if (j0 == 3)
                    judgeresult = "路基存在较重病害，应进行中修，有些病害需加强观测并根据其变化情况采取相应的措施";
                else if (j0 == 4)
                    judgeresult = "路基存在严重病害，应进行中修，个别病害需进行大修";
                else if (j0 == 5)
                    judgeresult = "路基存在极严重病害，应立即进行中修或大修";
                dataGridView1.Rows.Add(ri.Year, ri.RouteName, ri.Source + "-" + ri.Destination,j0,j.ToString("#0.000"),judgeresult);
                judgeinfolist.Add(new JudgeInfo(ri.Source + "-" + ri.Destination,ri.Year,j,j0,judgeresult));
            }
        }

        private void mapbutton_Click(object sender, EventArgs e)
        {
            MapDisplayForm mapform = new MapDisplayForm(judgeinfolist);
            mapform.Show();
        }
    }
}
