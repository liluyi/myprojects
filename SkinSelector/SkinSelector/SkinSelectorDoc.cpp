
// SkinSelectorDoc.cpp : CSkinSelectorDoc 类的实现
//

#include "stdafx.h"
#include "SkinSelector.h"

#include "SkinSelectorDoc.h"
#include "Graph.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CSkinSelectorDoc

IMPLEMENT_DYNCREATE(CSkinSelectorDoc, CDocument)

BEGIN_MESSAGE_MAP(CSkinSelectorDoc, CDocument)
END_MESSAGE_MAP()


// CSkinSelectorDoc 构造/析构

CSkinSelectorDoc::CSkinSelectorDoc()
{
	// TODO: 在此添加一次性构造代码
	m_imLoaded=FALSE;
	count=0;

}

CSkinSelectorDoc::~CSkinSelectorDoc()
{
}

BOOL CSkinSelectorDoc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;

	// TODO: 在此添加重新初始化代码
	// (SDI 文档将重用该文档)

	return TRUE;
}




// CSkinSelectorDoc 序列化

void CSkinSelectorDoc::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		// TODO: 在此添加存储代码
	}
	else
	{
		// TODO: 在此添加加载代码
	}
}


// CSkinSelectorDoc 诊断

#ifdef _DEBUG
void CSkinSelectorDoc::AssertValid() const
{
	CDocument::AssertValid();
}

void CSkinSelectorDoc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
#endif //_DEBUG


// CSkinSelectorDoc 命令

BOOL CSkinSelectorDoc::OnOpenDocument(LPCTSTR lpszPathName)
{
	if (!CDocument::OnOpenDocument(lpszPathName)) return FALSE;
    // TODO: Add your specialized creation code here
    m_image.Load(lpszPathName);
	ip_image=cvLoadImage(lpszPathName,1);
	if(ip_image)
	{
		count++;
		CString filepath=NULL;
		const char * filename;
		filepath.Format("D:/%05d.jpg", count); 
		cvSaveImage(filepath,ip_image,0);

		m_imLoaded=TRUE;
		if(count>1)
			m_picChange=TRUE;

		CvMemStorage * storage = cvCreateMemStorage(0);
		mask_image = NULL;
		
		CvSeq * contour = 0;
	
		//cvNamedWindow("contour",1);

		mask_image = cvCreateImage(cvGetSize(ip_image),8,1);
        //copy source image and convert it to BGR image
		cvCvtColor(ip_image, mask_image, CV_BGR2GRAY);
		cvNamedWindow("灰度图像",1);
		cvShowImage("灰度图像",mask_image);

		IplImage* tempimg=NULL;
		tempimg=cvCreateImage(cvGetSize(ip_image),8,1);
		cvCopy(mask_image,tempimg,0);

		cvThreshold(mask_image, mask_image,128,255,CV_THRESH_BINARY);
		//cvAdaptiveThreshold(tempimg,mask_image,255,CV_ADAPTIVE_THRESH_MEAN_C,CV_THRESH_BINARY,3,5);
		cvNamedWindow("二值化图像",1);
		cvShowImage("二值化图像",mask_image);
 
      //查找contour
		cvFindContours( mask_image, storage, &contour, sizeof(CvContour), CV_RETR_TREE,CV_CHAIN_APPROX_SIMPLE);
 
		//将轮廓画出   
		cvDrawContours(mask_image, contour,cvScalarAll(255),cvScalarAll(255),100);
	}
	else
		MessageBox(NULL,"打开的非图像文件！","注意！",0);

    return TRUE;
}

BOOL CSkinSelectorDoc::OnSaveDocument(LPCTSTR lpszPathName)
{
	m_image.Save(lpszPathName);
	for(int m=0;m<ip_image->height;m++)
	{
		for(int n=0;n<ip_image->width;n++)
		{
				//cvSet2D(pDoc->finalimg,m,n,cvScalarAll(0));//
			if(cvGet2D(mask_image,m,n).val[0]==255)
				cvSet2D(mask_image,m,n,cvScalarAll(0));
			/*for(int i=0;i<m_ptrArray.GetSize();i++)
			{
				if(CRect(((CGraph*)m_ptrArray.GetAt(i))->m_ptOrigin,((CGraph*)m_ptrArray.GetAt(i))->m_ptEnd).PtInRect(CPoint(m,n)))
					cvSet2D(mask_image,m,n,cvScalarAll(255));
			}*/
		}
	}
		for(int m=0;m<ip_image->height;m++)
		{
			for(int n=0;n<ip_image->width;n++)
			{
				if(cvGet2D(mask_image,m,n).val[0]==100)
					cvSet2D(mask_image,m,n,cvScalarAll(255));
			}
		}
			
		CString filepath;
		filepath.Format("D:/%05d.pbm", count); 
	    //cvSaveImage("D:/mask.pbm",mask_image,0);
		cvSaveImage(filepath,mask_image,0);
	
    return TRUE;
}
