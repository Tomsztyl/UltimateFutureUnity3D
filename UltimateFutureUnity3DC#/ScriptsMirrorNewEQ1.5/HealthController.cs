using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class HealthController : NetworkBehaviour
{
    [Header("Set Current Healt")]
    [Tooltip("Default Healt is 100")]
    [SyncVar]
    public float healt = 100;
    //[SerializeField] private KindCharacter kindCharacter;
    public TextMeshPro textHealt;

    private void Update()
    {
        if (isLocalPlayer&& Input.GetKeyDown(KeyCode.R)) 
        {
            PrintServer();
        }
        if (isLocalPlayer)
        {
            DisplayTextServer();
        }
    }
    [Command]
    private void PrintServer()
    {
        PrintClient();
    }
    [ClientRpc]
    private void PrintClient()
    {
        Debug.LogWarning(healt);
    }
    
    public void TakeDamage(float damage)
    {
        healt = healt - damage;
    }

    [Command]
    public void DisplayTextServer()
    {
        DisplayTextClients();
    }
    [ClientRpc]
    public void DisplayTextClients()
    {
        textHealt.text = "" + healt;
    }




}
