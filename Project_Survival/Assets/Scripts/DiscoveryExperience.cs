using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class DiscoveryExperience : MonoBehaviour
{
    [Range(1, 1000)]
    [SerializeField] int awardExp = 1;
    [SerializeField] string messageToPop;
    [Range(0f,10)]
    [SerializeField] float dectectionRadius = 3.5f;

    PlayerManager _playerManager;
    UIManager Ui;
    SphereCollider col;

    private void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        col = GetComponent<SphereCollider>();
        Ui = FindObjectOfType<UIManager>();
        col.radius = dectectionRadius;
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _playerManager.UpdatePlayerXp(awardExp);
            //ui message goes here and make it take in Xp int and string message
            Ui.AwardXpMessage(messageToPop, awardExp);
            Destroy(gameObject);
        }
    }
}
