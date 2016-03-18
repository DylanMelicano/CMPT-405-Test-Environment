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
            lastEnemy.GetComponent<FollowPlayer>().enabled = false;
            lastEnemy.GetComponent<AudioSource>().Pause();
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
