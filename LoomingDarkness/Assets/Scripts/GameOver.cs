using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public GameObject Character;

	[SerializeField]
	private GameObject GameOverUI;

	public void Quit() {
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}

	public void Restart() {
		Time.timeScale = 1;
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}
	public void EndGame() {
		Debug.Log("GameOver!");
		// Character.SetActive(false);
		Time.timeScale = 0;
		GameOverUI.SetActive(true);
	}
}
