using UnityEngine;
using System.Collections;

public class PlayerChanges : MonoBehaviour {
	
	//Script to change aspects of the player. 
	//Mainly wil lbe the torchlight Intensity, walking speed, sound and camera effects (tentative).
	
	OSCReceiver mainReceiverScript;
	Light playerTorch;	
	
	//Check variables to see if changes will initiate or not.
	bool changeTorch = false;
	float totalValueChange = 0f;
	
	bool changeWalkSpeed = false;
	bool heartSoundPlaying = false;
	bool cameraEffectPlaying = false;
	
	float obtainAverageTime = 0f;
	bool testCalc = false;
	
	public float prevAvg = 0f;
	public float currAvg = 0f;
	
	public bool checkAverages = false;	//Use to control the checking of averages in every frame update.
	
	// Use this for initialization
	void Start () {
		mainReceiverScript = GameObject.FindWithTag("MeasureObject").GetComponent<OSCReceiver>();
		playerTorch = GetComponentInChildren<Light>();
	}
	
	// Update is called once per frame
	void Update () {		
		if (checkAverages == true) {
			if (currAvg > (prevAvg * 1.1f)) {
				reduceTorchlight();
			} else if (currAvg <= (prevAvg * 0.9f)){
				increaseTorchlight();
			}						
		}
		
		if (changeTorch == true) {
			checkAverages = false;
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
		}
	}
	
	public void toggleChangesChecks () {
		changeTorch = false;
		changeWalkSpeed = false;
		//heartSoundPlaying = false;
		//cameraEffectPlaying = false;
	}
	
	//Used to set up the averages for the comparison values.
	public void setAverages (float prev, float curr) {
		prevAvg = prev;
		currAvg = curr;
	}
}
