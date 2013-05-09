
// FaceDetectionView.h : CFaceDetectionView ��Ľӿ�
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
protected: // �������л�����
	CFaceDetectionView();
	DECLARE_DYNCREATE(CFaceDetectionView)

// ����
public:
	CFaceDetectionDoc* GetDocument() const;
protected:
	HDC m_hDC;

// ����
public:

// ��д
public:
	virtual void OnDraw(CDC* pDC);  // ��д�Ի��Ƹ���ͼ
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
protected:
	virtual void OnInitialUpdate(); // ������һ�ε���
	virtual BOOL OnPreparePrinting(CPrintInfo* pInfo);
	virtual void OnBeginPrinting(CDC* pDC, CPrintInfo* pInfo);
	virtual void OnEndPrinting(CDC* pDC, CPrintInfo* pInfo);

// ʵ��
public:
	virtual ~CFaceDetectionView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// ���ɵ���Ϣӳ�亯��
protected:
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnDetectFace();
};

#ifndef _DEBUG  // FaceDetectionView.cpp �еĵ��԰汾
inline CFaceDetectionDoc* CFaceDetectionView::GetDocument() const
   { return reinterpret_cast<CFaceDetectionDoc*>(m_pDocument); }
#endif

