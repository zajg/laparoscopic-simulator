using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GrasperScript : MonoBehaviour {
	private Vector3 spawnLocation;
	private GameObject pickUp;
	private GameObject warning;
	private int count;
	public Text countTextLeft;
	public Text winText;

	void Start () {
		count = 0;
		countTextLeft.text = "";
		winText.text = "Dotykaj pojawiające się obiekty\nkońcówkami grasperów.";
	}

	void Update () {
		warning = GameObject.Find("Warning");
		if (warning.activeInHierarchy == true) {
			count = 0;
			SetCountTextLeft();
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "PickUp") {
			DestroyObject(other.gameObject);
			pickUp = (GameObject)Resources.Load("Prefabs/PickUp");
			spawnLocation = new Vector3(Random.Range(-57.0f, -52.0f), 
			                           2.0f, 
			                           Random.Range(-14.0f,-21.0f));
			Instantiate(pickUp, spawnLocation, Quaternion.identity);

			count += 1;
			SetCountTextLeft();
		}
		if (count > 0 && count < 11) {
			winText.text = "";
		}
	}

	void SetCountTextLeft() {
		countTextLeft.text = "Wynik: " + count.ToString ();
		if (count >= 10) {
			countTextLeft.text = "KONIEC";
			winText.text = "Naciśnij spację, aby wrócić do menu.";
		}
	}

}
