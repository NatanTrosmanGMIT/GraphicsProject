using UnityEngine;
using System.Collections;

// This here makes an in game object require PlayerController
[RequireComponent (typeof (Rigidbody))]

public class PlayerController : MonoBehaviour {

	Vector3 velocity;
	Rigidbody myRigidbody;

	void Start () {
		
		myRigidbody = GetComponent<Rigidbody> ();
	}
	
// Gives us speed
	public void Move(Vector3 _velocity) {

		velocity = _velocity;
	}
// Makes the player look in a direction (height corrected, otherwise (s)he looks down)
	public void LookAt(Vector3 lookPoint) {
		Vector3 heightCorrectedPoint = new Vector3 (lookPoint.x, transform.position.y, lookPoint.z);
		transform.LookAt (heightCorrectedPoint);
	}
// You usually want to move the rigidBody in FixedUpdate
	void FixedUpdate() {
		myRigidbody.MovePosition (myRigidbody.position + velocity * Time.fixedDeltaTime);

	}
}