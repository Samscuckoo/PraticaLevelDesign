using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    [Header("Configuração")]
    [SerializeField] private TextAsset inkJson;

    private Story story;
    private bool dialoguePlaying = false;

    private InkExternalFunctions inkExternalFunctions;
    private InkDialogueVariables inkDialogueVariables;

    private const string PORTRAIT_TAG = "portrait";

    private void Awake()
    {
        if (inkJson != null)
        {
            inkExternalFunctions = new InkExternalFunctions();
            story = new Story(inkJson.text);
            inkDialogueVariables = new InkDialogueVariables(story);
        }
        else
        {
            Debug.LogError("⚠ FALTANDO Main.json no Inspector!");
        }
    }

    private void OnEnable()
    {
        Debug.Log("DialogueManager ON - Registrando evento de diálogo...");
        GameEventsManager.instance.dialogueEvents.onEnterDialogue += EnterDialogue;
    }

    private void OnDisable()
    {
        if (GameEventsManager.instance != null)
            GameEventsManager.instance.dialogueEvents.onEnterDialogue -= EnterDialogue;
    }

    private void Update()
    {
        if (!dialoguePlaying) return;

        if (Input.GetKeyDown(KeyCode.E) ||
            Input.GetKeyDown(KeyCode.Space) ||
            Input.GetMouseButtonDown(0))
        {
            ContinueOrExitStory();
        }
    }

    private void EnterDialogue(string knotName)
    {
        if (dialoguePlaying) return;

        story = new Story(inkJson.text);

        inkExternalFunctions.Bind(story);
        inkDialogueVariables.SyncVariablesAndStartListening(story);

        dialoguePlaying = true;

        GameEventsManager.instance.dialogueEvents.DialogueStarted();

        if (!string.IsNullOrEmpty(knotName))
        {
            try { story.ChoosePathString(knotName); }
            catch { Debug.LogError("Erro no nó: " + knotName); }
        }

        ContinueOrExitStory();
    }

    private void ContinueOrExitStory()
    {
        if (story.canContinue)
        {
            string text = story.Continue();

            while (string.IsNullOrWhiteSpace(text) && story.canContinue)
                text = story.Continue();

            GameEventsManager.instance.dialogueEvents.DisplayDialogue(text, story.currentChoices);

            HandleTags(story.currentTags);
        }
        else if (story.currentChoices.Count > 0)
        {
            GameEventsManager.instance.dialogueEvents.DisplayDialogue("", story.currentChoices);
        }
        else
        {
            ExitDialogue();
        }
    }

    private void HandleTags(List<string> tags)
    {
        foreach (var tag in tags)
        {
            var split = tag.Split(':');
            if (split.Length != 2) continue;

            string key = split[0].Trim();
            string value = split[1].Trim();

            if (key == PORTRAIT_TAG)
            {
                Sprite portrait = Resources.Load<Sprite>("Personagens/" + value);
                if (portrait != null)
                    GameEventsManager.instance.dialogueEvents.PortraitChanged(portrait);
                else
                    Debug.LogWarning("Retrato não encontrado: " + value);
            }
        }
    }

    private void ExitDialogue()
    {
        dialoguePlaying = false;

        inkExternalFunctions?.Unbind(story);
        inkDialogueVariables?.StopListening(story);

        GameEventsManager.instance.dialogueEvents.DialogueFinished();
    }
}
