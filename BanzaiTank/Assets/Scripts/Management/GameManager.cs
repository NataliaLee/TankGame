using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public GameObject gameOverUI;
	public GameObject startMenuUI;
	public GameObject playerPrefab;

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

	void Start(){
		EventManager.StartListening ("PlayerDeath",GameOver);
	}

	public void GameOver(){
		gameOverUI.SetActive(true);
	}
		

	public void Replay(){
		//Application.LoadLevel(Application.loadedLevel);
		gameOverUI.SetActive(false);
		StartGame ();
	}

	public void Exit(){
		Application.Quit ();
	}

	public void Play(){
		startMenuUI.SetActive (false);
		StartGame ();
	}

	void StartGame(){
		EventManager.TriggerEvent ("Start game");
		Instantiate (playerPrefab);
	}
}
