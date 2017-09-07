using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D),typeof(Seeker))]
public class EnemyMovingPathfinder : MonoBehaviour {
	public float speed=8f;
	public float damage=1f;
	public float rotationSpeed=180f;

	//Determines how often it will search for new paths.
	public float repathRate = 0.5F;
	/** Current path which is followed */
	protected Path path;
	private bool canSearchAgain=true;

	private Rigidbody2D rigidBody;
	private Transform target;
	private bool canMove=true;
	private Seeker seeker;
	private int pointPathIndex;
	private float pointDistance=10f;

	void Awake() {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		if (player!=null)
			target = player.transform;
		seeker = GetComponent<Seeker> ();
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	void OnEnable(){
		seeker.pathCallback += OnPathComplete;
	}

	void OnDisable(){
		seeker.CancelCurrentPathRequest();

		// Release current path so that it can be pooled
		if (path != null) path.Release(this);
		path = null;
		seeker.pathCallback -= OnPathComplete;
	}

	void Start(){
		StartCoroutine(RepeatTrySearchPath());
	}

	void FixedUpdate(){
		if (canMove) {
			Move ();
		}
	}

	void Move(){
		if (path == null)
			return;
		float distance=Vector3.Distance ( transform.position, path.vectorPath [pointPathIndex]);
		if (distance < pointDistance) {
			pointPathIndex++;
		}
		if (pointPathIndex >= path.vectorPath.Count) {
			path.Release (this);
			path = null;
			SearchPath ();
			return;			
		}
		MoveTo (path.vectorPath[pointPathIndex]);
	}

	void MoveTo(Vector3 point){
		//rotate to look at point
		Vector3 rotationLook=transform.position - point;
		Quaternion newRotation = Quaternion.identity;
		if (rotationLook!=Vector3.zero)
			newRotation=Quaternion.LookRotation(rotationLook, Vector3.forward);
		newRotation.x = 0.0f;
		newRotation.y = 0.0f;
		transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
		Vector3 newPosition = transform.position + transform.up * Time.deltaTime * speed;
		//move towards
		rigidBody.MovePosition(newPosition);
	}

	/** Tries to search for a path every #repathRate seconds.
	 * \see TrySearchPath
	 */
	protected IEnumerator RepeatTrySearchPath () {
		while (true) {
			SearchPath ();
			yield return new WaitForSeconds (repathRate);
		}
	}

	/** Requests a path to the target */
	public void SearchPath () {
		if (target == null || !canSearchAgain) return;
		canSearchAgain = false;
		// This is where we should search to
		Vector3 targetPosition = target.position;
		// We should search from the current position
		seeker.StartPath(transform.position, targetPosition);
	}

	/** Called when a requested path has finished calculation.
	 * A path is first requested by #SearchPath, it is then calculated, probably in the same or the next frame.
	 * Finally it is returned to the seeker which forwards it to this function.\n
	 */
	public void OnPathComplete (Path _p) {
		ABPath p = _p as ABPath;

		if (p == null) throw new System.Exception("This function only handles ABPaths, do not use special path types");
		canSearchAgain = true;

		// Claim the new path
		p.Claim(this);

		// Path couldn't be calculated of some reason.
		// More info in p.errorLog (debug string)
		if (p.error) {
			p.Release(this);
			return;
		}

		// Release the previous path
		if (path != null) path.Release(this);

		// Replace the old path
		path = p;
		pointPathIndex = 0;
		pointDistance=Vector3.Distance ( path.vectorPath [0], path.vectorPath [1]);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		//TODO damage per sec?
		if ("Player".Equals (coll.gameObject.tag)) {
			Health health = coll.gameObject.GetComponent<Health> ();
			if (health != null) {
				health.TakeDamage (damage);
			}
			canMove = false;
			rigidBody.isKinematic = true;
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		if ("Player".Equals (coll.gameObject.tag)) {	
			canMove = true;
			rigidBody.isKinematic = false;
		}
	}




}
