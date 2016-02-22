using UnityEngine;
using System.Collections;

public class ActivateStatic : MonoBehaviour {

    public GameObject staticQuad;
    public float maximumProximity = 120;
    staticAnimator staticScript;
    public float distance;

    GameObject player;

    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player");
        staticScript = staticQuad.GetComponent<staticAnimator>();
	}
	
	// Update is called once per frame
	void Update () {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < maximumProximity && distance >= 0)
        {
            staticScript.staticOn = true;
            staticScript.proximity = (int)Mathf.Lerp(230f, 0.0f, distance/maximumProximity);
        }
	}
}
