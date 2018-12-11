using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoad : MonoBehaviour {

	private GameObject character;

	void Start () {
		GameObject load = GameObject.Find("LoadGame");

		if(load != null){
			if(load.GetComponent<StartingSceneLoad>().load == true) {
				loadPlayer();
			}
		}
		
	}

	public void loadPlayer() {
		Debug.Log("Inside Player.loadPlayer");

		PlayerData data = SaveSystem.LoadPlayer();
		Debug.Log("Got the player data");

		GameObject library = GameObject.Find("PrefabInventoryLibrary");
		Debug.Log("Got the library");

		character = GameObject.Find("Character");
		if(character == null) {
			Debug.Log("Character is: null");
		}
		Debug.Log("data.health = " + data.health);
		character.GetComponent<HealthHandler>().SetHealth(data.health);

		Debug.Log("Printing inventory:");
		for(int i = 0; i < 4; i++) {
			Debug.Log("Slot " + i + ": " + data.Inventory[i]);
		}
		for(int i = 0; i < data.torchSave.Length; i++) {
			if(data.torchSave[i] != null) {
				
				GameObject obj;
				for(int j = 0; j < library.GetComponent<PrefabLibrary>().prefabs.Length; j++) {
					if(data.torchSave[i].name == library.GetComponent<PrefabLibrary>().prefabs[j].name) {
						obj = library.GetComponent<PrefabLibrary>().prefabs[j];

						character.GetComponent<Inventory>().AddItem(obj.transform.GetChild(0).gameObject);
						obj.transform.GetChild(0).gameObject.SetActive(false);
						character.GetComponent<Inventory>().inventory[i].GetComponentInParent<Torch>().SetDurability(data.torchSave[i].durability);
					}
				}
			}
		}

		for(int i = 0; i < data.Inventory.Length; i++) {
			if(data.Inventory[i] != null) {
				for(int j = 0; j < library.GetComponent<PrefabLibrary>().prefabs.Length; j++) {
					if(data.Inventory[i] == library.GetComponent<PrefabLibrary>().prefabs[j].name) {
						Debug.Log("data.inventory: " + data.Inventory[i] + "|| library name: " + library.GetComponent<PrefabLibrary>().prefabs[j].name);
						character.GetComponent<Inventory>().AddItem(library.GetComponent<PrefabLibrary>().prefabs[j]);
						library.GetComponent<PrefabLibrary>().prefabs[j].SetActive(false);
					}
				}
			}
		}


		Debug.Log("Finished the inventory");
		Vector3 position;

		position.x = data.position[0];
		position.y = data.position[1];
		position.z = data.position[2];
		Debug.Log("Changing position");
		//transform.position = position;
	}
}
