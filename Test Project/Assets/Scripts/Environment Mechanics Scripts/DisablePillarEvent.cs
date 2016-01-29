using UnityEngine;
using System.Collections;

public class DisablePillarEvent : MonoBehaviour {

    bool playerLeft = false;
    PillarRoomEvent pillarEvent; 

	// Use this for initialization
	void Start () {
        pillarEvent = GameObject.Find("ActivatePillarEvent").GetComponent<PillarRoomEvent>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (playerLeft)
        {
            if (RenderSettings.ambientIntensity > 0f)
            {
                RenderSettings.ambientIntensity -= 0.05f;
            } else
            {
                pillarEvent.stopEvent();
                pillarEvent.enabled = false;
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Yosh");
        if (other.CompareTag("Player"))
        {
            playerLeft = true;
        }
    }
}
