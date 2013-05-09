#include "StdAfx.h"
#include "Graph.h"

/*CGraph::CGraph(void)
: m_nDrawType(0)
{
}
*/
CGraph::CGraph(void)
{
}
CGraph::CGraph(CPoint m_ptOrigin,CPoint m_ptEnd)
{
	//this->m_nDrawType=m_nDrawType;
	this->m_ptOrigin=m_ptOrigin;
	this->m_ptEnd=m_ptEnd;
}


CGraph::~CGraph(void)
{
}
