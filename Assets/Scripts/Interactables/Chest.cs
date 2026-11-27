using UnityEngine;
using System.Collections;

//TP2 - Itamar Graterol

public sealed class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private int antAmount = 5;
    [SerializeField] private float spawnDelay = 0.25f;
    [SerializeField] private GameObject antPrefab;

    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openSprite;

    private bool isOpen = false;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null && closedSprite != null)
            spriteRenderer.sprite = closedSprite;
    }
    public void Interact(Player player)
    {
        if (player != null)
            Open();
    }
    public void Open()
    {
        if (isOpen) return;

        isOpen = true;

        if (spriteRenderer != null && openSprite != null)
            spriteRenderer.sprite = openSprite;

        StartCoroutine(SpawnAntRoutine());
    }
    private IEnumerator SpawnAntRoutine()
    {
        if (antPrefab == null)
        {
            yield break;
        }

        for (int i = 0; i < antAmount; i++)
        {
            Vector3 spawnOffset = new Vector3(Random.Range(-1f, 1f),Random.Range(0.8f, 1.5f),0f);
            
            Instantiate(antPrefab, transform.position + spawnOffset, Quaternion.identity);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
