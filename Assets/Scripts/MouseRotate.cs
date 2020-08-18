using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotate : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 500;
    float mouseX, mouseY, xRotation;
    [SerializeField]
    GameObject camera;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.Rotate(Vector3.up * mouseX);
        camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
