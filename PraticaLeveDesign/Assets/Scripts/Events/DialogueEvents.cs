using UnityEngine;
using System;
using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;

public class DialogueEvents
{
    public event Action<string> onEnterDialogue;

    public void EnterDialogue(string knotName)
    {
        if (onEnterDialogue != null)
        {
            onEnterDialogue(knotName);
        }
    }

    public event Action onDialogueStarted;
    public void DialogueStarted()
    {
        if (onDialogueStarted != null)
        {
            onDialogueStarted();
        }
    }
    public event Action onDialogueFinished;
    public void DialogueFinished()
    {
        if (onDialogueFinished != null)
        {
            onDialogueFinished();
        }
    }

    public event Action<string, List<Choice>> onDisplayDialogue;
    public void DisplayDialogue(string dialogueText, List<Choice> choices)
    {
        if (onDisplayDialogue != null)
        {
            onDisplayDialogue(dialogueText, choices);
        }
    }

    public event Action<Sprite> onPortraitChanged;
    public void PortraitChanged(Sprite sprite)
    {
        if (onPortraitChanged != null)
            onPortraitChanged(sprite);
    }

    public event Action<int> onUpdateChoiceIndex;
    public void UpdateChoiceIndex(int choiceIndex)
    {
        if (onUpdateChoiceIndex != null)
        {
            onUpdateChoiceIndex(choiceIndex);
        }
    }
    
    public event Action<string, Ink.Runtime.Object> onUpdateInkDialogueVariable;
    public void UpdateInkDialogueVariable(string variableName, Ink.Runtime.Object value)
    {
        if (onUpdateInkDialogueVariable != null)
        {
            onUpdateInkDialogueVariable(variableName, value);
        }
    }
}
