//�n�N inventory Manager ���ƾڶǿ�� inventoryUI��
//�ϥ� �ƥ󤤤� �A�z�L�ƥ�q�\����k�ǻ���U�ӥN�X������Ө��

using System;
using System.ComponentModel.Design;
using UnityEngine;

public static class EventHandler
{
    public static event Action<ItemDetails, int> UpdateUIEvent;     //�ǤJaction�� ���~�P�Ǹ�
    public static void CallUpdateUIEvent(ItemDetails itemDetails, int index)
    {
        UpdateUIEvent?.Invoke(itemDetails, index);
    }

    public static event Action BeforeSceneUnloadEvent;
    public static void CallBeforeSceneUnloadEvent()
    {
        BeforeSceneUnloadEvent?.Invoke();
    }

    public static event Action AfterSceneLoadedEvent;
    public static void CallAfterSceneLoadedEvent()
    {
        AfterSceneLoadedEvent?.Invoke();
    }

    public static event Action<ItemDetails, bool> ItemSelectedEvent;        //*��N�X�ǻ�
    public static void CallItemSelectedEvent(ItemDetails itemDetails, bool isSelected)
    {
        ItemSelectedEvent?.Invoke(itemDetails, isSelected);
    }

    public static event Action<ItemName> ItemUsedEvent;     //�ϥΪ��~�nĲ�o���ƥ�
    public static void CallItemUsedEvent(ItemName itemName)
    {
        ItemUsedEvent?.Invoke(itemName);
    }


    public static event Action<int> ChangeItemEvent;    //�ǤJ�ƭȬ����~�ҹ������Ǹ�
    public static void CallChangeItemEvent(int index)
    {
        ChangeItemEvent?.Invoke(index);
    }

    //CharacterH2
    public static event Action<string> ShowDialogueEvent;
    public static void CallShowDialogueEvent(string dialogue)
    {
        ShowDialogueEvent?.Invoke(dialogue);
    }

    //�C�����A�A����Ȱ�     EX:��ܮɼȰ����i��������
    public static event Action<GameState> GameStateChangeEvent;
    public static void CallGameStateChangeEvent(GameState gameState)
    {
        GameStateChangeEvent?.Invoke(gameState);
    }

    //�ˬd�O�_�ŦX���եΨƥ�
    public static event Action CheckGameStateEvent;
    public static void CallCheckGameStateEvent()
    {
        CheckGameStateEvent?.Invoke();
    }

    //�ǰe���A(?
    public static event Action<string> GamePassEvent;
    public static void CallGamePassEvent(string gameName)
    {
        GamePassEvent?.Invoke(gameName);
    }

    //�}�l�C���ݭn�q���ܦh�a��A���@�Ө�s���ʧ@
    public static event Action<int> StartNewGameEvent;
    public static void CallStarNewGameEvent(int gameWeek)
    {
        StartNewGameEvent?.Invoke(gameWeek);
    }
}
