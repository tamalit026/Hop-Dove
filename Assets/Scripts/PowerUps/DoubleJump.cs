using System.Collections;
using UnityEngine;

public class DoubleJump : PowerUp
{
    private BoxCollider2D collider;
    private SpriteRenderer spriteRenderer;
    void start()
    {
        collider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Activate(Player player)
    {
        SetDoubleJump(player, true);
        StartCoroutine(DurationRoutine(player));
    }

    public override void Deactivate(Player player)
    {
        SetDoubleJump(player, false);
    }

    public void SetDoubleJump(Player player, bool enabled)
    {
        if (player != null) player.CanDoubleJump = enabled;
    }

    private IEnumerator DurationRoutine(Player player)
    {
        yield return new WaitForSeconds(duration);
        Deactivate(player);
        Destroy(gameObject);
    }
}
