using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float xDir;
    float yDir;
    float zDir;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] bool ApplyGravity;

    Rigidbody playerRb;

    [SerializeField] bool onTheGround;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Gravity(ApplyGravity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "floor")
        {
            onTheGround = true;
        }
        
    }

    public bool Gravity(bool ApplyGravity)
    {
        ApplyGravity = true;
        
        return ApplyGravity; 
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "floor")
        {
            Gravity(ApplyGravity);
            onTheGround = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        xDir = Input.GetAxis("Horizontal");
        zDir = Input.GetAxis("Vertical");
        if(ApplyGravity)
        {
            yDir = -10;
        }
        

        if(Input.GetButtonDown("Jump") && onTheGround)
        {
            playerRb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
        
        
        print("Value on  direction: " + yDir);
    }

    private void FixedUpdate()
    {
        
        playerRb.velocity = new Vector3(xDir * moveSpeed, yDir, zDir * moveSpeed);

    }
}
