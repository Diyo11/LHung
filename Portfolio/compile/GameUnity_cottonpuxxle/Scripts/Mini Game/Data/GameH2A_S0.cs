using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameH2A_S0", menuName = "Mini Game Data/GameH2A_S0")]

public class GameH2A_S0 : ScriptableObject
{
    [SceneName] public string gameName;     //爭對不同場景可以方便擴展

    [Header("球的名稱和對應圖片")]       //[unity可視文字]方便其他人(美術)做更改
    public List<BallDetails> ballDataList;
    public BallDetails GetBallDetails(BallName ballName)
    {
        return ballDataList.Find(b => b.ballName == ballName);
    }


    [Header("遊戲邏輯設計")]
    public List<Conections> lineConections;
    public List<BallName> startBallOrder;
}

[System.Serializable]       //資源共享器(??
public class BallDetails
{
    public BallName ballName;

    public Sprite wrongSprite;
    public Sprite rightSprite;
}

[System.Serializable]
public class Conections
{
    public int from;
    public int to;
}
