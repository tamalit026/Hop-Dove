using Unity.VisualScripting;
using UnityEngine;

public sealed class Ant : MonoBehaviour, IInteractable
{
    [SerializeField] private int value = 10;

    public void Interact(Player player)
    {
        if (player != null)
        {
            AudioManager.Instance.PlaySFX(SoundEffect.PickupCoin);
            player.AddScore(value);
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
