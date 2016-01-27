using UnityEngine;
using System.Collections;

public class RespawnScript : MonoBehaviour {
	
	public Transform spawnPoint;
	private bool respawn = false;
	public bool playerDead = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (playerDead) {
			respawn = true;
		} else {
			respawn = false;
		}
		
		if (respawn) {
			transform.position = spawnPoint.position;
			// Change player state back to being alive
			playerDead = false;
		}
	
	}
	
	// Method that changes the state of the player
	public void killPlayer () {
		playerDead = true;
	}
	
	// Method to access the state of the player
	public bool getPlayerStatus () {
		return playerDead;
	}
	
	//Change the checkpoint of the player appropriately
	public void makeCheckPoint (Transform point) {
		spawnPoint = point;
	}
}
