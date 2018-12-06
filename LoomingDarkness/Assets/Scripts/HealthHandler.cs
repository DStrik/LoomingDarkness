using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour {

	public GameObject healthBar;
	private HealthSystem healthSystem;
	public SafeRoomTrigger safeRoom;
	private float secDelay = 0;
	public float delayPeriod = 1;
	private float damage = 1;

	// Use this for initialization
	void Start () {
		healthSystem = new HealthSystem(100);
		healthBar.GetComponent<HealthBar>().Setup(healthSystem);
	}

	void Update() {
		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
		
		if(Input.GetKeyDown(KeyCode.LeftShift)){
			damage = (float) 2.5;
		}
		if(Input.GetKeyUp(KeyCode.LeftShift)) {
			damage = 1;
		}
		if(transform.position != transform.position + move && !safeRoom.inSafeRoom){
			if(Time.time > secDelay ) {
				secDelay += delayPeriod;
				healthSystem.damage(damage);
			}			
		}
		else
		{
			if(Time.time > secDelay) {
				secDelay+= delayPeriod;
			}
		}
	}

	private IEnumerator healRoutine(int healAmount) {
		for(int i = 1; i < healAmount; i++) {
			healthSystem.heal(2);

			yield return new WaitForSeconds(0.01f);
		}
	}

	public void heal(int healAmount) {
		StartCoroutine(healRoutine(healAmount));
	}
	
}
