using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
   
    //public float enemyHealth = 10f;
    public float maxHealth;
    public float health;

    public GameObject healthBarUI;
    public Slider slider;

    private void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealth();

    }
    private void Update()
    {
        //print(health);
        slider.value = CalculateHealth();
        if(health < maxHealth)
        {
            healthBarUI.SetActive(true);
        }
        if (health <= 0)
        {
            FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.EnemyDeath, transform.position, 1f);
            Destroy(gameObject);
        }
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }
    float CalculateHealth()
    {
        return health / maxHealth;
    }
}
    /*public void HealthDamage(float healthDamange)
    {
        enemyHealth -= healthDamange;

        if (enemyHealth <= 0)
        {
            EnemyDeath();
        }
    }

    void EnemyDeath()
    {
        Destroy(gameObject);
    }*/



// code for player attack

/*
 * RaycastHit hit;
 * [SerializeField]
 * float damageEnemy = 1f;
 * 
 * EnemyHealth enemyHealthScript = hit.transform.GetComponent<EnemyHealth>();
 * enemyHealthScript.HealthDamage(damageEnemy);
 */ 