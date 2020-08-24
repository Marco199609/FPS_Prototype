using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
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


    [Header("To be used in Player Health")]
    public GameObject[] bloodSprites;
    public AudioSource heartBeat;
    public Text healthText;
    public float playerHealth = 100;

    public GameObject torch;




    PlayerRotation playerRotation;
    PlayerMovement playerMovement;
    PlayerInput playerInput;
    PlayerJump playerJump;
    PlayerHealth playerHealthControl;
    PlayerHUD playerHUD;



    void Start()
    {
        playerRotation = gameObject.AddComponent<PlayerRotation>();
        playerMovement = gameObject.AddComponent<PlayerMovement>();
        playerInput = gameObject.AddComponent<PlayerInput>();
        playerJump = gameObject.AddComponent<PlayerJump>();
        playerHealthControl = gameObject.AddComponent<PlayerHealth>();
        playerHUD = gameObject.AddComponent<PlayerHUD>();
    }
}
