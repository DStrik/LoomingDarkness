using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SetInactive : MonoBehaviour {

	private List<string> InactiveItems = new List<string>();

	public void SetInactiveItem(string obj) {
		InactiveItems.Add(obj);
	}

	public List<string> GetInactiveItems() {
		return InactiveItems;
	}

	public void Print() {
		foreach(string s in InactiveItems) {
			Debug.Log("Inactive item: " + s);
		}
	}
}
