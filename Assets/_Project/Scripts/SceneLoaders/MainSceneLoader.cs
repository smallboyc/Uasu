using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneLoader : MonoBehaviour
{
    [SerializeField] private string startingLevel = "Level_01_Main";

    void Awake()
    {
        SceneManager.LoadSceneAsync(startingLevel, LoadSceneMode.Additive);
    }
}
