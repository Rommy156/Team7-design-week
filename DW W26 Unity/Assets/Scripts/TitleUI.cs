using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Dual Monitor Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
