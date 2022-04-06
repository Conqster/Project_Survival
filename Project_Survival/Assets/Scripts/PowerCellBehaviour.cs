using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteAlways]
public class PowerCellBehaviour : MonoBehaviour
{
    [Range(1, 200)]
    [SerializeField] int rotationSpeed = 100;
    Transform ObjectTransform;
    PlayerManager _playerManager;


    void Start()
    {
        ObjectTransform = GetComponent<Transform>();
        _playerManager = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ObjectTransform.Rotate(0,0, rotationSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _playerManager.BatteryInventory(1);
            Destroy(gameObject);
            //want to update the ui with battery image
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Vector3 _object = ObjectTransform.position;
    //    Gizmos.color = Color.black;
    //    Gizmos.DrawWireSphere(_object, 0.5f);
    //}

}
