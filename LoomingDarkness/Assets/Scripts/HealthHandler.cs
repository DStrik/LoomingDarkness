using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour {

	public HealthBar healthBar;
	private HealthSystem healthSystem;
	private float secDelay = 0;
	public float delayPeriod = 1;

	// Use this for initialization
	void Start () {
		healthSystem = new HealthSystem(100);

		healthBar.Setup(healthSystem);
	}

	void Update() {
		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

		if(transform.position != transform.position + move){
			if(Time.time > secDelay ) {
				secDelay += delayPeriod;
				healthSystem.damage(1);
			}
		}
		else
		{
			if(Time.time > secDelay) {
				secDelay+= delayPeriod;
			}
		}
	}
	
}
