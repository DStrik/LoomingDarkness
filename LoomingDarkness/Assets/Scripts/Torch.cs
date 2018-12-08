using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour {

	public float durability = 100;
	public bool inUse = false;
	public float reduction = 1;
	public float secDelay;
	public float delayPeriod = 1;

	void Start() {
		secDelay = Time.time;
	}
	// Update is called once per frame
	void Update () {
		if(inUse) {
			if(durability > 0) {
				if(Time.time > secDelay) {
					secDelay += delayPeriod;
					durability -= reduction;
				}
			}
		}else {
			if(Time.time > secDelay) {
				secDelay += delayPeriod;
			}
		}
	}
}
