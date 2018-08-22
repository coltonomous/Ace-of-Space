using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurretControls : MonoBehaviour {

	// Govern turret controls using mouse movement
	public float rotation = 5f;
	public float yaw = 0f;
	public float pitch = 0f;
	public float xLock = 45;
	public float yLock = 20;

	// Govern turrey shooting
	public float damage = 1f;
	public float range = 1000f;
	public float impactForce = 60f;	// applies force to push objects backward slightly.
	public float fireRate = 10f;

	public Transform turret;
	//public ParticleSystem muzzleFlash;
	public GameObject impactEffct;
	public Text scoreText;

	private float nextTimeToFire = 0f;
	private int score = 0;

	void Awake(){
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Confined;
	}

	void Update(){

		// Allows turret movement based on mouse position on screen.
		yaw += rotation * Input.GetAxis("Mouse X") * Time.deltaTime;
		pitch -= rotation * Input.GetAxis("Mouse Y") * Time.deltaTime;

		// Corrects Turret rotation to "lock" rotational range of turret
		if (yaw < -xLock) {
			yaw = -xLock;
		} else if(yaw > xLock){
			yaw = xLock;
		}

		if(pitch < -yLock){
			pitch = -yLock;
		} else if(pitch > yLock){
			pitch = yLock;
		}

		// Updates turret rotation
		transform.eulerAngles = new Vector3 (pitch, yaw, 0f);

		// Shooting controls
		if (Input.GetButton ("Fire1") && Time.time >= nextTimeToFire) {
			nextTimeToFire = Time.time + 1f / fireRate;
			Shoot ();
		}

		// Resets turret position with middle mouse click.
		if (Input.GetMouseButtonDown (2)) {
			pitch = 0f;
			yaw = 0f;
			transform.eulerAngles = new Vector3(-pitch, yaw, 0f);
		}
	}

	void Shoot(){
		//muzzleFlash.Play ();

		AudioSource gunshot = GetComponent<AudioSource> ();
		gunshot.Play();

		// Collision detection of bullets with objects
		RaycastHit hit;
		if (Physics.Raycast (turret.transform.position, turret.transform.forward, out hit, range)) {

			// Bullets only hit objects with a Target script attached
			Target target = hit.transform.GetComponent<Target> ();
			if (target != null) {

				if (target.health > 0) {
					UpdateScore ();
				} 
					
				target.TakeDamage (damage);
			}

			// Applies backward force to objects hit by "bullets"
			if (hit.rigidbody != null) {
				hit.rigidbody.AddForce (-hit.normal * impactForce);
			}

			// Impact particle effects
			GameObject impact = Instantiate (impactEffct, hit.point, Quaternion.LookRotation (hit.normal));
			Destroy (impact, .5f);
		}
	}

	void UpdateScore(){
		score += 50;
		scoreText.text = "Score: " + score;
	}
}