using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static int ability;
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogFormat("Player ability left - {0} ", ability);
    }

    public static void AddAbility(int abilityRatio)
    {
        int abilityNeeded = 100 - ability;
        //int addAbility  
        if(ability < 100 && abilityNeeded >= abilityRatio)
        {
             ability += abilityRatio;
        }
        else if(ability < 100 && abilityNeeded <= abilityRatio)
        {
            ability += abilityNeeded;
        }
        Debug.LogFormat("Player ability left - {0} ", ability);
        
    }

    public int MyAbility()
    {
        return ability;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
