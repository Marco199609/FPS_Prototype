using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    PlayerController2 playerController;
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
            playerController = gameObject.GetComponent<PlayerController2>();
        if (characterController == null)
            characterController = playerController.characterController;
        if (speed == 0)
            speed = playerController.speed;


        if (walkAnimator == null)
            walkAnimator = playerController.walkAnimator;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (gameObject.GetComponent<PlayerController>().playerHealth < 9)
                speed = 6;
            else
                speed = 20;
        }
        else
        {
            if (gameObject.GetComponent<PlayerController>().playerHealth < 9)
                speed = 3;
            else
                speed = 10;
        }

        characterController.Move(move * speed * Time.deltaTime);

        //Gravity();
    }




    void AnimationControl()
    {
        if(running || walking)
        {
            if (running)
            {
                if (playerController.playerHealth < 9) { }
                else
                {
                    walkAnimator.speed = 2f;
                    footSteps.pitch = 2f;
                }
            }
            else if (walking)
            {
                if (playerController.playerHealth < 9)
                {
                    walkAnimator.speed = 0.666f;
                    footSteps.pitch = 0.666f;
                }
                else
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
