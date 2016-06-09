using UnityEngine;
using System.Collections;

public class move_l_torque : MonoBehaviour {

	public float speed = 15.0f;
	
	void FixedUpdate () {		
		//float h = Input.GetAxis ("Horizontal") * torque * Time.deltaTime;
		//float v = Input.GetAxis ("Vertical") * torque * Time.deltaTime;
		
		//rigidbody.AddTorque (transform.up * h, ForceMode.VelocityChange);		
		//rigidbody.AddTorque (-transform.right * v, ForceMode.VelocityChange);
		
		//if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)|| Input.GetKeyUp(KeyCode.A)||Input.GetKeyUp(KeyCode.D))
		//	rigidbody.angularVelocity = Vector3.zero;		
		
		if (Input.GetKey (KeyCode.J))
			rigidbody.AddRelativeTorque (0, -speed, 0);
		
		if (Input.GetKey (KeyCode.L))
			rigidbody.AddRelativeTorque (0, speed, 0);

		if (Input.GetKey (KeyCode.I))
			rigidbody.AddRelativeTorque (-speed, 0, 0);
		
		if (Input.GetKey (KeyCode.K))
			rigidbody.AddRelativeTorque (speed, 0, 0);
		
		if (Input.GetKeyUp (KeyCode.U) || Input.GetKeyUp (KeyCode.O) || Input.GetKeyUp (KeyCode.I) || Input.GetKeyUp (KeyCode.J) || Input.GetKeyUp (KeyCode.K) || Input.GetKeyUp (KeyCode.L)) {		
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
		}
		
	}
}
