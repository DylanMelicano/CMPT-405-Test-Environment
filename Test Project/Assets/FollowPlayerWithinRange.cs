using UnityEngine;
using System.Collections;

public class FollowPlayerWithinRange : MonoBehaviour {

    public Camera m_Camera;
    public int speed;
    public GameObject ProximityDetector;

    ProximityDetector proxDet;
    bool close = false;
    GameObject parent;

    // Use this for initialization
    void Start()
    {
        proxDet = ProximityDetector.GetComponent<ProximityDetector>();

        parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        close = proxDet.close;
        parent.transform.LookAt(parent.transform.position + m_Camera.transform.rotation * Vector3.up,
            m_Camera.transform.rotation * -Vector3.forward);
        if(close == true)
        {
            parent.transform.position = Vector3.MoveTowards(parent.transform.position, m_Camera.transform.position, speed * Time.deltaTime);
        }
    }
}
