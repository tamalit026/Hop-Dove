using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health = 50;
    [SerializeField] protected int damage = 10;
    [SerializeField] protected float moveSpeed = 2f;

    public int Health => health;
    public int Damage => damage;
    public float MoveSpeed => moveSpeed;

    public abstract void TakeDamage(int damage);
    public abstract void Attack();
    public abstract bool DetectPlayer(Player player);
}
