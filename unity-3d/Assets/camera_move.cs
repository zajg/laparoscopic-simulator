using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Threading;

public class Camera
{	
	[DllImport("testcs.dll")]
	public static extern void Hello(int cam0, int cam1, 
	                                ref int x0, ref int y0,
	                                ref int x1, ref int y1, 
	                                ref int x2, ref int y2,
	                                ref int x3, ref int y3, 
	                                ref int run, 
	                                int Hmin0, int Hmax0, int Smin0, int Smax0, int Vmin0, int Vmax0, 
	                                int Hmin1, int Hmax1, int Smin1, int Smax1, int Vmin1, int Vmax1);
	
	public void threshold(ref int x0,ref int y0,
	                      ref int x1, ref int y1,
	                      ref int x2, ref int y2,
	                      ref int x3, ref int y3,
	                      ref int run)
	{
		Hello (1,0,
		       ref x0, ref y0,
		       ref x1, ref y1,
		       ref x2, ref y2,
		       ref x3, ref y3,
		       ref run,
		       56, 124, 80, 255, 37, 255,
		       40, 73, 44, 255, 51, 255); //tu powinny byc ustawione parametry drugiego znacznika
	}
};

public class camera_move : MonoBehaviour {
	
	//ustawienie prawy/lewy
	public bool isBlue = true; 
	
	
	//wspolrzedne x i y z kamerek
	
	//kamera 1 znacznik 1
	public int x0 = 0;
	public int y0 = 0;
	//kamera 2 znacznik 1
	public int x1 = 0;
	public int y1 = 0;
	//kamera 1 znacznik 2
	public int x2 = 0;
	public int y2 = 0;
	//kamera 2 znacznik 2
	public int x3 = 0;
	public int y3 = 0;
	
	//zmienna determinujaca dzialanie watku
	public int run = 1;
	
	//obiekt klasy kamera
	Camera kamerka = new Camera();
	
	//watek pobierajacy obraz z kamery 
	Thread oThread;
	
	//szybkosc poruszania
	public float speed = 15.0f; 
	
	public Transform ChildGameObject1;
	public Transform ChildGameObject2;
	
	private GameObject warning;
	void Start () 
	{
		warning = GameObject.Find("Warning");
		warning.SetActive(false);
		ChildGameObject1 = this.gameObject.transform.GetChild(0);
		ChildGameObject2 = this.gameObject.transform.GetChild(1);
		
		oThread = new Thread(() => kamerka.threshold(ref x0,ref y0,
		                                             ref x1,ref y1,
		                                             ref x2,ref y2,
		                                             ref x3,ref y3,
		                                             ref run));
		oThread.Start();
		while (!oThread.IsAlive);

	}
	
	void FixedUpdate () 
	{
		
		Debug.Log (" x0: " + x0 + " y0: " + y0 + " " + 
		           " x1: " + x1 + " y1: " + y1 + " " + 
		           " x2: " + x2 + " y2: " + y2 + " " +
		           " x3: " + x3 + " y3: " + y3 + " " +
		           " gx: " + gameObject.transform.position.x + " gy: " + gameObject.transform.position.y + " gz: " + gameObject.transform.position.z);
		
		
		///////// (∩ ͡° ͜ʖ ͡°)⊃━☆ﾟ. MAGIA ///////////////////
		var target = Vector3.zero;
		var target2 = Vector3.zero;
		//if (isBlue && x0!=0 && y0!=0 && x1!=0) 
		target = new Vector3 (x0 / 37.0f - 60.0f, (((480.0f - y0) / 41.0f) - 3.8f), (((640.0f-x1)/30.0f)-26.0f));
		//else if (!isBlue && x0!=0 && y0!=0 && x1!=0)
		target2 = new Vector3 (x2 / 37.0f - 60.0f, (((480.0f - y2) / 41.0f) - 3.8f), (((640.0f-x3)/30.0f)-26.0f));
		
		var dist = Vector3.Distance(target, ChildGameObject1.transform.position);
		var dist2 = Vector3.Distance(target2, ChildGameObject2.transform.position);
		
		if (dist > 0) 
		{
			ChildGameObject1.rigidbody.velocity = (target - ChildGameObject1.transform.position) * speed;
			
			//Vector3 av = ChildGameObject1.rigidbody.angularVelocity;
			//av.y = 0;
			//ChildGameObject1.rigidbody.angularVelocity = av;
		}
		else
		{
			ChildGameObject1.rigidbody.velocity = Vector3.zero;
			ChildGameObject1.rigidbody.angularVelocity = Vector3.zero;
		}
		
		
		
		if (dist2 > 0) 
		{
			ChildGameObject2.rigidbody.velocity = (target2 - ChildGameObject2.transform.position) * speed;
		}
		else
		{
			ChildGameObject2.rigidbody.velocity = Vector3.zero;
			ChildGameObject2.rigidbody.angularVelocity = Vector3.zero;
		}
		///////////////////////////////////////////////////
		if (ChildGameObject1.rigidbody.position[1] <= 0.9f || ChildGameObject2.rigidbody.position[1] <= 0.9f) {
		warning.SetActive(true);
		} else {
		warning.SetActive(false);


	}
	}
	
	//podczas zamykania aplikacji wylaczamy
	void OnApplicationQuit()
	{
		run = 0;
	}
	
}