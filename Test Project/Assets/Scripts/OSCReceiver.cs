using UnityEngine;
using System.Collections;

public class OSCReceiver : MonoBehaviour {
	
	public string RemoteIP = "127.0.0.1"; //127.0.0.1 signifies a local host (if testing locally
	public int SendToPort = 9000; //the port you will be sending from
	public int ListenerPort = 60000; //the port you will be listening on
	
	public Transform controller;
	public GameObject someObject;
	
	private Osc handler;
	
	//VARIABLES YOU WANT TO BE ANIMATED
	private float yRot = 0f; //the rotation around the y axis
	private float  textureValue = 0f;
	// Use this for initialization
	void Start () {
		
		//Initializes on start up to listen for messages
		//make sure this game object has both UDPPackIO and OSC script attached
	
		UDPPacketIO udp = GetComponent<UDPPacketIO>();
		udp.init(RemoteIP, SendToPort, ListenerPort);
		handler = GetComponent<Osc>();
		handler.init(udp);
		handler.SetAllMessageHandler(AllMessageHandler);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void AllMessageHandler(OscMessage oscMessage) {
		
		string msgString = Osc.OscMessageToString(oscMessage); //the message and value combined
		string msgAddress = oscMessage.Address; //the message parameters		
		//since we are just transferring double/float types I will just declare the message as float
		float msgValue = (float) oscMessage.Values[0]; //the message value
		
		if (msgValue > 0f) {
			Debug.Log(msgValue);
		}
		
		//FUNCTIONS YOU WANT CALLED WHEN A SPECIFIC MESSAGE IS RECEIVED
		switch (msgAddress){
			default:
				float rotateVal = msgValue * 10;
				Rotate(rotateVal);
				ChangeTextureHeartValue (msgValue);
				break;
		}		
	}
	
	//FUNCTIONS CALLED BY MATCHING A SPECIFIC MESSAGE IN THE ALLMESSAGEHANDLER FUNCTION
    public void Rotate(float msgValue) //rotate the cube around its axis
    {
        yRot = msgValue;
    }

    //Change Texture Heartbeat value
    public void ChangeTextureHeartValue(float msgValue) {
        textureValue = msgValue;
    }
}
