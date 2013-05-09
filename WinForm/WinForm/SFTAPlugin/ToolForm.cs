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
    public partial class ToolForm :BaseForm
    {
        public ToolForm()
        {
            InitializeComponent();
        }

        private void pictureBox_eventbasic_MouseDown(object sender, MouseEventArgs e)
        {
            this.pictureBox_eventbasic.DoDragDrop(this.pictureBox_eventbasic, DragDropEffects.Copy);
        }

        private void pictureBox_eventintermediate_MouseDown(object sender, MouseEventArgs e)
        {
            this.pictureBox_eventbasic.DoDragDrop(this.pictureBox_eventintermediate, DragDropEffects.Copy);
        }

        private void pictureBox_eventundeveloped_MouseDown(object sender, MouseEventArgs e)
        {
            this.pictureBox_eventbasic.DoDragDrop(this.pictureBox_eventundeveloped, DragDropEffects.Copy);
        }

        private void pictureBox_eventconditioning_MouseDown(object sender, MouseEventArgs e)
        {
            this.pictureBox_eventbasic.DoDragDrop(this.pictureBox_eventconditioning, DragDropEffects.Copy);
        }

        private void pictureBox_eventnormal_MouseDown(object sender, MouseEventArgs e)
        {
            this.pictureBox_eventbasic.DoDragDrop(this.pictureBox_eventnormal, DragDropEffects.Copy);
        }

        private void pictureBox_eventout_MouseDown(object sender, MouseEventArgs e)
        {
            this.pictureBox_eventbasic.DoDragDrop(this.pictureBox_eventout, DragDropEffects.Copy);
        }

        private void pictureBox_eventin_MouseDown(object sender, MouseEventArgs e)
        {
            this.pictureBox_eventbasic.DoDragDrop(this.pictureBox_eventin, DragDropEffects.Copy);
        }

        private void pictureBox_gateand_MouseDown(object sender, MouseEventArgs e)
        {
            this.pictureBox_eventbasic.DoDragDrop(this.pictureBox_gateand, DragDropEffects.Copy);
        }

        private void pictureBox_gateor_MouseDown(object sender, MouseEventArgs e)
        {
            this.pictureBox_eventbasic.DoDragDrop(this.pictureBox_gateor, DragDropEffects.Copy);
        }

        private void pictureBox_gateelect_MouseDown(object sender, MouseEventArgs e)
        {
            this.pictureBox_eventbasic.DoDragDrop(this.pictureBox_gateelect, DragDropEffects.Copy);
        }

        private void pictureBox_gatexor_MouseDown(object sender, MouseEventArgs e)
        {
            this.pictureBox_eventbasic.DoDragDrop(this.pictureBox_gatexor, DragDropEffects.Copy);
        }

        private void pictureBox_gatepri_MouseDown(object sender, MouseEventArgs e)
        {
            this.pictureBox_eventbasic.DoDragDrop(this.pictureBox_gatepri, DragDropEffects.Copy);
        }

        private void pictureBox_gateinhibit_MouseDown(object sender, MouseEventArgs e)
        {
            this.pictureBox_eventbasic.DoDragDrop(this.pictureBox_gateinhibit, DragDropEffects.Copy);
        }

        private void pictureBox_gatesequenceand_MouseDown(object sender, MouseEventArgs e)
        {
            this.pictureBox_eventbasic.DoDragDrop(this.pictureBox_gatesequenceand, DragDropEffects.Copy);
        }


    }
}
