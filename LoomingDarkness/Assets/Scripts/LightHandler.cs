using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHandler : MonoBehaviour {

	private LightSystem lightSystem;
	public GameObject light;
	public GameObject torch;
	private float secDelay = 0;	
	public float delayPeriod = 1;
	public float lightRatio = (float) 0.2;
	public float torchDepletion = 1;
	public bool usingTorch = false;


	// Use this for initialization
	void Start () {
		lightSystem = new LightSystem(20);
	}
	
	// Update is called once per frame
	void Update () {
		if(lightSystem.getLight() > 0) {
			if(Time.time > secDelay) {
				secDelay += delayPeriod;
				light.transform.localScale = new Vector3(lightSystem.getLight() * lightRatio,lightSystem.getLight() * lightRatio,1);
				// reduce torch health
			}
		}else {
			if(Time.time > secDelay){
				secDelay += delayPeriod;
			}

		}
	}

	void updateLight(GameObject torch) {
		this.torch = torch;
		usingTorch = true;
	}
}
