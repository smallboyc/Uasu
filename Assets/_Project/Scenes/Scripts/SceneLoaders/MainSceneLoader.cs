using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneLoader : MonoBehaviour
{
    [SerializeField] private string currentSceneToLoad = "SplashScreen";

    void Awake()
    {
        SceneManager.LoadSceneAsync(currentSceneToLoad, LoadSceneMode.Additive);
    }
}
