//使用 newtonsoft-json 插件     //com.unity.nuget.newtonsoft-json
//優點可以序列化列表、字典

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;
using System.IO;    //寫數據

public class SaveLoadManager : Singleton<SaveLoadManager>
{
    private string jsonFolder;

    private List<ISaveable> saveableList = new List<ISaveable>();
    private Dictionary<string, GameSaveData> saveDataDict = new Dictionary<string, GameSaveData>();     //儲存對應的數據


    protected override void Awake()
    {
        base.Awake();
        jsonFolder = Application.persistentDataPath + "/SAVE/";     //存放位置  //"persistentDataPath"不同平台(win,os)用法指令不一樣
    }

    private void OnEnable()
    {
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }
    private void OnDisable()
    {
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
    }
    private void OnStartNewGameEvent(int obj)
    {
        var resultPath = jsonFolder + "data.sav";//獲得最終路徑
        if(File.Exists(resultPath))     //有無這文件
        {
            File.Delete(resultPath);    //刪除    //新遊戲會覆蓋舊進度
        }
    }


    public void Register(ISaveable saveable)
    {
        saveableList.Add(saveable);
    }


    public void Save()
    {
        saveDataDict.Clear();   //清空確保不會存到別的資料

        foreach(var saveable in saveableList)
        {
            saveDataDict.Add(saveable.GetType().Name, saveable.GenerateSaveData());       //透過GetType獲得manager的名稱
        }

        var resultPath = jsonFolder + "data.sav";   //路徑
        var jsonData = JsonConvert.SerializeObject(saveDataDict, Formatting.Indented);  //內容序列(?
        if(!File.Exists(resultPath))
        {
            Directory.CreateDirectory(jsonFolder);  //創建文件夾
        }

        File.WriteAllText(resultPath, jsonData);    //寫入文件
        Debug.Log("SAVE");
    }

    public void Load()
    {
        var resultPath = jsonFolder + "data.sav";

        if (!File.Exists(resultPath)) return;

        var stringData = File.ReadAllText(resultPath);  //在路徑檔案裡讀取出
        var jsonData = JsonConvert.DeserializeObject<Dictionary<string, GameSaveData>>(stringData);     //DeserializeObject<Dictionary<string, GameSaveData>>直接強制變成"字典"型態
        
        foreach(var saveable in saveableList)   //搜索數據
        {
            saveable.RestoreGameData(jsonData[saveable.GetType().Name]);    //恢復數據內容
        }
        Debug.Log("Load");
    }

    void Start()
    {
        Debug.Log(Application.persistentDataPath);  //C:/Users/ghilk/AppData/LocalLow/DefaultCompany/CottonPuxxle
    }
}
