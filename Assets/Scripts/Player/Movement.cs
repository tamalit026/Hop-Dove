using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Transform groundCheck; 
    [SerializeField] private float checkRadius = 0.2f; 
    [SerializeField] private LayerMask groundLayer;

    private bool isGrounded; 
    private Player player;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        }
        else
        {
            isGrounded = Mathf.Abs(rb.linearVelocity.y) < 0.01f;
        }
    }
    public void Move(float direction)
    {
        if (rb == null) return;

        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        bool isMoving = Mathf.Abs(direction) > 0.01f;
        if (animator != null) animator.SetBool("IsMoving", isMoving);

        if (direction > 0f && spriteRenderer != null)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction < 0f && spriteRenderer != null)
        {
            spriteRenderer.flipX = true;
        }
    }

    public void Jump()
    {
        if (rb == null) return;

        if (isGrounded)
        { 
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        else if (player != null && player.CanDoubleJump)
        { 
            DoubleJump();
        }
    }

    public void DoubleJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * 0.9f);
        player.CanDoubleJump = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null) 
        { 
            Gizmos.color = Color.green; 
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius); 
        }
    }
}
