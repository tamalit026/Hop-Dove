using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int maxHealth = 100;

    [SerializeField] private bool canDoubleJump = false;
    [SerializeField] private float coinsMultiplier = 1.0f;
    [SerializeField] private bool isInvulnerable = false;

    public int Health => health;
    public int MaxHealth => maxHealth;
    public bool CanDoubleJump { get => canDoubleJump; set => canDoubleJump = value; }
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
    }

    private void Start()
    {
        OnHealthChanged?.Invoke(health, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return;

        health = Mathf.Max(0, health - damage);
        OnHealthChanged?.Invoke(health, maxHealth);

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
