using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    PlayerManager player;

    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.tag == "EnemyAttack")
        {
            PlayerTakingDamage();
            print ("playertakingdamage");
        } 
    }

    private void PlayerTakingDamage()
    {
        player.UpdatePlayerHealth(-50);
    }
    /*public void TakeDamage()
    {
        if (PlayerHp <= 0) DestroyPlayer();// players hp when being damage
    }
    public void DestroyPlayer()
    {
        {
            Destroy(gameObject);//players death
        }   
    }
    private void BossHp()//for the boss to take damage when its being hit by player
    {
        boss.TakeDamage();
        print ("damagetaken");
    }*/
}