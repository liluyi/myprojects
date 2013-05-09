
// FaceDetectionDoc.cpp : CFaceDetectionDoc 类的实现
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


// CFaceDetectionDoc 构造/析构

CFaceDetectionDoc::CFaceDetectionDoc()
{
	// TODO: 在此添加一次性构造代码
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

	// TODO: 在此添加重新初始化代码
	// (SDI 文档将重用该文档)

	return TRUE;
}




// CFaceDetectionDoc 序列化

void CFaceDetectionDoc::Serialize(CArchive& ar)
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


// CFaceDetectionDoc 诊断

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


// CFaceDetectionDoc 命令

BOOL CFaceDetectionDoc::OnOpenDocument(LPCTSTR lpszPathName)
{
	if (!CDocument::OnOpenDocument(lpszPathName)) return FALSE;
    // TODO: Add your specialized creation code here
	//打开文件
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
			MessageBox(NULL,"打开的非视频文件！","注意！",0);
	}

	return TRUE;
}

void CFaceDetectionDoc::On32774()//载入训练数据
{
	// TODO: 在此添加命令处理程序代码

}

void CFaceDetectionDoc::OnImportClassifier()
{
	// TODO: 在此添加命令处理程序代码
	//载入肤色分类器
	MessageBox(NULL,"正在载入训练集，请稍候……","提示",MB_OK);

	int r_bins = 256, g_bins = 256,b_bins=256;//RGB分量分别划分为256个等级 
	int hist_size[] = {r_bins, g_bins, b_bins};
	
	float r_ranges[] = { 0, 255 }; //RGB各个分量的变化范围 
	float g_ranges[] = { 0, 255 };
	float b_ranges[] = { 0, 255 };
	float* ranges[] = { r_ranges, g_ranges ,b_ranges};

	int nr_bins = 256, ng_bins = 256,nb_bins=256;//RGB分量分别划分为256个等级 
	int nhist_size[] = {nr_bins, ng_bins, nb_bins};
	
	float nr_ranges[] = { 0, 255 }; //RGB各个分量的变化范围 
	float ng_ranges[] = { 0, 255 };
	float nb_ranges[] = { 0, 255 };
	float* nranges[] = { nr_ranges, ng_ranges ,nb_ranges};

	printf("正在读取肤色直方图，请稍候……\n");
	CvFileStorage* sfs=cvOpenFileStorage("D:/projects/skin-data.xml",0,CV_STORAGE_READ);
	skintotal=cvReadRealByName(sfs,0,"total_count");

	skinhist=cvCreateHist( 3, hist_size, CV_HIST_ARRAY, ranges, 1 );
	skinhist=(CvHistogram*)cvReadByName(sfs,0,"Skin_Color_Histogram");
	printf("成功读入肤色直方图！\n");
	
	cvReleaseFileStorage(&sfs);

	printf("正在读取非肤色直方图，请稍候……\n");
	CvFileStorage* nfs=cvOpenFileStorage("D:/projects/non-skin-data.xml",0,CV_STORAGE_READ);
	nonskintotal=cvReadRealByName(nfs,0,"total_count");
	nonskinhist=cvCreateHist( 3, nhist_size, CV_HIST_ARRAY, nranges, 1 );
	nonskinhist=(CvHistogram*)cvReadByName(nfs,0,"Non-Skin_Color_Histogram");
	printf("成功读入非肤色直方图！\n");

	cvReleaseFileStorage(&nfs);
	isCasLoad=TRUE;

}
