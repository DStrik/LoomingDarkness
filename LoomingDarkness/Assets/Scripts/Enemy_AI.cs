using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour {

	private Vector3 player;
	private Vector2 playerDirection;
	private float xDif;
	private float yDif;
	public float range;
	public float speed;
	public LayerMask myLayerMask;
	private RaycastHit2D hit;
	public Animator animator;
	private Vector3 move;
	private bool stop = false;


		
	// Update is called once per frame
	void Update () {
		player = GameObject.Find("Character").transform.position;
		xDif = player.x - transform.position.x;
		yDif = player.y - transform.position.y;
		playerDirection = new Vector2 (xDif,yDif);
		attack();
	}

	void attack() {
		hit = Physics2D.Raycast(transform.position, playerDirection, range, myLayerMask);
		
		if(hit) {
			if(hit.transform.tag == "Character"){
				if(!stop) {
					move = new Vector3(xDif, yDif, 0.0f);
					transform.position = transform.position + move.normalized * Time.deltaTime * speed;			
				}
			}else {
				move = new Vector3(0f,0f,0f);
			}
			if(hit.transform.tag == "SafeZone") {
				Destroy(gameObject);
			}


		}else{
			move = new Vector3(0f,0f,0f);
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
	}

	void OnTriggerEnter2D(Collider2D other) {
		//Debug.Log("almost");
		if(other.CompareTag("Character")) {
			stop = true;
			move = new Vector3(0f,0f,0f);
			Invoke("resetStop", 3);
			//Debug.Log("stop");
		}
	}

	public void resetStop() {
		stop = false;
	}
	
}
