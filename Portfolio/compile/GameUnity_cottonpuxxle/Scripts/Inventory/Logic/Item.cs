using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemName itemName;

    public void ItemClicked()
    {
        //�K�[��I�]�̨����ê���
        InventoryManager.Instance.AddItem(itemName);    //�����I�s�K�[
        this.gameObject.SetActive(false);
    }
}
