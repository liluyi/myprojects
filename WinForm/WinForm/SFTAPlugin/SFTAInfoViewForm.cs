using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Platform.Core.UI;

namespace SFTAPlugin
{
    public delegate void SelectedSetChangeHandler();
    public delegate void NaviChangeHandler(FTATreeNodeInfo tni);

    public partial class SFTAInfoViewForm : BaseForm
    {
        private string treeid = string.Empty;
        private FTATreeInfo treeinfocollection = new FTATreeInfo();
        public SelectedSetChangeHandler SelectedSetChange;
        public NaviChangeHandler NaviChange;
        private FTATreeNodeInfo previousnode;

        public SFTAInfoViewForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 双击某一最小割集在图中标色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minicutdataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (FTATreeNodeInfo tni in this.treeinfocollection.ftatreenodeinfolist)//清空原有节点的从属标记
            {
                if (tni.nodedata is FTAEventNodeData)
                    ((FTAEventNodeData)tni.nodedata).belongtoMiniCut = false;
            }
            DataGridViewRow selectedrow = this.minicutdataGridView.SelectedRows[0];
            int key = Convert.ToInt32(selectedrow.Cells[2].Value);
            List<FTATreeNodeInfo> selectedset = new List<FTATreeNodeInfo>();
            this.treeinfocollection.cutsetdic.TryGetValue(key, out selectedset);
            if (selectedset != null)
            {
                foreach (FTATreeNodeInfo tni in selectedset)
                    ((FTAEventNodeData)tni.nodedata).belongtoMiniCut = true;
                previousnode = selectedset.ElementAt<FTATreeNodeInfo>(0);
            }
            
            if (SelectedSetChange != null)
                SelectedSetChange();
            if (NaviChange != null)
                NaviChange(previousnode);
        }
        public void generateMinimalCut(string treeid,FTATreeInfo tree)
        {
            this.treeid = treeid;
            this.treeinfocollection = tree;
            this.outputRichTextBox.Text += "正在生成最小割集……\r\n";
            if (this.treeinfocollection.AutoCheck() == true)//对故障树进行完整性校验
            {
                this.outputRichTextBox.Text += "故障树完整性校验成功！\r\n";
                Dictionary<int, List<FTATreeNodeInfo>> cutsetdic = treeinfocollection.GenerateMinimumCutSet();
                this.minicutdataGridView.Visible = true;
                this.copyminicutset.Visible = true;
                this.minicutdataGridView.Rows.Clear();
                int key = 1;
                foreach (KeyValuePair<int, List<FTATreeNodeInfo>> pair in cutsetdic)
                {
                    
                    string namecollection = string.Empty;
                    foreach (FTATreeNodeInfo tni in pair.Value)
                    {
                        if(tni.hasNotGate==false)
                            namecollection += tni.nodedata.nodeName + "   ";//后期更改为nodeName
                        else
                            namecollection += "（非）"+tni.nodedata.nodeName + "   ";//后期更改为nodeName
                    }

                    this.minicutdataGridView.Rows.Add(key.ToString(), namecollection, pair.Key.ToString());
                    key++;
                }

                this.outputRichTextBox.Text += "最小割集生成成功！\r\n";
                this.tabControl1.SelectedIndex = 1;//跳转至第2个table页，显示最小割集

                //mcs = (MinimumCutSetForm)ServicesManager.ServicesManagerSingleton.UIService.GetUserForm(null, new UserUIEventArgs(this.Name + "minimumcut"));
                //if (mcs == null)
                //{
                //    mcs = new MinimumCutSetForm(cutsetdic);//隶属于本树的最小割集显示面板
                //    mcs.Name = this.Name + "minimumcut";
                //    ServicesManager.ServicesManagerSingleton.UIService.AddMutableResourceSelf(mcs, new UserUIEventArgs(mcs.Name, new FormLoc("InfoViewForm", "InfoViewForm")));
                //}
                //else
                //{
                //    mcs.RefreshForm(cutsetdic);
                //    mcs.Show();
                //}
                //clicknum++;
            }
            else
            {
                this.outputRichTextBox.Text += ("警告：故障树绘制尚未完成！请完成故障树绘制再进行最小割集计算！\r\n");
                this.tabControl1.SelectedIndex = 0;//跳转至第1个table页，显示警告信息
                this.minicutdataGridView.Visible =false;
                this.copyminicutset.Visible = false;
                this.minicutdataGridView.Rows.Clear();
            }
        }

        private void copyminicutset_Click(object sender, EventArgs e)
        {
            if (this.minicutdataGridView.Visible == true)
            {
                string settext = string.Empty;
                int index = 1;
                foreach (DataGridViewRow row in this.minicutdataGridView.Rows)
                {
                    settext += "最小割集" + index.ToString() + "：" + row.Cells[1].Value + "\r\n";
                    index++;
                }
                Clipboard.SetDataObject(settext);
            }
        }

        private void minicutdataGridView_Click(object sender, EventArgs e)
        {
            
        }

        private void minicutdataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3&&previousnode!=null)//若单击的为导航按钮
            {
                int key = Convert.ToInt32(this.minicutdataGridView.Rows[e.RowIndex].Cells[2].Value);//获取该行的集合id
                List<FTATreeNodeInfo> selectedset = new List<FTATreeNodeInfo>();
                this.treeinfocollection.cutsetdic.TryGetValue(key, out selectedset);//获取该行的集合
                if (selectedset != null)
                {
                    if(((FTAEventNodeData)previousnode.nodedata).belongtoMiniCut == true)//看刚才是否已经双击了该集合，即是否已经标色
                    {
                        int preindex=selectedset.IndexOf(previousnode);
                        if(preindex==selectedset.Count-1)
                            previousnode = selectedset.ElementAt<FTATreeNodeInfo>(0);
                        else
                            previousnode = selectedset.ElementAt<FTATreeNodeInfo>(preindex + 1);
                        if (NaviChange != null)
                            NaviChange(previousnode);
                    }
                }
                
            }
        }
    }
}
