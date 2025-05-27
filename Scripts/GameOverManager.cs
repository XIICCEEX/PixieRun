using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    public void ShowGameOver()
    {
        Time.timeScale = 0f; // หยุดเกม
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // รีเซ็ตความเร็วเกม
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
