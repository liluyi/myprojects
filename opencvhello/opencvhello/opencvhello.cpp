/***********************************************************************
 * OpenCV 2.0 ��������
 * ������ �ṩ
 ***********************************************************************/
 
#include "stdafx.h"
#include "highgui.h"
 
//���е����·�������ĺ������� cv �����ռ���
//���ϣ����Ҫÿ�ζ����� cv:: �����ʹ���������
//using namespace cv;
 
int _tmain(int argc, _TCHAR* argv[])
{
 
	const char* imagename = "D:/pictures/lost.jpg";
 
	cv::Mat img = cv::imread(imagename); // Matlab���� cvLoadImage ��������һ�ֵ���
    if(img.empty())
    {
        fprintf(stderr, "Can not load image %s\n", imagename);
        return -1;
    }
 
    if( !img.data ) // ����Ƿ���ȷ����ͼ��
        return -1;
 
	cv::namedWindow("image", CV_WINDOW_AUTOSIZE); //��������
	cv::imshow("image", img); //��ʾͼ��
 
	cv::waitKey();
 
	return 0;
}