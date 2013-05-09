using System;
using WeifenLuo.WinFormsUI.Docking;
namespace Platform.Core.UI
{
    public interface UUID
    {
        string UUID { get; set; }
    }

    public class BaseForm : DockContent
    {

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BaseForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IsMdiContainer = true;
            this.Name = "BaseForm";
            this.ResumeLayout(false);

        }
    }
}