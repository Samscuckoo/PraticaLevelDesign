using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueChoiceButton : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI choiceText;

    private int choiceIndex = -1;

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }

    public void SetChoiceText(string text)
    {
        choiceText.text = text;
    }

    public void SetChoiceIndex(int index)
    {
        choiceIndex = index;
    }

    private void OnClick()
    {
        GameEventsManager.instance.dialogueEvents.UpdateChoiceIndex(choiceIndex);
    }

    public void OnSelect()
    {
        GameEventsManager.instance.dialogueEvents.UpdateChoiceIndex(choiceIndex);
    }
}
