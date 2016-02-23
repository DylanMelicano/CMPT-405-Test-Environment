using UnityEngine;
using System.Collections;

public class mazeGuardianBehaviour : MonoBehaviour {

    GameObject m_Camera;
    // Use this for initialization
    void Start () {
        m_Camera = GameObject.FindWithTag("MainCamera");
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(transform.position + m_Camera.transform.rotation * -Vector3.up,
                m_Camera.transform.rotation * -Vector3.forward);
    }
}
