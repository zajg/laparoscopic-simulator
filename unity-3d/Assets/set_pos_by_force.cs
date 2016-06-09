using UnityEngine;
using System.Collections;

public class set_pos_by_force : MonoBehaviour {

	public float speed = 15.0f; //szybkosc porusznia
	public float pos_z = -10;

	private GameObject warning;


	// Use this for initialization
	void Start () {
		warning = GameObject.Find("Warning");
		warning.SetActive(false);
	}

	// Update is called once per frame
	void FixedUpdate () {

		pos_z += 2*Input.GetAxis("Mouse ScrollWheel");

		var target = new Vector3(Input.mousePosition.x/100 -57, Input.mousePosition.y/100 -2, pos_z);
		var dist = Vector3.Distance(target, transform.position);

		if (dist > 0) { //jezeli klawisz wysuwania wcisniety
			rigidbody.velocity = (target - transform.position) * speed;
			//Debug.Log ("przesuwam");
		}
		else{
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
			//Debug.Log ("przesunieto");
		}
		if (rigidbody.position[1] <= 0.9f) {
			warning.SetActive(true);
		} else {
			warning.SetActive(false);
		}
	}
}
