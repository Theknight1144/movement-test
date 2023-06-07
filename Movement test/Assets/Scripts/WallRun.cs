using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{

    [Header("Camera")]
    [SerializeField] private Camera Cam;
    [SerializeField] private float Fov;
    [SerializeField] private float WallRunFov;
    [SerializeField] private float WallRunFovTime;
    [SerializeField] private float CamTilt;
    [SerializeField] private float CamTiltTime;

    public float tilt { get; private set; }

    [Header("Wall Running")]
    [SerializeField] private float WallRunGravity;
    [SerializeField] private float WallRunJumpForce;

    [Header("Detection")]
    [SerializeField] float WallDistance;
    [SerializeField] float MinimumJumpHeight = 1.5f;
    
    [Header("Movement")]
    [SerializeField] Transform oriorientation;



    bool WallLeft;
    bool WallRight;

    RaycastHit RightWallHit;
    RaycastHit LeftWallHit;

    private Rigidbody rb;

    bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, UnityEngine.Vector3.down, MinimumJumpHeight);
    }

    void CheckWall()
    {
        WallLeft = Physics.Raycast(transform.position, -oriorientation.right, out LeftWallHit, WallDistance);
        WallRight = Physics.Raycast(transform.position, oriorientation.right, out RightWallHit, WallDistance);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckWall();

        if(CanWallRun())
        {
            if(WallLeft)
            {
                StartWallRun();
            }
            else if(WallRight)
            {
                StartWallRun();
            }
            else
            {
                StopWallRun();
            }
        }
        else
        {
            StopWallRun();
        }
    }

    void StartWallRun()
    {
        rb.useGravity = false;

        rb.AddForce(UnityEngine.Vector3.down * WallRunGravity, ForceMode.Force);

        Cam.fieldOfView = Mathf.Lerp(Cam.fieldOfView, WallRunFov, WallRunFovTime * Time.deltaTime);

        if (WallLeft)
        {
            tilt = Mathf.Lerp(tilt, -CamTilt, CamTiltTime * Time.deltaTime);
        }
        else if (WallRight)
        {
            tilt = Mathf.Lerp(tilt, CamTilt, CamTiltTime * Time.deltaTime);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (WallLeft)
            {
                UnityEngine.Vector3 wallRunJumpDirection = transform.up + LeftWallHit.normal;
                rb.velocity = new UnityEngine.Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * WallRunJumpForce * 100, ForceMode.Force);
            }
            else if (WallRight)
            {
                UnityEngine.Vector3 wallRunJumpDirection = transform.up + RightWallHit.normal;
                rb.velocity = new UnityEngine.Vector3(rb.velocity.x, 0, rb.velocity.z); 
                rb.AddForce(wallRunJumpDirection * WallRunJumpForce * 100, ForceMode.Force);
            }
        }
    }

    void StopWallRun()
    {
        rb.useGravity = true;

        Cam.fieldOfView = Mathf.Lerp(Cam.fieldOfView, Fov, WallRunFovTime * Time.deltaTime);
        tilt = Mathf.Lerp(tilt, 0, CamTiltTime * Time.deltaTime);
    }
}
