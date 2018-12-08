using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleScreen : MonoBehaviour {

	public EventSystem eventSystem;
	public GameObject selectedObject;
	private bool buttonSelected = false;

	public void LoadByIndex (int index) {
		SceneManager.LoadScene(index);
	}

	void Update() {
		if(Input.GetAxisRaw("Vertical") != 0 && !buttonSelected) {
			eventSystem.SetSelectedGameObject(selectedObject);
			buttonSelected = true;
		}
	}

	private void Disable() {
		buttonSelected = false;
	}

	public void Quit() {
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
}
