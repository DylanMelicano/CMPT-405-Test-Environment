using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    public Camera m_Camera;
    public int speed;
	RespawnScript respawnStatus;

    // Use this for initialization
    void Start () {
		respawnStatus = GameObject.FindWithTag("Player").GetComponent<RespawnScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if (respawnStatus.getPlayerStatus() == false) {
			transform.LookAt(transform.position + m_Camera.transform.rotation * -Vector3.up,
				m_Camera.transform.rotation * -Vector3.forward);

			transform.position = Vector3.MoveTowards(transform.position, m_Camera.transform.position, speed * Time.deltaTime);
		}
    }
}
