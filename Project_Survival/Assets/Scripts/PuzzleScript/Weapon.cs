using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    // bullet prefab 
    [SerializeField] GameObject Bullet;
    [SerializeField] Transform FireBullet;
    [SerializeField] float bulletSpeed;
    [SerializeField] bool firePressed, useGizmos;

    Animator _animator;
    int fireHash;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        fireHash = Animator.StringToHash("shooting");
    }
    private void Update()
    {
        
        //print("not happy !!!!!!!!!");
        PlayerInputs();
        if(firePressed)
        {
            Shoot();
            _animator.SetBool("shooting", true);
        }
        else
        {
            _animator.SetBool("shooting", false);
        }
        //_animator.SetBool(fireHash, firePressed);
        //print(firePressed);
    }
    void PlayerInputs()
    {
        firePressed = Input.GetButtonDown("Fire1");
    }

    private void OnDrawGizmos()
    {
        if (useGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(FireBullet.position, 0.2f);
        }
    }
    void Shoot()
    {
        GameObject newBullet = Instantiate(Bullet, FireBullet.position, Quaternion.identity);

        Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();
        bulletRb.velocity = FireBullet.transform.forward * bulletSpeed;


    }
}
