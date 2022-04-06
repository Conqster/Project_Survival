using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonHealth : MonoBehaviour
{
    //public float enemyHealth = 10f;
    public float skeletonmaxHealth;
    public float skeltonhealth;

    public GameObject healthBarUI;
    public Slider slider;

    private void Start()
    {
        skeltonhealth = skeletonmaxHealth;
        slider.value = CalculateSkeletonHealth();

    }
    private void Update()
    {
        //print(skeltonhealth);
        slider.value = CalculateSkeletonHealth();
        if (skeltonhealth < skeletonmaxHealth)
        {
            healthBarUI.SetActive(true);
        }
        if (skeltonhealth <= 0)
        {
            FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.SkeletonDeath, transform.position, 1f);
            Destroy(gameObject);
        }
        if (skeltonhealth > skeletonmaxHealth)
        {
            skeltonhealth = skeletonmaxHealth;
        }
    }
    float CalculateSkeletonHealth()
    {
        return skeltonhealth / skeletonmaxHealth;
    }
}
