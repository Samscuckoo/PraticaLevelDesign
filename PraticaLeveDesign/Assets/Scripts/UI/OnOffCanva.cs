
using UnityEngine;

public class OnOffCanva : MonoBehaviour
{
    public GameObject canva;

    private bool isOpen = false;

    public bool vaiEsconder = true;

    void Start()
    {
        if (vaiEsconder)
        {
            canva.SetActive(false);
        }
        else
        {
            isOpen = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isOpen == true)
        {
            ToggleCanva();
        }
    }


    private void ToggleCanva()
    {
        isOpen = !isOpen;

        if (isOpen)
        {

            canva.SetActive(true); // Ativa o menu de pausa
        }
        else
        {

            canva.SetActive(false); // Desativa o menu de pausa
        }
    }

    public void Open()
    {
        if (!isOpen)
        {
            ToggleCanva(); // Chama a função TogglePause para despausar o jogo
        }
    }

    public void Close()
    {
        if (isOpen)
        {
            ToggleCanva(); // Chama a função TogglePause para despausar o jogo
        }
    }
}