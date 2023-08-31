using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour
{
    private Vector3 mouseWorldPos => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));  //由相機取得滑鼠的座標

    private bool canClick;

    private ItemName currentItem;   //
    public RectTransform hand;      //手的圖示
    private bool holdItem;      //拿著物品的狀態
    private void OnEnable()
    {
        EventHandler.ItemSelectedEvent += OnItemSelectedEvent;
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
    }
    private void OnDisable()
    {
        EventHandler.ItemSelectedEvent -= OnItemSelectedEvent;
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
    }
    private void OnItemSelectedEvent(ItemDetails itemDetails, bool isSelected)
    {
        holdItem = isSelected;
        if (isSelected)
        {
            currentItem = itemDetails.itemName;
        }
        hand.gameObject.SetActive(holdItem);
    }
    private void OnItemUsedEvent(ItemName obj)
    {
        currentItem = ItemName.None;
        holdItem = false;
        hand.gameObject.SetActive(false);
    }


    private void Update()
    {
        canClick = ObjectAtMousePosition();

        if (hand.gameObject.activeInHierarchy)
        {
            hand.position = Input.mousePosition;
        }


        if (InteractWithUI()) return;


        if(canClick && Input.GetMouseButtonDown(0))
        {
            //檢測滑鼠的互動狀態，可能點到物品，可能空點
            ClickAction(ObjectAtMousePosition().gameObject);
        }
    }
    
    private void ClickAction(GameObject clickObject)
    {
         switch(clickObject.tag)
        {
            case "Teleport":
                var teleport = clickObject.GetComponent<Teleport>();
                teleport?.TeleportToScene();
                break;
            case "Item":
                var item = clickObject.GetComponent<Item>();
                item?.ItemClicked();
                break;
            case "Interactive":
                var interactive = clickObject.GetComponent<Interactive>();
                if (holdItem)
                    interactive?.CheckItem(currentItem);        //如果有拿東西時就傳到 interactive 確認狀態
                else
                    interactive?.EmptyClicked();                //沒有就傳到 空點 狀態 
                break;
        }
    }
    
    
    
    //判斷是否點擊到物品，使用物理碰撞體的區域
    private Collider2D ObjectAtMousePosition()      //點擊後返回值
    {
        return Physics2D.OverlapPoint(mouseWorldPos);    //傳入一個點，點由滑鼠來，需透過相機獲得
    }


    //讓滑鼠對於右下角的 UI 不會按到其他方塊
    private bool InteractWithUI()
    {
        if(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return true;
        
        return false;
    }

}
