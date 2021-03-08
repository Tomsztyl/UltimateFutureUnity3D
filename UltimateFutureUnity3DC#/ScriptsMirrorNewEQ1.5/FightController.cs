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
    [SerializeField] private bool isShooting = false;   
    private float nextTimeToFire = 0f;


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
        isShooting = false;
    }
    #endregion

    private void LateUpdate()
    {
        StartShooting();
    }
    #region Mechanic Start Shooting
    private void StartShooting()
    {
        if (weaponeObjectController == null) return;
        if (isAiming)
        {
            if (Input.GetKeyDown(shootGun))
            {
                //Start Shooting
                isShooting = true;              
            }
            if (Input.GetKeyUp(shootGun))
            {
                //Stop Shooting
                isShooting = false;
            }
        }

        if (isShooting && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / weaponeObjectController.GetTimeToNextRateShoot();
            ShootController();
        }
    }
    #endregion
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
    private void ShootController()
    {
        ShootControllerToClient();
    }
    private void ShootControllerToClient()
    {
        if (weaponeObjectController == null) { return; }
        RaycastHit hit;
        //Start Muzzzle Effect
        MuzzleEffectTriggerServer();

        if (Physics.Raycast(objectHandChild.transform.position, objectHandChild.transform.forward, out hit, weaponeObjectController.GetRangeWeapone()))
        {
            //Send To Server to Clinets Point Hit
            InstatniateHitPointShootServer(hit.point, hit.normal) ;

            HealthController healthController = hit.transform.GetComponent<HealthController>();
            if (healthController!=null)
            {
                healthController.TakeDamage(weaponeObjectController.GetDamageWeapone());
            }

            BoxController boxController = hit.transform.GetComponent<BoxController>();
            if (boxController != null)
            {
                boxController.ExtractBox();
            }

        }
    }
    #region Effect Particle Shoot

    //Muzzle Effect Trigger Server to Clinets
    [Command]
    private void MuzzleEffectTriggerServer()
    {
        MuzzleEffectTriggerClients();
    }
    [ClientRpc]
    private void MuzzleEffectTriggerClients()
    {
        ParticleSystem muzzleEfectWeapone = objectHandChild.transform.Find("MuzzleWeapone").GetComponent<ParticleSystem>();
        if (muzzleEfectWeapone != null)
        {
            muzzleEfectWeapone.Play();
        }
    }
    //***

    //Start Shooting Effect Server to Clinets
    [Command]
    private void InstatniateHitPointShootServer(Vector3 hitPoint,Vector3 hitNormal)
    {
        //InstatniatePoinShoot(hitPoint, hitNormal);
        InstatniateHitPointShootClients(hitPoint, hitNormal) ;
    }
    [ClientRpc]
    private void InstatniateHitPointShootClients(Vector3 hitPoint, Vector3 hitNormal)
    {
        InstatniatePoinShoot(hitPoint, hitNormal); 
    }
    private void InstatniatePoinShoot(Vector3 hitPoint, Vector3 hitNormal)
    {
        if (weaponeObjectController.GetImpactParticle() != null)
        {
            var objectInstantiateParticle = Instantiate(weaponeObjectController.GetImpactParticle(), hitPoint, Quaternion.LookRotation(hitNormal));
            if (NetworkServer.active)
            {
                NetworkServer.Spawn(objectInstantiateParticle);

                //ShootingDestroyBoxServer(objectInstantiateParticle);
            }
            Destroy(objectInstantiateParticle.gameObject, 2f);
        }
    }
    //****

    //Trigger Destroy Box To Shooting Server to Clinets 
    [Command]
    private void ShootingDestroyBoxServer(GameObject hitParticle)
    {
        Debug.Log(hitParticle);
       // ShootingDestoyBoxClients(hitParticle);
    }
    [ClientRpc]
    private void ShootingDestoyBoxClients(GameObject hitParticle)
    {
        //BoxController boxController = hitParticle.transform.GetComponent<BoxController>();
        //if (boxController != null)
        //{
        //    boxController.ExtractBox();
        //}
    }
    //*****


    #endregion
    #endregion
}
