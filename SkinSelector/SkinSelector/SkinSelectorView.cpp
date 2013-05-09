
// SkinSelectorView.cpp : CSkinSelectorView 类的实现
//

#include "stdafx.h"
#include "SkinSelector.h"

#include "SkinSelectorDoc.h"
#include "SkinSelectorView.h"
#include "MainFrm.h"
#include "Graph.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

// CSkinSelectorView

IMPLEMENT_DYNCREATE(CSkinSelectorView, CScrollView)

BEGIN_MESSAGE_MAP(CSkinSelectorView, CScrollView)
	ON_WM_MOUSEMOVE()
	ON_WM_MBUTTONDOWN()
	ON_WM_LBUTTONUP()
	ON_WM_RBUTTONDOWN()
	ON_WM_LBUTTONDOWN()
	ON_WM_RBUTTONUP()
	ON_WM_PAINT()
END_MESSAGE_MAP()

// CSkinSelectorView 构造/析构

CSkinSelectorView::CSkinSelectorView()
{
	// TODO: 在此处添加构造代码

}

CSkinSelectorView::~CSkinSelectorView()
{
}

BOOL CSkinSelectorView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: 在此处通过修改
	//  CREATESTRUCT cs 来修改窗口类或样式
SetScrollSizes ( MM_TEXT,CSize (0 , 0 ) );//在初始化窗口前就要先定义好ScrollSizes，不知道为什么，但是一定要！！！！！！！！！！！--李璐祎
	return CScrollView::PreCreateWindow(cs);
}


// CSkinSelectorView 绘制

void CSkinSelectorView::OnDraw(CDC* pDC)
{
	CSkinSelectorDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);

	if (!pDoc)
		return;

	// TODO: 在此处为本机数据添加绘制代码
	//将图像绘制到窗口设备
	CvvImage & img = pDoc ->m_image;
	
	CRect rect;
	GetClientRect (&rect);
	rect.left=0;
	rect.top=0;
	m_hDC=pDC->GetSafeHdc();
	m_bLButtonDown = FALSE; // 设左鼠标键按下为假
    m_bErase = FALSE; // 设需要擦除为假
    pGrayPen = new CPen(PS_SOLID, 0, RGB(128, 128, 128));// 创建灰色笔
    pLinePen = new CPen(PS_SOLID, 0, RGB(0, 0, 255));// 创建红色的直线笔
	pRepaintPen=new CPen(PS_SOLID,0,RGB(255,0,0));

	if(pDoc->m_picChange==TRUE)
	{
		pDoc->m_picChange=FALSE;	
//
		//pDoc->finalimg=NULL;

		//CvScalar s;
///
		//pDoc->finalimg=cvCreateImage(cvGetSize(pDoc->ip_image),8,1);
		/*for(int m=0;m<pDoc->ip_image->height;m++)
			for(int n=0;n<pDoc->ip_image->width;n++)
			{
				cvSet2D(pDoc->finalimg,m,n,cvScalarAll(0));//
				if(cvGet2D(pDoc->mask_image,m,n).val[0]==255)
					cvSet2D(pDoc->mask_image,m,n,cvScalar(0));
				for(int i=0;i<m_ptrArray.GetSize();i++)
				{
					if(CRect(((CGraph*)m_ptrArray.GetAt(i))->m_ptOrigin,((CGraph*)m_ptrArray.GetAt(i))->m_ptEnd).PtInRect(CPoint(m,n)))
						
					cvSet2D(pDoc->mask_image,m,n,cvScalarAll(255));				
				}
			//if(cvGet2D(pDoc->mask_image,m,n).val[0]==100)
				//cvSet2D(pDoc->mask_image,m,n,cvScalarAll(255));
			}*/

			//cvShowImage( "contour", pDoc->mask_image );

			//cvSaveImage("D:/mask.pbm",pDoc->mask_image,0);
//				
	//}
			m_ptrArray.RemoveAll();
			//cvDestroyWindow( "contour" );
		//释放图像
		//cvReleaseImage( &pDoc->mask_image ); 
		//cvReleaseMemStorage(&storage);
	}
	

	if(img.Width())
	{
	CSize sizeTotal;
	sizeTotal.cx=img.Width();
	sizeTotal.cy=img.Height();

	rect.right=img.Width();
	rect.bottom=img.Height();

	SetScrollSizes(MM_TEXT, sizeTotal);
		img.DrawToHDC(m_hDC ,rect);
	}

	if(pDoc->m_imLoaded)
	{
		//窗口变化时重绘已经画好的矩形区域
		pDC->SelectObject(pLinePen);
		pDC->SetROP2(R2_XORPEN); 

		for(int i=0;i<m_ptrArray.GetSize();i++)
	   {
		pDC->Rectangle(CRect(((CGraph*)m_ptrArray.GetAt(i))->m_ptOrigin,((CGraph*)m_ptrArray.GetAt(i))->m_ptEnd));
	   }

		/*pDoc->mask_image = NULL;
		
		CvSeq * contour = 0;
	
		cvNamedWindow("contour",1);

		pDoc->mask_image = cvCreateImage(cvGetSize(pDoc->ip_image),8,1);
      //copy source image and convert it to BGR image
		cvCvtColor(pDoc->ip_image, pDoc->mask_image, CV_BGR2GRAY);
		cvThreshold( pDoc->mask_image, pDoc->mask_image,100,255,CV_THRESH_BINARY);
 
      //查找contour
		cvFindContours( pDoc->mask_image, storage, &contour, sizeof(CvContour), CV_RETR_TREE,CV_CHAIN_APPROX_SIMPLE);
 
		//将轮廓画出   
		cvDrawContours(pDoc->mask_image, contour,cvScalarAll(255),cvScalarAll(255),100);*/
		//显示图像
		cvNamedWindow("contour",1);
		cvShowImage( "contour", pDoc->mask_image );
		//cvWaitKey(0);
 
		//销毁窗口
		//cvDestroyWindow( "contour" );
		//释放图像
		//cvReleaseImage( &pDoc->mask_image ); 
		//cvReleaseMemStorage(&storage);
	}
	
  //////////////////////////////////////////////////////////////////////////////////////////

	
	if(pDoc->m_picChange==TRUE)
	{
		pDoc->m_picChange=FALSE;	

			//cvDestroyWindow( "contour" );
		//释放图像
		//cvReleaseImage( &pDoc->mask_image ); 
		//cvReleaseMemStorage(&storage);
	}
	
}

