// videoin.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "highgui.h"
#include "cv.h"
 
 
int g_slider_position =0;//全局变量：获取滚动条位置
CvCapture* g_capture=NULL;

void onTrackbarSlide(int pos){
	cvSetCaptureProperty(
		g_capture,
		CV_CAP_PROP_POS_FRAMES,
		pos);
}

int _tmain(int argc, _TCHAR* argv[])
{
 
	const char* videoname ="D:/Movies/Pandorum.DVDRip.XviD-DoNE/Sample/pandorum-done-sample.avi";//"D:/Series/Greys.Anatomy.S06E17.HDTV.XviD-2HD.avi";
 
	cv::namedWindow(videoname, CV_WINDOW_AUTOSIZE); //创建窗口
	g_capture=cvCreateFileCapture(videoname);

	int frames=(int) cvGetCaptureProperty(
		g_capture,
		CV_CAP_PROP_FRAME_COUNT
		);
	if(frames!=0){
		cvCreateTrackbar(
			"Position",
			videoname,
			&g_slider_position,
			frames,
			onTrackbarSlide
			);
	}

	IplImage* frame;

	if(g_capture==NULL)
	{
		printf("未能载入视频文件\n");
		return -1;
	}
	while(1){
		frame=cvQueryFrame(g_capture);
		if(!frame)
		{
			break;
		}
		cvShowImage(videoname,frame);
		int pos=(int) cvGetCaptureProperty(
		g_capture,
		CV_CAP_PROP_POS_FRAMES
		);
		cvSetTrackbarPos( "Position", videoname, pos );
		char c=cvWaitKey(33);
		if(c==27)
			break;
	}
	cvReleaseCapture(&g_capture);
	cvDestroyWindow("image");
	
	return 0;
}