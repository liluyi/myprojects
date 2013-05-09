
// SkinSelectorDoc.h : CSkinSelectorDoc 类的接口
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
protected: // 仅从序列化创建
	CSkinSelectorDoc();
	DECLARE_DYNCREATE(CSkinSelectorDoc)

// 属性
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

// 操作
public:

// 重写
public:
	virtual BOOL OnNewDocument();
	virtual void Serialize(CArchive& ar);

// 实现
public:
	virtual ~CSkinSelectorDoc();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// 生成的消息映射函数
protected:
	DECLARE_MESSAGE_MAP()
public:
	virtual BOOL OnOpenDocument(LPCTSTR lpszPathName);
	virtual BOOL OnSaveDocument(LPCTSTR lpszPathName);
};


