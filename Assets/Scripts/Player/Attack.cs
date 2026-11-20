using Unity.VisualScripting;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Attack : MonoBehaviour
{
    [SerializeField] private int damage = 20;
    [SerializeField] private float attackRange = 2.0f;
    [SerializeField] private Transform attackPoint;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void AttackTarget()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        Vector2 origin = attackPoint != null ? (Vector2)attackPoint.position : (Vector2)transform.position;
        float direction = (spriteRenderer != null && spriteRenderer.flipX) ? -1f : 1f;
        Vector2 attackCenter = origin + new Vector2(direction * (attackRange * 0.5f), 0);

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackCenter, attackRange);
        foreach (Collider2D col in hitColliders)
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Chest chest = col.GetComponent<Chest>();
            if (chest != null)
            {
                chest.Open();
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Vector2 origin = attackPoint != null ? (Vector2)attackPoint.position : (Vector2)transform.position; 
        float direction = (spriteRenderer != null && spriteRenderer.flipX) ? -1f : 1f; 
        Vector2 attackCenter = origin + new Vector2(direction * (attackRange * 0.5f), 0);

        Gizmos.color = Color.red; 
        Gizmos.DrawWireSphere(attackCenter, attackRange);
    }
}
