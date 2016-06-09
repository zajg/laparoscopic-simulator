using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public void LoadScene(int level) {
		// int level is the level's index in the build settings
		Application.LoadLevel(level);
	}

}
