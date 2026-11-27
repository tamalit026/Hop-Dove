using UnityEngine;

public class LevelUIManager : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject defeatPanel;
    public void ShowVictory()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(true);
    }
    public void ShowDefeat()
    {
        if (defeatPanel != null)
            defeatPanel.SetActive(true);
    }
    public void HidePanels()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(false);

        if (defeatPanel != null)
            defeatPanel.SetActive(false);
    }
    public void RestartButton()
    {
        GameManager.Instance.RestartLevel();
    }
    public void MainMenuButton()
    {
        GameManager.Instance.GoToMainMenu();
    }
    public void QuitButton()
    {
        GameManager.Instance.QuitGame();
    }
}
