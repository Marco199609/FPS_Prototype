using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    PlayerController2 playerController;
    PlayerMovement2 playerMovement;
    Transform groundCheck;
    LayerMask groundMask;
    CharacterController characterController;
    float groundDistance, jumpHeight, gravity;
    bool isGrounded;
    public bool jumping;
    Vector3 velocity;


    void SetVariables()
    {
        if (playerController == null)
            playerController = gameObject.GetComponent<PlayerController2>();
        if (playerMovement == null)
            playerMovement = gameObject.GetComponent<PlayerMovement2>();
        if (groundCheck == null)
            groundCheck = playerController.groundCheck;
        if (groundMask != playerController.groundMask)
            groundMask = playerController.groundMask;
        if (groundDistance == 0)
            groundDistance = playerController.groundDistance;
        if (jumpHeight == 0)
            jumpHeight = playerController.jumpHeight;
        if (gravity == 0)
            gravity = playerController.gravity;
        if (characterController == null)
            characterController = playerController.characterController;

        velocity = playerMovement.velocity;
    }


    void FixedUpdate()
    {
        SetVariables();
        Gravity();
        Jump();
    }


    void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            playerMovement.velocity.y = -2f;
        }

        Jump();

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    void Jump()
    {
        if (jumping && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            jumping = false;
        }
    }
}
