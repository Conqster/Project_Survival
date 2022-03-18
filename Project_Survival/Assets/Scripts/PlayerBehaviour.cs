using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //private GameBehaviour gameManager;
    public float MoveSpeed = 10f;
    public float RotateSpeed = 75f;
    public float jumpVelocity = 5f;
    public float DistanceToGround = 0.1f;
    public float BulletSpeed = 100f; // this variable stores the speed 

    public LayerMask GroundLayer;
    
    public bool isJumping;
    private bool isShooting; // a boolean to check if our player to be shooting 

    private float vInput;
    private float hInput;

    public GameObject Bullet; //this is used to store the prefab of bullet 

    private Rigidbody rb;
    private CapsuleCollider col;

    void Start()
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameBehaviour>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        //HP = GameObject.Find("Enemy");
        
    }
   

    void Update()
    {
        vInput = Input.GetAxis("Vertical") * MoveSpeed;
        hInput = Input.GetAxis("Horizontal") * RotateSpeed;

        isJumping |= Input.GetKeyDown(KeyCode.Space);
        isJumping |= Input.GetButtonDown("Jump");
        //isShooting |= Input.GetMouseButtonDown(0);  // using the OR logical operator, which returns true if weare pushing the specified button, just like with Input.GetKeyDown
        //isShooting |= Input.GetKeyDown(KeyCode.LeftControl); 
        isShooting |= Input.GetButtonDown("Fire1");
           // GetMouseButtonDown() takes an int parameter to determine which mouse button we want to check for; 0 is the left button, 1 is the right button, and 
           // and 2 is the middle button or scroll wheel. 
        /*
        this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);
        this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);
        */
    }

    void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * hInput;

        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);

        rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);

        rb.MoveRotation(rb.rotation * angleRot); 

        if(IsGrounded() && isJumping)
        {
            rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }
        isJumping = false;

        if(isShooting)  //to check if player is suppose to be shooting using the isShooting input check variable 
        {
            GameObject newBullet = Instantiate(Bullet,this.transform.position + new Vector3(0.1f, 0, 0), this.transform.rotation);
            // creates a local GameObject variable everytime th eleft mouse button is pressed 
            // Instantiate() method is used to assign a GameObject to newBullet by pressing in  the Bullect Prefab,
            // we also used the player capsule position to place the new bullet in front of the player to avoid any collidions 

            Rigidbody BulletRB = newBullet.GetComponent<Rigidbody>();
            // called GetComponent() to return and store the Rigidbody component on newBullet

            BulletRB.velocity = this.transform.forward * BulletSpeed;
            // set the velocity property of the rigidbody componenet to the players transform.forward direction multiplied by BulletSpeed 
            // changing the velocity instead of using AddForce() ensures that gravity doesnt pull our bullets down in arc when fired.
        }

        isShooting = false;
    }

    /*void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Enemy")
        {
            gameManager.HP -= 1;
        }
    }*/

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z);

        bool grounded = Physics.CheckCapsule(col.bounds.center, capsuleBottom, DistanceToGround, GroundLayer, QueryTriggerInteraction.Ignore);

        return grounded;
    }
}
