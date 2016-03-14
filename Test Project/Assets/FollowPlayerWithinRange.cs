using UnityEngine;
using System.Collections;

public class FollowPlayerWithinRange : MonoBehaviour {

    public Camera m_Camera;
	public Transform playerTransform;
    public int speed;
    public GameObject ProximityDetector;
    public AudioClip bahamutCry;

    ProximityDetector proxDet;
    bool close = false;
    bool hasCryed;
    GameObject parent;

    // Use this for initialization
    void Start()
    {
        proxDet = ProximityDetector.GetComponent<ProximityDetector>();
        hasCryed = proxDet.hasCryed;
        parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        close = proxDet.close;
        hasCryed = proxDet.hasCryed;
        parent.transform.LookAt(parent.transform.position + m_Camera.transform.rotation * Vector3.up,
            m_Camera.transform.rotation * -Vector3.forward);
        if(close == true)
        {
            //parent.transform.position = Vector3.MoveTowards(parent.transform.position, m_Camera.transform.position, speed * Time.deltaTime);
			parent.transform.position = Vector3.MoveTowards(parent.transform.position, playerTransform.position, speed * Time.deltaTime);
            if (hasCryed == false)
            {
                GetComponent<AudioSource>().PlayOneShot(bahamutCry, 0.8f);
                proxDet.hasCryed = true;
            }
        }
    }
}
