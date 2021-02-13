using Mirror;
using Mirror.Examples.NetworkRoom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class PlayerMirrorController : NetworkBehaviour
{
    [Header("This is Control Player Start")]
    public bool isPlayer = false;
    public PlayerController PlayerController;
    public CameraControllerPlayer CameraControllerPlayer;
    public GameObject Cameras = null;
    [Header("This is HUD Player")]
    public GameObject canvasHUD = null;
    public Canvas CanvasHUD = null;
    public UIManager UIManagerHUD = null;
    public GameObject EventSystem = null;
    public RepairControllerMouse repairControllerMouse=null;


    [Header("This is a Equipment Player")]
    public GameObject ToolBar = null;
    public List<GameObject> SlorControllerToolBar = new List<GameObject>();
    public GameObject EquipmentBox = null;
    public List<GameObject> SlorControllerEquipmentBox = new List<GameObject>();
    public GameObject InventoryManager = null;

    [Header("Select Object Hand Player ")]
    public GameObject rightHand;
    public GameObject leftHand;





    void OnValidate()
    {
        TriggerHudPlayer();
        TriggerEquipmentPlayer();
    }

    void Start()
    {
        TriggerPlayerHUDStart();
        if (isLocalPlayer) { FindAllSlotControllerServer(); CreateCameraToPlayer(); }
        isPlayer = isLocalPlayer;
        repairControllerMouse.enabled = isLocalPlayer;
        //CameraControllerPlayer.enabled = isLocalPlayer;
    }
    private void Update()
    {
        if (isLocalPlayer) { StartChecker(); };
    }


    public void TriggerPlayerHUDStart()
    {
        PlayerController.enabled = isLocalPlayer;
        CanvasHUD.enabled = isLocalPlayer;
        UIManagerHUD.enabled = isLocalPlayer;
        EventSystem.SetActive(isLocalPlayer);
    }
    public void TriggerHudPlayer()
    {

        if (PlayerController == null)
            PlayerController = GetComponent<PlayerController>();

        if (CameraControllerPlayer == null)
            CameraControllerPlayer = GetComponent<CameraControllerPlayer>();

        if (CanvasHUD == null)
            CanvasHUD = GameObject.Find(this.name + "/Canvas").GetComponent<Canvas>();

        if (UIManagerHUD == null)
            UIManagerHUD = GameObject.Find(this.name + "/Canvas").GetComponent<UIManager>();

        if (EventSystem == null)
            EventSystem = GameObject.Find(this.name + "/Canvas/EventSystem").GetComponent<GameObject>();

        if (canvasHUD == null)
            canvasHUD = GameObject.Find(this.name + "/Canvas/EventSystem").GetComponent<GameObject>();


    }
    public void TriggerEquipmentPlayer()
    {
        if (ToolBar == null)
            ToolBar = GameObject.Find(this.name + "/Canvas/EventSystem").GetComponent<GameObject>();

        if (EquipmentBox == null)
            EquipmentBox = GameObject.Find(this.name + "/Canvas/EventSystem").GetComponent<GameObject>();

        if (InventoryManager == null)
            InventoryManager = GameObject.Find(this.name + "/Canvas/EventSystem").GetComponent<GameObject>();
    }

    public void CreateCameraToPlayer()
    {
       var cameraInstantiate= Instantiate(Cameras,transform.position,Quaternion.identity);

        FreeLookCam freeLookCam = cameraInstantiate.transform.Find("FreeLookCameraRig").GetComponent<FreeLookCam>();
        freeLookCam.SetTarget(this.gameObject.transform);
    }

    //Check Trigger Key
    [Command]
    public void FindAllSlotControllerServer()
    {
        FindAllSlotController();
    }
    [ClientRpc]
    public void FindAllSlotController()
    {
        foreach (Transform child in ToolBar.transform)
        {
            SlorControllerToolBar.Add(child.gameObject);
        }
        foreach (Transform child in EquipmentBox.transform)
        {
            SlorControllerEquipmentBox.Add(child.gameObject);
        }
    }
    public void StartChecker()
    {
        CheckTheTriggerKey();
    }
    public void CheckTheTriggerKey()
    {
        foreach(GameObject slotControllerObj in SlorControllerToolBar)
        {
            SlotController slotControllerCheck = slotControllerObj.GetComponent<SlotController>();
            if (slotControllerCheck.ReturnKeyCodeObject()==KeyCode.None)
            {
                return;
            }
            else
            {
                if (Input.GetKeyDown(slotControllerCheck.ReturnKeyCodeObject()))
                {
                    foreach (var scriptableObject in ((NetworkRoomManagerExt)NetworkManager.singleton).scriptableObjectToMirror.Select(((value, index) => new { value, index })))
                    {
                        if (slotControllerCheck.ReturnPrefab() == null) { DestoryAllObjectInHandServer(); return; }
                        else if (scriptableObject.value.name== slotControllerCheck.ReturnPrefab().name)
                        {
                            DestoryAllObjectInHandServer();
                            SendSlotControllerToObjectServer(scriptableObject.index);
                        }
                    }
                }
            }
        }
    }
    [Command]
    public void SendSlotControllerToObjectServer(int indexSpanObject)
    {
        SendSlotControllerToObjectClients(indexSpanObject);
    }
    [ClientRpc]
    public void SendSlotControllerToObjectClients(int indexSpanObject)
    {
        var objectInstantiate = Instantiate(((NetworkRoomManagerExt)NetworkManager.singleton).objectHand, rightHand.transform);
        if (NetworkServer.active) { NetworkServer.Spawn(objectInstantiate); }



        if (((NetworkRoomManagerExt)NetworkManager.singleton).scriptableObjectToMirror[indexSpanObject] is WeaponeObjectController)
        {
            WeaponeObjectController weaponeObjectController = (WeaponeObjectController)((NetworkRoomManagerExt)NetworkManager.singleton).scriptableObjectToMirror[indexSpanObject];
            var objectWeaponeInstantiate = Instantiate(weaponeObjectController.ReturnWeaponeHand(), objectInstantiate.transform);
            if (NetworkServer.active) { NetworkServer.Spawn(objectWeaponeInstantiate); }
            objectWeaponeInstantiate.transform.localPosition = weaponeObjectController.ReturnPositionInstantiate();
            objectWeaponeInstantiate.transform.localEulerAngles = weaponeObjectController.ReturnRotationInstantiate();
            objectWeaponeInstantiate.transform.localScale = weaponeObjectController.ReturnScaleInstantiate() ;
        }
        else if (((NetworkRoomManagerExt)NetworkManager.singleton).scriptableObjectToMirror[indexSpanObject] is HandObjectController)
        {
            HandObjectController handObjectController = (HandObjectController)((NetworkRoomManagerExt)NetworkManager.singleton).scriptableObjectToMirror[indexSpanObject];
            var objectHandInstantiate = Instantiate(handObjectController.ReturnObjectInHandPref(), objectInstantiate.transform);
            if (NetworkServer.active) { NetworkServer.Spawn(objectHandInstantiate); }
            objectHandInstantiate.transform.localPosition = handObjectController.ReturnPositionInstantiate();
            objectHandInstantiate.transform.localEulerAngles = handObjectController.ReturnRotationInstantiate();
            objectHandInstantiate.transform.localScale = handObjectController.ReturnScaleInstantiate();
        }
    }

    [Command]
    public void DestoryAllObjectInHandServer()
    {
        DestoryAllObjectInHandClients();
    }
    [ClientRpc]
    public void DestoryAllObjectInHandClients()
    {
        foreach (Transform objectInHand in rightHand.transform)
        { 
            if (objectInHand.name== "ObjectHand(Clone)")
            {
                Destroy(objectInHand.gameObject);
            }
        }
    }

}