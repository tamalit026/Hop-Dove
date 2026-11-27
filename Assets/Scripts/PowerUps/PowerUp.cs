using System.Collections;
using UnityEngine;
using TMPro;

//TP2 - Eileen Moscovich.
public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] protected float duration = 5f;
    [SerializeField] protected TextMeshProUGUI activationText;

    protected BoxCollider2D powerUpCollider;
    protected SpriteRenderer spriteRenderer;
    public float Duration => duration;
    protected virtual void Awake()
    {
        powerUpCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public abstract void Activate(Player player);
    public abstract void Deactivate(Player player);
    protected abstract string GetActivationMessage();

    protected void StartDuration(Player player)
    {
        AudioManager.Instance.PlaySFX(SoundEffect.PowerUp);
        powerUpCollider.enabled = false;
        spriteRenderer.enabled = false;

        activationText.text = GetActivationMessage();
        activationText.gameObject.SetActive(true);

        StartCoroutine(DurationRoutine(player));
    }
    private IEnumerator DurationRoutine(Player player)
    {
        yield return new WaitForSeconds(duration);

        Deactivate(player);

        activationText.gameObject.SetActive(false);

        Destroy(gameObject);
    }
}
