using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;


    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

}