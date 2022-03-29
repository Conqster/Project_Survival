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

    float distToGround;


    //CharacterController _cc;
    Vector3 moveDir;

    //NEW ref Player rigiodbody 
    Rigidbody rb;
    CapsuleCollider col;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        distToGround = col.bounds.extents.y;
    }


    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f);
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

        WantToJump();
    }

    void WantToJump()
    {
        if ((Input.GetButton("Fire2")) && isGrounded())
        {
            print("want to jump");
            rb.AddForce(transform.up * jumpForce);
        }
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.cyan;
    //    Gizmos.DrawWireSphere(new Vector3(0,transform.position.y - distToGround), 0.5f);
    //}
}
