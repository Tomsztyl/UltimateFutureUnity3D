using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneAimingController : NetworkBehaviour
{
    [Header("This is a bone who move to a pointer")]
    [SerializeField] private Transform spineBone = null;
    [SerializeField] private Transform spine1Bone = null;
    [SerializeField] private Transform spine2Bone = null;

    [Header("This is a poninter to Move Look At ")]
    [SerializeField] private GameObject pointerAim=null;
    [SerializeField] private GameObject pointer = null;
    [SerializeField] private GameObject leftHandBone=null;

    private CameraControllerPlayer cameraControllerPlayer = null;
    private Animator animator;

    private void Start()
    {
        if (animator==null)
        {
            animator = GetComponent<Animator>();
        }

        if (cameraControllerPlayer==null)
        {
            cameraControllerPlayer = GetComponent<CameraControllerPlayer>();
        }
    }
    private void Update()
    {
        if (cameraControllerPlayer!=null)
        {
            PointerMove();
        }
    }
    private void PointerMove()
    {
        Vector3 temp = Input.mousePosition;
        temp.z = 10f; // Set this to be the distance you want the object to be placed in front of the camera.

        Camera camera = cameraControllerPlayer.GetChooseCamera().GetComponent<Camera>();

        pointer.transform.position = camera.ScreenToWorldPoint(temp);
        GetComponent<PlayerMirrorController>().UpdatePointerServer(pointer.transform.position);
    }
    [Command]
    private void LateUpdate()
    {
        BoneLookAtAimClients();
    }
    [ClientRpc]
    private void BoneLookAtAimClients()
    {
        spineBone.transform.LookAt(pointerAim.transform);
        spine1Bone.transform.LookAt(pointerAim.transform);
        spine2Bone.transform.LookAt(pointerAim.transform);
        Debug.DrawLine(leftHandBone.transform.position, pointer.transform.position, Color.magenta);
    }


}
