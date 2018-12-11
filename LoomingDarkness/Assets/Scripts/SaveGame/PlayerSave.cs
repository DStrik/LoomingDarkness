using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSave : MonoBehaviour {

	private float health;
	private string[] Inventory;

	public int scene;

	public TorchSave[] torchSave;

	private GameObject character;

	public void SavePlayer() {
		character = GameObject.Find("Character");
		Debug.Log("Inside SavePlayer function");
		scene = SceneManager.sceneCountInBuildSettings;
		health = character.GetComponent<HealthHandler>().GetHealth();
		Inventory = new string[4];
		torchSave = new TorchSave[4];
		string invName;
		int torchIndex = 0;
		for(int i = 0; i < 4; i++) {
			if(character.GetComponent<Inventory>().inventory[i] != null){
					invName = character.GetComponent<Inventory>().inventory[i].name;

				if(invName.Contains("Torch")) {
					TorchSave torch = new TorchSave();
					torch.name = character.GetComponent<Inventory>().inventory[i].transform.parent.name;
					torch.index = i;
					torch.durability = character.GetComponent<Inventory>().inventory[i].GetComponentInParent<Torch>().GetDurability();
					torchSave[torchIndex] = torch;
					torchIndex++;
				}
				else {
				Inventory[i] = invName;
				}
			}
		}
		foreach(string s in Inventory) {
		Debug.Log("InventoryItem: " + s);
		}

		SaveSystem.SavePlayer(this);
	}

	public float GetHealth() {
		return health;
	}

	public string GetInventory(int index) {
		return Inventory[index];
	}

	void OnTriggerEnter2D(Collider2D other) {
		SavePlayer();
    }
	
}
