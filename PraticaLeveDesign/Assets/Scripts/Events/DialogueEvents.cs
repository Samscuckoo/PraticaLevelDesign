using UnityEngine;
using System;
using System.Collections.Generic;
using Ink.Runtime;

public class DialogueEvents
{
    // ------------------------------
    // ENTRADA NO DIÁLOGO
    // ------------------------------
    public event Action<string> onEnterDialogue;
    public void EnterDialogue(string knotName)
    {
        Debug.Log("[DialogueEvents] EnterDialogue() chamado com knotName = " + knotName);
        onEnterDialogue?.Invoke(knotName);
    }

    // ------------------------------
    // INÍCIO DO DIÁLOGO
    // ------------------------------
    public event Action onDialogueStarted;
    public void DialogueStarted()
    {
        Debug.Log("[DialogueEvents] DialogueStarted()");
        onDialogueStarted?.Invoke();
    }

    // ------------------------------
    // FINAL DO DIÁLOGO
    // ------------------------------
    public event Action onDialogueFinished;
    public void DialogueFinished()
    {
        Debug.Log("[DialogueEvents] DialogueFinished()");
        onDialogueFinished?.Invoke();
    }

    // ------------------------------
    // EXIBIR DIÁLOGO + ESCOLHAS
    // ------------------------------
    public event Action<string, List<Choice>> onDisplayDialogue;
    public void DisplayDialogue(string dialogue, List<Choice> choices)
    {
        Debug.Log("[DialogueEvents] DisplayDialogue(): " + dialogue);
        onDisplayDialogue?.Invoke(dialogue, choices);
    }

    // ------------------------------
    // MUDAR RETRATO
    // ------------------------------
    public event Action<Sprite> onPortraitChanged;
    public void PortraitChanged(Sprite sprite)
    {
        Debug.Log("[DialogueEvents] PortraitChanged()");
        onPortraitChanged?.Invoke(sprite);
    }

    // ------------------------------
    // ÍNDICE DA ESCOLHA
    // ------------------------------
    public event Action<int> onUpdateChoiceIndex;
    public void UpdateChoiceIndex(int index)
    {
        Debug.Log("[DialogueEvents] UpdateChoiceIndex(): " + index);
        onUpdateChoiceIndex?.Invoke(index);
    }

    // ------------------------------
    // VARIÁVEIS DO INK
    // ------------------------------
    public event Action<string, Ink.Runtime.Object> onUpdateInkDialogueVariable;
    public void UpdateInkDialogueVariable(string varName, Ink.Runtime.Object value)
    {
        Debug.Log("[DialogueEvents] UpdateInkDialogueVariable(): " + varName);
        onUpdateInkDialogueVariable?.Invoke(varName, value);
    }

    // ------------------------------
    // AVANÇAR DIÁLOGO (FALTAVA ESSE)
    // ------------------------------
    public event Action onNextDialogueStep;

    public void NextDialogueStep()
    {
        Debug.Log("[DialogueEvents] NextDialogueStep() chamado");

        if (onNextDialogueStep != null)
            onNextDialogueStep.Invoke();
        else
            Debug.LogWarning("[DialogueEvents] NextDialogueStep chamado mas ninguém está ouvindo.");
    }
}
