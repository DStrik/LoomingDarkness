using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLevel : MonoBehaviour {

	public GameObject winlevel;

	void OnTriggerEnter2D(Collider2D other) {
		StartCoroutine(WinningRoutine());
    }

	private IEnumerator WinningRoutine() {
		bool start = true;

		while(start) {
			winlevel.SetActive(true);
			start = false;
			GameObject.Find("Character").GetComponent<basicmovement>().enabled = false;
			FindObjectOfType<AudioManager>().Stop("Walk");
			FindObjectOfType<AudioManager>().Stop("Run");
			yield return new WaitForSeconds(3f);
		}

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
