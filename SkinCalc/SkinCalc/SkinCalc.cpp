// SkinCalc.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <cv.h>
#include <highgui.h>
#include <iostream>
#include <fstream>
#include <sql.h>
#include<sqltypes.h>
#include<sqlext.h>
using namespace std;


int _tmain(int argc, _TCHAR* argv[])
{
	IplImage * src;
	IplImage * srcmask;

	int r_bins = 256, g_bins = 256,b_bins=256;//RGB分量分别划分为256个等级 
	int hist_size[] = {r_bins, g_bins, b_bins};
	
	float r_ranges[] = { 0, 255 }; //RGB各个分量的变化范围 
	float g_ranges[] = { 0, 255 };
	float b_ranges[] = { 0, 255 };
	float* ranges[] = { r_ranges, g_ranges ,b_ranges};
	int count=0;

	/** 创建直方图，三维, 每个维度上均分 */
	CvHistogram * hist = cvCreateHist( 3, hist_size, CV_HIST_ARRAY, ranges, 1 );
	
	ifstream maskfile,skinfile;
	char *mst=new char[20];
	//char *sst=new char[20];
	maskfile.open("E:/FaceDetection/mask");
	//skinfile.open("E:/FaceDetection/skin");
	if(maskfile==NULL)
	{
		printf("无法打开文件！");
		exit(0);
	}


	while(maskfile.getline(mst,20,'\n'))
	{	
		char *maskpath=(char*)malloc(strlen(mst)+28);//将从list中读出的文件名拼接成图片文件的物理地址
		char *skinpath=(char*)malloc(strlen(mst)+34);
				
		strcpy(maskpath,"E:/FaceDetection/masks/");
		strcat(maskpath,mst);
		strcat(maskpath,".pbm");

		strcpy(skinpath,"E:/FaceDetection/skin-images/");
		strcat(skinpath,mst);
		strcat(skinpath,".jpg");

		printf("%s:%s\n",maskpath,skinpath);

	//开始导入图片

	src= cvLoadImage(skinpath);//文件路径要用/而不是\，否则会被误认为是转义字符
	srcmask=cvLoadImage(maskpath,CV_LOAD_IMAGE_ANYCOLOR);


	/*cvNamedWindow( "src", CV_WINDOW_AUTOSIZE );
	cvShowImage( "src", src );
	cvNamedWindow( "srcmask", CV_WINDOW_AUTOSIZE );
	cvShowImage( "srcmask", srcmask);*/

	if(src!=NULL&&srcmask!=NULL&&src->height==srcmask->height&&src->width==srcmask->width)
	{
	count++;

	IplImage* r_plane = cvCreateImage( cvGetSize(src), 8, 1 );
	IplImage* g_plane = cvCreateImage( cvGetSize(src), 8, 1 );
	IplImage* b_plane = cvCreateImage( cvGetSize(src), 8, 1 );
	IplImage* planes[] = { r_plane, g_plane, b_plane };
 
	
	//rgb=cvCreateImage(cvGetSize(src),8,3);
	
	/** 输入图像转换到RGB颜色空间 */
	cvCvtPixToPlane( src, r_plane, g_plane, b_plane, 0 );
 
	
	/** 根据RGB三个平面数据统计直方图 */
	
	
	if(count==1)
	{
		printf("第一次");
	cvCalcHist( planes, hist, 0, srcmask );
	}
	else
	{
		printf("开始累计%d--",count);
	cvCalcHist(planes,hist,1,srcmask); //一定要先初始化hist，也就实现建立好一个已进行过统计的直方图之后再进行迭代才可以
	}
	/** 获取直方图统计的最大值，用于动态显示直方图 */
	float max_value;
	cvGetMinMaxHistValue( hist, 0, &max_value, 0, 0 );
 
	printf("%f\n",max_value);

	/*cvNamedWindow( "Source", CV_WINDOW_AUTOSIZE );
	cvShowImage( "Source", src );

	cvNamedWindow( "Mask", CV_WINDOW_AUTOSIZE );
	cvShowImage( "Mask", srcmask );

	cvNamedWindow( "R分量", CV_WINDOW_AUTOSIZE );
	cvShowImage( "R分量", r_plane );
	cvNamedWindow( "G分量", CV_WINDOW_AUTOSIZE );
	cvShowImage( "G分量", g_plane );
	cvNamedWindow( "B分量", CV_WINDOW_AUTOSIZE );
	cvShowImage( "B分量", b_plane );*/

	//if(count==1)
//	{
		for(int r = 0; r< r_bins; r++)
	{
		for(int g = 0; g < g_bins; g++)
		{
			for( int b=0;b<b_bins;b++)
			{

		    float bin_val = cvQueryHistValue_3D( hist, r, g, b );
			//int intensity = cvRound(bin_val*height/max_value);//cvRound是把long转成int，四舍五入

	//		if(bin_val>=0)
				//printf("(%3d,%3d,%3d):%f\n",r,g,b,bin_val);
 
			}
		}
	}
 
		//break;
//	}

	cvReleaseImage(&r_plane);
	cvReleaseImage(&g_plane);
	cvReleaseImage(&b_plane);

	free(skinpath);
	free(maskpath);
}

	}

	float bin_val=0;
	for(int r = 0; r< r_bins; r++)
	{
		for(int g = 0; g < g_bins; g++)
		{
			for( int b=0;b<b_bins;b++)
			{

		    bin_val += cvQueryHistValue_3D( hist, r, g, b );
			//int intensity = cvRound(bin_val*height/max_value);//cvRound是把long转成int，四舍五入

			//if(bin_val>=0)
				//printf("(%3d,%3d,%3d):%f\n",r,g,b,bin_val);
 
			}
		}
	}

	CvFileStorage* fs=cvOpenFileStorage("D:/projects/skin-data.xml",0,CV_STORAGE_WRITE);
	cvWriteReal(fs,"total_count",bin_val);
	cvWrite(fs,"Skin_Color_Histogram",hist);
	cvReleaseFileStorage(&fs);

	//cvSave("D:/projects/skin-data.xml",hist);
	cvWaitKey(0);
return 0;
}

