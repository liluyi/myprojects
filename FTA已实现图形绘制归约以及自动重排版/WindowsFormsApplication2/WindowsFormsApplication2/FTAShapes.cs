using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;

namespace WindowsFormsApplication2
{
    class FTAShapes
    {
        NodeType nodetype;//节点类型
        private GraphicsPath path = new GraphicsPath();//节点形状
        public GraphicsPath Path
        {
            get
            {
                return path;
            }
        }

        public FTAShapes(NodeType nt)
        {
            nodetype = nt;
            switch (nodetype)//根据节点类型为节点形状赋值
            {
                case NodeType.EventInitiating://屋形，正常事件
                    {
                        path.AddLine(0, 0, 0, 20);

                        path.AddLine(0, 20, 10, 30);
                        path.AddLine(10, 30, 20, 20);
                        path.AddLine(20, 20, 20, 0);
                        path.AddLine(20, 0, 0, 0);

                        path.CloseFigure();
                        path.FillMode = FillMode.Alternate;
                        break;
                    }
                case NodeType.EventBasic://圆形，基本事件
                    {
                        path.AddEllipse(0, 0, 10, 10);
                        path.FillMode = FillMode.Alternate;//.Alternate;//环绕填充
                        break;
                    }
                case NodeType.EventUndeveloped://菱形，未展开事件
                    {
                        path.AddLine(0, 10, 10, 20);
                        path.AddLine(10, 20, 20, 10);
                        path.AddLine(20, 10, 10, 0);
                        path.CloseFigure();
                        path.FillMode = FillMode.Alternate;
                        break;
                    }
                case NodeType.EventConditioning://椭圆，初始事件
                    {
                        path.AddEllipse(0, 0, 10, 10);
                        path.FillMode = FillMode.Alternate;
                        break;
                    }
                case NodeType.EventIn://入事件
                    {
                        path.AddLine(0, 10, 5, 0);

                        path.AddLine(5, 0, 10, 10);
                        path.AddLine(10, 10, 0, 10);

                        path.CloseFigure();
                        path.FillMode = FillMode.Alternate;
                        break;
                    }
                case NodeType.EventOut://出事件
                    {
                        path.AddLine(0, 5, 10, 0);
                        path.AddLine(10, 0, 20, 5);
                        path.AddLine(20, 5, 0, 5);
                        path.CloseFigure();
                        path.FillMode = FillMode.Alternate;
                        break;
                    }
                case NodeType.GateAnd://与门
                    {
                        path.AddArc(0, 0, 30, 15, 180, 180);//弧
                        path.CloseFigure();
                        path.FillMode = FillMode.Alternate;
                        break;
                    }
                case NodeType.GatePri://优先门
                    {
                        path.AddLine(0, 20, 10, 0);
                        path.AddLine(10, 0, 20, 20);
                        path.AddLine(20, 20, 0, 20);

                        path.AddArc(0, 0, 20, 40, 180, 180);//弧
                        path.CloseFigure();
                        path.FillMode = FillMode.Winding;
                        break;
                    }
                case NodeType.GateXor://异或门
                    {
                        path.AddArc(0, 0, 20, 40, 180, 180);//弧
                        path.AddArc(0, 20, 20, 5, 0, -180);

                        path.CloseFigure();
                        path.AddLine(0, 22, 10, 0);
                        path.CloseFigure();
                        path.AddLine(10, 0, 20, 22);
                        path.CloseFigure();

                        path.FillMode = FillMode.Alternate;
                        break;
                    }
            }
        }
    }
}

