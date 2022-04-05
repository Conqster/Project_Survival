using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] GameObject Location, Player;
    [SerializeField] string messageToPop;
    public bool InRadius;

    UIManager _uiSystem;

    private void Start()
    {
        _uiSystem = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        if(InRadius && Input.GetKey(KeyCode.E))
        {
            Player.transform.position = Location.transform.position;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        InRadius = true;
        _uiSystem.popMessagePanel.SetActive(true);
        // call a method in the ui to pop message 
        _uiSystem.PopMessage(messageToPop);
    }

    private void OnTriggerExit(Collider other)
    {
        InRadius = false;
        _uiSystem.popMessagePanel.SetActive(false);
    }

}
