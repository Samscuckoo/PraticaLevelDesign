using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private bool loadQuestState = true;

    private Dictionary<string, Quest> questMap;

    // Requisitos para iniciar quests
    private int currentPlayerLevel;

    private void Awake()
    {
        questMap = CreateQuestMap();
    }

    private IEnumerator Start()
    {
        // Espera o GameEventsManager inicializar (proteção contra ordem de execução)
        yield return StartCoroutine(WaitForGameEventsManager());

        // Registrando listeners com segurança
        if (GameEventsManager.instance != null)
        {
            var gem = GameEventsManager.instance;
            gem.questEvents.onStartQuest += StartQuest;
            gem.questEvents.onAdvanceQuest += AdvanceQuest;
            gem.questEvents.onFinishQuest += FinishQuest;
            gem.questEvents.onQuestStepStateChange += QuestStepStateChange;

            if (gem.playerEvents != null)
                gem.playerEvents.onPlayerLevelChange += PlayerLevelChange;
        }

        // Inicializa quests que já estavam em andamento no save
        foreach (Quest quest in questMap.Values)
        {
            if (quest == null) continue;

            if (quest.state == QuestState.IN_PROGRESS)
            {
                quest.InstantiateCurrentQuestStep(this.transform);
            }

            // Transmite o estado inicial de todas as quests (se possível)
            if (GameEventsManager.instance != null && GameEventsManager.instance.questEvents != null)
            {
                GameEventsManager.instance.questEvents.QuestStateChange(quest);
            }
        }
    }

    private IEnumerator WaitForGameEventsManager()
    {
        // espera até que GameEventsManager.instance exista (ou timeout)
        float timeout = 5f;
        float t = 0f;
        while (GameEventsManager.instance == null && t < timeout)
        {
            t += Time.deltaTime;
            yield return null;
        }

        if (GameEventsManager.instance == null)
        {
            Debug.LogError("[QuestManager] Timeout esperando GameEventsManager.instance. Muitos sistemas dependerão disso.");
        }
        else
        {
            Debug.Log("[QuestManager] GameEventsManager encontrado no Start(). Registrando listeners.");
        }
    }

    private void OnDisable()
    {
        // Unsubscribe com checagens para evitar NREs na desativação
        if (GameEventsManager.instance != null)
        {
            var gem = GameEventsManager.instance;
            if (gem.questEvents != null)
            {
                gem.questEvents.onStartQuest -= StartQuest;
                gem.questEvents.onAdvanceQuest -= AdvanceQuest;
                gem.questEvents.onFinishQuest -= FinishQuest;
                gem.questEvents.onQuestStepStateChange -= QuestStepStateChange;
            }

            if (gem.playerEvents != null)
            {
                gem.playerEvents.onPlayerLevelChange -= PlayerLevelChange;
            }
        }
    }

    // -------------------------
    // Métodos públicos / lógica
    // -------------------------
    private bool CheckRequirementsMet(Quest quest)
    {
        if (quest == null) return false;

        bool meetsRequirements = true;

        if (currentPlayerLevel < quest.info.levelRequirement)
        {
            meetsRequirements = false;
        }

        foreach (QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites)
        {
            Quest prereq = GetQuestByIdSafe(prerequisiteQuestInfo.id);
            if (prereq == null || prereq.state != QuestState.FINISHED)
            {
                meetsRequirements = false;
            }
        }

        return meetsRequirements;
    }

    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestByIdSafe(id);
        if (quest == null)
        {
            Debug.LogWarning("[QuestManager] ChangeQuestState: quest não encontrada: " + id);
            return;
        }

        quest.state = state;

        if (GameEventsManager.instance != null && GameEventsManager.instance.questEvents != null)
        {
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }
    }

    private void PlayerLevelChange(int level)
    {
        currentPlayerLevel = level;
    }

    private void StartQuest(string id)
    {
        Quest quest = GetQuestByIdSafe(id);
        if (quest == null)
        {
            Debug.LogWarning("[QuestManager] StartQuest: quest não encontrada: " + id);
            return;
        }

        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
    }

    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestByIdSafe(id);
        if (quest == null)
        {
            Debug.LogWarning("[QuestManager] AdvanceQuest: quest não encontrada: " + id);
            return;
        }

        quest.MoveToNextStep();

        if (quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        else
        {
            ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
        }
    }

    private void FinishQuest(string id)
    {
        Quest quest = GetQuestByIdSafe(id);
        if (quest == null)
        {
            Debug.LogWarning("[QuestManager] FinishQuest: quest não encontrada: " + id);
            return;
        }

        ClaimRewards(quest);
        ChangeQuestState(quest.info.id, QuestState.FINISHED);
    }

    private void ClaimRewards(Quest quest)
    {
        if (GameEventsManager.instance != null)
        {
            if (GameEventsManager.instance.goldEvents != null)
                GameEventsManager.instance.goldEvents.GoldGained(quest.info.goldReward);

            if (GameEventsManager.instance.playerEvents != null)
                GameEventsManager.instance.playerEvents.ExperienceGained(quest.info.experienceReward);
        }
    }

    private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        Quest quest = GetQuestByIdSafe(id);
        if (quest == null)
        {
            Debug.LogWarning("[QuestManager] QuestStepStateChange: quest não encontrada: " + id);
            return;
        }

        quest.StoreQuestStepState(questStepState, stepIndex);
        ChangeQuestState(id, quest.state);
    }

    // -------------------------
    // Helpers: criação / busca
    // -------------------------
    private Dictionary<string, Quest> CreateQuestMap()
    {
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestInfoSO questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogWarning("Duplicate ID found: " + questInfo.id);
            }
            idToQuestMap.Add(questInfo.id, LoadQuest(questInfo));
        }
        return idToQuestMap;
    }

    private Quest GetQuestByIdSafe(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            Debug.LogWarning("[QuestManager] GetQuestByIdSafe called with null/empty id.");
            return null;
        }

        if (questMap == null)
        {
            Debug.LogWarning("[QuestManager] questMap é null!");
            return null;
        }

        if (!questMap.TryGetValue(id, out Quest quest))
        {
            Debug.LogWarning("[QuestManager] ID not found in the Quest Map: " + id);
            return null;
        }

        return quest;
    }

    // -------------------------
    // Save / Load
    // -------------------------
    private void OnApplicationQuit()
    {
        if (questMap == null) return;
        foreach (Quest quest in questMap.Values)
        {
            SaveQuest(quest);
        }
    }

    private void SaveQuest(Quest quest)
    {
        if (quest == null) return;

        try
        {
            QuestData questData = quest.GetQuestData();
            string serializedData = JsonUtility.ToJson(questData);
            PlayerPrefs.SetString(quest.info.id, serializedData);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to save quest: " + e);
        }
    }

    private Quest LoadQuest(QuestInfoSO questInfo)
    {
        Quest quest = null;
        try
        {
            if (PlayerPrefs.HasKey(questInfo.id) && loadQuestState)
            {
                string serializedData = PlayerPrefs.GetString(questInfo.id);
                QuestData questData = JsonUtility.FromJson<QuestData>(serializedData);
                quest = new Quest(questInfo, questData.state, questData.questStepIndex, questData.questStepStates);
            }
            else
            {
                quest = new Quest(questInfo);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to load quest: " + e);
        }
        return quest;
    }
}
