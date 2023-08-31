using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataList_S0", menuName = "Inventory/ItemDataList_S0")]     //�ͦ��@��unity�̪��s��

public class ItemDataList_S0 : ScriptableObject
{
    public List<ItemDetails> itemDetailsList;
    public ItemDetails GetItemDetails(ItemName itemName)      //��o���~���T���A���K�i�H��o�Ϥ�
    {
        return itemDetailsList.Find(i => i.itemName == itemName);
    }
}

[System.Serializable]
public class ItemDetails        //�x�s���~���T��
{
    public ItemName itemName;
    public Sprite itemSprite;
}

