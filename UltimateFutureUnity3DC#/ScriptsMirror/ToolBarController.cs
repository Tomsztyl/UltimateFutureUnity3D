using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolBarController : NetworkBehaviour
{
    [SerializeField] private List<Image> slot_InventoryToolBarSprite = new List<Image>();
    [SerializeField] private List<Text> slot_InventoryToolBarTextCount = new List<Text>();
    [SerializeField] private List<KeyCode> slot_InventoryKeyToolBar = new List<KeyCode>();
    [SerializeField] private List<Button> slot_InventoryButtonToolBar = new List<Button>();


    [SerializeField]private HandController hanControllerWithGun;


    [SerializeField]private GameObject player;

    [SerializeField] private PlayerController playerControlle;

    public PlayerMirrorController playerMirrorController;


    private void Start()
    {
        player = this.gameObject.transform.parent.parent.gameObject;
        playerMirrorController = player.GetComponent<PlayerMirrorController>();
        //MainObject = GameObject.;
    }

    private void Update()
    {
        OnClickKeyButton();
    }

    public void SetSpriteAndCountToolBar(Sprite sprite,int countMaterial)
    {
        if (IsImageMaterial(sprite, slot_InventoryToolBarSprite.Count))
        {
            slot_InventoryToolBarSprite[IsImageMaterialIndex(sprite, slot_InventoryToolBarSprite.Count)].enabled = true;
            slot_InventoryToolBarTextCount[IsImageMaterialIndex(sprite, slot_InventoryToolBarSprite.Count)].text = ""+countMaterial;
        }
        else
        {
            if (CheckIsAPlace(slot_InventoryToolBarSprite.Count) == true)
            {
                slot_InventoryToolBarSprite[CheckIsAPlaceIndex(slot_InventoryToolBarSprite.Count)].enabled = true;
                slot_InventoryToolBarTextCount[CheckIsAPlaceIndex(slot_InventoryToolBarSprite.Count)].text = "" + countMaterial;
                slot_InventoryToolBarSprite[CheckIsAPlaceIndex(slot_InventoryToolBarSprite.Count)].sprite = sprite;
            }
            else
            {
                Debug.LogWarning("Tool Bar is Full");
            }
        }        
    }

    private bool CheckIsAPlace(int inventoryCount)
    {
        for (int i=0;i< inventoryCount; i++)
        {
            if (slot_InventoryToolBarSprite[i].sprite == null)
            {
                return true;
            }
        }
        return false;
    }
    private int CheckIsAPlaceIndex(int inventoryCount)
    {
        for (int i = 0; i < inventoryCount; i++)
        {
            if (slot_InventoryToolBarSprite[i].sprite == null)
            {
                return i;
            }
        }
        //Index is not Found
        Debug.LogError("Index is not Found I return -1");
        return -1;
    }
    private bool IsImageMaterial(Sprite sprite,int inventoryCount)
    {
        for (int i = 0; i < inventoryCount; i++)
        {
            if (slot_InventoryToolBarSprite[i].sprite == sprite)
            {
                return true;
            }
        }
        //Index is not Found
        Debug.LogWarning("Index is not Found I return false");
        return false;
    }
    private int IsImageMaterialIndex(Sprite sprite, int inventoryCount)
    {
        for (int i = 0; i < inventoryCount; i++)
        {
            if (slot_InventoryToolBarSprite[i].sprite == sprite)
            {
                return i;
            }
        }
        //Index is not Found
        Debug.LogError("Index is not Found I return -1");
        return -1;
    }

    private void OnClickKeyButton()
    {
        for (int i = 0; i < slot_InventoryButtonToolBar.Count; i++)
        {
            if (Input.GetKeyDown(slot_InventoryKeyToolBar[i]))
            {
                slot_InventoryButtonToolBar[i].Select();
                slot_InventoryButtonToolBar[i].onClick.Invoke();
                //slot_InventoryButtonToolBar[i].interactable = true;
                GetItemToCharacterHand(slot_InventoryToolBarSprite[i].sprite,i);
                //Debug.Log("Click Button: " + slot_InventoryKeyToolBar[i]);
                //slot_InventoryButtonToolBar[i].onClick = true;
            }
        }
    }
    private void GetItemToCharacterHand(Sprite sprite,int idItem)
    {
        //SelectGun
        InventoryController inventoryController = GetComponent<InventoryController>();
        playerControlle = player.GetComponent<PlayerController>();

        if (sprite!=null)
        {
            if (inventoryController.PrefabObjectItem[idItem].tag == "Gun")
            {
                if (playerControlle.HandWitObject.active == true)
                {
                    playerControlle.HandWitObject.SetActive(false);
                    playerMirrorController.SelectHandWithoutObject();
                }


                playerControlle.HandWithGun.SetActive(true);
                //playerMirrorController.indexChoose = 1;
                playerMirrorController.SelectHandWithGun();
                hanControllerWithGun = playerControlle.HandWithGun.GetComponent<HandController>();
                playerControlle.IsGun = 1;
                MeshRenderer meshRenderer = inventoryController.PrefabObjectItem[idItem].GetComponent<MeshRenderer>();
                hanControllerWithGun.ChangeMaterialGun(meshRenderer.sharedMaterial);
                Debug.LogWarning("You select gun");
                //MeshRenderer meshRenderer=GameObject.
            }
            else
            {
                if (playerControlle.IsGun==1)
                {
                    playerControlle.HandWithGun.SetActive(false);
                    playerMirrorController.SelectHandWithoutGun();
                    playerControlle.IsGun = 0;
                }

                playerControlle.HandWitObject.SetActive(true);
                playerMirrorController.SelectHandWithObject();
                //playerMirrorController.indexChoose=2;
                MeshRenderer meshRendererHandObject = playerControlle.HandWitObject.GetComponent<MeshRenderer>();
                MeshFilter meshFilterHandObject = playerControlle.HandWitObject.GetComponent<MeshFilter>();

                MeshRenderer meshRendererObject = inventoryController.PrefabObjectItem[idItem].GetComponentInChildren<MeshRenderer>();
                MeshFilter meshFilterObject = inventoryController.PrefabObjectItem[idItem].GetComponentInChildren<MeshFilter>();

                 meshRendererHandObject.sharedMaterial = meshRendererObject.sharedMaterial;
                 meshFilterHandObject.sharedMesh = meshFilterObject.sharedMesh;

                 Debug.LogWarning("You select something else");                              
            }
        }
        else
        {
            Debug.LogWarning("Nothing it is in slot["+idItem+"]");
        }
       
    }
}
