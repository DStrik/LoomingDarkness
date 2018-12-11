using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHandler : MonoBehaviour {

	private LightSystem lightSystem;
	public GameObject charLight;
	public GameObject torch;
	public Animator animator;
	public float lightRatio = 0.1f;
	public float torchDepletion = 1;
	public bool usingTorch = false;
	public Inventory inventory;


	// Use this for initialization
	void Start () {
		lightSystem = new LightSystem(0);
	}
	
	// Update is called once per frame
	void Update () {
		if(usingTorch) {	
			lightSystem.updateLight(torch.GetComponentInParent<Torch>().durability);
			charLight.transform.localScale = new Vector3(lightSystem.getLight() * lightRatio,lightSystem.getLight() * lightRatio,1);	
		}
		if(torch != null){
			if(torch.GetComponentInParent<Torch>().durability <= 10) {
				usingTorch = false;
				animator.SetBool("Torch", usingTorch);
				inventory.RemoveItem(torch);
				FindObjectOfType<AudioManager>().Stop("SmallBurn");
				FindObjectOfType<AudioManager>().Play("ExtinguishTorch");
				charLight.transform.localScale = new Vector3(0,0,1);
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
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("SafeZone")){
			turnOffTorch();
		}
	}
}
