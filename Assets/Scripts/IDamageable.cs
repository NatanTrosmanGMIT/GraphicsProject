using UnityEngine;

// Interface to tell us if something took a hit
public interface IDamageable {

	void TakeHit (float damage, Vector3 hitPoint, Vector3 hitDirection);

	void TakeDamage (float damage);

}