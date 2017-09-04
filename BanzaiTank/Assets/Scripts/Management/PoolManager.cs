using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PoolManager : MonoBehaviour {

	Dictionary<int,List<GameObject>> poolDictionary = new Dictionary<int, List<GameObject>> ();

	static PoolManager _instance;

	public static PoolManager instance {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType<PoolManager> ();
			}
			return _instance;
		}
	}

	public void CreatePool(GameObject prefab, int poolSize) {
		Debug.Log ("Create pool "+prefab.name+" "+poolSize);
		int poolKey = prefab.GetInstanceID ();

		if (!poolDictionary.ContainsKey (poolKey)) {
			poolDictionary.Add (poolKey, new List<GameObject> ());

			GameObject poolHolder = new GameObject (prefab.name + " pool");
			poolHolder.transform.parent = transform;

			for (int i = 0; i < poolSize; i++) {
				GameObject newObject = Instantiate (prefab) as GameObject;
				newObject.SetActive (false);
				poolDictionary [poolKey].Add (newObject);
				newObject.transform.SetParent (poolHolder.transform);
			}
		}
	}

	public GameObject ReuseObject(GameObject prefab, Vector3 position, Quaternion rotation) {
		int poolKey = prefab.GetInstanceID ();
		if (poolDictionary.ContainsKey (poolKey)) {			
			GameObject objectToReuse = poolDictionary [poolKey].FirstOrDefault (obj => !obj.activeInHierarchy);
			if (objectToReuse == null) {
				//расширим пул 
				objectToReuse = Instantiate (prefab) as GameObject;
				objectToReuse.SetActive (false);
				objectToReuse.transform.SetParent (poolDictionary [poolKey][0].transform.parent);
				poolDictionary [poolKey].Add (objectToReuse);
			}
			objectToReuse.transform.SetPositionAndRotation (position, rotation);
			objectToReuse.SetActive (true);
			return objectToReuse;
		}
		return null;
	}

}