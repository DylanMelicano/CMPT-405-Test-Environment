using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter (Collider other) {
		Debug.Log ("Yosh");
		if (other.CompareTag("Player")){
			RespawnScript spawnScript = other.GetComponent<RespawnScript>();
			spawnScript.killPlayer();
		}
	}
}
