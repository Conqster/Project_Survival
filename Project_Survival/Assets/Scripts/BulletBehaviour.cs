using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
   [SerializeField] float OnScreenDelay = 3f;

   void Start()
   {
       Destroy(gameObject, OnScreenDelay);
   }
}
