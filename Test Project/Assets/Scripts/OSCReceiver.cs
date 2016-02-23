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
	
	//VARIABLES TO COMPUTE AVERAGE 
	float[] stackOfRates;
	
	public float sumOfRates = 0f;
	public int numOfReadings = 0; 
	
	public int testNumber = 0;
	public int testNumberCheck = 0;
	
	public float averageOfRates;
	
	public float prevAverage = 0f;
	public float currAverage = 0f;
	
	float calibrationTime = 0f;
	bool startComputation = false;
	bool calculated = false;
	
	PlayerChanges playerChanges;
	
	// Use this for initialization
	void Start () {
		
		//Initializes on start up to listen for messages
		//make sure this game object has both UDPPackIO and OSC script attached
		stackOfRates = new float[500];
		
		//The scripts that will do the changes
		playerChanges = GameObject.FindWithTag("PlayerChange").GetComponent<PlayerChanges>();
		
		UDPPacketIO udp = GetComponent<UDPPacketIO>();
		udp.init(RemoteIP, SendToPort, ListenerPort);
		handler = GetComponent<Osc>();
		handler.init(udp);
		handler.SetAllMessageHandler(AllMessageHandler);
	}
	
	// Update is called once per frame
	void Update () {
		calibrationTime = calibrationTime + Time.deltaTime;
		if (calibrationTime >= 10f && calculated == false) {

			//Debug.Log ("Ten seconds have passed. Number of Readings: " + numOfReadings + " test Reading: " + testNumber + " Checking array content: " + testNumberCheck + " Average of Readings: " + averageRates());			
			prevAverage = currAverage;
			currAverage = averageRates();
			
			Debug.Log("Ten seconds have passed. Previous Average of Readings: " + prevAverage + " Current Average of Readings: " + currAverage);
			
			//If no recorded averages yet, do not start the checking
			if (prevAverage != 0f && currAverage != 0f) {
					playerChanges.setAverages(prevAverage, currAverage);
					playerChanges.toggleCheck();
			}

			calibrationTime = 0f;	//Start another average measurement in the next 10 seconds
		}
		
		/**for (int i = 0; i < stackOfRates.Length; i++) {
				if (stackOfRates[i] > 0f) {
					testNumberCheck++;
				}
			}**/
		// If effect is still going on, do not compare new measurement to new measurement
		// If changing effect is done, start comparing measurements again
		// Call on methods from the various Environment Changing Scripts, pass them the previous and new values, and let them do the increments or not.
		// Have the changes be little increments until they reach their maximum threshold
		// Environment changes will be permanent. With fog possibly weakening if we want to have it.
		// Enemy and player (torch light, walk speed(?), camera effects (?) ) changes will vary via the measurement
	
	}
	
	public void AllMessageHandler(OscMessage oscMessage) {
		
		string msgString = Osc.OscMessageToString(oscMessage); //the message and value combined
		string msgAddress = oscMessage.Address; //the message parameters		
		//since we are just transferring double/float types I will just declare the message as float
		float msgValue = (float) oscMessage.Values[0]; //the message value
		
		/**if (calculated == false) {
			//Debug.Log(msgValue);
			testNumber++;
		}**/
		
		//FUNCTIONS YOU WANT CALLED WHEN A SPECIFIC MESSAGE IS RECEIVED
		switch (msgAddress){
			default:
				pushRates(msgValue);
				break;
		}		
	}
	
	//Functions for the array
	public void addRates (float msgValue) {
		
		//Add to the sumOfRates each message value
        sumOfRates = sumOfRates + msgValue;
        
        //For every addition of rates, increment the count
        numOfReadings++;
	}
	
	public void pushRates (float msgValue) {     
        if (numOfReadings >= 500) {
            for (int c = 0; c < 499; c++) {
               stackOfRates[c] = stackOfRates [c+1];
            }
            stackOfRates[499] = msgValue;
        } else {
            stackOfRates[numOfReadings] = msgValue;
            numOfReadings++;
        }
    }
	
	public float averageRates () {
        for (int i = 0; i < numOfReadings; i++) {
            sumOfRates = sumOfRates + stackOfRates[i];
        }

        averageOfRates = sumOfRates / ((float) numOfReadings);

        sumOfRates = 0f;
       
        return averageOfRates;
    }
	
	// Function to clear of all the values read by the LighTStone device
	// Used to reset measurements
	public void clearStackOfRates () {
		for (var i = 0; i < stackOfRates.Length; i++) {
            stackOfRates[i] = 0f;
        }
	}
	
	public float getAverageOfRates () {
		return averageOfRates;
	}
	
	public float getPrevAverage () {
		return prevAverage;
	}
	
	public float getCurrAverage () {
		return currAverage;
	}
	
	void startComp () {
		startComputation = true;
		calculated = false;
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
