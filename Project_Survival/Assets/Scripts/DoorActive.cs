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
    [SerializeField] Transform Door, NewPosition;

    bool complete = false, openDoor;

    private void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        _uiSystem = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        batteries = _playerManager.playerBattery;
        GetInput();

        if(batteries == 3 && complete)
        {
            openDoor = true;
            //Door.position = Vector3.right * Time.deltaTime;
        }

        if(openDoor)
        {
            Door.position = Vector3.Lerp(Door.position, NewPosition.position, 0.1f);
            Invoke("DemoComplete", 3f);
        }
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

    void DemoComplete()
    {
        _uiSystem.GameOver();
    }

    void GetInput()
    {
        complete = Input.GetKeyDown(KeyCode.E);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _uiSystem.popMessagePanel.SetActive(false);
        }
    }
}
