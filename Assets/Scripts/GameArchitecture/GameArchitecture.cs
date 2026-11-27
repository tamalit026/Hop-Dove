using UnityEngine;

//TP2 - Eileen Moscovich.
public enum GameState
{
    MainMenu,
    Playing,
    Victory,
    Defeat
}

public enum SoundEffect
{
    Jump,
    Attack,
    TakeDamage,
    PickupCoin,
    Heal,
    PowerUp,
    Victory,
    Defeat,
    Hover
}

[System.Serializable]
public struct PlayerStats
{
    public int Health;
    public int MaxHealth;
    public float MovementSpeed;
    public float JumpForce;

    public PlayerStats(int health, int maxHealth, float movementSpeed, float jumpForce)
    {
        Health = health;
        MaxHealth = maxHealth;
        MovementSpeed = movementSpeed;
        JumpForce = jumpForce;
    }
}

public delegate void GameStateEventHandler(GameState newState);

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<T>();
                if (_instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    _instance = go.AddComponent<T>();
                }
            }
            return _instance;
        }
    }
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
}

public static class PlayerExtensions
{
    public static bool IsAlive(this Player player)
    {
        return player != null && player.Health > 0;
    }

    public static float GetHealthPercentage(this Player player)
    {
        if (player == null || player.MaxHealth <= 0) return 0f;
        return (float)player.Health / player.MaxHealth;
    }
}
