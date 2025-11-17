using UnityEngine;

public class Sair : MonoBehaviour
{
    // Função para sair do jogo
    public void SairDoJogo()
    {
        Debug.Log("Saindo do jogo...");

        // Fecha o jogo
        Application.Quit();

    }
}
