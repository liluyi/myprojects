
// FaceDetectionView.h : CFaceDetectionView 类的接口
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


class CFaceDetectionView : public CScrollView
{
protected: // 仅从序列化创建
	CFaceDetectionView();
	DECLARE_DYNCREATE(CFaceDetectionView)

// 属性
public:
	CFaceDetectionDoc* GetDocument() const;
protected:
	HDC m_hDC;

// 操作
public:

// 重写
public:
	virtual void OnDraw(CDC* pDC);  // 重写以绘制该视图
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
protected:
	virtual void OnInitialUpdate(); // 构造后第一次调用
	virtual BOOL OnPreparePrinting(CPrintInfo* pInfo);
	virtual void OnBeginPrinting(CDC* pDC, CPrintInfo* pInfo);
	virtual void OnEndPrinting(CDC* pDC, CPrintInfo* pInfo);

// 实现
public:
	virtual ~CFaceDetectionView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// 生成的消息映射函数
protected:
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnDetectFace();
};

#ifndef _DEBUG  // FaceDetectionView.cpp 中的调试版本
inline CFaceDetectionDoc* CFaceDetectionView::GetDocument() const
   { return reinterpret_cast<CFaceDetectionDoc*>(m_pDocument); }
#endif

