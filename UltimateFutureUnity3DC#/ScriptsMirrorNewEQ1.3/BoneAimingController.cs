using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneAimingController : MonoBehaviour
{
    [Header("This is a bone who move to a pointer")]
    [SerializeField] private Transform spineBone = null;
    [SerializeField] private Transform spine1Bone = null;
    [SerializeField] private Transform spine2Bone = null;

    [Header("This is a poninter to Move Look At ")]
    [SerializeField] private GameObject pointerAim=null;
    [SerializeField] private GameObject pointer = null;
    [SerializeField] private GameObject leftHandBone=null;



    private void LateUpdate()
    {
        BoneLookAtAim();
    }
    private void BoneLookAtAim()
    {
        spineBone.transform.LookAt(pointerAim.transform);
        spine1Bone.transform.LookAt(pointerAim.transform);
        spine2Bone.transform.LookAt(pointerAim.transform);
        //neckBone.transform.LookAt(pointerAim.transform);
        //leftShoulderBone.transform.LookAt(pointerAim.transform);
       // rightShoulderBone.transform.LookAt(pointerAim.transform);
        Debug.DrawLine(leftHandBone.transform.position, pointer.transform.position, Color.magenta);
    }

}
