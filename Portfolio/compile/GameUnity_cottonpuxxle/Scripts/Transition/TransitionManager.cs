using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;  //�������[���M����

public class TransitionManager : Singleton<TransitionManager>,ISaveable   //Singleton<TransitionManager>���ܦ���@�~��
{
    [SceneName] public string startScene;

    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration;          //��������ɶ� *�H�X�ɶ�

    private bool isFade;//�P�_�O�_������A

    private bool canTransition;     //�C�����A�A����Ȱ�     EX:��ܮɼȰ����i��������

    private void Start()
    {
        //���Usaveable
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
        yield return Fade(1);                                                       //yield return �|���ݰ��槹���~�i��U�@�ӫ��O
        if(from !=string.Empty)
        {
            EventHandler.CallBeforeSceneUnloadEvent();      //�I�s�s�r��

            yield return SceneManager.UnloadSceneAsync(from);   //�����ƻ�
        }
        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);   //�[���ƻ�

        //�]�m�s�������E������
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);

        EventHandler.CallAfterSceneLoadedEvent();

        yield return Fade(0);
    }

    //��������I�ĪG
    //"targetAlpha" 1�O�¡A0�O�z��
    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;

        fadeCanvasGroup.blocksRaycasts = true;

        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;        //(��e - ����)/  �H�X�ɶ�
        //float speed = 3.3f;
        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))         //Approximately �P�_��ȬO�_�ۦ�
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
