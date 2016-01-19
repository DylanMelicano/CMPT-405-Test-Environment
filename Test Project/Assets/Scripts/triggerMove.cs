using UnityEngine;
using System.Collections;

public class triggerMove : MonoBehaviour {
    public float activeTime;
    public float speed;


    private bool triggered = false;
    private float timeSincetriggered;
    
    void Update()
    {
        if (triggered == true)
        {
            Debug.Log("hello");
            transform.position += new Vector3(0, 0, -speed);
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
