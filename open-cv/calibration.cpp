#include "stdafx.h"
#include <iostream>
#include <opencv\highgui.h>
#include <opencv\cv.h>


using namespace cv;
using namespace std;

const string N_windowName = "Normal";
const int FRAME_WIDTH = 640;
const int FRAME_HEIGHT = 480;
VideoCapture capture;
Mat cameraFeed;

//zmienne dotyczace klasyfikatorow
int H_MIN = 0;
int H_MAX = 255;
int S_MIN = 0;
int S_MAX = 255;
int V_MIN = 0;
int V_MAX = 255;

//nazwa okienka z obrazem threshold
string  T_windowName = "Threshold";


//nazwa okienka z trackbarami
string trackbarWindowName = "Trackbar";

//macierz na obraz threshold
Mat thresholdFeed;
//macierz na obraz HSV
Mat hsvFeed;

void on_trackbar(int, void*){}

//funkcja tworzaca trackbar
void createTrackbars(){
	//create window for trackbars

	namedWindow(trackbarWindowName, 1);
	//create memory to store trackbar name on window
	char TrackbarName[20];
	sprintf(TrackbarName, "H_MIN", H_MIN);
	sprintf(TrackbarName, "H_MAX", H_MAX);
	sprintf(TrackbarName, "S_MIN", S_MIN);
	sprintf(TrackbarName, "S_MAX", S_MAX);
	sprintf(TrackbarName, "V_MIN", V_MIN);
	sprintf(TrackbarName, "V_MAX", V_MAX);
	//create trackbars and insert them into window
	//3 parameters are: the address of the variable that is changing when the trackbar is moved(eg.H_LOW),
	//the max value the trackbar can move (eg. H_HIGH), 
	//and the function that is called whenever the trackbar is moved(eg. on_trackbar)
	//                                  ---->    ---->     ---->      
	createTrackbar("H_MIN", trackbarWindowName, &H_MIN, H_MAX, on_trackbar);
	createTrackbar("H_MAX", trackbarWindowName, &H_MAX, H_MAX, on_trackbar);
	createTrackbar("S_MIN", trackbarWindowName, &S_MIN, S_MAX, on_trackbar);
	createTrackbar("S_MAX", trackbarWindowName, &S_MAX, S_MAX, on_trackbar);
	createTrackbar("V_MIN", trackbarWindowName, &V_MIN, V_MAX, on_trackbar);
	createTrackbar("V_MAX", trackbarWindowName, &V_MAX, V_MAX, on_trackbar);
}

void morphOps(Mat &thresh){
	Mat erodeElement = getStructuringElement(MORPH_RECT, Size(3, 3));
	Mat dilateElement = getStructuringElement(MORPH_RECT, Size(8, 8));

	erode(thresh, thresh, erodeElement);
	erode(thresh, thresh, erodeElement);

	dilate(thresh, thresh, dilateElement);
	dilate(thresh, thresh, dilateElement);
}

void trackFilteredObject(Mat threshold){
	const int MIN_OBJECT_AREA = 20 * 20;
	const int MAX_OBJECT_AREA = FRAME_HEIGHT*FRAME_WIDTH / 1.5;
	int x, y;
	Mat temp;
	threshold.copyTo(temp);
	vector< vector<Point> > contours;
	vector<Vec4i> hierarchy;
	findContours(temp, contours, hierarchy, CV_RETR_CCOMP, CV_CHAIN_APPROX_SIMPLE);

	double refArea = 0;
	bool objectFound = false;
	if (hierarchy.size() > 0) {
		int numObjects = hierarchy.size();
			for (int index = 0; index >= 0; index = hierarchy[index][0]) {

				Moments moment = moments((cv::Mat)contours[index]);
				double area = moment.m00;
				if (area>MIN_OBJECT_AREA && area<MAX_OBJECT_AREA && area>refArea){
					x = moment.m10 / area;
					y = moment.m01 / area;
					objectFound = true;
					refArea = area;
				}
				else objectFound = false;


			}
			if (objectFound == true){
				printf("\nx: %d\ty: %d", x, y);
			}
	}
}


int main(int argc, char* argv[])
{
	int cam;
	printf("Ktora kamerke otworzyc: ");
	scanf("%d", &cam);

	capture.open(cam);

	if (!capture.isOpened())
	{
		printf("\nNie udalo sie otworzyc kamerki \n");
		return -1;
	}
	else
		printf("\nUdalo sie otworzyc kamerke \n");

	//tworzenie trackbar
	createTrackbars();

	while (true){
		if (capture.isOpened()){
			capture.retrieve(cameraFeed);
			capture.read(cameraFeed);

			//konwersja z modelu RGB na HSV
			cvtColor(cameraFeed, hsvFeed, COLOR_BGR2HSV);

			//ustawianie macierzy threshold zgodnie z trackbarami
			inRange(hsvFeed, Scalar(H_MIN, S_MIN, V_MIN), Scalar(H_MAX, S_MAX, V_MAX), thresholdFeed);

			imshow(N_windowName, cameraFeed);
			morphOps(thresholdFeed);
			//wyswietlanie macierzy threshold
			imshow(T_windowName, thresholdFeed);
			//sledzenie obiektu
			trackFilteredObject(thresholdFeed);

		}

		waitKey(30);
	}

	return 0;
}