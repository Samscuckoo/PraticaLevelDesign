using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("Configuração")]
    [SerializeField] private TextAsset inkJson;
    
    [Header("UI (Arraste aqui!)")]
    [SerializeField] private GameObject dialoguePanel;      
    [SerializeField] private TextMeshProUGUI dialogueText;  
    [SerializeField] private TextMeshProUGUI displayNameText; 
    [SerializeField] private Animator portraitAnimator;     

    private Story story;
    private bool dialoguePlaying = false;

    // --- SEUS SCRIPTS AUXILIARES ---
    private InkExternalFunctions inkExternalFunctions;
    private InkDialogueVariables inkDialogueVariables;
    // -------------------------------

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";

    private void Awake() 
    {
        if (inkJson != null)
        {
            // Inicializa seus scripts auxiliares
            inkExternalFunctions = new InkExternalFunctions();
            // Precisamos criar a história aqui para passar para o DialogueVariables
            story = new Story(inkJson.text);
            inkDialogueVariables = new InkDialogueVariables(story); 
        }
        else
        {
            Debug.LogError("⚠️ FALTANDO ARQUIVO: Arraste o 'Main.json' para o campo Ink Json no Inspector!");
        }

        if (dialoguePanel != null) dialoguePanel.SetActive(false); 
    }

    private void OnEnable() 
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue += EnterDialogue;
    }

    private void OnDisable() 
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.dialogueEvents.onEnterDialogue -= EnterDialogue;
        }
    }

    private void Update()
    {
        if (!dialoguePlaying) return;

        // Avança com E, Espaço ou Clique
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            ContinueOrExitStory();
        }
    }

    private void EnterDialogue(string knotName) 
    {
        if (dialoguePlaying) return;

        // Reinicia a história para garantir estado limpo
        story = new Story(inkJson.text); 
        
        // --- USA SEUS SCRIPTS PARA CONECTAR ---
        // Liga as funções (Quests) e Variáveis
        // CORREÇÃO: Removido o segundo argumento que causava erro
        inkExternalFunctions.Bind(story); 
        
        // CORREÇÃO: Usando o nome correto do método do seu script
        inkDialogueVariables.SyncVariablesAndStartListening(story);             
        // --------------------------------------

        dialoguePlaying = true;
        if (dialoguePanel != null) dialoguePanel.SetActive(true);

        // Tenta ir para o nó específico
        if (!string.IsNullOrEmpty(knotName))
        {
            try {
                story.ChoosePathString(knotName);
            }
            catch (System.Exception e) {
                Debug.LogError("Erro ao ir para o nó: " + knotName + ". " + e.Message);
            }
        }

        // Configurações iniciais
        if (displayNameText != null) displayNameText.text = "???";
        
        // PROTEÇÃO: Só tenta tocar animação se existir um Controlador no Animator
        if (portraitAnimator != null && portraitAnimator.runtimeAnimatorController != null) 
        {
            portraitAnimator.Play("default");
        }

        ContinueOrExitStory();
    }

    private void ContinueOrExitStory() 
    {
        if (story.canContinue)
        {
            string textoAtual = story.Continue();
            
            // Pula linhas em branco se houver
            if (string.IsNullOrWhiteSpace(textoAtual) && story.canContinue)
            {
                ContinueOrExitStory();
                return;
            }

            // Atualiza a tela
            if (dialogueText != null) dialogueText.text = textoAtual;

            HandleTags(story.currentTags);
        }
        else
        {
            ExitDialogue();
        }
    }

    private void HandleTags(List<string> tags)
    {
        foreach (string tag in tags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2) continue;

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    if (displayNameText != null) displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    // PROTEÇÃO: Só toca se tiver controlador
                    if (portraitAnimator != null && portraitAnimator.runtimeAnimatorController != null) 
                    {
                        portraitAnimator.Play(tagValue);
                    }
                    break;
            }
        }
    }

    private void ExitDialogue()
    {
        dialoguePlaying = false;
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
        if (dialogueText != null) dialogueText.text = "";

        // Desliga seus scripts para evitar erro
        if (inkExternalFunctions != null) inkExternalFunctions.Unbind(story);
        if (inkDialogueVariables != null) inkDialogueVariables.StopListening(story);

        GameEventsManager.instance.dialogueEvents.DialogueFinished();
    }
}