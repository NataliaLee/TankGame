using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
	public GameObject[] enemiesPrefabs;
	public int maxEnemiesCount=10;
	public float spawn_time=5f;
	public float spawn_offset = 50f;
	public Transform parentGameObject;

	void Start () {
		EventManager.StartListening ("PlayerDeath",OnPlayerDeath);
		EventManager.StartListening ("Start game",OnStartGame);
	}

	void OnStartGame(){
		InvokeRepeating ("SpawnEnemyInvoke",1f,spawn_time);		
	}

	void OnPlayerDeath(){
		EventManager.StopListening ("PlayerDeath", OnPlayerDeath);
		CancelInvoke ();
	}

	void SpawnEnemyInvoke(){
		int enemiesCount=GameObject.FindGameObjectsWithTag ("Enemy").Length;
		if (enemiesCount >= maxEnemiesCount) {
			CancelInvoke ();
			return;
		}
		SpawnEnemy ();
	}

	public void SpawnEnemy(){
		if (enemiesPrefabs.Length == 0)
			return;
		float x = Screen.width+spawn_offset;
		float y = Screen.height+spawn_offset;
		if (Random.Range(0,2)==0) {
			//spawn top/bottom
			x = Random.Range (-spawn_offset, Screen.width + spawn_offset);
			if (Random.Range (0, 2)==0) {
				//bottom
				y = -spawn_offset;
			}			
		} else {
			//spawn left/right
			y=Random.Range(-spawn_offset,Screen.height+spawn_offset);
			if (Random.Range (0, 2)==0) {
				//left
				x=-spawn_offset;
			}
		}
		Vector3 position = Camera.main.ScreenToWorldPoint (new Vector3(x,y,0));
		position.z = 0;
		int enemyToSpawnType = Random.Range (0,enemiesPrefabs.Length);
		GameObject newEnemy=(GameObject)Instantiate (enemiesPrefabs[enemyToSpawnType],position,Quaternion.identity);
		newEnemy.transform.SetParent (parentGameObject);
	}
}
