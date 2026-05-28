using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject gameOverPanel;

    // =========================
    // Start按钮
    // =========================
    public void StartGame()
    {
        AudioManager.Instance.PlayButtonSound();

        startPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    // =========================
    // Restart按钮
    // =========================
    public void RestartGame()
    {
        AudioManager.Instance.PlayButtonSound();

        Time.timeScale = 1f;

        StartCoroutine(RestartDelay());
    }

    IEnumerator RestartDelay()
    {
        yield return new WaitForSeconds(0.2f);

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    // =========================
    // Exit按钮
    // =========================
    public void ExitGame()
    {
        AudioManager.Instance.PlayButtonSound();

        StartCoroutine(ExitDelay());
    }

    IEnumerator ExitDelay()
    {
        yield return new WaitForSeconds(0.2f);

        Application.Quit();
    }
}