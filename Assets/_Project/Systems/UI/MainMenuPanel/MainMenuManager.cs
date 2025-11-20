using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level_01_Main");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
