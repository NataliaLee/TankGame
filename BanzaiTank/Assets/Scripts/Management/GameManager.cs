using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public GameObject gameOverUI;

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
		//GameObject gameOver=(GameObject)Instantiate (gameOverUI);
		//gameOver.transform.SetParent (canvas.transform);
		gameOverUI.SetActive(true);
	}
		

	public void Replay(){
		Application.LoadLevel(Application.loadedLevel);
	}

	public void Exit(){
		Application.Quit ();
	}
}
