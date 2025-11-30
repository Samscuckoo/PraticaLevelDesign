using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColetarPistasQuestStep : QuestStep
{
    private int pistasColetadas = 0;
    private int pistasParaCompletar = 3;
    private const string statusBase = "Colete as pistas: ";

    private void Awake()
    {
        // O Awake() é chamado antes do Start(), dando prioridade à assinatura.
        if (GameEventsManager.instance != null && GameEventsManager.instance.miscEvents != null)
        {
            GameEventsManager.instance.miscEvents.onPistaCollected += PistaCollected;
            // Se a inicialização de UI/estado precisa ser depois do Awake,
            // pode ser necessário mover UpdateQuestState() para um Start() vazio.
            UpdateQuestState(statusBase + pistasColetadas + " / " + pistasParaCompletar);
        }
    }

    // Mantenha o restante do código como estava, incluindo o OnDestroy() para desassinatura:
    private void OnDestroy()
    {
        if (GameEventsManager.instance != null && GameEventsManager.instance.miscEvents != null)
        {
            GameEventsManager.instance.miscEvents.onPistaCollected -= PistaCollected;
        }
    }

    private void PistaCollected()
    {
        if (pistasColetadas < pistasParaCompletar)
        {
            pistasColetadas++;
            UpdateQuestState(statusBase + pistasColetadas + " / " + pistasParaCompletar);
        }

        if (pistasColetadas >= pistasParaCompletar)
        {
            FinishQuestStep();
        }
    }

    private void UpdateQuestState(string newStatus)
    {
        ChangeState(pistasColetadas.ToString(), newStatus);
    }

    protected override void SetQuestStepState(string state)
    {
        bool parsed = int.TryParse(state, out pistasColetadas);
        if (parsed)
        {
            UpdateQuestState(statusBase + pistasColetadas + " / " + pistasParaCompletar);
        }
    }
}