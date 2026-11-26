using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Slug : Enemy
{
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float detectionRange = 6f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float lastAttackTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Player player = Object.FindFirstObjectByType<Player>();
        if (player != null && DetectPlayer(player))
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (spriteRenderer != null)
            { 
                spriteRenderer.flipX = player.transform.position.x > transform.position.x;
            }

            if (distance <= attackRange) 
                { rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); 
                if (Time.time >= lastAttackTime + attackCooldown) 
                {
                    Attack(); 
                    lastAttackTime = Time.time; 
                } 
            }
            else 
            { 
                float dir = player.transform.position.x > transform.position.x ? 1f : -1f; 
                rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y); 
            }
        }
    }

    public override void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0) Destroy(gameObject);
    }

    public override void Attack()
    {
        if (animator != null) animator.SetTrigger("Attack");

        Player player = Object.FindFirstObjectByType<Player>();
        if (player != null && Vector2.Distance(transform.position, player.transform.position) <= attackRange)
        {
            player.TakeDamage(damage);
        }
    }

     public override bool DetectPlayer(Player player)
    {
        if (player == null) return false;
        return Vector2.Distance(transform.position, player.transform.position) <= detectionRange;
    }
}
