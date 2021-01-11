using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMaterial : MonoBehaviour
{
    [SerializeField] private HandController hanController=null;   
    [SerializeField] private Sprite spritePickUpObject=null;
    [SerializeField] private GameObject prefabPickUp = null;

    [SerializeField] private bool isPistol = false;
    [SerializeField] private bool isRifle = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            if (this.tag=="Gun")
            {
                InventoryController inventoryController = GameObject.Find("Canvas/InventoryPanel").GetComponent<InventoryController>();
                inventoryController.SetItem(spritePickUpObject, 1, prefabPickUp);
                Destroy(this.gameObject);
            }
            else
            {
                InventoryController inventoryController = GameObject.Find("Canvas/InventoryPanel").GetComponent<InventoryController>();
                inventoryController.SetItem(spritePickUpObject, 1, prefabPickUp);
                Destroy(this.gameObject);
            }
        }
    }
}
