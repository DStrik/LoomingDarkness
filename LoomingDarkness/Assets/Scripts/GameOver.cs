using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public GameObject Character;

	[SerializeField]
	private GameObject GameOverUI;

	public void Quit() {
		Debug.Log("Application terminated");
		Application.Quit();
	}

	public void Restart() {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}
	public void EndGame() {
		Debug.Log("GameOver!");
		Character.SetActive(false);
		GameOverUI.SetActive(true);
	}
}
