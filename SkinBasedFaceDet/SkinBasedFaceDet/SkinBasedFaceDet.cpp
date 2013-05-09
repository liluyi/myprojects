// SkinBasedFaceDet.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"


#include "cv.h"
#include "highgui.h"
 
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <assert.h>
#include <math.h>
#include <float.h>
#include <limits.h>
#include <time.h>
#include <ctype.h>

static CvMemStorage* storage = 0;
static CvHaarClassifierCascade* cascade = 0;
static CvHaarClassifierCascade* profilecascade = 0;
 
const char* cascade_name =
    "haarcascade_frontalface_alt.xml";
/*    "haarcascade_profileface.xml";*/
const char* profilecascade_name =
    "haarcascade_frontalface_alt.xml";


int _tmain(int argc, _TCHAR* argv[])
{
	IplImage *src;
	
	CvScalar s;
	int r,g,b;

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
	float skintotal=cvReadRealByName(sfs,0,"total_count");
	CvHistogram* skinhist=cvCreateHist( 3, hist_size, CV_HIST_ARRAY, ranges, 1 );
	skinhist=(CvHistogram*)cvReadByName(sfs,0,"Skin_Color_Histogram");
	printf("成功读入肤色直方图！\n");
	
	cvReleaseFileStorage(&sfs);

	printf("正在读取非肤色直方图，请稍候……\n");
	CvFileStorage* nfs=cvOpenFileStorage("D:/projects/non-skin-data.xml",0,CV_STORAGE_READ);
	float nonskintotal=cvReadRealByName(nfs,0,"total_count");
	CvHistogram* nonskinhist=cvCreateHist( 3, nhist_size, CV_HIST_ARRAY, nranges, 1 );
	nonskinhist=(CvHistogram*)cvReadByName(nfs,0,"Non-Skin_Color_Histogram");
	printf("成功读入非肤色直方图！\n");

	cvReleaseFileStorage(&nfs);

	//src=cvLoadImage("D:/photo/李璐祎2.jpg");
	//src=cvLoadImage("D:/photo/superwe/DSC02036.JPG");
	//src=cvLoadImage("D:/photo/奥运/large_qKeg_16960b206099.jpg");
	src=cvLoadImage("D:/photo/重庆/large_vH8C_4603n200066.jpg");

	cvNamedWindow( "原始图片", CV_WINDOW_AUTOSIZE );
	cvShowImage( "原始图片", src );

	printf("正在进行脸部检测，请稍候……\n");

	float skin_bin_val=0;
	float nonskin_bin_val=0;

	IplImage *dst=cvCreateImage( cvGetSize(src), 8, 3 );
	IplImage *dilateim=cvCreateImage( cvGetSize(src), 8, 3 );
	IplImage *erodeim=cvCreateImage( cvGetSize(src), 8, 3 );


	for(int x=0;x<src->height;x++)
	{
		for(int y=0;y<src->width;y++)
		{
			cvSet2D(dst,x,y,cvScalar(0,0,0));
		}
	}

	for(int x=0;x<src->height;x++)
	{
		for(int y=0;y<src->width;y++)
		{
			s=cvGet2D(src,x,y);
			r=s.val[0];
			g=s.val[1];
			b=s.val[2];
			skin_bin_val = cvQueryHistValue_3D( skinhist, r, g, b );
			nonskin_bin_val = cvQueryHistValue_3D( nonskinhist, r, g, b );
			float p_skin=skin_bin_val/skintotal;
			float p_nonskin=nonskin_bin_val/nonskintotal;

			if(p_skin/p_nonskin>=0.8)
				cvSet2D(dst,x,y,cvScalar(255,255,255));
		}
	}

///////////////////////////////
	cascade_name = "D:/Program Files/OpenCV2.0/data/haarcascades/haarcascade_frontalface_alt2.xml";//haarcascade_profileface.xml
	profilecascade_name = "D:/Program Files/OpenCV2.0/data/haarcascades/haarcascade_profileface.xml";//haarcascade_profileface.xml
	cascade = (CvHaarClassifierCascade*)cvLoad( cascade_name, 0, 0, 0 );//加载训练集
	profilecascade = (CvHaarClassifierCascade*)cvLoad( cascade_name, 0, 0, 0 );//加载训练集
	storage = cvCreateMemStorage(0);
	cvNamedWindow( "haarresult", 1 );
	
	static CvScalar colors[] = 
    {
        {{0,0,255}},
        {{0,128,255}},
        {{0,255,255}},
        {{0,255,0}},
        {{255,128,0}},
        {{255,255,0}},
        {{255,0,0}},
        {{255,0,255}}
    };
 
    double scale = 1.3;
    IplImage* gray = cvCreateImage( cvSize(src->width,src->height), 8, 1 );
    IplImage* small_img = cvCreateImage( cvSize( cvRound (src->width/scale),
                         cvRound (src->height/scale)),
                     8, 1 );
    int i;
 
    cvCvtColor( src, gray, CV_BGR2GRAY );
    cvResize( gray, small_img, CV_INTER_LINEAR );
    cvEqualizeHist( small_img, small_img );
    cvClearMemStorage( storage );
 
    if( cascade )
    {
        double t = (double)cvGetTickCount();
        CvSeq* faces = cvHaarDetectObjects( small_img, cascade, storage,1.1, 2, 0/*CV_HAAR_DO_CANNY_PRUNING*/,cvSize(30, 30) );
		CvSeq* profilefaces = cvHaarDetectObjects( small_img, profilecascade, storage,1.1, 2, 0/*CV_HAAR_DO_CANNY_PRUNING*/,cvSize(30, 30) );
        t = (double)cvGetTickCount() - t;
        printf( "detection time = %gms\n", t/((double)cvGetTickFrequency()*1000.) );
        for( i = 0; i < (faces ? faces->total : 0); i++ )
        {
			int skincount=0;
            CvRect* r = (CvRect*)cvGetSeqElem( faces, i );
            /*CvPoint center;
            int radius;
            center.x = cvRound((r->x + r->width*0.5)*scale);
            center.y = cvRound((r->y + r->height*0.5)*scale);
            radius = cvRound((r->width + r->height)*0.25*scale);
            cvCircle( src, center, radius, colors[i%8], 3, 8, 0 );*/
			for(int a=(r->x)*scale;a<(r->x+r->width)*scale;a++)
				for(int b=(r->y)*scale;b<(r->y+r->height)*scale;b++)
				{
					s=cvGet2D(dst,b,a);
					if(s.val[0]==255)
						skincount++;
				}
			int totalarea=(r->width)*scale*(r->height)*scale;
			printf("%d,%d\n",skincount,totalarea);
			if((double)skincount/totalarea>0.7)
			{
				cvRectangle(dst,cvPoint((r->x)*scale,(r->y)*scale),cvPoint((r->x+r->width)*scale,(r->y+r->height)*scale),colors[i%8]);
			}
			cvRectangle(src,cvPoint((r->x)*scale,(r->y)*scale),cvPoint((r->x+r->width)*scale,(r->y+r->height)*scale),colors[i%8]);
        }

		for( i = 0; i < (profilefaces ? profilefaces->total : 0); i++ )
        {
			int skincount=0;
            CvRect* r = (CvRect*)cvGetSeqElem( profilefaces, i );
            /*CvPoint center;
            int radius;
            center.x = cvRound((r->x + r->width*0.5)*scale);
            center.y = cvRound((r->y + r->height*0.5)*scale);
            radius = cvRound((r->width + r->height)*0.25*scale);
            cvCircle( src, center, radius, colors[i%8], 3, 8, 0 );*/
			for(int a=(r->x)*scale;a<(r->x+r->width)*scale;a++)
				for(int b=(r->y)*scale;b<(r->y+r->height)*scale;b++)
				{
					s=cvGet2D(dst,b,a);
					if(s.val[0]==255)
						skincount++;
				}
			int totalarea=(r->width)*scale*(r->height)*scale;
			printf("%d,%d\n",skincount,totalarea);
			if((double)skincount/totalarea>0.7)
			{
				cvRectangle(dst,cvPoint((r->x)*scale,(r->y)*scale),cvPoint((r->x+r->width)*scale,(r->y+r->height)*scale),colors[i%8]);
			}
			//cvRectangle(src,cvPoint((r->x)*scale,(r->y)*scale),cvPoint((r->x+r->width)*scale,(r->y+r->height)*scale),colors[i%8]);
        }
    }

	
    
 
    cvShowImage( "haarresult", src );
    cvReleaseImage( &gray );
    cvReleaseImage( &small_img );

	////////////////////////////////
		cvNamedWindow( "检测结果", CV_WINDOW_AUTOSIZE );
		cvShowImage( "检测结果", dst );

		cvErode(dst,erodeim,0,1);
		cvDilate(erodeim,dilateim,0,1);

		cvNamedWindow( "腐蚀", CV_WINDOW_AUTOSIZE );
		cvShowImage( "腐蚀", erodeim );

		cvNamedWindow( "膨胀", CV_WINDOW_AUTOSIZE );
		cvShowImage( "膨胀", dilateim );

		printf("检测成功！\n");

		cvWaitKey(0);
		cvReleaseImage(&src);
		cvReleaseImage(&dst);
		cvReleaseImage(&dilateim);
		cvReleaseImage(&erodeim);


	return 0;
}

