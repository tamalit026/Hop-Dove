using UnityEngine;

public class Eagle : Enemy
{
    [SerializeField] private float flightHeight = 8f;
    [SerializeField] private float grabHeight = 2f;

    private Player grabbedPlayer;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            if (grabbedPlayer != null) DropPlayer(grabbedPlayer);
            Destroy(gameObject);
        }
    }

    public override void Attack()
    {
        if (animator != null) animator.SetTrigger("Attack");
        Player player = FindObjectOfType<Player>();
        if (player != null && DetectPlayer(player))
        {
            GrabPlayer(player);
        }
    }

    public override bool DetectPlayer(Player player)
    {
        if (player == null) return false;
        return Vector2.Distance(transform.position, player.transform.position) <= grabHeight;
    }

    public void GrabPlayer(Player player)
    {
        grabbedPlayer = player;
        grabbedPlayer.transform.SetParent(transform);
        grabbedPlayer.TakeDamage(damage);
    }

    public void DropPlayer(Player player)
    {
        if (grabbedPlayer == player)
        {
            grabbedPlayer.transform.SetParent(null);
            grabbedPlayer = null;
        }
    }
}
