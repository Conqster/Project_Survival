using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Haha : MonoBehaviour
{
    Transform Tr;
    new Light light;
    float radius;
    [SerializeField]

    private void Start()
    {
        Tr = GetComponent<Transform>();
        light = GetComponent<Light>();

        radius = light.range;
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

    }
}
