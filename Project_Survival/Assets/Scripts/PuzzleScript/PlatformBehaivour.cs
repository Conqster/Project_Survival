using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaivour : MonoBehaviour
{
    [Range(0.0f, 10f)]
    [SerializeField] float move;
    [Range(-1, 1)]
    [SerializeField] int xDir, yDir, zDir;
    [Range(0, 10)]
    [SerializeField] float destroyTimer;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.velocity = new Vector3(xDir, yDir, zDir) * move;
        //transform.position += new Vector3(xDir, yDir, zDir) * move;

        Destroy(gameObject, destroyTimer);
    }
}
