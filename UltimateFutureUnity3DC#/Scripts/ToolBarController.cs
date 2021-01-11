using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolBarController : MonoBehaviour
{
    [SerializeField] private List<Image> slot_InventoryToolBarSprite = new List<Image>();
    [SerializeField] private List<Text> slot_InventoryToolBarTextCount = new List<Text>();
    [SerializeField] private List<KeyCode> slot_InventoryKeyToolBar = new List<KeyCode>();
    [SerializeField] private List<Button> slot_InventoryButtonToolBar = new List<Button>();


    [SerializeField]private HandController hanControllerWithGun;


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
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (sprite!=null)
        {
            if (inventoryController.PrefabObjectItem[idItem].tag == "Gun")
            {
                if (player.HandWitObject.active == true)
                    player.HandWitObject.SetActive(false);

                player.HandWithGun.SetActive(true);
                hanControllerWithGun = GameObject.FindGameObjectWithTag("Hand").GetComponent<HandController>();
                player.IsGun = 1;
                MeshRenderer meshRenderer = inventoryController.PrefabObjectItem[idItem].GetComponent<MeshRenderer>();
                hanControllerWithGun.ChangeMaterialGun(meshRenderer.sharedMaterial);
                Debug.LogWarning("You select gun");
                //MeshRenderer meshRenderer=GameObject.
            }
            else
            {
                if (player.IsGun==1)
                {
                    player.HandWithGun.SetActive(false);
                    player.IsGun = 0;
                }
                               
                player.HandWitObject.SetActive(true);
                MeshRenderer meshRendererHandObject = player.HandWitObject.GetComponent<MeshRenderer>();
                MeshFilter meshFilterHandObject = player.HandWitObject.GetComponent<MeshFilter>();

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
