
// SkinSelectorView.h : CSkinSelectorView 类的接口
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
protected: // 仅从序列化创建
	CSkinSelectorView();
	DECLARE_DYNCREATE(CSkinSelectorView)

// 属性
public:
	CSkinSelectorDoc* GetDocument() const;
	CPoint m_OrigPoint;
	CPtrArray m_ptrArray;

protected:
       BOOL m_bLButtonDown, m_bErase; // 判断是否按下左鼠标键
//和是否需要擦除图形的类变量
       CPoint p0, pm; // 记录直线起点和动态终点的类变量
       CPen * pGrayPen, * pLinePen, * pRepaintPen; // 定义灰色和直线笔
	   HDC m_hDC;
	   


// 操作
public:

// 重写
public:
	virtual void OnDraw(CDC* pDC);  // 重写以绘制该视图
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
protected:
	virtual void OnInitialUpdate(); // 构造后第一次调用

// 实现
public:
	virtual ~CSkinSelectorView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// 生成的消息映射函数
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

#ifndef _DEBUG  // SkinSelectorView.cpp 中的调试版本
inline CSkinSelectorDoc* CSkinSelectorView::GetDocument() const
   { return reinterpret_cast<CSkinSelectorDoc*>(m_pDocument); }
#endif

