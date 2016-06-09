using UnityEngine;
using System.Collections;

public class move_force : MonoBehaviour {

	//zmienne zmodyfikowane w tej sekcji możemy definiować z poziomu GUI w Unity
	public float speed = 15.0f; //szybkosc porusznia
	public string slide_in = "q"; //przypisanie klawisza wsuwania
	public string slide_out = "e"; //przypisanie klawisza wysuwania


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {	
		if(Input.GetKey(slide_in)) //jezeli klawisz wsuwania wcisniety
			rigidbody.AddRelativeForce(0,-speed,0); //dodajemy do obiektu siłę w osi y
		
		if(Input.GetKey(slide_out)) //jezeli klawisz wysuwania wcisniety
			rigidbody.AddRelativeForce(0,speed,0); //dodajemy do obiektu przeciwnie zwróconą siłę w osi y

		if (Input.GetKeyUp (slide_in) || Input.GetKeyUp (slide_out) || Input.GetKeyUp("w")|| Input.GetKeyUp("s")|| Input.GetKeyUp("a")|| Input.GetKeyUp("d")|| Input.GetKeyUp("j")|| Input.GetKeyUp("k")|| Input.GetKeyUp("l")|| Input.GetKeyUp("i")) {		
			//jeżeli puszczamy klawisz odpowiedzialny za przemieszczanie
			rigidbody.velocity = Vector3.zero;  //to siła jest zerowana
			rigidbody.angularVelocity = Vector3.zero; //i moment obrotowy jest zerowany
		}
	}
}
