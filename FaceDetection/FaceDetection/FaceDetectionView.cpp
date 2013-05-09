
// FaceDetectionView.cpp : CFaceDetectionView 类的实现
//

#include "stdafx.h"
#include "FaceDetection.h"

#include "FaceDetectionDoc.h"
#include "FaceDetectionView.h"




#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CFaceDetectionView 

IMPLEMENT_DYNCREATE(CFaceDetectionView, CScrollView)

BEGIN_MESSAGE_MAP(CFaceDetectionView, CScrollView)
	// 标准打印命令
	ON_COMMAND(ID_FILE_PRINT, &CScrollView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, &CScrollView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, &CScrollView::OnFilePrintPreview)
	ON_COMMAND(ID_DETECT_FACE, &CFaceDetectionView::OnDetectFace)
END_MESSAGE_MAP()

// CFaceDetectionView 构造/析构

CFaceDetectionView::CFaceDetectionView()
{
	// TODO: 在此处添加构造代码

}

CFaceDetectionView::~CFaceDetectionView()
{
}

BOOL CFaceDetectionView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: 在此处通过修改
	//  CREATESTRUCT cs 来修改窗口类或样式

	SetScrollSizes ( MM_TEXT,CSize (0 , 0 ) );
	return CScrollView::PreCreateWindow(cs);
}

// CFaceDetectionView 绘制

void CFaceDetectionView::OnDraw(CDC* pDC)
{
	CFaceDetectionDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (!pDoc)
		return;

	// TODO: 在此处为本机数据添加绘制代码
	if(pDoc->isImLoad)
	{
	CvvImage & img = pDoc ->m_image;
	CRect rect;
	GetClientRect (&rect);
	rect.left=0;
	rect.top=0;
	//m_hDC = GetDC()->GetSafeHdc();
	m_hDC=pDC->GetSafeHdc();

	if(img.Width())
	{
	CSize sizeTotal;
	sizeTotal.cx=img.Width();
	sizeTotal.cy=img.Height();

	rect.right=img.Width();
	rect.bottom=img.Height();

	SetScrollSizes(MM_TEXT, sizeTotal);
		img.DrawToHDC(m_hDC ,rect);
	}
	}
	else if(pDoc->isVidLoad)
	{
		//while(1)
		{
			pDoc->frame=cvQueryFrame(pDoc->capture);
			//if(!pDoc->frame)
			//	break;
			//cvNamedWindow( "原始视频文件", CV_WINDOW_AUTOSIZE );
			//cvShowImage("原始视频文件",pDoc->frame);
			CvvImage img;
			img.CopyOf(pDoc->frame);
	CRect rect;
	GetClientRect (&rect);
	rect.left=0;
	rect.top=0;
	m_hDC=pDC->GetSafeHdc();

	if(img.Width())
	{
	CSize sizeTotal;
	sizeTotal.cx=img.Width();
	sizeTotal.cy=img.Height();

	rect.right=img.Width();
	rect.bottom=img.Height();

	SetScrollSizes(MM_TEXT, sizeTotal);
	img.DrawToHDC(m_hDC ,rect);}
		//	char c=cvWaitKey(33);
			//if(c==27)
			//	break;
		}
		//cvReleaseCapture(&pDoc->capture);
	
	}
}

void CFaceDetectionView::OnInitialUpdate()
{
	CScrollView::OnInitialUpdate();

	CSize sizeTotal;
	// TODO: 计算此视图的合计大小
	sizeTotal.cx = sizeTotal.cy = 100;
	SetScrollSizes(MM_TEXT, sizeTotal);
}


// CFaceDetectionView 打印

BOOL CFaceDetectionView::OnPreparePrinting(CPrintInfo* pInfo)
{
	// 默认准备
	return DoPreparePrinting(pInfo);
}

void CFaceDetectionView::OnBeginPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: 添加额外的打印前进行的初始化过程
}

void CFaceDetectionView::OnEndPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: 添加打印后进行的清理过程
}


