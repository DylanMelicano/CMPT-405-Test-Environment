using UnityEngine;
using System.Collections;

public class PillarRoomEvent : MonoBehaviour {
	
	public bool outIdleZone = false;
	public AudioClip roomSound;
	public int keyNumber;
	
	public Material firstWall;
	public Material redWall;
	Material currWall;
	private float eventTime;
	bool soundPlaying = false;
	
	GameObject[] pillarWalls;
	GameObject[] pillars;
	InventoryScript invScript;
	AudioSource roomAudio;
	
	bool audioPlaying = false;

	// Use this for initialization
	void Start () {
		pillarWalls = GameObject.FindGameObjectsWithTag("PillarWall");
		pillars = GameObject.FindGameObjectsWithTag("Pillars");
		invScript = GameObject.Find("KeyInventory").GetComponent<InventoryScript>();
		roomAudio = GetComponent<AudioSource>();
		roomAudio.clip = roomSound;
	}
	
	// Update is called once per frame
	void Update () {
		if (invScript.hasKey(1)) {
			if (RenderSettings.ambientIntensity < 1.0f) {
				RenderSettings.ambientIntensity += 0.01f;
			} else {
				eventTime = eventTime + Time.deltaTime;
				
				currWall = redWall;
				
				/**if (eventTime > 2f) {
					currWall = firstWall;
				}**/
				
				foreach (GameObject pillarWall in pillarWalls) {
					pillarWall.GetComponent<Renderer>().material = currWall;
				}
			
				foreach (GameObject pillar in pillars) {
					pillar.GetComponent<rotateMe>().enabled = true;
				}
				
				if (soundPlaying == false) {
					roomAudio.Play();
					soundPlaying = true;
				}
				
			}
		}
	}
}
