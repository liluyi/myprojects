
// SkinPickerDoc.cpp : CSkinPickerDoc ���ʵ��
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


// CSkinPickerDoc ����/����

CSkinPickerDoc::CSkinPickerDoc()
{
	// TODO: �ڴ����һ���Թ������

}

CSkinPickerDoc::~CSkinPickerDoc()
{
}

BOOL CSkinPickerDoc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;

	// TODO: �ڴ�������³�ʼ������
	// (SDI �ĵ������ø��ĵ�)

	return TRUE;
}




// CSkinPickerDoc ���л�

void CSkinPickerDoc::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		// TODO: �ڴ���Ӵ洢����
	}
	else
	{
		// TODO: �ڴ���Ӽ��ش���
	}
}


// CSkinPickerDoc ���

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


// CSkinPickerDoc ����

BOOL CSkinPickerDoc::OnOpenDocument(LPCTSTR lpszPathName)
{
	if (!CDocument::OnOpenDocument(lpszPathName)) 
		return FALSE;
    // TODO: Add your specialized creation code here
    m_image.Load(lpszPathName);
	img=cvLoadImage(lpszPathName);
	
	IplImage *image=img;
		
	/*cvNamedWindow("ԭʼ�ļ�",CV_WINDOW_AUTOSIZE);
	cvShowImage("ԭʼ�ļ�",image);
	cvWaitKey(0);
	cvReleaseImage(& image);
	cvDestroyWindow("ԭʼ�ļ�");*/
	
    return TRUE;
}

BOOL CSkinPickerDoc::OnSaveDocument(LPCTSTR lpszPathName)
{
	m_image.Save(lpszPathName);

    return TRUE;
}
