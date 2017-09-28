using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour {

	void OnEnable(){
		Slider slider = GetComponent<Slider> ();
		slider.value = slider.maxValue;
	}

	void OnDisable(){
		gameObject.transform.position = new Vector3 (0,0,0);
	}
}
