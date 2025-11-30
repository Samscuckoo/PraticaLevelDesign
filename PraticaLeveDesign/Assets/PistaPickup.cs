using UnityEngine;

public class PistaPickup : MonoBehaviour
{
    [Header("Configuração")]
    [SerializeField] private string questIdNecessaria = "ColetarPistasQuest";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            GameEventsManager.instance.miscEvents.PistaCollected();
            Destroy(gameObject);
        }
    }
}