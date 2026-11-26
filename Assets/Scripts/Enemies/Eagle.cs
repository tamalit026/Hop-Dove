using System.Collections;
using UnityEngine;

public class Eagle : Enemy
{
    [SerializeField] private float grabHeight = 2f;
    [SerializeField] private float grabDuration = 3f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private Transform grabPoint;

    private Player targetPlayer;
    private Player grabbedPlayer;
    private Transform originalParent;
    private Animator animator;

    private float nextAttackTime;
    private Coroutine grabCoroutine;
    private bool isDead;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        targetPlayer = FindFirstObjectByType<Player>();
    }

    private void Update()
    {
        if (isDead || grabbedPlayer != null)
            return;

        if (Time.time < nextAttackTime)
            return;

        if (targetPlayer == null)
            targetPlayer = FindFirstObjectByType<Player>();

        if (DetectPlayer(targetPlayer))
        {
            Attack();
        }
    }

    public override void TakeDamage(int damageAmount)
    {
        if (isDead)
            return;

        health -= damageAmount;

        if (health <= 0)
        {
            isDead = true;

            if (grabbedPlayer != null)
                DropPlayer(grabbedPlayer);

            Destroy(gameObject);
        }
    }

    public override void Attack()
    {
        if (isDead || grabbedPlayer != null)
            return;

        if (Time.time < nextAttackTime)
            return;

        if (!DetectPlayer(targetPlayer))
            return;

        if (animator != null)
            animator.SetTrigger("Attack");

        GrabPlayer(targetPlayer);
    }

    public override bool DetectPlayer(Player player)
    {
        if (player == null)
            return false;

        if (player.IsInvulnerable)
            return false;

        return Vector2.Distance(
            transform.position,
            player.transform.position
        ) <= grabHeight;
    }

    public void GrabPlayer(Player player)
    {
        if (isDead || player == null || grabbedPlayer != null)
            return;

        if (player.IsInvulnerable)
            return;

        grabbedPlayer = player;
        originalParent = player.transform.parent;

        if (grabPoint != null)
        {
            player.transform.SetParent(grabPoint);
            player.transform.localPosition = Vector3.zero;
        }
        else
        {
            player.transform.SetParent(transform);
        }

        grabbedPlayer.TakeDamage(damage);

        grabCoroutine = StartCoroutine(GrabRoutine());
    }

    public void DropPlayer(Player player)
    {
        if (grabbedPlayer == null || grabbedPlayer != player)
            return;

        if (grabCoroutine != null)
        {
            StopCoroutine(grabCoroutine);
            grabCoroutine = null;
        }

        grabbedPlayer.transform.SetParent(originalParent);

        grabbedPlayer = null;
        originalParent = null;

        nextAttackTime = Time.time + attackCooldown;
    }

    private IEnumerator GrabRoutine()
    {
        yield return new WaitForSeconds(grabDuration);

        grabCoroutine = null;

        DropPlayer(grabbedPlayer);
    }

    private void OnDisable()
    {
        if (grabbedPlayer != null)
            DropPlayer(grabbedPlayer);
    }
}