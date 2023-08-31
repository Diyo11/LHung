using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SceneName] public string sceneFrom;
    [SceneName] public string sceneTogo;

    //點擊的觸發方法
    public void TeleportToScene()
    {
        TransitionManager.Instance.Transition(sceneFrom, sceneTogo);
    }
}
