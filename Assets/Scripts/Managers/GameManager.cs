using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    private LevelUIManager levelUIManager;
    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f;

        levelUIManager = FindFirstObjectByType<LevelUIManager>();

        if (levelUIManager != null)
            levelUIManager.HidePanels();
    }

    public void TriggerVictory()
    {

        Time.timeScale = 0f;
        AudioManager.Instance.PlaySFX(SoundEffect.Victory);

        if (levelUIManager != null)
            levelUIManager.ShowVictory();

    }
    public void TriggerDefeat()
    {
        Time.timeScale = 0f;
        AudioManager.Instance.PlaySFX(SoundEffect.Defeat);

        if (levelUIManager != null)
            levelUIManager.ShowDefeat();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        ScoreManager.Instance.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;

        if (ScoreManager.Instance != null)
            ScoreManager.Instance.ResetScore();

        SceneManager.LoadScene(mainMenuSceneName);
    }

    public static event GameStateEventHandler OnGameStateChanged;
    public GameState CurrentState { get; private set; }
    public void SetGameState(GameState newState)
    {
        CurrentState = newState;
        OnGameStateChanged?.Invoke(newState);
    }
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
