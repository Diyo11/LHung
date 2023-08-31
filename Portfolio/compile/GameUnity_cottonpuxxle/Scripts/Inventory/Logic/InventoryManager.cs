using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>, ISaveable
{
    public ItemDataList_S0 itemData;

    [SerializeField] private List<ItemName> itemList = new List<ItemName>();

    public void AddItem(ItemName itemName)
    {
        if (!itemList.Contains(itemName))
        {
            itemList.Add(itemName);
            //UI�������
            EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemName), itemList.Count - 1);
        }
    }

    private void OnEnable()
    {
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
        EventHandler.ChangeItemEvent += OnChangeItemEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }
    private void OnDisable()
    {
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
        EventHandler.ChangeItemEvent -= OnChangeItemEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
    }

    private void OnAfterSceneLoadEvent()
    {
        if (itemList.Count == 0)
            EventHandler.CallUpdateUIEvent(null, -1);
        else
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemList[i]), i);
            }
        }
    }

    private void OnChangeItemEvent(int index)
    {
        if(index >= 0 && index < itemList.Count)
        {
            ItemDetails item = itemData.GetItemDetails(itemList[index]);
            EventHandler.CallUpdateUIEvent(item, index);
        }
    }

    private void OnItemUsedEvent(ItemName itemName)
    {
        var index = GetItemIndex(itemName);     //��o�Q�I�������~�Ǹ�
        itemList.Remove(itemName);           //����
        //Debug.Log(index);
        //TODO:�Ȯɨϥγ�@�ϥΪ��~���ĪG
        if (itemList.Count == 0)        //�]�بS���F��
            EventHandler.CallUpdateUIEvent(null, -1);       //��s���A�S���F��?
    }
    private int GetItemIndex(ItemName itemName)     //�I�]�̹������Ǹ�
    {
        for(int i = 0; i< itemList.Count; i++)
        {
            if (itemList[i] == itemName)
                return i;
        }
        return -1;
    }         
    
    private void OnStartNewGameEvent(int obj)
    {
        itemList.Clear();
    }

    private void Start()
    {
        ISaveable savable = this;
        savable.SaveableRegister();
    }


    //--ISaveable
    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.itemList = this.itemList;
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        this.itemList = saveData.itemList;
    }
}
