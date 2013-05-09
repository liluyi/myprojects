/***********************************************************************
 * OpenCV 2.0 测试例程
 * 于仕琪 提供
 ***********************************************************************/
 
#include "stdafx.h"
#include "highgui.h"
 
//所有的以新风格命名的函数都在 cv 命名空间中
//如果希望不要每次都输入 cv:: ，则可使用下面语句
//using namespace cv;
 
int _tmain(int argc, _TCHAR* argv[])
{
 
	const char* imagename = "D:/pictures/lost.jpg";
 
	cv::Mat img = cv::imread(imagename); // Matlab风格的 cvLoadImage 函数的另一种调用
    if(img.empty())
    {
        fprintf(stderr, "Can not load image %s\n", imagename);
        return -1;
    }
 
    if( !img.data ) // 检查是否正确载入图像
        return -1;
 
	cv::namedWindow("image", CV_WINDOW_AUTOSIZE); //创建窗口
	cv::imshow("image", img); //显示图像
 
	cv::waitKey();
 
	return 0;
}