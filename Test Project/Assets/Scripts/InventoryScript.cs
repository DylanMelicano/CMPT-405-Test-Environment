using UnityEngine;
using System.Collections;

public class InventoryScript : MonoBehaviour {
	
	public bool[] keys;

	// Use this for initialization
	void Start () {
		keys = new bool [5] {false, false, false, false, false};
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Check is specific key is in player possession
	public bool hasKey (int keyNum) {
		if (keyNum >= 5 || keyNum < 0) {
			Debug.LogError("Key number is invalid. Only 0 to 4 to access the five keys.");
			return false;
		}
			return keys[keyNum];
	}
	
	// Obtain the key
	public void obtainKey (int keyNum) {
		if (keyNum >= 5 || keyNum < 0) {
			Debug.LogError("Key number is invalid. Only 0 to 4 to access the five keys.");
		}
		
		keys[keyNum] = true;
	}
	
	// If door has been open, make key[position] false/unavailable
	public void useKey (int keyNum) {
	if (keyNum >= 5 || keyNum < 0) {
		Debug.LogError("Key number is invalid. Only 0 to 4 to access the five keys.");
	}
	
		keys[keyNum] = false;
	}
}
