using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SceneName] public string sceneFrom;
    [SceneName] public string sceneTogo;

    //�I����Ĳ�o��k
    public void TeleportToScene()
    {
        TransitionManager.Instance.Transition(sceneFrom, sceneTogo);
    }
}
