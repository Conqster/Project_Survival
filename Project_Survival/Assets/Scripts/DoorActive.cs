using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class DoorActive : MonoBehaviour
{
    PlayerManager _playerManager;
    UIManager _uiSystem;
    [SerializeField] int batteries;
    [SerializeField] string CongratsMessage, errorMessage, clueMessage;

    private void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        _uiSystem = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        batteries = _playerManager.playerBattery;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
                _uiSystem.popMessagePanel.SetActive(true);
            if(batteries == 3)
            {
                print("Congrats");
                _uiSystem.PopMessage(CongratsMessage);
            }
            if(batteries == 2)
            {
                _uiSystem.PopMessage("One more batteries");
            }
            if(batteries == 1)
            {
                _uiSystem.PopMessage("Two more batteries");
            }
            if(batteries <= 0)
            {
                _uiSystem.PopMessage(errorMessage);
                _uiSystem.PopMessage(clueMessage);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _uiSystem.popMessagePanel.SetActive(false);
        }
    }
}
