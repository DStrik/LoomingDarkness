using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {
	public GameObject currInterObj = null;
	public InteractableObject currInterObjScript = null;
	public Inventory inventory;
	public Animator animator;
	private bool usingTorch = false;

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

		if(Input.GetButtonDown("Use torch")) {
			GameObject torch = inventory.FindItemByType("Torch");
			Debug.Log("Pressing torch, torch item: " + torch.name);
			if(torch != null && !usingTorch) {
				usingTorch = true;
				animator.SetBool("Torch", usingTorch);
				// make torch item start depleting
			}
			else {
				Debug.Log("turn off torch");
				usingTorch = false;
				animator.SetBool("Torch", usingTorch);
				// make torch item stop depleting
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
