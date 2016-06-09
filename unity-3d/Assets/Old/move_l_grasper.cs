using UnityEngine;
using System.Collections;

public class move_l_grasper : MonoBehaviour {

	public float moveSpeed = 0.2f;
	public float turnSpeed = 0.1f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.S))
			transform.Rotate(Vector3.left * turnSpeed * Time.deltaTime);

		
		if(Input.GetKey(KeyCode.W))
			transform.Rotate(Vector3.left * -turnSpeed * Time.deltaTime);


		if(Input.GetKey(KeyCode.A))
			transform.Rotate(Vector3.forward * -turnSpeed * Time.deltaTime);
		
		
		if(Input.GetKey(KeyCode.D))
			transform.Rotate(Vector3.forward * turnSpeed * Time.deltaTime);
	



		if(Input.GetKey(KeyCode.E))
			transform.Translate(Vector3.down * -moveSpeed * Time.deltaTime);
		
		
		if(Input.GetKey(KeyCode.Q))
			transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
	}
}
