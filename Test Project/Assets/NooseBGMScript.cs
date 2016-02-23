using UnityEngine;
using System.Collections;

public class NooseBGMScript : MonoBehaviour {
	
	bool playMusic = false;
	bool isMusicPlaying = false;
	AudioSource whispers;
	
	// Use this for initialization
	void Start () {
		whispers = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (playMusic == true) {
			if (isMusicPlaying == false) {
				whispers.Play();
				isMusicPlaying = true;
			}
		} else {
			whispers.Pause();
			isMusicPlaying = false;
		}
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Player")){
			playMusic = true;
		}
	}
	
		void OnTriggerExit (Collider other) {
		if (other.CompareTag("Player")){
			playMusic = false;
		}
	}
}
