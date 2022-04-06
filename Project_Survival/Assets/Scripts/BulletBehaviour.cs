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
    BossHealth _bossHp;
    SkeletonHealth _skeletonHp;
    void Start()
   {
        _playerManager = FindObjectOfType<PlayerManager>();
        _bossHp = FindObjectOfType<BossHealth>();
        _skeletonHp = FindObjectOfType<SkeletonHealth>();
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
        if (collision.gameObject.tag == "Boss")
        {
            _bossHp.bosshealth -= _bulletDamage;
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Skeleton")
        {
            _skeletonHp.skeltonhealth -= _bulletDamage;
            Destroy(gameObject);
        }
    }
}
