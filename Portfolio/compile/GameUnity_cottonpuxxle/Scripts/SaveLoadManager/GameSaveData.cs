//保存數據

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveData
{
    public int gameWeek;
    public string currentScene;

    public Dictionary<string, bool> miniGamesStateDict;

    public Dictionary<ItemName, bool> itemAvailableDict;

    public Dictionary<string, bool> interactiveStateDict;

    public List<ItemName> itemList;

}
