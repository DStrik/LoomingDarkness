using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicmovement : MonoBehaviour {
	public int speed = 1;
	public Animator animator;

	// Update is called once per frame
	void Update () {
		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
		if(Input.GetKeyDown(KeyCode.LeftShift)) {
			speed = 2;
		}
		if(Input.GetKeyUp(KeyCode.LeftShift)) {
			speed = 1;
		}
		animator.SetFloat("Horizontal", move.x);
		animator.SetFloat("Vertical", move.y);
		animator.SetFloat("Magnitude", move.magnitude);

		if(move.x < 0) {
			animator.SetInteger("Direction", 0);
		}
		else if(move.x > 0) {
			animator.SetInteger("Direction", 1);
		}
		else if(move.y < 0) {
			animator.SetInteger("Direction", 2);
		}
		else if(move.y > 0) {
			animator.SetInteger("Direction", 3);
		}

		transform.position = transform.position + move * Time.deltaTime * speed;
	}
}
