using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTrack : MonoBehaviour {

	public float zDistance = 300;	// Distance from player to generate new track.
	public float trackLength = 1024;	// Length of track in Unity units.

	public Transform playerTransform;
	public Transform trackPrefab;

	float lastZPos;	// Tracks player position at last track spawn

	void start(){
		lastZPos = playerTransform.position.z;
	}

	void Update(){

		// If player has progressed sufficient distance, make moar track!
		if (playerTransform.position.z >= lastZPos + trackLength - zDistance) {
			Instantiate (trackPrefab, new Vector3 (0, 0, playerTransform.position.z + zDistance + (trackLength / 2)), Quaternion.identity);
			lastZPos = playerTransform.position.z;
		}
	}
}