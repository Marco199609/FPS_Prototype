using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    PlayerController playerController;
    PlayerJump playerJump;
    PlayerMovement playerMovement;
    PlayerRotation playerRotation;

    void SetVariables()
    {
        if(playerController == null)
            playerController = gameObject.GetComponent<PlayerController>();
        if(playerJump == null)
            playerJump = gameObject.GetComponent<PlayerJump>();
        if (playerMovement == null)
            playerMovement = gameObject.GetComponent<PlayerMovement>();
        if (playerRotation == null)
            playerRotation = gameObject.GetComponent<PlayerRotation>();
    }

    void Update()
    {
        SetVariables();
        Inputs();
    }



    void Inputs()
    {
        playerMovement.horizontal = Input.GetAxis("Horizontal");
        playerMovement.vertical = Input.GetAxis("Vertical");

        playerRotation.mouseX = Input.GetAxis("Mouse X") * playerController.mouseSensitivity * Time.deltaTime;
        playerRotation.mouseY = Input.GetAxis("Mouse Y") * playerController.mouseSensitivity * Time.deltaTime;


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerMovement.walking = false;
                playerMovement.running = true;
            }
            else
            {
                playerMovement.walking = true;
                playerMovement.running = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            playerMovement.walking = false;
            playerMovement.running = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            playerJump.jumping = true;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (playerController.torch.activeInHierarchy)
                playerController.torch.SetActive(false);
            else
                playerController.torch.SetActive(true);
        }
    }
}
