﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 10, groundDistance = 0.4f, jumpHeight = 3, gravity = -19.62f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] GameObject cam, armPivot;
    [SerializeField] AudioSource footSteps;

    float horizontal, vertical;
    bool isGrounded;
    Vector3 velocity;

    Animator armPivotAnimator;

    private void Start()
    {
        armPivotAnimator = armPivot.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        WalkEffects();

    }



    void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        if (Input.GetKey(KeyCode.LeftShift))
            speed = 14;
        else
            speed = 10;

            controller.Move(move * speed * Time.deltaTime);

        Gravity();
    }


    void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Jump();

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }

    void WalkEffects()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                armPivotAnimator.speed = 1.4f;
                footSteps.pitch = 1.4f;
            }
            else
            {
                armPivotAnimator.speed = 1f;
                footSteps.pitch = 1f;
            }

            armPivotAnimator.SetTrigger("isWalking");
            if (!footSteps.isPlaying)
                footSteps.Play();
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            armPivotAnimator.ResetTrigger("isWalking");
            if (footSteps.isPlaying)
                footSteps.Stop();
        }

    }
}
