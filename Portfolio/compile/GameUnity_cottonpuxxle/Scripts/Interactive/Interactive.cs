//對應的互動物品是否正確

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public ItemName requireItem;    //互動的物品名稱
    public bool isDone;     //互動是否完成了

    public void CheckItem(ItemName itemName)
    {
        if (itemName == requireItem && !isDone)
        {
            isDone = true;
            //使用物品，移除物品
            OnClickedAction();
            EventHandler.CallItemUsedEvent(itemName);
        }
    }
    

    //默認是正確物品的情況執行
    protected virtual void OnClickedAction()
    {

    }

    public virtual void EmptyClicked()
    {
        Debug.Log("Empty");
    }

}
