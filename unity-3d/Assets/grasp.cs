using UnityEngine;
using System.Collections;

public class grasp : MonoBehaviour {

	public float turnSpeed = 50f;	
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {		
		if (Input.GetKey (KeyCode.X) )//&& ((transform.rotation.z >= 330 && transform.rotation.z <= 360) || transform.rotation.z == 0))
			gameObject.transform.Rotate (Vector3.left * turnSpeed * Time.deltaTime);		
		if (Input.GetKey (KeyCode.Z) && transform.rotation.x > 0 && transform.rotation.x < 180)//&& ((transform.rotation.z >= 330 && transform.rotation.z <= 360) || transform.rotation.z == 0))
			gameObject.transform.Rotate (Vector3.left * -turnSpeed * Time.deltaTime);
		//if (transform.rotation.x > 0 && transform.rotation.x < 180)
		//	 transform.localEulerAngles = new Vector3 (0, transform.rotation.y, transform.rotation.z);
		//if (transform.rotation.x >= 180 && transform.rotation.x < 330)
		//	 transform.localEulerAngles = new Vector3 (330, transform.rotation.y, transform.rotation.z);
	}
}