void CSkinSelectorView::OnInitialUpdate()
{
	
	CScrollView::OnInitialUpdate();

	CSize sizeTotal;
	// TODO: 计算此视图的合计大小
	/*sizeTotal.cx = sizeTotal.cy = 3000;
	SetScrollSizes(MM_TEXT, sizeTotal);*/

	//CFile file;
	//file.Open( "D:/skinrgb.txt", CFile::modeCreate|CFile::modeNoTruncate|CFile::modeWrite);  
	//file.Close();
	SetScrollSizes(MM_TEXT,CSize(100,100));
	
}

void CSkinSelectorView::OnContextMenu(CWnd* pWnd, CPoint point)
{
	theApp.GetContextMenuManager()->ShowPopupMenu(IDR_POPUP_EDIT, point.x, point.y, this, TRUE);
}


// CSkinSelectorView 诊断

#ifdef _DEBUG
void CSkinSelectorView::AssertValid() const
{
	CScrollView::AssertValid();
}

void CSkinSelectorView::Dump(CDumpContext& dc) const
{
	CScrollView::Dump(dc);
}

CSkinSelectorDoc* CSkinSelectorView::GetDocument() const // 非调试版本是内联的
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CSkinSelectorDoc)));
	return (CSkinSelectorDoc*)m_pDocument;
}
#endif //_DEBUG


// CSkinSelectorView 消息处理程序


void CSkinSelectorView::OnMButtonDown(UINT nFlags, CPoint point)
{
	// TODO: 在此添加消息处理程序代码和/或调用默认值

	CSkinSelectorDoc* pDoc = GetDocument();
	if(pDoc->m_imLoaded)
	{
	CDC* pDC = GetDC();
	OnPrepareDC(pDC);
	pDC->DPtoLP(&point);
	cvFloodFill(pDoc->mask_image,cvPoint(point.x,point.y),cvScalarAll(0));
	cvNamedWindow("漫水填充",1);
	cvShowImage("漫水填充",pDoc->mask_image);
	}

	CScrollView::OnMButtonDown(nFlags, point);
}


