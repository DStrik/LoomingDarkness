using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {
	public GameObject currInterObj = null;
	public InteractableObject currInterObjScript = null;
	public Inventory inventory;
	public HealthHandler healthHandler;
	public LightHandler lightHandler;
	public GameObject currTorchInUse = null;
	public GameObject currDamager = null;
	public Damager currDamagerObjScript = null;
	

	void Update() {
		if(Input.GetButtonDown("Interact") && currInterObj) {
			if(currInterObjScript.storeable) {
				if(inventory.AddItem(currInterObj)) {
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
						inventory.RemoveItem(currInterObjScript.unlocker);
						currInterObjScript.setInactive();
						Debug.Log(currInterObj + " unlocked");
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
				currInterObj.GetComponent<switcher>().UseSwitch();
			}
		}

		if(Input.GetButtonDown("Use torch")) {
			Debug.Log("slot1: " + inventory.inventory[0]);
			GameObject torch = inventory.FindItemByType("Torch");
			//Debug.Log("Pressing torch, torch item: " + torch.name);
			if(torch != null && !lightHandler.usingTorch) {
				lightHandler.turnOnTorch(torch);
				FindObjectOfType<AudioManager>().Play("TorchOn");
				FindObjectOfType<AudioManager>().Play("SmallBurn");
				// make torch item start depleting
			}
			else if(torch !=null && lightHandler.usingTorch) {
				Debug.Log("turn off torch");
				lightHandler.turnOffTorch();
				FindObjectOfType<AudioManager>().Stop("SmallBurn");
				FindObjectOfType<AudioManager>().Play("ExtinguishTorch");
				// make torch item stop depleting
			}
		}

		if(Input.GetButtonDown("Eat food")) {
			GameObject food = inventory.FindItemByType("Food");
			if(food != null) {
				Debug.Log("Pressing Eat food, food item: " + food.name);
				healthHandler.heal(50); // change value to food heal value in future
				inventory.RemoveItem(food);
				FindObjectOfType<AudioManager>().Play("Eat");
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
					Debug.Log("turn off torch");
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
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("interObj")) {
			Debug.Log(other.name + " interactable collision");
			currInterObj = other.gameObject;
			currInterObjScript = currInterObj.GetComponent<InteractableObject>();
		}
		else if(other.CompareTag("Damager")) {
			Debug.Log(other.name + " damager collision");
			currDamager = other.gameObject;
			currDamagerObjScript = currDamager.GetComponent<Damager>();
			healthHandler.hurt(currDamagerObjScript.damage);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.CompareTag("interObj")) {
			if(other.gameObject == currInterObj) {
				currInterObj = null;
				currInterObjScript = null;
			}
		}
		else if(other.CompareTag("Damager")) {
			if(other.gameObject == currDamager) {
				currDamager = null;
				currDamagerObjScript = null;
			}
		}
	}
}
