using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathDestroy : MonoBehaviour {

	/* 
	 * This script is needed because the other destroy objects script destroyed them too early
	 * for some reason, so they would pop out of view. Additionally, this made it hard to appreciate 
	 * the death animations.
	*/

	public float destroyDist = 30;
	GameObject player;

	void Awake(){
		player = GameObject.FindWithTag ("Player");
	}

	// Update is called once per frame
	void Update () {
		if (player.transform.position.z > this.transform.position.z + destroyDist) {
			Destroy (gameObject);
		}
	}
}
