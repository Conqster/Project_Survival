using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
   [Range(0,10)]
   [SerializeField] float OnScreenDelay = 3f;

    int _bulletDamage;
    PlayerManager _playerManager;
    EnemyHealth _enemyHp;
    void Start()
   {
        _playerManager = FindObjectOfType<PlayerManager>();
        _enemyHp = FindObjectOfType<EnemyHealth>();
        _bulletDamage = _playerManager.bulletDamage;
       Destroy(gameObject, OnScreenDelay);

        MeshRenderer bulletMr = GetComponent<MeshRenderer>();
        if (_bulletDamage < 20)
        {
            bulletMr.material.SetColor("_Color", Color.green);
        }
        else
        {
            bulletMr.material.SetColor("_Color", Color.red);
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Enemy")
        {
            _enemyHp.health -= _bulletDamage;
            Destroy(gameObject);
        }
    }
}
