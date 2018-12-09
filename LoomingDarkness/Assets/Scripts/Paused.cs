using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Paused : MonoBehaviour {
	public EventSystem eventSystem;
	public GameObject selectedObject;
	private bool buttonSelected = false;

	public void LoadByIndex (int index) {
		SceneManager.LoadScene(index);
	}

	public void Resume() {
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxisRaw("Vertical") != 0 && !buttonSelected) {
			eventSystem.SetSelectedGameObject(selectedObject);
			buttonSelected = true;
		}
	}

	private void Disable() {
		buttonSelected = false;
	}

	private void Quit() {
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}

	public void SetVolume(Slider slider) {
		AudioListener.volume = slider.value;
	}
}
