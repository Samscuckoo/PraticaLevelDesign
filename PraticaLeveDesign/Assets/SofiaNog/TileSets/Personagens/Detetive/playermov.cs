using UnityEngine;

public class playermov : MonoBehaviour
{
    public float speed = 5f;

    private Animator anim;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;          // garante que nunca vai cair
    }

    void Update()
    {
        // Movimento WASD
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // IMPORTANTE: Normalizar evita que andar na diagonal seja mais rápido
        movement = movement.normalized;

        // Envia valores para as animações
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        // Mover usando VELOCIDADE respeita os colliders e a física
        // Se estiver usando Unity 6 (novo):
        rb.linearVelocity = movement * speed;

        // Se der erro vermelho no 'linearVelocity', use o antigo:
        // rb.velocity = movement * speed;
    }
}