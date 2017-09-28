using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

	public float speed=12f;
	public float turnSpeed=180f;

	private Rigidbody2D rigidBody;
	private Vector3 movement; 
	private float turn;
	private Vector3 minPoint;
	private Vector3 maxPoint;


	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();

		minPoint = Camera.main.ScreenToWorldPoint (Vector3.zero);
		maxPoint = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width,Screen.height,0));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Store the input axes.
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		// Move the player around the scene.
		Move (v);
		Turn (-h);
	}

	void Turn(float h){
		turn = h * turnSpeed * Time.deltaTime;
		rigidBody.MoveRotation (rigidBody.rotation+turn);
	}

	void Move (float v)
	{
		movement = transform.up* speed * v * Time.deltaTime;
		Vector3 newPosition = transform.position + movement;
		if (newPosition.x < minPoint.x || newPosition.x > maxPoint.x)
			return;
		if (newPosition.y < minPoint.y || newPosition.y > maxPoint.y)
			return;		
		// Move the player to it's current position plus the movement.
		rigidBody.MovePosition (newPosition);
	}
}
