using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSWord = Microsoft.Office.Interop.Word;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Core;

namespace SFTAPlugin.ToWord
{
    class DoWord
    {
        private static MSWord._Application word_app;
        //private static Microsoft.Office.Interop.Word.Application word_app;
        private static MSWord._Document word_doc;
        //private static Microsoft.Office.Interop.Word.Document word_doc;
        private static object oNothing = Missing.Value;
        private static object oEndOfDoc = "\\endofdoc";
        private string app_path = @"D:\";
        private string file_path = null;
        private object format = MSWord.WdSaveFormat.wdFormatDocumentDefault;
        private static int count = 0;

        public DoWord()
        {
            //word_app = new MSWord.ApplicationClass();
            word_app=new Microsoft.Office.Interop.Word.Application();
            word_app.Visible = true;
            word_doc = word_app.Documents.Add(ref oNothing, ref oNothing, ref oNothing, ref oNothing);
            word_doc.Activate();
        }

        public void CreateWordDoc(string filepath)
        {
            file_path = filepath;
            if (File.Exists(file_path))
            {
                try
                {
                    File.Delete(file_path);
                }
                catch (Exception)
                {

                }
            }
        }

        public bool doInsert(string value, int type, ItemParagraph item)
        {
            if (item == null)
            {
                item = new ItemParagraph();
            }
            switch (type)
            {
                case HONG.TYPE_TITLE_LEVEL1:
                    count++;
                    doInsertParagraph(value, item);
                    break;
                case HONG.TYPE_TITLE_LEVEL2:
                    count++;
                    doInsertParagraph(value, item);
                    break;
                case HONG.TYPE_TITLE_LEVEL3:
                    count++;
                    doInsertParagraph(value, item);
                    break;
                case HONG.TYPE_TITLE_LEVEL4:
                    count++;
                    doInsertParagraph(value, item);
                    break;
                case HONG.TYPE_VALUE:
                    count++;
                    value = value.Insert(0, "      ");
                    doInsertParagraph(value, item);
                    break;
                case HONG.TYPE_PICTURE:
                    doInsertPicture(value, item);
                    break;
                case HONG.TYPE_TABLE:
                    count += item.Col * item.Row ;
                    doInsertTable(item);
                    break;
            }
            return true;
        }

        private bool doInsertParagraph(string value, ItemParagraph item)
        {
            MSWord.Paragraph myparagraph1;
            myparagraph1 = word_doc.Content.Paragraphs.Add(ref oNothing);
            myparagraph1.Range.Text = value;
            myparagraph1.Range.Font.Name = item.Font_name;
            myparagraph1.Range.Font.Bold = item.Font_bold;
            myparagraph1.Range.Font.Size = item.Font_size;            
            myparagraph1.Format.SpaceAfter = item.SpaceAfter;
            myparagraph1.Range.InsertParagraphAfter();
            myparagraph1.Alignment = item.Alignment;
            return true;
        }

        private bool doInsertPicture(string filepath, ItemParagraph item)
        {
            string FileName = filepath;
            object LinkToFile = false;
            object SaveWithDocument = true;
            object myrange;
            myrange = word_doc.Paragraphs[count].Range;
            word_doc.Application.ActiveDocument.InlineShapes.AddPicture(FileName, ref  LinkToFile, ref  SaveWithDocument, ref myrange);
            word_doc.Application.ActiveDocument.InlineShapes[1].Width = item.Width;//图片宽度 
            word_doc.Application.ActiveDocument.InlineShapes[1].Height = item.Height;//图片高度  
            //将图片设置为四周环绕型    
            Microsoft.Office.Interop.Word.Shape s = word_doc.Application.ActiveDocument.InlineShapes[1].ConvertToShape();
            s.WrapFormat.Type = WdWrapType.wdWrapInline;
            return true;
        }

        public bool doInsertTable(ItemParagraph item)
        {
            MSWord.Table mytable;
            if (count > word_doc.Paragraphs.Count)
            {
                count = word_doc.Paragraphs.Count;
            }
            object myrange = word_doc.Paragraphs[count].Range;
            mytable = word_doc.Tables.Add((MSWord.Range)myrange, item.Row, item.Col, ref oNothing, ref oNothing);
            mytable.Range.ParagraphFormat.SpaceAfter = item.SpaceAfter;

            for (int i = 1; i <= mytable.Rows.Count; i++)
            {
                for (int k = 1; k <= mytable.Columns.Count; k++)
                {
                    mytable.Cell(i, k).Range.Text = i.ToString() + "  " + k.ToString();
                }
            }
            mytable.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
            mytable.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
            mytable.Borders.Enable = 1;
            //给表格表头加斜线
            mytable.Cell(1, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderDiagonalDown].Visible = true;
            mytable.Cell(1, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderDiagonalDown].Color = Microsoft.Office.Interop.Word.WdColor.wdColorBlack;
            mytable.Cell(1, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderDiagonalDown].LineWidth = Microsoft.Office.Interop.Word.WdLineWidth.wdLineWidth050pt;
            count = word_doc.Paragraphs.Count - 1;
            return true;
        }

