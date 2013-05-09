
// SkinSelectorView.cpp : CSkinSelectorView ���ʵ��
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

// CSkinSelectorView ����/����

CSkinSelectorView::CSkinSelectorView()
{
	// TODO: �ڴ˴���ӹ������

}

CSkinSelectorView::~CSkinSelectorView()
{
}

BOOL CSkinSelectorView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: �ڴ˴�ͨ���޸�
	//  CREATESTRUCT cs ���޸Ĵ��������ʽ
SetScrollSizes ( MM_TEXT,CSize (0 , 0 ) );//�ڳ�ʼ������ǰ��Ҫ�ȶ����ScrollSizes����֪��Ϊʲô������һ��Ҫ����������������������--��贵t
	return CScrollView::PreCreateWindow(cs);
}


// CSkinSelectorView ����

void CSkinSelectorView::OnDraw(CDC* pDC)
{
	CSkinSelectorDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);

	if (!pDoc)
		return;

	// TODO: �ڴ˴�Ϊ����������ӻ��ƴ���
	//��ͼ����Ƶ������豸
	CvvImage & img = pDoc ->m_image;
	
	CRect rect;
	GetClientRect (&rect);
	rect.left=0;
	rect.top=0;
	m_hDC=pDC->GetSafeHdc();
	m_bLButtonDown = FALSE; // ������������Ϊ��
    m_bErase = FALSE; // ����Ҫ����Ϊ��
    pGrayPen = new CPen(PS_SOLID, 0, RGB(128, 128, 128));// ������ɫ��
    pLinePen = new CPen(PS_SOLID, 0, RGB(0, 0, 255));// ������ɫ��ֱ�߱�
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
		//�ͷ�ͼ��
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
		//���ڱ仯ʱ�ػ��Ѿ����õľ�������
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
 
      //����contour
		cvFindContours( pDoc->mask_image, storage, &contour, sizeof(CvContour), CV_RETR_TREE,CV_CHAIN_APPROX_SIMPLE);
 
		//����������   
		cvDrawContours(pDoc->mask_image, contour,cvScalarAll(255),cvScalarAll(255),100);*/
		//��ʾͼ��
		cvNamedWindow("contour",1);
		cvShowImage( "contour", pDoc->mask_image );
		//cvWaitKey(0);
 
		//���ٴ���
		//cvDestroyWindow( "contour" );
		//�ͷ�ͼ��
		//cvReleaseImage( &pDoc->mask_image ); 
		//cvReleaseMemStorage(&storage);
	}
	
  //////////////////////////////////////////////////////////////////////////////////////////

	
	if(pDoc->m_picChange==TRUE)
	{
		pDoc->m_picChange=FALSE;	

			//cvDestroyWindow( "contour" );
		//�ͷ�ͼ��
		//cvReleaseImage( &pDoc->mask_image ); 
		//cvReleaseMemStorage(&storage);
	}
	
}

void CSkinSelectorView::OnInitialUpdate()
{
	
	CScrollView::OnInitialUpdate();

	CSize sizeTotal;
	// TODO: �������ͼ�ĺϼƴ�С
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


// CSkinSelectorView ���

#ifdef _DEBUG
void CSkinSelectorView::AssertValid() const
{
	CScrollView::AssertValid();
}

void CSkinSelectorView::Dump(CDumpContext& dc) const
{
	CScrollView::Dump(dc);
}

CSkinSelectorDoc* CSkinSelectorView::GetDocument() const // �ǵ��԰汾��������
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CSkinSelectorDoc)));
	return (CSkinSelectorDoc*)m_pDocument;
}
#endif //_DEBUG


// CSkinSelectorView ��Ϣ�������


void CSkinSelectorView::OnMButtonDown(UINT nFlags, CPoint point)
{
	// TODO: �ڴ������Ϣ�����������/�����Ĭ��ֵ

	CSkinSelectorDoc* pDoc = GetDocument();
	if(pDoc->m_imLoaded)
	{
	CDC* pDC = GetDC();
	OnPrepareDC(pDC);
	pDC->DPtoLP(&point);
	cvFloodFill(pDoc->mask_image,cvPoint(point.x,point.y),cvScalarAll(0));
	cvNamedWindow("��ˮ���",1);
	cvShowImage("��ˮ���",pDoc->mask_image);
	}

	CScrollView::OnMButtonDown(nFlags, point);
}


void CSkinSelectorView::OnRButtonDown(UINT nFlags, CPoint point)
{
	// TODO: �ڴ������Ϣ�����������/�����Ĭ��ֵ
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
	file.Close(); *///���������ʼ�ȡ��ǰRGBֵ�����������

	if(pDoc->m_imLoaded)
	{
	CDC* pDC = GetDC();
	OnPrepareDC(pDC);
	pDC->DPtoLP(&point);
	cvFloodFill(pDoc->mask_image,cvPoint(point.x,point.y),cvScalarAll(100));
	cvNamedWindow("��ˮ���",1);
	cvShowImage("��ˮ���",pDoc->mask_image);
	}

	CScrollView::OnRButtonDown(nFlags, point);
}

void CSkinSelectorView::OnLButtonDown(UINT nFlags, CPoint point)
{
	// TODO: �ڴ������Ϣ�����������/�����Ĭ��ֵ
	CSkinSelectorDoc* pDoc = GetDocument();
	if(pDoc->m_imLoaded)
	{

	m_bLButtonDown = TRUE; // ������������Ϊ��
	SetCapture(); // ������겶��
	p0 = point; // ����������Ͻ�
	pm = p0; // �þ������½ǵ������Ͻ�
	}

	CScrollView::OnLButtonDown(nFlags, point);
	
}

