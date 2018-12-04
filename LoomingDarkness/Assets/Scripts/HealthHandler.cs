using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour {

	public HealthBar healthBar;
	private HealthSystem healthSystem;
	public float secDelay = 0;
	public float delayPeriod = 1;

	// Use this for initialization
	void Start () {
		healthSystem = new HealthSystem(100);

		healthBar.Setup(healthSystem);
	}

	void Update() {
		if(Time.time > secDelay) {
			secDelay += delayPeriod;
			healthSystem.damage(1);
		}
	}
	
}
