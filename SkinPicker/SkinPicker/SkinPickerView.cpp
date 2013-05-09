
// SkinPickerView.cpp : CSkinPickerView 类的实现
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
	// 标准打印命令
	ON_COMMAND(ID_FILE_PRINT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, &CSkinPickerView::OnFilePrintPreview)
END_MESSAGE_MAP()

// CSkinPickerView 构造/析构

CSkinPickerView::CSkinPickerView()
{
	// TODO: 在此处添加构造代码

}

CSkinPickerView::~CSkinPickerView()
{
}

BOOL CSkinPickerView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: 在此处通过修改
	//  CREATESTRUCT cs 来修改窗口类或样式

	return CView::PreCreateWindow(cs);
}

// CSkinPickerView 绘制

void CSkinPickerView::OnDraw(CDC* pDC)
{
	CSkinPickerDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (!pDoc)
		return;
	

	// TODO: 在此处为本机数据添加绘制代码
	
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


// CSkinPickerView 打印


void CSkinPickerView::OnFilePrintPreview()
{
	AFXPrintPreview(this);
}

BOOL CSkinPickerView::OnPreparePrinting(CPrintInfo* pInfo)
{
	// 默认准备
	return DoPreparePrinting(pInfo);
}

void CSkinPickerView::OnBeginPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: 添加额外的打印前进行的初始化过程
}

void CSkinPickerView::OnEndPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: 添加打印后进行的清理过程
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


// CSkinPickerView 诊断

#ifdef _DEBUG
void CSkinPickerView::AssertValid() const
{
	CView::AssertValid();
}

void CSkinPickerView::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

CSkinPickerDoc* CSkinPickerView::GetDocument() const // 非调试版本是内联的
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CSkinPickerDoc)));
	return (CSkinPickerDoc*)m_pDocument;
}
#endif //_DEBUG


// CSkinPickerView 消息处理程序
