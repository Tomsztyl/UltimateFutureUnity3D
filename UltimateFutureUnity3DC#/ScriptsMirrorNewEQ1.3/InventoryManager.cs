using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : NetworkBehaviour
{
    [SerializeField] private GameObject toolBar;
    [SerializeField] private List<GameObject> slotToolBar = new List<GameObject>();

    [SerializeField] private GameObject equipmentBox;
    [SerializeField] private List<GameObject> slotEquipmentBox = new List<GameObject>();




    private void Start()
    {
        FindAllSlot();
    }



    private void FindAllSlot()
    {
        foreach (Transform child in toolBar.transform)
        {
            slotToolBar.Add(child.gameObject);
        }
        foreach (Transform child in equipmentBox.transform)
        {
            slotEquipmentBox.Add(child.gameObject);
        }
    }
    private GameObject CheckObjectItExist(ScriptableObject gameObject, Sprite sprite, List<GameObject> listGameObject)
    {
        foreach (GameObject slotControllerObj in listGameObject)
        {
            SlotController slotController = slotControllerObj.GetComponent<SlotController>();
            if (slotController.ReturnPrefab() == gameObject && slotController.ReturnSprite() == sprite)
                return slotControllerObj;
        }
        return null;
    }
    private GameObject FindSortEquipObject(List<GameObject> listGameObject)
    {
        foreach (GameObject slotControllerObj in listGameObject)
        {
            SlotController slotController = slotControllerObj.GetComponent<SlotController>();
            if (slotController.ReturnPrefab() == null && slotController.ReturnSprite() == null)
                return slotControllerObj;
        }
        return null;
    }
    public bool IsExistObject(ScriptableObject scriptableObject, Sprite spriteObj)
    {
        //Check First Sloot ToolBar
        if (CheckObjectItExist(scriptableObject, spriteObj, slotToolBar) != null) { return true; }
        //Check Second Sloot Tool Box
        if (CheckObjectItExist(scriptableObject, spriteObj, slotEquipmentBox) != null) { return true; }
        return false;
    }
    public bool IsHavePlaceInEq()
    {
        //Check First Sloot ToolBar
        if (FindSortEquipObject(slotToolBar) != null) { return true; }
        //Check Second Sloot Tool Box
        if (FindSortEquipObject(slotEquipmentBox) != null) { return true; }
        return false;
    }
    public void SetObjectIsExist(ScriptableObject scriptableObject, Sprite spriteObj, float countObj)
    {
        if (CheckObjectItExist(scriptableObject, spriteObj, slotToolBar) != null)
        {
            SlotController slotController = CheckObjectItExist(scriptableObject, spriteObj, slotToolBar).GetComponent<SlotController>();
            slotController.SetCount(slotController.ReturnCountObject() + countObj);
            //SendToServerObjectToolBar(slotController, scriptableObject, spriteObj, countObj);
        }
        else if (CheckObjectItExist(scriptableObject, spriteObj, slotEquipmentBox) != null)
        {
            SlotController slotController = CheckObjectItExist(scriptableObject, spriteObj, slotEquipmentBox).GetComponent<SlotController>();
            slotController.SetCount(slotController.ReturnCountObject() + countObj);
            //SendToServerObjectToolBar(slotController, scriptableObject, spriteObj, countObj);
        }
    }
    public void SetObjectIsHavePlace(ScriptableObject scriptableObject, Sprite spriteObj, float countObj)
    {
        if (FindSortEquipObject(slotToolBar) != null)
        {
            SlotController slotController = FindSortEquipObject(slotToolBar).GetComponent<SlotController>();
            slotController.SetPrefab(scriptableObject);
            slotController.SetSprite(spriteObj);
            slotController.SetCount(countObj);
            //SendToServerObjectToolBar(slotController, scriptableObject, spriteObj, countObj);
        }
        else if (FindSortEquipObject(slotEquipmentBox) != null)
        {
            SlotController slotController = FindSortEquipObject(slotEquipmentBox).GetComponent<SlotController>();
            slotController.SetPrefab(scriptableObject);
            slotController.SetSprite(spriteObj);
            slotController.SetCount(countObj);
            //SendToServerObjectToolBar(slotController, scriptableObject,spriteObj,countObj);
        }
    }
}
