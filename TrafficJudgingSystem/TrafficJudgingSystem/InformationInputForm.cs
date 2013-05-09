using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TrafficJudgingSystem
{
    public partial class InformationInputForm : Form
    {
        RouteInfoList routeinfolist = new RouteInfoList();
        public InformationInputForm()
        {
            InitializeComponent();
            this.routeinfolist = Program.routeinfolist;
        }

        private void addbutton_Click(object sender, EventArgs e)
        {
            RouteInfo routeinfo = new RouteInfo();
            if (this.yeartextBox.Text == string.Empty || routenametextBox.Text == string.Empty || routetypetextBox.Text == string.Empty ||  srctextBox.Text == string.Empty || dsttextBox.Text == string.Empty )
                MessageBox.Show("信息不完全！");
            else
            {
                routeinfo.Year = Convert.ToInt32(this.yeartextBox.Text);
                routeinfo.RouteName = this.routenametextBox.Text;
                routeinfo.RouteType = this.routetypetextBox.Text;
                routeinfo.Source = this.srctextBox.Text;
                routeinfo.Destination = this.dsttextBox.Text;
                routeinfolist.AddRouteInfo(routeinfo);
                MessageBox.Show("信息已添加！");
            }
        }


        private void inputTabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
                dataGridView1.Rows.Clear();
            foreach (RouteInfo ri in routeinfolist.infolist)
            {
                dataGridView1.Rows.Add(ri.RouteName, ri.RouteType, ri.Source, ri.Destination,  ri.Year);

            }
            if (dataGridView1.Rows.Count != 0)
                dataGridView1.Rows[0].Selected = true;
        }

        private void nextbutton_Click(object sender, EventArgs e)
        {
            //EvaluateForm evaluateform = new EvaluateForm();
            //evaluateform.Show();
            JudgementForm judgementform = new JudgementForm();
            judgementform.Show();
            Program.routeinfolist = this.routeinfolist;
        }

        private void firstinfobutton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                row.Selected = false;
            dataGridView1.Rows[0].Selected = true;
        }

        private void lastinfobutton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                row.Selected = false;
            dataGridView1.Rows[dataGridView1.Rows.Count-1].Selected = true;
        }

        private void upbutton_Click(object sender, EventArgs e)
        {
            int currentpointer = 0;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                row.Selected = false;
                currentpointer = row.Index;
            }
            if(currentpointer>0)
                dataGridView1.Rows[currentpointer - 1].Selected = true;
        }

        private void downbutton_Click(object sender, EventArgs e)
        {
            int currentpointer = 0;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                row.Selected = false;
                currentpointer = row.Index;
            }
            if (currentpointer !=dataGridView1.Rows.Count-1)
                dataGridView1.Rows[currentpointer + 1].Selected = true;
        }
    }
}
