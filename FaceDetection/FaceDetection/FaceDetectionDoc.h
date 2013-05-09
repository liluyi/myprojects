
// FaceDetectionDoc.h : CFaceDetectionDoc 类的接口
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
protected: // 仅从序列化创建
	CFaceDetectionDoc();
	DECLARE_DYNCREATE(CFaceDetectionDoc)

// 属性
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
// 操作
public:

// 重写
public:
	virtual BOOL OnNewDocument();
	virtual void Serialize(CArchive& ar);

// 实现
public:
	virtual ~CFaceDetectionDoc();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// 生成的消息映射函数
protected:
	DECLARE_MESSAGE_MAP()
public:
	BOOL OnOpenDocument(LPCTSTR lpszPathName);
	afx_msg void On32774();
	afx_msg void OnImportClassifier();
};


