using UnityEngine;
using System.Collections;

public class FinalScareScript : MonoBehaviour {
	
	InventoryScript inventory;
	//Script of final enemy/scare
	public GameObject finalEnemy;
	
	bool hasFinalKey = false;
	bool usedFinalkey = false;

	// Use this for initialization
	void Start () {
		inventory = GameObject.Find("KeyInventory").GetComponent<InventoryScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if (hasFinalKey == true) {
			finalEnemy.SetActive(true);
			//Summon the final enemy. In waiting
            //Play audio files of "Please don't leave. Don't leave me here. Are you leaving?"
			if (inventory.hasKey(2) == false) {
				//Enable the follow the player script for the final enemy
				finalEnemy.GetComponent<FollowPlayer>().enabled = true;
                finalEnemy.GetComponent<AudioSource>().enabled = true;
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
