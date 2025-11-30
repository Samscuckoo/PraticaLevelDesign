using UnityEngine;
using TMPro; // Biblioteca de Texto

public class QuestDisplay : MonoBehaviour
{
    // Variável PRIVADA (não aparece no Inspector, o script acha sozinho)
    private TextMeshProUGUI textoNaTela;

    [Header("Configuração")]
    [Tooltip("ID EXATO da Quest (igual no arquivo)")]
    [SerializeField] private string questId = "ColetarPistasQuest";

    [Tooltip("Total de itens para mostrar")]
    [SerializeField] private int totalItens = 3;

    private void Awake()
    {
        // MÁGICA: O script procura o componente de texto no próprio objeto
        textoNaTela = GetComponent<TextMeshProUGUI>();

        if (textoNaTela == null)
        {
            Debug.LogError("ERRO: Não achei o TextMeshPro no objeto " + gameObject.name);
        }
    }

    private void Start()
    {
        // Se não tiver Gerente, cancela
        if (GameEventsManager.instance == null) return;

        // Se inscreve nos eventos
        GameEventsManager.instance.questEvents.onQuestStepStateChange += QuestStepStateChange;
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;

        // Texto Inicial
        AtualizarTexto("0");
    }

    private void OnDestroy()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.questEvents.onQuestStepStateChange -= QuestStepStateChange;
            GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
        }
    }

    private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        if (id == questId)
        {
            AtualizarTexto(questStepState.state);
        }
    }

    private void QuestStateChange(Quest quest)
    {
        if (quest.info.id == questId)
        {
            if (quest.state == QuestState.IN_PROGRESS)
            {
                AtualizarTexto("0");
            }
            else if (quest.state == QuestState.FINISHED)
            {
                if (textoNaTela != null) textoNaTela.text = "Missão Completa!";
            }
        }
    }

    private void AtualizarTexto(string quantidade)
    {
        if (textoNaTela != null)
        {
            textoNaTela.text = "Pistas: " + quantidade + " / " + totalItens;
        }
    }
}