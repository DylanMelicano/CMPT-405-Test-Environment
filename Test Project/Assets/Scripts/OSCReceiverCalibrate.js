
public var RemoteIP : String = "127.0.0.1"; //127.0.0.1 signifies a local host (if testing locally
public var SendToPort : int = 9000; //the port you will be sending from
public var ListenerPort : int = 60000; //the port you will be listening on
public var controller : Transform;
public var object : GameObject;
private var handler : Osc;

var stackOfRates : float[];

public var sumOfRates : float = 0f;
public var numOfReadings : int = 0;

public var averageOfRates : float;

var calibrationTime : float = 0f;
var startComputation : boolean = false;
var calculated : boolean= false;

//VARIABLES YOU WANT TO BE ANIMATED
private var yRot : float = 0f; //the rotation around the y axis


public function Start ()
{
    //Initializes on start up to listen for messages
    //make sure this game object has both UDPPackIO and OSC script attached

    stackOfRates = new float[10];

    var udp : UDPPacketIO = GetComponent("UDPPacketIO");
    udp.init(RemoteIP, SendToPort, ListenerPort);
    handler = GetComponent("Osc");
    handler.init(udp);
    handler.SetAllMessageHandler(AllMessageHandler);

    // Get renderer of gameobject
    // rend = gameObject.getComponent(Renderer);

}

Debug.Log("Running");

public function Update () {
	if (startComputation === true) {
		calibrationTime = calibrationTime + Time.deltaTime;
		
		if (calibrationTime >= 10f && calculated === false) {
			Debug.Log ("Average: " + averageRates());
			calculated = true;
			startComputation = false;
			calibrationTime = 0f;
		}
	}

}

//These functions are called when messages are received
//Access values via: oscMessage.Values[0], oscMessage.Values[1], etc

public function AllMessageHandler(oscMessage: OscMessage) {
    var msgString = Osc.OscMessageToString(oscMessage); //the message and value combined
    var msgAddress = oscMessage.Address; //the message parameters
    var msgValue = oscMessage.Values[0]; //the message value
    //Debug.Log(msgString); //log the message and values coming from OSC

     //FUNCTIONS YOU WANT CALLED WHEN A SPECIFIC MESSAGE IS RECEIVED
    switch (msgAddress){
        default:
            pushRates (msgValue);
            break;
    }

   //Debug.Log ("Current Value: " + msgValue + " | Current Array: " + stackOfRates[0] + ", " + stackOfRates[1] + ", " + stackOfRates[2] + ", " + stackOfRates[3] + ", " + stackOfRates[4] + ", " + stackOfRates[5] + ", " + stackOfRates[6] + ", " + stackOfRates[7] + ", " + stackOfRates[8] + ", " + stackOfRates[9]);
}

    //FUNCTIONS CALLED BY MATCHING A SPECIFIC MESSAGE IN THE ALLMESSAGEHANDLER FUNCTION
    public function addRates (msgValue) : void 
    {
        //Add to the sumOfRates each message value
        sumOfRates = sumOfRates + msgValue;
        
        //For every addition of rates, increment the count
        numOfReadings += 1.0f;
    }

    public function pushRates (msgValue) : void 
    {     
        if (numOfReadings >= 10) {
            for (var c = 0; c < 9; c++) {
               stackOfRates[c] = stackOfRates [c+1];
            }
            stackOfRates[9] = msgValue;
        } else {
            stackOfRates[numOfReadings] = msgValue;
            numOfReadings += 1;
        }
    }

    public function averageRates () : float 
    {
        for (var i = 0; i < 10; i++) {
            sumOfRates = sumOfRates + stackOfRates[i];
        }

        averageOfRates = sumOfRates / 10f;

        sumOfRates = 0f;
       
        return averageOfRates;
    }
	
	// Function to clear of all the values read by the LighTStone device
	// Used to reset measurements
	public function clearStackOfRates () : void {
		for (var i = 0; i < 10; i++) {
            stackOfRates[i] = 0f;
        }
		//Debug.Log ("Current Array: " + stackOfRates[0] + ", " + stackOfRates[1] + ", " + stackOfRates[2] + ", " + stackOfRates[3] + ", " + stackOfRates[4] + ", " + stackOfRates[5] + ", " + stackOfRates[6] + ", " + stackOfRates[7] + ", " + stackOfRates[8] + ", " + stackOfRates[9]);
	}
	
	public function getAverageOfRates () : float{
		return averageOfRates;
	}

	function startComp () : void {
		startComputation = true;
		calculated = false;
	}