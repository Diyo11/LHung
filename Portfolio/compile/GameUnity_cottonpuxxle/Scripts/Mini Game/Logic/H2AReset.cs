using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;      //�ʵe����

public class H2AReset : Interactive
{
    private Transform gearSprite;

    private void Awake()
    {
        gearSprite = transform.GetChild(0);
    }

    public override void EmptyClicked()     //override�л\�������e
    {
        //���m�C��
        GameController.Instance.RestGame();
        gearSprite.DOPunchRotation(Vector3.forward * 180, 1, 1, 0);     //�ĪG
        Debug.Log(gearSprite);
    }
}
