using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;      //動畫插件

public class H2AReset : Interactive
{
    private Transform gearSprite;

    private void Awake()
    {
        gearSprite = transform.GetChild(0);
    }

    public override void EmptyClicked()     //override覆蓋父類內容
    {
        //重置遊戲
        GameController.Instance.RestGame();
        gearSprite.DOPunchRotation(Vector3.forward * 180, 1, 1, 0);     //效果
        Debug.Log(gearSprite);
    }
}
