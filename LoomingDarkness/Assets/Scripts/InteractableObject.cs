using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {

	public bool storeable; // if true this object can be stored in inventory

	public void setInactive() {
		gameObject.SetActive(false);
	}
}
