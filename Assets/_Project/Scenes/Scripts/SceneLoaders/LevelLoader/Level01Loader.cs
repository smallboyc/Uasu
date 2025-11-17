#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level01Loader : MonoBehaviour
{
    [SerializeField]
    private SceneAsset[] scenes;

    private void Awake()
    {
        foreach (var scene in scenes)
        {
            string path = AssetDatabase.GetAssetPath(scene);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(path);

            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
    }
}
