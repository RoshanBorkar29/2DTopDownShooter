using UnityEngine;
using UnityEngine.UI;
 using UnityEngine.SceneManagement;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;
    public Text scoreText;
    public GameObject gameOverPanel;
    public Slider healthBar;

    public Text powerUpText;
    public Text waveText;
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateScoreText();
        gameOverPanel.SetActive(false);
    }
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // pause the game
    }


    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void UpdateHealthBar(int current, int max)
    {
        healthBar.maxValue = max;
        healthBar.value = current;
    }


    public void UpdateWaveText(int wave)
    {
        waveText.text = "Wave: " + wave;
    }

    public void ShowPowerUpTimer(string powerUpName, float duration)
    {
        StartCoroutine(PowerUpTimerRoutine(powerUpName, duration));
    }

    IEnumerator PowerUpTimerRoutine(string powerUpName, float duration)
    {
        float remaining = duration;

        while (remaining > 0)
        {
            powerUpText.text = $"{powerUpName}: {Mathf.CeilToInt(remaining)}";
            yield return new WaitForSeconds(1f);
            remaining -= 1f;
        }

        powerUpText.text = "";
    }

}
