using UnityEngine;

//TP2 - Eileen Moscovich.
public sealed class Attack : MonoBehaviour
{
    [SerializeField] private int damage = 20;
    [SerializeField] private float attackCooldown = 0.8f;

    private Animator animator;
    private Player player;
    private AttackHitbox attackHitbox;

    private float nextAttackTime;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        attackHitbox = GetComponent<AttackHitbox>();
    }
    public void AttackTarget()
    {
        if (Time.time < nextAttackTime) return;

        nextAttackTime = Time.time + attackCooldown;

        AudioManager.Instance.PlaySFX(SoundEffect.Attack);

        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }
    public void DealDamage(Enemy enemy)
    {
        if (enemy == null) return;

        float multiplier = player != null ? player.DamageMultiplier : 1f;
        int finalDamage = Mathf.RoundToInt(damage * multiplier);

        enemy.TakeDamage(finalDamage);
    }
}
