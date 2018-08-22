using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

	public PlayerMovement movement;
	public Text healthText;
	public int health = 10;
	public float impactForce = 35000f;
	public float iFrames = 5f; 	// To prevent ista-death from a jumpy collision

	AudioSource[] audioSources;
	AudioSource engineSound;
	AudioSource alarmSound;
	AudioSource crashSound;

	public Dot_Truck_Controller vehicleAnims;
	public GameObject tireSmoke;
	public GameObject explosion;

	private float nextDamage;

	void Start(){
		audioSources = this.GetComponents<AudioSource> ();
		engineSound = audioSources [0];
		alarmSound = audioSources [1];
		crashSound = audioSources [2];
	}

	void OnCollisionEnter(Collision collisionInfo){

		// Player loses health upon collision with enemy attacks and obstacles.
		if (collisionInfo.collider.tag == "Obstacle" || collisionInfo.collider.tag == "Projectile") {
			if (Time.time >= nextDamage){
				nextDamage = Time.time + 1f / iFrames;
				crashSound.Play ();
				UpdateHealth (1);
			}
		} else if (collisionInfo.collider.tag == "Enemy") {
			// Kill enemy on collision
			Target enemyTarget = collisionInfo.gameObject.GetComponent<Target> ();

			if (enemyTarget.health > 0) {
				crashSound.Play ();
				UpdateHealth (1);
			}

			enemyTarget.TakeDamage (enemyTarget.health);

			// Send enemy flying
			Rigidbody enemyBody = collisionInfo.gameObject.GetComponent<Rigidbody> ();
			if (enemyBody != null) {
				enemyBody.AddForce (0, impactForce * .6f * Time.deltaTime, impactForce * Time.deltaTime, ForceMode.Impulse);
			}
		}
		// Disables movement of "dead" player.
		if (health <= 0) {
			movement.forwardForce = 0f;
			movement.enabled = false;

			vehicleAnims.enabled = false;
			alarmSound.Stop ();
			tireSmoke.GetComponent<ParticleSystem> ().Stop();
			engineSound.Stop();

			Instantiate (explosion, transform.position, Quaternion.identity);
			this.gameObject.SetActive (false);
		}
	}

	void UpdateHealth(int damage){
		health -= damage;

		if (health <= 2) {
			alarmSound.Play ();
		} else if (health < 0) {
			health = 0;
		}
		healthText.text = "Health: " + health;
	}
}
