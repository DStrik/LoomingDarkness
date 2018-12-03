using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public GameObject[] inventory = new GameObject[6];

	public void AddItem(GameObject item) {
		bool wasAdded = false;
		for(int i = 0; i < inventory.Length; i++) {
			if(inventory[i] == null) {
				inventory[i] = item;
				Debug.Log(item.name + " was added to the inventory");
				wasAdded = true;
				break;
			}
		}

		if(wasAdded == false) {
			Debug.Log(item.name + " was NOT added to the inventory");
		}
	}
}
