﻿using System.Collections;
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

    public void CreateObject(Transform Hand)
    {
        var objectHandObj = Instantiate(objectHand, Hand);
        var objectInHandPrefObj = Instantiate(objectInHandPref, objectHandObj.transform);
        objectInHandPrefObj.transform.localPosition = positionInstantiate;
        objectInHandPrefObj.transform.localEulerAngles = rotationInstantiate;
        objectInHandPrefObj.transform.localScale = scaleInstantiate;
    }
}
