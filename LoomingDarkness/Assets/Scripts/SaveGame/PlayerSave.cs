using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSave : MonoBehaviour {

	private float health;
	private string[] Inventory;

	public int scene;
	public Vector3 position;

	public TorchSave[] torchSave;

	private GameObject character;

	public List<string> InactiveItems = new List<string>();

	public void SavePlayer() {
		character = GameObject.Find("Character");
		InactiveItems = character.GetComponent<SetInactive>().GetInactiveItems();
		position = new Vector3(character.transform.position.x, character.transform.position.y, character.transform.position.z);
		scene = SceneManager.GetActiveScene().buildIndex;
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
					torch.durability = character.GetComponent<Inventory>().inventory[i].GetComponentInParent<Torch>().GetDurability();
					torchSave[torchIndex] = torch;
					torchIndex++;
				}
				else {
				Inventory[i] = invName;
				}
			}
		}

		SaveSystem.SavePlayer(this);

		this.gameObject.SetActive(false);
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
