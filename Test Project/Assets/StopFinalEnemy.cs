using UnityEngine;
using System.Collections;

public class StopFinalEnemy : MonoBehaviour {

    public GameObject lastEnemy;
    bool stopEnemy = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (stopEnemy == true)
        {
			if (lastEnemy != null) {
				lastEnemy.GetComponent<FollowPlayer>().enabled = false;
				if (lastEnemy.GetComponent<ActivateStatic>().maximumProximity > 10f) {
					lastEnemy.GetComponent<ActivateStatic>().maximumProximity-=0.1f;
				}
				lastEnemy.GetComponent<AudioSource>().loop = false;
			}
        }
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stopEnemy = true;
        }
    }

}
