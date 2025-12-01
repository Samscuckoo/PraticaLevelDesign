using UnityEngine;
using UnityEngine.UI;

public class DialoguePanelUI : MonoBehaviour
{
    [Header("Dialogue UI Elements")]
    [SerializeField] private GameObject panel;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Text speakerNameText;
    [SerializeField] private Button nextButton;

    private void Awake()
    {
        if (nextButton != null)
            nextButton.onClick.AddListener(OnNextClicked);
    }

    private void OnEnable()
    {
        if (GameEventsManager.instance == null)
        {
            Debug.LogError("[DialoguePanelUI] GameEventsManager.instance is NULL in OnEnable!");
            return;
        }

        var events = GameEventsManager.instance.dialogueEvents;
        events.onDialogueStarted += ShowPanel;
        events.onDialogueFinished += HidePanel;

        Debug.Log("[DialoguePanelUI] Registered to onDialogueStarted/onDialogueFinished");
    }

    private void OnDisable()
    {
        if (GameEventsManager.instance == null) return;

        var events = GameEventsManager.instance.dialogueEvents;
        events.onDialogueStarted -= ShowPanel;
        events.onDialogueFinished -= HidePanel;
    }

    private void ShowPanel()
    {
        Debug.Log("[DialoguePanelUI] ShowPanel() called");

        if (panel == null)
        {
            Debug.LogError("[DialoguePanelUI] panel is NULL!");
            return;
        }

        panel.SetActive(true);
    }

    private void HidePanel()
    {
        Debug.Log("[DialoguePanelUI] HidePanel() called");

        if (panel != null)
            panel.SetActive(false);
    }

    public void SetDialogueText(string text)
    {
        if (dialogueText != null)
            dialogueText.text = text;
    }

    public void SetSpeakerName(string name)
    {
        if (speakerNameText != null)
            speakerNameText.text = name;
    }

    private void OnNextClicked()
    {
        if (GameEventsManager.instance != null)
            GameEventsManager.instance.dialogueEvents.NextDialogueStep();
    }
}
