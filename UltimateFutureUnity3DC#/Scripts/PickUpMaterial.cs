using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMaterial : MonoBehaviour
{
    [SerializeField] private HandController hanController=null;
    

    [SerializeField] private Sprite spritePickUpObject=null;
    [SerializeField] private float idItem;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            if (this.tag=="Gun")
            {
                Debug.Log("Pick up Gun");
                PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                MeshRenderer objcGun = GetComponent<MeshRenderer>();
                player.HandWithGun.SetActive(true);
                player.IsGun = 1;
                hanController = GameObject.FindGameObjectWithTag("Hand").GetComponent<HandController>();
                hanController.ChangeMaterialGun(objcGun.sharedMaterial);
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("Pick up Random Material");
                ToolBarController toolBarController = GameObject.Find("Canvas/InventoryPanel").GetComponent<ToolBarController>();
                toolBarController.SetImageItem(spritePickUpObject, true);
                Destroy(this.gameObject);
            }
        }
    }
}
