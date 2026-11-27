using UnityEngine;

//TP2 - Agustina Mendiri
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health = 50;
    [SerializeField] protected int damage = 10;
    [SerializeField] protected float moveSpeed = 2f;

    [SerializeField] protected float detectionRange = 5f; 
    [SerializeField] protected LayerMask detectionMask;    

    protected float nextAttackTime = 0f;
    protected bool isDead = false;

    protected Animator animator;
    protected Player targetPlayer;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        targetPlayer = FindFirstObjectByType<Player>();
    }

    protected virtual void Update()
    {
        if (isDead) return;

        if (targetPlayer == null)
            targetPlayer = FindFirstObjectByType<Player>();
    }
    public virtual void TakeDamage(int dmg)
    {
        if (isDead) return;
        health -= dmg;
        if (health <= 0) Die();
    }
    protected virtual void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }
    public abstract void Attack();
    public abstract bool DetectPlayer(Player player);
}
