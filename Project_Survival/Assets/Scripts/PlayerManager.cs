using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //get and set property can be moved to different class to prevent confusion 
    //GameObject _gameUI;
    UIManager _gameUI;

    [Range(0, 5000)]
    [SerializeField] private int playerHealth, playerMaxHealth = 200, PlayerMaxExperience = 1600;
    [SerializeField] private int currentPlayerExperience = 0;
    // variable get&set property storing player current health

    public int PlayerHp
    {
        get
        {
            return playerHealth;
        }
        set
        {
            playerHealth = value;
        }
    }

    // variable get&set property storing player current maximum health obtainable
    public int PlayerMaxHp
    {
        get
        {
            return playerMaxHealth;
        }
        set
        {
            playerMaxHealth = value; // used to set new value for private playerMaxHp
        }
    }

    //variables for current Xp & get&set for current player maximum Xp obtainable
    public int CurrentXp
    {
        get
        {
            return currentPlayerExperience;
        }
        set
        {
            currentPlayerExperience = value;
        }
    }
    
    public int CurrentMaxXp
    {
        get
        {
            return PlayerMaxExperience;
        }
        set
        {
            PlayerMaxExperience = value;
        }
    }

    private void Start()
    {
        //_gameUI = GameObject.Find("GameUI").GetComponent<UIManager>();
        _gameUI = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            UpdatePlayerXp(50);
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            UpdatePlayerHealth(15);
        }
    }

    // call when ever the player gains health item
    //need modification to dealth damage on player - something like if health < 0 take live if > 0 do function playerHealth < playermaxHealth
    public void UpdatePlayerHealth(int health)
    {
        //PlayerMaxHealth();
        //stop adding health when health is full 
        if (PlayerHp < PlayerMaxHp) // ***playerMaxHealth was changed from PlayerMaxHealth() & PlayerHp from playerHealth
        {
            // to avoid getting health than max health
            int healthNeeded = PlayerMaxHp - PlayerHp; // ***playerMaxHealth was changed from PlayerMaxHealth() & PlayerHp from playerHealth
            if (healthNeeded > health) // if what player needs is more that health pickup
            {
                PlayerHp += health; // PlayerHp from playerHealth
            }
            else
            {
                PlayerHp += healthNeeded; // if the pick up is more than what the player needs just give them what they need //& PlayerHp from playerHealth
            }
        }
        _gameUI.UpdatePlayerHpUI(); // tell ui manager the player health has been up dated 
    }

    

    public void UpdatePlayerXp(int newPoints)
    {
        int newXp = CurrentXp + newPoints;
        //might another line of code to balance the logic 
        if (CurrentXp % CurrentMaxXp > newPoints && newXp % CurrentMaxXp <= newPoints) // CurrentMaxXp from maxXp
        {
            // need to increase max health
            PlayerMaxHp += 50; // this increase the player maximum health by 5
            CurrentMaxXp += 500; // this increases the amount xp reqiured to get to next level or earn extra stuffs 
            CurrentXp = 0; // resets current Xp back to zero 
            // need to increase max abilty & an if statement to check if player can perform any ability 
            // need to inrease enemies health with a small value
            // increase AI awareness 
        }
        else
        {
            // need the new Xp be the current xp the player has
            CurrentXp = newXp;
        }
        print(newXp);
        //_gameUI.UpdatePlayerXpText();
    }
}
