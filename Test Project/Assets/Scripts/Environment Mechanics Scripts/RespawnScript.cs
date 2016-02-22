using UnityEngine;
using System.Collections;

public class RespawnScript : MonoBehaviour {
	
	public Transform spawnPoint;
	private bool respawn = false;
	public bool playerDead = false;
	
	ExampleMovement move;
	CameraMovement playerCamera;
    staticAnimator staticScript;

    // Use this for initialization
    void Start () {
		move = this.GetComponent<ExampleMovement>();
		playerCamera = this.GetComponent<CameraMovement>();
        staticScript = GetComponentInChildren<staticAnimator>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (playerDead) {
			respawn = true;
		} else {
			respawn = false;
		}
		
		if (respawn) {

			if (move.enabled == false) {
				move.enabled = true;
			}
			
			if (playerCamera.enabled == false) {
				playerCamera.enabled = true;
			}

            staticScript.staticOn = false;
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
