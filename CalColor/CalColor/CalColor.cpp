// CalColor.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "cv.h"
#include "highgui.h"
#include "iostream"
using namespace std;

int _tmain(int argc, _TCHAR* argv[])//分别显示RGb
{
	IplImage * src= cvLoadImage("D:/pictures/movies/avatar.jpg");//("D:/pictures/lost.jpg");
	//IplImage * src= cvLoadImage("E:/masks/2378.pbm");
 
	//IplImage* hsv = cvCreateImage( cvGetSize(src), 8, 3 );
	
	/*IplImage* h_plane = cvCreateImage( cvGetSize(src), 8, 1 );
	IplImage* s_plane = cvCreateImage( cvGetSize(src), 8, 1 );
	IplImage* v_plane = cvCreateImage( cvGetSize(src), 8, 1 );
	IplImage* planes[] = { h_plane, s_plane };*/

	IplImage* r_plane = cvCreateImage( cvGetSize(src), 8, 1 );
	IplImage* g_plane = cvCreateImage( cvGetSize(src), 8, 1 );
	IplImage* b_plane = cvCreateImage( cvGetSize(src), 8, 1 );
	IplImage* planes1[] = { r_plane, g_plane};
	IplImage* planes2[] = { g_plane, b_plane};
	IplImage* planes3[] = { r_plane, b_plane};
 
	/** H 分量划分为16个等级，S分量划分为8个等级 */
	/*int h_bins = 16, s_bins = 8;
	int hist_size[] = {h_bins, s_bins};*/
	int r_bins = 16, g_bins = 16, b_bins=16;
	int hist_size[] = {r_bins, g_bins};
 
	/** H 分量的变化范围 */
	//float h_ranges[] = { 0, 180 }; 
	float r_ranges[] = { 0, 255 }; 
 
	/** S 分量的变化范围*/
	//float s_ranges[] = { 0, 255 };
	float g_ranges[] = { 0, 255 };
	float b_ranges[] = { 0, 255 };
	//float* ranges[] = { h_ranges, s_ranges };
	float* ranges[] = { r_ranges, g_ranges};
 
	/** 输入图像转换到HSV颜色空间 */
	//cvCvtColor( src, hsv, CV_BGR2HSV );
	//cvCvtPixToPlane( hsv, h_plane, s_plane, v_plane, 0 );
	cvCvtPixToPlane( src, r_plane, g_plane, b_plane, 0 );
 
	/** 创建直方图，二维, 每个维度上均分 */
	//CvHistogram * hist = cvCreateHist( 2, hist_size, CV_HIST_ARRAY, ranges, 1 );
	CvHistogram * hist1 = cvCreateHist( 2, hist_size, CV_HIST_ARRAY, ranges, 1 );
	CvHistogram * hist2 = cvCreateHist( 2, hist_size, CV_HIST_ARRAY, ranges, 1 );
	CvHistogram * hist3 = cvCreateHist( 2, hist_size, CV_HIST_ARRAY, ranges, 1 );
	/** 根据H,S两个平面数据统计直方图 */
	cvCalcHist( planes1, hist1, 0, 0 );
	cvCalcHist( planes2, hist2, 0, 0 );
	cvCalcHist( planes3, hist3, 0, 0 );
 
	/** 获取直方图统计的最大值，用于动态显示直方图 */
	float max_value1,max_value2,max_value3;
	cvGetMinMaxHistValue( hist1, 0, &max_value1, 0, 0 );
	cvGetMinMaxHistValue( hist2, 0, &max_value2, 0, 0 );
	cvGetMinMaxHistValue( hist3, 0, &max_value3, 0, 0 );
 
 
	/** 设置直方图显示图像 */
	/*int height = 240;
	int width = (h_bins*s_bins*6);*/
	int height = 600;
	int width = (r_bins*g_bins*4);
	IplImage* hist_img1 = cvCreateImage( cvSize(width,height), 8, 3 );
	IplImage* hist_img2 = cvCreateImage( cvSize(width,height), 8, 3 );
	IplImage* hist_img3 = cvCreateImage( cvSize(width,height), 8, 3 );
	cvZero( hist_img1 );
	cvZero( hist_img2 );
	cvZero( hist_img3 );
 
	/** 用来进行HSV到RGB颜色转换的临时单位图像 */
	//IplImage * hsv_color = cvCreateImage(cvSize(1,1),8,3);
	IplImage * rgb_color1 = cvCreateImage(cvSize(1,1),8,3);
	int bin_w = width / (r_bins * r_bins);
	for(int r = 0; r < r_bins; r++)
	{
		for(int g = 0; g < g_bins; g++)
		{
			int i = r*g_bins + g;
			/** 获得直方图中的统计次数，计算显示在图像中的高度 */
			float bin_val = cvQueryHistValue_2D( hist1, r, g );
			int intensity = cvRound(bin_val*height/max_value1);
 
			/** 获得当前直方图代表的颜色，转换成RGB用于绘制 */
			//cvSet2D(rgb_color1,0,0,cvScalar(r*255.f / r_bins,g*255.f/g_bins,255,0));
			cvSet2D(rgb_color1,0,0,cvScalar(r,g,255,0));
			//cvCvtColor(hsv_color,rgb_color,CV_HSV2BGR);
			CvScalar color = cvGet2D(rgb_color1,0,0);
 
			cvRectangle( hist_img1, cvPoint(i*bin_w,height),
				cvPoint((i+1)*bin_w,height - intensity),
				color, -1, 8, 0 );
		}
	}

	IplImage * rgb_color2 = cvCreateImage(cvSize(1,1),8,3);
	bin_w = width / (r_bins * r_bins);
	for(int g = 0; g < g_bins; g++)
	{
		for(int b = 0; b < b_bins; b++)
		{
			int i = g*b_bins + b;
			/** 获得直方图中的统计次数，计算显示在图像中的高度 */
			float bin_val = cvQueryHistValue_2D( hist2, g, b );
			int intensity = cvRound(bin_val*height/max_value2);
 
			/** 获得当前直方图代表的颜色，转换成RGB用于绘制 */
			cvSet2D(rgb_color2,0,0,cvScalar(g*255.f / g_bins,b*255.f/b_bins,255,0));
			//cvCvtColor(hsv_color,rgb_color,CV_HSV2BGR);
			CvScalar color = cvGet2D(rgb_color2,0,0);
 
			cvRectangle( hist_img2, cvPoint(i*bin_w,height),
				cvPoint((i+1)*bin_w,height - intensity),
				color, -1, 8, 0 );
		}
	}

	IplImage * rgb_color3 = cvCreateImage(cvSize(1,1),8,3);
	bin_w = width / (r_bins * r_bins);
	for(int r = 0; r < r_bins; r++)
	{
		for(int b = 0; b < b_bins; b++)
		{
			int i = r*b_bins + b;
			/** 获得直方图中的统计次数，计算显示在图像中的高度 */
			float bin_val = cvQueryHistValue_2D( hist3, r, b );
			int intensity = cvRound(bin_val*height/max_value3);
 
			/** 获得当前直方图代表的颜色，转换成RGB用于绘制 */
			cvSet2D(rgb_color3,0,0,cvScalar(r*255.f / r_bins,b*255.f/b_bins,255,0));
			//cvCvtColor(hsv_color,rgb_color,CV_HSV2BGR);
			CvScalar color = cvGet2D(rgb_color3,0,0);
 
			cvRectangle( hist_img3, cvPoint(i*bin_w,height),
				cvPoint((i+1)*bin_w,height - intensity),
				color, -1, 8, 0 );
		}
	}
 
	cvNamedWindow( "源文件", CV_WINDOW_AUTOSIZE );
	cvShowImage( "源文件", src );
 
	cvNamedWindow( "R-G直方图", CV_WINDOW_AUTOSIZE );
	cvShowImage( "R-G直方图", hist_img1 );
	cvNamedWindow( "G-B直方图", CV_WINDOW_AUTOSIZE );
	cvShowImage( "G-B直方图", hist_img2 );
	cvNamedWindow( "R-B直方图", CV_WINDOW_AUTOSIZE );
	cvShowImage( "R-B直方图", hist_img3 );
 
	cvWaitKey(0);
return 0;
}


