using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    protected bool isFinished = false;
    protected string questId;
    protected int stepIndex;

    public void InitializeQuestStep(string questId, int stepIndex, string questStepState)
    {
        this.questId = questId;
        this.stepIndex = stepIndex;

        if (questStepState != null && questStepState != "")
        {
            SetQuestStepState(questStepState);
        }
    }

    protected abstract void SetQuestStepState(string state);

    protected void FinishQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;

            
            if (GameEventsManager.instance != null)
            {
                GameEventsManager.instance.questEvents.AdvanceQuest(questId);
            }

            
            Destroy(this.gameObject);
        }
    }


    protected void ChangeState(string newState)
    {
        if (questId == null) return;

        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.questEvents.QuestStepStateChange(questId, stepIndex, new QuestStepState(newState));
        }
    }


    protected void ChangeState(string newState, string newStatusDescription)
    {
    
        ChangeState(newState);
    }
}