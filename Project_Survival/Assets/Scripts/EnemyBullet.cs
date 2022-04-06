using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    PlayerManager player;
    [SerializeField] float OnScreenDelay = 3f;

    void Start()
    {
        Destroy(gameObject, OnScreenDelay);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerTakingDamage();
        }
    }

    private void PlayerTakingDamage()
    {
        player.UpdatePlayerHealth(-50);
    }
}
