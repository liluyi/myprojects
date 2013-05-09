using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Platform.Core;

namespace SFTAPlugin
{
    public partial class SettingForm : Form
    {
        public Color color1=Color.Yellow, color2=Color.Red, color3=Color.AliceBlue,color4 = Color.Salmon,color5=Color.SpringGreen;

        public SettingForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void marklabel_Click(object sender, EventArgs e)
        {
            ColorDialog ColorPickDialog = new ColorDialog();
            ColorPickDialog.AllowFullOpen = false;//禁止自定义颜色
            ColorPickDialog.ShowHelp = true;
            ColorPickDialog.Color = this.marklabel.BackColor;//默认颜色

            //点击确认时更改标签颜色
            if (ColorPickDialog.ShowDialog() == DialogResult.OK)
                this.marklabel.BackColor = ColorPickDialog.Color;
            color1 = this.marklabel.BackColor;
        }

        private void unfinishedlabel_Click(object sender, EventArgs e)
        {
            ColorDialog ColorPickDialog = new ColorDialog();
            ColorPickDialog.AllowFullOpen = false;//禁止自定义颜色
            ColorPickDialog.ShowHelp = true;
            ColorPickDialog.Color = this.unfinishedlabel.BackColor;//默认颜色

            //点击确认时更改标签颜色
            if (ColorPickDialog.ShowDialog() == DialogResult.OK)
                this.unfinishedlabel.BackColor = ColorPickDialog.Color;
            color2 = this.unfinishedlabel.BackColor;
        }

        private void normallabel_Click(object sender, EventArgs e)
        {
            ColorDialog ColorPickDialog = new ColorDialog();
            ColorPickDialog.AllowFullOpen = false;//禁止自定义颜色
            ColorPickDialog.ShowHelp = true;
            ColorPickDialog.Color = this.normallabel.BackColor;//默认颜色

            //点击确认时更改标签颜色
            if (ColorPickDialog.ShowDialog() == DialogResult.OK)
                this.normallabel.BackColor = ColorPickDialog.Color;
            color3 = this.normallabel.BackColor;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            ColorDialog ColorPickDialog = new ColorDialog();
            ColorPickDialog.AllowFullOpen = false;//禁止自定义颜色
            ColorPickDialog.ShowHelp = true;
            ColorPickDialog.Color = this.label7.BackColor;//默认颜色

            //点击确认时更改标签颜色
            if (ColorPickDialog.ShowDialog() == DialogResult.OK)
                this.label7.BackColor = ColorPickDialog.Color;
            color4 = this.label7.BackColor;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            ColorDialog ColorPickDialog = new ColorDialog();
            ColorPickDialog.AllowFullOpen = false;//禁止自定义颜色
            ColorPickDialog.ShowHelp = true;
            ColorPickDialog.Color = this.label7.BackColor;//默认颜色

            //点击确认时更改标签颜色
            if (ColorPickDialog.ShowDialog() == DialogResult.OK)
                this.label7.BackColor = ColorPickDialog.Color;
            color4 = this.label7.BackColor;
        }

        private void label8_Click(object sender, EventArgs e)
        {
            ColorDialog ColorPickDialog = new ColorDialog();
            ColorPickDialog.AllowFullOpen = false;//禁止自定义颜色
            ColorPickDialog.ShowHelp = true;
            ColorPickDialog.Color = this.label8.BackColor;//默认颜色

            //点击确认时更改标签颜色
            if (ColorPickDialog.ShowDialog() == DialogResult.OK)
                this.label8.BackColor = ColorPickDialog.Color;
            color5 = this.label8.BackColor;
        }
    }
}
