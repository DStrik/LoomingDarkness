using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {

	public float health;
	public string[] Inventory;
	public float[] position;
	public int scene;
	public TorchSave[] torchSave;

	public PlayerData(PlayerSave player) {
		health = player.GetHealth();
		Inventory = new string[4];
		torchSave = new TorchSave[player.torchSave.Length];
		for(int i = 0; i < 4; i++) {
			Inventory[i] = player.GetInventory(i);
		}

		for(int i = 0; i < torchSave.Length; i++) {
			torchSave[i] = player.torchSave[i];
		}

		scene = player.scene;

		position = new float[3];
		position[0] = player.position.x;
		position[1] = player.position.y;
		position[2] = player.position.z;
	} 
}
