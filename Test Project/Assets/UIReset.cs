using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIReset : MonoBehaviour {

    Canvas canvas;

    // Use this for initialization
    void Start () {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        canvas.GetComponentInChildren<Text>().text = "";
    }

    void onGUI()
    {
        
    }
}
