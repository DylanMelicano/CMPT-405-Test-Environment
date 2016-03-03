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
	//public float duration = 1.5f;
	
	//Wall texture and material changes
	CrossFade crossFadeScript;
	Texture mainWallChange;
	public Texture firstWallChange;
	public Texture secondWallChange;
	public Texture thirdWallChange;
	
	//first material contains default brick2 texture
	public Material secondMaterial;	//base texture is firstWallChange
	public Material thirdMaterial; //base texture is secondWallChange
	public Material fourthMaterial; //base texture is thirdWallChange
	
	Vector2 wallOffset;
	Vector2 wallTiling;
	
	bool passedCheckPoint1 = false;
	bool passedCheckPoint2 = false;
	bool passedCheckPoint3 = false;
	
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
					if (flameSoundPlaying == false) {
						flameBGM.Play();
						flameSoundPlaying = true;
					}
					currFlameColor = 1;
				}
				//Walls change to next texture based on location and current material
				//Once passed the area checkpoint, change the next texture and material to change to, as well as the 
				//as well as return changing once again
				crossFadeScript.crossFadeTo (mainWallChange, wallOffset, wallTiling);
				/**if (respawnLocation.passedCheckPoint("CheckPoint1")) {
					crossFadeScript.crossFadeTo (mainWallChange, wallOffset, wallTiling);
				}**/
				
				//Add more enemies (delusions of enemies) in the pillar room based on high HRV
				//pillarRoom.enableDelusions();
				
				toggleEnvFlags();
			} else if (currAvgEnv <= (prevAvgEnv * 1.002f)){
				//torch changes back to normal
				if (currFlameColor == 1) {
					changeTorches (baseTorch);
					currFlameColor = 0;
				}
				//Walls do not change back at all
				toggleEnvFlags();
			} else {
				toggleEnvFlags();
			}
		}
		
		//Set of code to check player location, and change wall texture and materials accordingly
		/**if (respawnLocation.passedCheckPoint("CheckPoint1") && passedCheckPoint1 == false) {
			passedCheckPoint1 = true;	
		}

		if (respawnLocation.passedCheckPoint("CheckPoint2") && passedCheckPoint2 == false) {
			mainWallChange = secondWallChange;
			crossFadeScript.setNewMaterial(thirdMaterial);
			crossFadeScript.resetWallChanging();
			passedCheckPoint2 = true;
		} 
		
		if (respawnLocation.passedCheckPoint("CheckPoint3") && passedCheckPoint3 == false) {
			mainWallChange = thirdWallChange;
			crossFadeScript.setNewMaterial(fourthMaterial);
			crossFadeScript.resetWallChanging();
			passedCheckPoint3 = true;
		}**/
			
			envTime += Time.deltaTime;
			if (envTime >= 5f) {
				pillarRoom.enableDelusions();
				envTime = 0f;
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
