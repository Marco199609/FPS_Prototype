using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController2 : MonoBehaviour
{
    [Header("To be used in Player Rotation")]
    public float mouseSensitivity = 500;
    public GameObject playerCamera;

    [Header("To be used in Player Movement")]
    public CharacterController characterController;
    public float speed = 10, groundDistance = 0.4f, jumpHeight = 3, gravity = -19.62f;
    public Transform groundCheck;
    public LayerMask groundMask;
    public Animator walkAnimator;
    public AudioSource footSteps;


    public GameObject torch;
    public GameObject[] bloodSprites;
    public float bloodTransparencyIndex = 1;
    public AudioSource heartBeat;
    public Text healthText;
    public bool transparentBlood;
    public float playerHealth = 18;




    PlayerRotation playerRotation;
    PlayerMovement2 playerMovement;
    PlayerInput playerInput;
    PlayerJump playerJump;



    void Start()
    {
        playerRotation = gameObject.AddComponent<PlayerRotation>();
        playerMovement = gameObject.AddComponent<PlayerMovement2>();
        playerInput = gameObject.AddComponent<PlayerInput>();
        playerJump = gameObject.AddComponent<PlayerJump>();
    }
}
