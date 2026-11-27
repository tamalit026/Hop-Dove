using UnityEngine;
using UnityEngine.SceneManagement;

//TP2 - Eileen Moscovich.
public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel; 
    [SerializeField] private GameObject controlsPanel; 
    [SerializeField] private GameObject creditsPanel;

    [SerializeField] private string levelSceneName = "MainScene";

    private void Start() 
    { 
        ShowMainPanel(); 
    }
    public void PlayGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(levelSceneName);
    }
    public void ShowControls()
    {
        if (mainPanel != null) mainPanel.SetActive(false); 
        if (controlsPanel != null) controlsPanel.SetActive(true); 
        if (creditsPanel != null) creditsPanel.SetActive(false);
    }
    public void ShowCredits()
    {
        if (mainPanel != null) mainPanel.SetActive(false); 
        if (controlsPanel != null) controlsPanel.SetActive(false); 
        if (creditsPanel != null) creditsPanel.SetActive(true);
    }
    public void ShowMainPanel() 
    { 
        if (mainPanel != null) mainPanel.SetActive(true); 
        if (controlsPanel != null) controlsPanel.SetActive(false); 
        if (creditsPanel != null) creditsPanel.SetActive(false); 
    }
    public void QuitGame() 
    { 
        Application.Quit(); 
    }
}
