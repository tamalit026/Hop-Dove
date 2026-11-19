using UnityEngine;

public class Plant : Enemy
{
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    private float lastAttackTime;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null && DetectPlayer(player))
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
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
        ThrowProjectile();
    }

    public override bool DetectPlayer(Player player)
    {
        if (player == null) return false;
        return Vector2.Distance(transform.position, player.transform.position) <= attackRange;
    }

    public void ThrowProjectile()
    {
        if (projectilePrefab == null) return;
        Transform spawnPos = firePoint != null ? firePoint : transform;
        GameObject projObj = Instantiate(projectilePrefab, spawnPos.position, Quaternion.identity);
        Projectile projectile = projObj.GetComponent<Projectile>();
        if (projectile != null)
        {
            Player player = FindObjectOfType<Player>();
            Vector2 dir = player != null ? (player.transform.position - transform.position).normalized : Vector2.left;
            projectile.Launch(dir);
        }
    }
}
