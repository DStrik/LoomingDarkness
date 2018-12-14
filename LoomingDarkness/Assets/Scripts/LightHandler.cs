using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHandler : MonoBehaviour {

	private LightSystem lightSystem;
	public GameObject lightBar;
	public GameObject charLight;
	public GameObject torch;
	public Animator animator;
	public float lightRatio = 0.2f;
	public float torchDepletion = 1;
	public bool usingTorch = false;
	public Inventory inventory;
	public ParticleSystem r;
	public ParticleSystem l;
	public ParticleSystem m;

	// Use this for initialization
	void Start () {
		lightSystem = new LightSystem(0);
		lightBar.GetComponent<LightBar>().updateBar(0f);
		stopParticles();
	}
	
	// Update is called once per frame
	void Update () {
		if(usingTorch) {	
			lightSystem.updateLight(torch.GetComponentInParent<Torch>().lightMeter);
			charLight.transform.localScale = new Vector3(lightSystem.getLight() * lightRatio,lightSystem.getLight() * lightRatio,1);	
			lightBar.GetComponent<LightBar>().updateBar(torch.GetComponentInParent<Torch>().durability);
			particles();
		}else {
			lightBar.GetComponent<LightBar>().updateBar(0f);
		}
		if(torch != null){
			if(torch.GetComponentInParent<Torch>().durability <= 1) {
				usingTorch = false;
				animator.SetBool("Torch", usingTorch);
				inventory.RemoveItem(torch);
				FindObjectOfType<AudioManager>().Stop("SmallBurn");
				FindObjectOfType<AudioManager>().Play("ExtinguishTorch");
				charLight.transform.localScale = new Vector3(0,0,1);
				stopParticles();
			}
		}
	}

	public void turnOnTorch(GameObject torch) {
		torch.GetComponentInParent<Torch>().inUse = true;
		this.torch = torch;
		usingTorch = true;
		inventory.showTorchActive(torch);
		animator.SetBool("Torch", usingTorch);
	}

	public void particles() {
		if(animator.GetInteger("Direction") == 2) {
			if(!l.IsAlive()){				
				r.Stop();
				m.Stop();
				l.Play();
			}
		}
		else if(animator.GetInteger("Direction") == 3) {
			if(!r.IsAlive()) {
				l.Stop();
				m.Stop();
				r.Play();
			}	
				
		}
		else {
			if(!m.IsAlive()) {
				l.Stop();
				r.Stop();
				m.Play();
			}
		}
	}
	
	public void stopParticles() {
		l.Stop();
		r.Stop();
		m.Stop();
	}

	public void turnOffTorch() {
		if(torch != null) {
			torch.GetComponentInParent<Torch>().inUse = false;
			inventory.showTorchInactive(torch);
		}
			
		this.torch = null;
		usingTorch = false;
		animator.SetBool("Torch", usingTorch);
		FindObjectOfType<AudioManager>().Stop("SmallBurn");
		FindObjectOfType<AudioManager>().Play("ExtinguishTorch");
		charLight.transform.localScale = new Vector3(0,0,1);
		stopParticles();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("SafeZone") && usingTorch){
			turnOffTorch();
		}
	}
}
