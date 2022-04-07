using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] Transform ResetPlayer;
    [SerializeField] Transform Player;

    [SerializeField] bool ignore;
    private void Start()
    {
        Player = Player.GetComponent<Transform>();
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            if(ignore)
            {
                ignore = false;
            }
            else if(!ignore)
            {
                ignore = true;
            }
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(!ignore)
        {
            if (collision.gameObject.tag == "Player")
            {
                Player.transform.position = ResetPlayer.transform.position;
            }
        }
    }
}
