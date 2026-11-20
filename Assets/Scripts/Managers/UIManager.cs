using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image healthBarFill;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        Player.OnHealthChanged += UpdateHealth;
        ScoreManager.OnScoreChanged += UpdateScore;
    }

    private void OnDisable()
    {
        Player.OnHealthChanged -= UpdateHealth;
        ScoreManager.OnScoreChanged -= UpdateScore;
    }

    private void Start()
    {
        int initialScore = ScoreManager.Instance != null ? ScoreManager.Instance.GetScore() : 0;
        UpdateScore(initialScore);
    }
    public void UpdateHealth(int current, int max)
    {
        if (healthBarFill != null && max > 0)
        {
            healthBarFill.fillAmount = (float)current / max;
        }
    }
    public void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
