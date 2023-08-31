//�s�@�r��
//�x�s�������w���ܩΤ��ʤF������
//�C���ഫ������(����)�n�q�r�夤�P�_�������A

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour, ISaveable 
{
    private Dictionary<ItemName, bool> itemAvailableDict = new Dictionary<ItemName, bool>();

    private Dictionary<string, bool> interactiveStateDict = new Dictionary<string, bool>();     //�O�s���~�����ʪ��A

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

    private void OnBeforeSceneUnloadEvent()     //�����ܴ��e������?
    {
        foreach (var item in FindObjectsOfType<Item>())
        {
            if (!itemAvailableDict.ContainsKey(item.itemName))
                itemAvailableDict.Add(item.itemName, true);           
        }

        foreach (var item in FindObjectsOfType<Interactive>())    //���������~��r�夤
        {
            if (interactiveStateDict.ContainsKey(item.name))    //�r�夤���S���o���~
                interactiveStateDict[item.name] = item.isDone;      //���N��s
            else
                interactiveStateDict.Add(item.name, item.isDone);   //�S���N�s�W
        }
    }
    private void OnAfterSceneLoadEvent()        //�������ܮɱ���B����
    {
        //�p�G�w�g�b�r�夤�h��s��ܪ��A�A���b�h�K�[
        foreach(var item in FindObjectsOfType<Item>())      //FindObjectsOfType ���y������������
        {
            if (!itemAvailableDict.ContainsKey(item.itemName))
                itemAvailableDict.Add(item.itemName, true);
            else
                item.gameObject.SetActive(itemAvailableDict[item.itemName]);
        }

        foreach (var item in FindObjectsOfType<Interactive>())    //��r�夤�����
        {
            if (interactiveStateDict.ContainsKey(item.name))    
                item.isDone = interactiveStateDict[item.name];      //���X
            //else
                //interactiveStateDict.Add(item.name, item.isDone);   
        }
    }

    private void OnUpdateUIEvent(ItemDetails itemDetails, int arg2)     //�u�b�ߨ�F��ɰ���
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
