using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private string currentScene = "Level1";

    public static event GameStateEventHandler OnGameStateChanged;

    public void SetGameState(GameState newState)
    {
        OnGameStateChanged?.Invoke(newState);
    }

    public void LoadLevel()
    {
        SetGameState(GameState.Playing);
        SceneManager.LoadScene(currentScene);
    }

    public void LoadVictory()
    {
        SetGameState(GameState.Victory);
    }

    public void LoadDefeat()
    {
        SetGameState(GameState.Defeat);
    }

    public void RestartLevel()
    {
        SetGameState(GameState.Playing);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
