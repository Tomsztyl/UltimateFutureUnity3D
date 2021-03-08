using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Rifle", menuName = "HandObject/Make New Weapon", order = 1)]
public class WeaponeObjectController : ScriptableObject
{

    //Set Prefab In The Player
    [Header("Prefab In The Player")]
    [Tooltip("Set Parent RifleHand")]
    [SerializeField] private GameObject rifleHand;
    [Tooltip("Set Child WepaoneHand")]
    public GameObject weaponeHand;

    //Set Tranform In Unity
    [Tooltip("Weapon it is being created how child from parent RifleHand in Player")]
    [Header("Set Transform Weapone Where Is Instantiate")]
    [SerializeField] private Vector3 positionInstantiate;
    [SerializeField] private Vector3 rotationInstantiate;
    [Tooltip("Default properties for the size object x=1f,y=1f,z=1f")]
    [SerializeField] private Vector3 scaleInstantiate = new Vector3(1f, 1f, 1f);

    //Properties Weapone
    [Tooltip("Set Properties Weapone")]
    [Header("This is a properties Weapone")]
    [SerializeField] private float damageWeapone;
    [Tooltip("This Variable set range Shooting Weapone")]
    [SerializeField] private float rangeWeapone;
    [Tooltip("This Variable set a Time to next shoot Weapone")]
    [SerializeField] private float timeToNextRateShoot = 0f;
    [Tooltip("Set It is A rifle Or Pistol Weapone or White Gun")]
    [SerializeField] private bool rifle = false;
    [SerializeField] private bool pistol = false;
    [SerializeField] private bool whiteGun = false;
    [Header("Variable Controll Speed Player")]
    [Tooltip("This variables set speed Player if have object in hand")]
    [SerializeField] private float speedWalkingPlayer = 0f;
    [Tooltip("This variable set speed rotation Player if have object in hand")]
    [SerializeField] private float speedRotationPlayer = 0f;


    //Propetries Particle
    [Header("This is variables to particle gun shoot")]
    [SerializeField] private bool isParticle = false;
    [SerializeField] private GameObject impactParticle=null;


    #region Properties Return to Instantiate Weapone Start
    public GameObject ReturnWeaponeHand()
    {
        return weaponeHand;
    }
    public Vector3 ReturnPositionInstantiate()
    {
        return positionInstantiate;
    }
    public Vector3 ReturnRotationInstantiate()
    {
        return rotationInstantiate;
    }
    public Vector3 ReturnScaleInstantiate()
    {
        return scaleInstantiate;
    }

    #endregion
    #region Properties Particle Impact
    public bool GetIsParticle()
    {
        return isParticle;
    }
    public GameObject GetImpactParticle()
    {
        return impactParticle;
    }
    #endregion
    #region Properties Properties Weapone
    public float GetDamageWeapone()
    {
        return damageWeapone;
    }
    public float GetRangeWeapone()
    {
        return rangeWeapone;
    }
    public float GetTimeToNextRateShoot()
    {
        return timeToNextRateShoot;
    }
    #endregion

    public float ReturnSpeedWalikingPlayer()
    {
        return speedWalkingPlayer;
    }
    public float ReturnSpeedRotationPlayer()
    {
        return speedRotationPlayer;
    }
}