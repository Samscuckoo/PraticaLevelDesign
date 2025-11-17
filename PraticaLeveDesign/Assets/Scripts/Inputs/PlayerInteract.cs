using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public void SubmitPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GameEventsManager.instance.inputEvents.SubmitPressed();
        }
    }
}
