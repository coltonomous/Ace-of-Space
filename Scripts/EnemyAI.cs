using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	GameObject player;
	Animator enemyAnims;

	public float aggroDistance;
	public float range;

	bool inCover;

	void Start(){
		player = GameObject.FindWithTag ("Player");
		enemyAnims = GetComponent<Animator> ();
		inCover = true;
	}
	
	// Update is called once per frame
	void Update () {

		if(!enemyAnims.GetBool("isDeath")){
			
			// Enemies aggro/ peek from cover if player is close enough
			if ((Vector3.Distance(transform.position, player.transform.position) <= aggroDistance) && inCover) {
				if (transform.position.x < player.transform.position.x) {
					transform.position = new Vector3 (transform.position.x + 2, transform.position.y, transform.position.z);
					enemyAnims.Play ("Stand_AppearR");
				} else {
					transform.position = new Vector3 (transform.position.x - 2, transform.position.y, transform.position.z);
					enemyAnims.Play ("Stand_AppearL");
				}
				inCover = false;

			// Enemies attack if player is within range and they are not in cover
			} else if (Vector3.Distance(transform.position, player.transform.position) <= range){
				enemyAnims.Play ("StandFire0");
			}

			// Rotate to track player
			if (!inCover) {
				Vector3 direction = player.transform.position - this.transform.position;
				this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation(direction), 0.2f);
			}
		}
	}
}
