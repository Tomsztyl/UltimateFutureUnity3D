using System.Collections;
using Mirror;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "HandObject", menuName = "HandObject/Make New Object", order = 0)]
public class HandObjectController : ScriptableObject
{
    //Set Prefab In The Player
    [Header("Prefab In The Player")]
    [Tooltip("Set Parent ObjectHand")]
    [SerializeField] private GameObject objectHand;
    [Tooltip("Set Child objectInHandPref")]
    [SerializeField] private GameObject objectInHandPref;

    //Set Tranform In Unity
    [Tooltip("Object it is being created how child from parent ObjectHand in Player")]
    [Header("Set Transform ObjectPrefab Where Is Instantiate")]
    [SerializeField] private Vector3 positionInstantiate;
    [SerializeField] private Vector3 rotationInstantiate;
    [Tooltip("Default properties for the size object x=1f,y=1f,z=1f")]
    [SerializeField] private Vector3 scaleInstantiate = new Vector3(1f, 1f, 1f);

    //Properties Object
    [Tooltip("Set Properties Object")]
    [Header("This is a properties Object")]
    [SerializeField] private float healPoint;
    [Header("Variable Controll Speed Player")]
    [Tooltip("This variables set speed Player if have object in hand")]
    [SerializeField] private float speedWalkingPlayer=0f;
    [Tooltip("This variable set speed rotation Player if have object in hand")]
    [SerializeField] private float speedRotationPlayer=0f;

    public GameObject ReturnObjectInHandPref()
    {
        return objectInHandPref;
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
    public float ReturnSpeedWalikingPlayer()
    {
        return speedWalkingPlayer;
    }
    public float ReturnSpeedRotationPlayer()
    {
        return speedRotationPlayer;
    }
}
