using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueController))]

public class CharacterH2 : Interactive
{
    private DialogueController dialogueController;

    private void Awake()
    {
        dialogueController = GetComponent<DialogueController>();
    }

    public override void EmptyClicked()         //繼承父類
    {      
        if (isDone)
            dialogueController.ShowDialogueFinish();
        else
            dialogueController.ShowDialogueEmpty();     //對話內容A
    }
    protected override void OnClickedAction()   //繼承父類
    {
        //對話內容B
        dialogueController.ShowDialogueFinish();
    }
}
