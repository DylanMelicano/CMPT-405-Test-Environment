using UnityEngine;
using System.Collections;

public class triggerMove : MonoBehaviour {
    public float activeTime;
    public float xspeed = 0;
    public float yspeed = 0;
    public float zspeed = 0;


    private bool triggered = false;
    private float timeSincetriggered;
    
    void Update()
    {
        if (triggered == true)
        {
            Debug.Log("hello");
            transform.position += new Vector3(xspeed, yspeed, zspeed);
            if ((Time.time - timeSincetriggered) > activeTime)
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
