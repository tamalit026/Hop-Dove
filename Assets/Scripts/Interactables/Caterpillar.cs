using UnityEngine;

public class Caterpillar : MonoBehaviour, IInteractable
{
    [SerializeField] private int healAmount = 25;

    public void Interact(Player player)
    {
        if (player != null)
        {
            player.Heal(healAmount);
            Destroy(gameObject);
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
