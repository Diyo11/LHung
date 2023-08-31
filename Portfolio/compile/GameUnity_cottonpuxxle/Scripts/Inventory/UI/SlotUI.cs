using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;       //介面

using UnityEngine.EventSystems;     //鼠標動作

public class SlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image itemImage;
    private ItemDetails currentItem;    //儲存當前物品訊息
    private bool isSelected;        //是否被選中

    //鼠標拿東西圖示
    public ItemTooltip tooltip;

    public void SetItem(ItemDetails itemDetails)        //接收傳遞的訊息
    {
        currentItem = itemDetails;
        this.gameObject.SetActive(true);
        itemImage.sprite = itemDetails.itemSprite;
        itemImage.SetNativeSize();
    }

    public void SetEmpty()  //使用完設置為空
    {
        this.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)  //滑鼠點擊
    {
        isSelected = !isSelected;
        EventHandler.CallItemSelectedEvent(currentItem, isSelected);
    }

    public void OnPointerEnter(PointerEventData eventData)  //滑鼠滑入
    {
        if (this.gameObject.activeInHierarchy)
        {
            tooltip.gameObject.SetActive(true);
            tooltip.UpdateItemName(currentItem.itemName);
        }
    }

    public void OnPointerExit(PointerEventData eventData)   //滑鼠滑出
    {
        tooltip.gameObject.SetActive(false);
    }
}
