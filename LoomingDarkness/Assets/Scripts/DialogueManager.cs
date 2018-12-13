using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text text;
	public Queue<string> sentences;
	public Animator animator;
	private bool inProgress = false;
	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
	}

	void Update() {
		if(inProgress) {
			if(Input.GetKeyDown("space")) {
				DisplayNextSentence();
			}
		}
	}
	
	public void startDialogue(Dialogue dialogue) {
		animator.SetBool("isOpen", true);
		inProgress = true;

		sentences.Clear();

		foreach(string sentence in dialogue.sentences) {
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();

	}

	public void DisplayNextSentence() {
		
		if(sentences.Count == 0) {
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence) {
		text.text = "";
		foreach(char letter in sentence.ToCharArray()) {
			text.text += letter;
			yield return null;
		}
	}

	public void EndDialogue() {
		animator.SetBool("isOpen", false);
		inProgress = false;
	}
}