        public void AddSimpleHeader(string HeaderText, ItemParagraph item)
        {
            if (item == null)
            {
                item = new ItemParagraph();
            }
            //添加页眉    
            word_app.ActiveWindow.View.Type = WdViewType.wdOutlineView;
            word_app.ActiveWindow.View.SeekView = WdSeekView.wdSeekPrimaryHeader;
            word_app.ActiveWindow.ActivePane.Selection.InsertAfter(HeaderText);
            word_app.Selection.Font.Name = item.Font_name;//设置字体格式
            word_app.Selection.Font.Color = item.Font_color;//设置字体颜色    
            word_app.Selection.Font.Size = 10;//设置字体大小    
            word_app.Selection.Font.Bold = item.Font_bold;//设置字体粗细
            word_app.Selection.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;//设置对齐方式    
            word_app.ActiveWindow.View.SeekView = WdSeekView.wdSeekMainDocument;
        }

        public void AddPages(string Pages, ItemParagraph item)
        {
            if (item == null)
            {
                item = new ItemParagraph();
            }
            Microsoft.Office.Interop.Word.PageNumbers wpages = word_app.Selection.Sections[1].Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterEvenPages].PageNumbers;
            wpages.NumberStyle = Microsoft.Office.Interop.Word.WdPageNumberStyle.wdPageNumberStyleNumberInDash;
            wpages.HeadingLevelForChapter = 0;
            wpages.IncludeChapterNumber = false;
            wpages.ChapterPageSeparator = Microsoft.Office.Interop.Word.WdSeparatorType.wdSeparatorHyphen;
            wpages.RestartNumberingAtSection = false;
            wpages.StartingNumber = 0;
            object pagenmbetal = Microsoft.Office.Interop.Word.WdPageNumberAlignment.wdAlignPageNumberCenter;
            object first = true;
            word_app.Selection.Sections[1].Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterEvenPages].PageNumbers.Add(ref pagenmbetal, ref first);
        }
        public void InsertPageBreak(string Break, ItemParagraph item)
        {
            if (item == null)
            {
                item = new ItemParagraph();
            }
            Microsoft.Office.Interop.Word.Range wordRange = word_doc.Bookmarks.get_Item(oEndOfDoc).Range;//获取当前文档的末尾位置
            object oCollapseEnd = Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd;
            object oPageBreak = Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak;//分页符             
            wordRange.Collapse(ref oCollapseEnd);
            wordRange.InsertBreak(ref oPageBreak);//插入分页符 
            wordRange.Collapse(ref oCollapseEnd);
        }
        public void Save()
        {
            //object filepath = (object)file_path;
            //word_doc.SaveAs2(ref filepath, ref format, ref oNothing, ref oNothing, ref oNothing, ref oNothing, ref oNothing, ref oNothing, ref oNothing, ref oNothing, ref oNothing, ref oNothing, ref oNothing, ref oNothing, ref oNothing, ref oNothing);
            word_doc.Save();
            if (word_doc != null)
            {
                word_doc.Close(ref oNothing, ref oNothing, ref oNothing);
            }
            if (word_app != null)
            {
                word_app.Quit(ref oNothing, ref oNothing, ref oNothing);
            }            
        }
        public void Close()
        {
            count = 0;
            word_doc.Close();
            word_app.Quit();
        }
        public void initCount()
        {
            count = 0;
        }
    }
    class WordServe
    {
        private DoWord dw;
        private ItemParagraph item1;
        private ItemParagraph item2;
        private ItemParagraph item3;
        private ItemParagraph item4;
        public WordServe()
        {
            dw=new DoWord();
            dw.CreateWordDoc("text");
            item1 = new ItemParagraph();
            item1.Font_size = 22;
            item1.Font_name = "黑体";
            item1.SpaceAfter = 6;
            item2 = new ItemParagraph();
            item2.Font_size = 16;
            item2.Font_name = "黑体";
            item2.SpaceAfter = 6;
            item3 = new ItemParagraph();
            item3.Font_size = 14;
            item3.Font_name = "黑体";
            item3.SpaceAfter = 6;
            item4 = new ItemParagraph();
            item4.Font_size = 12;
            item4.Font_bold = 1;
            item4.SpaceAfter = 6;   
            dw.AddSimpleHeader("图表生成", null);
        }
        public WordServe(string filepath)
        {
            dw = new DoWord();
            dw.CreateWordDoc(filepath);
            item1 = new ItemParagraph();
            item1.Font_size = 22;
            item1.Font_name = "黑体";
            item1.SpaceAfter = 6;
            dw.AddSimpleHeader("大江东去，浪淘尽，千古风流人物", null);
        }
        public void insertText(string text)
        {
            dw.doInsert(text, HONG.TYPE_TITLE_LEVEL1, item1);
        }
        public void insertText(string text, int item)
        {
            switch (item)
            {
                case 1:
                    dw.doInsert(text,HONG.TYPE_TITLE_LEVEL1,item1);
                    break;
                case 2:
                    dw.doInsert(text, HONG.TYPE_TITLE_LEVEL1, item2);
                    break;
                case 3:
                    dw.doInsert(text, HONG.TYPE_TITLE_LEVEL1, item3);
                    break;
                case 4:
                    dw.doInsert(text, HONG.TYPE_TITLE_LEVEL1, item4);
                    break;
            }
        }
        public void insertPic(string path)
        {
            dw.doInsert("", HONG.TYPE_VALUE, null);
            dw.doInsert(path, HONG.TYPE_PICTURE, null);
        }
        public void close()
        {
            dw.Close();
         }
        public void initCount()
        {
            dw.initCount();
        }
    }
}
