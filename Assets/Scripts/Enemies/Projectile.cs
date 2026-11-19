using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage = 15;
    [SerializeField] private float speed = 8f;
    [SerializeField] private Vector2 direction = Vector2.left;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 dir)
    {
        direction = dir.normalized;
        if (rb != null) rb.linearVelocity = direction * speed;
    }

    public void HitPlayer(Player player)
    {
        if (player != null) player.TakeDamage(damage);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null) HitPlayer(player);
    }
}
