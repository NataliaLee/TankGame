using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{
	public GameObject explosionParticles;
	public float damage = 1f;
	public float destroyTime = 4f;

	void OnEnable(){
		Invoke ("Destroy",destroyTime);
	}

	void OnDisable(){
		CancelInvoke ();
	}

	void Destroy(){
		gameObject.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D other) {
		//TODO events?
		// send damage
		Health health = other.GetComponent<Health> ();
		if (health != null) {
			health.TakeDamage(damage);
		}

		//show particleeffect  
		PoolManager.instance.ReuseObject(explosionParticles,transform.position,transform.rotation);
		gameObject.SetActive (false);
	}

}
