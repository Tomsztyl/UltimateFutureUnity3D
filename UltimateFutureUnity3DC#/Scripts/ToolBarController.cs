using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolBarController : MonoBehaviour
{
    [SerializeField] private List<GameObject> slot_Inventory = new List<GameObject>();
    [SerializeField] private List<KeyCode> slot_InventoryKey = new List<KeyCode>();
    [SerializeField] private List<Image> slot_Inventory_ItemImage = new List<Image>();
    //[SerializeField] private List<Text> slot_Inventory_ItemText = new List<Text>();


    //Slot Inventory Full Box
    [SerializeField] private List<GameObject> slot_Inventory_Box = new List<GameObject>();

    public Text text;

    private void Start()
    {
        //Find GameObjcect Item Image
        FindImageItem();
    }

    private void FindImageItem()
    {
        for (int i=0;i<slot_Inventory.Count;i++)
        {
            slot_Inventory_ItemImage.Add(GameObject.Find(slot_Inventory[i].name + "/Border/ItemImage").GetComponent<Image>());
           // slot_Inventory_ItemText.Add(GameObject.Find(slot_Inventory[i].name + "/Border/Count").GetComponent<Text>());
        }
    }
    public void SetImageItem(Sprite ItemImageSet,bool pickUpItem)
    {
        if (pickUpItem==true)
        {
            //ChceckIsExistItem(ItemImageSet);
            CheckIsEmptySlot(ItemImageSet);
        }
        else
        {
            Debug.Log("Drop Item: " + ItemImageSet);
        }

    }
    private void CheckIsEmptySlot(Sprite ItemImageSet)
    {
        if (ChceckIsExistItem(ItemImageSet) !=null)
        {
            //Object it is Exsist
            text = GameObject.Find(ChceckIsExistItem(ItemImageSet).name+ "/Count").GetComponent<Text>();
            //ChceckIsExistItem(ItemImageSet)
        }
        else
        {
            //Object is not Exist
        }
        for (int i = 0; i < slot_Inventory.Count; i++)
        {
            //Debug.Log(slot_Inventory_ItemImage[i].sprite);
            if (slot_Inventory_ItemImage[i].sprite == null)
            {
                Debug.Log(slot_Inventory_ItemImage[i] + "Is empty");
                slot_Inventory_ItemImage[i].enabled = true;
                slot_Inventory_ItemImage[i].sprite = ItemImageSet;
                return;
            }
        }
    }
    private Image ChceckIsExistItem(Sprite ItemImageSet)
    {
        for (int i = 0; i < slot_Inventory.Count; i++)
        {
            //Debug.Log(slot_Inventory_ItemImage[i].sprite);
            if (slot_Inventory_ItemImage[i].sprite == ItemImageSet)
            {
                return slot_Inventory_ItemImage[i];
            }
            else
            {
                return null;
            }
        }
        return null;
    }
}
