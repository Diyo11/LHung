using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;       //����

using UnityEngine.EventSystems;     //���аʧ@

public class SlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image itemImage;
    private ItemDetails currentItem;    //�x�s��e���~�T��
    private bool isSelected;        //�O�_�Q�襤

    //���Ю��F��ϥ�
    public ItemTooltip tooltip;

    public void SetItem(ItemDetails itemDetails)        //�����ǻ����T��
    {
        currentItem = itemDetails;
        this.gameObject.SetActive(true);
        itemImage.sprite = itemDetails.itemSprite;
        itemImage.SetNativeSize();
    }

    public void SetEmpty()  //�ϥΧ��]�m����
    {
        this.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)  //�ƹ��I��
    {
        isSelected = !isSelected;
        EventHandler.CallItemSelectedEvent(currentItem, isSelected);
    }

    public void OnPointerEnter(PointerEventData eventData)  //�ƹ��ƤJ
    {
        if (this.gameObject.activeInHierarchy)
        {
            tooltip.gameObject.SetActive(true);
            tooltip.UpdateItemName(currentItem.itemName);
        }
    }

    public void OnPointerExit(PointerEventData eventData)   //�ƹ��ƥX
    {
        tooltip.gameObject.SetActive(false);
    }
}
