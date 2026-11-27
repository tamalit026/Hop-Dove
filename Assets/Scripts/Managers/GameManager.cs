using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private LevelUIManager levelUIManager;

    public GameState CurrentState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Start()
    {
        UpdateGameState(SceneManager.GetActiveScene());
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

        UpdateGameState(scene);
    }
    private void UpdateGameState(Scene scene)
    {
        if (scene.name == mainMenuSceneName)
            CurrentState = GameState.MainMenu;
        else
            CurrentState = GameState.Playing;
    }
    public void TriggerVictory()
    {
        if (CurrentState != GameState.Playing) return;

        CurrentState = GameState.Victory;

        Time.timeScale = 0f;
        AudioManager.Instance.PlaySFX(SoundEffect.Victory);

        if (levelUIManager != null)
            levelUIManager.ShowVictory();
    }
    public void TriggerDefeat()
    {
        if (CurrentState != GameState.Playing) return;

        CurrentState = GameState.Defeat;

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
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
