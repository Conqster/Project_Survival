using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public float RotateSpeed = 75f;
    public float jumpForce = 10f;

    public static int maxJump = 2;
    //public int currentJump;

    int currentJump = 0;
    int ability;
    Inventory inventory;

    private float vInput;
    private float hInput;

    public bool onTheGround;
    public bool isJumping;

    private Rigidbody playerRb;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        inventory = GetComponent<Inventory>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "floor")
        {
            onTheGround = true;
            currentJump = 0;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "floor")
        {
           // Gravity(ApplyGravity);
            onTheGround = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        vInput = Input.GetAxis("Vertical") * MoveSpeed;
        hInput = Input.GetAxis("Horizontal") * RotateSpeed;

        this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);
        this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);

    
        ability = inventory.MyAbility();
        

        if (Input.GetKeyDown(KeyCode.Space) && (onTheGround || (maxJump > currentJump && ability !< 25)))
        {
            playerRb.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
            currentJump++;
            ability -= 25;
            Debug.LogFormat("My current jump is {0} and my ability is at {1}%", currentJump, ability);
        }
    }

    private void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * hInput;

        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);

        playerRb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);

        playerRb.MoveRotation(playerRb.rotation * angleRot);
    }
}
