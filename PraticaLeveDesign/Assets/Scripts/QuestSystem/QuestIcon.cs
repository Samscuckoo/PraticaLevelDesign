using UnityEngine;

public class QuestIcon : MonoBehaviour
{
    [SerializeField] private GameObject requirementsNotMetIcon;
    [SerializeField] private GameObject canStartIcon;
    [SerializeField] private GameObject requirementsNotMetToFinishIcon;
    [SerializeField] private GameObject canFinishIcon;

    public void SetState(QuestState newState, bool startPoint, bool finishPoint)
    {
        requirementsNotMetIcon.SetActive(false);
        canStartIcon.SetActive(false);
        requirementsNotMetToFinishIcon.SetActive(false);
        canFinishIcon.SetActive(false);

        switch (newState)
        {
            case QuestState.REQUIREMENTS_NOT_MET:
                if (startPoint)
                {
                    requirementsNotMetIcon.SetActive(true);
                }
                break;
            case QuestState.CAN_START:
                if (startPoint)
                {
                    canStartIcon.SetActive(true);
                }
                break;
            case QuestState.IN_PROGRESS:
                if (finishPoint)
                {
                    requirementsNotMetToFinishIcon.SetActive(true);
                }
                break;
            case QuestState.CAN_FINISH:
                if (finishPoint)
                {
                    canFinishIcon.SetActive(true);
                }
                break;
            case QuestState.FINISHED:
                // No icon for finished state
                break;
            default:
                Debug.LogWarning("Unhandled QuestState: " + newState);
                break;
        }    
    }
}
