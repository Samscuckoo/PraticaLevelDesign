using UnityEngine;
using System;
using System.Collections.Generic;
using Ink.Runtime;

public class DialogueEvents
{
    public event Action<string> onEnterDialogue;
    public void EnterDialogue(string knotName)
    {
        onEnterDialogue?.Invoke(knotName);
    }

    public event Action onDialogueStarted;
    public void DialogueStarted()
    {
        onDialogueStarted?.Invoke();
    }

    public event Action onDialogueFinished;
    public void DialogueFinished()
    {
        onDialogueFinished?.Invoke();
    }

    public event Action<string, List<Choice>> onDisplayDialogue;
    public void DisplayDialogue(string dialogue, List<Choice> choices)
    {
        onDisplayDialogue?.Invoke(dialogue, choices);
    }

    public event Action<Sprite> onPortraitChanged;
    public void PortraitChanged(Sprite sprite)
    {
        onPortraitChanged?.Invoke(sprite);
    }

    public event Action<int> onUpdateChoiceIndex;
    public void UpdateChoiceIndex(int index)
    {
        onUpdateChoiceIndex?.Invoke(index);
    }

    public event Action<string, Ink.Runtime.Object> onUpdateInkDialogueVariable;
    public void UpdateInkDialogueVariable(string varName, Ink.Runtime.Object value)
    {
        onUpdateInkDialogueVariable?.Invoke(varName, value);
    }
}
