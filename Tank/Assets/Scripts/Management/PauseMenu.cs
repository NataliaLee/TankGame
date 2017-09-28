using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : Menu {
	public GameObject pausePanel;

	void Start () {
		pausePanel.SetActive(false);
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			//if in start menu or in game over menu
			if (!GameManager.instance.inGame)
				return;
			if (!pausePanel.activeInHierarchy) {
				PauseGame ();
			}else {
				ContinueGame ();   
			}
		}
	}

	private void PauseGame()
	{
		Debug.Log ("PauseGame");
		pausePanel.SetActive(true);
		Time.timeScale = 0;
	} 

	public void ContinueGame()
	{
		Debug.Log ("Continue game");
		pausePanel.SetActive(false);
		Time.timeScale = 1;
	}

	public void Restart(){
		GameManager.instance.Restart();
		ContinueGame();
	}
}