void CSkinSelectorView::OnRButtonDown(UINT nFlags, CPoint point)
{
	// TODO: 在此添加消息处理程序代码和/或调用默认值
	CSkinSelectorDoc* pDoc = GetDocument();
	pDoc->m_point=point;
	//InvalidateRect(NULL,FALSE);	

	/*CDC *pDC = GetDC();
	COLORREF rgb=pDC->GetPixel(pDoc->m_point);
	int R=GetRValue(rgb);
	int G=GetGValue(rgb);
	int B=GetBValue(rgb);
	CString strr,strg,strb;
	strr.Format("%03d", R); 
	strg.Format("%03d", G); 
	strb.Format("%03d", B); 
	CString str;
	str.Format("(%03d,%03d,%03d)",R,G,B);

	CFile file; 
	file.Open( "D:/skinrgb.txt", CFile::modeWrite ); 
	file.SeekToEnd();
	file.Write( str.GetBuffer(0),str.GetLength() ); 
	file.Close(); *///曾经用来邮件取当前RGB值并保存过……

	if(pDoc->m_imLoaded)
	{
	CDC* pDC = GetDC();
	OnPrepareDC(pDC);
	pDC->DPtoLP(&point);
	cvFloodFill(pDoc->mask_image,cvPoint(point.x,point.y),cvScalarAll(100));
	cvNamedWindow("漫水填充",1);
	cvShowImage("漫水填充",pDoc->mask_image);
	}

	CScrollView::OnRButtonDown(nFlags, point);
}

void CSkinSelectorView::OnLButtonDown(UINT nFlags, CPoint point)
{
	// TODO: 在此添加消息处理程序代码和/或调用默认值
	CSkinSelectorDoc* pDoc = GetDocument();
	if(pDoc->m_imLoaded)
	{

	m_bLButtonDown = TRUE; // 设左鼠标键按下为真
	SetCapture(); // 设置鼠标捕获
	p0 = point; // 保存矩形左上角
	pm = p0; // 让矩形右下角等于左上角
	}

	CScrollView::OnLButtonDown(nFlags, point);
	
}

void CSkinSelectorView::OnMouseMove(UINT nFlags, CPoint point)
{
	// TODO: 在此添加消息处理程序代码和/或调用默认值
	

	CSkinSelectorDoc* pDoc = GetDocument();
	if(pDoc->m_imLoaded)
	{
	pDoc->m_point=point;//获取当前鼠标点
	//InvalidateRect(NULL,FALSE);	//

	CDC *pDC = GetDC();//获取设备句柄，检测鼠标出RGB值并显示在状态栏
	COLORREF rgb=pDC->GetPixel(pDoc->m_point);
	int R=GetRValue(rgb);
	int G=GetGValue(rgb);
	int B=GetBValue(rgb);
	CString strr,strg,strb;
	//strr.Format("%03d", R); 
	//strg.Format("%03d", G); 
	//strb.Format("%03d", B); 
	//CString str="("+strr+","+strg+","+strb+")";
	CString str;
	str.Format("(%03d,%03d,%03d)",R,G,B);//第二种格式化方法，更简便！

     SetCursor(LoadCursor(NULL, IDC_CROSS)); // 设置鼠标为十字
       if (m_bLButtonDown) 
	   { // 左鼠标键按下为真
		   
               CDC* pDC = GetDC(); //获取设备上下文
              pDC->SelectObject(pGrayPen);// 选取灰色笔
              pDC->SetROP2(R2_XORPEN);// 设置为异或绘图方式
		  
              if (m_bErase) 
			  { // 需要擦除为真
					pDC->Rectangle(CRect(p0,pm));// 擦除原矩形
              }
              else // 需要擦除为假
                     m_bErase = TRUE; // 设需要擦除为真
			  pDC->Rectangle(CRect(p0,point));// 绘制新矩形
              pm = point; // 记录老终点
              ReleaseDC(pDC); // 释放设备上下文
       }

	CClientDC dc(this);
	CSize sz=dc.GetTextExtent(str);
	((CMainFrame*)GetParent())->m_wndStatusBar.SetPaneInfo(1,ID_INDICATOR_RGBVALUE,SBPS_NORMAL,sz.cx);
	((CMainFrame*)GetParent())->m_wndStatusBar.SetPaneText(1,str);
	}
    
	CScrollView::OnMouseMove(nFlags, point);
}


