using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public GameObject[] inventory = new GameObject[6];

	public bool AddItem(GameObject item) {
		bool wasAdded = false;
		for(int i = 0; i < inventory.Length; i++) {
			if(inventory[i] == null) {
				inventory[i] = item;
				Debug.Log(item.name + " was added to the inventory");
				wasAdded = true;
				return true;
			}
		}

		if(wasAdded == false) {
			Debug.Log(item.name + " was NOT added to the inventory");
		}
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
}
