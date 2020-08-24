using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    PlayerController playerController;
    float mouseSensitivity;
    GameObject playerCamera;
    public float mouseX, mouseY;
    float xRotation;

    void SetVariables()
    {
        if (playerController == null)
            playerController = gameObject.GetComponent<PlayerController>();
        if(mouseSensitivity == 0)
            mouseSensitivity = playerController.mouseSensitivity;
        if (playerCamera == null)
            playerCamera = playerController.playerCamera;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        SetVariables();
    }

    void Update()
    {
        MouseRotate();
    }

    void MouseRotate()
    {
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80, 80);

        transform.Rotate(Vector3.up * mouseX);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
