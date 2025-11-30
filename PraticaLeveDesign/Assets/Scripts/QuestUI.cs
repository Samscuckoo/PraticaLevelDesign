using UnityEngine;
using TMPro;

public class QuestUI : MonoBehaviour
{
    [Header("ARRASTE O TEXTO AQUI 👇")]
    public TextMeshProUGUI texto;

    [Header("Configuração")]
    public string questId = "ColetarPistasQuest";
    public int totalItens = 3;

    private void Awake()
    {
        // PLANO B: Se você esqueceu de arrastar, ele tenta pegar sozinho
        if (texto == null)
        {
            texto = GetComponent<TextMeshProUGUI>();
        }
    }

    private void Start()
    {
        // Se ainda assim for nulo, ele avisa no console QUAL objeto está com problema
        if (texto == null)
        {
            Debug.LogError("🚨 ERRO GRAVE: O script QuestUI está no objeto '" + gameObject.name + "' mas não achou o Texto!");
            return; // Cancela tudo para não travar o jogo
        }

        if (GameEventsManager.instance == null) return;

        GameEventsManager.instance.questEvents.onQuestStepStateChange += AtualizarPasso;
        GameEventsManager.instance.questEvents.onQuestStateChange += AtualizarEstado;

        texto.text = "Pistas: 0 / " + totalItens;
    }

    private void OnDestroy()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.questEvents.onQuestStepStateChange -= AtualizarPasso;
            GameEventsManager.instance.questEvents.onQuestStateChange -= AtualizarEstado;
        }
    }

    private void AtualizarPasso(string id, int stepIndex, QuestStepState questStepState)
    {
        // Proteção extra: Só atualiza se o texto existir
        if (id == questId && texto != null)
        {
            texto.text = "Pistas: " + questStepState.state + " / " + totalItens;
        }
    }

    private void AtualizarEstado(Quest quest)
    {
        // Proteção extra: Só atualiza se o texto existir
        if (quest.info.id == questId && texto != null)
        {
            if (quest.state == QuestState.IN_PROGRESS) texto.text = "Pistas: 0 / " + totalItens;
            else if (quest.state == QuestState.FINISHED) texto.text = "Missão Completa!";
        }
    }
}