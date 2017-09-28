using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectsToPool : MonoBehaviour {
	public ObjectToPool[] objectsToPool;


	void Start () {
		foreach (var objectToPool in objectsToPool) {
			PoolManager.instance.CreatePool (objectToPool.prefab,objectToPool.countToPool);
		}
	}

}

[Serializable]
public class ObjectToPool
{
	public GameObject prefab;
	public int countToPool;
}


