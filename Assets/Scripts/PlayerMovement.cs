using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 7f;

    [Header("Jump")]
    public float jumpForce = 10f;
    public LayerMask groundMask;
    public Transform groundCheck;   // ayak altına küçük bir empty
    public float groundCheckRadius = 0.15f;

    Rigidbody2D rb;

    // --- Input state (Update'ta doldur) ---
    float moveInput;      // -1,0,1
    bool jumpPressed;     // bu framede space basıldı mı?

    // --- Runtime state (FixedUpdate'ta tüket) ---
    bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1) INPUTU SADECE OKU (frame-rate bağımlı)
        moveInput = Input.GetAxisRaw("Horizontal");        // A/D, Sol/Sağ

        // Jump’ı “edge-trigger” gibi yakalamak için GetKeyDown kullan:
        if (Input.GetKeyDown(KeyCode.Space))
            jumpPressed = true;
    }

    void FixedUpdate()
    {
        // 2) FİZİĞİ SADECE BURADA UYGULA (sabit adım)
        // Zemin kontrolü (fizik katmanı)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask);

        // Yatay hareket: velocity’nin x’ini set et, y'yi koru
        Vector2 v = rb.linearVelocity;
        v.x = moveInput * moveSpeed;
        rb.linearVelocity = v;

        // Zıplama: sadece burada kuvvet/velocity uygula, basıldı flag’ini tüket
        if (jumpPressed)
        {
            if (isGrounded)
            {
                // direkt velocity set, daha keskin his
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            jumpPressed = false; // input tek kullanımlık
        }
    }

    // İsteğe bağlı: sahnede görsel kontrol
    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
