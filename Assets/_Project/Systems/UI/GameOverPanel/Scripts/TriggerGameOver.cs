using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[RequireComponent(typeof(SceneLoader))]
#endif

public class TriggerGameOver : MonoBehaviour
{

    private SceneLoader _sceneLoader;

    void Awake()
    {
        _sceneLoader = GetComponent<SceneLoader>();
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void Respawn()
    {
        if (PlayerManager.Instance)
        {
            PlayerManager.Instance.PlayerStateMachine.ChangeState(PlayerManager.Instance.IdleState);
            PlayerManager.Instance.SoulCounter = 0;
            PlayerManager.Instance.HealthManager.Health = 4;
            PlayerHealthBarManager.Instance.SetHealth(PlayerManager.Instance.HealthManager.Health);

            if (PlayerManager.Instance.Checkpoint != null)
            {
                PlayerManager.Instance.gameObject.transform.position = PlayerManager.Instance.Checkpoint.position;
                _sceneLoader.LoadSceneByName(PlayerManager.Instance.Checkpoint.scene);
            }
        }


    }

}



