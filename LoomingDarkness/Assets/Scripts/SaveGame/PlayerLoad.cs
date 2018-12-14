using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLoad : MonoBehaviour {

	private GameObject character;
	public GameObject inactive;

	void Start() {
		GameObject load = GameObject.Find("CheckLoadGame");

		if(load != null){
			if(load.GetComponent<StartingSceneLoad>().load == true) {
				Invoke("loadPlayer", 0.05f);
				load.GetComponent<StartingSceneLoad>().SetLoadFalse();
			}
		}
		
	}

	public void loadPlayer() {
		PlayerData data = SaveSystem.LoadPlayer();
		GameObject library = GameObject.Find("PrefabInventoryLibrary");
		character = GameObject.Find("Character");
		character.GetComponent<HealthHandler>().SetHealth(data.health);

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
						character.GetComponent<Inventory>().AddItem(library.GetComponent<PrefabLibrary>().prefabs[j]);
						character.GetComponent<SetInactive>().SetInactiveItem(data.Inventory[i]);						
						library.GetComponent<PrefabLibrary>().prefabs[j].SetActive(false);
					}
				}
			}
		}

		Vector3 position;

		position.x = data.position[0];
		position.y = data.position[1];
		position.z = data.position[2];

		character.transform.position = position;

		foreach(string s in data.InactiveItems) {
			if(GameObject.Find(s) != null) {
				character.GetComponent<SetInactive>().SetInactiveItem(s);
				
				if(s.Contains("Torch")) {
					GameObject.Find(s).transform.GetChild(0).gameObject.SetActive(false);
				}
				else {
					GameObject.Find(s).SetActive(false);
				}
			}
		}
	}
}
