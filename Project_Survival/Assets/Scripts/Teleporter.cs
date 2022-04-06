using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] GameObject Location, Player;
    [SerializeField] string messageToPop;
    public bool inRadius;

    UIManager _uiSystem;

    private void Start()
    {
        _uiSystem = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        if(inRadius && Input.GetKey(KeyCode.E))
        {
            Player.transform.position = Location.transform.position;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        inRadius = true;
        _uiSystem.popMessagePanel.SetActive(true);
        // call a method in the ui to pop message 
        _uiSystem.PopMessage(messageToPop);
    }

    private void OnTriggerExit(Collider other)
    {
        inRadius = false;
        _uiSystem.popMessagePanel.SetActive(false);
    }

}
