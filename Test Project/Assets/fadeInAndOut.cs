using UnityEngine;
using System.Collections;

public class fadeInAndOut : MonoBehaviour {

    public float minAlpha;
    public float maxAlpha;
    public float interval;
    Color alphaColour;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

            //alpha = (Mathf.Lerp(transform.GetComponent<Renderer>().GetComponent<Material>().color.a, minAlpha, Time.deltaTime * interval));
            alphaColour = new Color(1, 1, 1, Mathf.PingPong(Time.time, maxAlpha)+minAlpha);
            transform.GetComponent<Renderer>().GetComponent<MeshRenderer>().material.SetColor("_Color", alphaColour);

    }
}
