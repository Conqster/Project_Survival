using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    //public float enemyHealth = 10f;
    public float bossmaxHealth;
    public float bosshealth;

    public GameObject healthBarUI;
    public Slider slider;

    private void Start()
    {
        bosshealth = bossmaxHealth;
        slider.value = CalculateBossHealth();

    }
    private void Update()
    {
        //print(bosshealth);
        slider.value = CalculateBossHealth();
        if (bosshealth < bossmaxHealth)
        {
            healthBarUI.SetActive(true);
        }
        if (bosshealth <= 0)
        {
            FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.BossDeath, transform.position, 1f);
            Destroy(gameObject);
        }
        if (bosshealth > bossmaxHealth)
        {
            bosshealth = bossmaxHealth;
        }
    }
    float CalculateBossHealth()
    {
        return bosshealth / bossmaxHealth;
    }
}

