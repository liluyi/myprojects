
// SkinSelectorDoc.cpp : CSkinSelectorDoc ���ʵ��
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


// CSkinSelectorDoc ����/����

CSkinSelectorDoc::CSkinSelectorDoc()
{
	// TODO: �ڴ����һ���Թ������
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

	// TODO: �ڴ�������³�ʼ������
	// (SDI �ĵ������ø��ĵ�)

	return TRUE;
}




// CSkinSelectorDoc ���л�

void CSkinSelectorDoc::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		// TODO: �ڴ���Ӵ洢����
	}
	else
	{
		// TODO: �ڴ���Ӽ��ش���
	}
}


// CSkinSelectorDoc ���

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


// CSkinSelectorDoc ����

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
		cvNamedWindow("�Ҷ�ͼ��",1);
		cvShowImage("�Ҷ�ͼ��",mask_image);

		IplImage* tempimg=NULL;
		tempimg=cvCreateImage(cvGetSize(ip_image),8,1);
		cvCopy(mask_image,tempimg,0);

		cvThreshold(mask_image, mask_image,128,255,CV_THRESH_BINARY);
		//cvAdaptiveThreshold(tempimg,mask_image,255,CV_ADAPTIVE_THRESH_MEAN_C,CV_THRESH_BINARY,3,5);
		cvNamedWindow("��ֵ��ͼ��",1);
		cvShowImage("��ֵ��ͼ��",mask_image);
 
      //����contour
		cvFindContours( mask_image, storage, &contour, sizeof(CvContour), CV_RETR_TREE,CV_CHAIN_APPROX_SIMPLE);
 
		//����������   
		cvDrawContours(mask_image, contour,cvScalarAll(255),cvScalarAll(255),100);
	}
	else
		MessageBox(NULL,"�򿪵ķ�ͼ���ļ���","ע�⣡",0);

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
