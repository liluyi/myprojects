
// FaceDetectionDoc.cpp : CFaceDetectionDoc ���ʵ��
//

#include "stdafx.h"
#include "FaceDetection.h"

#include "FaceDetectionDoc.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CFaceDetectionDoc

IMPLEMENT_DYNCREATE(CFaceDetectionDoc, CDocument)

BEGIN_MESSAGE_MAP(CFaceDetectionDoc, CDocument)
	ON_COMMAND(ID_32774, &CFaceDetectionDoc::On32774)
	ON_COMMAND(ID_IMPORT_CLASSIFIER, &CFaceDetectionDoc::OnImportClassifier)
END_MESSAGE_MAP()


// CFaceDetectionDoc ����/����

CFaceDetectionDoc::CFaceDetectionDoc()
{
	// TODO: �ڴ����һ���Թ������
	isImLoad=FALSE;
	isCasLoad=FALSE;
	isVidLoad=FALSE;

}

CFaceDetectionDoc::~CFaceDetectionDoc()
{
}

BOOL CFaceDetectionDoc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;

	// TODO: �ڴ�������³�ʼ������
	// (SDI �ĵ������ø��ĵ�)

	return TRUE;
}




// CFaceDetectionDoc ���л�

void CFaceDetectionDoc::Serialize(CArchive& ar)
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


// CFaceDetectionDoc ���

#ifdef _DEBUG
void CFaceDetectionDoc::AssertValid() const
{
	CDocument::AssertValid();
}

void CFaceDetectionDoc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
#endif //_DEBUG


// CFaceDetectionDoc ����

BOOL CFaceDetectionDoc::OnOpenDocument(LPCTSTR lpszPathName)
{
	if (!CDocument::OnOpenDocument(lpszPathName)) return FALSE;
    // TODO: Add your specialized creation code here
	//���ļ�
	isImLoad=FALSE;
	isVidLoad=FALSE;
	ip_image=0;
	capture=0;
	frame=0;
	frame_copy=0;
    m_image.Load(lpszPathName);
	ip_image=cvLoadImage(lpszPathName,1);
	
	if(ip_image)
		isImLoad=TRUE;
	else 
	{
		capture=cvCreateFileCapture(lpszPathName);
		if(capture)
			isVidLoad=TRUE;
		else
			MessageBox(NULL,"�򿪵ķ���Ƶ�ļ���","ע�⣡",0);
	}

	return TRUE;
}

void CFaceDetectionDoc::On32774()//����ѵ������
{
	// TODO: �ڴ���������������

}

void CFaceDetectionDoc::OnImportClassifier()
{
	// TODO: �ڴ���������������
	//�����ɫ������
	MessageBox(NULL,"��������ѵ���������Ժ򡭡�","��ʾ",MB_OK);

	int r_bins = 256, g_bins = 256,b_bins=256;//RGB�����ֱ𻮷�Ϊ256���ȼ� 
	int hist_size[] = {r_bins, g_bins, b_bins};
	
	float r_ranges[] = { 0, 255 }; //RGB���������ı仯��Χ 
	float g_ranges[] = { 0, 255 };
	float b_ranges[] = { 0, 255 };
	float* ranges[] = { r_ranges, g_ranges ,b_ranges};

	int nr_bins = 256, ng_bins = 256,nb_bins=256;//RGB�����ֱ𻮷�Ϊ256���ȼ� 
	int nhist_size[] = {nr_bins, ng_bins, nb_bins};
	
	float nr_ranges[] = { 0, 255 }; //RGB���������ı仯��Χ 
	float ng_ranges[] = { 0, 255 };
	float nb_ranges[] = { 0, 255 };
	float* nranges[] = { nr_ranges, ng_ranges ,nb_ranges};

	printf("���ڶ�ȡ��ɫֱ��ͼ�����Ժ򡭡�\n");
	CvFileStorage* sfs=cvOpenFileStorage("D:/projects/skin-data.xml",0,CV_STORAGE_READ);
	skintotal=cvReadRealByName(sfs,0,"total_count");

	skinhist=cvCreateHist( 3, hist_size, CV_HIST_ARRAY, ranges, 1 );
	skinhist=(CvHistogram*)cvReadByName(sfs,0,"Skin_Color_Histogram");
	printf("�ɹ������ɫֱ��ͼ��\n");
	
	cvReleaseFileStorage(&sfs);

	printf("���ڶ�ȡ�Ƿ�ɫֱ��ͼ�����Ժ򡭡�\n");
	CvFileStorage* nfs=cvOpenFileStorage("D:/projects/non-skin-data.xml",0,CV_STORAGE_READ);
	nonskintotal=cvReadRealByName(nfs,0,"total_count");
	nonskinhist=cvCreateHist( 3, nhist_size, CV_HIST_ARRAY, nranges, 1 );
	nonskinhist=(CvHistogram*)cvReadByName(nfs,0,"Non-Skin_Color_Histogram");
	printf("�ɹ�����Ƿ�ɫֱ��ͼ��\n");

	cvReleaseFileStorage(&nfs);
	isCasLoad=TRUE;

}
