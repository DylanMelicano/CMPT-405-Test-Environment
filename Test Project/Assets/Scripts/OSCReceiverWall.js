
public var RemoteIP : String = "127.0.0.1"; //127.0.0.1 signifies a local host (if testing locally
public var SendToPort : int = 9000; //the port you will be sending from
public var ListenerPort : int = 60000; //the port you will be listening on
public var controller : Transform;
public var object : GameObject;
public var gameReceiver = "Wall"; //the tag of the object on stage that you want to manipulate
private var handler : Osc;

// Public Texture Objects
public var baseMat : Material;
public var secondMat : Material;

//private var rend : Renderer;

//VARIABLES YOU WANT TO BE ANIMATED
private var yRot : float = 0f; //the rotation around the y axis
private var textureValue : float = 0f;

public function Start ()
{
    //Initializes on start up to listen for messages
    //make sure this game object has both UDPPackIO and OSC script attached

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
    //var go = GameObject.Find (gameReceiver);
    // object.transform.Rotate(0, yRot, 0);
    
    //Wall Changing (all walls so far)
    /**var walls: GameObject[];
    walls = GameObject.FindGameObjectsWithTag("Wall");
    for (var wall: GameObject in walls) {
        if (textureValue > 0.25f) {
            wall.GetComponent(Renderer).material = secondMat;
        } else {
           wall.GetComponent(Renderer).material = baseMat;
        }
    }

         //Torch Changing (all torches so far)
          var torches: GameObject[];
          torches = GameObject.FindGameObjectsWithTag("Torch");
          for (var torch: GameObject in torches) {
               if (textureValue > 0.25f) {
               torch.GetComponentInChildren(Light).range = 4.0f;
               torch.GetComponentInChildren(ParticleSystem).startLifetime = 0.55f;
           } else {
               torch.GetComponentInChildren(Light).range = 5.77f;
                torch.GetComponentInChildren(ParticleSystem).startLifetime = 1.80f;
                }
           }**/
}

//These functions are called when messages are received
//Access values via: oscMessage.Values[0], oscMessage.Values[1], etc

public function AllMessageHandler(oscMessage: OscMessage) {

    var msgString = Osc.OscMessageToString(oscMessage); //the message and value combined
    var msgAddress = oscMessage.Address; //the message parameters
    var msgValue = oscMessage.Values[0]; //the message value
    Debug.Log(msgValue); //log the message and values coming from OSC

    //FUNCTIONS YOU WANT CALLED WHEN A SPECIFIC MESSAGE IS RECEIVED
    switch (msgAddress){
        default:
            var rotateVal = msgValue * 10;
            Rotate(rotateVal);
            ChangeTextureHeartValue (msgValue);
            break;
    }

}

    //FUNCTIONS CALLED BY MATCHING A SPECIFIC MESSAGE IN THE ALLMESSAGEHANDLER FUNCTION
    public function Rotate(msgValue) : void //rotate the cube around its axis
    {
        yRot = msgValue;
    }

    //Change Texture Heartbeat value
    public function ChangeTextureHeartValue(msgValue) : void 
    {
        textureValue = msgValue;
    }

