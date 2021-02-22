using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class SlotController : NetworkBehaviour
{
    [SerializeField] private ScriptableObject prefabObject;
    [SerializeField] private GameObject objectFromScriptableObject = null;
    [SerializeField] private Sprite spriteObject;
    [SerializeField] private float countObject;
    [SerializeField] private KeyCode keyCodeObject;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerMirrorController playerMirrorController;
    [SerializeField] private Transform rightHand;
    [SerializeField] private Transform leftHand;

    public SlotController slotController;


    private void Update()
    {
        SetObjectKeyTrigger();
    }
    private void Start()
    {
        playerMirrorController = this.gameObject.transform.root.GetComponent<PlayerMirrorController>();
        slotController = GetComponent<SlotController>();
    }
    private void DestoryAllObjectInHand(Transform hand)
    {
        foreach (Transform child in hand.transform)
        {
            if (child.name == "ObjectHand(Clone)")
            {
                Destroy(child.gameObject);
            }
        }
    }
    private void SetObjectKeyTrigger()
    {
        if (Input.GetKeyDown(keyCodeObject) && playerMirrorController.isPlayer)
        {
            if (prefabObject == null)
            {
                playerController = this.gameObject.transform.root.GetComponent<PlayerController>();
                playerController.IsGun = 0;
                //DestoryAllObjectInHand(rightHand);
            }
            else if (prefabObject is WeaponeObjectController)
            {
               // DestoryAllObjectInHand(rightHand);
                WeaponeObjectController weaponeObjectController = (WeaponeObjectController)prefabObject;
                playerController = this.gameObject.transform.root.GetComponent<PlayerController>();
                playerController.IsGun = 1;
            }
            else if (prefabObject is HandObjectController)
            {
                //DestoryAllObjectInHand(rightHand);
                HandObjectController handObjectController = (HandObjectController)prefabObject;
                playerController = this.gameObject.transform.root.GetComponent<PlayerController>();
                playerController.IsGun = 0;
            }

            Button buttonSlot = gameObject.transform.Find("Border").GetComponent<Button>();
            buttonSlot.Select();
            buttonSlot.onClick.Invoke();
        }
    }
    public void SetPrefab(ScriptableObject gameObjectPrefab)
    {
        prefabObject = gameObjectPrefab;
    }
    public void SetSprite(Sprite sprite)
    {
        spriteObject = sprite;
        Image spriteimage = gameObject.transform.Find("Border/ItemImage").GetComponent<Image>();
        spriteimage.enabled = true;
        spriteimage.sprite = spriteObject;
    }
    public void SetCount(float count)
    {
        countObject = count;
        Text textCount = gameObject.transform.Find("Border/ItemImage/Count").GetComponent<Text>();
        textCount.text = "" + countObject;
    }
    public void SetKeyCode(KeyCode keyCode)
    {
        keyCodeObject = keyCode;
    }
    public ScriptableObject ReturnPrefab()
    {
        return prefabObject;
    }
    public Sprite ReturnSprite()
    {
        return spriteObject;
    }
    public float ReturnCountObject()
    {
        return countObject;
    }
    public KeyCode ReturnKeyCodeObject()
    {
        return keyCodeObject;
    }
    public GameObject ObjectReturn()
    {
        return objectFromScriptableObject;
    }
}
