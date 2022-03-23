using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace S5
{
    public class UIBossHealthBar : MonoBehaviour
    {
        public Text bossName;
        Slider slider;

        private void Awake()
        {
            slider = GetComponentInChildren<Slider>();
            bossName = GetComponentInChildren<Text>();
        }

        private void Start()
        {
            slider.gameObject.SetActive(false);
        }

        public void SetBossName(string name)
        {
            bossName.text = name;
        }

        public void SetUIHealthBarToActive()
        {
            slider.gameObject.SetActive(true);
        }

        public void SetHeathToInActive()
        {
            slider.gameObject.SetActive(false);
        }

        public void SetBossMaxHealth(int maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }

        public void SetBossCurrentHealth(int currentHealth)
        {
            slider.value = currentHealth;
        }

    }
}