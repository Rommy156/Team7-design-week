using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    void Start()
    {
        int score = FindObjectOfType<TeamPlayerController>()?.teamScore ?? 0;
        finalScoreText.text = "Final Score: " + score;
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
