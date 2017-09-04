using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour {
	public float speed=8f;
	public float damage=1f;
	public float rotationSpeed=180f;

	private Rigidbody2D rigidBody;
	private Transform target;
	private bool canMove=true;

	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void FixedUpdate () {
		if (!canMove || !target || !target.gameObject.activeSelf)
			return;
		//rotate to look at the player
		var newRotation = Quaternion.LookRotation(transform.position - target.position, Vector3.forward);
		newRotation.x = 0.0f;
		newRotation.y = 0.0f;
		transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
		Vector3 newPosition = transform.position + transform.up * Time.deltaTime * speed;
		//move towards the player
		rigidBody.MovePosition(newPosition);
		//transform.position += transform.up * Time.deltaTime * speed;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		//TODO damage per sec?
		if ("Player".Equals (coll.gameObject.tag)) {
			Health health = coll.gameObject.GetComponent<Health> ();
			if (health != null) {
				health.TakeDamage (damage);
			}
			canMove = false;
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		if ("Player".Equals (coll.gameObject.tag)) {			
			canMove = true;
		}
	}
}
