using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    // jumping has been implemented yet!!!!
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 0.5f;
    [SerializeField] private float gravity = 2f;
    [SerializeField] private float turnSpeed = 3f;
    //player inputs
    float inputH, inputV;
    // to reset player back to position when game loads B is the Hotkey
    public bool ResetPlayerToStart = false;
    Vector3 PlayerStartPoint, offsetPoint;

    float distToGround;
    [Range(-1, 1)]
    [SerializeField] float offsetDist;



    //Animator variables
    [SerializeField]
    bool useAnimator, useGizmos;
    Animator animator;
    public bool jumping = false;
    int walkHash, stepBackHash, stepRtHash, stepLtHash, jumpHash;



    //CharacterController _cc;
    Vector3 moveDir, ground;

    //NEW ref Player rigiodbody 
    Rigidbody rb;
    CapsuleCollider col;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        //distToGround = col.bounds.extents.y;
        distToGround = col.height / 2;
        PlayerStartPoint = transform.position;
        //converting bool to int, to avoid executing bool multiple time to increse performance
        walkHash = Animator.StringToHash("isWalking");
        stepBackHash = Animator.StringToHash("stepsBack");
        stepRtHash = Animator.StringToHash("stepsRight");
        stepLtHash = Animator.StringToHash("stepsLeft");
        jumpHash = Animator.StringToHash("jump");
    }


    void BackToStart()
    {
        if(ResetPlayerToStart && Input.GetKeyDown(KeyCode.B))
        {
            transform.position = PlayerStartPoint;
        }
    }
    bool isGrounded()
    {
        return Physics.Raycast(transform.position + offsetPoint, Vector3.down, distToGround);
    }


    private void Update()
    {
        BackToStart();
        float horizontal = Input.GetAxis("Mouse X");
        transform.Rotate(horizontal * turnSpeed * Vector3.up, Space.World);

        if(isGrounded())
        {
            jumping = false;
        }
        offsetPoint = new Vector3(0, offsetDist);
        // animation
        if(useAnimator)
        {
            bool moveForward = (inputV > 0);
            bool moveBack = (inputV < 0);
            bool stepRight = (inputH > 0);
            bool stepLeft = (inputH < 0);
            //print(stepLeft);
            if(moveForward && isGrounded())
            {
                animator.SetBool(walkHash, true);
            }
            if(!moveForward || !isGrounded())
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
            if (jumping)
            {
                animator.SetBool(jumpHash, true);
            }
            if (!jumping)
            {
                animator.SetBool(jumpHash, false);
            }
            print(isGrounded());
        }
    }

    private void FixedUpdate()
    {
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(inputH, 0, inputV);
        Vector3 transformDirection = transform.TransformDirection(inputDirection);

        Vector3 flatMovement = moveSpeed * Time.deltaTime * transformDirection;

        moveDir = new Vector3(flatMovement.x, moveDir.y, flatMovement.z);
        rb.MovePosition(transform.position + moveDir);

        WantToJump();
    }

    void WantToJump()
    {
        if ((Input.GetButton("Fire2")) && isGrounded())
        {
            //print("want to jump");
            rb.AddForce(transform.up * jumpForce);
            jumping = true;
            //Invoke("StopJump", 1);
        }
        //jumping = false;
    }

    void StopJump()
    {
        jumping = false;
    }
    private void OnDrawGizmos()
    {
        col = GetComponent<CapsuleCollider>();
        if (useGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + offsetPoint, distToGround);
        }
    }
}
