//���������ʪ��~�O�_���T

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public ItemName requireItem;    //���ʪ����~�W��
    public bool isDone;     //���ʬO�_�����F

    public void CheckItem(ItemName itemName)
    {
        if (itemName == requireItem && !isDone)
        {
            isDone = true;
            //�ϥΪ��~�A�������~
            OnClickedAction();
            EventHandler.CallItemUsedEvent(itemName);
        }
    }
    

    //�q�{�O���T���~�����p����
    protected virtual void OnClickedAction()
    {

    }

    public virtual void EmptyClicked()
    {
        Debug.Log("Empty");
    }

}
