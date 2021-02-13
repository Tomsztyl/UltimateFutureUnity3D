using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMaterial : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;

    [SerializeField] private Sprite spritePickUpObject = null;
    [SerializeField] private ScriptableObject prefabPickUp = null;
    [SerializeField] [Range(1, 10)] private float countObject = 1;

    [SerializeField] private bool isPistol = false;
    [SerializeField] private bool isRifle = false;

    private void Start()
    {
        //Random coundObject
        countObject = Random.Range(1, 10);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inventoryManager = other.transform.Find("Canvas/InventoryManager").GetComponent<InventoryManager>();
            if (this.tag == "Gun")
            {

                CheckObjectEq();
                Destroy(this.gameObject);
            }
            else
            {
                CheckObjectEq();
                Destroy(this.gameObject);
            }
        }
    }
    public void CheckObjectEq()
    {
        if (inventoryManager.IsExistObject(prefabPickUp, spritePickUpObject))
        {
            //It is Exist in Equipment
            inventoryManager.SetObjectIsExist(prefabPickUp, spritePickUpObject, countObject);
        }
        else if (inventoryManager.IsHavePlaceInEq())
        {
            //Is Pleace In Equipment
            inventoryManager.SetObjectIsHavePlace(prefabPickUp, spritePickUpObject, countObject);
        }
        else
        {
            Debug.LogWarning("Object no exist and you don't have slot in EQ");
        }
    }
}
