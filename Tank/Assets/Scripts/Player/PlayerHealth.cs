using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health {
	public GameObject healthUIPrefab;
	private GameObject healthPanel;
	private Canvas canvas;
	protected Slider healthSlider;

	void Start(){
		EventManager.StartListening ("OnRestart",Remove);
		canvas = FindObjectOfType<Canvas> ();
		healthPanel = Instantiate(healthUIPrefab);
		healthPanel.transform.SetParent(canvas.transform, false);
		healthPanel.transform.SetAsFirstSibling ();
		healthSlider = healthPanel.GetComponentInChildren<Slider> ();
		Die += OnDead;
	}

	public override void UpdateHealthUI(){
		healthSlider.value = currentHealthPoints/totalHealthPoints;
	}

	public void OnDead(){
		Destroy(gameObject);
		Destroy (healthPanel);
		EventManager.TriggerEvent ("PlayerDeath");
	}

	public void Remove(){
		EventManager.StopListening ("OnRestart", Remove);
		Destroy(gameObject);
		Destroy (healthPanel);
	}
}
