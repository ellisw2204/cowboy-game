using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 100f;
    // determines the speed of the mouse

    public Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        // the cursor cant leave the screen and click on somethign outside the gamespace by accident
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //takes the X and Y movements of the player's mouse and changes the movement of the camera accordingly
        // delta.Time is the amount of time since the update function was last called, stops the framerate affecting the mouse speed

        playerBody.Rotate(Vector3.up * mouseX);
        // this will rotate the player's body rather than just the camera when looking side to side (on the X axis)

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        // when looking up and down (y axis) the player is limited to a degree, so they dont go upside down

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);





    }
}
