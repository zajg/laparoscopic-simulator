using UnityEngine;
using System.Collections;

public class move_grasper : MonoBehaviour {
	
	public float moveSpeed = 2f;
	public float turnSpeed = 1f;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKey(KeyCode.K))
			transform.Rotate(Vector3.left * turnSpeed * Time.deltaTime);
		
		
		if(Input.GetKey(KeyCode.I))
			transform.Rotate(Vector3.left * -turnSpeed * Time.deltaTime);
		

		if(Input.GetKey(KeyCode.J))
			transform.Rotate(Vector3.forward * -turnSpeed * Time.deltaTime);
		

		if(Input.GetKey(KeyCode.L))
			transform.Rotate(Vector3.forward * turnSpeed * Time.deltaTime);
		
		
		
		
		if(Input.GetKey(KeyCode.O))
			transform.Translate(Vector3.down * -moveSpeed * Time.deltaTime);
		
		
		if(Input.GetKey(KeyCode.U))
			transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
	}
}
