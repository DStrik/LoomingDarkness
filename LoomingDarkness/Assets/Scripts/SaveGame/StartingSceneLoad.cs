using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingSceneLoad : MonoBehaviour {

	public bool load;
	public static StartingSceneLoad instance;

	// Use this for initialization
	void Awake () {

		if(instance == null) {
			instance = this;
		}
		else {
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);

		load = false;
	}

	public void SetLoadTrue() {
		load = true;
	}

	public void SetLoadFalse() {
		load = false;
	}
	
}
