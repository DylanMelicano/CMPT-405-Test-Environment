using UnityEngine;
using System.Collections;

public class KillScript : MonoBehaviour {

    RespawnScript spawnScript;
    bool hasDied = false;
    public GameObject disableEnemy;

	// Use this for initialization
	void Start () {
        spawnScript = GameObject.FindWithTag("Player").GetComponent<RespawnScript>();
    }
	
	// Update is called once per frame
	void Update () {
		if (hasDied) {
            if (disableEnemy != null)
            {
                disableEnemy.SetActive(false);
            }
            spawnScript.killPlayer();
            hasDied = false;
            
        }
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player"){
            hasDied = true;
		}
	}
	
	void OnCollisionEnter (Collision other) {
		if (other.collider.tag == "Player"){
			hasDied = true;
		}
	}
}
