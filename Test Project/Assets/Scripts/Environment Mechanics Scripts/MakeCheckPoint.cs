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
		if (other.CompareTag("Player")){
			Debug.Log ("Entered Checkpoint");
			RespawnScript spawnScript;
			spawnScript = other.GetComponent<RespawnScript>();
			spawnScript.makeCheckPoint(this.transform);
			spawnScript.passedCheckPoint(this.name);
		}
	}
}
