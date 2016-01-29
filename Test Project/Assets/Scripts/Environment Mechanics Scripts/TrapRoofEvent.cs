using UnityEngine;
using System.Collections;

public class TrapRoofEvent : MonoBehaviour {
	
	public bool exitIdleZone = false;
	public AudioClip collapseSound;
	public int keyNumber; 
	
	GameObject trapRoof;
	Transform roofTransform;
	InventoryScript invScript;
	AudioSource roofSound;
	bool audioPlaying = false;
	
	//On ground pos = -6.14f
	//On top position = -1.353f

	// Use this for initialization
	void Start () {
		trapRoof = GameObject.FindWithTag("TrapRoof");
		invScript = GameObject.Find("KeyInventory").GetComponent<InventoryScript>();
		roofSound = GetComponent<AudioSource>();
		roofSound.clip = collapseSound;
	}
	
	// Update is called once per frame
	void Update () {
		if (exitIdleZone == true && invScript.hasKey(2)) {
			if (trapRoof.transform.position.y > -6.1f) {
				Vector3 temp = new Vector3(0,0.007f,0);
				trapRoof.transform.position -= temp;
				
				if (audioPlaying == false) {
					roofSound.Play();
					audioPlaying = true;
				}
			} else {
					GetComponent<AudioSource>().Stop();	
			}			
		}
	
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Player")){
			Debug.Log ("Player in idle zone. Prepare Trap");
			exitIdleZone = false;
        }
	}
	
	void OnTriggerExit (Collider other) {
		if (other.CompareTag("Player")){
			Debug.Log ("Player outside idle zone. Activate Trap");			
			exitIdleZone = true;
        }
	}
}
