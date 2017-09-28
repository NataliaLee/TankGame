using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
	public GameObject bulletPrefab;
	//место откуда будет вылетать пуля
	public Transform bulletSpawn;
	//скорость пули
	public float bulletSpeed=30f;
	//урон от пули
	public float damage=1f;
	//TODO задержка перед выпуском другой пули?

	void Update(){
		if (Input.GetKeyDown (KeyCode.X)) {
			Fire ();
		}
	}

	public void Fire(){
		var bullet=PoolManager.instance.ReuseObject (bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
		Bullet bulletScript=bullet.GetComponent<Bullet> ();
		bulletScript.damage = damage;
		bullet.GetComponent<Rigidbody2D> ().velocity=bullet.transform.up*bulletSpeed;
	}
}
