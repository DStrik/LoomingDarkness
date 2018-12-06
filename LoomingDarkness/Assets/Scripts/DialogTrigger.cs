using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour {

	public GameObject controls;
	private bool show;
	void Start() {
		show = false;
		controls.SetActive(show);
	}

	void Update() {
		controls.SetActive(show);
	}
	void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Hello world");
		show = true;
    }

    void OnTriggerExit2D(Collider2D other) {
        Debug.Log("Bye World");
		show = false;
    }
}
