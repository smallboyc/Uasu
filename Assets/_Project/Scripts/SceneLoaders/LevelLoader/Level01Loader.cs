using UnityEngine;
using UnityEngine.SceneManagement;

public class Level01Loader : MonoBehaviour
{
    [SerializeField]
    public string[] scenesToLoad = {
        "Level_01_Gameplay",
        "Level_01_Environment",
    };

    public void LoadScenes()
    {
        foreach (var scene in scenesToLoad)
        {
            SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        }

        SceneManager.UnloadSceneAsync("UI_Menu_Playground");
    }
}
