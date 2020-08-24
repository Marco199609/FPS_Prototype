using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    PlayerController2 playerController;
    float mouseSensitivity;
    GameObject playerCamera;
    float mouseX, mouseY, xRotation;

    void SetVariables()
    {
        if (playerController == null)
            playerController = gameObject.GetComponent<PlayerController2>();
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

    void FixedUpdate()
    {
        SetVariables();
        MouseRotate();
    }

    void MouseRotate()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80, 80);

        transform.Rotate(Vector3.up * mouseX);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
