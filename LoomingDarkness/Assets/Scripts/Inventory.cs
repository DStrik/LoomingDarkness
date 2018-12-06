using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Inventory : MonoBehaviour {

	public GameObject[] inventory = new GameObject[4];
	public Button[] inventorySlots = new Button[4];
	public event EventHandler OnInventoryChange;
	public bool AddItem(GameObject item) {
		for(int i = 0; i < inventory.Length; i++) {
			if(inventory[i] == null) {
				inventory[i] = item;
				inventorySlots[i].image.overrideSprite = item.GetComponent<InteractableObject>().image;
				Debug.Log(item.name + " was added to the inventory");
				return true;
			}
		}
		Debug.Log(item.name + " was NOT added to the inventory");
		return false;
	}

	public bool RemoveItem(GameObject item) {
		for(int i = 0; i < inventory.Length; i++) {
			if(inventory[i] == item) {
				inventory[i] = null;
				inventorySlots[i].image.overrideSprite = null;
				Debug.Log(item.name + " was removed from inventory");
				rearrangeArray();
				Destroy(item);
				return true;
			}
		}
		Debug.Log(item.name + " was NOT removed from inventory");
		return false;
	}

	public GameObject FindItemByType(string type) {
		for(int i = 0; i < inventory.Length; i++) {
			if(inventory[i] != null) {
				if(inventory[i].GetComponent<InteractableObject>().type == type) {
					return inventory[i];
				}
			}
		}
		return null;
	}

	void rearrangeArray() {

		for(int i = 0; i < inventory.Length; i++) {
			if(inventory[i] == null) {
				for(int k = i + 1; k < inventory.Length; k++) {
					if(inventory[k] != null) {
						inventory[i] = inventory[k];
						inventory[k] = null;
						inventorySlots[i].image.overrideSprite = inventorySlots[k].image.overrideSprite;
						inventorySlots[k].image.overrideSprite = null;
						break;
					}
				}
			}
		}
	}
}