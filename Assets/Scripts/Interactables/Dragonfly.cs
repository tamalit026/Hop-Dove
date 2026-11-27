using UnityEngine;

//TP2 - Itamar Graterol

public sealed class Dragonfly : MonoBehaviour, IInteractable
{
    [SerializeField] private PowerUp powerUp;

    public void Interact(Player player)
    {
        if (powerUp != null && player != null)
        {
            powerUp.Activate(player);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            Interact(player);
        }
    }
}
