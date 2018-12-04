using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {
	public GameObject currInterObj = null;
	public InteractableObject currInterObjScript = null;
	public Inventory inventory;

	void Update() {
		if(Input.GetButtonDown("Interact") && currInterObj) {
			if(currInterObjScript.storeable) {
				if(inventory.AddItem(currInterObj)) {
					currInterObj.SendMessage("setInactive");
					currInterObj = null;
					currInterObjScript = null;
				}
			}
		}	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("interObj")) {
			Debug.Log(other.name + " interactable collision");
			currInterObj = other.gameObject;
			currInterObjScript = currInterObj.GetComponent<InteractableObject>();
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.CompareTag("interObj")) {
			if(other.gameObject == currInterObj) {
				currInterObj = null;
				currInterObjScript = null;
			}
		}
	}
}
