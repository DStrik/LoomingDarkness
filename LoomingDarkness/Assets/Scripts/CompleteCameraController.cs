using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteCameraController : MonoBehaviour {

	public GameObject Character;
	private Vector3 offset;
	// Use this for initialization
	void Start () {
		offset = transform.position - Character.transform.position;		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = Character.transform.position + offset;
	}
}
