using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {
	
	bool enteredFinalRoom = false;
	bool panicStart = false;
	bool blackOutDone = false;
	bool soundPlaying = false;
	
	public AudioClip finalLaugh; 
	public Color darkness = new Color(0f, 0f, 0f, 1.0f);
	public GameObject MazeBGM;
	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (panicStart) {
			if (RenderSettings.fogDensity < 1f) {
				RenderSettings.fogColor = darkness;
				RenderSettings.fogDensity += 0.01f;
			} else {
				player.GetComponentInChildren<Light>().intensity -= 0.1f;
				if (player.GetComponentInChildren<Light>().intensity == 0f) {
					player.GetComponentInChildren<ParticleSystem>().loop = false;
					blackOutDone = true;
				}
			}
		}
		
		if (blackOutDone) {
			//Remove the movement script of the player
			player.GetComponent<ExampleMovement>().enabled = false;
			if (MazeBGM.GetComponent<AudioSource>().volume > 0f) {
				MazeBGM.GetComponent<AudioSource>().volume -= 0.001f;
			}
			if (soundPlaying == false) {
				this.GetComponent<AudioSource>().PlayOneShot(finalLaugh);
				soundPlaying = true;
			}
			if (this.GetComponent<AudioSource>().isPlaying == false) {
				Application.LoadLevel("Game Finish");
			}
		}
	}
	
	void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enteredFinalRoom = true;
        }
    }
	
	void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panicStart = true;
        }
    }
}