void CSkinSelectorView::OnLButtonUp(UINT nFlags, CPoint point)
{
	// TODO: 在此添加消息处理程序代码和/或调用默认值
	
	CSkinSelectorDoc* pDoc = GetDocument();
	if(pDoc->m_imLoaded)
	{

	ReleaseCapture(); // 释放鼠标捕获
	if (m_bLButtonDown) 
	{ // 左鼠标键按下为真
		CDC* pDC = GetDC(); //->GetSafeHdc() 获取设备上下文
		//pDC->Attach(m_hDC2);
		
		///
		pDC->SelectObject(pGrayPen);// 选取灰色笔
		pDC->SetROP2(R2_XORPEN); // 设置为异或绘图方式
		//pDC->SetROP2(R2_NOP);
		pDC->Rectangle(CRect(p0,pm));// 擦除原矩形

		pDC->SelectObject(pLinePen); // 选择直线笔
		//pDC->SetROP2(R2_COPYPEN);// 设置为覆盖绘图方式
		//pDC->SetROP2(R2_NOP);
		pDC->SetROP2(R2_XORPEN); 

		//CClientDC dc(this);
		
		/*OnPrepareDC(&dc);
		dc.DPtoLP(&p0);
		dc.DPtoLP(&point);*/
		//CString tempstr1,tempstr2;
			
		OnPrepareDC(pDC);
		
		//tempstr1.Format("(%03d,%03d)",p0.x,p0.y);此段可以证明设备坐标的确已被转换
		//pDC->TextOutA(p0.x,p0.y,tempstr1,9);
		pDC->DPtoLP(&p0);//设备坐标转换为逻辑坐标
		pDC->DPtoLP(&point);	
		pDoc->p0.x=p0.x;
		pDoc->p0.y=p0.y;
		pDoc->pn.x=point.x;
		pDoc->pn.y=point.y;
		
			  
		CGraph *pGraph=new CGraph(p0,point);
		//CGraph pGraph(p0,point);此种方式不行……
		m_ptrArray.Add(pGraph);

		/*CPoint tempoint;//此段原来用于将所选区域的RGB值保存到文本文档中。
		for(tempoint=p0;tempoint.y<pm.y+1;tempoint.SetPoint(tempoint.x,tempoint.y+1))
			for(;tempoint.x<pm.x+1;tempoint.SetPoint(tempoint.x+1,tempoint.y))
		{	   
			COLORREF rgb=pDC->GetPixel(tempoint);
			int R=GetRValue(rgb);
			int G=GetGValue(rgb);
			int B=GetBValue(rgb);
			CString str;
			str.Format("(%03d,%03d,%03d)",R,G,B);

			CFile file;
			
			file.Open( "D:/skinrgb.txt", CFile::modeWrite);  //CFile::modeCreate|CFile::modeNoTruncate|
			file.SeekToEnd();
			file.Write( str.GetBuffer(0),str.GetLength() ); 
			file.Close(); 
		}*/

		pDC->Rectangle(CRect(p0,point));// 绘制最终的矩形
		cvRectangle(pDoc->mask_image,pDoc->p0,pDoc->pn,cvScalarAll(255),1,8,0);
		cvShowImage("contour",pDoc->mask_image);
			
		m_bLButtonDown = FALSE; // 重设左鼠标键按下为假
		m_bErase = FALSE; // 重需要擦除为假

		/*pDC->SelectObject(pRepaintPen);
		for(int i=0;i<m_ptrArray.GetSize();i++)
		{
		    pDC->Rectangle(CRect(((CGraph*)m_ptrArray.GetAt(i))->m_ptOrigin,((CGraph*)m_ptrArray.GetAt(i))->m_ptEnd));
		}*/

		ReleaseDC(pDC); // 释放设备上下文
       }
	}
   //CRect(p0,pm).PtInRect(//判断某点是不是在此矩形内
	CScrollView::OnLButtonUp(nFlags, point);
}





void CSkinSelectorView::OnRButtonUp(UINT nFlags, CPoint point)
{
	// TODO: 在此添加消息处理程序代码和/或调用默认值

	
	CScrollView::OnRButtonUp(nFlags, point);
}

void CSkinSelectorView::OnPaint()
{
	CPaintDC dc(this); // device context for painting
	// TODO: 在此处添加消息处理程序代码
	// 不为绘图消息调用 CScrollView::OnPaint()
	OnPrepareDC(&dc);
	OnDraw(&dc);
}
