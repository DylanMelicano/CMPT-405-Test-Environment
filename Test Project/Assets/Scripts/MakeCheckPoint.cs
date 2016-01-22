using UnityEngine;
using System.Collections;

public class MakeCheckPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter (Collider other) {
		Debug.Log ("Entered Checkpoint");
		if (other.CompareTag("Player")){
			RespawnScript spawnScript;
			spawnScript = other.GetComponent<RespawnScript>();
			spawnScript.makeCheckPoint(this.transform);
		}
	}
}
