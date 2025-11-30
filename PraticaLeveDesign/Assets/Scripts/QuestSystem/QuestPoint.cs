using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField] private QuestInfoSO questInfoForPoint;

    [Header("Config")]
    [SerializeField] private bool startPoint = true;
    [SerializeField] private bool finishPoint = true;

    private bool playerIsNear = false;
    private string questId;
    private QuestState currentQuestState;
    private QuestIcon questIcon;

    private void Awake()
    {
        questId = questInfoForPoint.id;
        questIcon = GetComponentInChildren<QuestIcon>();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    private void Update()
    {
        // DEBUG: Se apertar E, conta tudo o que sabe
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("--- TESTE DE INTERAÇÃO ---");
            Debug.Log("1. Tecla E reconhecida.");
            Debug.Log("2. Player está perto? " + playerIsNear);
            Debug.Log("3. Estado da Quest: " + currentQuestState);
            Debug.Log("4. É StartPoint? " + startPoint);

            if (playerIsNear)
            {
                Interact();
            }
            else
            {
                Debug.LogWarning("❌ O Player não está perto o suficiente (playerIsNear = false).");
            }
        }
    }

    private void Interact()
    {
        // Tenta INICIAR a quest
        if (currentQuestState == QuestState.CAN_START && startPoint)
        {
            Debug.Log("✅ TENTANDO INICIAR A QUEST!");
            GameEventsManager.instance.questEvents.StartQuest(questId);
        }
        // Tenta FINALIZAR a quest
        else if (currentQuestState == QuestState.CAN_FINISH && finishPoint)
        {
            Debug.Log("✅ TENTANDO FINALIZAR A QUEST!");
            GameEventsManager.instance.questEvents.FinishQuest(questId);
        }
        else
        {
            Debug.LogError("❌ Ação negada! Motivo provável: O Estado da Quest não permite.");
            Debug.Log("Esperado: CAN_START ou CAN_FINISH. Encontrado: " + currentQuestState);
        }
    }

    private void QuestStateChange(Quest quest)
    {
        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
            Debug.Log("O Estado da Quest mudou para: " + currentQuestState);
            questIcon.SetState(currentQuestState, startPoint, finishPoint);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("🦶 Player ENTROU no Trigger.");
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("👋 Player SAIU do Trigger.");
            playerIsNear = false;
        }
    }
}