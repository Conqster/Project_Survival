using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyWave : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] int numberOfEnemies = 1;
    [SerializeField] Transform SpawnPoint;
    [SerializeField] GameObject EnemyType;
    [Range(3,30)]
    [SerializeField] float spawnTimer, radiusOfDectection = 3f;
    bool start = false;
    float timer = 1f;

    SphereCollider col;
    private void Start()
    {
        col = GetComponent<SphereCollider>();
        col.isTrigger = true;
        col.radius = radiusOfDectection;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            start = true;
        }
    }

    private void Update()
    {
        if(start)
        {
            timer -= Time.deltaTime;
        }

        if(timer <= 0)
        {
            Spawn();
            timer = spawnTimer;
            numberOfEnemies--;
            //print("whats up");
        }

        if(numberOfEnemies <= 0)
        {
            Destroy(gameObject);
        }
        print(timer);
    }

    private void OnDrawGizmos()
    {
        
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, radiusOfDectection);
        
    }
    void Spawn()
    {
        GameObject newEnemy = Instantiate(EnemyType, SpawnPoint.position, Quaternion.identity);
    }
}
