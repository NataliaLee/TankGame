using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

	public GameObject[] weapons;
	private int currentWeapon = 0;

	void Start(){
		//LoadWeapons ();
	}

	void LoadWeapons(){
		//load weapons
		weapons= GameObject.FindGameObjectsWithTag("Weapon");
		if (weapons.Length==0) {
			Debug.LogError ("No Weapons");
			return;
		}
		weapons [0].SetActive (true);
		if (weapons.Length > 1) {
			//TODO sort?
			for (int i = 1; i < weapons.Length; i++)
				weapons [i].SetActive (false);
		}
	}

	void Update () {
		if (weapons.Length < 2)
			return;
		if (Input.GetKeyDown (KeyCode.Q)) {
			weapons [currentWeapon].SetActive (false);
			if (currentWeapon == 0)
				currentWeapon = weapons.Length - 1;
			else
				currentWeapon--;
		}

		if (Input.GetKeyDown (KeyCode.W)) {
			weapons [currentWeapon].SetActive (false);
			if (currentWeapon == weapons.Length - 1)
				currentWeapon = 0;
			else
				currentWeapon++;
		}
		weapons [currentWeapon].SetActive (true);
	}
}
