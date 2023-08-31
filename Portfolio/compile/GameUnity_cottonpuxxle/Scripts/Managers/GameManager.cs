using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveable
{
    public Dictionary<string, bool> miniGameStateDict = new Dictionary<string, bool>();     //製作一個字典，方便擴展

    private int gameWeek;       //存狀態，gameManager要通知minigame
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


        //保存數據
        ISaveable saveable = this;
        saveable.SaveableRegister();
    }

    private void OnAfterSceneLoadEvent()        //切換場景時會執行
    {
        foreach(var miniGame in FindObjectsOfType<MiniGame>())
        {
            if (miniGameStateDict.TryGetValue(miniGame.gameName, out bool isPass))
            {
                miniGame.isPass = isPass;
                miniGame.UpdateMiniGameState();
            }           
        }

        currentGame = FindObjectOfType<GameController>();   //找場景裡是否有minigame
        currentGame?.SetGameWeekData(gameWeek);        //找到就把gameWeek傳進去

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

        //對應數據儲存到對應數據上  //??
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
