using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class SpawnEnemies : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] int numberOfEnemies = 1;
    [Range(1, 2)]
    [SerializeField] int enemiesPerSpawn = 1;

    [SerializeField] GameObject Skeleton, Zombie, Boss;
    [SerializeField] bool spawnSkeleton, spawnZombie, spawnBoss;
    [SerializeField] Transform SpawnPoint1, SpawnPoint2;

    bool time = false;
    float timer = 1f;
    enum EnemiesGroup { Skeleton, Zombies, Boss};
    EnemiesGroup enemiesGroupToSpawn;

    private void Update()
    {
        //GameObject _skeleton = Instantiate(Skeleton);
        List<EnemiesGroup> possibleEnemy = new List<EnemiesGroup>();

        if (spawnSkeleton)
        {
            possibleEnemy.Add(EnemiesGroup.Skeleton);
        }
        if (spawnZombie)
        {
            possibleEnemy.Add(EnemiesGroup.Zombies);
        }
        if (spawnBoss)
        {
            possibleEnemy.Add(EnemiesGroup.Boss);
        }

        int chooseRnd = Random.Range(0, possibleEnemy.Count);
        enemiesGroupToSpawn = possibleEnemy[chooseRnd];

        if(time)
        {
            timer -= Time.deltaTime;
        }

        if(timer == 0)
        {
            Spawn();
            timer = 1f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            time = true;
        }
    }
    void Spawn()
    {
        List<EnemiesGroup> possibleEnemy = new List<EnemiesGroup>();

        if (spawnSkeleton)
        {
            possibleEnemy.Add(EnemiesGroup.Skeleton);
        }
        if (spawnZombie)
        {
            possibleEnemy.Add(EnemiesGroup.Zombies);
        }
        if (spawnBoss)
        {
            possibleEnemy.Add(EnemiesGroup.Boss);
        }

        int chooseRnd = Random.Range(0, possibleEnemy.Count);
        enemiesGroupToSpawn = possibleEnemy[chooseRnd];
        for (int i = 0; i < numberOfEnemies; i++)
        {
            for (int j = 0; j < enemiesPerSpawn; j++)
            {
                switch (enemiesGroupToSpawn)
                {
                    case EnemiesGroup.Skeleton:
                        SpawnSkeleton();
                        break;
                    case EnemiesGroup.Zombies:
                        SpawnZombie();
                        break;
                    case EnemiesGroup.Boss:
                        SpawnBoss();
                        break;
                }
            }
        }
    }

    void SpawnSkeleton()
    {
        if(enemiesPerSpawn <= 1)
        {
            GameObject newSkeleton = Instantiate(Skeleton, SpawnPoint1.position, Quaternion.identity);
        }
        if(enemiesPerSpawn > 1)
        {
            GameObject newSkeleton = Instantiate(Skeleton, SpawnPoint1.position + new Vector3(5f,0f), Quaternion.identity);
        }
        
    }
    void SpawnZombie()
    {

    }
    void SpawnBoss()
    {

    }

}
