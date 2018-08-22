using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObstacles : MonoBehaviour {

	public float zDistance = 10;	// Distance from player to generate new obstacles.
	public float minSpread = 5;		// Controls lower bound of space between obstacles in z direction.
	public float maxSpread = 10;	// Controls upper bound of space between obstacles in z direction.

	public Transform playerTransform;
	public Transform obstaclePrefab;

	public Transform[] enemies;

	public int enemySpawnProbability;	// Higher values mean less enemies.

	float zSpread;
	float lastZPos;

	void start(){
		lastZPos = Mathf.NegativeInfinity;
		zSpread = Random.Range (minSpread, maxSpread);	// Randomizes spacing between obstacles in z direction.
	}

	void Update(){

		// Generates new obstacles at zDistance from player, zSpread apart so long as player is moving.
		if (playerTransform.position.z - lastZPos >= zSpread) {
			float lanePos = Random.Range (1, 14);

			// Controls position of obstacles in x position.
			if (lanePos > 0f) {
				lanePos -= 6.5f;
			}

			// Generates enemies in cover based on spawn probability
			if(Random.Range(1, enemySpawnProbability) == enemySpawnProbability - 1){
				int enemyType = Random.Range (0, 3);
				Instantiate (obstaclePrefab, new Vector3 (lanePos, 1, playerTransform.position.z + zDistance), Quaternion.identity);
				Instantiate (enemies[enemyType], new Vector3 (lanePos, .64f, playerTransform.position.z + zDistance + 3), Quaternion.identity);
			
			// Otherwise, just generates obstacles
			} else{
				Instantiate (obstaclePrefab, new Vector3 (lanePos, 1, playerTransform.position.z + zDistance), Quaternion.identity);
			}

			lastZPos = playerTransform.position.z;	// Updates time to next spawn
			zSpread = Random.Range (minSpread, maxSpread);
		}
	}
}