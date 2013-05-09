using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Word;

namespace SFTAPlugin.ToWord
{
    class ItemParagraph
    {
        private string font_name = "仿宋";
        public string Font_name
        {
            get { return font_name; }
            set { font_name = value; }
        }

        private int font_size = 12;
        public int Font_size
        {
            get { return font_size; }
            set { font_size = value; }
        }
        private int font_bold = 0;
        public int Font_bold
        {
            get { return font_bold; }
            set { font_bold = value; }
        }
        private WdLineSpacing line_space = WdLineSpacing.wdLineSpace1pt5;
        public WdLineSpacing Line_space
        {
            get { return line_space; }
            set { line_space = value; }
        }
        private WdColor font_color = WdColor.wdColorBlack;
        public WdColor Font_color
        {
            get { return font_color; }
            set { font_color = value; }
        }
        private WdParagraphAlignment alignment = WdParagraphAlignment.wdAlignParagraphLeft;
        public WdParagraphAlignment Alignment
        {
            get { return alignment; }
            set { alignment = value; }
        }

        private int spaceAfter = 6;
        public int SpaceAfter
        {
            get { return spaceAfter; }
            set { spaceAfter = value; }
        }

        private int height = 200;
        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        private int width = 280;
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        private int col = 5;
        public int Col
        {
            get { return col; }
            set { col = value; }
        }
        private int row = 3;
        public int Row
        {
            get { return row; }
            set { row = value; }
        }
    }
    
}
