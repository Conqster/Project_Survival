using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalManager : MonoBehaviour
{
    // a static for the instance of the manager 
    public static SurvivalManager instance;

    PlayerManager _playerManager;
    [Range(1, 20)]
    [SerializeField] int assignPlayersHealth, assignPlayerXp;


    private void Awake()
    {
        if (instance != null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        _playerManager.UpdatePlayerHealth(assignPlayersHealth);
        //_playerManager.UpdatePlayerXp(assignPlayerXp);
    }
}
