#include <stdio.h>
#include <opencv\highgui.h>
#include <opencv\cv.h>


using namespace cv;
using namespace std;

//const string window = "Window";

const int FRAME_WIDTH = 640;
const int FRAME_HEIGHT = 480;

VideoCapture capture0,capture1;

Mat cameraFeed0, cameraFeed1;

//nazwa okienka z obrazem threshold
//string  T_windowName = "Threshold";


//nazwa okienka z trackbarami
//string trackbarWindowName = "Trackbar";

//macierz na obraz threshold
Mat thresholdFeed0, thresholdFeed1, thresholdFeed2, thresholdFeed3;
//macierz na obraz HSV
Mat hsvFeed0, hsvFeed1, hsvFeed2, hsvFeed3;

Mat erodeElement = getStructuringElement(MORPH_RECT, Size(5, 5));
Mat dilateElement = getStructuringElement(MORPH_RECT, Size(7, 7));


const int MIN_OBJECT_AREA = 12 * 12;
const int MAX_OBJECT_AREA = FRAME_HEIGHT*FRAME_WIDTH / 1.5;
vector< vector<Point> > contours;
vector<Vec4i> hierarchy;
int numObjects;
Moments moment;
double area;

extern "C"
{
	__declspec(dllexport) void morphOps(Mat &thresh){

		erode(thresh, thresh, erodeElement);
		dilate(thresh, thresh, dilateElement);
	}

	__declspec(dllexport)void trackFilteredObject(Mat threshold, int & x, int & y){
		findContours(threshold, contours, hierarchy, CV_RETR_CCOMP, CV_CHAIN_APPROX_SIMPLE);

		double refArea = 0;
		bool objectFound = false;
		if (hierarchy.size() > 0) {
			numObjects = hierarchy.size();
			for (int index = 0; index >= 0; index = hierarchy[index][0]) {

				moment = moments((cv::Mat)contours[index]);
				area = moment.m00;
				if (area>MIN_OBJECT_AREA && area<MAX_OBJECT_AREA && area>refArea){
					x = moment.m10 / area;
					y = moment.m01 / area;
					refArea = area;
				}
			}
		}
	}

	__declspec(dllexport) void Hello(int cam0, int cam1, int & x0, int & y0, int & x1, int &y1, int & x2, int &y2, int & x3, int &y3, int & run, int Hmin0, int Hmax0, int Smin0, int Smax0, int Vmin0, int Vmax0, int Hmin1, int Hmax1, int Smin1, int Smax1, int Vmin1, int Vmax1)
	{
		capture0.open(cam0);
		capture1.open(cam1);

		while (run == 1)
		{
				if (capture0.isOpened()&&capture1.isOpened()){
					capture0.retrieve(cameraFeed0);
					capture0.read(cameraFeed0);
					capture1.retrieve(cameraFeed1);
					capture1.read(cameraFeed1);

					//konwersja z modelu RGB na HSV
					cvtColor(cameraFeed0, hsvFeed0, COLOR_BGR2HSV);
					cvtColor(cameraFeed1, hsvFeed1, COLOR_BGR2HSV);

					//ustawianie macierzy threshold zgodnie z trackbarami
					inRange(hsvFeed0, Scalar(Hmin0, Smin0 ,Vmin0), Scalar(Hmax0, Smax0, Vmax0), thresholdFeed0);
					inRange(hsvFeed1, Scalar(Hmin0, Smin0, Vmin0), Scalar(Hmax0, Smax0, Vmax0), thresholdFeed1);
					inRange(hsvFeed0, Scalar(Hmin1, Smin1, Vmin1), Scalar(Hmax1, Smax1, Vmax1), thresholdFeed2);
					inRange(hsvFeed1, Scalar(Hmin1, Smin1, Vmin1), Scalar(Hmax1, Smax1, Vmax1), thresholdFeed3);

					morphOps(thresholdFeed0);
					morphOps(thresholdFeed1);
					morphOps(thresholdFeed2);
					morphOps(thresholdFeed3);
			
					trackFilteredObject(thresholdFeed0, x0, y0);
					trackFilteredObject(thresholdFeed1, x1, y1);
					trackFilteredObject(thresholdFeed2, x2, y2);
					trackFilteredObject(thresholdFeed3, x3, y3);

				}
			//	waitKey(30);
		}	
		capture0.release();
		capture1.release();
	}

}