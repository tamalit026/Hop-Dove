using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private int antAmount = 5;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private GameObject antPrefab;

    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openSprite;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && closedSprite != null)
        {
            spriteRenderer.sprite = closedSprite;
        }
    }

    public void Interact(Player player)
    {
        if (!isOpen) Open();
    }

    public void Open()
    {
        if (isOpen) return;

        isOpen = true;

        if (spriteRenderer != null && openSprite != null)
        {
            spriteRenderer.sprite = openSprite;
        }

        SpawnAnt();
    }

    public void SpawnAnt()
    {
        if (antPrefab == null)
        {
            Debug.LogWarning("No anda el prefab");
            return;
        }

        for (int i = 0; i < antAmount; i++)
        {
            if (antPrefab != null)
            {
                Vector3 spawnOffset = new Vector3(Random.Range(-0.8f, 0.8f), 1.2f, 0);
                Instantiate(antPrefab, transform.position + spawnOffset, Quaternion.identity);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null || collision.CompareTag("Player") || collision.gameObject.name.Contains("Attack"))
        {
            Open();
        }
    }
}
