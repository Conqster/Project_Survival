using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //get and set property can be moved to different class to prevent confusion 
    //GameObject _gameUI;
    UIManager _gameUI;

    // variable get&set property storing player current health
    [SerializeField] private int playerHealth;
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
    [SerializeField] private int playerMaxHp = 10;
    public int PlayerMaxHealth
    {
        get
        {
            return playerMaxHp;
        }
        set
        {
            playerMaxHp = value; // used to set new value for private playerMaxHp
        }
    }

    //variables for current Xp & get&set for current player maximum Xp obtainable
    [SerializeField] private int currentXp = 0;
    public int CurrentXp
    {
        get
        {
            return currentXp;
        }
        set
        {
            currentXp = value;
        }
    }
    [SerializeField] private int PlayerMaxXp = 50;
    public int CurrentMaxXp
    {
        get
        {
            return PlayerMaxXp;
        }
        set
        {
            PlayerMaxXp = value;
        }
    }

    private void Start()
    {
        //_gameUI = GameObject.Find("GameUI").GetComponent<UIManager>();
        _gameUI = FindObjectOfType<UIManager>();
        UpdatePlayerHealth(9); // assign that player health when the game starts 
        // thinking of way to give playera little xp for starting the game => might just use a collider in the start area an destroy after 
        UpdatePlayerXp(0); // intialize the player xp 
    }

    // call when ever the player gains health item
    //need modification to dealth damage on player - something like if health < 0 take live if > 0 do function playerHealth < playermaxHealth
    public void UpdatePlayerHealth(int health)
    {
        //PlayerMaxHealth();
        //stop adding health when health is full 
        if (PlayerHp < PlayerMaxHealth) // ***playerMaxHealth was changed from PlayerMaxHealth() & PlayerHp from playerHealth
        {
            // to avoid getting health than max health
            int healthNeeded = PlayerMaxHealth - PlayerHp; // ***playerMaxHealth was changed from PlayerMaxHealth() & PlayerHp from playerHealth
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
        int newXp = currentXp + newPoints;
        if (CurrentXp % CurrentMaxXp > newPoints && newXp % CurrentMaxXp <= newPoints) // CurrentMaxXp from maxXp
        {
            print("grant more hp");
            // need to increase max health
            PlayerMaxHealth += 5; // this increase the player maximum health by 5
            CurrentMaxXp += 10; // this increases the amount xp reqiured to get to next level or earn extra stuffs 
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
        _gameUI.UpdatePlayerXpText();
    }

}
