using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;  //場景的加載和卸載

public class TransitionManager : Singleton<TransitionManager>,ISaveable   //Singleton<TransitionManager>改變成單一繼承
{
    [SceneName] public string startScene;

    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration;          //控制切換時間 *淡出時間

    private bool isFade;//判斷是否轉場狀態

    private bool canTransition;     //遊戲狀態，控制暫停     EX:對話時暫停不可切換場景

    private void Start()
    {
        //註冊saveable
        ISaveable saveable = this;
        saveable.SaveableRegister();
    }

    private void OnEnable()
    {
        EventHandler.GameStateChangeEvent += OnGameStateChangeEvent;
        EventHandler.StartNewGameEvent += OnStarNewGameEvent;
    }
    private void OnDisable()
    {
        EventHandler.GameStateChangeEvent -= OnGameStateChangeEvent;
        EventHandler.StartNewGameEvent -= OnStarNewGameEvent;
    }
    private void OnGameStateChangeEvent(GameState gameState)
    {
        canTransition = gameState == GameState.GamePlay;
    }
    private void OnStarNewGameEvent(int obj)
    {
        StartCoroutine(TransitionToScene("Menu", startScene));
    }


    public void Transition(string from, string to)
    {
        if(!isFade && canTransition)
            StartCoroutine(TransitionToScene(from, to));
    }

    private IEnumerator TransitionToScene(string from, string to)
    {
        yield return Fade(1);                                                       //yield return 會等待執行完畢才進行下一個指令
        if(from !=string.Empty)
        {
            EventHandler.CallBeforeSceneUnloadEvent();      //呼叫存字典

            yield return SceneManager.UnloadSceneAsync(from);   //卸載甚麼
        }
        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);   //加載甚麼

        //設置新場景為激活場景
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);

        EventHandler.CallAfterSceneLoadedEvent();

        yield return Fade(0);
    }

    //讓轉場有點效果
    //"targetAlpha" 1是黑，0是透明
    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;

        fadeCanvasGroup.blocksRaycasts = true;

        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;        //(當前 - 對應)/  淡出時間
        //float speed = 3.3f;
        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))         //Approximately 判斷兩值是否相似
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);      //
            yield return null;
        }

        fadeCanvasGroup.blocksRaycasts = false;

        isFade = false;
    }


    //--ISaveable
    GameSaveData ISaveable.GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.currentScene = SceneManager.GetActiveScene().name;
        return saveData;
    }

    void ISaveable.RestoreGameData(GameSaveData saveData)
    {
        Transition("Menu", saveData.currentScene);
    }
}
