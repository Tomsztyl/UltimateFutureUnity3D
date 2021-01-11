using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private List<Sprite> itemSpriteItem = new List<Sprite>();
    [SerializeField] private List<int> countItemSprite = new List<int>();
    public List<GameObject> PrefabObjectItem = new List<GameObject>();


    public void SetItem(Sprite sprite,int count,GameObject prefabObject)
    {
        if (ChceckIsItemExistInEq(sprite))
        {
            countItemSprite[GetIndexCount(sprite)]+=count;
            ToolBarController toolBarController = GetComponent<ToolBarController>();
            toolBarController.SetSpriteAndCountToolBar(sprite, countItemSprite[GetIndexCount(sprite)]);
        }
        else
        {
            itemSpriteItem.Add(sprite);
            PrefabObjectItem.Add(prefabObject);
            countItemSprite.Add(count);
            ToolBarController toolBarController = GetComponent<ToolBarController>();
            toolBarController.SetSpriteAndCountToolBar(sprite, count);
        }
    }
    private bool ChceckIsItemExistInEq(Sprite sprite)
    {
       return itemSpriteItem.Exists(x => x.name == sprite.name);
    }
    private int GetIndexCount(Sprite sprite)
    {
        return itemSpriteItem.IndexOf(sprite);
    }
}
