using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData_SO", menuName = "Dialogue/DialogueData_SO")]                                      //生成一個unity裡的連結

public class DialogueData_SO : ScriptableObject
{
    public List<string> dialogueList;       //LIST類型str
}

