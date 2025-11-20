using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
