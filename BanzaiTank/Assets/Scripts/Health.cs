using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class Health : MonoBehaviour {

	public float totalHealthPoints=5f;
	protected float currentHealthPoints;
	[Range(0f,1f)]
	public float shield=1f;
	protected event Action Die;

	void OnEnable(){
		currentHealthPoints = totalHealthPoints;
	}

	public void TakeDamage(float damage){
		float damageToTake = damage * shield;
		if (currentHealthPoints - damageToTake <= 0) {
			currentHealthPoints = 0;
			Die ();
		} else {
			currentHealthPoints = currentHealthPoints - damageToTake;
		}
		UpdateHealthUI ();
	}

	public abstract void UpdateHealthUI ();
}
