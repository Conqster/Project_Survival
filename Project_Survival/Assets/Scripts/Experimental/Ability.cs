using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public int abilityRatio;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "player")
        {
            Inventory.AddAbility(abilityRatio);
            Debug.LogFormat("Player picked up - {0}", abilityRatio);
            Destroy(gameObject);
        }
    }
}
