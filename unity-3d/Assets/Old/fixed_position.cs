using UnityEngine;
using System.Collections;

public class fixed_position : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var newPosition = new Vector3(0.0f,33.64153f,0.0f);		
		transform.position = newPosition;
	}
}
