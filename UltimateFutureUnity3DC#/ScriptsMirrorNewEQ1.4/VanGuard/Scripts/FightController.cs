using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightController : NetworkBehaviour
{
    [Header("Variables who Define Shooting")]
    [Tooltip("Key set Shoot Gun")]
    [SerializeField] private KeyCode shootGun = KeyCode.Mouse0;
    [SerializeField] private bool isAiming = false; 


    [Header("This is Clone Main ObjectHand")]
    [SerializeField] private GameObject objectHand=null;
    [Header("This is a child ObjectHand [OBJECT/WEAPONE]")]
    [SerializeField] private GameObject objectHandChild=null;


    [Header("This is a ScriptableObject to weapone in hand")]
    [Tooltip("This variable set when player get weapone in hand")]
    [SerializeField] private WeaponeObjectController weaponeObjectController=null;
    [Tooltip("Tihs variable set when player get object in hand")]
    [SerializeField] private HandObjectController handObjectController=null;

    #region Method Change Trigger Shoot
    public void StartAiming()
    {
        isAiming = true;
    }
    public void StopAiming()
    {
        isAiming = false;
    }
    #endregion

    private void Update()
    {
        if (isAiming)
        {
            if (Input.GetKeyDown(shootGun))
            {
                //Start Shooting
                Debug.Log("Start Shooting");
                ShootController();
            }
            if (Input.GetKeyUp(shootGun))
            {
                //Stop Shooting
                Debug.Log("Stop Shooting");
            }
        }
    }
    #region Controller Scriptable Object And Object In Hand
    public void SetWeaponeObjectController(WeaponeObjectController weaponeObjectController)
    {
        this.weaponeObjectController = weaponeObjectController;
    }
    public void GetWeaponeObjectController()
    {

    }
    public void SetHandObjectController(HandObjectController handObjectController)
    {
        this.handObjectController = handObjectController;
    }
    public void GetHandObjectController()
    {

    }
    public void SetObjectHand(GameObject objectHand)
    {
        this.objectHand = objectHand;
    }
    public void SetObjcetHandChild(GameObject objectHandChild)
    {
        this.objectHandChild = objectHandChild;
    }
    #endregion
    #region Mechanic Shooting RayCast
    [Command]
    private void ShootController()
    {
        ShootControllerToClient();
    }
    [ClientRpc]
    private void ShootControllerToClient()
    {
        if (weaponeObjectController == null) { return; }
        RaycastHit hit;
        ParticleSystem muzzleEfectWeapone = objectHandChild.transform.Find("MuzzleWeapone").GetComponent<ParticleSystem>();
        if (muzzleEfectWeapone != null)
        {
            muzzleEfectWeapone.Play();
        }
        if (Physics.Raycast(objectHandChild.transform.position, objectHandChild.transform.forward, out hit, weaponeObjectController.GetRangeWeapone()))
        {
           // Debug.LogWarning(hit.transform.name);

            BoxController boxController = hit.transform.GetComponent<BoxController>();
            if (boxController != null)
            {
                boxController.ExtractBox();
            }

            if (weaponeObjectController.GetImpactParticle() != null)
            {
                var objectInstantiateParticle = Instantiate(weaponeObjectController.GetImpactParticle(), hit.point, Quaternion.LookRotation(hit.normal));
                if (NetworkServer.active) { NetworkServer.Spawn(objectInstantiateParticle); }
                Destroy(objectInstantiateParticle.gameObject, 2f);
            }

        }
    }
    #endregion
}
