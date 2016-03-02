using UnityEngine;
using System.Collections;

public class ProximityDetector : MonoBehaviour {

    public bool close = false;
    public bool hasCryed = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            close = true;
            hasCryed = false;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            close = false;
        }
    }
}
