using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveable
{
    public Dictionary<string, bool> miniGameStateDict = new Dictionary<string, bool>();     //�s�@�@�Ӧr��A��K�X�i

    private int gameWeek;       //�s���A�AgameManager�n�q��minigame
    private GameController currentGame; 

    private void OnEnable()
    {
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadEvent;
        EventHandler.GamePassEvent += OnGamePassEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }
    private void OnDisable()
    {
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadEvent;
        EventHandler.GamePassEvent -= OnGamePassEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;

    }

    void Start()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        EventHandler.CallGameStateChangeEvent(GameState.GamePlay);


        //�O�s�ƾ�
        ISaveable saveable = this;
        saveable.SaveableRegister();
    }

    private void OnAfterSceneLoadEvent()        //���������ɷ|����
    {
        foreach(var miniGame in FindObjectsOfType<MiniGame>())
        {
            if (miniGameStateDict.TryGetValue(miniGame.gameName, out bool isPass))
            {
                miniGame.isPass = isPass;
                miniGame.UpdateMiniGameState();
            }           
        }

        currentGame = FindObjectOfType<GameController>();   //������̬O�_��minigame
        currentGame?.SetGameWeekData(gameWeek);        //���N��gameWeek�Ƕi�h

    }

    private  void OnGamePassEvent(string gameName)
    {
        miniGameStateDict[gameName] = true;
    }

    private void OnStartNewGameEvent(int gameWeek)
    {
        this.gameWeek = gameWeek;
        miniGameStateDict.Clear();
    }


    //-----ISaveable
    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();

        //�����ƾ��x�s������ƾڤW  //??
        saveData.gameWeek = this.gameWeek;
        saveData.miniGamesStateDict = this.miniGameStateDict;   

        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        this.gameWeek = saveData.gameWeek;
        this.miniGameStateDict = saveData.miniGamesStateDict;
    }
}
