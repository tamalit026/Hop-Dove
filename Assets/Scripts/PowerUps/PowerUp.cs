using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] protected float duration = 5f;
    public float Duration => duration;

    public abstract void Activate(Player player);
    public abstract void Deactivate(Player player);
}
