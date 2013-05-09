
// FaceDetectionDoc.h : CFaceDetectionDoc ��Ľӿ�
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


class CFaceDetectionDoc : public CDocument
{
protected: // �������л�����
	CFaceDetectionDoc();
	DECLARE_DYNCREATE(CFaceDetectionDoc)

// ����
public:
	CvvImage m_image;
	IplImage *ip_image;
	CvCapture* capture;
	IplImage *frame, *frame_copy;
	CvHistogram* skinhist;
	CvHistogram* nonskinhist;
	CvMemStorage* storage;
	CvHaarClassifierCascade* cascade;
	CvHaarClassifierCascade* profilecascade;
	const char* cascade_name;
/*    "haarcascade_profileface.xml";*/
	const char* profilecascade_name;
	float skintotal,nonskintotal;
	BOOL isImLoad;
	BOOL isCasLoad;
	BOOL isVidLoad;
// ����
public:

// ��д
public:
	virtual BOOL OnNewDocument();
	virtual void Serialize(CArchive& ar);

// ʵ��
public:
	virtual ~CFaceDetectionDoc();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// ���ɵ���Ϣӳ�亯��
protected:
	DECLARE_MESSAGE_MAP()
public:
	BOOL OnOpenDocument(LPCTSTR lpszPathName);
	afx_msg void On32774();
	afx_msg void OnImportClassifier();
};


