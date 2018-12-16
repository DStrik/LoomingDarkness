using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTriggerMain : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		StartCoroutine(MainMenu());
	}

	private IEnumerator MainMenu() {
		bool scenePlaying = true;

		while(scenePlaying) {
			scenePlaying = false;

			yield return new WaitForSeconds(50f);
		}

		SceneManager.LoadScene(0);
	}
	
}
