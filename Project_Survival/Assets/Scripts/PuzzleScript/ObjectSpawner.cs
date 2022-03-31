using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    private float SpawnTime;
    [Range(0.0f, 100f)]
    [SerializeField] float _moveSpeed;
    [Range(-1, 1)]
    [SerializeField] int _xDir, _yDir, _zDir;
    [Range(0, 10)]
    [SerializeField] float SpawnTimer = 5f, _destroyTimer = 2f;

    [SerializeField] GameObject objectToSpawn;
    [SerializeField] Transform SpawnPoint;
    [SerializeField] bool useGravity, defineAxisColour;


    private void Update()
    {
        if ((_xDir != 0 || _yDir != 0 || _zDir != 0) && _moveSpeed != 0)
        {
            SpawnTime += Time.deltaTime;
            if (SpawnTime >= SpawnTimer)
            {
                SpawnObject();
                SpawnTime = 0f;
            }
        }

        //print("f");
    }

    void SpawnObject()
    {
        GameObject newObject = Instantiate(objectToSpawn, SpawnPoint.position, Quaternion.identity);
        //MeshRenderer mr = newPlatform.AddComponent<MeshRenderer>()
        if (defineAxisColour)
        {
            MeshRenderer objectMr = newObject.GetComponent<MeshRenderer>();
            if (objectMr != null)
            {
                if (_xDir != 0)
                {
                    objectMr.material.SetColor("_Color", Color.red);
                }
                if (_yDir != 0)
                {
                    objectMr.material.SetColor("_Color", Color.green);
                }
                if (_zDir != 0)
                {
                    objectMr.material.SetColor("_Color", Color.blue);
                }
            }
        }
        Rigidbody objectRb = newObject.GetComponent<Rigidbody>();
        objectRb.useGravity = useGravity;
        //if(addMass)
        //{
        //    objectRb.mass = mass;
        //}
        objectRb.velocity = new Vector3(_xDir, _yDir, _zDir) * _moveSpeed;

        Destroy(newObject, _destroyTimer);
    }
}
