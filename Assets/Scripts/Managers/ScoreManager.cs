using System;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private int currentScore = 0;
    [SerializeField] private int totalScore = 0;

    public static event Action<int> OnScoreChanged;

    public void AddScore(int amount)
    {
        currentScore += amount;
        totalScore += amount;
        OnScoreChanged?.Invoke(currentScore);
    }

    public void ResetScore()
    {
        currentScore = 0;
        OnScoreChanged?.Invoke(currentScore);
    }

    public int GetScore() => currentScore;
    public int GetTotalScore() => totalScore;
}
