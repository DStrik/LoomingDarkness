using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switcher : MonoBehaviour {

	public bool up = true;

	public GameObject spikes;

	public Animator animator;
	private void Start() {
		animator.SetBool("Up", up);
	}

	public void UseSwitch() {
		if(up) {
			up = false;
		}
		else {
			up = true;
		}
		animator.SetBool("Up", up);
		spikes.GetComponent<spikes>().OnSwitch();
	}
}
