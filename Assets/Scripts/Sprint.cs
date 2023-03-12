using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : MonoBehaviour
{
    PlayerMovement basicMovementScript;
    public float sprintSpeed = 50f;

    void Start()
    {
        basicMovementScript = GetComponent<PlayerMovement>();
        //for the rest of this script, we can take variables(?) from inside my other PlayerMovement script.
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
            basicMovementScript.speed += sprintSpeed;

        else if(Input.GetKeyUp(KeyCode.LeftShift))
            basicMovementScript.speed -= sprintSpeed;
        //when the shift key is held down the player's speed will be taht of the regular speed + whatever SprintSpeed is set to.

    }
}
