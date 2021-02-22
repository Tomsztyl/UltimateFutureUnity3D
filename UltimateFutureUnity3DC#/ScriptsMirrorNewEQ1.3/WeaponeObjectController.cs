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
    [Tooltip("Set It is A rifle Or Pistol Weapone or White Gun")]
    [SerializeField] private bool rifle = false;
    [SerializeField] private bool pistol = false;
    [SerializeField] private bool whiteGun = false;


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
}