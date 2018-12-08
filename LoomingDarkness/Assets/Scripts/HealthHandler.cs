using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour {

	public GameObject healthBar;
	private HealthSystem healthSystem;
	public bool inSafeRoom = true;
	private float secDelay;
	public float delayPeriod = 1;
	private float damage = 1;
	private bool death;

	// Use this for initialization
	void Start () {
		healthSystem = new HealthSystem(100);
		healthBar.GetComponent<HealthBar>().Setup(healthSystem);
		death = false;
		secDelay = Time.time;
	}

	void Update() {
		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
		
		if(Input.GetKeyDown(KeyCode.LeftShift)){
			damage = (float) 2.5;
		}
		if(Input.GetKeyUp(KeyCode.LeftShift)) {
			damage = 1;
		}
		if(transform.position != transform.position + move && !inSafeRoom){
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

		if(healthSystem.getHealth() <= 0 && !death) {
			death = true;
			FindObjectOfType<GameOver>().EndGame();
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

	void OnTriggerExit2D(Collider2D other) {
		if(other.CompareTag("SafeZone")){
			inSafeRoom = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("SafeZone")){
			inSafeRoom = true;
		}
	}
	
}
