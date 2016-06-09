using UnityEngine;
using System.Collections;

public class move_l_grasper : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey(KeyCode.A))
		{
			//Vector3 position = this.transform.position;
			//position.x--;
			//this.transform.position = position;
		}
		if (Input.GetKey(KeyCode.D))
		{
			//Vector3 position = this.transform.position;
			//position.x++;
			//this.transform.position = position;
		}
		if (Input.GetKey(KeyCode.W))
		{
			//Vector3 position = this.transform.position;
			//position.y++;
			//this.transform.position = position;
			//transform.Translate(Vector3.forward * 1f);
		}
		if (Input.GetKey(KeyCode.S))
		{
			//Vector3 position = this.transform.position;
			//position.y--;
			//this.transform.position = position;
		}
	
	}
}
