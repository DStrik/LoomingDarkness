using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHandler : MonoBehaviour {

	private LightSystem lightSystem;
	public GameObject light;
	private float secDelay = 0;	
	public float delayPeriod = 1;
	public float lightDepletion = (float) 0.2;


	// Use this for initialization
	void Start () {
		lightSystem = new LightSystem(20);
	}
	
	// Update is called once per frame
	void Update () {
		if(lightSystem.getLight() > 0) {
			if(Time.time > secDelay) {
				secDelay += delayPeriod;
				lightSystem.deplete(lightDepletion);
				light.transform.localScale = new Vector3(lightSystem.getLight(),lightSystem.getLight(),1);
				// reduce torch health
			}
		}
	}
}
