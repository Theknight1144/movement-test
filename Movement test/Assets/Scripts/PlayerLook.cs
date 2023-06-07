using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    [SerializeField] WallRun WallRun;
    [SerializeField] private float MouseSensX;
    [SerializeField] private float MouseSensY;

    [SerializeField] Transform orientation = null;

    [SerializeField] Transform Cam;

    float MouseX;
    float MouseY;

    public float Multiplier = 0.001f;

    float xRotation;
    float yRotation;

    void Start()
    {
        Cursor.lockState =CursorLockMode.Locked;
    }

    void Update()
    {
        MouseX = Input.GetAxisRaw("Mouse X");
        MouseY = Input.GetAxisRaw("Mouse Y");
         
        yRotation += MouseX * MouseSensX * Multiplier;
        xRotation -= MouseY * MouseSensY * Multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        Cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, WallRun.tilt);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
