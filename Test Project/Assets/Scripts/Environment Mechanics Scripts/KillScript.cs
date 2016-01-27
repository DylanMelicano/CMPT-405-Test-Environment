using UnityEngine;
using System.Collections;

public class KillScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void onTriggerEnter (Collider other) {
		Debug.Log ("Yosh");
		if (other.gameObject.tag == "Player"){
		RespawnScript spawnScript;
			spawnScript = other.GetComponent<RespawnScript>();
			spawnScript.killPlayer();
		}
	}
}
