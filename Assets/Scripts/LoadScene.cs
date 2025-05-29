using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private int currentLevel;

    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    // เริ่มเกม (โหลด Scene ถัดไป)
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // ปิดเกม
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

}