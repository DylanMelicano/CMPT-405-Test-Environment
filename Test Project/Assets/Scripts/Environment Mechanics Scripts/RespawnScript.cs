using UnityEngine;
using System.Collections;

public class RespawnScript : MonoBehaviour {
	
	public Transform spawnPoint;
	private bool respawn = false;
	public bool playerDead = false;
	public AudioClip deathSound;
	public Color blackOut = new Color(0f, 0f, 0f, 1.0f);
	public Color deathVision;
	
	public bool passedCheckPoint1 = false;
	public bool passedCheckPoint2 = false;
	public bool passedCheckPoint3 = false;
	
	ExampleMovement move;
	CameraMovement playerCamera;
    staticAnimator staticScript;
	
	bool controlDown = false;
	bool deathAudio = false;
	bool deathAnimationDone = false;
    bool triggerVisionRestore = false;

    // Use this for initialization
    void Start () {
		move = this.GetComponent<ExampleMovement>();
		playerCamera = this.GetComponent<CameraMovement>();
        staticScript = GetComponentInChildren<staticAnimator>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (playerDead) {
			//Do animation properties
			//Disable control first
			if (controlDown == false) {
				move.enabled = false;
				playerCamera.enabled = false;
				controlDown = true;
			}
			
			//Play Audio 
			if (deathAudio == false) {
				GetComponent<AudioSource>().PlayOneShot(deathSound, 2f);
				deathAudio = true;
			}
			
			//Blackout screen afterwards
			if (deathAudio == true && /*GetComponent<AudioSource>().isPlaying == false &&*/ controlDown == true) {
				if (RenderSettings.fogDensity < 14f)
                {
					//RenderSettings.fogColor = blackOut;
					RenderSettings.fogColor = deathVision;
					RenderSettings.fogDensity += 0.15f;
				}
                else
                {
					deathAnimationDone = true;
				}
			}
		}
		
		if (deathAnimationDone) {
			respawn = true;
		} else {
			respawn = false;
		}

        if(triggerVisionRestore == true)
        {
            RenderSettings.fogDensity -= 0.007f;
            if(RenderSettings.fogDensity <= 0)
            {
                triggerVisionRestore = false;
                move.enabled = true;
            }
        }
        

        if (respawn) {
			transform.position = spawnPoint.position;
            //RenderSettings.fogDensity += 0.05f;
            triggerVisionRestore = true;
            RenderSettings.fogDensity = 1f;
            
			
				if (playerCamera.enabled == false) {
					playerCamera.enabled = true;
				}

				staticScript.staticOn = false;
				// Change player state back to being alive
				playerDead = false;
				//Change animation states back to false
				controlDown = false;
				deathAudio = false;
				deathAnimationDone = false;
                

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
	
	public void passedCheckPoint (string checkPoint) {
		if (checkPoint == "CheckPoint1") {
			passedCheckPoint1 = true;
		} else if (checkPoint == "CheckPoint2") {
			passedCheckPoint2 = true;
		} else if (checkPoint == "CheckPoint3") {
			passedCheckPoint3 = true;
		}
		/**if (spawnPoint.name == checkPoint ) {
			return true;
		}
		return false;**/
	}
}
