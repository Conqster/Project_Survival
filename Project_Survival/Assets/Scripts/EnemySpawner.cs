using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject zombie;
    public float spawnTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnZombie", 0f, spawnTime);
    }
    void SpawnZombie()
    {
        Instantiate(zombie, transform.position, Quaternion.identity);
    }
}
