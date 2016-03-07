using UnityEngine;
using System.Collections;

public class PlayerChanges : MonoBehaviour {
	
	//Script to change aspects of the player. 
	//Torchlight Intensity changes and Heartbeat sound will or will not play
	
	OSCReceiver mainReceiverScript;
	Light playerTorch;	
	
	//Check variables to see if changes will initiate or not.
	public bool changeTorch = false;
	float totalValueChange = 0f;
	
	public AudioClip heartBeat;
	public bool heartSoundPlaying = false;
	
	//float heartBeatTime = 0f;
	//bool testCalc = false;
	
	public float prevAvg = 0f;
	public float currAvg = 0f;
	
	public bool checkAverages = false;	//Use to control the checking of averages in every frame update.
	
	// Use this for initialization
	void Start () {
		mainReceiverScript = GameObject.FindWithTag("MeasureObject").GetComponent<OSCReceiver>();
		GameObject player = GameObject.FindWithTag("Player");
		playerTorch = player.GetComponentInChildren<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		/**heartBeatTime += Time.deltaTime;
		if (heartBeatTime >= 5f) {
				sample.CrossFadeTo(sampleText, temp, tempTiling);
				//Set new materials to change to based on player's location in the maze
				heartBeatTime = 0f;
		}	**/	
		
		if (checkAverages == true) {
			if (currAvg > (prevAvg * 1.002f)) {
				reduceTorchlight();
				if (heartSoundPlaying == false) {
					GetComponent<AudioSource>().Play();
					heartSoundPlaying = true;
				}
			} else if (currAvg <= (prevAvg * 1.002f)){
				increaseTorchlight();
				heartSoundPlaying = false;
			} else { //When the value is near the previous average (initially anyways)
				toggleChangesChecks();
			}						
		}
		
		if (changeTorch == true) {
			toggleChangesChecks();
			totalValueChange = 0f;
		}
	}
	
	//Reset the checking of averages to do the changes again.
	public void toggleCheck() {
		checkAverages = true;
	}
	
	//Changes to the torchlight intensity
	public void reduceTorchlight() {
		if (playerTorch.intensity > 4.0f) {
			if (totalValueChange < 1.0f) {
				playerTorch.intensity -= 0.01f;
				totalValueChange += 0.01f;
			} else {
				changeTorch = true;
			}
		} else {
			changeTorch = true;
		}
	}
	
	public void increaseTorchlight() {
		if (playerTorch.intensity < 8.0f) {
			if (totalValueChange < 1.0f) {
				playerTorch.intensity += 0.01f;
				totalValueChange += 0.01f;
			} else {
				changeTorch = true;
			}
		} else {
			changeTorch = true;
		}
	}	
	
	public void toggleChangesChecks () {
		checkAverages = false;
		changeTorch = false;
		heartSoundPlaying = false;
	}
	
	//Used to set up the averages for the comparison values.
	public void setAverages (float prev, float curr) {
		prevAvg = prev;
		currAvg = curr;
	}
}
