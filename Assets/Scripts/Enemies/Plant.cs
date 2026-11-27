using UnityEngine;

//TP2 - Agustina Mendiri
public sealed class Plant : Enemy
{
    [SerializeField] private float attackRange = 8f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float attackDelay = 0.25f;  
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    private float lastAttackTime;

    protected override void Awake()
    {
        base.Awake();                      
        lastAttackTime = -attackCooldown;  
    }
    protected override void Update()
    {
        base.Update();                   

        if (isDead || targetPlayer == null) return;
        if (!DetectPlayer(targetPlayer)) return;
        if (Time.time < lastAttackTime + attackCooldown) return;

        lastAttackTime = Time.time;
        Attack();
    }
    public override void Attack()
    {
        if (animator != null) animator.SetTrigger("Attack");
        Invoke(nameof(ThrowProjectile), attackDelay);
    }

    public void ThrowProjectile()
    {
        if (isDead) return;                
        if (projectilePrefab == null) return;

        Transform spawnPos = firePoint != null ? firePoint : transform;
        GameObject projObj = Instantiate(projectilePrefab, spawnPos.position, spawnPos.rotation);

        Projectile projectile = projObj.GetComponent<Projectile>();
        if (projectile == null) return;

        Vector2 dir = spawnPos.right;

        projectile.Launch(dir);
    }
    public override bool DetectPlayer(Player player)
    {
        if (player == null) return false;
        return Vector2.Distance(transform.position, player.transform.position) <= attackRange;
    }

    protected override void Die()
    {
        CancelInvoke();   
        base.Die();
    }
}
