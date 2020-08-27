using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerController playerController;
    CharacterController characterController;
    Animator walkAnimator;
    AudioSource footSteps;
    float speed;
    public float horizontal, vertical;      //Called in player input
    public Vector3 velocity;
    public bool walking, running;

    void SetVariables()
    {
        if (playerController == null)
            playerController = gameObject.GetComponent<PlayerController>();
        if (characterController == null)
            characterController = playerController.characterController;
        if (speed == 0)
            speed = playerController.speed;
        if(footSteps == null)
            footSteps = playerController.footSteps;

        if (walkAnimator == null)
            walkAnimator = playerController.walkAnimator;
    }


    void FixedUpdate()
    {
        SetVariables();
        Move();
        AnimationControl();
    }


    void Move()
    {
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        move = Vector3.ClampMagnitude(move, 1);                                         //Does not let diagonal movement to be faster

        if (running)
        {
            if (playerController.playerHealth < 50)
                speed = 6;
            else
                speed = 20;
        }
        else
        {
            if (playerController.playerHealth < 50)
                speed = 3;
            else
                speed = 10;
        }

        characterController.Move(move * speed * Time.deltaTime);
    }




    void AnimationControl()
    {
        if(running || walking)
        {
            if(playerController.playerHealth < 50)
            {
                walkAnimator.speed = 0.666f;
                footSteps.pitch = 0.666f;
            }
            else
            {
                if(running)
                {
                    walkAnimator.speed = 2f;
                    footSteps.pitch = 2f;
                }
                else if(walking)
                {
                    walkAnimator.speed = 1f;
                    footSteps.pitch = 1f;
                }
            }



            walkAnimator.SetBool("isWalking", true);
            if (!footSteps.isPlaying)
                footSteps.Play();
        }
        else if (!running && !walking)
        {
            walkAnimator.SetBool("isWalking", false);
            if (footSteps.isPlaying)
                footSteps.Stop();
        }
    }
}
