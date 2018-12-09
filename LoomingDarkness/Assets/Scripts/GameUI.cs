using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

	public GameObject GameOverUI, pauseMenu;

	public void Quit() {
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			Time.timeScale = 0;
			pauseMenu.SetActive(true);
		}
	}

	public void Restart() {
		Time.timeScale = 1;
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}
	public void EndGame() {
		Debug.Log("GameOver!");
		Time.timeScale = 0;
		GameOverUI.SetActive(true);
	}
}
