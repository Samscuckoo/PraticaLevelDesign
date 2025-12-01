using UnityEngine;
using Ink.Runtime;
using System.Reflection;

public class StatueNPCInk : MonoBehaviour
{
    [Header("Ink")]
    [SerializeField] private string inkBoolName = "orbe_azul_1";

    [Header("Sprite após ativação")]
    [SerializeField] private Sprite spriteAtivo;

    private SpriteRenderer sr;
    private Sprite spriteInativo;
    private bool jaAtivou = false;

    private DialogueManager dialogueManager;
    private Story story;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        spriteInativo = sr.sprite;

        // Acha automaticamente o DialogueManager da cena
        dialogueManager = FindAnyObjectByType<DialogueManager>();

        // === AQUI ESTÁ O TRUQUE (Reflection) ===
        FieldInfo storyField = typeof(DialogueManager)
            .GetField("story", BindingFlags.NonPublic | BindingFlags.Instance);

        story = (Story)storyField.GetValue(dialogueManager);

        // Lê o estado direto do Ink
        bool estadoInk = (bool)story.variablesState[inkBoolName];

        jaAtivou = estadoInk;
        sr.sprite = jaAtivou ? spriteAtivo : spriteInativo;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (jaAtivou) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            AtivarOrbe();
        }
    }

    void AtivarOrbe()
    {
        jaAtivou = true;
        sr.sprite = spriteAtivo;

        // Escreve direto na variável do Ink via Reflection
        story.variablesState[inkBoolName] = true;
    }
}
