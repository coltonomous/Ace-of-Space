using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public Rigidbody playerJeep;
	public float forwardForce = 800f;
	public float sidewaysForce = 50f;

	// For directional rotation when steering
	public float rotationStep = 1f;
	public float maxRotation = 20f;
	private float drift = 0f;

	// Update is called once per frame
	void FixedUpdate () {

		// Constant forward force applied for "endless runner" feel.
		if(transform.position.y >= .92){
			playerJeep.AddForce (0, 0, forwardForce * Time.deltaTime, ForceMode.Impulse);

			// Controls right player movement.
			if (Input.GetKey ("d") || Input.GetKey(KeyCode.RightArrow)) {
				drift += rotationStep;

				if (drift > maxRotation) {
					drift = maxRotation;
				} 
				playerJeep.AddForce (sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
			}

			// Controls left player movement.
			if (Input.GetKey ("a") || Input.GetKey(KeyCode.LeftArrow)) {
				drift -= rotationStep;

				if (drift < -maxRotation) {
					drift = -maxRotation;
				}
				playerJeep.AddForce (-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
			}

			// Rotates player in direction of horizontal movement. Simulates steering
			transform.eulerAngles = new Vector3 (0f, drift, 0f);

			// Corrects back toward forward when no buttons are pressed
			if (!Input.GetKey ("d") && !Input.GetKey(KeyCode.RightArrow) && drift > 0f) {
				drift -= rotationStep;
			} else if(!Input.GetKey ("a") && !Input.GetKey(KeyCode.LeftArrow) && drift < 0){
				drift += rotationStep;
			}
		}
	}
}
