using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningState : MonoBehaviour {

	public GameObject win;

	void OnTriggerEnter2D(Collider2D other) {
		StartCoroutine(WinningRoutine());
    }

	private IEnumerator WinningRoutine() {
		bool start = true;

		while(start) {
			win.SetActive(true);
			start = false;
			GameObject.Find("Character").GetComponent<basicmovement>().enabled = false;
			FindObjectOfType<AudioManager>().Stop("Walk");
			FindObjectOfType<AudioManager>().Stop("Run");
			yield return new WaitForSeconds(3f);
		}

		SceneManager.LoadScene("EndingSequence");
	}
}
