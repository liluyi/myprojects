
// SkinPickerView.cpp : CSkinPickerView ���ʵ��
//

#include "stdafx.h"
#include "SkinPicker.h"

#include "SkinPickerDoc.h"
#include "SkinPickerView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CSkinPickerView

IMPLEMENT_DYNCREATE(CSkinPickerView, CView)

BEGIN_MESSAGE_MAP(CSkinPickerView, CView)
	// ��׼��ӡ����
	ON_COMMAND(ID_FILE_PRINT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, &CSkinPickerView::OnFilePrintPreview)
END_MESSAGE_MAP()

// CSkinPickerView ����/����

CSkinPickerView::CSkinPickerView()
{
	// TODO: �ڴ˴���ӹ������

}

CSkinPickerView::~CSkinPickerView()
{
}

BOOL CSkinPickerView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: �ڴ˴�ͨ���޸�
	//  CREATESTRUCT cs ���޸Ĵ��������ʽ

	return CView::PreCreateWindow(cs);
}

// CSkinPickerView ����

void CSkinPickerView::OnDraw(CDC* pDC)
{
	CSkinPickerDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (!pDoc)
		return;
	

	// TODO: �ڴ˴�Ϊ����������ӻ��ƴ���
	
	CvvImage & img = pDoc ->m_image;
	CRect rect;
	GetClientRect (&rect);
	rect.left=10;
	rect.top=10;

	if(img.Width())
	{
		rect.right=img.Width()+10;
		rect.bottom=img.Height()+10;

		//img.DrawToHDC(pDC->GetSafeHdc() ,rect);
		img.DrawToHDC(pDC->m_hDC,rect);
	}
}


// CSkinPickerView ��ӡ


void CSkinPickerView::OnFilePrintPreview()
{
	AFXPrintPreview(this);
}

BOOL CSkinPickerView::OnPreparePrinting(CPrintInfo* pInfo)
{
	// Ĭ��׼��
	return DoPreparePrinting(pInfo);
}

void CSkinPickerView::OnBeginPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: ��Ӷ���Ĵ�ӡǰ���еĳ�ʼ������
}

void CSkinPickerView::OnEndPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: ��Ӵ�ӡ����е��������
}

void CSkinPickerView::OnRButtonUp(UINT nFlags, CPoint point)
{
	ClientToScreen(&point);
	OnContextMenu(this, point);
}

void CSkinPickerView::OnContextMenu(CWnd* pWnd, CPoint point)
{
	theApp.GetContextMenuManager()->ShowPopupMenu(IDR_POPUP_EDIT, point.x, point.y, this, TRUE);
}


// CSkinPickerView ���

#ifdef _DEBUG
void CSkinPickerView::AssertValid() const
{
	CView::AssertValid();
}

void CSkinPickerView::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

CSkinPickerDoc* CSkinPickerView::GetDocument() const // �ǵ��԰汾��������
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CSkinPickerDoc)));
	return (CSkinPickerDoc*)m_pDocument;
}
#endif //_DEBUG


// CSkinPickerView ��Ϣ�������
