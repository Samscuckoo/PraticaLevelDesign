using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Anim: MonoBehaviour
{
    [Header("Componentes")]
    public Image imagem;          // A imagem que será animada

    [Header("Sprites da animação")]
    public Sprite[] sprites;      // Array de sprites para a animação

    [Header("Configurações")]
    public float tempoPorFrame = 0.1f;  // Tempo por frame
    public bool animandoAutomatico = false; // Começa animando ao ligar

    private Coroutine animacaoCoroutine;

    void Start()
    {
        if (animandoAutomatico)
            IniciarAnimacao();
    }

    public void IniciarAnimacao()
    {
        if (animacaoCoroutine != null)
            StopCoroutine(animacaoCoroutine);

        animacaoCoroutine = StartCoroutine(Animar());
    }

    public void PararAnimacao()
    {
        if (animacaoCoroutine != null)
        {
            StopCoroutine(animacaoCoroutine);
            animacaoCoroutine = null;
        }
    }

    private IEnumerator Animar()
    {
        int indiceAtual = 0;

        while (true)
        {
            if (sprites.Length > 0 && imagem != null)
            {
                imagem.sprite = sprites[indiceAtual];
                indiceAtual = (indiceAtual + 1) % sprites.Length;
            }

            yield return new WaitForSeconds(tempoPorFrame);
        }
    }
}
