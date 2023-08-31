//製作字典
//儲存場景中已改變或互動了的物件
//每次轉換場景時(卸載)要從字典中判斷場景狀態

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour, ISaveable 
{
    private Dictionary<ItemName, bool> itemAvailableDict = new Dictionary<ItemName, bool>();

    private Dictionary<string, bool> interactiveStateDict = new Dictionary<string, bool>();     //保存物品的互動狀態

    private void OnEnable()
    {
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadEvent;
        EventHandler.UpdateUIEvent += OnUpdateUIEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameevent;
    }

    private void OnDisable()
    {
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent; ;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadEvent;
        EventHandler.UpdateUIEvent -= OnUpdateUIEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameevent;
    }

    private void OnBeforeSceneUnloadEvent()     //場景變換前的改變?
    {
        foreach (var item in FindObjectsOfType<Item>())
        {
            if (!itemAvailableDict.ContainsKey(item.itemName))
                itemAvailableDict.Add(item.itemName, true);           
        }

        foreach (var item in FindObjectsOfType<Interactive>())    //先收集物品到字典中
        {
            if (interactiveStateDict.ContainsKey(item.name))    //字典中有沒有這物品
                interactiveStateDict[item.name] = item.isDone;      //有就更新
            else
                interactiveStateDict.Add(item.name, item.isDone);   //沒有就新增
        }
    }
    private void OnAfterSceneLoadEvent()        //場景改變時掃秒且紀錄
    {
        //如果已經在字典中則更新顯示狀態，不在則添加
        foreach(var item in FindObjectsOfType<Item>())      //FindObjectsOfType 掃描場景中的物件
        {
            if (!itemAvailableDict.ContainsKey(item.itemName))
                itemAvailableDict.Add(item.itemName, true);
            else
                item.gameObject.SetActive(itemAvailableDict[item.itemName]);
        }

        foreach (var item in FindObjectsOfType<Interactive>())    //找字典中的資料
        {
            if (interactiveStateDict.ContainsKey(item.name))    
                item.isDone = interactiveStateDict[item.name];      //取出
            //else
                //interactiveStateDict.Add(item.name, item.isDone);   
        }
    }

    private void OnUpdateUIEvent(ItemDetails itemDetails, int arg2)     //只在撿到東西時執行
    {
        if(itemDetails != null)
        {
            itemAvailableDict[itemDetails.itemName] = false;
        }
    }

    private void OnStartNewGameevent(int obj)
    {
        itemAvailableDict.Clear();
        interactiveStateDict.Clear();
    }

    private void Start()
    {
        ISaveable saveable = this;
        saveable.SaveableRegister();
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.itemAvailableDict = this.itemAvailableDict;
        saveData.interactiveStateDict = this.interactiveStateDict;

        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        this.itemAvailableDict = saveData.itemAvailableDict;
        this.interactiveStateDict = saveData.interactiveStateDict;
    }
}
