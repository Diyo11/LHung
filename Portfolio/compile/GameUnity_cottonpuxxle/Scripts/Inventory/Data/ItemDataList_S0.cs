using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataList_S0", menuName = "Inventory/ItemDataList_S0")]     //生成一個unity裡的連結

public class ItemDataList_S0 : ScriptableObject
{
    public List<ItemDetails> itemDetailsList;
    public ItemDetails GetItemDetails(ItemName itemName)      //獲得物品的訊息，順便可以獲得圖片
    {
        return itemDetailsList.Find(i => i.itemName == itemName);
    }
}

[System.Serializable]
public class ItemDetails        //儲存物品的訊息
{
    public ItemName itemName;
    public Sprite itemSprite;
}

