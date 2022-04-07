using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] Transform ResetPlayer;
    [SerializeField] Transform Player;

    private void Start()
    {
        Player = Player.GetComponent<Transform>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player.transform.position = ResetPlayer.transform.position;
        }
    }
}
