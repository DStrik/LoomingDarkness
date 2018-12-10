using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {

	public bool storeable; // if true this object can be stored in inventory
	public bool openable; // if true this object can be opened
	public bool locked; // if true this object is locked
	public GameObject unlocker; // item needed to unlock object
	public string type;
	public Sprite image;
	public Sprite altImage;

	public void setInactive() {
		gameObject.SetActive(false);
	}

/*	public void disableRendering() {
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
	}
	*/
}
