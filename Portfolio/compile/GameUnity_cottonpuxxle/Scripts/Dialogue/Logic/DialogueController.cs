using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public DialogueData_SO dialogueEmpty;
    public DialogueData_SO dialogueFinish;

    private bool isTalking;

    private Stack<string> dialogueEmptyStack;       //���|�k�ŧi(?  "���i��X" ���ƧǪk
    private Stack<string> dialogueFinishStack;


    private void Awake()
    {
        FillDialogueStack();
    }

    private void FillDialogueStack()        //���|�k��k(?  "���i��X" ���ƧǪk
    {
        dialogueEmptyStack = new Stack<string>();
        dialogueFinishStack = new Stack<string>();

        for(int i = dialogueEmpty.dialogueList.Count -1; i > -1; i--)       //count �Ӽ�
        {
            dialogueEmptyStack.Push(dialogueEmpty.dialogueList[i]);
        }
        for (int i = dialogueFinish.dialogueList.Count - 1; i > -1; i--)
        {
            dialogueFinishStack.Push(dialogueFinish.dialogueList[i]);
        }
    }

    public void ShowDialogueEmpty()
    {
        if (!isTalking)
            StartCoroutine(DialogueRoutine(dialogueEmptyStack));
    }
    public void ShowDialogueFinish()
    {
        if (!isTalking)
            StartCoroutine(DialogueRoutine(dialogueFinishStack));
    }
    private IEnumerator DialogueRoutine(Stack<string> data)
    {
        isTalking = true;
        if(data.TryPop(out string result))
        {
            EventHandler.CallShowDialogueEvent(result);
            yield return null;
            isTalking = false;
            EventHandler.CallGameStateChangeEvent(GameState.Pause);
        }
        else
        {
            EventHandler.CallShowDialogueEvent(string.Empty);
            FillDialogueStack();
            isTalking = false;
            EventHandler.CallGameStateChangeEvent(GameState.GamePlay);
        }
    }
}
