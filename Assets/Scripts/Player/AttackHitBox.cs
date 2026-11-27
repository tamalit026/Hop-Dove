using UnityEngine;
using System.Collections.Generic;

//TP2 - Eileen Moscovich.
public sealed class AttackHitbox : MonoBehaviour
{
    [SerializeField] private Collider2D hitbox;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private float extraDistanceWhenFlipped = 0.3f;

    private Attack attack;
    private bool canHit = false;
    private Vector3 initialHitboxLocalPosition;
    private Transform hitboxTransform;

    private HashSet<Enemy> hitEnemies = new HashSet<Enemy>();

    private void Awake()
    {
        attack = GetComponent<Attack>();

        if (playerSprite == null)
            playerSprite = GetComponentInParent<SpriteRenderer>();

        if (hitbox != null)
        {
            hitbox.isTrigger = true;
            hitbox.enabled = false;
        }

        hitboxTransform = hitbox.transform;
        initialHitboxLocalPosition = hitboxTransform.localPosition;
    }
    private void LateUpdate()
    {
        if (hitboxTransform == null || playerSprite == null) return;

        Vector3 position = initialHitboxLocalPosition;

        if (playerSprite.flipX)
            position.x = -initialHitboxLocalPosition.x - extraDistanceWhenFlipped;

        hitboxTransform.localPosition = position;
    }
    public void EnableHitbox()
    {
        hitEnemies.Clear();
        canHit = true;

        if (hitbox != null)
            hitbox.enabled = true;
    }
    public void DisableHitbox()
    {
        canHit = false;

        if (hitbox != null)
            hitbox.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canHit) return;

        if ((enemyLayer.value & (1 << collision.gameObject.layer)) == 0)
            return;

        Chest chest = collision.GetComponentInParent<Chest>();

        if (chest != null)
        {
            chest.Open();
            return;
        }

        Enemy enemy = collision.GetComponentInParent<Enemy>();

        if (enemy == null) return;

        if (hitEnemies.Contains(enemy)) return;

        hitEnemies.Add(enemy);

        if (attack != null)
            attack.DealDamage(enemy);
    }
}

