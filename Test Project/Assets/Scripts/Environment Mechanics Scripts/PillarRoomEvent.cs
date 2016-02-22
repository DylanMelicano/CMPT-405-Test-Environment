using UnityEngine;
using System.Collections;

public class PillarRoomEvent : MonoBehaviour {
	
	public bool outIdleZone = false;
	public AudioClip roomSound;
	public int keyNumber;

    public Color colour = new Color(0.2f, 0f, 0f, 1.0f);
	
	public Material firstWall;
	public Material redWall;
    public Texture emissionWall;
    Material currWall;
	private float eventTime;
	
	GameObject[] pillarWalls;
	GameObject[] pillars;
	InventoryScript invScript;
	AudioSource roomAudio;
    public GameObject pillarGuardian;
	
	bool soundPlaying = false;
	bool allRotating = false;
    bool guardianSpawned = false;
	

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
            if(guardianSpawned == false)
            {
                pillarGuardian.SetActive(true);
                guardianSpawned = true;
            }
            if (RenderSettings.ambientIntensity < 0f) {
				RenderSettings.ambientIntensity += 0.01f;
			} else {
				eventTime = eventTime + Time.deltaTime;
				
				currWall = redWall;
				
				/**if (eventTime > 2f) {
					currWall = firstWall;
				}**/
				
				foreach (GameObject pillarWall in pillarWalls) {
					pillarWall.GetComponent<Renderer>().material = currWall;
                    pillarWall.gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                    pillarWall.GetComponent<Renderer>().material.SetColor("_EmissionColor", (colour * Mathf.LinearToGammaSpace(Mathf.PingPong(Time.time, 5f))));
                    //pillarWall.GetComponent<Renderer>().material.SetFloat("_EmissiveIntensity", 0.3f);
                    pillarWall.GetComponent<Renderer>().material.SetTexture("_EmissionMap", emissionWall);
                }
				
				if (allRotating == false) {
					foreach (GameObject pillar in pillars) {
						pillar.GetComponent<rotateMe>().enabled = true;
						//Change the Material to a scary one. 
					}
					allRotating = true;
				}
				
				if (soundPlaying == false) {
					roomAudio.Play();
					soundPlaying = true;
				}
				
			}
		}
	}

    public void stopEvent ()
    {
        foreach (GameObject pillarWall in pillarWalls)
        {
            pillarWall.GetComponent<Renderer>().material = firstWall;
        }

        foreach (GameObject pillar in pillars)
        {
            pillar.GetComponent<rotateMe>().enabled = false;
        }
        pillarGuardian.SetActive(false);
        roomAudio.Stop();
        soundPlaying = false;
    }
}
