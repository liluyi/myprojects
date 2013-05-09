
// SkinSelectorView.h : CSkinSelectorView ��Ľӿ�
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


class CSkinSelectorView : public CScrollView
{
protected: // �������л�����
	CSkinSelectorView();
	DECLARE_DYNCREATE(CSkinSelectorView)

// ����
public:
	CSkinSelectorDoc* GetDocument() const;
	CPoint m_OrigPoint;
	CPtrArray m_ptrArray;

protected:
       BOOL m_bLButtonDown, m_bErase; // �ж��Ƿ���������
//���Ƿ���Ҫ����ͼ�ε������
       CPoint p0, pm; // ��¼ֱ�����Ͷ�̬�յ�������
       CPen * pGrayPen, * pLinePen, * pRepaintPen; // �����ɫ��ֱ�߱�
	   HDC m_hDC;
	   


// ����
public:

// ��д
public:
	virtual void OnDraw(CDC* pDC);  // ��д�Ի��Ƹ���ͼ
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
protected:
	virtual void OnInitialUpdate(); // ������һ�ε���

// ʵ��
public:
	virtual ~CSkinSelectorView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// ���ɵ���Ϣӳ�亯��
protected:
	afx_msg void OnFilePrintPreview();
	afx_msg void OnContextMenu(CWnd* pWnd, CPoint point);
	DECLARE_MESSAGE_MAP()

public:
	afx_msg void OnMouseMove(UINT nFlags, CPoint point);
	afx_msg void OnMButtonDown(UINT nFlags, CPoint point);
	afx_msg void OnLButtonUp(UINT nFlags, CPoint point);
	afx_msg void OnRButtonDown(UINT nFlags, CPoint point);
	afx_msg void OnLButtonDown(UINT nFlags, CPoint point);
	afx_msg void OnRButtonUp(UINT nFlags, CPoint point);
	afx_msg void OnPaint();
};

#ifndef _DEBUG  // SkinSelectorView.cpp �еĵ��԰汾
inline CSkinSelectorDoc* CSkinSelectorView::GetDocument() const
   { return reinterpret_cast<CSkinSelectorDoc*>(m_pDocument); }
#endif

