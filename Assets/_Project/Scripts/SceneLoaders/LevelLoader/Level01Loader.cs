using UnityEngine;
using UnityEngine.SceneManagement;

public class Level01Loader : MonoBehaviour
{
    [SerializeField]
    private string[] scenesToLoad = {
        "Level_01_Gameplay",
        "Level_01_Environment",
    };

    void Awake()
    {
        foreach (var scene in scenesToLoad)
        {
            SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        }
    }
}
