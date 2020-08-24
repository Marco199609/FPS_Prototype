using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    PlayerController2 playerController;
    PlayerJump playerJump;
    PlayerMovement2 playerMovement;

    void SetVariables()
    {
        if(playerController == null)
            playerController = gameObject.GetComponent<PlayerController2>();
        if(playerJump == null)
            playerJump = gameObject.GetComponent<PlayerJump>();
        if (playerMovement = null)
            playerMovement = gameObject.GetComponent<PlayerMovement2>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void Inputs()
    {
        playerMovement.horizontal = Input.GetAxis("Horizontal");
        playerMovement.vertical = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerMovement.walking = false;
                playerMovement.running = true;
            }
            else
            {
                playerMovement.running = true;
                playerMovement.running = false;
            }
        }



        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            playerMovement.running = false;
            playerMovement.running = false;
        }


        if (Input.GetButtonDown("Jump"))
        {
            playerJump.jumping = true;
        }
    }
}
