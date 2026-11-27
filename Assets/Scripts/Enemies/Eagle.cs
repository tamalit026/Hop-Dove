using System.Collections;
using UnityEngine;

public sealed class Eagle : Enemy
{
    [SerializeField] private float grabHeight = 1.5f;    
    [SerializeField] private float grabDuration = 2f;    
    [SerializeField] private float attackCooldown = 3f;  
    [SerializeField] private Transform grabPoint;      

    private Player grabbedPlayer;         
    private Transform originalParent;      
    private Coroutine grabCoroutine;
    private bool isAttackPending;

    protected override void Awake()
    {
        base.Awake();          
        detectionRange = 5f;   
    }
    protected override void Update()
    {
        base.Update();         

        if (isDead) return;                
        if (grabbedPlayer != null) return; 
        if (Time.time < nextAttackTime) return; 
        
        if (targetPlayer != null && DetectPlayer(targetPlayer))
            Attack();
    }
    public override bool DetectPlayer(Player player)
    {
        if (player == null) return false;
        if (player.IsInvulnerable) return false; 

        if (Vector2.Distance(transform.position, player.transform.position) > detectionRange)
            return false;

        return Mathf.Abs(transform.position.y - player.transform.position.y) <= grabHeight;
    }

    public override void Attack()
    {
        if (grabbedPlayer != null || isAttackPending) return;

        isAttackPending = true;

        animator?.SetTrigger("Attack");

        Invoke(nameof(GrabPlayer), 0.3f);
    }
    private void GrabPlayer()
    {
        isAttackPending = false;

        if (isDead || grabbedPlayer != null) return;

        if (targetPlayer == null) return;

        if (!DetectPlayer(targetPlayer)) return;

        grabbedPlayer = targetPlayer;
        originalParent = grabbedPlayer.transform.parent; 

        if (grabPoint != null)
        {
            grabbedPlayer.transform.SetParent(grabPoint);
            grabbedPlayer.transform.localPosition = Vector3.zero;
        }
        else
        {
            grabbedPlayer.transform.SetParent(transform);
            grabbedPlayer.transform.localPosition = Vector3.zero;
        };

        grabbedPlayer.TakeDamage(damage);


        grabCoroutine = StartCoroutine(GrabRoutine());
    }
    private IEnumerator GrabRoutine()
    {
        yield return new WaitForSeconds(grabDuration);
        DropPlayer();
    }
    private void DropPlayer()
    {
        if (grabbedPlayer == null) return;

        grabbedPlayer.transform.SetParent(originalParent);
        grabbedPlayer = null;
        originalParent = null;

        nextAttackTime = Time.time + attackCooldown;

        if (grabCoroutine != null)
        {
            StopCoroutine(grabCoroutine);
            grabCoroutine = null;
        }
    }
    protected override void Die()
    {
        DropPlayer();   
        base.Die();    
    }

    private void OnDisable()
    {
        DropPlayer();  
    }
}