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

    [Header("This is variable to trigger PingBar")]
    public GameObject pingBar = null;

    [Header("Variable Trigger Pointer ")]
    public GameObject pointer = null;
    [SyncVar]
    public Vector3 pointerMovePosition;
    public bool isPointer = false;  



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
        CameraControllerPlayer.enabled = isLocalPlayer;
    }
    private void Update()
    {
        if (isLocalPlayer) 
        { 
            StartChecker();
        }
    }
    public void TriggerPlayerHUDStart()
    {
        PlayerController.enabled = isLocalPlayer;
        CanvasHUD.enabled = isLocalPlayer;
        UIManagerHUD.enabled = isLocalPlayer;
        EventSystem.SetActive(isLocalPlayer);
        pingBar.SetActive(isLocalPlayer);
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

        if (pingBar == null)
            pingBar = GameObject.Find(this.name + "/Canvas/PingBar").GetComponent<GameObject>();

        if (pointer == null)
            pointer = GameObject.Find(this.name + "/Canvas/Pointer").GetComponent<GameObject>();
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
        var cameraInstantiate = Instantiate(Cameras, transform.position, Quaternion.identity);

        FreeLookCam freeLookCam = cameraInstantiate.transform.Find("FreeLookCameraRig").GetComponent<FreeLookCam>();
        freeLookCam.SetTarget(this.gameObject.transform);


        Transform ThirdCameraObj = GameObject.FindWithTag("ThirdCamera").GetComponent<Transform>();
        GameObject objectgoot;
        objectgoot = ThirdCameraObj.gameObject;
        CameraControllerPlayer.SetThirdCamera(objectgoot);
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
        foreach (GameObject slotControllerObj in SlorControllerToolBar)
        {
            SlotController slotControllerCheck = slotControllerObj.GetComponent<SlotController>();
            if (slotControllerCheck.ReturnKeyCodeObject() == KeyCode.None)
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
                        else if (scriptableObject.value.name == slotControllerCheck.ReturnPrefab().name)
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
        FightController fightController = objectInstantiate.transform.root.GetComponent<FightController>();



        if (((NetworkRoomManagerExt)NetworkManager.singleton).scriptableObjectToMirror[indexSpanObject] is WeaponeObjectController)
        {
            WeaponeObjectController weaponeObjectController = (WeaponeObjectController)((NetworkRoomManagerExt)NetworkManager.singleton).scriptableObjectToMirror[indexSpanObject];
            var objectWeaponeInstantiate = Instantiate(weaponeObjectController.ReturnWeaponeHand(), objectInstantiate.transform);
            if (NetworkServer.active) { NetworkServer.Spawn(objectWeaponeInstantiate); }

            //Set Properties Weapone
            objectWeaponeInstantiate.transform.localPosition = weaponeObjectController.ReturnPositionInstantiate();
            objectWeaponeInstantiate.transform.localEulerAngles = weaponeObjectController.ReturnRotationInstantiate();
            objectWeaponeInstantiate.transform.localScale = weaponeObjectController.ReturnScaleInstantiate();

            //Set ScriptableObject In Script Fight
            fightController.SetHandObjectController(null);
            fightController.SetWeaponeObjectController(weaponeObjectController);

            //Set ObjectHand and ObjectInHandChild
            fightController.SetObjcetHandChild(objectWeaponeInstantiate);
            fightController.SetObjectHand(objectInstantiate);
        }
        else if (((NetworkRoomManagerExt)NetworkManager.singleton).scriptableObjectToMirror[indexSpanObject] is HandObjectController)
        {
            HandObjectController handObjectController = (HandObjectController)((NetworkRoomManagerExt)NetworkManager.singleton).scriptableObjectToMirror[indexSpanObject];
            var objectHandInstantiate = Instantiate(handObjectController.ReturnObjectInHandPref(), objectInstantiate.transform);
            if (NetworkServer.active) { NetworkServer.Spawn(objectHandInstantiate); }

            //Set Properties Object
            objectHandInstantiate.transform.localPosition = handObjectController.ReturnPositionInstantiate();
            objectHandInstantiate.transform.localEulerAngles = handObjectController.ReturnRotationInstantiate();
            objectHandInstantiate.transform.localScale = handObjectController.ReturnScaleInstantiate();

            //Set ScriptableObject In Script Fight
            fightController.SetWeaponeObjectController(null);
            fightController.SetHandObjectController(handObjectController);

            //Set ObjectHand and ObjectInHandChild
            fightController.SetObjcetHandChild(objectHandInstantiate);
            fightController.SetObjectHand(objectInstantiate);
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
            if (objectInHand.name == "ObjectHand(Clone)")
            {
                Destroy(objectInHand.gameObject);
            }
        }
    }
    public void CheckIsClientSendToServerComand(Vector3 postionPointer)
    {
        isPointer = true;
        UpdatePointerPostionServer(postionPointer);
    }

    [Command]
    public void UpdatePointerPostionServer(Vector3 postionPointer)
    {
        UpdatePointerPostionClients(postionPointer);
    }
    [ClientRpc]
    public void UpdatePointerPostionClients(Vector3 postionPointer)
    {
        if (isPointer == false)
        {
            pointerMovePosition = postionPointer;
            pointer.transform.position = pointerMovePosition;
        }
        else
        {
            isPointer = false;
        }
    }
    //private void UpdateLocalPostionPointer()
    //{
    //    pointer.transform.position = pointerMovePosition;
    //}
    //public void StartUpdatePointer()
    //{
    //    pointerMovePosition = pointer.transform.position;
    //}
    //[Command]
    //public void UpdatePointerServer(Vector3 positionPointer)
    //{
    //    pointer.transform.position = positionPointer;
    //    UpdatePointerClients(positionPointer);
    //}
    //[ClientRpc]
    //public void UpdatePointerClients(Vector3 positionPointer)
    //{
    //    pointer.transform.position = positionPointer;
    //}


}