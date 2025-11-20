using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Level_01_Main");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMain()
    {
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
    }
}

