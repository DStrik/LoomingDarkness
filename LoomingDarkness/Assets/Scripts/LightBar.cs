using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBar : MonoBehaviour {

	private LightSystem lightSystem;
	public void Setup (LightSystem lightSystem) {
		this.lightSystem = lightSystem;
	}

	public void updateBar(float durability) {
		transform.Find("Bar").localScale = new Vector3(durability * 0.1f, 10);
	}
}
