using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireProjectile : MonoBehaviour {

	public GameObject projectile;
	public float height = 2;
	public float span = 2;

	void SpawnProjectile(){
		Vector3 barrelPos = new Vector3 (this.transform.position.x, this.transform.position.y + height, this.transform.position.z);
		Instantiate (projectile, barrelPos, transform.rotation);
	}
}
