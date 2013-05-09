using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;

namespace SFTAPlugin
{
    class FTAShapes
    {
        EventType eventtype;//事件类型
        GateType gatetype;//门类型
        private GraphicsPath path = new GraphicsPath();//节点形状
        public GraphicsPath Path
        {
            get
            {
                return path;
            }
        }

        public FTAShapes(EventType nt)
        {
            eventtype = nt;
            switch (eventtype)//根据节点类型为节点形状赋值
            {
                case EventType.EventInitiating://屋形，正常事件
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
                case EventType.EventBasic://圆形，基本事件
                    {
                        path.AddEllipse(0, 0, 10, 10);
                        path.FillMode = FillMode.Alternate;//.Alternate;//环绕填充
                        break;
                    }
                case EventType.EventUndeveloped://菱形，未展开事件（省略事件）
                    {
                        path.AddLine(0, 5, 15, 10);
                        path.AddLine(15, 10, 30, 5);
                        path.AddLine(30, 5, 15, 0);
                        path.CloseFigure();
                        path.FillMode = FillMode.Alternate;
                        break;
                    }
                case EventType.EventConditioning://椭圆，初始事件
                    {
                        path.AddEllipse(0, 0, 20, 10);
                        path.FillMode = FillMode.Alternate;
                        break;
                    }
                case EventType.EventIn://入事件
                    {
                        path.AddLine(0, 10, 5, 0);

                        path.AddLine(5, 0, 10, 10);
                        path.AddLine(10, 10, 0, 10);

                        path.CloseFigure();
                        path.FillMode = FillMode.Alternate;
                        break;
                    }
                case EventType.EventOut://出事件
                    {
                        path.AddLine(0, 5, 10, 0);
                        path.AddLine(10, 0, 20, 5);
                        path.AddLine(20, 5, 0, 5);
                        path.CloseFigure();
                        path.FillMode = FillMode.Alternate;
                        break;
                    }
            }
        }
        public FTAShapes(GateType nt)
        {
            gatetype = nt;
            switch (gatetype)//根据节点类型为节点形状赋值
            {
                case GateType.GateAnd://与门
                    {
                        path.AddArc(0, 0, 30, 15, 180, 180);//弧
                        path.CloseFigure();
                        path.FillMode = FillMode.Alternate;
                        break;
                    }
                case GateType.GatePri://优先门
                    {
                        path.AddLine(0, 20, 10, 0);
                        path.AddLine(10, 0, 20, 20);
                        path.AddLine(20, 20, 0, 20);

                        path.AddArc(0, 0, 20, 40, 180, 180);//弧
                        path.CloseFigure();
                        path.FillMode = FillMode.Winding;
                        break;
                    }
                case GateType.GateXor://异或门
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

