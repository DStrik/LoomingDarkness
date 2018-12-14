using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryDialogue : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Character")){
			GetComponent<InteractableDialogue>().TriggerDialogue();
		}
	}
	void OnTriggerExit2D(Collider2D other) {
		if(other.CompareTag("Character")) {
			FindObjectOfType<DialogueManager>().EndDialogue();
		}
	}
}
