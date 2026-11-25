using System.Collections;
using UnityEngine;

public class DoubleCoins : PowerUp
{
    public override void Activate(Player player)
    {
        SetCoinsMultiplier(player, 2.0f);
        StartCoroutine(DurationRoutine(player));
    }

    public override void Deactivate(Player player)
    {
        SetCoinsMultiplier(player, 1.0f);
    }

    public void SetCoinsMultiplier(Player player, float multiplier)
    {
        if (player != null) player.CoinsMultiplier = multiplier;
    }

    private IEnumerator DurationRoutine(Player player)
    {
        yield return new WaitForSeconds(duration);
        Deactivate(player);
        Destroy(gameObject);
    }
}
