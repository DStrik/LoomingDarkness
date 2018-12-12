using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerPlayerLoad : MonoBehaviour {

	public void LoadGame() {
		GameObject load = GameObject.Find("CheckLoadGame");
		load.GetComponent<StartingSceneLoad>().SetLoadTrue();
		Time.timeScale = 1;
		SceneManager.LoadScene("Loading");
	}
}
