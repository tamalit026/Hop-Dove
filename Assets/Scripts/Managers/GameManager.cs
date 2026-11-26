using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject victoryPanel; 
    [SerializeField] private GameObject defeatPanel;
    [SerializeField] private string mainMenuSceneName = "MainMenu";

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

        victoryPanel = GameObject.Find("VictoryPanel");
        defeatPanel = GameObject.Find("DefeatPanel");

        if (victoryPanel != null)
            victoryPanel.SetActive(false);

        if (defeatPanel != null)
            defeatPanel.SetActive(false);
    }

    public void TriggerVictory()
    {

        Time.timeScale = 0f;

        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }
    }

    public void TriggerDefeat()
    {
        Time.timeScale = 0f;
        if (defeatPanel != null) defeatPanel.SetActive(true); 
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
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
    public void SetGameState(GameState newState) 
    { 
        OnGameStateChanged?.Invoke(newState); 
    }
}
