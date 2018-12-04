using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

	private HealthSystem healthSystem;
	public void Setup(HealthSystem healthSystem) {
		this.healthSystem = healthSystem;

		healthSystem.OnHealthChange += healthSystem_OnHealthChange;
	}

	private void healthSystem_OnHealthChange(object sender, System.EventArgs e) {
		transform.Find("Bar").localScale = new Vector3(healthSystem.getHealthPercentage() * 10, 10);

	}

	private void Update() {
		//transform.Find("Bar").localScale = new Vector3(healthSystem.getHealthPercentage() * 10, 10);
	}
}
