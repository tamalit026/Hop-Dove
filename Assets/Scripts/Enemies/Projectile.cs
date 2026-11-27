using UnityEngine;

//TP2 - Agustina Mendiri
public sealed class Projectile : MonoBehaviour
{
    [SerializeField] private int damage = 15;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private bool destroyOnAnyHit = true;

    private Vector2 direction = Vector2.left;
    private bool launched = false;
    private bool hasHit = false;

    private void Awake()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.isTrigger = true;
    }
    public void Launch(Vector2 dir)
    {
        if (launched) return;                 
        if (dir.sqrMagnitude < 0.0001f) return;

        direction = dir.normalized;
        launched = true;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Destroy(gameObject, lifetime);
    }
    private void Update()
    {
        if (!launched || hasHit) return;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }
    public void HitPlayer(Player player) { }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit) return;

        Player player = collision.GetComponent<Player>()
                     ?? collision.GetComponentInParent<Player>();

        if (player != null)
        {
            hasHit = true;
            player.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        if (destroyOnAnyHit && !collision.isTrigger)
        {
            hasHit = true;
            Destroy(gameObject);
        }
    }
}