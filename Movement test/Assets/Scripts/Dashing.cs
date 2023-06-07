using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    [Header("References")]
    public Transform Orientation;
    public Transform Cam;
    private Rigidbody rb;
    private PlayerMovement pm;

    [Header("Dashing")]
    public float DashForce;
    public float DashUpWardsForce;
    public float DashDuraion;

    [Header("Cooldown")]
    public float DashCooldown;
    public float DashCooldownTimer;

    [Header("Input")]
    public KeyCode DashKeyBind = KeyCode.LeftShift;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }


    void Update()
    {
        if (Input.GetKeyDown(DashKeyBind))
        {
            Dash();
        }
    }

    private void Dash()
    {
        pm.dashing = true;
        
        Vector3 ForceToAplly = Orientation.forward * DashForce + Orientation.up * DashUpWardsForce;

        rb.AddForce(ForceToAplly, ForceMode.Impulse);

        Invoke(nameof(ResetDash), DashDuraion);
    }

    private void ResetDash()
    {
        
    }
}
