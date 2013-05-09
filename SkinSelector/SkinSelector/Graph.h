#pragma once

class CGraph
{
public:
	CGraph(void);
	CGraph(CPoint ,CPoint);
	~CGraph(void);
	UINT m_nDrawType;
	CPoint m_ptOrigin;
	CPoint m_ptEnd;
};
