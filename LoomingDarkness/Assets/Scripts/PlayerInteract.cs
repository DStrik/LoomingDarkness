using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteract : MonoBehaviour {
	public GameObject currInterObj = null;
	public InteractableObject currInterObjScript = null;
	public Inventory inventory;
	public HealthHandler healthHandler;
	public LightHandler lightHandler;
	public GameObject currTorchInUse = null;
	public GameObject currDamager = null;
	public Damager currDamagerObjScript = null;
	private bool stillDamage = false;

	public SetInactive inactive;

	void Update() {
		if(Input.GetButtonDown("Interact") && currInterObj) {
			if(currInterObjScript.storeable) {
				if(inventory.AddItem(currInterObj)) {
					if(currInterObj.name.Contains("Torch")) {
						inactive.SetInactiveItem(currInterObj.transform.parent.gameObject.name);
					}
					else {
						inactive.SetInactiveItem(currInterObj.name);
					}
					currInterObj.SendMessage("setInactive");
					currInterObj = null;
					currInterObjScript = null;
					FindObjectOfType<AudioManager>().Play("ItemPickup");				
				}
			}
			else if(currInterObjScript.type == "Fountain") {
				healthHandler.heal(100);
			}
			else if(currInterObjScript.openable) {
				if(currInterObjScript.locked) {
					if(inventory.FindItem(currInterObjScript.unlocker)) {
						currInterObjScript.locked = false;
						FindObjectOfType<AudioManager>().Play("Unlock");
						inventory.RemoveItem(currInterObjScript.unlocker);
						currInterObjScript.setInactive();
						Debug.Log(currInterObj + " unlocked");
						inactive.SetInactiveItem(currInterObj.name);	
					}
					else {
						Debug.Log(currInterObj + " not unlocked");
					}
				}
				else {
					currInterObjScript.setInactive();
				}
			}
			else if(currInterObjScript.type == "Switch") {
				FindObjectOfType<AudioManager>().Play("Switch");
				currInterObj.GetComponent<switcher>().UseSwitch();
			}else if(currInterObjScript.type == "Dialogue") {
				currInterObj.GetComponent<InteractableDialogue>().TriggerDialogue();
			}
		}
		else if(Input.GetButtonDown("Slot 0")) {
			useInventoryItem(0);
		}
		else if(Input.GetButtonDown("Slot 1")) {
			useInventoryItem(1);
		}
		else if(Input.GetButtonDown("Slot 2")) {
			useInventoryItem(2);
		}
		else if(Input.GetButtonDown("Slot 3")) {
			useInventoryItem(3);
		}
	}

	void useInventoryItem(int invNum) {
		GameObject item = inventory.inventory[invNum];
		if(item != null) {
			if(item.GetComponent<InteractableObject>().type == "Torch") {
				if(currTorchInUse == null) {
					currTorchInUse = item;
					lightHandler.turnOnTorch(currTorchInUse);
					FindObjectOfType<AudioManager>().Play("TorchOn");
					FindObjectOfType<AudioManager>().Play("SmallBurn");
				}
				else if(currTorchInUse != null) {
					// Debug.Log("turn off torch");
					lightHandler.turnOffTorch();
					FindObjectOfType<AudioManager>().Stop("SmallBurn");
					FindObjectOfType<AudioManager>().Play("ExtinguishTorch");
					if(currTorchInUse == item) {
						currTorchInUse = null;
					}
					else {
						currTorchInUse = item;
						lightHandler.turnOnTorch(currTorchInUse);
						FindObjectOfType<AudioManager>().Play("TorchOn");
						FindObjectOfType<AudioManager>().Play("SmallBurn");
					}
				}
			}
			else if(item.GetComponent<InteractableObject>().type == "Food") {
				healthHandler.heal(50); // change value to food heal value in future
				inventory.RemoveItem(item);
				FindObjectOfType<AudioManager>().Play("Eat");
			}
			else if(currInterObj != null && currInterObjScript.locked && item == currInterObjScript.unlocker) {
				currInterObjScript.locked = false;
				FindObjectOfType<AudioManager>().Play("Unlock");	
				inventory.RemoveItem(currInterObjScript.unlocker);
				Debug.Log(currInterObj + " unlocked");
				inactive.SetInactiveItem(currInterObj.name);	
				currInterObjScript.setInactive();
			}
		}
	}
	

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("interObj")) {
			// Debug.Log(other.name + " interactable collision");
			currInterObj = other.gameObject;
			currInterObjScript = currInterObj.GetComponent<InteractableObject>();
		}
		else if(other.CompareTag("Damager")) {
			// Debug.Log(other.name + " damager collision");
			stillDamage = true;
			currDamager = other.gameObject;
			currDamagerObjScript = currDamager.GetComponent<Damager>();
			healthHandler.hurt(currDamagerObjScript.damage);
			Invoke("stillDamaging", 2);
		}
		else if(other.CompareTag("SafeZone") && lightHandler.usingTorch){
			lightHandler.turnOffTorch();
			currTorchInUse = null;
			FindObjectOfType<AudioManager>().Stop("SmallBurn");
			FindObjectOfType<AudioManager>().Play("ExtinguishTorch");
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.CompareTag("interObj")) {
			if(other.gameObject == currInterObj) {
				if(currInterObjScript.type == "Dialogue") {
					FindObjectOfType<DialogueManager>().EndDialogue();
				}
				currInterObj = null;
				currInterObjScript = null;
			}
		}
		else if(other.CompareTag("Damager")) {
			if(other.gameObject == currDamager) {
				stillDamage = false;
				currDamager = null;
				currDamagerObjScript = null;
			}
		}
	}

	public void stillDamaging() {
		if(stillDamage) {
			currDamagerObjScript = currDamager.GetComponent<Damager>();
			healthHandler.hurt(currDamagerObjScript.damage);
			Invoke("stillDamaging", 2);
		}
	}
}
