using System;

public class MiscEvents
{
    // =========================================================
    // EVENTOS DE PISTA
    // =========================================================
    // A Action (o "on") que o QuestStep assina
    public event Action onPistaCollected;

    // O método que o PistaPickup chama para disparar o evento
    public void PistaCollected()
    {
        // Dispara o evento, notificando todos os ouvintes.
        onPistaCollected?.Invoke();
    }

    // =========================================================
    // OUTROS EVENTOS (Mantidos como exemplo)
    // =========================================================
    public event Action onCoinCollected;
    public void CoinCollected()
    {
        onCoinCollected?.Invoke();
    }

    public event Action onGemCollected;
    public void GemCollected()
    {
        onGemCollected?.Invoke();
    }
}