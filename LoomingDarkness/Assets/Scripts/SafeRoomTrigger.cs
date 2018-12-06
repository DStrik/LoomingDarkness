using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeRoomTrigger : MonoBehaviour {

	// Use this for initialization
	public bool inSafeRoom;

	void Start() {
		inSafeRoom = true;
	}
	
	void OnTriggerExit2D(Collider2D other) {
		inSafeRoom = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		inSafeRoom = true;
	}
}
