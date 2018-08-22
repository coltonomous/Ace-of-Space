using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Target : MonoBehaviour {

	Animator enemyAnim;
	public float health = 3f;	// Enemy health

	void Start(){
		enemyAnim = GetComponent<Animator> ();
	}

	public void TakeDamage(float damage){
		if (health > 0f) {
			health -= damage;
			if (health <= 0f) {
				Die ();
			}
		}
	}

	void Die(){
		// Play death sounds and animations
		AudioSource deathSound = GetComponent<AudioSource> ();
		deathSound.Play ();
		enemyAnim.SetBool ("isDeath", true);
	}
}