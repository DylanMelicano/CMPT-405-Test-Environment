using UnityEngine;
using System.Collections;

public class FinalScareScript : MonoBehaviour {
	
	InventoryScript inventory;
	//Script of final enemy/scare
	public GameObject finalEnemy;
	
	public AudioClip leaving;
	bool audio1 = false;
	public AudioClip dontLeave;
	bool audio2 = false;
	public AudioClip stay;
	bool audio3 = false;
	public float girlVolume = 0.3f;
	float audioTime = 0f;
	
	bool hasFinalKey = false;
	bool usedFinalkey = false;

	// Use this for initialization
	void Start () {
		inventory = GameObject.Find("KeyInventory").GetComponent<InventoryScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if (hasFinalKey == true) {
			audioTime = audioTime + Time.deltaTime;
			//summon final enemy and play voices
			if (usedFinalkey == false) {
				finalEnemy.SetActive(true);
				finalEnemy.GetComponent<ActivateStatic>().enabled = true;
				if (audio1 == false) {
					GetComponent<AudioSource>().PlayOneShot(leaving, girlVolume);
					audio1 = true;
				}
				
				if (audio1 == true && audio2 == false && GetComponent<AudioSource>().isPlaying == false && audioTime > 2.0f) {
					GetComponent<AudioSource>().PlayOneShot(dontLeave, girlVolume);
					audio2 = true;
				}
			}
			//Make final enemy move.
			if (inventory.hasKey(2) == false) {
				usedFinalkey = true;
				if (audio3 == false && GetComponent<AudioSource>().isPlaying == false) {
					GetComponent<AudioSource>().PlayOneShot(stay, girlVolume);
					audio3 = true;
				}
				//Enable the follow the player script for the final enemy
				if (audio3 == true && GetComponent<AudioSource>().isPlaying == false) {
					finalEnemy.GetComponent<FollowPlayer>().enabled = true;
					finalEnemy.GetComponent<AudioSource>().enabled = true;
				}
			}
		}	
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Player")){
			if (inventory.hasKey(2)) {
				hasFinalKey = true;
			}
		}
	}
}
