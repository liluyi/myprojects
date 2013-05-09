
// SkinSelectorDoc.h : CSkinSelectorDoc ��Ľӿ�
//
#include "highgui.h"
#include "cv.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <assert.h>
#include <math.h>
#include <float.h>
#include <limits.h>
#include <time.h>
#include <ctype.h>

#pragma once


class CSkinSelectorDoc : public CDocument
{
protected: // �������л�����
	CSkinSelectorDoc();
	DECLARE_DYNCREATE(CSkinSelectorDoc)

// ����
public:
	CvvImage m_image;
	IplImage *ip_image;
	IplImage *mask_image;
	IplImage *finalimg;
	CPoint m_point;
	CvPoint p0,pn;
	BOOL m_imLoaded;
	BOOL m_picChange;
	int count;
	CPtrArray m_ptrArray;

// ����
public:

// ��д
public:
	virtual BOOL OnNewDocument();
	virtual void Serialize(CArchive& ar);

// ʵ��
public:
	virtual ~CSkinSelectorDoc();
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
	virtual BOOL OnSaveDocument(LPCTSTR lpszPathName);
};


