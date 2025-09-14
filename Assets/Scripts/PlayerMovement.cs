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
    public Vector2 groundCheckSize;

    Rigidbody2D rb;


    float moveInput;
    bool jumpPressed;

    float coyoteCounter;
    [SerializeField] private float coyoteTime = 0.15f;
    [SerializeField] private float xSpeedBonusLimit;
    //[SerializeField] private float bonusJumpMultiplier = 1.25f;
    [SerializeField] private ParticleSystem superSpeedPS;
    [SerializeField] private float superSpeedLimit;
    bool isGrounded;

    [SerializeField] private float screenLimit;
    bool isOutOfScreen = false;
    int psCounter = 0;
    bool canJump = true;
    RaycastHit2D hit2D;
    SinglePlatform sp;
    public float angle;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {

        CheckIfOutOfScreen();
        moveInput = Input.GetAxisRaw("Horizontal");
        if (rb.linearVelocityY > superSpeedLimit && psCounter == 0)
        {
            superSpeedPS.Play();
            psCounter = 1;
        }
        else if (rb.linearVelocityY <= superSpeedLimit && psCounter != 0)
        {
            superSpeedPS.Stop();
            psCounter = 0;
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        jumpPressed = true;
    }

    void FixedUpdate()
    {
        ScreenWrap();

        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, angle, groundMask);


        //isGrounded = canJump;
        MoveHorizontally();
        //rb.AddForce(Vector2.right * moveInput * moveSpeed, ForceMode2D.Force);
        CoyoteTime();
        Jump();

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(groundCheck.position, groundCheckSize * 1.01f);
    }
    private void Jump()
    {
        if (jumpPressed)
        {
            if (coyoteCounter > 0)
            {
                if (rb.linearVelocityY <= 0)
                {
                    Collider2D hit = Physics2D.OverlapBox(groundCheck.position, groundCheckSize * 1.1f, angle, groundMask);
                    if (hit != null)
                    {

                        hit.gameObject.TryGetComponent<SinglePlatform>(out sp);

                    }
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                    Vector2 jumpVector = Vector2.up * jumpForce;
                    rb.AddForce(jumpVector, ForceMode2D.Impulse);
                    GameManager.Instance.SetHighestY(transform.position.y);
                    AudioManager.Instance.PlaySFX(AudioManager.SFXClip.jump);
                    if (sp != null && !sp.IsUsed())
                    {
                        sp.UsedIt();
                        Debug.Log("thıs is : " + hit.gameObject.name + " ");
                        sp = null;
                    }
                }


            }

            jumpPressed = false;
            coyoteCounter = 0;
        }
    }

    private void CoyoteTime()
    {
        if (isGrounded)
        {
            coyoteCounter = coyoteTime;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;

        }
    }

    private void MoveHorizontally()
    {
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * 1, transform.localScale.y, transform.localScale.z);

        }
        if (moveInput < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);

        }
        Vector2 v = rb.linearVelocity;
        v.x = moveInput * moveSpeed;
        rb.linearVelocity = v;
    }

    public void ScreenWrap()
    {
        if (isOutOfScreen)
        {
            if (transform.position.x < 0)
            {
                transform.position = new Vector3(-1f * (transform.position.x + 0.2f), transform.position.y, transform.position.z);
            }
            else if (transform.position.x > 0)
            {
                transform.position = new Vector3(-1f * (transform.position.x - 0.2f), transform.position.y, transform.position.z);
            }

            isOutOfScreen = false;
        }
    }
    public void PowerItUp(Vector3 powerVector)
    {

        rb.AddForce(powerVector, ForceMode2D.Impulse);
    }
    public void SetMoveInput(float MoveInput)
    {
        moveInput = MoveInput;
    }
    void CheckIfOutOfScreen()
    {
        if (Mathf.Abs(transform.position.x) > screenLimit)
        {
            isOutOfScreen = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
