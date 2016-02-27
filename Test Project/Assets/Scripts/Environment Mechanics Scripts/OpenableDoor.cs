using UnityEngine;
using System.Collections;

public class OpenableDoor : MonoBehaviour {

    public float smooth = 2;
    public float DoorOpenAngle = 90;
    public AudioClip openSound;
    public AudioClip closeSound;
    public AudioClip lockedSound;
    public float doorVolume = 0.2f;
	
	public bool lockedDoor = false;
	public int doorNumber;
	GameObject inventory;
	InventoryScript itemScript;

    private bool open;
    private bool enter;

    private Vector3 defaultRot;
    private Vector3 openRot;

	// Use this for initialization
	void Start () {
        defaultRot = transform.eulerAngles;
        openRot = new Vector3(defaultRot.x, defaultRot.y + DoorOpenAngle, defaultRot.z);
		
		inventory = GameObject.Find("KeyInventory");
		itemScript = inventory.GetComponent<InventoryScript>();
    }
	
	// Update is called once per frame
	void Update () {
		
		// For normal doors
		if (lockedDoor == false) {
			if (open)
			{
				//Open door
				transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, openRot, Time.deltaTime * smooth);
			}
			else
			{
				//Close door
				transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, defaultRot, Time.deltaTime * smooth);
			}
			
			if (Input.GetKeyDown("f") && enter)
			{
				if(open == false)
				{
					GetComponent<AudioSource>().PlayOneShot(openSound, doorVolume);
				}
				else
				{
					GetComponent<AudioSource>().PlayOneShot(closeSound, doorVolume);
				}
				open = !open;
			}		
		} else { // for locked doors 
			if (open && itemScript.hasKey(doorNumber) == true)
			{
				//Open and unlocks door, and uses up key[doorNumber]
				transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, openRot, Time.deltaTime * smooth);
				lockedDoor = false;
				itemScript.useKey(doorNumber);
			}
            
			else
			{
				//Close door
				transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, defaultRot, Time.deltaTime * smooth);
			}
			
			if (Input.GetKeyDown("f") && enter && itemScript.hasKey(doorNumber) == true)
			{
				if(open == false)
				{
					GetComponent<AudioSource>().PlayOneShot(openSound, doorVolume);
				}

				else
				{
					GetComponent<AudioSource>().PlayOneShot(closeSound, doorVolume);
				}
				open = !open;
			}
            else if (Input.GetKeyDown("f") && enter && itemScript.hasKey(doorNumber) == false)
            {
                GetComponent<AudioSource>().PlayOneShot(lockedSound, doorVolume);
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enter = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enter = false;
        }
    }

    void OnGUI()
    {
        if (enter == true && open == true)
        {
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 170, 30), "Press 'F' to close the door");
        }
		else if (enter == true && open != true && lockedDoor == true)
        {
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 170, 30), "Press 'F' to use key to open the door");
        }
        else if (enter == true && open != true)
        {
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 170, 30), "Press 'F' to open the door");
        }
    }
}