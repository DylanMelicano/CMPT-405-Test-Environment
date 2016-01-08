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
    public AudioClip footSound;
    private bool audioPlaying = false;
    private float walkTime;

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
                if (walkTime >= 0.75f)
                {
                    GetComponent<AudioSource>().PlayOneShot(footSound, 0.20f);
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
