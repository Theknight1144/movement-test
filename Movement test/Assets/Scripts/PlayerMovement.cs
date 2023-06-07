using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] WallRun WallRun;

    public Transform orientation;
    [SerializeField] KeyCode JumpKey = KeyCode.Space;
    [SerializeField] Transform GroundCheckPos;
    [SerializeField] float GroundDistance = 0.2f;
    float JumpForce = 15f;

    public float PlayerHight =2f;
    public float MovemontSpeed = 6f;
    public float DashSpeed;
    public bool dashing;
    public float MovemntMultiplier = 10f;
    [SerializeField] float AirMultiplier = 0.4f;
    public float VerticalMovement;
    public float HorizontalMovement;
    public bool isGrounded;
    public Rigidbody rb;
    public float GroundDrag = 6f;
    public float AirDrag = 2f;
    public Vector3 MovementDirection;
    [SerializeField] LayerMask Groundlayer;

    RaycastHit SlopeHit;
    Vector3 SlopeMovementDirection;

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out SlopeHit, PlayerHight / 2 + 0.5f))
        {
            if(SlopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(GroundCheckPos.position, GroundDistance , Groundlayer);


        if(Input.GetKeyDown(JumpKey) && isGrounded)
        {
            Jump();
        }

        PlayerInput();
        DragControl();

        SlopeMovementDirection = Vector3.ProjectOnPlane(MovementDirection, SlopeHit.normal);
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
        }
    }

    void DragControl()
    {
        //The drag controler for when the player is grounded or in air
        if(isGrounded)
        {
            rb.drag = GroundDrag;
        }
        else
        {
            rb.drag = AirDrag;
        }

        //The drag controler for when the player is dashing
    }

    void PlayerInput()
    {
        HorizontalMovement = Input.GetAxisRaw("Horizontal");
        VerticalMovement = Input.GetAxisRaw("Vertical");

        MovementDirection = orientation.forward * VerticalMovement + orientation.right * HorizontalMovement;
    }

    void MovePlayer()
    {
        if(isGrounded && !OnSlope())
        {
            rb.AddForce(MovementDirection.normalized * MovemontSpeed * MovemntMultiplier, ForceMode.Acceleration);
        }
        else if(isGrounded && OnSlope())
        {
            rb.AddForce(SlopeMovementDirection.normalized * MovemontSpeed * MovemntMultiplier * AirMultiplier, ForceMode.Acceleration);
    
        }
        else if(!isGrounded)
        {
            rb.AddForce(MovementDirection.normalized * MovemontSpeed * MovemntMultiplier * AirMultiplier, ForceMode.Acceleration);
        };
    }

    private void StateHandeler()
    {
        //State = MovementState.Dashing;
       //MovemontSpeed = DashSpeed;
    }
}
