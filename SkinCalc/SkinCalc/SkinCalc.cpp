// SkinCalc.cpp : �������̨Ӧ�ó������ڵ㡣
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

	int r_bins = 256, g_bins = 256,b_bins=256;//RGB�����ֱ𻮷�Ϊ256���ȼ� 
	int hist_size[] = {r_bins, g_bins, b_bins};
	
	float r_ranges[] = { 0, 255 }; //RGB���������ı仯��Χ 
	float g_ranges[] = { 0, 255 };
	float b_ranges[] = { 0, 255 };
	float* ranges[] = { r_ranges, g_ranges ,b_ranges};
	int count=0;

	/** ����ֱ��ͼ����ά, ÿ��ά���Ͼ��� */
	CvHistogram * hist = cvCreateHist( 3, hist_size, CV_HIST_ARRAY, ranges, 1 );
	
	ifstream maskfile,skinfile;
	char *mst=new char[20];
	//char *sst=new char[20];
	maskfile.open("E:/FaceDetection/mask");
	//skinfile.open("E:/FaceDetection/skin");
	if(maskfile==NULL)
	{
		printf("�޷����ļ���");
		exit(0);
	}


	while(maskfile.getline(mst,20,'\n'))
	{	
		char *maskpath=(char*)malloc(strlen(mst)+28);//����list�ж������ļ���ƴ�ӳ�ͼƬ�ļ��������ַ
		char *skinpath=(char*)malloc(strlen(mst)+34);
				
		strcpy(maskpath,"E:/FaceDetection/masks/");
		strcat(maskpath,mst);
		strcat(maskpath,".pbm");

		strcpy(skinpath,"E:/FaceDetection/skin-images/");
		strcat(skinpath,mst);
		strcat(skinpath,".jpg");

		printf("%s:%s\n",maskpath,skinpath);

	//��ʼ����ͼƬ

	src= cvLoadImage(skinpath);//�ļ�·��Ҫ��/������\������ᱻ����Ϊ��ת���ַ�
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
	
	/** ����ͼ��ת����RGB��ɫ�ռ� */
	cvCvtPixToPlane( src, r_plane, g_plane, b_plane, 0 );
 
	
	/** ����RGB����ƽ������ͳ��ֱ��ͼ */
	
	
	if(count==1)
	{
		printf("��һ��");
	cvCalcHist( planes, hist, 0, srcmask );
	}
	else
	{
		printf("��ʼ�ۼ�%d--",count);
	cvCalcHist(planes,hist,1,srcmask); //һ��Ҫ�ȳ�ʼ��hist��Ҳ��ʵ�ֽ�����һ���ѽ��й�ͳ�Ƶ�ֱ��ͼ֮���ٽ��е����ſ���
	}
	/** ��ȡֱ��ͼͳ�Ƶ����ֵ�����ڶ�̬��ʾֱ��ͼ */
	float max_value;
	cvGetMinMaxHistValue( hist, 0, &max_value, 0, 0 );
 
	printf("%f\n",max_value);

	/*cvNamedWindow( "Source", CV_WINDOW_AUTOSIZE );
	cvShowImage( "Source", src );

	cvNamedWindow( "Mask", CV_WINDOW_AUTOSIZE );
	cvShowImage( "Mask", srcmask );

	cvNamedWindow( "R����", CV_WINDOW_AUTOSIZE );
	cvShowImage( "R����", r_plane );
	cvNamedWindow( "G����", CV_WINDOW_AUTOSIZE );
	cvShowImage( "G����", g_plane );
	cvNamedWindow( "B����", CV_WINDOW_AUTOSIZE );
	cvShowImage( "B����", b_plane );*/

	//if(count==1)
//	{
		for(int r = 0; r< r_bins; r++)
	{
		for(int g = 0; g < g_bins; g++)
		{
			for( int b=0;b<b_bins;b++)
			{

		    float bin_val = cvQueryHistValue_3D( hist, r, g, b );
			//int intensity = cvRound(bin_val*height/max_value);//cvRound�ǰ�longת��int����������

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
			//int intensity = cvRound(bin_val*height/max_value);//cvRound�ǰ�longת��int����������

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

