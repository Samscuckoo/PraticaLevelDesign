using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Vector2 moveInput;
    public Animator animator;

    // torna visível no Inspector para debug
    [SerializeField] private bool movementEnabled = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!movementEnabled)
        {
            if (rb != null) rb.linearVelocity = Vector2.zero;
            return;
        }

        if (rb != null) rb.linearVelocity = moveInput * moveSpeed;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onDisablePlayerMovement += DisableMovement;
        GameEventsManager.instance.playerEvents.onEnablePlayerMovement += EnableMovement;
    }

     private void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onDisablePlayerMovement -= DisableMovement;
        GameEventsManager.instance.playerEvents.onEnablePlayerMovement -= EnableMovement;        
    }

    public void Move(InputAction.CallbackContext context)
    {
        // sempre atualiza moveInput (para quando reabilitar manter último valor)
        moveInput = context.ReadValue<Vector2>();

        // se movimento desabilitado, garante que animação pare e não processa
        if (!movementEnabled)
        {
            if (animator != null) animator.SetBool("IsWalking", false);
            return;
        }

        bool isWalking = !context.canceled && moveInput != Vector2.zero;
        if (animator != null)
        {
            animator.SetBool("IsWalking", isWalking);

            if (context.canceled)
            {
                animator.SetFloat("LastInputX", moveInput.x);
                animator.SetFloat("LastInputY", moveInput.y);
            }

            animator.SetFloat("InputX", moveInput.x);
            animator.SetFloat("InputY", moveInput.y);
        }
    }

    private void DisableMovement()
    {
        movementEnabled = false;
        if (rb != null) rb.linearVelocity = Vector2.zero;
        if (animator != null) animator.SetBool("IsWalking", false);
    }

    private void EnableMovement()
    {
        movementEnabled = true;
    }
}
