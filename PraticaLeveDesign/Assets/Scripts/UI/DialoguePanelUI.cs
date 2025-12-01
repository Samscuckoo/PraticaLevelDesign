using UnityEngine;

public class DialoguePanelUI : MonoBehaviour
{
    [Tooltip("Arraste aqui o GameObject que representa o painel inteiro (root).")]
    public GameObject panel;

    private void Awake()
    {
        // tentativa automática (caso você esqueça de arrastar no Inspector)
        if (panel == null)
        {
            var found = GameObject.Find("PainelDialogo"); // nome que aparece no Hierarchy
            if (found != null) panel = found;
        }
    }

    private void OnEnable()
    {
        if (GameEventsManager.instance == null || GameEventsManager.instance.dialogueEvents == null)
        {
            Debug.LogWarning("GameEventsManager ou dialogueEvents não encontrado ao ligar DialoguePanelUI.");
            return;
        }
        var events = GameEventsManager.instance.dialogueEvents;

        // registra ouvintes de forma segura
        events.onDialogueStarted += ShowPanel;
        events.onDialogueFinished += HidePanel;
    }

    private void OnDisable()
    {
        if (GameEventsManager.instance == null || GameEventsManager.instance.dialogueEvents == null) return;
        var events = GameEventsManager.instance.dialogueEvents;
        events.onDialogueStarted -= ShowPanel;
        events.onDialogueFinished -= HidePanel;
    }

    private void Start()
    {
        if (panel == null)
        {
            Debug.LogWarning("DialoguePanelUI: campo 'panel' não assignado e não foi encontrado automaticamente.");
            return;
        }
        panel.SetActive(false);
    }

    private void ShowPanel()
    {
        if (panel == null)
        {
            Debug.LogWarning("ShowPanel chamado mas 'panel' é null.");
            return;
        }
        panel.SetActive(true);
        Debug.Log("Painel de diálogo ativado");
    }

    private void HidePanel()
    {
        if (panel == null) return;
        panel.SetActive(false);
        Debug.Log("Painel de diálogo desativado");
    }
}
