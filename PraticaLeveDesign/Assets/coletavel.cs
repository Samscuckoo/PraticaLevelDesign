using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Renomeado para refletir o item que ele representa
public class PistaPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Verifica se quem entrou na colisão é o Player (confira a Tag no Unity!)
        if (other.CompareTag("Player"))
        {
            // 2. DISPARA o evento no GameEventsManager, chamando o método PistaCollected()
            // Isso notifica o ColetarPistasQuestStep
            if (GameEventsManager.instance != null && GameEventsManager.instance.miscEvents != null)
            {
                GameEventsManager.instance.miscEvents.PistaCollected();
            }
            else
            {
                Debug.LogError("GameEventsManager ou MiscEvents não está acessível! A pista não foi registrada.");
            }

            // 3. Destrói ou desativa o objeto coletável
            gameObject.SetActive(false);
            // Se preferir, use: Destroy(gameObject);
        }
    }
}