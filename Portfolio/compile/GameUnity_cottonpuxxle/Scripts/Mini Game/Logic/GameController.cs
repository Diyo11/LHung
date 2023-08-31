using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class GameController : Singleton<GameController>
{
    [Header ("�C���ƾ�")]
    public GameH2A_S0 gameData;         //�u��s�@�P
    public GameH2A_S0[] gameDataArry;     //�ҥH�令��s�ܦh�P�A�A����gameData

    //�ͦ�
    public GameObject lineParent;
    public LineRenderer linePrefab;
    public Ball ballPrefab;

    public Transform[] holderTransforms;


    [Header("------------")]
    public UnityEvent OnFinish;


    private void Start()
    {
        DrawLine();
        CreateBall();
    }

    private void OnEnable()
    {
        EventHandler.CheckGameStateEvent += OnCheckGameStateEvent;
        Debug.Log("OnEnable");
    }
    private void OnDisable()
    {
        EventHandler.CheckGameStateEvent -= OnCheckGameStateEvent;
        Debug.Log("Disable");
    }
    private void OnCheckGameStateEvent()
    {
        foreach(var ball in FindObjectsOfType<Ball>())
        {
            if (!ball.isMatch)
                return;
        }

        Debug.Log("END");
        foreach(var holder in holderTransforms)
        {
            holder.GetComponent<Collider2D>().enabled = false;
        }

        EventHandler.CallGamePassEvent(gameData.gameName);

        OnFinish?.Invoke();
    }


    //���m����
    public void RestGame()
    {
        for(int i =0; i< lineParent.transform.childCount; i++)
        {
            Destroy(lineParent.transform.GetChild(i).gameObject);
        }
        foreach(var holder in holderTransforms)
        {
            if (holder.childCount > 0)
                Destroy(holder.GetChild(0).gameObject);
        }

        DrawLine();
        CreateBall();
    }


    public void DrawLine()
    {
        foreach (var conections in gameData.lineConections)
        {
            var line = Instantiate(linePrefab, lineParent.transform);     //�ͦ��u
            line.SetPosition(0, holderTransforms[conections.from].position);
            line.SetPosition(1, holderTransforms[conections.to].position);

            //�ЫبC��Holder���s�����Y
            holderTransforms[conections.from].GetComponent<Holder>().linkHolders.Add(holderTransforms[conections.to].GetComponent<Holder>());
            holderTransforms[conections.to].GetComponent<Holder>().linkHolders.Add(holderTransforms[conections.from].GetComponent<Holder>());

        }
    }

    public void CreateBall()
    {
        for(int i = 0; i<gameData.startBallOrder.Count; i++)
        {
            if (gameData.startBallOrder[i] == BallName.None)
            {
                holderTransforms[i].GetComponent<Holder>().isEmpty = true;
                continue;
            }
            Ball ball = Instantiate(ballPrefab, holderTransforms[i]);

            holderTransforms[i].GetComponent<Holder>().CheckBall(ball);     //��holder���D�ثe���y�O�ƻ�
            holderTransforms[i].GetComponent<Holder>().isEmpty = false;
            ball.SetupBall(gameData.GetBallDetails(gameData.startBallOrder[i]));
        }
    }


    public void SetGameWeekData(int week)
    {
        gameData = gameDataArry[week];
        DrawLine();
        CreateBall();
    }
}
