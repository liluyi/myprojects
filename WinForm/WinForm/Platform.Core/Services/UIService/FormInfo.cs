using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using WeifenLuo.WinFormsUI.Docking;
using Platform.Core.UI;
using Platform.Core.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Platform.Core.Services
{
    [Serializable]
    //窗体信息类
    public class FormInfo
    {        
        private string formtext=String.Empty;
        private string formid=string.Empty;
        private FormLoc loc;
        private Type type;

        public FormInfo()
        {
        }

        public FormInfo(string formid, string formtext, FormLoc loc,Type type)
        {
            this.formid = formid;
            this.formtext = formtext;
            this.loc = loc;
            this.type = type;
        }
        public FormInfo(string formid, string formtext, Type type)
        {
            this.formid = formid;
            this.formtext = formtext;
            this.type = type;
        }

        public string FormID
        {
            get { return this.formid; }
            set { this.formid = value; }
        }
        public string FormText
        {
            get { return this.formtext; }
            set { this.formtext = value; }
        }
        public FormLoc FormLocation
        {
            get { return this.loc; }
            set { this.loc = value; }
        }
        public Type FormType
        {
            get { return this.type; }
            set { this.type = value; }
        }
    }

    [Serializable]
    //窗体位置类
    public class FormLoc
    {        
        //窗体位置
        private DockState dockstate=DockState.Unknown;
        //停靠窗体名称
        private string previouspanename=String.Empty;
        //停靠方法
        private DockAlignment alignment=DockAlignment.Bottom;
        //停靠比例
        private double proportion=0;
        //停靠位置的窗体名称
        private string beforepanename = String.Empty;

        //默认构造方法
        public FormLoc()
        { }

        //
        public FormLoc(DockState dockstate)
        {
            this.dockstate = dockstate;
        }
        //
        public FormLoc(string previouspanename, DockAlignment alignment, double proportion)
        {
            this.previouspanename = previouspanename;
            this.alignment = alignment;
            this.proportion = proportion;
        }
        //
        public FormLoc(string previouspanename, string beforepanename)
        {
            this.previouspanename = previouspanename;
            this.beforepanename = beforepanename;
        }
        public DockState State
        {
            get{return this.dockstate;}
            set{this.dockstate=value;}
        }
        public string PreviousPaneName
        {
            get{return this.previouspanename;}
            set{this.previouspanename=value;}
        }
        public string BeforePaneName
        {
            get { return this.beforepanename; }
            set { this.beforepanename = value; }
        }
        public DockAlignment Alignment
        {
            get{return this.alignment;}
            set{this.alignment=value;}
        }
        public double Proportion
        {
            get { return this.proportion; }
            set{this.proportion=value;}
        }
    }
}
