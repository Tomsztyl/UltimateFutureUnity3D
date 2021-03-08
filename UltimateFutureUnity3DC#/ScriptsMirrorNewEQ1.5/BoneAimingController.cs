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
    [SerializeField] private bool isAiming = false;

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
    //private void Update()
    //{
    //    if (cameraControllerPlayer!=null &&isLocalPlayer)
    //    {
    //        PointerMove();
    //    }
    //}
    private void PointerMove()
    {
        Vector3 temp = Input.mousePosition;
        temp.z = 10f; // Set this to be the distance you want the object to be placed in front of the camera.

        Camera camera = cameraControllerPlayer.GetChooseCamera().GetComponent<Camera>();

        pointer.transform.position = camera.ScreenToWorldPoint(temp);
        //Debug.Log(NetworkIdentity.spawned[GetComponent<NetworkIdentity>().netId]);
        GetComponent<PlayerMirrorController>().CheckIsClientSendToServerComand(pointer.transform.position);
       //GetComponent<PlayerMirrorController>().UpdatePointerPostionServer(pointer.transform.position, GetComponent<NetworkIdentity>().netId);
    }
    private void LateUpdate()
    {
        if (cameraControllerPlayer != null && isLocalPlayer)
        {
            PointerMove();
        }

        if (isAiming)
        BoneLookAtAimClients();
    }
    private void BoneLookAtAimClients()
    {
        spineBone.transform.LookAt(pointerAim.transform);
        spine1Bone.transform.LookAt(pointerAim.transform);
        spine2Bone.transform.LookAt(pointerAim.transform);
        Debug.DrawLine(leftHandBone.transform.position, pointer.transform.position, Color.magenta);
    }
    [Command]
    public void SetStartIsAimingServer()
    {
        SetIsStartAimingClient();
    }
    [ClientRpc]
    public void SetIsStartAimingClient()
    {
        isAiming = true;
    }


    [Command]
    public void SetStopIsAimingServer()
    {
        SetIsStopAimingClient();
    }
    [ClientRpc]
    public void SetIsStopAimingClient()
    {
        isAiming = false;
    }


}
