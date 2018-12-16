using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour {

	public GameObject healthBar;
	private HealthSystem healthSystem;
	public bool inSafeRoom = false;
	private float secDelay;
	public float delayPeriod = 1;
	private float damage = 1;
	private bool death;
	public GameObject bloodSplash;
	public GameObject healEffect;

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
			FindObjectOfType<GameUI>().EndGame();
		}
	}

	private IEnumerator healRoutine(int healAmount) {
		for(int i = 1; i < healAmount; i = i + 2) {
			healthSystem.heal(2);

			yield return new WaitForSeconds(0.01f);
		}
	}

	private IEnumerator hurtRoutine(int hurtAmount) {
		Debug.Log("hurtroutine:" + hurtAmount);
		for(int i = 1; i < hurtAmount; i = i + 2) {
			healthSystem.damage(2);

			yield return new WaitForSeconds(0.01f);
		}
	}

	public void heal(int healAmount) {
		Instantiate(healEffect, gameObject.transform.position, Quaternion.Euler(-90, 0, 0));
		StartCoroutine(healRoutine(healAmount));
	}

	public void hurt(int hurtAmount) {
		FindObjectOfType<AudioManager>().Play("Hurt");
		Debug.Log("hurt:" + hurtAmount);
		Instantiate(bloodSplash, gameObject.transform.position, gameObject.transform.rotation);	
		StartCoroutine(hurtRoutine(hurtAmount));
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

	public float GetHealth() {
		return healthSystem.getHealth();
	}

	public void SetHealth(float health) {
		healthSystem.SetHealth(health);
		healthSystem.damage(1);
		healthSystem.heal(1);
	}

	public void hurtStart() {
		healthSystem.damage(50);
	}
	
}
