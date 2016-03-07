using UnityEngine;
using System.Collections;

public class EnvironmentChanges : MonoBehaviour {
	
	OSCReceiver hrvReceiverScript;
	
	public float prevAvgEnv = 0f;
	public float currAvgEnv = 0f;
	
	public bool checkAvgEnv = false;	//Use to control the checking of averages in every frame update.
	
	//Respawn Script to check the location of the player
	RespawnScript respawnLocation;
	
	//Pillar Room Event that makes delusions of enemies
	PillarRoomEvent pillarRoom;
	
	//Maze Torchlight changes
	GameObject[] torches;
	Color baseTorch;
	Color dreadTorch;
	bool torchChanged = false;
	AudioSource flameBGM;
	bool flameSoundPlaying = false;
	int currFlameColor = 0; // 0 is default. 1 is the dreaded/scarier color.
	int currRand;
	int prevRand = 0;
	//public float duration = 1.5f;
	
	//Wall texture and material changes
	CrossFade crossFadeScript;
	Texture mainWallChange;
	public Texture firstWallChange;
	public Texture secondWallChange;
	bool usedSecondChange = false;
	public Texture thirdWallChange;
	bool usedThirdChange = false;
	
	//first material contains default brick2 texture
	public Material secondMaterial;	//base texture is firstWallChange
	public Material thirdMaterial; //base texture is secondWallChange
	public Material fourthMaterial; //base texture is thirdWallChange
	
	Vector2 wallOffset;
	Vector2 wallTiling;

	float envTime = 0f;
	
	// Use this for initialization
	void Start () {
		hrvReceiverScript = GameObject.FindWithTag("MeasureObject").GetComponent<OSCReceiver>();
		respawnLocation = GameObject.FindWithTag("Player").GetComponent<RespawnScript>();
		pillarRoom = GameObject.FindWithTag("PillarEvent").GetComponent<PillarRoomEvent>();
		
		//Torch changing related variables
		torches = GameObject.FindGameObjectsWithTag("Torch");
		baseTorch = new Vector4(0.97f, 0.73f, 0.21f, 1);
		dreadTorch = new Vector4(0.49f, 0.02f, 0.11f, 1);
		flameBGM = GameObject.Find("MazeChanges/FlameChangeBGM").GetComponent<AudioSource>();
	
		//Wall changing related variables
		crossFadeScript = GetComponent<CrossFade>();
		wallOffset = new Vector2 (0f,0f);
		wallTiling = new Vector2 (3f,1.5f);
		mainWallChange = firstWallChange;
		crossFadeScript.setNewMaterial(secondMaterial); //The first material it will change to
	}
	
	// Update is called once per frame
	void Update () {
		if (checkAvgEnv == true) {
			if (currAvgEnv > (prevAvgEnv * 1.002f)) {
				//Torch changes to dreader color
				if (currFlameColor == 0) {
					changeTorches (dreadTorch);
					randomNum();
					if (flameSoundPlaying == false && currRand == 1 ) {
						flameBGM.Play();
						flameSoundPlaying = true;
					}
					currFlameColor = 1;
				}
				//Walls change as heart rate increases due to being scared.
				crossFadeScript.crossFadeTo (mainWallChange, wallOffset, wallTiling);
				
				//Add more enemies (delusions of enemies) in the pillar room based on high HRV
				pillarRoom.enableDelusions();
				
				toggleEnvFlags();
			} else if (currAvgEnv <= (prevAvgEnv * 1.002f)){  //Should still be *0.something equivalent of high heart rate value
				//torch changes back to normal
				if (currFlameColor == 1) {
					changeTorches (baseTorch);
					currFlameColor = 0;
				}
				toggleEnvFlags();
			} else { //When the value is near the previous average (initially anyways)
				toggleEnvFlags();
			}
		}
		
		/**envTime += Time.deltaTime;
			if (envTime >= 1f) {
				//pillarRoom.enableDelusions();
				crossFadeScript.crossFadeTo (mainWallChange, wallOffset, wallTiling);
				envTime = 0f;
			}
		
		if (crossFadeScript.hasWallsChanged() == true && respawnLocation.passedCheckPoint1 == true) {
			CheckPoint1Change = true;
		} 
		
		if (crossFadeScript.hasWallsChanged() == true && respawnLocation.passedCheckPoint2 == true) {
			CheckPoint2Change = true;
		} **/
		
		//Set of code to check player location, and change wall texture and materials accordingly
		//Based on the activated bool flags, change to the corresponding wall textures
		if (crossFadeScript.hasWallsChanged() == true && respawnLocation.passedCheckPoint1 == true && usedSecondChange == false) {
			mainWallChange = secondWallChange;
			crossFadeScript.setNewMaterial(thirdMaterial);
			crossFadeScript.resetWallChanging();
			usedSecondChange = true;
		} else if (crossFadeScript.hasWallsChanged() == true && respawnLocation.passedCheckPoint2 == true && usedThirdChange == false) {
			mainWallChange = thirdWallChange;
			crossFadeScript.setNewMaterial(fourthMaterial);
			crossFadeScript.resetWallChanging();
			usedThirdChange = true;
		}
	}
	
	//Toggle avergae check for environmen changes
	public void toggleEnvCheck () {
		checkAvgEnv = true;
	}
	
	//Revert back the different flags to their default state
	public void toggleEnvFlags () {
		checkAvgEnv = false;
		torchChanged = false;
		flameSoundPlaying = false;
	}
	
	//Function that handles the torch changes
	public void changeTorches (Color secondColor) {
		if (torchChanged == false) {
			foreach (GameObject torch in torches) {
				//Color baseColor = torch.GetComponentInChildren<Light>().color;
				torch.GetComponentInChildren<ParticleSystem>().startColor = secondColor;
				torch.GetComponentInChildren<Light>().color = secondColor; //Color.Lerp (baseColor, secondColor, Time.deltaTime * duration);
			}
			//flameBGM.Play();
			torchChanged = true;
		}
	}
	
	//Play the torch bgm randomly.
	void randomNum() {
        currRand = Random.Range(1, 3);
        if (currRand == prevRand)
        {
            randomNum();
        }
	}
	
	//Used to set up the averages for the comparison values.
	public void setAveragesEnv (float prev, float curr) {
		prevAvgEnv = prev;
		currAvgEnv = curr;
	}
}
