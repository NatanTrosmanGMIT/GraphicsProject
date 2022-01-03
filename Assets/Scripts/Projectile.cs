using UnityEngine.AI;
using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public LayerMask collisionMask;
		public Color trailColour;

	float speed = 10;
	float damage = 1;

	float lifetime = 3;
	float skinWidth = .1f;
// Gives projectiles a lifetime
	void Start() {
		Destroy (gameObject, lifetime);
// checks if projectile was spawned inside something else and make sure it does damage
		Collider[] initialCollisions = Physics.OverlapSphere (transform.position, .1f, collisionMask);
		if (initialCollisions.Length > 0) {
			OnHitObject(initialCollisions[0], transform.position);
		}
			GetComponent<TrailRenderer> ().material.SetColor ("_TintColor", trailColour);
	}
// sets speed for different projectiles
	public void SetSpeed(float newSpeed) {
		speed = newSpeed;
	}
	
	void Update () {
		float moveDistance = speed * Time.deltaTime;
		CheckCollisions (moveDistance);
		transform.Translate (Vector3.forward * moveDistance);
	}

// Checks if projectile hit anything
	void CheckCollisions(float moveDistance) {
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, moveDistance + skinWidth, collisionMask, QueryTriggerInteraction.Collide)) {
			OnHitObject(hit.collider, hit.point);
		}
	}

// If it does, destroy it
	void OnHitObject(Collider c, Vector3 hitPoint) {
		IDamageable damageableObject = c.GetComponent<IDamageable> ();
		if (damageableObject != null) {
			damageableObject.TakeHit(damage, hitPoint, transform.forward);
		}
		GameObject.Destroy (gameObject);
	}
}