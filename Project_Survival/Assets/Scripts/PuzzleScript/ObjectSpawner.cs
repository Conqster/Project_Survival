using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    private float SpawnTime;
    [Range(0.0f, 10f)]
    [SerializeField] float _moveSpeed;
    [Range(-1, 1)]
    [SerializeField] int _xDir, _yDir, _zDir;
    [Range(0, 10)]
    [SerializeField] float SpawnTimer = 5f, _destroyTimer = 2f, delayBy;
    [Range(0, 2000)]
    [SerializeField] float force;

    [SerializeField] GameObject objectToSpawn;
    [SerializeField] Transform SpawnPoint;
    [SerializeField] bool useGravity,delayStart = false, defineAxisColour, useForce = false;


    private void Update()
    {
        if ((_xDir != 0 || _yDir != 0 || _zDir != 0) && _moveSpeed != 0)
        {
            SpawnTime += Time.deltaTime;
            if (SpawnTime >= SpawnTimer)
            {
                if(!delayStart)
                { SpawnObject(); }
                else
                { Invoke("SpawnObject", delayBy); }
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
        //potential bug
        //objectRb.AddForce(_xDir * _moveSpeed, _yDir * _moveSpeed, _zDir * _moveSpeed);
        objectRb.useGravity = useGravity;
        if(!useForce)
        { objectRb.velocity = new Vector3(_xDir, _yDir, _zDir) * _moveSpeed; }
        else
        { objectRb.AddForce(_xDir * force, _yDir * force, _zDir * force); }

        Destroy(newObject, _destroyTimer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
