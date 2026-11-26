using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private int damage = 20;
    [SerializeField] private float knockbackForce = 8f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            player.TakeDamage(damage);

            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, knockbackForce);
            }
        }
    }
}