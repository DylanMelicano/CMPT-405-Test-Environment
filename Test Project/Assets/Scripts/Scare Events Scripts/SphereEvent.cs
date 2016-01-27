using UnityEngine;
using System.Collections;

public class SphereEvent : MonoBehaviour {
	
	public float eventDuration;	//In seconds
	private float eventsTime; // Manipulates the series of events
	
	//index 0 = default material
	public Material[] sphereMaterials;
	
	public AudioClip doorClose;
	public AudioClip soundEffect1;
	public AudioClip soundEffect2; 	
	private AudioSource sphereAudio;
	
	//Editor changeable rotate speed
	public float scrollSpeedX;
    public float scrollSpeedY;
	
	//Update used rotate speed (has value initially)
	private float actualSpeedX = 0.1f;
    private float actualSpeedY = 0.1f;
	
    public bool main = true;
    public bool bump = true;
	
	private float offsetX;
	private float offsetY;
	
	private int randTemp;
    private int previousTemp = 0;
	private bool calledRandom = false;
	private bool audioPlaying = false;

	// Use this for initialization
	void Start () {
		RandomNumber (1,6);
		sphereAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (eventDuration > 0/* || sphereAudio.isPlaying == true*/) {
			eventsTime = eventsTime + Time.deltaTime;
			
			// Start with the initial rotation of sphereMaterials then change accordingly
			rotateTexture (actualSpeedX, actualSpeedY);
			
			//Based on the total event duration, change the rotation direction and material/texture on intervals
			if (eventsTime >= 2f) //(eventsDuration/sphereMaterials.size))
			{	
				if (calledRandom == false) {	
					RandomNumber (1,6);
					calledRandom = true;
				}

				randomizeRotation();
				randomizeMaterialChoice();
				
				if (audioPlaying == false) {
					sphereAudio.PlayOneShot(soundEffect1);
					audioPlaying = true;
				}
				
				previousTemp = randTemp;
				calledRandom = false;
				eventsTime = 0.0f;
			} 
			
			if (eventDuration != 0) {
				eventDuration -= 1.0f; //Still needs fixing
			}
		}
		
		if (eventDuration == 0) {
			sphereAudio.loop = false;
		}
	
		if (eventDuration == 0/* && sphereAudio.isPlaying == false */) {
			ActivateSphereEvent activeEventScript;
			activeEventScript = GameObject.Find("ActivationEvent").GetComponent<ActivateSphereEvent>();
			
			if (RenderSettings.ambientIntensity > 0f && RenderSettings.ambientIntensity != 0f) {
				RenderSettings.ambientIntensity -= 0.05f;
			}
			
			if (RenderSettings.ambientIntensity <= 0f) {
				activeEventScript.openSphereRoomDoor();
			}
		}
	}
	
	//Rotating the offset script
	public void rotateTexture (float xSpeed, float ySpeed) {
		
		offsetX = (xSpeed * Time.time);
        offsetY = (ySpeed * Time.time);
        
		if(main)
        {
            GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
        }
        if(bump)
        {
            GetComponent<Renderer>().material.SetTextureOffset("_BumpMap", new Vector2(offsetX, offsetY));
        }
	}
	
	//Randomize selection
	public void RandomNumber(int firstRange, int secondRange)
    {
        randTemp = Random.Range(firstRange, secondRange);
        if (randTemp == previousTemp)
        {
            RandomNumber(firstRange, secondRange);
        }
    }
	
	//Randomize choice of rotation
	public void randomizeRotation () {
		if (randTemp == 1) {
			actualSpeedX = 0;
			actualSpeedY = scrollSpeedY;
		} else if (randTemp == 2) {
			actualSpeedX = scrollSpeedX;
			actualSpeedY = 0;
		} else if (randTemp == 3) {
			actualSpeedX = scrollSpeedX;
			actualSpeedY = scrollSpeedY;
		} else if (randTemp == 4) {
			actualSpeedX = 0;
			actualSpeedY = -scrollSpeedY;
		} else if (randTemp == 5) {
			actualSpeedX = -scrollSpeedX;
			actualSpeedY = 0;
		} else {
			actualSpeedX = -scrollSpeedX;
			actualSpeedY = -scrollSpeedY;
		}
	}
	
	//Randomize Material Change
	public void randomizeMaterialChoice () {
		if (randTemp == 1 || randTemp == 2 || randTemp == 3) {
			this.GetComponent<Renderer>().material = sphereMaterials[0];
		} else {
			this.GetComponent<Renderer>().material = sphereMaterials[1];
		}
	}
}
