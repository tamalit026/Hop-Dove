
//TP2 - Eileen Moscovich.
public sealed class DoubleCoins : PowerUp
{
    public override void Activate(Player player)
    {
        if (player == null) return;

        player.CoinsMultiplier = 2f;
        StartDuration(player);
    }
    public override void Deactivate(Player player)
    {
        if (player != null)
            player.CoinsMultiplier = 1f;
    }
    protected override string GetActivationMessage()
    {
        return "Double Coins Activated!";
    }
}
