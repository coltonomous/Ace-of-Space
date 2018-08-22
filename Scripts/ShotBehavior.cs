using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour {

	public float bulletSpeed = 1f;
	public GameObject impactEffect;

	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * Time.deltaTime * bulletSpeed;
	
	}

	void OnCollisionEnter(Collision collisionInfo){

		if (collisionInfo.gameObject.tag != "Enemy") {
			GameObject impact = Instantiate (impactEffect, this.transform.position, Quaternion.identity);
			Destroy (this.gameObject);
			Destroy (impact, .5f);
		}
	}
}
