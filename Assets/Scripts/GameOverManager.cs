using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    public void ShowGameOver()
    {
        Time.timeScale = 0f; // หยุดเกม
        gameOverPanel.SetActive(true);

        // หยุดเพลงด้วย
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopBGM();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // รีเซ็ตเกม
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ResumeBGM(); // เล่นเพลงกลับมา
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
