using UnityEngine;

//TP2 - Eileen Moscovich.
public sealed class AcidPool : Obstacle
{
    [SerializeField] private float damageInterval = 1f;

    private float nextDamageTime;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        Player player = other.GetComponent<Player>();

        if (player != null)
            nextDamageTime = Time.time + damageInterval;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player == null) return;

        if (Time.time >= nextDamageTime)
        {
            player.TakeDamage(damage);
            nextDamageTime = Time.time + damageInterval;
        }
    }
    protected override void ApplyEffect(Player player)
    {
    }
}
