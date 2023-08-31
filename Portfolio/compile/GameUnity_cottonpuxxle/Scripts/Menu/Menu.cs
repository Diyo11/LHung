using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }


    public void ContinueGame()
    {
        //加載遊戲數據
        SaveLoadManager.Instance.Load();
        Debug.Log("Continue");
    }


    public void GoBackToMenu()
    {
        var currentScene = SceneManager.GetActiveScene().name;
        TransitionManager.Instance.Transition(currentScene, "Menu");

        //保存進度
        SaveLoadManager.Instance.Save();
    }


    public void StartGameWeek(int gameWeek)
    {
        EventHandler.CallStarNewGameEvent(gameWeek);
    }
}
