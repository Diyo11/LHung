//�ϥ� newtonsoft-json ����     //com.unity.nuget.newtonsoft-json
//�u�I�i�H�ǦC�ƦC��B�r��

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;
using System.IO;    //�g�ƾ�

public class SaveLoadManager : Singleton<SaveLoadManager>
{
    private string jsonFolder;

    private List<ISaveable> saveableList = new List<ISaveable>();
    private Dictionary<string, GameSaveData> saveDataDict = new Dictionary<string, GameSaveData>();     //�x�s�������ƾ�


    protected override void Awake()
    {
        base.Awake();
        jsonFolder = Application.persistentDataPath + "/SAVE/";     //�s���m  //"persistentDataPath"���P���x(win,os)�Ϊk���O���@��
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
        var resultPath = jsonFolder + "data.sav";//��o�̲׸��|
        if(File.Exists(resultPath))     //���L�o���
        {
            File.Delete(resultPath);    //�R��    //�s�C���|�л\�¶i��
        }
    }


    public void Register(ISaveable saveable)
    {
        saveableList.Add(saveable);
    }


    public void Save()
    {
        saveDataDict.Clear();   //�M�ŽT�O���|�s��O�����

        foreach(var saveable in saveableList)
        {
            saveDataDict.Add(saveable.GetType().Name, saveable.GenerateSaveData());       //�z�LGetType��omanager���W��
        }

        var resultPath = jsonFolder + "data.sav";   //���|
        var jsonData = JsonConvert.SerializeObject(saveDataDict, Formatting.Indented);  //���e�ǦC(?
        if(!File.Exists(resultPath))
        {
            Directory.CreateDirectory(jsonFolder);  //�Ыؤ��
        }

        File.WriteAllText(resultPath, jsonData);    //�g�J���
        Debug.Log("SAVE");
    }

    public void Load()
    {
        var resultPath = jsonFolder + "data.sav";

        if (!File.Exists(resultPath)) return;

        var stringData = File.ReadAllText(resultPath);  //�b���|�ɮ׸�Ū���X
        var jsonData = JsonConvert.DeserializeObject<Dictionary<string, GameSaveData>>(stringData);     //DeserializeObject<Dictionary<string, GameSaveData>>�����j���ܦ�"�r��"���A
        
        foreach(var saveable in saveableList)   //�j���ƾ�
        {
            saveable.RestoreGameData(jsonData[saveable.GetType().Name]);    //��_�ƾڤ��e
        }
        Debug.Log("Load");
    }

    void Start()
    {
        Debug.Log(Application.persistentDataPath);  //C:/Users/ghilk/AppData/LocalLow/DefaultCompany/CottonPuxxle
    }
}
