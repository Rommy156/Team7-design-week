using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Timer")]
    public float matchTime = 120f;
    private float currentTime;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;

    private bool gameOver;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentTime = matchTime;
        UpdateScoreUI(0);
    }

    void Update()
    {
        if (gameOver) return;

        currentTime -= Time.deltaTime;
        currentTime = Mathf.Max(currentTime, 0f);

        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";

        if (currentTime <= 0f)
        {
            EndGame();
        }
    }


    public void UpdateScoreUI(int score)
    {
        scoreText.text = "Score: " + score;
    }

    void EndGame()
    {
        gameOver = true;
        SceneManager.LoadScene("GameOverScene");
    }
}
