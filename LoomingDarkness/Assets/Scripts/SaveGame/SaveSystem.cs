using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem {

	public static void SavePlayer(PlayerSave player) {
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/player.txt";
		// string path = "C:/Users/danni/TestSave/player.txt";
		FileStream stream = new FileStream(path, FileMode.Create);

		PlayerData data = new PlayerData(player);

		formatter.Serialize(stream, data);
		stream.Close();
	}

	public static PlayerData LoadPlayer() {
		Debug.Log("Inside SaveSystem.LoadPlayer()");
		string path = Application.persistentDataPath + "/player.txt";
		Debug.Log("Got the path");
		if(File.Exists(path)) {
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			PlayerData data = formatter.Deserialize(stream) as PlayerData;
			stream.Close();

			return data;
		}
		else {
			Debug.LogError("SaveFile not found in: " + path);
			return null;
		}
	}
}
