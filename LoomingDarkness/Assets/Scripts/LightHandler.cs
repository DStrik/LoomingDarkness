using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHandler : MonoBehaviour {

	private LightSystem lightSystem;
	public GameObject light;
	public GameObject torch;
	public Animator animator;
	public float lightRatio = (float) 0.2;
	public float torchDepletion = 1;
	public bool usingTorch = false;


	// Use this for initialization
	void Start () {
		lightSystem = new LightSystem(0);
	}
	
	// Update is called once per frame
	void Update () {
		if(usingTorch) {	
			lightSystem.updateLight(torch.GetComponentInParent<Torch>().durability);
			light.transform.localScale = new Vector3(lightSystem.getLight() * lightRatio,lightSystem.getLight() * lightRatio,1);	
		}
		if(torch != null){
			if(torch.GetComponentInParent<Torch>().durability <= 0) {
				usingTorch = false;
				animator.SetBool("Torch", usingTorch);
				Destroy(torch);
			}
		}
	}

	public void turnOnTorch(GameObject torch) {
		torch.GetComponentInParent<Torch>().inUse = true;
		this.torch = torch;
		usingTorch = true;
		animator.SetBool("Torch", usingTorch);
	}

	public void turnOffTorch() {
		if(torch != null) {
			torch.GetComponentInParent<Torch>().inUse = false;
		}
		this.torch = null;
		usingTorch = false;
		animator.SetBool("Torch", usingTorch);
		light.transform.localScale = new Vector3(0,0,1);
	}
}
