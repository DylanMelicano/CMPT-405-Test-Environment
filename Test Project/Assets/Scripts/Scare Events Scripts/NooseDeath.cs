using UnityEngine;
using System.Collections;

public class NooseDeath : MonoBehaviour {
	
	bool startHangEvent = false;
	float waitTime = 0f;
	bool soundPlaying = false;
	bool eventStarted = false;
	bool playerDead = false;
	
	public AudioClip chokeSound;
	
	GameObject player;
	GameObject nooseChair;
	Renderer chair;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		nooseChair = GameObject.FindWithTag("NooseChair");
		chair = nooseChair.GetComponentInChildren<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (startHangEvent) {
			waitTime = waitTime + Time.deltaTime;
			
			if (waitTime > 5.0f) {
				if (eventStarted == false) {
					nooseChair.GetComponentInChildren<Renderer>().transform.rotation = Quaternion.Euler(0,0,90f);
					player.GetComponent<ExampleMovement>().enabled = false;
					eventStarted = true;
				}				
			}
			
			if (waitTime > 10f && eventStarted) {
				if (soundPlaying == false) {
					this.GetComponent<AudioSource>().PlayOneShot(chokeSound);
					soundPlaying = true;
				} else {
					player.GetComponent<CameraMovement>().enabled = false;
					if (this.GetComponent<AudioSource>().isPlaying == false) {
						playerDead = true;
					} 
				}
			}
			
			if (playerDead) {
				RespawnScript spawnScript = player.GetComponent<RespawnScript>();
				spawnScript.killPlayer();
				startHangEvent = false;
				playerDead = false;
			}
		}
	
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Player")){
			Debug.Log("Hanging");
			startHangEvent = true;
		}
	}
	
	void OnTriggerExit (Collider other) {
		if (other.CompareTag("Player")){
			waitTime = 0f;
			//Debug.Log(waitTime);
			startHangEvent = false;
		}
	}
}
