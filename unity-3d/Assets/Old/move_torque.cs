using UnityEngine;
using System.Collections;

public class move_torque : MonoBehaviour {	

	//zmienne zmodyfikowane w tej sekcji możemy definiować z poziomu GUI w Unity
	public float speed = 15.0f; //szybkosc obrotu
	
	void FixedUpdate () {

		//float h = Input.GetAxis ("Horizontal") * torque * Time.deltaTime;
		//float v = Input.GetAxis ("Vertical") * torque * Time.deltaTime;
		
		//rigidbody.AddTorque (transform.up * h, ForceMode.VelocityChange);		
		//rigidbody.AddTorque (-transform.right * v, ForceMode.VelocityChange);

		//if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)|| Input.GetKeyUp(KeyCode.A)||Input.GetKeyUp(KeyCode.D))
		//	rigidbody.angularVelocity = Vector3.zero;
		

		if (Input.GetKey (KeyCode.A)) //jezeli klawisz a wcisniety
			rigidbody.AddRelativeTorque (0, -speed, 0); //dodajemy moment obrotowy do osi o zadanym zwrocie

		if (Input.GetKey (KeyCode.D)) //jezeli klawisz d wcisniety
			rigidbody.AddRelativeTorque (0, speed, 0); //dodajemy moment obrotowy do osi o zadanym zwrocie

		if (Input.GetKey (KeyCode.W)) //jezeli klawisz w  wcisniety
			rigidbody.AddRelativeTorque (-speed, 0, 0); //dodajemy moment obrotowy do osi o zadanym zwrocie

		if (Input.GetKey (KeyCode.S)) //jezeli klawisz s wcisniety
			rigidbody.AddRelativeTorque (speed, 0, 0); //dodajemy moment obrotowy do osi o zadanym zwrocie

		if (Input.GetKeyUp (KeyCode.Q) || Input.GetKeyUp (KeyCode.E) || Input.GetKeyUp (KeyCode.W) || Input.GetKeyUp (KeyCode.S) || Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.D)) {		
			//jeżeli puszczamy klawisz odpowiedzialny za przemieszczanie
			rigidbody.velocity = Vector3.zero; //to siła jest zerowana
			rigidbody.angularVelocity = Vector3.zero; //i moment obrotowy jest zerowany
		}
	}
}
