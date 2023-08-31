using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameH2A_S0", menuName = "Mini Game Data/GameH2A_S0")]

public class GameH2A_S0 : ScriptableObject
{
    [SceneName] public string gameName;     //���藍�P�����i�H��K�X�i

    [Header("�y���W�٩M�����Ϥ�")]       //[unity�i����r]��K��L�H(���N)�����
    public List<BallDetails> ballDataList;
    public BallDetails GetBallDetails(BallName ballName)
    {
        return ballDataList.Find(b => b.ballName == ballName);
    }


    [Header("�C���޿�]�p")]
    public List<Conections> lineConections;
    public List<BallName> startBallOrder;
}

[System.Serializable]       //�귽�@�ɾ�(??
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
