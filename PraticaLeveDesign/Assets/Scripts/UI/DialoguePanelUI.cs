using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;

public class DialoguePanelUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject contentParent;
    [SerializeField] private TextMeshProUGUI dialogueTextUI;
    [SerializeField] private DialogueChoiceButton[] choiceButtons;
    [SerializeField] private Image portraitImage;

    // Dicionário para mapear escolhas a sprites
    [SerializeField] private Dictionary<string, Sprite> choiceSprites;

    private void Awake()
    {
        contentParent.SetActive(false);
        if (portraitImage != null) portraitImage.gameObject.SetActive(false);
        ResetPanel();

        // Inicializa o dicionário de sprites
        choiceSprites = new Dictionary<string, Sprite>();
        // Adicione suas sprites aqui, por exemplo:
        // choiceSprites.Add("Choice1", yourSprite1);
        // choiceSprites.Add("Choice2", yourSprite2);
    }

    private void OnEnable()
    {
        GameEventsManager.instance.dialogueEvents.onDialogueStarted += DialogueStarted;
        GameEventsManager.instance.dialogueEvents.onDialogueFinished += DialogueFinished;
        GameEventsManager.instance.dialogueEvents.onDisplayDialogue += DisplayDialogue;
        GameEventsManager.instance.dialogueEvents.onPortraitChanged += SetPortrait; 
    }

    private void OnDisable()
    {
        GameEventsManager.instance.dialogueEvents.onDialogueStarted -= DialogueStarted;
        GameEventsManager.instance.dialogueEvents.onDialogueFinished -= DialogueFinished;
        GameEventsManager.instance.dialogueEvents.onDisplayDialogue -= DisplayDialogue;
        GameEventsManager.instance.dialogueEvents.onPortraitChanged -= SetPortrait; 
    }

    private void DialogueStarted()
    {
        contentParent.SetActive(true);
        if (portraitImage != null) portraitImage.gameObject.SetActive(true);
    }

    private void DialogueFinished()
    {
        contentParent.SetActive(false);
        ResetPanel(); 
        if (portraitImage != null) portraitImage.gameObject.SetActive(false);
    }

    private void DisplayDialogue(string dialogueText, List<Choice> choices)
    {
        dialogueTextUI.text = dialogueText;

        if (choices.Count > choiceButtons.Length)
        {
            Debug.LogWarning("Not enough choice buttons to display all choices.");
        }

        // desativa todos e remove listeners antigos
        foreach (DialogueChoiceButton button in choiceButtons)
        {
            button.gameObject.SetActive(false);
            var btn = button.GetComponent<UnityEngine.UI.Button>();
            if (btn != null) btn.onClick.RemoveAllListeners();
        }

        int choiceButtonIndex = choices.Count - 1;
        for (int inkChoiceIndex = 0; inkChoiceIndex < choices.Count; inkChoiceIndex++)
        {
            Choice dialogueChoice = choices[inkChoiceIndex];
            DialogueChoiceButton choiceButton = choiceButtons[choiceButtonIndex];

            choiceButton.gameObject.SetActive(true);
            choiceButton.SetChoiceText(dialogueChoice.text);
            choiceButton.SetChoiceSprite(GetSpriteForChoice(dialogueChoice)); // Defina a sprite aqui

            // captura local do índice para evitar problema de closure
            int capturedIndex = inkChoiceIndex;

            // garante que o Button chame UpdateChoiceIndex(capturedIndex) e confirme a escolha
            var btn = choiceButton.GetComponent<UnityEngine.UI.Button>();
            if (btn != null)
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() =>
                {
                    GameEventsManager.instance.dialogueEvents.UpdateChoiceIndex(capturedIndex);
                    GameEventsManager.instance.inputEvents.SubmitPressed(); // confirma escolha
                });
            }

            if (inkChoiceIndex == 0)
            {
                choiceButton.SelectButton();
                GameEventsManager.instance.dialogueEvents.UpdateChoiceIndex(0);
            }
            choiceButtonIndex--;
        }
    }

    private void SetPortrait(Sprite sprite)
    {
        if (portraitImage == null) return;
        portraitImage.sprite = sprite;
        portraitImage.enabled = sprite != null;
    }

    private void ResetPanel()
    {
        dialogueTextUI.text = "";
        if (portraitImage != null) portraitImage.sprite = null;
    }

    private Sprite GetSpriteForChoice(Choice choice)
    {
        // Retorna a sprite correspondente à escolha, se existir
        if (choiceSprites.TryGetValue(choice.text, out Sprite sprite))
        {
            return sprite;
        }
        return null; // Retorna null se não houver sprite correspondente
    }
}
