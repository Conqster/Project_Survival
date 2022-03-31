using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] GameObject Location, Player;
    public bool InRadius;


    private void OnCollisionEnter(Collision collision)
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            print("trying to teleport");

        }
    }

    private void Update()
    {
        if(InRadius && Input.GetKey(KeyCode.Space))
        {
            Player.transform.position = Location.transform.position;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        InRadius = true;
    }

    private void OnTriggerExit(Collider other)
    {
        InRadius = false;
    }

}
