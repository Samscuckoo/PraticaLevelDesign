using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DialogueChoiceButton : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI choiceText;
    [SerializeField] private Sprite choiceSprite; // Adicione este campo para a sprite

    private int choiceIndex = -1;

    public void SetChoiceText(string text)
    {
        choiceText.text = text;
    }

    public void SetChoiceIndex(int index)
    {
        this.choiceIndex = index;
    }

    public void SetChoiceSprite(Sprite sprite) // Método para definir a sprite
    {
        choiceSprite = sprite;
    }

    public Sprite GetChoiceSprite() // Método para obter a sprite
    {
        return choiceSprite;
    }

    public void SelectButton()
    {
        button.Select();
    }
    
    public void OnSelect()
    {
        GameEventsManager.instance.dialogueEvents.UpdateChoiceIndex(choiceIndex);
    }
}
