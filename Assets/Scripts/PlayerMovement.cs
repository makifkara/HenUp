using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 7f;

    [Header("Jump")]
    public float jumpForce = 10f;
    public LayerMask groundMask;
    public Transform groundCheck;
    public float groundCheckRadius = 0.15f;

    Rigidbody2D rb;


    float moveInput;
    bool jumpPressed;

    float coyoteCounter;
    [SerializeField] private float coyoteTime = 0.15f;
    int bonusJumpCounter;
    [SerializeField] private float xSpeedBonusLimit;
    [SerializeField] private float bonusJumpMultiplier = 1.25f;
    [SerializeField] private ParticleSystem bonusJumpPS;
    bool isGrounded;

    [SerializeField] private float screenLimit;
    bool isOutOfScreen = false;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        CheckIfOutOfScreen();
        moveInput = Input.GetAxisRaw("Horizontal");


        //if (Input.GetKeyDown(KeyCode.Space))
        jumpPressed = true;
    }

    void FixedUpdate()
    {
        if (isOutOfScreen)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
            isOutOfScreen = false;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask);


        Vector2 v = rb.linearVelocity;
        v.x = moveInput * moveSpeed;
        rb.linearVelocity = v;

        //rb.AddForce(Vector2.right * moveInput * moveSpeed, ForceMode2D.Force);
        if (isGrounded)
        {
            coyoteCounter = coyoteTime;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;

        }

        if (jumpPressed)
        {
            if (coyoteCounter > 0)
            {
                if (rb.linearVelocityY <= 0)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                    Vector2 jumpVector = Vector2.up * jumpForce;
                    jumpVector = BonusJump(jumpVector);
                    rb.AddForce(jumpVector, ForceMode2D.Impulse);
                }


            }

            jumpPressed = false;
            coyoteCounter = 0;
        }
    }
    void CheckIfOutOfScreen()
    {
        if (Mathf.Abs(transform.position.x) > screenLimit)
        {
            isOutOfScreen = true;
        }
    }
    Vector2 BonusJump(Vector2 jumpVector)
    {
        /*
        if (rb.linearVelocity.x == 0f)
        {
            bonusJumpCounter++;
            if (bonusJumpCounter > 2)
            {
                jumpVector *= bonusJumpMultiplier;
                bonusJumpCounter = 0;
                bonusJumpPS.Play();
            }

        }*/
        return jumpVector;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
