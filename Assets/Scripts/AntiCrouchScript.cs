using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiCrouchScript : MonoBehaviour
{
    private Vector3 gunScale = new Vector3(1, 0.5f, 1);


    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.localScale = gunScale;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        }
        // when crouch key is held down, the y value of the player is reduced by 0.5. the scale of the character controller is also
        // defined as the base size of the player.


        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale = gunScale;
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        }
        // when crouch key is released down, the y value of the player is reduced by 0.5.

    }
}

