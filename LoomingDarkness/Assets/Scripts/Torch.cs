using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour {

	public float durability = 100;
	public bool inUse = false;
	public float reduction = 1;
	public float secDelay = 0;
	public float delayPeriod = 1;

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
