using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : Menu {
	public GameObject gameOverPanel;

	void Start () {
		gameOverPanel.SetActive (false);
		EventManager.StartListening ("PlayerDeath",GameOver);
	}

	public void GameOver(){
		GameManager.instance.inGame=false;
		gameOverPanel.SetActive(true);
	}

	public void Replay(){
		gameOverPanel.SetActive(false);
		GameManager.instance.StartGame ();
	}
}
