using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Platform.Core.UI;

namespace SFTAPlugin
{
    public partial class SFTAServerViewForm:BaseForm
    {
        public SFTAServerViewForm()
        {
            InitializeComponent();
            //DataTable dt = new DataTable();
            //dt.Columns.Add(new DataColumn("Value", typeof(int)));
            //dt.Columns.Add(new DataColumn("Text", typeof(string)));
            //DataRow dr = dt.NewRow();
            //dr["Value"] = 1;
            //dr["Text"] = "Text1";
            //dt.Rows.Add(dr);
            //this.dataGridView1.DataSource = dt;
        }
    }
}
