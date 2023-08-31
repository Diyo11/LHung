using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour
{
    private Vector3 mouseWorldPos => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));  //�Ѭ۾����o�ƹ����y��

    private bool canClick;

    private ItemName currentItem;   //
    public RectTransform hand;      //�⪺�ϥ�
    private bool holdItem;      //���۪��~�����A
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
            //�˴��ƹ������ʪ��A�A�i���I�쪫�~�A�i����I
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
                    interactive?.CheckItem(currentItem);        //�p�G�����F��ɴN�Ǩ� interactive �T�{���A
                else
                    interactive?.EmptyClicked();                //�S���N�Ǩ� ���I ���A 
                break;
        }
    }
    
    
    
    //�P�_�O�_�I���쪫�~�A�ϥΪ��z�I���骺�ϰ�
    private Collider2D ObjectAtMousePosition()      //�I�����^��
    {
        return Physics2D.OverlapPoint(mouseWorldPos);    //�ǤJ�@���I�A�I�ѷƹ��ӡA�ݳz�L�۾���o
    }


    //���ƹ����k�U���� UI ���|�����L���
    private bool InteractWithUI()
    {
        if(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return true;
        
        return false;
    }

}
