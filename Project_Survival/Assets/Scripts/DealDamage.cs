using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [Range(1, 200)]
    [SerializeField] int dealDamage = 50;
    PlayerManager _playerManger;


    private void Start()
    {
        _playerManger = FindObjectOfType<PlayerManager>(); 
    }
    private void OnTriggerEnter(Collider other)
    {
        //print(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            //print(other.gameObject.tag);
            //print ("playertakingdamage");
            _playerManger.UpdatePlayerHealth(-dealDamage);
        }
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