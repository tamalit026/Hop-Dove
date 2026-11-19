using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private int damage = 20;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void AttackTarget()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
         Enemy enemy = collision.GetComponent<Enemy>();

         if (enemy != null)
         {
            enemy.TakeDamage(damage);
         }
        
    }
}
