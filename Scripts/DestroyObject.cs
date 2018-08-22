using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {

	// Destroys objects no longer within camera view to save memory.
	void OnBecameInvisible(){
		Destroy (gameObject);
	}
}
