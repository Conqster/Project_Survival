using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    // jumping has been implemented yet!!!!
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float turnSpeed = 3f;
    //player inputs
    float mouseHorizontal, inputH, inputV, yVel, radius;
    // to reset player back to position when game loads B is the Hotkey
    public bool ResetPlayerToStart = false;
    Vector3 inputDirection, PlayerStartPoint, offsetPoint, pos;

    [SerializeField] LayerMask groundLayers, testingLayer;


     public bool jumpPressed, jumping, firePressed;

    //Animator variables
    [SerializeField]
    bool useAnimator, useGizmos, isGrounded;
    Animator animator;
    int walkHash, stepBackHash, stepRtHash, stepLtHash, jumpHash;



    //CharacterController _cc;
    Vector3 moveDir, groundCheck, offset;

    //NEW ref Player rigiodbody 
    Rigidbody rb;
    //CapsuleCollider col;
    CapsuleCollider col;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        //distToGround = col.bounds.extents.y;
        PlayerStartPoint = transform.position;
        //converting bool to int, to avoid executing bool multiple time to increse performance
        walkHash = Animator.StringToHash("isWalking");
        stepBackHash = Animator.StringToHash("stepsBack");
        stepRtHash = Animator.StringToHash("stepsRight");
        stepLtHash = Animator.StringToHash("stepsLeft");
        jumpHash = Animator.StringToHash("jump");
    }

    private void Update()
    {
        PlayerInputs();
        //CalculateScales();
        CharacterMovement();
        BackToStart();
        ControlAnimation();

        //print(jumping);
        //offset = new Vector3(0, pointOffset, 0);
        //RaycastHit hit;
        //Physics.Raycast(transform.position, Vector3.down, out hit, 100000f);
        //Debug.DrawRay(transform.position, Vector3.down, Color.red);
        //print(hit.distance);

        
    }

    //void CalculateScales()
    //{
    //    radius = col.radius * groundCheckRadius;

    //    pos = transform.position + Vector3.up * (radius * groundCheckRadius);
    //}
    void BackToStart()
    {
        if(ResetPlayerToStart && Input.GetKeyDown(KeyCode.B))
        {
            transform.position = PlayerStartPoint;
        }
    }


    //bool isGrounded()
    //{
    //    return Physics.CheckSphere(pos, radius, groundLayers);
    //}

    private void OnCollisionEnter(Collision collision)
    {
        //collision.gameObject.layer
        if(collision.gameObject.layer == 6)
        {

            //print(collision.gameObject.layer);
            isGrounded = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            //print(collision.gameObject.layer);
            isGrounded = false;
        }
    }
    private void OnDrawGizmos()
    {
        //col = GetComponent<CapsuleCollider>();
        if (useGizmos)
        {
            Gizmos.color = Color.red;
            //Gizmos.DrawWireSphere(transform.position, distToGround);
            Gizmos.DrawWireSphere(pos, radius);
        }
    }
    void PlayerInputs()
    {
        jumpPressed = Input.GetButtonDown("Jump");
        mouseHorizontal = Input.GetAxis("Mouse X");
        inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    void CharacterMovement()
    {
        transform.Rotate(mouseHorizontal * turnSpeed * Vector3.up, Space.World);

        Vector3 transformDirection = transform.TransformDirection(inputDirection);

        Vector3 flatMovement = moveSpeed * Time.deltaTime * transformDirection;

        moveDir = new Vector3(flatMovement.x, yVel, flatMovement.z);

        //rb.MovePosition(transform.position + moveDir);

        //if(isGrounded && yVel <= 0)
        //{
        //    //rb.velocity = rb.useGravity * Time.deltaTime;
        //    yVel = 0f;
        //}
        //else
        //{
        //    yVel -= gravity * Time.deltaTime;
        //}
        if(jumpPressed)
        {
            jumpPressed = true;

            if(isGrounded)
            {
                FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.Jump, transform.position, 1f);
                jumping = true;
                //rb.AddForce(transform.up * jumpForce);
                rb.velocity = new Vector3(0, jumpSpeed, 0);
            }
        }
        
        rb.MovePosition(transform.position + moveDir);

        if(isGrounded && !jumpPressed && jumping)
        {
            jumping = false;
        }
    }

    void ControlAnimation()
    {
        //print(inputDirection.x);
        if (useAnimator)
        {
            bool moveForward = (inputDirection.z > 0);
            bool moveBack = (inputDirection.z < 0);
            bool stepRight = (inputDirection.x > 0);
            bool stepLeft = (inputDirection.x < 0);
            //print(stepLeft);
            if (moveForward)
            {   
                animator.SetBool(walkHash, true);
            }
            if (!moveForward)
            {
                animator.SetBool(walkHash, false);
            }
            if (moveBack)
            {
                animator.SetBool(stepBackHash, true);
            }
            if (!moveBack)
            {
                animator.SetBool(stepBackHash, false);
            }
            if (stepRight)
            {
                animator.SetBool(stepRtHash, true);
            }
            if (!stepRight)
            {
                animator.SetBool(stepRtHash, false);
            }
            if (stepLeft)
            {
                animator.SetBool(stepLtHash, true);
            }
            if (!stepLeft)
            {
                animator.SetBool(stepLtHash, false);
            }
            animator.SetBool(jumpHash, jumping);
            //animation.SetBool(walkHash, walking);
            //animation.SetBool(stepBackHash, backStepping);
            //animation.SetBool(stepRtHash, steppingRight);
            //animation.SetBool(stepLtHash, steppingLeft);
        }
    }


    //private void FixedUpdate()
    //{
    //    //might change this to .velocity
    //    //rb.MovePosition(transform.position + moveDir);
    //    rb.MovePosition(transform.position + moveDir);
    //    //rb.velocity = moveDir;
    //    //rb.velocity = moveVector
    //}
}
