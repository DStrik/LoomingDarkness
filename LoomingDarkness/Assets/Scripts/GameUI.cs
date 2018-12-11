using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

	public GameObject GameOverUI, pauseMenu;
	private bool PauseMenuOn = false;

	public void Quit() {
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape) && !PauseMenuOn) {
			Time.timeScale = 0;
			pauseMenu.SetActive(true);
			PauseMenuOn = true;
		}
		else if(Input.GetKeyDown(KeyCode.Escape) && PauseMenuOn) {
			Time.timeScale = 1;
			pauseMenu.SetActive(false);
			PauseMenuOn = false;
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
