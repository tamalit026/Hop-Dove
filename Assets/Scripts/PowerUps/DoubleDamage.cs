
//TP2 - Eileen Moscovich.
public sealed class DoubleDamage : PowerUp
{
    public override void Activate(Player player)
    {
        if (player == null) return;

        player.DamageMultiplier = 2f;
        StartDuration(player);
    }

    public override void Deactivate(Player player)
    {
        if (player != null)
            player.DamageMultiplier = 1f;
    }

    protected override string GetActivationMessage()
    {
        return "Double Damage Activated!";
    }
}
