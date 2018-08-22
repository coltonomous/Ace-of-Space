using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	public Transform player;
	public Vector3 offset;

	// Update is called once per frame
	void Update () {

		// Make (camera or turret) follow the player.
		transform.position = player.position + offset;
	}
}