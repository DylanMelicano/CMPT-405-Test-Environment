using UnityEngine;
using System.Collections;

public class EnvironmentChanges : MonoBehaviour {
	
	OSCReceiver hrvReceiverScript;
	
	public float prevAvgEnv = 0f;
	public float currAvgEnv = 0f;
	
	public bool checkAvgEnv = false;	//Use to control the checking of averages in every frame update.
	
	//Maze Torchlight changes
	GameObject[] torches;
	Color baseTorch;
	Color dreadTorch;
	bool torchChanged = false;
	AudioSource flameBGM;
	bool flameSoundPlaying = false;
	int currFlameColor = 0; // 0 is default. 1 is the dreaded/scarier color.
	//public float duration = 1.5f;
	
	float envTime = 0f;
	
	// Use this for initialization
	void Start () {
		hrvReceiverScript = GameObject.FindWithTag("MeasureObject").GetComponent<OSCReceiver>();
		
		//Array of torches and the values of the colors
		torches = GameObject.FindGameObjectsWithTag("Torch");
		baseTorch = new Vector4(0.97f, 0.73f, 0.21f, 1);
		dreadTorch = new Vector4(0.49f, 0.02f, 0.11f, 1);
		flameBGM = GameObject.Find("MazeChanges/FlameChangeBGM").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (checkAvgEnv == true) {
			if (currAvgEnv > (prevAvgEnv * 1.1f)) {
				if (currFlameColor == 0) {
					changeTorches (dreadTorch);
					if (flameSoundPlaying == false) {
						flameBGM.Play();
						flameSoundPlaying = true;
					}
					currFlameColor = 1;
				}
				toggleEnvFlags();
			} else if (currAvgEnv <= (prevAvgEnv * 0.9f)){
				if (currFlameColor == 1) {
					changeTorches (baseTorch);
					currFlameColor = 0;
				}
				toggleEnvFlags();
			} else {
				toggleEnvFlags();
			}
		}
			
			/**envTime += Time.deltaTime;
			if (envTime >= 5f) {
				changeTorches(dreadTorch);
				envTime = 0f;
			}**/
	}
	
	public void toggleEnvCheck () {
		checkAvgEnv = true;
	}
	
	public void toggleEnvFlags () {
		checkAvgEnv = false;
		torchChanged = false;
		flameSoundPlaying = false;
	}
	
	public void changeTorches (Color secondColor) {
		if (torchChanged == false) {
			foreach (GameObject torch in torches) {
				Color baseColor = torch.GetComponentInChildren<Light>().color;
				torch.GetComponentInChildren<ParticleSystem>().startColor = secondColor;
				torch.GetComponentInChildren<Light>().color = secondColor; //Color.Lerp (baseColor, secondColor, Time.deltaTime * duration);
			}
			//flameBGM.Play();
			torchChanged = true;
		}
	}
	
	//Used to set up the averages for the comparison values.
	public void setAveragesEnv (float prev, float curr) {
		prevAvgEnv = prev;
		currAvgEnv = curr;
	}
}
