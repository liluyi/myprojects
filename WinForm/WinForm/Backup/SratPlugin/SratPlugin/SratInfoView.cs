using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Platform.Core.Data;
using Platform.Core.UI;
using WeifenLuo.WinFormsUI.Docking;
using Platform.Core;
using Platform.Core.Services;

namespace SratPlugin
{
    public partial class SratInfoView : BaseForm
    {
        public SratInfoView()
        {
            InitializeComponent();
        }

        public string FormOwnerProp { get; set; }
    }
}
