using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text text;
	public Queue<string> sentences;
	public Animator animator;
	private string sentence;
	private bool inProgress = false;
	private bool sentenceNotCompleted;
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
		
		if(sentences.Count == 0 && !sentenceNotCompleted) {
			EndDialogue();
			return;
		}

		StopAllCoroutines();
		if(sentenceNotCompleted) {
			text.text = this.sentence;
			sentenceNotCompleted = false;
		}else {
			this.sentence = sentences.Dequeue();
			StartCoroutine(TypeSentence(this.sentence));
		}
	}

	IEnumerator TypeSentence(string sentence) {
		sentenceNotCompleted = true;
		text.text = "";
		foreach(char letter in sentence.ToCharArray()) {
			text.text += letter;
			yield return null;
		}
		sentenceNotCompleted = false;
	}

	public void EndDialogue() {
		animator.SetBool("isOpen", false);
		inProgress = false;
	}
}
