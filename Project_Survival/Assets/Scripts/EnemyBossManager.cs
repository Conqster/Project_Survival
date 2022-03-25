using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace S5
{
       public class EnemyBossManager : MonoBehaviour
    {
        public string bossName;
        UIBossHealthBar bossHealthBar;
        EnemyAI enemyAI;

        private void Awake()
        {
            bossHealthBar = FindObjectOfType<UIBossHealthBar>();
            enemyAI = GetComponent<EnemyAI>();
        }

        private void Start()
        {
            bossHealthBar.SetBossName(bossName);
            bossHealthBar.SetBossMaxHealth(enemyAI.health);
        }
    }
}