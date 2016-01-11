using UnityEngine;
using System.Collections;

public class ExampleMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    //Variables
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    public AudioClip footSound1;
    public AudioClip footSound2;
    public AudioClip footSound3;
    public AudioClip footSound4;
    public AudioClip footSound5;
    public AudioClip footSound6;
    public float footSoundVolume = 0.2f;
    private bool audioPlaying = false;
    private float walkTime;
    private int randTemp;
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        // is the controller on the ground?
        if (controller.isGrounded)
        {
            //Feed moveDirection with input.
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            //Multiply it by speed.
            moveDirection.z *= speed;
            moveDirection.y *= speed;
            moveDirection.x *= speed;

            //Footstep sound
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) && !audioPlaying)
            {
                walkTime = walkTime + Time.deltaTime;
                if (walkTime >= 0.7f)
                {
                    randTemp = Random.Range(1, 5);
                    if(randTemp == 1)
                    {
                        GetComponent<AudioSource>().PlayOneShot(footSound1, footSoundVolume);
                    }
                    else if (randTemp == 2)
                    {
                        GetComponent<AudioSource>().PlayOneShot(footSound2, footSoundVolume);
                    }
                    else if (randTemp == 3)
                    {
                        GetComponent<AudioSource>().PlayOneShot(footSound3, footSoundVolume);
                    }
                    else if (randTemp == 4)
                    {
                        GetComponent<AudioSource>().PlayOneShot(footSound4, footSoundVolume);
                    }
                    else if (randTemp == 5)
                    {
                        GetComponent<AudioSource>().PlayOneShot(footSound5, footSoundVolume);
                    }
                    else if (randTemp == 6)
                    {
                        GetComponent<AudioSource>().PlayOneShot(footSound6, footSoundVolume);
                    }

                    audioPlaying = true;
                    walkTime = 0.0f;
                }
            }
            else
            {
                audioPlaying = false;
            }

            //Jumping
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        //Applying gravity to the controller
        moveDirection.y -= gravity * Time.deltaTime;
        //Making the character move
        controller.Move(moveDirection * Time.deltaTime);
    }
}
