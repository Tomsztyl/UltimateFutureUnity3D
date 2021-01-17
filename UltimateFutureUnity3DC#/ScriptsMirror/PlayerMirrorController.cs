using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMirrorController : NetworkBehaviour
{
    public PlayerController playerController;
    public GameObject canvasHUD;

    public GameObject HandWithGun;

    public GameObject HandWitObject;

    public MeshRenderer meshRendererGun;

    void OnValidate()
    {
        if (playerController == null)
            playerController = GetComponent<PlayerController>();

        if (canvasHUD == null)
            canvasHUD = GameObject.Find(this.name + "/Canvas").GetComponent<GameObject>();
    }

    void Start()
    {
        playerController.enabled = isLocalPlayer;
        canvasHUD.SetActive(isLocalPlayer);
    }

    //Controll Gun Hand
    public void SelectHandWithGun()
    {
        //Variables indexChoose
        //0-is No gun
        //1-is Gun 
        //2-is Object
        if (isLocalPlayer)
        {
            Debug.Log("Is Client");
            //ChooseHandActive();
            SendHandWithGun();
        }
    }

    [Command]
    public void SendHandWithGun()
    {
        ActiveHandWithGun();
    }
    [ClientRpc]
    public void ActiveHandWithGun()
    {
        HandWithGun.SetActive(true);
    }

    public void SelectHandWithoutGun()
    {
        if (isLocalPlayer)
        {
            SendHandWithoutGun();
        }
    }

    [Command]
    public void SendHandWithoutGun()
    {
        ActiveHandWithoutGun();
    }
    [ClientRpc]
    public void ActiveHandWithoutGun()
    {
        HandWithGun.SetActive(false);
    }

    //Controll Object Hand

    public void SelectHandWithObject()
    {
        if (isLocalPlayer)
        {
            Debug.Log("Is Client");
            SendHandWithObject();
        }
    }

    [Command]
    public void SendHandWithObject()
    {
        ActiveHandWithObject();
    }
    [ClientRpc]
    public void ActiveHandWithObject()
    {
        HandWitObject.SetActive(true);
    }

    public void SelectHandWithoutObject()
    {
        if (isLocalPlayer)
        {
            SendHandWithoutObject();
        }
    }

    [Command]
    public void SendHandWithoutObject()
    {
        ActiveHandWithoutObject();
    }
    [ClientRpc]
    public void ActiveHandWithoutObject()
    {
        HandWitObject.SetActive(false);
    }
}
