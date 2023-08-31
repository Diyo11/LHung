using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemName itemName;

    public void ItemClicked()
    {
        //添加到背包裡並隱藏物體
        InventoryManager.Instance.AddItem(itemName);    //直接呼叫添加
        this.gameObject.SetActive(false);
    }
}
