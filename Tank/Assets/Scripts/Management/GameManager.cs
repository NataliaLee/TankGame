using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public GameObject playerPrefab;

	public bool inGame = false;

	static GameManager _instance;
	public static GameManager instance {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType<GameManager> ();
			}
			return _instance;
		}
	}
	private Canvas canvas;

	void Awake()
	{
		canvas = FindObjectOfType<Canvas> ();
	}

	public void StartGame(){
		inGame = true;
		EventManager.TriggerEvent ("Start game");
		Instantiate (playerPrefab);
	}

	public void Restart(){
		EventManager.TriggerEvent ("OnRestart");
		StartGame ();
	}
}