void CSkinSelectorView::OnMouseMove(UINT nFlags, CPoint point)
{
	// TODO: �ڴ������Ϣ�����������/�����Ĭ��ֵ
	

	CSkinSelectorDoc* pDoc = GetDocument();
	if(pDoc->m_imLoaded)
	{
	pDoc->m_point=point;//��ȡ��ǰ����
	//InvalidateRect(NULL,FALSE);	//

	CDC *pDC = GetDC();//��ȡ�豸������������RGBֵ����ʾ��״̬��
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
	str.Format("(%03d,%03d,%03d)",R,G,B);//�ڶ��ָ�ʽ������������㣡

     SetCursor(LoadCursor(NULL, IDC_CROSS)); // �������Ϊʮ��
       if (m_bLButtonDown) 
	   { // ����������Ϊ��
		   
               CDC* pDC = GetDC(); //��ȡ�豸������
              pDC->SelectObject(pGrayPen);// ѡȡ��ɫ��
              pDC->SetROP2(R2_XORPEN);// ����Ϊ����ͼ��ʽ
		  
              if (m_bErase) 
			  { // ��Ҫ����Ϊ��
					pDC->Rectangle(CRect(p0,pm));// ����ԭ����
              }
              else // ��Ҫ����Ϊ��
                     m_bErase = TRUE; // ����Ҫ����Ϊ��
			  pDC->Rectangle(CRect(p0,point));// �����¾���
              pm = point; // ��¼���յ�
              ReleaseDC(pDC); // �ͷ��豸������
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
	// TODO: �ڴ������Ϣ�����������/�����Ĭ��ֵ
	
	CSkinSelectorDoc* pDoc = GetDocument();
	if(pDoc->m_imLoaded)
	{

	ReleaseCapture(); // �ͷ���겶��
	if (m_bLButtonDown) 
	{ // ����������Ϊ��
		CDC* pDC = GetDC(); //->GetSafeHdc() ��ȡ�豸������
		//pDC->Attach(m_hDC2);
		
		///
		pDC->SelectObject(pGrayPen);// ѡȡ��ɫ��
		pDC->SetROP2(R2_XORPEN); // ����Ϊ����ͼ��ʽ
		//pDC->SetROP2(R2_NOP);
		pDC->Rectangle(CRect(p0,pm));// ����ԭ����

		pDC->SelectObject(pLinePen); // ѡ��ֱ�߱�
		//pDC->SetROP2(R2_COPYPEN);// ����Ϊ���ǻ�ͼ��ʽ
		//pDC->SetROP2(R2_NOP);
		pDC->SetROP2(R2_XORPEN); 

		//CClientDC dc(this);
		
		/*OnPrepareDC(&dc);
		dc.DPtoLP(&p0);
		dc.DPtoLP(&point);*/
		//CString tempstr1,tempstr2;
			
		OnPrepareDC(pDC);
		
		//tempstr1.Format("(%03d,%03d)",p0.x,p0.y);�˶ο���֤���豸�����ȷ�ѱ�ת��
		//pDC->TextOutA(p0.x,p0.y,tempstr1,9);
		pDC->DPtoLP(&p0);//�豸����ת��Ϊ�߼�����
		pDC->DPtoLP(&point);	
		pDoc->p0.x=p0.x;
		pDoc->p0.y=p0.y;
		pDoc->pn.x=point.x;
		pDoc->pn.y=point.y;
		
			  
		CGraph *pGraph=new CGraph(p0,point);
		//CGraph pGraph(p0,point);���ַ�ʽ���С���
		m_ptrArray.Add(pGraph);

		/*CPoint tempoint;//�˶�ԭ�����ڽ���ѡ�����RGBֵ���浽�ı��ĵ��С�
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

		pDC->Rectangle(CRect(p0,point));// �������յľ���
		cvRectangle(pDoc->mask_image,pDoc->p0,pDoc->pn,cvScalarAll(255),1,8,0);
		cvShowImage("contour",pDoc->mask_image);
			
		m_bLButtonDown = FALSE; // ��������������Ϊ��
		m_bErase = FALSE; // ����Ҫ����Ϊ��

		/*pDC->SelectObject(pRepaintPen);
		for(int i=0;i<m_ptrArray.GetSize();i++)
		{
		    pDC->Rectangle(CRect(((CGraph*)m_ptrArray.GetAt(i))->m_ptOrigin,((CGraph*)m_ptrArray.GetAt(i))->m_ptEnd));
		}*/

		ReleaseDC(pDC); // �ͷ��豸������
       }
	}
   //CRect(p0,pm).PtInRect(//�ж�ĳ���ǲ����ڴ˾�����
	CScrollView::OnLButtonUp(nFlags, point);
}





void CSkinSelectorView::OnRButtonUp(UINT nFlags, CPoint point)
{
	// TODO: �ڴ������Ϣ�����������/�����Ĭ��ֵ

	
	CScrollView::OnRButtonUp(nFlags, point);
}

void CSkinSelectorView::OnPaint()
{
	CPaintDC dc(this); // device context for painting
	// TODO: �ڴ˴������Ϣ����������
	// ��Ϊ��ͼ��Ϣ���� CScrollView::OnPaint()
	OnPrepareDC(&dc);
	OnDraw(&dc);
}
