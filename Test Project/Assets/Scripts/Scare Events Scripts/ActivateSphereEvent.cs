using UnityEngine;
using System.Collections;

public class ActivateSphereEvent : MonoBehaviour {


    public AudioClip doorClose;

    private bool enteredSphere = false;
	GameObject sphereRoom;
	GameObject sphereDoor;
	private bool lightComplete = false;
	private bool doorShutDone = false;

	// Use this for initialization
	void Start () {
		sphereRoom = GameObject.FindWithTag("SphereRoom");
		sphereDoor = GameObject.FindWithTag("SphereDoor");
	}
	
	// Update is called once per frame
	void Update () {
		if (enteredSphere == true) {
			if (RenderSettings.ambientIntensity < 1.10f) {
				RenderSettings.ambientIntensity += 0.01f;
			} else {
				lightComplete = true;
			}
			
			//Debug.Log(sphereDoor.transform.position.y);
			
			if (sphereDoor.transform.position.y > -4.35f) {
				Vector3 temp = new Vector3(0,0.018f,0);
				sphereDoor.transform.position -= temp;
			} else {
				doorShutDone = true;
			}
				
			if (lightComplete == true && doorShutDone == true) {
				sphereRoom.GetComponent<SphereEvent>().enabled = true;
			}
		}
		
		/**if (enteredSphere == false) {
			if (RenderSettings.ambientIntensity > 0f)
				RenderSettings.ambientIntensity -= 0.1f;
		}**/
	
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Player")){
			Debug.Log ("Sphere Room Activate");
			//Increase lighting for the room
			enteredSphere = true;
            sphereDoor.GetComponent<AudioSource>().PlayOneShot(doorClose);	
			//sphereRoom.GetComponent<SphereEvent>().enabled = true;
		}
	}
	/*
	void OnTriggerExit (Collider other) {
		if (other.CompareTag("Player")){
			Debug.Log ("Sphere Room Deactivate");
			//Increase lighting for the room
			enteredSphere = false;			
			sphereRoom.GetComponent<SphereEvent>().enabled = false;
			gameObject.SetActive(false);
		}
	}
    */
	
	public void openSphereRoomDoor () {
		sphereDoor.SetActive(false);
	}
}
