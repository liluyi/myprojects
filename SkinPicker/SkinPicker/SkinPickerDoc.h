
// SkinPickerDoc.h : CSkinPickerDoc ��Ľӿ�
//
#include "highgui.h"

#pragma once


class CSkinPickerDoc : public CDocument
{
protected: // �������л�����
	CSkinPickerDoc();
	DECLARE_DYNCREATE(CSkinPickerDoc)

// ����
public:

	CvvImage m_image;
	IplImage* img;
// ����
public:

// ��д
public:
	virtual BOOL OnNewDocument();
	virtual void Serialize(CArchive& ar);

// ʵ��
public:
	virtual ~CSkinPickerDoc();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// ���ɵ���Ϣӳ�亯��
protected:
	DECLARE_MESSAGE_MAP()
public:
	virtual BOOL OnOpenDocument(LPCTSTR lpszPathName);
	BOOL OnSaveDocument(LPCTSTR lpszPathName);
};


