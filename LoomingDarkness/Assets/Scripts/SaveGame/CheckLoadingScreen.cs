using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckLoadingScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(GameObject.Find("AudioManager"));
		GameObject load = GameObject.Find("CheckLoadGame");
		if(load.GetComponent<StartingSceneLoad>().load) {
			PlayerData data = SaveSystem.LoadPlayer();
			LoadByIndex(data.scene);
		}
		else {
			LoadByIndex(1);
		}
	}

	public void LoadByIndex (int index) {
		Time.timeScale = 1;
		SceneManager.LoadScene(index);
	}
}
