using UnityEngine;
using System.Collections;

public class BackToMenu : MonoBehaviour {
	
	void Update () {
		if (Input.GetKey (KeyCode.Space))
			Application.LoadLevel(0);
	}
}
