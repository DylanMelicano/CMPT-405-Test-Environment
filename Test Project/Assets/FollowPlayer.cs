using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    public Camera m_Camera;
    public int speed;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(transform.position + m_Camera.transform.rotation * -Vector3.up,
            m_Camera.transform.rotation * -Vector3.forward);

        transform.position = Vector3.MoveTowards(transform.position, m_Camera.transform.position, speed * Time.deltaTime);
    }
}
