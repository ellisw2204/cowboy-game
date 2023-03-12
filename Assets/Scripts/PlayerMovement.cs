using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float crouchingHeight;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    // when nearing the ground velocity will decrease to give a more natural fall

    Vector3 velocity;
    bool isGrounded;

    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //the game will determine if we are on the ground(is grounded) based on the physics check, wherein a shere is made at the bottom,
        //of the object to check the distance, using the ground mask. If a collision is detected, "isGrounded" becomes true.

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        // this assigns a name to the different axis of movement the player can take

        Vector3 move = transform.right * x + transform.forward * z;
        // the player will now move locally based on the direction that the player is faced rather than in relation to the world#

        controller.Move(move * speed * Time.deltaTime);
        // once again deltaTime adds independant framerate (same reason applies to all future use of this variable)

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(Input.GetButtonDown("Jump") & isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            speed = 8f;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            speed = +18f;
        }
        // the player will slow down when crouching, then revert back to walking speed when the key is released.


    }
}
