using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

// SceneLoader can be include in a gameobject to start some action about loading scenes.
public class SceneLoader : MonoBehaviour
{
    [Header("Event Control")]

    [SerializeField] private bool _isLoadOnEvent = false;

    [SerializeField] private bool _isSingleScene = true;

    [Header("Single Scene")]
    [ShowIf("ShowSingleSceneField")]
    [SerializeField] private string singleScene;

    [ShowIf("_isSingleScene")]
    [SerializeField] private bool loadAdditive = false;

    [Header("Multiple Scenes")]
    [ShowIf("ShowMultipleScenesField")]
    [SerializeField] private List<string> multipleScenes;

    private bool ShowSingleSceneField() => !_isLoadOnEvent && _isSingleScene;
    private bool ShowMultipleScenesField() => !_isLoadOnEvent && !_isSingleScene;

    private bool HasMultipleScenes() => multipleScenes != null && multipleScenes.Count > 0;

    private void Awake()
    {
        if (!_isLoadOnEvent)
            LoadScenes();
    }

    public void LoadScenes()
    {
        if (!string.IsNullOrEmpty(singleScene))
        {
            if (loadAdditive)
                SceneManager.LoadSceneAsync(singleScene, LoadSceneMode.Additive);
            else
                SceneManager.LoadScene(singleScene);
        }
        else if (HasMultipleScenes())
        {
            foreach (var scene in multipleScenes)
                SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        }
    }

    public void LoadSceneByNameWithTransition(string sceneName)
    {
        // UIManager.Instance.Show(PanelType.Transition);
        TransitionPanelManager.Instance.NewTransition(TransitionPanelManager.TransitionType.FadeOut, TransitionPanelManager.TransitionColor.Black);
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        yield return new WaitForSeconds(5.0f);
        LoadSceneByName(sceneName);
    }

    public void LoadSceneByName(string sceneName)
    {
        // When we change to a new scene, we need to stop the character controller to reset the Player position.
        if (PlayerManager.Instance)
        {
            PlayerManager.Instance.PlayerStateMachine.ChangeState(PlayerManager.Instance.IdleState);
            PlayerManager.Instance.CharacterController.enabled = false;
        }

        if (string.IsNullOrEmpty(sceneName)) return;

        if (loadAdditive)
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        else
            SceneManager.LoadScene(sceneName);
    }

    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