// CFaceDetectionView 诊断

#ifdef _DEBUG
void CFaceDetectionView::AssertValid() const
{
	CScrollView::AssertValid();
}

void CFaceDetectionView::Dump(CDumpContext& dc) const
{
	CScrollView::Dump(dc);
}

CFaceDetectionDoc* CFaceDetectionView::GetDocument() const // 非调试版本是内联的
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CFaceDetectionDoc)));
	return (CFaceDetectionDoc*)m_pDocument;
}
#endif //_DEBUG


// CFaceDetectionView 消息处理程序

void CFaceDetectionView::OnDetectFace()
{
	// TODO: 在此添加命令处理程序代码
	CFaceDetectionDoc* pDoc = GetDocument();
	if((pDoc->isVidLoad||pDoc->isImLoad)&&pDoc->isCasLoad)//待检测文件以及分类器是否已经正确导入
	{
		if(pDoc->isImLoad)
		{
	IplImage *src=pDoc->ip_image;
	CvScalar s;
	int r,g,b;

	float skin_bin_val=0;
	float nonskin_bin_val=0;

	IplImage *dst=cvCreateImage( cvGetSize(src), 8, 3 );//目标文件
	IplImage *dilateim=cvCreateImage( cvGetSize(src), 8, 3 );//腐蚀文件
	IplImage *erodeim=cvCreateImage( cvGetSize(src), 8, 3 );//腐蚀膨胀后文件

	for(int x=0;x<src->height;x++)//目标文件背景设置为黑色
	{
		for(int y=0;y<src->width;y++)
		{
			cvSet2D(dst,x,y,cvScalar(0,0,0));
		}
	}

	for(int x=0;x<src->height;x++)//输入图片逐个像素扫描，确定为肤色像素后在结果图像中显示，其他像素点抛弃
	{
		for(int y=0;y<src->width;y++)
		{
			s=cvGet2D(src,x,y);
			r=s.val[0];
			g=s.val[1];
			b=s.val[2];
			skin_bin_val = cvQueryHistValue_3D( pDoc->skinhist, r, g, b );
			nonskin_bin_val = cvQueryHistValue_3D( pDoc->nonskinhist, r, g, b );
			float p_skin=skin_bin_val/pDoc->skintotal;
			float p_nonskin=nonskin_bin_val/pDoc->nonskintotal;

			if(p_skin/p_nonskin>=0.4)
				//cvSet2D(dst,x,y,cvScalar(255,255,255));
				cvSet2D(dst,x,y,cvScalar(r,g,b));
		}
	}

	    cvErode(dst,erodeim,0,2);//腐蚀噪点
		cvDilate(erodeim,dilateim,0,2);//腐蚀之后膨胀

	/*	CvMemStorage * storage = cvCreateMemStorage(0);
		
		CvSeq * contour = 0;

		IplImage* tempimg=NULL;
		tempimg=cvCreateImage(cvGetSize(dst),8,1);
		cvCvtColor(erodeim, tempimg, CV_BGR2GRAY);

		cvFindContours( tempimg, storage, &contour, sizeof(CvContour), CV_RETR_EXTERNAL,CV_CHAIN_APPROX_SIMPLE);

		

		double contour_area_temp=0,contour_area_max=0;   
        CvSeq * area_max_contour = 0 ;//指向面积最大的轮廓   
        CvSeq* c=0;   
        //printf( "Total Contours Detected: %d\n", Nc );   
		for(c=contour; c!=NULL; c=c->h_next )   
		{//寻找面积最大的轮廓，即循环结束时的area_max_contour   
			contour_area_temp = fabs(cvContourArea( c, CV_WHOLE_SEQ )); //获取当前轮廓面积   
			if( contour_area_temp > contour_area_max )   
			{   
				contour_area_max = contour_area_temp; //找到面积最大的轮廓   
				area_max_contour = c;//记录面积最大的轮廓   
			}   

			CvRect rect=cvBoundingRect(c,0);
		    cvRectangle(dst,cvPoint(rect.x,rect.y),cvPoint(rect.x+rect.width,rect.y+rect.height),cvScalar(0,255,0),3,8,0);
		}   */

		//CvRect rect=cvBoundingRect(area_max_contour,0);
		//cvRectangle(dst,cvPoint(rect.x,rect.y),cvPoint(rect.x+rect.width,rect.y+rect.height),cvScalar(255,0,0),3,8,0);

		//找出完整包含轮廓的最小矩形
		/*CvBox2D rect = cvMinAreaRect2(area_max_contour);
		//用cvBoxPoints找出矩形的4个顶点
		CvPoint2D32f rect_pts0[4];
		cvBoxPoints(rect, rect_pts0);
		//因为cvPolyLine要求点集的输入类型是CvPoint**,所以要把 CvPoint2D32f 型的 rect_pts0 转换为 CvPoint 型的 rect_pts,并赋予一个对应的指针 *pt
		int npts = 4;
		CvPoint rect_pts[4], *pt = rect_pts;
		for (int rp=0; rp<4; rp++)
			rect_pts[rp]= cvPointFrom32f(rect_pts0[rp]);
		//画出Box
		cvPolyLine(dst, &pt, &npts, 1, 1, CV_RGB(255,0,0), 2);*/


 
		//将轮廓画出   
		//cvDrawContours(dst, contour,cvScalarAll(255),cvScalarAll(255),100);

	    cvNamedWindow( "基于肤色的检测结果", CV_WINDOW_AUTOSIZE );
		cvShowImage( "基于肤色的检测结果", dst );

		//cvNamedWindow( "腐蚀结果", CV_WINDOW_AUTOSIZE );
		//cvShowImage( "腐蚀结果", erodeim );

		cvNamedWindow( "腐蚀膨胀结果", CV_WINDOW_AUTOSIZE );
		cvShowImage( "腐蚀膨胀结果", dilateim );

///////////////////////////////
	pDoc->cascade_name = "D:/Program Files/OpenCV2.0/data/haarcascades/haarcascade_frontalface_alt2.xml";
	pDoc->cascade = (CvHaarClassifierCascade*)cvLoad( pDoc->cascade_name, 0, 0, 0 );//加载训练集
	pDoc->storage = cvCreateMemStorage(0);
	cvNamedWindow( "Haar检测", 1 );
	
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
    IplImage* small_img = cvCreateImage( cvSize( cvRound (src->width/scale),cvRound (src->height/scale)),8, 1 );
    int i;
 
    cvCvtColor( src, gray, CV_BGR2GRAY );
    cvResize( gray, small_img, CV_INTER_LINEAR );
    cvEqualizeHist( small_img, small_img );
    cvClearMemStorage( pDoc->storage );
 
    if( pDoc->cascade )
    {
        double t = (double)cvGetTickCount();
        CvSeq* faces = cvHaarDetectObjects( small_img, pDoc->cascade, pDoc->storage,1.1, 2, 0/*CV_HAAR_DO_CANNY_PRUNING*/,cvSize(30, 30) );
        t = (double)cvGetTickCount() - t;
        printf( "detection time = %gms\n", t/((double)cvGetTickFrequency()*1000.) );
		int scount=0;
        for( i = 0; i < (faces ? faces->total : 0); i++ )
        {
			int skincount=0;
			scount++;
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
					s=cvGet2D(dilateim,b,a);
					if(s.val[0]!=0&&s.val[1]!=0&&s.val[2]!=0)
						skincount++;
				}
			double totalarea=(r->width)*scale*(r->height)*scale;
			printf("%d,%d\n",skincount,totalarea);
			double skinintotal=(double)skincount/totalarea;
			
			if(skinintotal>0.5)
			{
				cvNamedWindow("SingleFace",CV_WINDOW_AUTOSIZE);
				CvRect result=cvRect(r->x*scale,r->y*scale,r->width*scale,r->height*scale);
				cvSetImageROI(dst, result );	
                IplImage *smallface=cvCreateImage( cvSize((r->width)*scale,(r->height)*scale), 8, 3 ); 
                cvCopy(dst,smallface); 
                cvResetImageROI(dst); 
                cvShowImage ("SingleFace",smallface); 

				CString str,filepath; //获取系统时间
				CTime tm; 
				tm=CTime::GetCurrentTime();
				str=tm.Format("%Y%m%d%H%M%S"); 
				filepath.Format("E:/FaceDetection/Result/%s%02d.jpg",str,scount);
				cvSaveImage(filepath,smallface,0);

				cvRectangle(dst,cvPoint((r->x)*scale,(r->y)*scale),cvPoint((r->x+r->width)*scale,(r->y+r->height)*scale),colors[i%8]);
			}
			//cvRectangle(dilateim,cvPoint((r->x)*scale,(r->y)*scale),cvPoint((r->x+r->width)*scale,(r->y+r->height)*scale),colors[i%8]);
        }
    }
 
    cvShowImage( "Haar检测", dst );
    cvReleaseImage( &gray );
    cvReleaseImage( &small_img );

	////////////////////////////////
		

		cvWaitKey(0);
		cvReleaseImage(&src);
		cvReleaseImage(&dst);
		cvReleaseImage(&dilateim);
		cvReleaseImage(&erodeim);
		}
		//////////////////////////////////////////////读入文件为视频文件
		else if(pDoc->isVidLoad)
		{
			int rcount=0;
			IplImage* src=0;
			for(;;)
			{
				rcount++;
				if( !cvGrabFrame( pDoc->capture ))
					break;
				pDoc->frame = cvRetrieveFrame( pDoc->capture );
			    cvSaveImage("D:\image1.jpg",pDoc->frame);
				if( !pDoc->frame )
					break;
				if( !src )
					src = cvCreateImage( cvSize(pDoc->frame->width,pDoc->frame->height), IPL_DEPTH_8U, pDoc->frame->nChannels);
				src=cvLoadImage("D:\image1.jpg",1);
           /* if( pDoc->frame->origin == IPL_ORIGIN_TL )
				cvCopy( pDoc->frame, src, 0 );
            else
                cvFlip( pDoc->frame, src, 0 );*/

				
			
			
	CvScalar s;
	int r,g,b;

	float skin_bin_val=0;
	float nonskin_bin_val=0;

	IplImage *dst=cvCreateImage( cvGetSize(src),8,3 );//目标文件
	IplImage *dilateim=cvCreateImage( cvGetSize(src),8,3 );//腐蚀文件
	IplImage *erodeim=cvCreateImage( cvGetSize(src),8,3 );//腐蚀膨胀后文件

	for(int x=0;x<src->height;x++)//目标文件背景设置为黑色
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
			skin_bin_val = cvQueryHistValue_3D( pDoc->skinhist, r, g, b );
			nonskin_bin_val = cvQueryHistValue_3D( pDoc->nonskinhist, r, g, b );
			float p_skin=skin_bin_val/pDoc->skintotal;
			float p_nonskin=nonskin_bin_val/pDoc->nonskintotal;

			if(p_skin/p_nonskin>=0.4)
				cvSet2D(dst,x,y,cvScalar(r,g,b));
		}
	}

	
	    cvNamedWindow( "基于肤色的检测结果", CV_WINDOW_AUTOSIZE );
		cvShowImage( "基于肤色的检测结果", dst );

		cvErode(dst,erodeim,0,2);
		cvDilate(erodeim,dilateim,0,2);
		cvNamedWindow( "腐蚀膨胀结果", CV_WINDOW_AUTOSIZE );
		cvShowImage( "腐蚀膨胀结果", dilateim );


///////////////////////////////
	pDoc->cascade_name = "D:/Program Files/OpenCV2.0/data/haarcascades/haarcascade_frontalface_alt2.xml";
	//pDoc->cascade_name = "D:/Program Files/OpenCV2.0/data/haarcascades/haarcascade_mcs_mouth.xml";
	pDoc->cascade = (CvHaarClassifierCascade*)cvLoad( pDoc->cascade_name, 0, 0, 0 );
	pDoc->storage = cvCreateMemStorage(0);
	cvNamedWindow( "原始视频文件", 1 );
	cvShowImage( "原始视频文件", src );
	
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
    cvClearMemStorage( pDoc->storage );
 
    if( pDoc->cascade )
    {
		int scount=0;
        double t = (double)cvGetTickCount();
        CvSeq* faces = cvHaarDetectObjects( small_img, pDoc->cascade, pDoc->storage,1.1, 2, 0/*CV_HAAR_DO_CANNY_PRUNING*/,cvSize(30, 30) );
        t = (double)cvGetTickCount() - t;
        printf( "detection time = %gms\n", t/((double)cvGetTickFrequency()*1000.) );
        for( i = 0; i < (faces ? faces->total : 0); i++ )
        {
			int skincount=0;
            CvRect* r = (CvRect*)cvGetSeqElem( faces, i );

			for(int a=(r->x)*scale;a<(r->x+r->width)*scale;a++)
				for(int b=(r->y)*scale;b<(r->y+r->height)*scale;b++)
				{
					s=cvGet2D(dilateim,b,a);
					if(s.val[0]!=0&&s.val[1]!=0&&s.val[2]!=0)
						skincount++;
				}
			double totalarea=(r->width)*scale*(r->height)*scale;
			printf("%d,%d\n",skincount,totalarea);
			double skinintotal=(double)skincount/totalarea;
			
			if(skinintotal>0.5)
			{
				scount++;
				//cvNamedWindow("SingleFace",CV_WINDOW_AUTOSIZE);
				CvRect result=cvRect(r->x*scale,r->y*scale,r->width*scale,r->height*scale);
				cvSetImageROI(dst, result );	
                IplImage *smallface=cvCreateImage( cvSize((r->width)*scale,(r->height)*scale), 8, 3 ); 
                cvCopy(dst,smallface); 
                cvResetImageROI(dst); 
                //cvShowImage ("SingleFace",smallface); 
				CString str,filepath; //获取系统时间
				CTime tm; 
				tm=CTime::GetCurrentTime();
				str=tm.Format("%Y%m%d%H%M%S"); 
				filepath.Format("E:/FaceDetection/Result/%s%02d.jpg",str,scount);
				cvSaveImage(filepath,smallface,0);
				cvRectangle(dst,cvPoint((r->x)*scale,(r->y)*scale),cvPoint((r->x+r->width)*scale,(r->y+r->height)*scale),colors[i%8]);
			}
			//cvRectangle(src,cvPoint((r->x)*scale,(r->y)*scale),cvPoint((r->x+r->width)*scale,(r->y+r->height)*scale),colors[i%8]);
        }
    }
 
	
    cvShowImage( "肤色模型+Haar检测", dst );
    cvReleaseImage( &gray );
    cvReleaseImage( &small_img );

	////////////////////////////////
		

		//cvWaitKey(0);
		cvReleaseImage(&src);
		cvReleaseImage(&dst);
		cvReleaseImage(&dilateim);
		cvReleaseImage(&erodeim);
		if( cvWaitKey(10)>= 0 )
		{
			cvDestroyWindow("原始视频文件");
			cvDestroyWindow("基于肤色的检测结果");
			cvDestroyWindow("腐蚀膨胀结果");
			cvDestroyWindow("肤色模型+Haar检测");
            break;
		}
		}
        //cvReleaseImage( &pDoc->frame);
		cvReleaseImage( &pDoc->frame_copy);
        cvReleaseCapture( &pDoc->capture );
		}//for
		}//else if
		else
			//MessageBox(NULL,"尚未载入分类器或待检测图像！",MB_OK);
			MessageBoxA("尚未载入分类器或待检测图像！","警告！",0);
}