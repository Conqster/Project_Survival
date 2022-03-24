using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    // jumping has been implemented yet!!!!
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpSpeed = 0.5f;
    [SerializeField] private float gravity = 2f;
    [SerializeField] private float turnSpeed = 3f;
    [SerializeField] float dashAbility = 5f;


    //CharacterController _cc;
    Vector3 moveDir;

    //NEW ref Player rigiodbody 
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void Awake()
    {
        //_cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Mouse X");
        transform.Rotate(horizontal * turnSpeed * Vector3.up, Space.World);
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(h, 0, v);
        Vector3 transformDirection = transform.TransformDirection(inputDirection);

        Vector3 flatMovement = moveSpeed * Time.deltaTime * transformDirection;

        moveDir = new Vector3(flatMovement.x, moveDir.y, flatMovement.z);
        rb.MovePosition(transform.position + moveDir);

        //trying to implement dashing 
        //!!!working progress
        if (Input.GetButton("Fire2"))
        {
            DashForward();
            print("lets go");
        }

        //private int currentJump = 0;
        //if (PlayerJumped)
        //    moveDir.y = jumpSpeed;
        //else if (_cc.isGrounded)
        //    moveDir.y = 0f;
        //else
        //    moveDir.y -= gravity * Time.deltaTime;

        //_cc.Move(moveDir);
        //NEW to move the player using move position
        //rb.MovePosition(flatMovement);
    }

    //private bool PlayerJumped => _cc.isGrounded && Input.GetButton("Fire2");

    //New method for Dash Ability 
    void DashForward()
    {
        //rb.AddForce(transform.forward * dashAbility);
        //rb.AddForce(Vector3.forward * dashAbility);
        float dashDistance = 5f;
        transform.position += transform.forward * dashDistance;
        // to prevent dashing through walls need to do a gameObject check
    }

}
