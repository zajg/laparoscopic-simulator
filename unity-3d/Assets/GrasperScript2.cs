using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GrasperScript2 : MonoBehaviour {
	private Vector3 spawnLocation;
	private GameObject pickUp;
	private GameObject warning;
	private int count;
	public Text countTextRight;
	public Text winText;

	void Start () {
		count = 0;
		countTextRight.text = "";
	}

	void Update () {
		warning = GameObject.Find("Warning");
		Debug.Log (warning.activeInHierarchy);
		if (warning.activeInHierarchy == true) {
			count = 0;
			SetCountTextRight();
		}
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "PickUp") {
			DestroyObject(other.gameObject);
			pickUp = (GameObject)Resources.Load("PickUp");
			spawnLocation = new Vector3(Random.Range(-0.5F,7F), 
			                            3.0f, 
			                            Random.Range(1F,-6.0F));
			Instantiate(pickUp, spawnLocation, Quaternion.identity);
			
			count += 1;
			SetCountTextRight();
		}
		if (count > 0 && count < 11) {
			winText.text = "";
		}
	}
	
	void SetCountTextRight() {
		countTextRight.text = "Wynik: " + count.ToString ();
		if (count >= 10) {
			countTextRight.text = "KONIEC";
			winText.text = "Naciśnij spację, aby wrócić do menu.";
		}
	}
	
}

