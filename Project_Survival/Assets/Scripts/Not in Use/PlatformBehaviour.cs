using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    Transform _player;

    private void Start()
    {
        _player = GetComponent<Transform>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "player")
        {
            _player.transform.position = transform.position;
        }
    }
}
