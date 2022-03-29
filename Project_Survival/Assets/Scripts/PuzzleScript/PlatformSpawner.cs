using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [Range(0, 10)]
    [SerializeField] float SpawnTimer;
    private float Spawn;

    [SerializeField] GameObject Platform;
    [SerializeField] Transform SpawnPoint;

    private void Start()
    {
        Spawn = SpawnTimer - 0.3f;
    }
    private void Update()
    {
        Spawn += Time.deltaTime;
        if (Spawn > SpawnTimer)
        {
            GameObject newPlatform = Instantiate(Platform, SpawnPoint.position, Quaternion.identity);
            Spawn = 0f;
        }
    }
}
