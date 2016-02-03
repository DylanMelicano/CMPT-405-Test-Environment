using UnityEngine;
using System.Collections;

public class triggerMove : MonoBehaviour {
    public float activeTime;
    public float startDelay;
    public float xspeed = 0;
    public float yspeed = 0;
    public float zspeed = 0;
	public AudioClip scream;
	bool audioPlaying = false;


    private bool triggered = false;
    private float timeSincetriggered;
    
    void Update()
    {
        if (triggered == true && startDelay < (Time.time - timeSincetriggered))
        {
            //Debug.Log("hello");
            transform.position += new Vector3(xspeed, yspeed, zspeed);
			
			if (audioPlaying == false) {
				this.GetComponent<AudioSource>().PlayOneShot(scream);
				audioPlaying = true;
			}
			
            if (((Time.time - startDelay) - timeSincetriggered) > activeTime)
            {
                gameObject.SetActive(false);
            }
        }
        
    }

	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {			
            triggered = true;
            timeSincetriggered = Time.time;
        }
    }
}
