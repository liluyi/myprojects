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
    public partial class MinimumCutSetForm : BaseForm
    {
        private Dictionary<int,List<FTATreeNodeInfo>> cutsetdic=new Dictionary<int,List<FTATreeNodeInfo>>();
        public MinimumCutSetForm(Dictionary<int,List<FTATreeNodeInfo>> cutsetdic)
        {
            InitializeComponent();
            this.cutsetdic=cutsetdic;
        }
        public MinimumCutSetForm()
        {
            InitializeComponent();
        }

        private void MinimumCutSetForm_Load(object sender, EventArgs e)
        {
            this.RefreshForm(this.cutsetdic);//刷新label，显示最小割集
        }

        public void RefreshForm(Dictionary<int, List<FTATreeNodeInfo>> cutsetdic)
        {
            this.cutsetdic = cutsetdic;
            label1.Text = String.Empty;//将label上原有数据清除
            foreach (KeyValuePair<int, List<FTATreeNodeInfo>> pair in cutsetdic)
            {
                label1.Text += pair.Key.ToString() + ":{";
                foreach (FTATreeNodeInfo tni in pair.Value)
                    label1.Text += tni.nodedata.nodeName + ", ";//后期更改为nodeName
                label1.Text.Remove(label1.Text.Length - 2);
                label1.Text += "}\n";
                label1.Refresh();
            }
        }
    }
}
