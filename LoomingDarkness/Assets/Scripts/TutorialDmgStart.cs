using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDmgStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<HealthHandler>().Invoke("hurtStart", 0.5f);
	}
	
}
