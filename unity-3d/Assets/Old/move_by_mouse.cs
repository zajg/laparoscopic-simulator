using UnityEngine;
using System.Collections;

public class move_by_mouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 newPosition = transform.position; //utworzenie wektora do przetrzymywania pozucji
		//Vector3 upVector = new Vector3 (1, 1, 1);
		newPosition.x = Input.mousePosition.x/100 -57;
		newPosition.z = Input.mousePosition.y/100 -14;
		//newPosition.y = 2;
		newPosition.y += Input.GetAxis("Mouse ScrollWheel");
		Debug.Log(newPosition.y);
		
		transform.position = newPosition;
	}
}
