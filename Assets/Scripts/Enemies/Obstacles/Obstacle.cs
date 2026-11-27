using UnityEngine;

//TP2 - Eileen Moscovich.
public abstract class Obstacle : MonoBehaviour
{
    [SerializeField] protected int damage = 20;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            player.TakeDamage(damage);
            ApplyEffect(player);
        }
    }
    protected abstract void ApplyEffect(Player player);
}
