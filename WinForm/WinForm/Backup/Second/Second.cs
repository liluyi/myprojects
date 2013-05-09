using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using log4net;
using Platform.Core;
using Platform.Core.Data;
using Platform.Core.Services;
using Platform.Core.UI;
using WeifenLuo.WinFormsUI.Docking;

namespace Second
{
    public class SecondPlugin : IPlugin
    {
        #region IPlugin Members

        public AbstractProject GetDefaultProject()
        {
            return new SecondProject();
        }

        public AbstractProject GetProjectFromPath(string path)
        {
            return new SecondProject();
        }

        public string MutableResourceClassFullName
        {
            get
            {
                return resourceFullName;
            }
            set
            {
                resourceFullName = value;
            }
        }

        public ToolStripMenuItem[] PluginMenus
        {
            get { return Menus; }
        }

        public ToolStrip[] PluginTools
        {
            get { return Tools; }
        }

        public string ProjectClassFullName
        {
            get
            {
                return projectName;
            }
            set
            {
                projectName = value;
            }
        }

        public string ProjectSuffix
        {
            get
            {
                return pSuffix;
            }
            set
            {
                pSuffix = value;
            }
        }

        public string Token
        {
            get
            {
                return token;
            }
            set
            {
                token = value;
            }
        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem[] Menus;//声明菜单资源。

        private System.Windows.Forms.ToolStrip[] Tools;//声明工具栏资源。

        private string token;//声明“token”变量。

        private string resourceFullName; //声明变量“resourceFullName”。

        private string projectName;//声明变量“projectName”。

        private string pSuffix;//声明变量“projectSuffix”。

        public SecondPlugin()
        {
            //构造插件的具体菜单资源和工具资源。

            //构造菜单资源。

            Menus = new ToolStripMenuItem[2];

            for (int i = 0; i < 2; i++)
            {
                Menus[i] = new ToolStripMenuItem("SecondMenus_" + i.ToString());

                for (int j = 0; j < 3; j++)
                {
                    Menus[i].DropDownItems.Add("SubMenus_" + i.ToString() + "_" + j.ToString());
                }
            }

            //构造工具栏。
            Tools = new ToolStrip[1];

            Tools[0] = new ToolStrip();

            Tools[0].Items.Add(Resource._new);

            Tools[0].Items[0].ToolTipText = "新增";

            Tools[0].Items[0].Click += new EventHandler(Tool_new_Click);           

            Tools[0].Items.Add(Resource.open);

            Tools[0].Items[1].ToolTipText = "打开";

            Tools[0].Items[1].Click += new EventHandler(Tool_open_Click);          

            Tools[0].Items.Add(Resource.save);

            Tools[0].Items[2].ToolTipText = "保存";

            Tools[0].Items[2].Click += new EventHandler(Tool_save_Click);

            Tools[0].Items.Add(Resource.cut);

            Tools[0].Items[3].ToolTipText = "剪切";

            Tools[0].Items[3].Click += new EventHandler(Tool_cut_Click);

            Tools[0].Items.Add(Resource.copy);

            Tools[0].Items[4].ToolTipText = "复制";

            Tools[0].Items[4].Click += new EventHandler(Tool_copy_Click);

            Tools[0].Items.Add(Resource.paste);

            Tools[0].Items[5].ToolTipText = "粘贴";

            Tools[0].Items[5].Click += new EventHandler(Tool_paste_Click);

            Tools[0].Items.Add(Resource.redo);

            Tools[0].Items[6].ToolTipText = "恢复";

            Tools[0].Items[6].Click +=new EventHandler(Tool_redo_Click);

            Tools[0].Items.Add(Resource.undo);

            Tools[0].Items[7].ToolTipText = "反恢复";

            Tools[0].Items[7].Click += new EventHandler(Tool_undo_Click);
        }
        
        private void Tool_new_Click(object sender, EventArgs e)
        {
            MessageBox.Show("这是新增工具");
        }
        
        private void Tool_open_Click(object sender, EventArgs e)
        {
            MessageBox.Show("这是打开工具");
        }

        private void Tool_save_Click(object sender, EventArgs e)
        {
            MessageBox.Show("这是保存工具");
        }

        private void Tool_cut_Click(object sender, EventArgs e)
        {
            MessageBox.Show("这是剪切工具");
        }

        private void Tool_copy_Click(object sender, EventArgs e)
        {
            MessageBox.Show("这是复制工具");
        }

        private void Tool_paste_Click(object sender, EventArgs e)
        {
            MessageBox.Show("这是粘贴工具");
        }

        private void Tool_redo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("这是恢复工具");
        }

        private void Tool_undo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("这是反恢复工具");
        }
    }

    public class SecondMutableReource : MutableResource
    {
        public override BaseForm InfoForm
        {
            get
            {
                return myInfoView;
            }
            set
            {
                myInfoView = value;
            }
        }

        public override BaseForm TabForm
        {
            get
            {
                return myTabView;
            }
            set
            {
                myTabView = value;
            }
        }

        public override BaseForm ViewForm
        {
            get
            {
                return myTreeView;
            }
            set
            {
                myTreeView = value;
            }
        }

        private BaseForm myInfoView = new SecondInfoView();

        private BaseForm myTabView = new SecondTabView();

        private BaseForm myTreeView = new SecondTreeView();
    }


    public class SecondProject : AbstractProject
    {
        private string suffix;

        public override string Suffix
        {
            get { return suffix; }
        }

        public SecondProject()
        {
            suffix = "second";
        }
    }

}
