using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Explosion : MonoBehaviour {

	void OnEnable(){
		GetComponent<ParticleSystem> ().Clear ();
		GetComponent<ParticleSystem> ().Play ();
		Invoke ("Destroy",0.2f);
	}

	void OnDisable(){
		CancelInvoke ();
	}

	void Destroy(){
		gameObject.SetActive (false);
	}
}
