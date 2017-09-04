using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : Health {

	public GameObject healthUIPrefab;

	public float healthPanelOffset = 1f;
	private Canvas canvas;
	private GameObject healthPanel;
	protected Slider healthSlider;

	void Start(){
		canvas = FindObjectOfType<Canvas> ();
		EventManager.StartListening ("PlayerDeath",OnPlayerDeath);
		Die += OnDead;
		setHealthPanel ();
	}

	void Update(){
		if (healthPanel == null)
			return;
		//Vector3 worldPos = new Vector3(transform.position.x, transform.position.y + healthPanelOffset, transform.position.z);
		Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);//worldPos);
		healthPanel.transform.SetPositionAndRotation(screenPos, transform.rotation);

	}

	public override void UpdateHealthUI(){
		if(healthSlider!=null)
			healthSlider.value = currentHealthPoints/totalHealthPoints;
	}

	private void setHealthPanel(){
		Vector3 outOfScreenPos = new Vector3 (Screen.width,Screen.height,0);
		healthPanel = PoolManager.instance.ReuseObject (healthUIPrefab,outOfScreenPos,transform.rotation);
		healthPanel.transform.SetParent(canvas.transform, false);
		healthPanel.transform.SetAsFirstSibling ();
		healthSlider = healthPanel.GetComponentInChildren<Slider>();
		RectTransform rectRansform = healthPanel.GetComponent<RectTransform> ();
		rectRansform.pivot = new Vector2 (rectRansform.pivot.x,healthPanelOffset);
	}

	public void OnDead(){
		Remove ();
		// spawn new enemy 
		EnemyManager enemyManager=GameManager.instance.GetComponent<EnemyManager>();
		enemyManager.SpawnEnemy ();
	}

	private void OnPlayerDeath(){
		EventManager.StopListening ("PlayerDeath", OnPlayerDeath);
		Remove ();
	}

	private void Remove(){
		gameObject.SetActive (false);
		Destroy (gameObject);
		healthPanel.SetActive (false);
	}
}
