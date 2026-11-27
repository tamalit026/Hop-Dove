using System;
using UnityEngine;

public sealed class Player : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private PlayerStats startingStats = new PlayerStats(100, 100, 5f, 5f);

    [SerializeField] private float damageMultiplier = 1f;
    [SerializeField] private float coinsMultiplier = 1.0f;
    [SerializeField] private bool isInvulnerable = false;

    public int Health => health;
    public int MaxHealth => maxHealth;
    public float DamageMultiplier { get => damageMultiplier; set => damageMultiplier = value; }
    public float CoinsMultiplier { get => coinsMultiplier; set => coinsMultiplier = value; }
    public bool IsInvulnerable { get => isInvulnerable; set => isInvulnerable = value; }

    public Movement MovementComponent { get; private set; }
    public Attack AttackComponent { get; private set; }
    public Control ControlComponent { get; private set; }

    private Animator animator;

    public static event Action<int, int> OnHealthChanged;
    public static event Action OnPlayerDied;

    private void Awake()
    {
        MovementComponent = GetComponent<Movement>();
        AttackComponent = GetComponent<Attack>();
        ControlComponent = GetComponent<Control>();
        animator = GetComponent<Animator>();

        maxHealth = startingStats.MaxHealth;
        health = Mathf.Clamp(startingStats.Health, 0, maxHealth);

        if (MovementComponent != null)
        {
            MovementComponent.Initialize(startingStats.MovementSpeed,startingStats.JumpForce);
        }
    }
    private void Start()
    {
        OnHealthChanged?.Invoke(health, maxHealth);
    }

    public void TakeDamage(int damage)
    {

        if (isInvulnerable) return;

        if (isInvulnerable) return;

            health = Mathf.Max(0, health - damage);
            OnHealthChanged?.Invoke(health, maxHealth);

        AudioManager.Instance.PlaySFX(SoundEffect.TakeDamage);

        if (health <= 0)
        {
            if (animator != null) animator.SetTrigger("Dead");
            OnPlayerDied?.Invoke();
            if (GameManager.Instance != null)
            {
                GameManager.Instance.TriggerDefeat();
            }
        }
        else
        {
            if (animator != null) animator.SetTrigger("Hurt");
        }
    }
    public void Heal(int amount)
    {
        health = Mathf.Min(maxHealth, health + amount);
        OnHealthChanged?.Invoke(health, maxHealth);
    }
    public void AddScore(int amount)
    {
        int finalAmount = Mathf.RoundToInt(amount * coinsMultiplier);
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(finalAmount);
        }
    }
}
