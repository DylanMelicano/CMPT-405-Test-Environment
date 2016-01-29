using UnityEngine;
using System.Collections;

public class KillScript : MonoBehaviour {

    RespawnScript spawnScript;
    bool hasDied = false;

	// Use this for initialization
	void Start () {
        spawnScript = GameObject.FindWithTag("Player").GetComponent<RespawnScript>();
    }
	
	// Update is called once per frame
	void Update () {
		if (hasDied) {
            spawnScript.killPlayer();
            hasDied = false;
        }
	}
	
	void OnTriggerEnter (Collider other) {
		Debug.Log ("Yosh");
		if (other.gameObject.tag == "Player"){
            hasDied = true;
		}
	}
}
