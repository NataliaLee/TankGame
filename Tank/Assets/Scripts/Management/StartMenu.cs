using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : Menu {
	public GameObject startPanel;


	public void StartGame(){
		startPanel.SetActive (false);
		GameManager.instance.StartGame ();
	}
}
