using UnityEngine;

//TP2 - Agustina Mendiri
public sealed class Slug : Enemy
{
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 2f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float lastAttackTime;

    protected override void Awake()
    {
        base.Awake();              
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        lastAttackTime = -attackCooldown;
    }

    protected override void Update()
    {
        base.Update(); 

        if (targetPlayer == null) return; 

        if (!DetectPlayer(targetPlayer)) return;

        if (spriteRenderer != null)
            spriteRenderer.flipX = targetPlayer.transform.position.x > transform.position.x;

        float distance = Vector2.Distance(transform.position, targetPlayer.transform.position);

        if (distance <= attackRange)
        {
   
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

            if (Time.time >= lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
        else
        {
            float dir = targetPlayer.transform.position.x > transform.position.x ? 1f : -1f;
            rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);
        }
    }
    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);
    }
    public override void Attack()
    {
        animator?.SetTrigger("Attack");

        if (targetPlayer != null &&
            Vector2.Distance(transform.position, targetPlayer.transform.position) <= attackRange)
        {
            targetPlayer.TakeDamage(damage);
        }
    }
    public override bool DetectPlayer(Player player)
    {
        if (player == null) return false;
        return Vector2.Distance(transform.position, player.transform.position) <= detectionRange;
    }
}
