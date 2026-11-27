using UnityEngine;

//TP2 - Agustina Mendiri

public sealed class Spikes : Obstacle
{
    [SerializeField] private float knockbackForce = 8f;

    protected override void ApplyEffect(Player player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = new Vector2( rb.linearVelocity.x,knockbackForce);
        }
    }
}