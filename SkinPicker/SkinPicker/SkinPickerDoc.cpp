
// SkinPickerDoc.cpp : CSkinPickerDoc 类的实现
//

#include "stdafx.h"
#include "SkinPicker.h"

#include "SkinPickerDoc.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CSkinPickerDoc

IMPLEMENT_DYNCREATE(CSkinPickerDoc, CDocument)

BEGIN_MESSAGE_MAP(CSkinPickerDoc, CDocument)
END_MESSAGE_MAP()


// CSkinPickerDoc 构造/析构

CSkinPickerDoc::CSkinPickerDoc()
{
	// TODO: 在此添加一次性构造代码

}

CSkinPickerDoc::~CSkinPickerDoc()
{
}

BOOL CSkinPickerDoc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;

	// TODO: 在此添加重新初始化代码
	// (SDI 文档将重用该文档)

	return TRUE;
}




// CSkinPickerDoc 序列化

void CSkinPickerDoc::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		// TODO: 在此添加存储代码
	}
	else
	{
		// TODO: 在此添加加载代码
	}
}


// CSkinPickerDoc 诊断

#ifdef _DEBUG
void CSkinPickerDoc::AssertValid() const
{
	CDocument::AssertValid();
}

void CSkinPickerDoc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
#endif //_DEBUG


// CSkinPickerDoc 命令

BOOL CSkinPickerDoc::OnOpenDocument(LPCTSTR lpszPathName)
{
	if (!CDocument::OnOpenDocument(lpszPathName)) 
		return FALSE;
    // TODO: Add your specialized creation code here
    m_image.Load(lpszPathName);
	img=cvLoadImage(lpszPathName);
	
	IplImage *image=img;
		
	/*cvNamedWindow("原始文件",CV_WINDOW_AUTOSIZE);
	cvShowImage("原始文件",image);
	cvWaitKey(0);
	cvReleaseImage(& image);
	cvDestroyWindow("原始文件");*/
	
    return TRUE;
}

BOOL CSkinPickerDoc::OnSaveDocument(LPCTSTR lpszPathName)
{
	m_image.Save(lpszPathName);

    return TRUE;
}
